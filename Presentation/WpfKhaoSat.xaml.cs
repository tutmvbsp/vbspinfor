using System;
using System.Data;
using System.Windows;
using System.Text;
using System.IO;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for Wpf_kt740.xaml
    /// </summary>
    public partial class WpfKhaoSat : Window
    {

        public WpfKhaoSat()
        {
            InitializeComponent();
        }

        private ToolBll bll = new ToolBll();
        private ServerInfor srv = new ServerInfor();
        private ClsServer cls = new ClsServer();
        private DataTable dt = new DataTable();
        private string Mau = "";
        private string CT = "";
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                var dtpos = cls.LoadDataText("select PO_MA,PO_TEN from DMPOS where PO_MA='" + BienBll.NdMadv.Trim()+"'");
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                CboPos.SelectedIndex = 0;
                var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();

        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Mau);
        }

        private void Ration01_Checked(object sender, RoutedEventArgs e)
        {
            WpfKSM01 f = new WpfKSM01();
            f.ShowDialog();
        }

        private void Ration02_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Mẩu này sẽ tổng hợp khi nhập đủ tại mẩu 01", "Thông báo", MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        private void Ration03_Checked(object sender, RoutedEventArgs e)
        {
            Mau = "M03";
            CT = "01";
            WpfKSM03 f = new WpfKSM03(Mau,CT);
            f.ShowDialog();
        }

        private void Ration04_Checked(object sender, RoutedEventArgs e)
        {
            Mau = "M04";
            CT = "19";
            WpfKSM03 f = new WpfKSM03(Mau,CT);
            f.ShowDialog();
        }

        private void Ration05_Checked(object sender, RoutedEventArgs e)
        {
            Mau = "M05";
            CT = "10";
            WpfKSM05 f = new WpfKSM05(Mau,CT);
            f.ShowDialog();
        }

        private void Ration09_Checked(object sender, RoutedEventArgs e)
        {
            Mau = "M09";
            CT = "04";
            WpfKSM03 f = new WpfKSM03(Mau, CT);
            f.ShowDialog();
        }

        private void Ration11_Checked(object sender, RoutedEventArgs e)
        {
            Mau = "M11";
            CT = "18";
            WpfKSM04 f = new WpfKSM04(Mau, CT);
            f.ShowDialog();

        }

        private void Ration12_Checked(object sender, RoutedEventArgs e)
        {
            Mau = "M12";
            CT = "15";
            WpfKSM04 f = new WpfKSM04(Mau, CT);
            f.ShowDialog();
        }

        private void Ration13_Checked(object sender, RoutedEventArgs e)
        {
            Mau = "M13";
            CT = "07";
            WpfKSM04 f = new WpfKSM04(Mau, CT);
            f.ShowDialog();

        }

        private void Ration07_Checked(object sender, RoutedEventArgs e)
        {
            Mau = "M07";
            CT = "06";
            WpfKSM05 f = new WpfKSM05(Mau, CT);
            f.ShowDialog();
        }

        private void Ration10_Checked(object sender, RoutedEventArgs e)
        {
            Mau = "M10";
            CT = "03";
            WpfKSM05 f = new WpfKSM05(Mau, CT);
            f.ShowDialog();
        }

        private void Ration08_Checked(object sender, RoutedEventArgs e)
        {
            Mau = "M08";
            CT = "02";
            WpfKSM06 f = new WpfKSM06(Mau, CT);
            f.ShowDialog();
        }

        private void Ration14_Checked(object sender, RoutedEventArgs e)
        {
            Mau = "M14";
            CT = "09";
            WpfKSM07 f = new WpfKSM07(Mau, CT);
            f.ShowDialog();
        }

        private void Ration06_Checked(object sender, RoutedEventArgs e)
        {
            Mau = "M06";
            CT = "11";
            WpfKSM08 f = new WpfKSM08(Mau, CT);
            f.ShowDialog();

        }
    }
}
