using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDinhSv_ThuCong.xaml
    /// </summary>
    public partial class WpfDinhSv_ThuCong : Window
    {
        public WpfDinhSv_ThuCong()
        {
            InitializeComponent();
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
           // MessageBox.Show("ok");
            Close();
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            if (dtpNgayVay.SelectedDate.Value > dtpNgayRaTr.SelectedDate.Value)
            {
                MessageBox.Show("Sai ngay");
                return;
            }
           // int SoKyDuocNhan = (int) CboSoKyHoc.SelectedValue;
            int SoKyDaNhan = (int) CboSoKyVay.SelectedValue;
          //  int SoKyKhongNhan = SoKyDuocNhan - SoKyDaNhan;
            TimeSpan SoNgay = dtpNgayRaTr.SelectedDate.Value - dtpNgayVay.SelectedDate.Value;
            int Ngay = Convert.ToInt32(SoNgay.TotalDays);
            int SoThang = Ngay / 30;
            int SoThangknhan = ((int)Math.Ceiling((double)SoThang / 6) - SoKyDaNhan) * 6;
            int SoThangTN = SoThang - SoThangknhan;
            int z_thang = SoThang+12+SoThangTN;
            DateTime ng_new = new DateTime();
            ng_new = dtpNgayVay.SelectedDate.Value;
            MessageBox.Show("Thời gian phát tiền vay "+SoThang.ToString()+"\nThời gian không nhận tiền"+SoThangknhan.ToString()
                +"\nThời gian trả nợ "+SoThangTN.ToString()+"\nSố tháng trả nợ : " + z_thang.ToString() + "\nNgày đến hạn cuối cùng : " + ng_new.AddMonths(z_thang).ToString("dd/MM/yyyy"), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            /*
            if (ky1 == ky2)
            {
                TimeSpan SoNgay = dtpNgayRaTr.SelectedDate.Value - dtpNgayVay.SelectedDate.Value;
                int Ngay = Convert.ToInt32(SoNgay.TotalDays);
                int SoThang = Ngay/30;
                SoThang = (SoThang*2) + 12;
                DateTime ngd = new DateTime();
                ngd = dtpNgayVay.SelectedDate.Value;
                MessageBox.Show("Số Tháng : "+SoThang.ToString()+"   Ngày đến hạn cuối cùng : "+ngd.AddMonths(SoThang).ToString("dd/MM/yyyy"),"Thông báo",MessageBoxButton.OK,MessageBoxImage.Information);
            }else if (ky1 > ky2)
            {
                TimeSpan SoNgay = dtpNgayRaTr.SelectedDate.Value - dtpNgayVay.SelectedDate.Value;
                int Ngay = Convert.ToInt32(SoNgay.TotalDays);
                int SoThang = Ngay / 30;
                SoThang = (SoThang * 2) + 12-(ky1-ky2)*6;
                DateTime ngd = new DateTime();
                ngd = dtpNgayVay.SelectedDate.Value;
                MessageBox.Show("Số Tháng : " + SoThang.ToString() + "   Ngày đến hạn cuối cùng : " + ngd.AddMonths(SoThang).ToString("dd/MM/yyyy"), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                MessageBox.Show("Số kỳ học không thể nhỏ hơn số kỳ vay","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            //MessageBox.Show(CboSoKyHoc.SelectedValue.ToString()+"    "+CboSoKyVay.SelectedValue.ToString());
             */

        }

        private void WpfDinhSv_ThuCong_OnLoaded(object sender, RoutedEventArgs e)
        {
            dtpNgayVay.SelectedDate = DateTime.Parse("01/01/" + DateTime.Now.AddYears(-1).ToString("yyyy"));
            dtpNgayRaTr.SelectedDate = DateTime.Now.AddDays(-1);
       
            for (int i = 1; i < 11; i++)
            {
                CboSoKyVay.Items.Add(i);
            }
            CboSoKyVay.SelectedIndex = 3;
        }
    }
}
