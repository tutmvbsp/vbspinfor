using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DAL;
using BLL;
using System.Data;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDvut.xaml
    /// </summary>
    public partial class WpfKTNB_Set : Window
    {
        public WpfKTNB_Set()
        {

            InitializeComponent();
        }

        private ClsServer cls = new ClsServer();
        private DataTable dt = new DataTable();
        private string Mau = "";
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            var dtpos = cls.LoadDataText("select KT_STT_HT,KT_DKT from ktnb01 where MAU='00' order by MAU,KT_KHOA");
            CboMau.ItemsSource = dtpos.DefaultView;
            CboMau.DisplayMemberPath = "KT_DKT";
            CboMau.SelectedValuePath = "KT_STT_HT";

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
                cls.ClsConnect();
                string mau = CboMau.SelectedValue.ToString().Trim();
                foreach (DataRow dr in dt.Rows)
                    {
                        string strup = "update KTNB01 set KT_STT_HT='" + dr["KT_STT_HT"] + "',KT_DKT=N'" + dr["KT_DKT"] + "',INDAM='" +
                                       dr["INDAM"] + "',KT_CAPHT='" + dr["KT_CAPHT"] + "',KT_AUTH='" + dr["KT_AUTH"]  +
                                       "' where MAU='" + mau + "' and KT_KHOA='" + dr["KT_KHOA"]+ "'";
                       // MessageBox.Show(strup);
                        cls.UpdateDataText(strup);
                    }
                    MessageBox.Show("Lưu thành công !", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();

        }

        private void LblGetData_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                string mau = CboMau.SelectedValue.ToString().Trim();

                dt = cls.LoadDataText("select * from KTNB01 where MAU='"+mau+ "' order by KT_KHOA");
                if (dt.Rows.Count > 0)
                {
                    dgvData.ItemsSource = dt.DefaultView;
                }
                else MessageBox.Show("Không có xã nào !", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();
        }

   }
}
