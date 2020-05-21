using System;
using System.Data;
using System.Windows;
using System.ComponentModel;
using System.Windows.Input;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfLaiTon.xaml
    /// </summary>
    public partial class WpfKhCbtdM
    {
        public WpfKhCbtdM()
        {
            InitializeComponent();
        }

        BackgroundWorker _worker = new BackgroundWorker();
        ToolBll s = new ToolBll();
        ClsServer cls = new ClsServer();
        DataTable dt = new DataTable();
        ServerInfor srv = new ServerInfor();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // GroupBox.IsEnabled = false;
            try
            {
                cls.ClsConnect();
                
                //DataTable dtpos;
                var sql = BienBll.NdCapbc.Trim() == "1" ? string.Format("select PO_MA,PO_TEN from DMPOS where PO_MA='{0}'", BienBll.NdMadv.Trim()) : "select PO_MA,PO_TEN from DMPOS";
                var dtpos = cls.LoadDataText(sql);
                for (var i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                CboPos.SelectedIndex = BienBll.NdCapbc.Trim() == "1" ? 0 : 5;
                var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                DtpDenNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());

                //var ngay = DtpDenNgay.SelectedDate.Value.AddMonths(-1);
                //DtpNgay.SelectedDate = DateTime.Parse(ngay.ToString("yyyy-MM") + "-" + DateTime.DaysInMonth(ngay.Year, ngay.Month));
                

                //DtpDenNgay.SelectedDate = DateTime.Parse(DtpNgay.SelectedDate.Value.ToString("yyyy-MM") + "-" + DateTime.DaysInMonth(DtpNgay.SelectedDate.Value.Year, DtpNgay.SelectedDate.Value.Month).ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
            
        }

        
        private void btnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                const int thamso = 3;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@MaPos";
                giatri[0] = s.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                bien[1] = "@DenNgay";
                if (DtpDenNgay.SelectedDate == null) return;
                giatri[1] = DtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                bien[2] = "@Mau";
                // MessageBox.Show(giatri[1].ToString()+"  "+giatri[2].ToString());
                if (RadioButton1.IsChecked == true)
                {
                    giatri[2] = "1";
                }
                else if (RadioButton2.IsChecked == true)
                {
                    giatri[2] = "2";
                }
                else
                {
                    giatri[2] = "3";
                }
                dt = cls.LoadDataProcPara(RadioButton4.IsChecked != true ? "usp_KH_CBTD_MOI" : "usp_Export_KH_CBTD_MOI", bien, giatri, thamso);
                if (dt.Rows.Count > 0)
                {
                        if ((string) giatri[0] == "003000" || (RadioButton3.IsChecked == true))
                        {
                            
                            rpt_KH_XA_MOI rpt = new rpt_KH_XA_MOI();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                srv.DbPassSerVer());
                        }
                        else
                        {
                            rpt_KH_CBTD_MOI rpt = new rpt_KH_CBTD_MOI();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                        }

                    /*
                    else if (RadioButton1.IsChecked == true)
                    {
                        rpt_KH_CBTD rpt = new rpt_KH_CBTD();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                            srv.DbPassSerVer());
                    }
                    else if (RadioButton2.IsChecked == true)
                    {
                        rpt_KH_CBKT rpt = new rpt_KH_CBKT();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                            srv.DbPassSerVer());
                    }
                    */
                }
                else if (RadioButton4.IsChecked == true)
                {
                    MessageBox.Show("insert OK");
                }
                else
                {

                    MessageBox.Show("Không có số liệu", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                cls.DongKetNoi();
            }

        }
        private void lblMess_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (BienBll.ChucVu.Trim() == "3" || BienBll.ChucVu.Trim() == "4")
            {
                MessageBox.Show("Bạn không vào mục này !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                WpfNhapTGDC f = new WpfNhapTGDC();
                f.ShowDialog();

            }

        }

    }
}
