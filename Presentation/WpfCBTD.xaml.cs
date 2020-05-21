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
    public partial class WpfCbtd : Window
    {
        public WpfCbtd()
        {
            InitializeComponent();
        }

        private readonly ClsServer cls = new ClsServer();
        private DataTable dt = new DataTable();
        private DataTable dtdb = new DataTable();
        private DataTable dtNew = new DataTable();
        //bool chon;
        //DataTable dtnew = new DataTable();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
            try
            {
                cls.ClsConnect();
                const string sql = "select PO_MA,PO_TEN from DMPOS order by PO_MA";
                var dtpos = cls.LoadDataText(sql);
                /*
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                 */
                CboPos.ItemsSource = dtpos.DefaultView;
                CboPos.SelectedValuePath = "PO_MA";
                CboPos.DisplayMemberPath = "PO_TEN";
                CboPos.SelectedIndex = 1;
                /*       
                const string sqlload = "select * from CBTD ";
                dt = cls.LoadDataText(sqlload);
                dgvData.ItemsSource = dt.DefaultView;
                 */
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();

        }




        private void BtnThem_OnClick(object sender, RoutedEventArgs e)
        {
            if (TxtCmtBox.Text != "")
            {
                try
                {
                    cls.ClsConnect();
                    var sql = "";
                    if (RadioButton1.IsChecked == true)
                    {
                        sql = "select * from CBTD where CMT_CBTD='" + TxtCmtBox.Text.Trim() + "'";
                    }
                    else
                    {
                        sql = "select * from CBKT where KT_CMT='" + TxtCmtBox.Text.Trim() + "'";
                    }
                    //MessageBox.Show(sql);
                    var dtkt = cls.LoadDataText(sql);
                    if (dtkt.Rows.Count > 0)
                    {
                        MessageBox.Show("CMT : " + TxtCmtBox.Text.Trim() + " đã tồn tại", "Thông báo",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }
                    else
                    {
                        var sqladd = "";
                        if (RadioButton1.IsChecked == true)
                        {
                            sqladd =
                                "insert into CBTD(TD_MACN,TD_MAPGD,TEN_CBTD,CMT_CBTD,TRANGTHAI) values ('003005','" +
                                TxtMaBox.Text + "',N'" + TxTenBox.Text + "','" + TxtCmtBox.Text + "','A')";
                        }
                        else if (RadioButton2.IsChecked == true)
                        {
                            sqladd = "insert into CBKT(KT_MACN,KT_MAPGD,TRANGTHAI,KT_TEN,KT_CMT) values ('003005','" +
                                     TxtMaBox.Text + "','A',N'" + TxTenBox.Text + "','" + TxtCmtBox.Text + "')";
                        }
                        //MessageBox.Show(sqladd);
                        cls.UpdateDataText(sqladd);
                        MessageBox.Show("Đã thêm " + TxTenBox.Text + " vào CBTD !", "Thông báo",
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
                MessageBox.Show("Nhập CMT ", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }


        private void BtnSua_OnClick(object sender, RoutedEventArgs e)
        {
            if (RadioButton3.IsChecked == true)
            {
                #region

                if (TxtMaBox.Text != "")
                {
                    try
                    {
                        cls.ClsConnect();
                        var sql = "";
                        if (RadioButton1.IsChecked == true)
                        {
                            sql = "select * from CBTD where CMT_CBTD='" + TxtCmtBox.Text.Trim() + "'";
                        }
                        else if (RadioButton2.IsChecked == true)
                        {
                            sql = "select * from CBKT where KT_CMT='" + TxtCmtBox.Text.Trim() + "'";
                        }
                        var dtkt = cls.LoadDataText(sql);
                        if (dtkt.Rows.Count == 0)
                        {
                            MessageBox.Show("CMT : " + TxtCmtBox.Text.Trim() + " Chưa tồn tại", "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                        }
                        else
                        {
                            var sqledit = "";
                            if (RadioButton1.IsChecked == true)
                            {
                                sqledit = "update CBTD set TD_MAPGD='" + TxtMaBox.Text + "',TEN_CBTD=N'" +
                                          TxTenBox.Text + "', TRANGTHAI='" + TxtTT.Text.Trim() + "' where CMT_CBTD='" +
                                          TxtCmtBox.Text + "'";

                            }
                            else if (RadioButton2.IsChecked == true)
                            {
                                sqledit = "update CBKT set KT_MAPGD='" + TxtMaBox.Text + "',KT_TEN=N'" +
                                          TxTenBox.Text + "', TRANGTHAI='" + TxtTT.Text.Trim() + "' where KT_CMT='" +
                                          TxtCmtBox.Text + "'";
                            }
                            //MessageBox.Show(sqledit);
                            cls.UpdateDataText(sqledit);
                            MessageBox.Show("Đã sửa " + TxTenBox.Text + " !", "Thông báo", MessageBoxButton.OK,
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
                else
                {
                    MessageBox.Show("Nhập UserName ", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                #endregion
            }
            else if (RadioButton4.IsChecked == true)
            {
                #region

                dtNew = dtdb.Clone();
                foreach (DataRow dr in dtdb.Rows)
                {
                    if ((bool) dr[0] == true)
                    {
                        dtNew.ImportRow(dr);
                    }
                }
                if (dtNew == null || dtNew.Rows.Count == 0)
                {
                    MessageBox.Show("Chưa chọn khách hàng nào ", "Mess");
                }
                else
                {
                    dgvDmxa.ItemsSource = null;
                    dgvDmxa.ItemsSource = dtNew.DefaultView;
                    cls.ClsConnect();
                    foreach (DataRow dr in dtNew.Rows)
                    {
                        if ((bool) dr[0] == true)
                        {
                            try
                            {
                                //MessageBox.Show(dr[0].ToString() + "  " + CboCbtd.SelectedValue.ToString().Trim(), "Mess");
                                var strup = "";
                                if (RadioButton1.IsChecked == true)
                                {
                                    strup = "update DMXA set CMT_CBTD='" +
                                            CboCbtd.SelectedValue.ToString().Trim() +
                                            "' where MA='" + dr[1].ToString().Trim() + "'";
                                }
                                else if (RadioButton2.IsChecked == true)
                                {
                                    strup = "update DMXA set CMT_CBKT='" +
                                            CboCbtd.SelectedValue.ToString().Trim() +
                                            "' where MA='" + dr[1].ToString().Trim() + "'";
                                }
                                //MessageBox.Show(strup);
                                cls.UpdateDataText(strup);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                    MessageBox.Show("Đã cập nhật địa bàn thành cộng !", "Thông báo", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    CboPos_SelectionChanged(null, null);
                }

                #endregion
            }
        }

        private void BtnXoa_OnClick(object sender, RoutedEventArgs e)
        {
            if (TxtCmtBox.Text != "")
            {
                try
                {
                    cls.ClsConnect();
                    var sqldele = "";
                    if (RadioButton1.IsChecked == true)
                    {
                        sqldele = "delete from CBTD where CMT_CBTD='" + TxtCmtBox.Text.Trim() + "'";
                    }
                    else if (RadioButton2.IsChecked == true)
                    {
                        sqldele = "delete from CBKT where KT_CMT='" + TxtCmtBox.Text.Trim() + "'";

                    }
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
                MessageBox.Show("Click để chọn UserName ", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            TxtCmtBox.Text = "";
        }


        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
            if (RadioButton3.IsChecked == true)
            {
                try
                {
                    cls.ClsConnect();
                    var sqlload = "";
                    if (RadioButton1.IsChecked == true)
                    {
                        sqlload = "select * from CBTD where TD_MAPGD='" + CboPos.SelectedValue.ToString().Trim() + "'";
                    }
                    else if (RadioButton2.IsChecked == true)
                    {
                        sqlload =
                            "select *,kt_mapgd as TD_MAPGD,kt_cmt as CMT_CBTD,kt_ten as TEN_CBTD from CBKT where KT_MAPGD='" +
                            CboPos.SelectedValue.ToString().Trim() + "'";
                    }
                    dt = cls.LoadDataText(sqlload);
                    dgvData.ItemsSource = dt.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
                }
                cls.DongKetNoi();

            }
            else if (RadioButton4.IsChecked == true)
            {
                {
                    try
                    {
                        dgvData.ItemsSource = null;
                        dgvData.Items.Clear();
                        dgvData.Items.Refresh();
                        cls.ClsConnect();
                        var sqldb = "";
                        if (RadioButton1.IsChecked == true)
                        {
                            sqldb =
                                "select a.CHON,a.MA,a.TEN,b.TEN_CBTD from DMXA a left join CBTD b on a.CMT_CBTD=b.CMT_CBTD where a.PGD_QL='" +
                                CboPos.SelectedValue.ToString().Trim() + "' and right(a.MA,2)<>'00' order by a.MA";
                        }
                        else if (RadioButton2.IsChecked == true)
                        {
                            sqldb =
                                "select a.CHON,a.MA,a.TEN,b.KT_TEN as TEN_CBTD from DMXA a left join CBKT b on a.CMT_CBKT=b.KT_CMT where a.PGD_QL='" +
                                CboPos.SelectedValue.ToString().Trim() + "' and right(a.MA,2)<>'00' order by a.MA";
                        }
                        dtdb = cls.LoadDataText(sqldb);
                        dgvDmxa.ItemsSource = dtdb.DefaultView;
                        var cbtd = "";
                        if (RadioButton1.IsChecked == true)
                        {
                            cbtd = "select CMT_CBTD,TEN_CBTD from CBTD where TD_MAPGD='" +
                                   CboPos.SelectedValue.ToString().Trim() + "'";
                        }
                        else if (RadioButton2.IsChecked == true)
                        {
                            cbtd = "select KT_CMT as CMT_CBTD,KT_TEN as TEN_CBTD from CBKT where KT_MAPGD='" +
                                   CboPos.SelectedValue.ToString().Trim() + "'";
                        }
                        var dtcbtd = cls.LoadDataText(cbtd);
                        CboCbtd.ItemsSource = dtcbtd.DefaultView;
                        CboCbtd.DisplayMemberPath = "TEN_CBTD";
                        CboCbtd.SelectedValuePath = "CMT_CBTD";
                        CboCbtd.SelectedIndex = 1;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void dgvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //dtnew = dt.Copy();
            if (dt.Rows.Count > 0)
            {
                DataRowView dr = (DataRowView) dgvData.SelectedItems[0];
                TxtMaBox.Text = dr["TD_MAPGD"].ToString();
                TxTenBox.Text = dr["TEN_CBTD"].ToString();
                TxtCmtBox.Text = dr["CMT_CBTD"].ToString();
                TxtTT.Text = dr["TRANGTHAI"].ToString();
            }
            else
            {
                MessageBox.Show("Không có dòng nào ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RadioButton4_Checked(object sender, RoutedEventArgs e)
        {
            /*phu trach dia ban */
            lblCbtd.IsEnabled = true;
            CboCbtd.IsEnabled = true;
            lblMa.IsEnabled = false;
            TxtMaBox.IsEnabled = false;
            lblTen.IsEnabled = false;
            TxTenBox.IsEnabled = false;
            lblCMT.IsEnabled = false;
            TxtCmtBox.IsEnabled = false;
            btnThem.IsEnabled = false;
            btnXoa.IsEnabled = false;
            try
            {
                dgvData.ItemsSource = null;
                dgvData.Items.Clear();
                dgvData.Items.Refresh();
                cls.ClsConnect();
                var sqldb = "";
                if (RadioButton1.IsChecked == true)
                {
                    sqldb =
                        "select a.CHON,a.MA,a.TEN,b.TEN_CBTD from DMXA a left join CBTD b on a.CMT_CBTD=b.CMT_CBTD where a.PGD_QL='" +
                        CboPos.SelectedValue.ToString().Trim() + "' and right(a.MA,2)<>'00' order by a.MA";
                }
                else if (RadioButton2.IsChecked == true)
                {
                    sqldb =
                        "select a.CHON,a.MA,a.TEN,b.KT_TEN as TEN_CBTD from DMXA a left join CBKT b on a.CMT_CBKT=b.KT_CMT where a.PGD_QL='" +
                        CboPos.SelectedValue.ToString().Trim() + "' and right(a.MA,2)<>'00' order by a.MA";
                }

                dtdb = cls.LoadDataText(sqldb);
                dgvDmxa.ItemsSource = dtdb.DefaultView;
                //string cbtd = "select CMT_CBTD,TEN_CBTD from CBTD where TD_MAPGD='" + CboPos.SelectedValue.ToString().Trim() + "'";
                var cbtd = "";
                if (RadioButton1.IsChecked == true)
                {
                    cbtd = "select CMT_CBTD,TEN_CBTD from CBTD where TD_MAPGD='" +
                           CboPos.SelectedValue.ToString().Trim() + "'";
                }
                else if (RadioButton2.IsChecked == true)
                {
                    cbtd = "select KT_CMT as CMT_CBTD,KT_TEN as TEN_CBTD from CBKT where KT_MAPGD='" +
                           CboPos.SelectedValue.ToString().Trim() + "'";
                }

                var dtcbtd = cls.LoadDataText(cbtd);
                CboCbtd.ItemsSource = dtcbtd.DefaultView;
                CboCbtd.DisplayMemberPath = "TEN_CBTD";
                CboCbtd.SelectedValuePath = "CMT_CBTD";
                CboCbtd.SelectedIndex = 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RadioButton3_Checked(object sender, RoutedEventArgs e)
        {
            /*them / sua / xoa */
            lblCbtd.IsEnabled = false;
            CboCbtd.IsEnabled = false;
            lblMa.IsEnabled = true;
            TxtMaBox.IsEnabled = true;
            lblTen.IsEnabled = true;
            TxTenBox.IsEnabled = true;
            lblCMT.IsEnabled = true;
            TxtCmtBox.IsEnabled = true;
            btnThem.IsEnabled = true;
            btnXoa.IsEnabled = true;
            try
            {
                dgvDmxa.ItemsSource = null;
                dgvDmxa.Items.Clear();
                dgvDmxa.Items.Refresh();
                /*
                if (dgvData.ItemsSource == null)
                {
                    cls.ClsConnect();
                    string sqlload = "select * from CBTD where TD_MAPGD='" + CboPos.SelectedValue.ToString().Trim() +"'";
                    dt = cls.LoadDataText(sqlload);
                    dgvData.ItemsSource = dt.DefaultView;
                }
                */
                cls.ClsConnect();
                var sqlload = "";
                if (RadioButton1.IsChecked == true)
                {
                    sqlload = "select * from CBTD where TD_MAPGD='" + CboPos.SelectedValue.ToString().Trim() + "'";
                }
                else if (RadioButton2.IsChecked == true)
                {
                    sqlload =
                        "select *,kt_mapgd as TD_MAPGD,kt_cmt as CMT_CBTD,kt_ten as TEN_CBTD from CBKT where KT_MAPGD='" +
                        CboPos.SelectedValue.ToString().Trim() + "'";
                }
                dt = cls.LoadDataText(sqlload);
                dgvData.ItemsSource = dt.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void BtnChuyen_OnClick(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            try
            {
                if (RadioButton1.IsChecked == true)
                {
                    if (TxtCmtBox.Text.Trim() == "")
                        MessageBox.Show("Hãy chọn người cần chuyển !", "Thông Báo", MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    else
                    {
                        string str =
                            "insert into CBKT select TD_MACN KT_MACN,TD_MAPGD KT_MAPGD,TRANGTHAI,CMT_CBTD KT_CMT,TEN_CBTD KT_TEN,CMT_CBTD CMT from CBTD where CMT_CBTD='" +
                            TxtCmtBox.Text.Trim() + "'";
                        string str1 = "delete from CBTD where CMT_CBTD='" + TxtCmtBox.Text.Trim() + "'";
                        cls.UpdateDataText(str);
                        cls.UpdateDataText(str1);
                    }

                }
                else
                {
                    if (TxtCmtBox.Text.Trim() == "")
                        MessageBox.Show("Hãy chọn người cần chuyển !", "Thông Báo", MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    else
                    {
                        string str =
                            "insert into CBTD select KT_MACN TD_MACN,KT_MAPGD TD_MAPGD,KT_TEN TEN_CBTD,KT_CMT CMT_CBTD,TRANGTHAI,CMT from CBKT where KT_CMT='" +
                            TxtCmtBox.Text.Trim() + "'";
                        string str1 = "delete from CBKT where KT_CMT='" + TxtCmtBox.Text.Trim() + "'";
                        cls.UpdateDataText(str);
                        cls.UpdateDataText(str1);
                    }

                }
                MessageBox.Show("Cập nhật thành công !", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lổi" + ex.Message, "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }

        private void BtnChuyenPos_OnClick(object sender, RoutedEventArgs e)
        {
            {
                cls.ClsConnect();
                try
                {
                    if (RadioButton1.IsChecked == true)
                    {
                        if (TxtCmtBox.Text.Trim() == "" || TxtPosMoi.Text.Trim() == "")
                            MessageBox.Show("Hãy chọn người cần chuyển hoặc chưa nhập mã POS mới !", "Thông Báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                        else
                        {
                            string str =
                                "update CBTD set TD_MAPGD='" + TxtPosMoi.Text + "' where CMT_CBTD='" +
                                TxtCmtBox.Text.Trim() + "'";
                            cls.UpdateDataText(str);
                            TxtPosMoi.Clear();
                        }

                    }
                    else
                    {
                        if (TxtCmtBox.Text.Trim() == "" || TxtPosMoi.Text.Trim() == "")
                            MessageBox.Show("Hãy chọn người cần chuyển hoặc chưa nhập mã POS mới !", "Thông Báo",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                        {
                            string str = "update CBKT set KT_MAPGD='" + TxtPosMoi.Text + "' where KT_CMT='" +
                                         TxtCmtBox.Text.Trim() + "'";
                            cls.UpdateDataText(str);
                            TxtPosMoi.Clear();
                        }

                    }
                    MessageBox.Show("Cập nhật thành công !", "Thông Báo", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi" + ex.Message, "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                cls.DongKetNoi();
            }
        }
    }
}
