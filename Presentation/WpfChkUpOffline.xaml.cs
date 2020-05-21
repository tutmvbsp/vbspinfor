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
using System.ComponentModel;
using System.Data;
using System.IO;
using DAL;
using BLL;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDelete.xaml
    /// </summary>
    public partial class WpfChkUpOffline : Window
    {
        public WpfChkUpOffline()
        {
            InitializeComponent();
        }
        readonly ClsServer _cls = new ClsServer();
        ToolBll str = new ToolBll();
        DataTable _dt = new DataTable();
        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _cls.ClsConnect();
                ListBox.Items.Clear();
                ListUp.Items.Clear();
                ListBak.Items.Clear();
                //------------------------------
                DirectoryInfo dirbak = new DirectoryInfo(txtPathBak.Text.Trim());
                FileInfo[] filesbak = dirbak.GetFiles("*.bak*");
                foreach (FileInfo filebak in filesbak)
                {
                    string DiemGd = filebak.Name.Trim();
                    string Ngay = DiemGd.Substring(17, 8);//str.Right(DiemGd, 8);
                    string Maxa = DiemGd.Substring(11, 6);
                    if (Ngay == dtpNgay.SelectedDate.Value.ToString("yyyyMMdd"))
                    {
                        //MessageBox.Show("Ma xa : " + Maxa + "   Ngay : " + Ngay);
                        string sqlgd = "select MA MAXA, TEN TENXA from DMXA where MA='" + Maxa + "'";
                        var dtup = _cls.LoadDataText(sqlgd);
                        foreach (DataRow dr in dtup.Rows)
                        {
                            ListBak.Items.Add(dr["MAXA"] + " | " + dr["TENXA"]);
                        }
                        
                    }
                }

                //------------------------------
                DirectoryInfo dir = new DirectoryInfo(txtPath.Text.Trim());
                FileInfo[] files = dir.GetFiles("*.Offline*");
                foreach (FileInfo file in files)
                {
                    string DiemGd = file.Name.Trim();
                    string Ngay = DiemGd.Substring(10,8);//str.Right(DiemGd, 8);
                    string Maxa = DiemGd.Substring(4, 6);
                    if (Ngay == dtpNgay.SelectedDate.Value.ToString("yyyyMMdd"))
                    {
                        //MessageBox.Show("Ma xa : " + Maxa + "   Ngay : " + Ngay);
                        string sqlgd = "select MA MAXA, TEN TENXA from DMXA where MA='"+Maxa+"'";
                        var dtup = _cls.LoadDataText(sqlgd);
                        foreach (DataRow dr in dtup.Rows)
                        {
                            ListBox.Items.Add(dr["MAXA"] + " | " + dr["TENXA"]);    
                        }
                        
                    }
                }
                if (ListBox.Items.Count == 0)
                {
                    MessageBox.Show("Ngày : " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy")+" không có giao dịch tại xã !", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.None);
                }
                else
                {
                    string sql =
                        "select right(a.TransCd,6) MAXA,b.TEN TENXA from OfflineUp a,DMXA b where a.NGAYOFL='" +
                        dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") +
                        "' and RIGHT(a.TransCd,6)=b.MA order by a.TransCd";
                    _dt = _cls.LoadDataText(sql);
                    //DataGrid.ItemsSource = _dt.DefaultView;
                    foreach (DataRow dr in _dt.Rows)
                    {
                        ListUp.Items.Add(dr["MAXA"] + " | " + dr["TENXA"]);
                    }
                    if (ListBox.Items.Count == ListUp.Items.Count)
                    {
                        ListBox.Items.Clear();
                        MessageBox.Show("OK Đã Upload All", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.None);
                    }
                    else
                    {
                        for (int i = 0; i < ListBox.Items.Count; i++)
                        {

                            for (int j = 0; j < ListUp.Items.Count; j++)
                            {
                                //MessageBox.Show(str.Left(ListBox.Items[i].ToString().Trim(), 6) + "       " + str.Left(ListUp.Items[j].ToString().Trim(), 6));
                                if (str.Left(ListBox.Items[i].ToString().Trim(), 6) ==
                                    str.Left(ListUp.Items[j].ToString().Trim(), 6))
                                {
                                    //MessageBox.Show("Giatri I: " + i.ToString() + "Gia tri list box" + ListBox.Items.Count.ToString());
                                    ListBox.Items.RemoveAt(i);
                                }
                                //MessageBox.Show(ListBox.Items[i].ToString() + "       " + ListUp.Items[j].ToString());
                            }

                        }
                    }
                }
                //MessageBox.Show(sql, "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _cls.DongKetNoi();
            }
        }
        private void WpfDelete_OnLoaded(object sender, RoutedEventArgs e)
        {
            //dtpNgay.SelectedDate = DateTime.Parse(DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month).ToString()); //Gia tri ngay cuoi thang truoc
            dtpNgay.SelectedDate = DateTime.Now;
        }


        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
