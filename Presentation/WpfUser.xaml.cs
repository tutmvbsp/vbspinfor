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
using MessageBox = System.Windows.MessageBox;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfLaiTon.xaml
    /// </summary>
    public partial class WpfUser : Window
    {
        public WpfUser()
        {
            InitializeComponent();
        }

        private ToolBll s = new ToolBll();
        private readonly ClsServer cls = new ClsServer();
        private DataTable dt = new DataTable();
        private DataTable dtxa = new DataTable();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                cls.ClsConnect();
                var dtpos = cls.LoadDataText("select PO_MA,PO_TEN from DMPOS order by PO_MA");
                RadCboPos.ItemsSource = dtpos.DefaultView;
                RadCboPos.DisplayMemberPath = "PO_TEN";
                RadCboPos.SelectedValuePath = "PO_MA";
                //RadCboPos.SelectedIndex = 0;
                //for (int i = 0; i < dtpos.Rows.Count; i++)
                //{
                //    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                //}
                CboPos.ItemsSource = dtpos.DefaultView;
                CboPos.SelectedValuePath = "PO_MA";
                CboPos.DisplayMemberPath = "PO_TEN";

                const string sql1 = "select MA,TEN from DM_PHONGBAN order by MA";
                var dtphong = cls.LoadDataText(sql1);
                CboPhong.ItemsSource = dtphong.DefaultView;
                CboPhong.DisplayMemberPath = "TEN";
                CboPhong.SelectedValuePath = "MA";
                //CboPhong.SelectedIndex = 0;

                const string sql2 = "select MA,TEN from DM_CHUCVU order by MA";
                var dtchucvu = cls.LoadDataText(sql2);
                CboChucVu.ItemsSource = dtchucvu.DefaultView;
                CboChucVu.DisplayMemberPath = "TEN";
                CboChucVu.SelectedValuePath = "MA";
                //CboChucVu.SelectedIndex = 0;
                const string sql3 = "select * from DM_QUYEN order by MA";
                var dtquyen= cls.LoadDataText(sql3);
                CboQuyen.ItemsSource = dtquyen.DefaultView;
                CboQuyen.DisplayMemberPath = "TEN";
                CboQuyen.SelectedValuePath = "MA";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
            // RefreshLabel_OnMouseDown(null,null);
            // LoadGrid();
        }





        private void BtnThem_OnClick(object sender, RoutedEventArgs e)
        {
            if (TxtMaBox.Text != "")
            {
                try
                {
                    cls.ClsConnect();
                    string sql = "select * from NG_DUNG where ND_MA='" + TxtMaBox.Text.Trim() + "'";
                    //MessageBox.Show(sql);
                    var dtkt = cls.LoadDataText(sql);
                    if (dtkt.Rows.Count > 0)
                    {
                        MessageBox.Show("UserName : " + TxtMaBox.Text.Trim() + " đã tồn tại", "Thông báo",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }
                    else
                    {
                        string sqladd =
                            "insert into NG_DUNG(ND_MA,ND_TEN,ND_MOBILE,ND_MATKHAU,ND_TTHAI,ND_QUYEN,ND_MADV,ND_PHONGBAN,ND_CHUCVU,MA_CIF,SUB_CMT)" +
                            " Values('" + TxtMaBox.Text + "',N'" + TxTenBox.Text + "','" + TxtMobile.Text + "','" +
                            s.Encrypt(passwordBox.Password, true) + "','" + TxtTrThai.Text + "','"+TxtQuyen.Text+"','" + TxtPos.Text.Trim() +
                            "','" + TxtPhong.Text.Trim() + "','" + TxtChucVu.Text.Trim() + "','"+TxtCif.Text+"','" + TxtCMTBox.Text + "')";
                        MessageBox.Show(sqladd);
                        cls.UpdateDataText(sqladd);
                        MessageBox.Show("Đã thêm " + TxTenBox.Text + " vào người dùng !", "Thông báo",
                            MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    cls.DongKetNoi();
                    ClearAll();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi, kiểm tra lại thông tin " + ex.Message, "Thông báo", MessageBoxButton.OK,
                        MessageBoxImage.Error);

                }

            }
            else
            {
                MessageBox.Show("Nhập UserName ", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }


        private void BtnSua_OnClick(object sender, RoutedEventArgs e)
        {
            if (TxtMaBox.Text != "")
            {
                try
                {
                    cls.ClsConnect();
                    string sql = "select * from NG_DUNG where ND_MA='" + TxtMaBox.Text.Trim() + "'";
                    var dtkt = cls.LoadDataText(sql);
                    if (dtkt.Rows.Count == 0)
                    {
                        MessageBox.Show("UserName : " + TxtMaBox.Text.Trim() + " Chưa tồn tại", "Thông báo",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        //string sqledit = "update NG_DUNG set ND_TEN= N'" + TxTenBox.Text + "',ND_TTHAI='" + TxtTrThai.Text + "',ND_MADV='" + TxtPos.Text.Trim() +
                        //    "',ND_PHONGBAN='" + TxtPhong.Text.Trim() + "',ND_CHUCVU='"+TxtChucVu.Text.Trim()+"', ND_MATKHAU='"+ s.Encrypt(passwordBox.Password, true) + "'" +
                        //    ",SUB_CMT='"+TxtCMTBox.Text+"',ND_LOGIN='"+TxtLogIn.Text.Trim()+"' where ND_MA='" + TxtMaBox.Text+"'";
                        string sqledit = "update NG_DUNG set ND_TEN= N'" + TxTenBox.Text + "',ND_TTHAI='" +
                                         TxtTrThai.Text + "',ND_MADV='" + TxtPos.Text.Trim() +
                                         "',ND_PHONGBAN='" + TxtPhong.Text.Trim() + "',ND_CHUCVU='" +
                                         TxtChucVu.Text.Trim() + "',CHAMCONG='" + TxtChamCong.Text.Trim() + "'" +
                                         ",SUB_CMT='" + TxtCMTBox.Text + "',ND_LOGIN='" + TxtLogIn.Text.Trim() +
                                         "',ND_MOBILE='"+TxtMobile.Text+"', ND_QUYEN='"+TxtQuyen.Text+"',MA_CIF='"+TxtCif.Text+"' where ND_MA='" + TxtMaBox.Text + "'";

                        // MessageBox.Show(sqledit);
                        // MessageBox.Show(s.Decrypt(TxtPassBox.Text, true));
                        // MessageBox.Show(s.Encrypt(s.Decrypt(TxtPassBox.Text, true), true));
                        cls.UpdateDataText(sqledit);
                        cls.UpdateDataText(
                            "update a set a.ND_DIACHI=b.PO_TEN from ng_dung a, dmpos b where a.ND_MADV=b.PO_MA");
                        cls.UpdateDataText(
                            "update a set a.MA_CIF=b.KH_MAKH from NG_DUNG a, HSKH b where a.SUB_CMT=b.KH_CMT and b.KH_TTRANG='A'");
                        cls.UpdateDataText(
                            "update a set a.ND_TTHAI = b.ND_TTHAI,a.ND_MADV = b.ND_MADV,a.ND_PHONGBAN = b.ND_PHONGBAN,a.ND_CHUCVU = b.ND_CHUCVU from DM_CANBO a, NG_DUNG b  where a.MA_CIF = b.MA_CIF and b.ND_MA='" +
                            TxtMaBox.Text + "'");
                        MessageBox.Show("Đã sửa " + TxTenBox.Text + " vào người dùng !", "Thông báo",
                            MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    cls.DongKetNoi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi, kiểm tra lại thông tin " + ex.Message, "Thông báo", MessageBoxButton.OK,
                        MessageBoxImage.Error);

                }

            }
            else
            {
                MessageBox.Show("Nhập UserName ", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void BtnXoa_OnClick(object sender, RoutedEventArgs e)
        {
            if (TxtMaBox.Text != "")
            {
                try
                {
                    cls.ClsConnect();
                    string sqldele = "delete from NG_DUNG where ND_MA='" + TxtMaBox.Text.Trim() + "'";
                    cls.UpdateDataText(sqldele);
                    MessageBox.Show("Đã Xóa " + TxTenBox.Text + " !", "Thông báo",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    cls.DongKetNoi();
                    ClearAll();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi, kiểm tra lại thông tin " + ex.Message, "Thông báo", MessageBoxButton.OK,
                        MessageBoxImage.Error);

                }

            }
            else
            {
                MessageBox.Show("Click đúp để chọn UserName ", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void LoadData_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            TxtMaBox.Text = "";
            TxTenBox.Text = "";
            passwordBox.Password = "";
            TxtTrThai.Text = "";
            TxtCMTBox.Text = "";
        }

        private void LblCheck_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                string sql = "select * from NG_DUNG where ND_MA='" + TxtMaBox.Text.Trim() + "'";
                //MessageBox.Show(sql);
                var dtkt = cls.LoadDataText(sql);
                if (dtkt.Rows.Count > 0)
                {
                    MessageBox.Show("UserName : " + TxtMaBox.Text.Trim() + " đã tồn tại", "Thông báo",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show(
                        "UserName : " + TxtMaBox.Text.Trim() +
                        " chưa tồn tại, thực hiện nhập thông tin khác rồi nhấn Add", "Thông báo", MessageBoxButton.OK,
                        MessageBoxImage.Information);

                }
                cls.DongKetNoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lổi, kiểm tra lại thông tin " + ex.Message, "Thông báo", MessageBoxButton.OK,
                    MessageBoxImage.Error);

            }

        }

        private void LblShowPass_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MessageBox.Show(s.Decrypt(passwordBox.Password, true));
            }
            catch (Exception)
            {
                MessageBox.Show("Mật khẩu chưa đươc mã hóa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Stop);
            }

            //MessageBox.Show(s.Encrypt(TxtPassBox.Text, true)+"      "+s.Decrypt(s.Encrypt(TxtPassBox.Text, true), true));
        }

        private void LblResetPass_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string sqledit = "update NG_DUNG set ND_MATKHAU='" + s.Encrypt(passwordBox.Password, true) +
                                 "' where ND_MA='" + TxtMaBox.Text + "'";
                cls.UpdateDataText(sqledit);
                MessageBox.Show("Đã Reset mật khẩu : " + s.Decrypt(passwordBox.Password, true), "Thông báo",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Mật khẩu chưa đươc mã hóa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Stop);
            }

            //MessageBox.Show(s.Encrypt(TxtPassBox.Text, true)+"      "+s.Decrypt(s.Encrypt(TxtPassBox.Text, true), true));
        }

        private void dgvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    //DataRow dtr = dt.Rows[0];
                    //DataRow dr = (DataRow) dgvData.SelectedItems[0];
                    DataRowView dr = (DataRowView) dgvData.SelectedItems[0];
                    TxtMaBox.Text = dr["ND_MA"].ToString();
                    TxTenBox.Text = dr["ND_TEN"].ToString();
                    passwordBox.Password = dr["ND_MATKHAU"].ToString();
                    TxtTrThai.Text = dr["ND_TTHAI"].ToString();
                    TxtCMTBox.Text = dr["SUB_CMT"].ToString();
                    TxtPos.Text = dr["ND_MADV"].ToString();
                    TxtPhong.Text = dr["ND_PHONGBAN"].ToString();
                    TxtChucVu.Text = dr["ND_CHUCVU"].ToString();
                    TxtLogIn.Text = dr["ND_LOGIN"].ToString();
                    TxtChamCong.Text = dr["CHAMCONG"].ToString();
                    TxtQuyen.Text = dr["ND_QUYEN"].ToString();
                    TxtMobile.Text = dr["ND_MOBILE"].ToString();
                    TxtCif.Text = dr["MA_CIF"].ToString();
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

        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TxtPos.Text = CboPos.SelectedValue.ToString().Trim();
        }

        private void CboPhong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TxtPhong.Text = CboPhong.SelectedValue.ToString().Trim();
        }

        private void CboChucVu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TxtChucVu.Text = CboChucVu.SelectedValue.ToString().Trim();
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RadCboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //MessageBox.Show(bll.Left(cboPos.SelectedValue.ToString().Trim(),6));
                CboPB.Items.Clear();
                cls.ClsConnect();
                dtxa = cls.LoadDataText(s.Left(RadCboPos.SelectedValue.ToString().Trim(), 6) == BienBll.MainPos.Trim() ? "select * from DM_PHONGBAN where MA in ('17','18','19','20','21','22') order by MA" : "select * from DM_PHONGBAN where MA in ('29','30','31') order by MA");
                for (int i = 0; i < dtxa.Rows.Count; i++)
                {
                    CboPB.Items.Add(dtxa.Rows[i][0].ToString().Trim() + " | " + dtxa.Rows[i][1]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }

        private void CboPB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LblGetData_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                string sqlload = "select a.*,b.TEN as PHONGBAN,c.TEN as CHUCVU from NG_DUNG a, DM_PHONGBAN b,DM_CHUCVU c where a.ND_MADV='" + RadCboPos.SelectedValue + "' and a.ND_PHONGBAN='"+ s.Left(CboPB.SelectedValue.ToString().Trim(), 2) + "' and a.ND_PHONGBAN=b.MA and a.ND_CHUCVU=c.MA  order by a.ND_MADV,a.ND_CHUCVU,a.ND_MA";
                //MessageBox.Show(sqlload);
                dt = cls.LoadDataText(sqlload);
                dgvData.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CboQuyen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TxtQuyen.Text = CboQuyen.SelectedValue.ToString().Trim();
        }
    }
}
