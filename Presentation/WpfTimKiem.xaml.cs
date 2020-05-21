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
    /// Interaction logic for WpfDvut.xaml
    /// </summary>
    public partial class WpfTimKiem : Window
    {
        public WpfTimKiem()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        DataTable dt = new DataTable();
        private string _Soku = "";
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            DataTable dtng = new DataTable();
            dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
            dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
            cls.DongKetNoi();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(_Soku);
            this.Close();
            WpfTTKU f = new WpfTTKU();
            f.txtSoku.Text = _Soku.Trim();
            f.ShowDialog();

        }

        private void dgvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                DataRowView dr = (DataRowView)dgvData.SelectedItems[0];
                /*
                TxtMaBox.Text = dr["TD_MAPGD"].ToString();
                TxTenBox.Text = dr["TEN_CBTD"].ToString();
                TxtCmtBox.Text = dr["CMT_CBTD"].ToString();
                 */
                //WpfTTKU f = new WpfTTKU();
                //f.txtSoku.Text = dr["KU_SOKU"].ToString();
                _Soku = dr["KU_SOKU"].ToString();
                //MessageBox.Show(dr["KU_SOKU"].ToString());
                //f.ShowDialog();

            }
            else
            {
                MessageBox.Show("Không có dòng nào ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void LblTimKiem_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            {
                try
                {
                    cls.ClsConnect();
                    int thamso = 3;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@CMT";
                    giatri[0] = txtCMT.Text.Trim();
                    bien[1] = "@Ngay";
                    if (dtpNgay.SelectedDate == null)
                    {
                        MessageBox.Show("Chưa chọn ngày", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    }
                    bien[2] = "@CIF";
                    giatri[2] = txtCIF.Text.Trim();
                    if (txtCMT.Text.Trim().Length == 0 && txtCIF.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("Chưa nhập gia trị tìm kiếm", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        //string sql = "select KU_SOKU,KH_MAKH,KH_TENKH,KH_CMT,SC_TEN from LDBF where NGAY='" + giatri[1] +
                        //             "' and (KH_CMT='" + giatri[0] + "' or KH_MAKH='" + giatri[2] + "') and KU_TTMONVAY<>'CLOSE'";
                        string sql = " select a.KU_SOKU,b.KH_MAKH,b.KH_TENKH,b.KH_CMT,c.TEN_CT from hsku a,HSKH b,DM_CHTRINH c"
                                     + " where a.KU_NGAYBC='" + giatri[1] + "' and (b.KH_CMT='" + giatri[0] +
                                     "' or b.KH_MAKH='" + giatri[2] +
                                     "') and a.KU_TTMONVAY<>'CLOSE' and a.KU_MAKH=b.KH_MAKH and a.KU_CHTRINH=c.CHTRINH";
                        dt = cls.LoadDataText(sql);
                        if (dt.Rows.Count > 0)
                        {
                            dgvData.ItemsSource = dt.DefaultView;
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy", "Thông báo",MessageBoxButton.OK,MessageBoxImage.Warning);
                        }

                        cls.DongKetNoi();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
        }
    }
}
