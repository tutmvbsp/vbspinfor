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
using CrystalDecisions.Shared;
using DAL;
using BLL;
using System.Data;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfLaiTon.xaml
    /// </summary>
    public partial class WpfLaiTon : Window
    {
        public WpfLaiTon()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ToolBll bll  = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        string Thumuc = "C:\\KT740";
        private string FileName = "";
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
            try
            {
                cls.ClsConnect();
                //DataTable dtpos = new DataTable();
                string sql = "select PO_MA,PO_TEN from DMPOS order by PO_MA";                
                var dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                //DataTable dtng = new DataTable();
                var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGKU,MAX(convert(date,NGAYBT,105)) as NGBT from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGKU"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();

        }

        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CboXa.Items.Clear();
                cls.ClsConnect();
                CboXa.Items.Add("0000000 | ALL");
                CboTo.Items.Add("0000000 | ALL");
                string sql = "select MA,TEN from DMXA where PGD_QL= " + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) +  " order by MA";
                var dtxa = cls.LoadDataText(sql);
                for (int i = 0; i < dtxa.Rows.Count; i++)
                {
                    CboXa.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                }
                CboXa.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }

        }

        private void CboXa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CboTo.Items.Clear();
                cls.ClsConnect();
                CboTo.Items.Add("0000000 | ALL");
                string sql = "select TO_MATO,TO_TENTT from HSTO where TRANGTHAI='A' and Left(TO_MADP,6) = " + bll.Left(CboXa.SelectedValue.ToString().Trim(), 6) + " order by TO_MATO";
                //MessageBox.Show(sql);
                var dtto = cls.LoadDataText(sql);
                for (int i = 0; i < dtto.Rows.Count; i++)
                {
                    CboTo.Items.Add(dtto.Rows[i][0] + " | " + dtto.Rows[i][1]);
                }                
                CboTo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }

        }

  
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            
            cls.ClsConnect();
                try
                {
                    int thamso = 5;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@Mato";
                    if (CboTo != null)
                        giatri[0] = bll.Left(CboTo.SelectedValue.ToString().Trim(), 7);
                    else
                    {
                        MessageBox.Show("Chọn Tổ", "Mess");
                        return;
                    }
                    bien[1] = "@Ngay";
                    if (dtpNgay.SelectedDate != null)
                        giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    else
                    {
                        MessageBox.Show("Chọn Ngày", "Mess");
                        return;
                    }
                    bien[2] = "@MaPos";
                    giatri[2] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[3] = "@Mau";
                    if (Ration1.IsChecked == true)
                    {
                        giatri[3] = '1';
                        FileName = Thumuc + "\\" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_LAITHANG_XA_"+bll.Left(CboTo.SelectedValue.ToString().Trim(), 7)+"_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                    }
                    else
                    {
                        giatri[3] = '2';
                        FileName = Thumuc + "\\" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_LAITHANG_CHTR_" + bll.Left(CboTo.SelectedValue.ToString().Trim(), 7) + "_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                    }
                    bien[4] = "@MaXa";
                    giatri[4] = bll.Left(CboXa.SelectedValue.ToString().Trim(), 6);

                dt = cls.LoadDataProcPara("usp_LAIDT", bien, giatri, thamso);
                    if (dt.Rows.Count > 0)
                    {
                    if (Ration1.IsChecked == true)
                        {
                            rpt_LAIDT rpt = new rpt_LAIDT();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        }
                    else
                        {
                            bll.ExportToExcel(dt, FileName);
                            MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            bll.OpenExcel(FileName);
                        }
                    }               
                    else
                    {
                        MessageBox.Show("Không có bản ghi nào ", "Mess");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

        }   
   
    }
}
