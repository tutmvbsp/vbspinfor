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
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfLuong01.xaml
    /// </summary>
    public partial class WpfQttcNhapKH : Window
    {
        public WpfQttcNhapKH()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        DataTable dt = new DataTable();
        DataTable dtnew = new DataTable();
        ToolBll bll = new ToolBll();
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            string field = "KH" + bll.Right(BienBll.NdMadv.Trim(), 2);
            string str = "select STT,CHITIEU," + field + " KH from KHTC where NHAPTAY='T'";
            dt = cls.LoadDataText(str);
            dgvData.ItemsSource = dt.DefaultView;
            cls.DongKetNoi();    
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dtnew == null || dtnew.Rows.Count==0)
            {
                MessageBox.Show("Chưa có giá trị nào thay đối", "Mess", MessageBoxButton.OK,
                MessageBoxImage.Information);

            }
            else
            {
                try
                {
                    cls.ClsConnect();
                    int thamso = 10;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    foreach (DataRow dr in dtnew.Rows)
                    {
                        string field = "KH" + bll.Right(BienBll.NdMadv.Trim(), 2);
                        bien[0] = "@STT";
                        giatri[0] = dr["STT"];
                        bien[1] = "@GIATRI";
                        giatri[1] = dr["KH"];
                        cls.UpdateDataText("update KHTC set " + field + "=" + giatri[1] + " where STT='" + giatri[0] +"'");
                    }
                    MessageBox.Show("Update OK", "Mess", MessageBoxButton.OK,
                MessageBoxImage.Information);
                    cls.DongKetNoi();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dtnew = dt.GetChanges();
                if (dtnew == null)
                {
                    MessageBox.Show("Chưa có giá trị nào thay đối", "Mess", MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                }
                else
                {
                    dgvTarGet.ItemsSource = dtnew.DefaultView;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    
    }
}
