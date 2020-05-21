using System;
using System.Data;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Globalization;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDelete.xaml
    /// </summary>
    public partial class WpfTuyenTruyenSet : Window
    {
        public WpfTuyenTruyenSet()
        {
            InitializeComponent();
        }
        readonly ClsServer _cls = new ClsServer();
        ToolBll bll = new ToolBll();
        ServerInfor srv = new ServerInfor();
        private DataTable dttable = new DataTable();
        private DataTable dtNew = new DataTable();
        string tablename = "";
        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
            var f = new WpfTuyenTruyen();
            f.ShowDialog();
        }

    

        private void WpfTuyenTruyenSet_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
    
                /*
                var dtky=_cls.LoadDataText("select MA_CIF,ND_TEN from ng_dung where ND_PHONGBAN='17' and ND_TTHAI='A' order by ND_CHUCVU");
                RadCboKy.ItemsSource = dtky.DefaultView;
                RadCboKy.DisplayMemberPath = "ND_TEN";
                RadCboKy.SelectedValuePath = "MA_CIF";
                var dtcv = _cls.LoadDataText("select * from DM_CHUCVU where ma in ('1','2')");
                RadCboCvu.ItemsSource = dtcv.DefaultView;
                RadCboCvu.DisplayMemberPath = "TEN";
                RadCboCvu.SelectedValuePath = "MA";
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _cls.DongKetNoi();
        }

        private void dgvTarGet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            try
            {
                if (dttable.Rows.Count > 0)
                {
                    //DataRow dtr = dt.Rows[0];
                    //DataRow dr = (DataRow) dgvData.SelectedItems[0];
                    DataRowView dr = (DataRowView)dgvTarGet.SelectedItems[0];
                    txtMa.Text = dr["MA"].ToString();
                    txtTen.Text = dr["TEN"].ToString();
                }
                else
                {
                    MessageBox.Show("Không có bản ghi nào !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void Them_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _cls.ClsConnect();
                string strchk = "select * from " + tablename + " where MA='" + txtMa.Text.Trim() + "'";
                var dtchk = _cls.LoadDataText(strchk);
                if (dtchk.Rows.Count == 0)
                {
                    string strup = "insert into " + tablename + " (MA,TEN,MA_THELOAI) values ('" + txtMa.Text.Trim() +
                                   "',N'" + txtTen.Text.Trim() + "','" + txtMaTheLoai.Text.Trim() + "')";
                    _cls.UpdateDataText(strup);
                    MessageBox.Show("Thêm thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    GetForm_OnMouseDown(null, null);
                } else MessageBox.Show("Không thể thêm mã này vì đã tồn tại mã "+txtMa.Text+" trong bảng "+dttable, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _cls.ClsConnect();
                string strup = "delete from " + tablename + " where MA='" + txtMa.Text.Trim() + "'";
                _cls.UpdateDataText(strup);
                MessageBox.Show("Xóa thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                GetForm_OnMouseDown(null, null);
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

        private void Sua_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _cls.ClsConnect();
                dtNew = dttable.GetChanges();
                if (dttable.Rows.Count > 0)
                {
                    if (dtNew != null)
                        foreach (DataRow dr in dtNew.Rows)
                        {
                            string strup = "update " + tablename + " set TEN=N'" + dr["TEN"] + "',MA_THELOAI='"+dr["MA_THELOAI"]+"' where MA='" + dr["MA"] + "'";
                            //MessageBox.Show(strup);
                            _cls.UpdateDataText(strup);
                        }
                    MessageBox.Show("Lưu thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else MessageBox.Show("Chưa có thay đổi nào !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                GetForm_OnMouseDown(null,null);
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
        private void GetForm_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                _cls.ClsConnect();
                if (radio1.IsChecked == true) tablename = "TT_CAP";
                else if (radio2.IsChecked == true) tablename = "TT_THELOAI";
                else if (radio3.IsChecked == true) tablename = "TT_NGUONTIN";
                else if (radio4.IsChecked == true) tablename = "TT_THOILUONG";
                else tablename = "TT_LOAITIN";
                _cls.ClsConnect();
                string str = "select * from " + tablename + " order by MA";
                dttable = _cls.LoadDataText(str);
                dgvTarGet.ItemsSource = dttable.DefaultView;
                //MessageBox.Show("Lưu thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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
    }
}
