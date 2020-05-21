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
    public partial class WpfQttcNhapTay : Window
    {
        public WpfQttcNhapTay()
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
            string str = "";
           // dtpNgay.SelectedDate = DateTime.Parse("31/12/" + DateTime.Now.AddYears(-1).ToString("yyyy"));
            cls.ClsConnect();
            if (BienBll.NdMadv == BienBll.MainPos)
            {
                str = "select * from VARMCN where MAU='QT' and NHAPTAY='T'";
            }
            else
            {
                string field = "CN" + bll.Right(BienBll.NdMadv, 2);
                str = "select STT,TENBIEN," + field + " from VARMCN where MAU='QT'and NHAPTAY='T' ";
            }
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
                        bien[0] = "@STT";
                        giatri[0] = dr[0];
                        bien[1] = "@GIATRI";
                        giatri[1] = dr[2];
                        bien[2] = "@CN01";
                        giatri[2] = dr[3];
                        bien[3] = "@CN02";
                        giatri[3] = dr[4];
                        bien[4] = "@CN03";
                        giatri[4] = dr[5];
                        bien[5] = "@CN04";
                        giatri[5] = dr[6];
                        bien[6] = "@CN05";
                        giatri[6] = dr[7];
                        bien[7] = "@CN06";
                        giatri[7] = dr[8];
                        bien[8] = "@CN07";
                        giatri[8] = dr[9];
                        bien[9] = "@CN08";
                        giatri[9] = dr[10];
                        cls.UpdateDataProcPara("usp_UpdateVARMCN", bien, giatri, thamso);
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
