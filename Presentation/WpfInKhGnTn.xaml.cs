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
    /// Interaction logic for WpfInKhGnTn.xaml
    /// </summary>
    public partial class WpfInKhGnTn : Window
    {
        public WpfInKhGnTn()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        ServerInfor srv = new ServerInfor();
        ClsServer cls = new ClsServer();
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(BienBll.NdMadv.Trim());
            cls.ClsConnect();
            if (ChkTongHop.IsChecked == false)
            {
                int thamso = 3;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@TuNgay";
                if (dtpTuNgay.SelectedDate != null)
                    giatri[0] = dtpTuNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                else
                {
                    MessageBox.Show("Chọn ngày : ");
                    return;
                }
                bien[1] = "@DenNgay";
                if (dtpTuNgay.SelectedDate != null)
                    giatri[1] = dtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                else
                {
                    MessageBox.Show("Chọn ngày : ");
                    return;
                }

                bien[2] = "@MaPos";
                giatri[2] = BienBll.NdMadv.Trim();
                // MessageBox.Show(giatri[0] + "  " + giatri[1]);
                dt = cls.LoadDataProcPara("usp_KhGnTn", bien, giatri, thamso);
                if (dt.Rows.Count > 0)
                {
                   // dataGrid1.ItemsSource = dt.DefaultView;
                    rpt_KhGnTn rpt = new rpt_KhGnTn();
                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                     srv.DbPassSerVer());
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu !", "Mess");
                }
            }
            else if (Ration1.IsChecked==true)
            {
                int thamso = 2;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@TuNgay";
                if (dtpTuNgay.SelectedDate != null)
                    giatri[0] = dtpTuNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                else
                {
                    MessageBox.Show("Chọn ngày : ");
                    return;
                }
                bien[1] = "@DenNgay";
                if (dtpTuNgay.SelectedDate != null)
                    giatri[1] = dtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                else
                {
                    MessageBox.Show("Chọn ngày : ");
                    return;
                }

                dt = cls.LoadDataProcPara("usp_KhGnTn01", bien, giatri, thamso);
                if (dt.Rows.Count > 0)
                {
                    //dataGrid1.ItemsSource = dt.DefaultView;
                    rpt_KhGnTn01 rpt = new rpt_KhGnTn01();
                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                     srv.DbPassSerVer());
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu !", "Mess");
                }
                
            }
            else
            {
                int thamso = 2;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@TuNgay";
                if (dtpTuNgay.SelectedDate != null)
                    giatri[0] = dtpTuNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                else
                {
                    MessageBox.Show("Chọn ngày : ");
                    return;
                }
                bien[1] = "@DenNgay";
                if (dtpTuNgay.SelectedDate != null)
                    giatri[1] = dtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                else
                {
                    MessageBox.Show("Chọn ngày : ");
                    return;
                }

                dt = cls.LoadDataProcPara("usp_KhGnTn02", bien, giatri, thamso);
                if (dt.Rows.Count > 0)
                {
                    //dataGrid1.ItemsSource = dt.DefaultView;
                    rpt_KhGnTn02 rpt = new rpt_KhGnTn02();
                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu !", "Mess");
                }

            }
            cls.DongKetNoi();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtpTuNgay.SelectedDate = DateTime.Now.AddDays(-7);
            GrpMau.IsEnabled = false;
        }

        private void dtpTuNgay_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dtpDenNgay.SelectedDate = dtpTuNgay.SelectedDate.Value.AddDays(6); //DateTime.Now.AddDays(-1);
        }

        private void ChkTongHop_Click(object sender, RoutedEventArgs e)
        {
            Ration1.IsChecked = true;
            if (ChkTongHop.IsChecked == true)
            {
                GrpMau.IsEnabled = true;
            }
            else
            {
                GrpMau.IsEnabled = false;
            }

        }

    
    }
}
