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
    public partial class WpfRsGqvl : Window
    {
        public WpfRsGqvl()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ToolBll bll = new ToolBll();
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
            var firstDayOfMonth = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month, 1);
            var lastDay = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month, 25);
            //dtpTuNgay.SelectedDate = firstDayOfMonth;
            //dtpDenNgay.SelectedDate = lastDay;
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
                /*
                str.TaoThuMuc(Thumuc);
                dtNew = dt.Clone();
                foreach (DataRow dr in dt.Rows)
                {
                    if ((bool)dr[0] == true)
                    {
                        dtNew.ImportRow(dr);
                    }
                }
                */
                dtNew = dt.GetChanges();
                if (dtNew == null || dtNew.Rows.Count == 0) MessageBox.Show("Chưa có thay đổi ngày nào !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    rpt_RsGqvl rpt = new rpt_RsGqvl();
                    RPUtility.ShowRp(rpt, dtNew, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());

                    cls.ClsConnect();
                    foreach (DataRow dr in dtNew.Rows)
                    {
                        string upd = "update LUU_GQVL set TT_CHECK='Y' where KU_SOKU='" + dr["KU_SOKU"] + "' and TT_CHECK='N'";
                        cls.UpdateDataText(upd);
                    }
                    MessageBox.Show("Lưu thành công ! " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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
                if (bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) != "003000")
                {
                    CboXa.Items.Clear();
                    cls.ClsConnect();
                    string sql = "select MA,TEN from DMXA where PGD_QL= " + "'" +
                                 bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "' and right(MA,2)<>'00' order by MA";
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
            string str = "";
            try
            {

                cls.ClsConnect();
                string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                string xa = bll.Left(CboXa.SelectedValue.ToString().Trim(), 6);
                string pos = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                int thamso = 2;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                string Ngay = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                bien[0] = "@MaXa";
                giatri[0] = xa;
                bien[1] = "@Ngay";
                giatri[1] = Ngay;
                cls.UpdateDataProcPara("usp_RsGqvl", bien, giatri, thamso);
                if (chkInLai.IsChecked==true)
                    str = "select * from LUU_GQVL where MAXA='" + xa + "' order by KU_MADP,KU_MATO,KH_MAKH";
                else str = "select * from LUU_GQVL where MAXA='" + xa + "' and TT_CHECK='N'order by KU_MADP,KU_MATO,KH_MAKH";
                dt = cls.LoadDataText(str);
                if (dt.Rows.Count > 0)
                {
                    dgvSource.ItemsSource = dt.DefaultView;
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



        private void ChkAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach (DataRow dr in dt.Rows)
            {
                dr["CHON"] = true;
            }
        }

        private void ChkAll_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (DataRow dr in dt.Rows)
            {
                dr["CHON"] = false;
            }

        }
        private void DatePicker_SelectedDateChanged(object sender, RoutedEventArgs e)
        {
            //if (dtpTuNgay.SelectedDate != null)
            //{
            //    var lastDay = new DateTime(dtpTuNgay.SelectedDate.Value.Year, dtpTuNgay.SelectedDate.Value.AddMonths(1).Month, 25);
            //    dtpDenNgay.SelectedDate = lastDay;
            //}
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
