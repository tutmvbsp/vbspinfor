using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using DAL;
using BLL;
using System.Data;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDvut.xaml
    /// </summary>
    public partial class WpfGiaoNhanTV : Window
    {
        public WpfGiaoNhanTV()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ToolBll str = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        string Thumuc = "C:\\Saoke";
       // private string strsql = "";
        private string FileName = "";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
            dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
            //string sql = "select PO_MA,PO_TEN from DMPOS where PO_MACN=" + "'" + BienBll.MainPos + "'" + " order by PO_MA";
            var sql = BienBll.NdCapbc.Trim() == "1" ? string.Format("select PO_MA,PO_TEN from DMPOS where PO_MA='{0}'", BienBll.NdMadv.Trim()) : "select PO_MA,PO_TEN from DMPOS where right(PO_MA,2)<>'00'";
            var dtpos = cls.LoadDataText(sql);
            for (int i = 0; i < dtpos.Rows.Count; i++)
            {
                CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
            }
   
            cls.DongKetNoi();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                str.TaoThuMuc(Thumuc);
                dtNew = dt.Clone();
                foreach (DataRow dr in dt.Rows)
                {
                    if ((bool)dr[0] == true)
                    {
                        dtNew.ImportRow(dr);
                    }
                }

                //dtNew = dt.GetChanges();
                if (dtNew == null || dtNew.Rows.Count == 0)
                {
                    MessageBox.Show("Chưa có thay đổi ngày nào !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    rpt_GiaoNhanTVM01 rpt = new rpt_GiaoNhanTVM01();
                    RPUtility.ShowRp(rpt, dtNew, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                    rpt_GiaoNhanTVM02 rpt1 = new rpt_GiaoNhanTVM02();
                    RPUtility.ShowRp(rpt1, dtNew, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                    dgvSource.ItemsSource = null;
                    FileName = Thumuc + "\\" + str.Left(CboXa.SelectedValue.ToString().Trim(), 6) +"_"+ str.Left(CboToGiao.SelectedValue.ToString().Trim(), 7)+"_"+ str.Left(CboToNhan.SelectedValue.ToString().Trim(), 7) + "_" + "_GIAONHAN_TV_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                    str.ExportToExcel(dtNew, FileName);
                    MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    str.OpenExcel(FileName);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
            
        }

        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //MessageBox.Show(str.Left(cboPos.SelectedValue.ToString().Trim(),6));
                if (str.Left(CboPos.SelectedValue.ToString().Trim(), 6) != "003000")
                {
                    CboXa.Items.Clear();
                    cls.ClsConnect();
                    string sql = "select MA,TEN from DMXA where PGD_QL= " + "'" +
                                 str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "' and right(MA,2)<>'00' order by MA";
                    var dtxa = cls.LoadDataText(sql);
                    for (int i = 0; i < dtxa.Rows.Count; i++)
                    {
                        CboXa.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                    }
                }
                else
                {
                   // CboXa.Items.Add("003000 | Tất cả");
                    MessageBox.Show("Không chọn POS 003000", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
               // CboXa.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }


        private void LblGetData_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

                cls.ClsConnect();
                int thamso = 3;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@ToGiao";
                giatri[0] = str.Left(CboToGiao.SelectedValue.ToString().Trim(), 7);
                bien[1] = "@Ngay";
                if (dtpNgay.SelectedDate != null)
                    giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                else
                {
                    MessageBox.Show("Chưa chọn ngày ", "Mess");
                    return;
                }
                bien[2] = "@ToNhan";
                giatri[2] = str.Left(CboToNhan.SelectedValue.ToString().Trim(), 7);
                dt = cls.LoadDataProcPara("usp_GiaoNhanTV", bien, giatri, thamso);
                //rpt_kt740_01 rpt = new rpt_kt740_01();
                if (dt.Rows.Count > 0)
                {
                    dgvSource.ItemsSource = dt.DefaultView;
                    // rpt_SkeTo rpt = new rpt_SkeTo();
                    // RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                    // string filename = "C:\\Tam\\" + str.Left(cboTo.SelectedValue.ToString().Trim(), 7) + ".xlsx";
                    // bll.WriteDataTableToExcel(dt, "Person Details", filename, "Details");
                    //dtNew = dt.GetChanges();
                }
                else
                {
                    MessageBox.Show("Chưa có số liệu", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();
        }

        private void CboXa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (str.Left(CboPos.SelectedValue.ToString().Trim(), 6) != "003000")
                {
                    CboToGiao.Items.Clear();
                    CboToNhan.Items.Clear();
                    cls.ClsConnect();
                    string sql = "select TO_MATO,TO_TENTT from HSTO where LEFT(TO_MADP,6)=" + str.Left(CboXa.SelectedValue.ToString().Trim(), 6) + " order by TO_MATO";
                   // MessageBox.Show(sql);
                    var dtxa = cls.LoadDataText(sql);
                    for (int i = 0; i < dtxa.Rows.Count; i++)
                    {
                        CboToGiao.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                        CboToNhan.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                    }
                }
                else
                {
                   // CboXa.Items.Add("003000 | Tất cả");
                    MessageBox.Show("Không chọn POS 003000", "Mess",MessageBoxButton.OK,MessageBoxImage.Warning);
                }
               // CboXa.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }
    }
}
