using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using DAL;
using BLL;
using System.Data;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDvut.xaml
    /// </summary>
    public partial class WpfKTNB03 : Window
    {
        public WpfKTNB03()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ClsOracle ora = new ClsOracle();
        ToolBll str = new ToolBll();
        //ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        string Thumuc = "C:\\KT740";
        private string FileName = "";
        private string FileName1 = "";
        private string FileName2 = "";
        private string strstr1 = "";
        private string strstr2 = "";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            string sqlMau = "select * from KTNB01 where MAU='00' order by KT_KHOA";
            var dtMau = cls.LoadDataText(sqlMau);
            CboMau.ItemsSource = dtMau.DefaultView;
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
                 string mau = CboMau.SelectedValue.ToString().Trim();
                 //MessageBox.Show(mau);
                 switch (mau)
                        {
                    case "01":
                            {
                                var f = new WpfKTNB_01();
                                f.ShowDialog();
                             }
                             break;
                    case "02":
                        {
                            var f = new WpfKTNB_02();
                            f.ShowDialog();
                        }
                        break;
                    case "03":
                        {
                            var f = new WpfKTNB_03();
                            f.ShowDialog();
                        }
                        break;
                    case "04":
                        {
                            var f = new WpfKTNB_04();
                            f.ShowDialog();
                        }
                        break;
                    case "05":
                        {
                            var f = new WpfKTNB_05();
                            f.ShowDialog();
                        }
                        break;

                    case "06":

                            {
                                var f = new WpfKTNB_06();
                                f.ShowDialog();
                            }
                            break;
                    case "07":
                        {
                            var f = new WpfKTNB_07();
                            f.ShowDialog();
                        }
                        break;
                    case "08":
                        {
                            var f = new WpfKTNB_08();
                            f.ShowDialog();
                        }
                        break;

                    default:
                                MessageBox.Show("None");
                                break;
                        }

            }

            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
            ora.DongKetNoi();
        }

   
    }
}
