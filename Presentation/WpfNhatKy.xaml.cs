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
using DAL;
using BLL;
using System.Data;
using System.Windows.Forms;
using System.IO;
using MessageBox = System.Windows.MessageBox;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfLaiTon.xaml
    /// </summary>
    public partial class WpfNhatKy : Window
    {
        public WpfNhatKy()
        {
            InitializeComponent();
        }

        ToolBll s = new ToolBll();
        DataTable dt = new DataTable();
        private readonly ClsServer cls = new ClsServer();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
            try
            {
                cls.ClsConnect();
                var dtpos = cls.LoadDataText("select PO_MA,PO_TEN from DMPOS order by PO_MA");
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
     
                dtpNgay.SelectedDate = DateTime.Now;
                var dtv = cls.LoadDataText("select * from NHATKY_HOTRO order by NGAY");
                dataGrid.ItemsSource = dtv.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }



  

        private void BtnThem_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                    string sqladd =
                        "insert into NHATKY_HOTRO(NGAY,POS_YC,PHANHE,NGUOI_YC,NGUOI_XL,NOIDUNG,STT)" +
                        " Values('"+dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd")+"','"+s.Left(CboPos.SelectedValue.ToString(),6)+"','"
                        + CboChuDe.SelectionBoxItem + "',N'" + CboUser.SelectedValue + "',N'" + BienBll.NdTen.Trim() + "',N'" +TxtNoiDung.Text + "',N'" +0+ "')";
                    // MessageBox.Show(sqladd);
                    cls.UpdateDataText(sqladd);
                    MessageBox.Show("OK", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lổi " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            cls.DongKetNoi();
        }


        private void BtnSua_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Chưa làm " , "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lổi " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            cls.DongKetNoi();
        }

        private void BtnXoa_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Chưa làm ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lổi " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            cls.DongKetNoi();
        }

     
     
  

    

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
           Close();
        }

        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                var dtuser = cls.LoadDataText("select ND_MA,ND_TEN from NG_DUNG where ND_MADV= " + "'" + s.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "'" + " order by ND_TEN");
                CboUser.ItemsSource = dtuser.DefaultView;
                CboUser.SelectedValuePath = "ND_TEN";
                CboUser.DisplayMemberPath = "ND_TEN";
                CboUser.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }
    }
}
