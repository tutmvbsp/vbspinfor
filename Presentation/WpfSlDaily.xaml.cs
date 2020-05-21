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
using System.Data;
using DAL;
using BLL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfKhGnTn.xaml
    /// </summary>
    public partial class WpfSlDaily : Window
    {
        public WpfSlDaily()
        {
            InitializeComponent();
        }

        private ClsServer cls = new ClsServer();
        private ServerInfor srv = new ServerInfor();
        private ToolBll str = new ToolBll();
        private DataTable dtpos = new DataTable();
        private DataTable dt = new DataTable();
        private string FileName = "";
        string Thumuc = "C:\\SaoKe";

        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            #region

            try
            {
                if (str.Right(str.Left(CboPos.SelectedValue.ToString().Trim(), 6), 2) == "00")
                {
                    MessageBox.Show("Không chọn POS tổng hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    cls.ClsConnect();
                    int thamso = 6;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@NgayKu";
                    if (dtpNgayKu.SelectedDate != null) giatri[0] = dtpNgayKu.SelectedDate.Value.ToString("yyyy-MM-dd");
                    bien[1] = "@NgayBt";
                    if (dtpNgayBt.SelectedDate != null) giatri[1] = dtpNgayBt.SelectedDate.Value.ToString("yyyy-MM-dd");
                    bien[2] = "@MaPos";
                    giatri[2] = str.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[3] = "@MaXa";
                    giatri[3] = str.Left(cboXa.SelectedValue.ToString().Trim(), 6);
                    //MessageBox.Show(giatri[3].ToString());
                    bien[4] = "@Nguon";
                    if (Ration1.IsChecked == true)
                    {
                        giatri[4] = "1";
                    }
                    else if (Ration2.IsChecked == true)
                    {
                        giatri[4] = "2";
                    }
                    else
                    {
                        giatri[4] = "3";
                    }
                    bien[5] = "@Mau";
                    if (Ration4.IsChecked==true) giatri[5]="1";
                    else if (Ration5.IsChecked == true) giatri[5] = "2";
                    else giatri[5] = "3";

                    if (Ration4.IsChecked == true)
                    {
                        dt = cls.LoadLdbf("usp_SlDaily", bien, giatri, thamso);
                        if (Ration6.IsChecked == true)
                        {
                            rpt_SlDaily rpt = new rpt_SlDaily();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        }
                        else
                        {
                            FileName = Thumuc + "\\" + giatri[2] + "_" + giatri[3] + "_SLTD_XA_" +
                                       dtpNgayBt.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                            str.ExportToExcel(dt, FileName);
                            MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK,
                                MessageBoxImage.Information);
                            //str.OpenExcel(FileName);

                        }
                    }
                    else
                    {
                        if (Ration5.IsChecked == true)
                        {
                            dt = cls.LoadLdbf("usp_SlDailyCT", bien, giatri, thamso);
                            if (Ration6.IsChecked == true)
                            {
                                rpt_SlDailyCt rpt = new rpt_SlDailyCt();
                                RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                            }
                            else
                            {
                                FileName = Thumuc + "\\" + giatri[2] + "_" + giatri[3] + "_SLTD_CHTR_" +
                                           dtpNgayBt.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                str.ExportToExcel(dt, FileName);
                                MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                                //str.OpenExcel(FileName);

                            }
                        }
                        else
                        {
                            dt = cls.LoadLdbf("usp_SlDailyDvut", bien, giatri, thamso);
                        
                        if (Ration6.IsChecked == true)
                            {
                                rpt_SlDaily rpt = new rpt_SlDaily();
                                RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                            }
                            else
                            {
                                FileName = Thumuc + "\\" + giatri[2] + "_" + giatri[3] + "_SLTD_DVUT_" +
                                           dtpNgayBt.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                str.ExportToExcel(dt, FileName);
                                MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                                //str.OpenExcel(FileName);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();

            #endregion
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
            try
            {
                cls.ClsConnect();
                string sql = "select PO_MA,PO_TEN from DMPOS order by PO_MA";
                dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                //CboPos.SelectedIndex = 5;
                DataTable dtng = new DataTable();
                dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGKU,MAX(convert(date,NGAYBT,105)) as NGBT from U_HSTD");
                dtpNgayKu.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGKU"].ToString());
                dtpNgayBt.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGBT"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Mess",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }

        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //MessageBox.Show(str.Left(cboPos.SelectedValue.ToString().Trim(),6));
                cboXa.Items.Clear();
                cls.ClsConnect();
                DataTable dtxa = new DataTable();
                string sql = "select MA,TEN from DMXA where PGD_QL= " + "'" + str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "'" + " order by MA";
                dtxa = cls.LoadDataText(sql);
                for (int i = 0; i < dtxa.Rows.Count; i++)
                {
                    cboXa.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                }
                cboXa.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }
  
    }
}
