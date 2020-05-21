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
    public partial class WpfTSCCNhap : Window
    {
        public WpfTSCCNhap()
        {
            InitializeComponent();
        }

        private ToolBll s = new ToolBll();
        HardwareInfo infor= new HardwareInfo();
        private readonly ClsServer cls = new ClsServer();
        private DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        string Thumuc = "C:\\KT740";
        private string FileName = "";
        string strpos = "";
        string strphong = "";
        private string ma_ts = "";
        private string mats = "";
        private string tents = "";
        private string tenphong = "";
        private string cauhinh = "";
        private string ch = "";
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                var dtng = cls.LoadDataText("select MAX(NGAYBC) as NGMAX from QT_TSCC");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
                //DateTime lastMonth = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month, DateTime.DaysInMonth(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month));
                //if (BienBll.NdMadv == BienBll.MainPos)
                //{
                    strpos = "select PO_MA MA,PO_TEN TEN from DMPOS  order by PO_MA";
                //    strphong = "select * from DM_PHONGBAN order by MA";
                //}
                //else
                //{
                //    strpos = "select PO_MA MA,PO_TEN TEN from DMPOS where PO_MA='"+CboPos.SelectedValue.ToString().Trim()+"'";
                //    strphong = "select * from DM_PHONGBAN where ma not in ('17','18','19','20','21','22','34')";
                //}
                //strpos = "select PO_MA MA,PO_TEN TEN from DMPOS where PO_MA='" + BienBll.NdMadv + "'";
                //strphong = "select * from DM_PHONGBAN where MA='"+BienBll.PhongBan+"'";
                var dtpos = cls.LoadDataText(strpos);
                CboPos.ItemsSource = dtpos.DefaultView;
                CboPos.DisplayMemberPath = "TEN";
                CboPos.SelectedValuePath = "MA";

                var dtloaitsct = cls.LoadDataText("select * from loai_ts_chitiet order by ma");
                CboLoaiTSCT.ItemsSource = dtloaitsct.DefaultView;
                CboLoaiTSCT.DisplayMemberPath = "TEN";
                CboLoaiTSCT.SelectedValuePath = "MA";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }



        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ShowCh_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            TxtNd.Text = cauhinh;
        }

        private void ShowGrid_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DateTime newdate = new DateTime(dtpNgay.SelectedDate.Value.AddYears(-1).Year + 1, 1, 1);
                //Enday off year
                //MessageBox.Show(newdate.AddDays(-1).ToString("yyyy-MM-dd"));
                cls.ClsConnect();
                    string sqlload =
                        "select a.* from LUU_TSCC a " +
                        "where a.TRANGTHAI='A' and  a.POS_CD='" +
                        CboPos.SelectedValue +
                        "' and LOAI_TS_CHITIET='" + CboLoaiTSCT.SelectedValue + "' and right(MAPHONG,2)='"+s.Right(CboPhong.SelectedValue.ToString().Trim(),2)+"' order by a.MA_NHANHIEU_TS,a.MA_TS";
                    //MessageBox.Show(sqlload);
                    dt = cls.LoadDataText(sqlload);
                    if (dt.Rows.Count > 0) dgvData.ItemsSource = dt.DefaultView;
                    else MessageBox.Show("Không có dữ liệu !", "Thông báo", MessageBoxButton.OK,MessageBoxImage.Information);
                    cauhinh=infor.GetSystemModel() + " , " + infor.GetProcessor() + "," + infor.GetPhysicalMemory() + " , " + infor.GetGraphic() + " , " + infor.GetDisk();
                    TxtNd.Text = cauhinh; 
                    TxtCB.Text = BienBll.NdTen;
                string strsql = "select * from DM_CANBO where ND_MADV='" + CboPos.SelectedValue.ToString().Trim() + "' and ND_PHONGBAN='" + CboPhong.SelectedValue.ToString().Trim() + "' and ND_TTHAI='A' order by ND_CHUCVU,MA_CIF";
                var dtcbo = cls.LoadDataText(strsql);
                CboCanBo.ItemsSource = dtcbo.DefaultView;
                CboCanBo.DisplayMemberPath = "ND_TEN";
                CboCanBo.SelectedValuePath = "ND_TEN";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

     

        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(CboPos.SelectedValue.ToString());
            try
            {
                cls.ClsConnect();
                if (CboPos.SelectedValue.ToString().Trim() == BienBll.MainPos)
                    strphong = "select * from DM_PHONGBAN where ma  in ('17','18','19','20','21','22','34') order by MA";
                else strphong = "select * from DM_PHONGBAN where ma not in ('17','18','19','20','21','22','34','98','99')";
                var dtphong = cls.LoadDataText(strphong);
                CboPhong.ItemsSource = dtphong.DefaultView;
                CboPhong.DisplayMemberPath = "TEN";
                CboPhong.SelectedValuePath = "MA";
                CboPhongPass.ItemsSource = dtphong.DefaultView;
                CboPhongPass.DisplayMemberPath = "TEN";
                CboPhongPass.SelectedValuePath = "MA";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Thông báo ",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            cls.DongKetNoi();

       }

        private void CboPhong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                string pos = CboPos.SelectedValue.ToString().Trim();
                string strsql = "select distinct LOAI_TS MA,TEN_LOAI_TS TEN from LUU_TSCC where POS_CD='" + pos + "' and RIGHT(MAPHONG,2)='"+CboPhong.SelectedValue.ToString().Trim()+"' order by LOAI_TS";
                var dtloaits = cls.LoadDataText(strsql);
                CboLoaiTS.ItemsSource = dtloaits.DefaultView;
                CboLoaiTS.DisplayMemberPath = "TEN";
                CboLoaiTS.SelectedValuePath = "MA";
                CboLoaiTS.SelectedIndex = 1;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();

        }

        private void CboLoaiTS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                string pos = CboPos.SelectedValue.ToString().Trim();
                string strsql = "select distinct LOAI_TS_CHITIET MA,TEN_LOAI_TS_CHITIET TEN from LUU_TSCC where POS_CD='" + pos + "' and RIGHT(MAPHONG,2)='" + CboPhong.SelectedValue.ToString().Trim() + "' and LOAI_TS='"+CboLoaiTS.SelectedValue.ToString().Trim()+ "' order by LOAI_TS_CHITIET";
                var dtloaits = cls.LoadDataText(strsql);
                CboLoaiTSCT.ItemsSource = dtloaits.DefaultView;
                CboLoaiTSCT.DisplayMemberPath = "TEN";
                CboLoaiTSCT.SelectedValuePath = "MA";
                CboLoaiTSCT.SelectedIndex = 1;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();

        }

        private void btnGetInfor_Click(object sender, RoutedEventArgs e)
        {
            string info = infor.GetSystemModel() + " , " + infor.GetProcessor()+","+ infor.GetPhysicalMemory() +" , " +infor.GetGraphic() + " , " + infor.GetDisk();
            MessageBox.Show(info);
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                cls.ClsConnect();
                if (Ration1.IsChecked == false && Ration2.IsChecked == false)
                    MessageBox.Show("Bạn chưa chọn cập nhật cấu hình hay chuyển đơn vị quản lý !", "Thông báo",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    if (Ration1.IsChecked == true)
                    {
                        var dtchk = cls.LoadDataText("select * from LUU_TSCC where CB_QUANLY<>'' and MA_TS='"+ma_ts+"'");
                        if (dtchk.Rows.Count > 0)
                        {
                            MessageBoxResult result =
                                MessageBox.Show(
                                    "Mã tài sản " + ma_ts + " đã được nhập thông tin cấu hình, Có muốn nhập lại?",
                                    "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            //DialogResult dialogResult = MessageBox.Show("Sure", "Some Title", MessageBoxButton.YesNo,MessageBoxImage.Question);
                            if (result == MessageBoxResult.Yes)
                            {
                                string strsql = "update LUU_TSCC set MOTA=N'" + TxtNd.Text + "', CB_QUANLY=N'" +
                                                TxtCB.Text +
                                                "' where MA_TS='" + label.Content + "'";
                                cls.UpdateDataText(strsql);
                                MessageBox.Show("Cập nhật thông tin cấu hình thành công !", "Thông báo",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                            }
                        }
                        else
                        {
                            string strsql = "update LUU_TSCC set MOTA=N'" + TxtNd.Text + "', CB_QUANLY=N'" +
                                            TxtCB.Text +
                                            "' where MA_TS='" + label.Content + "'";
                            cls.UpdateDataText(strsql);
                            MessageBox.Show("Cập nhật thông tin cấu hình thành công !", "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        if (CboPhongPass.SelectedValue == null)
                            MessageBox.Show("Bạn chưa chọn đơn vị quản lý !", "Thông báo", MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                        else
                        {
                            string strchuyen = "update LUU_TSCC set MAPHONG='VBSP'+right(POS_CD,4)+'" +CboPhongPass.SelectedValue.ToString().Trim()+ "' where MA_TS='" + label.Content + "'";
                            string strchuyen1 = "update a set a.TENPHONG=b.TEN from LUU_TSCC a, DM_PHONGBAN b where MA_TS='" + label.Content + "' and right(a.MAPHONG,2)=b.MA";
                            cls.UpdateDataText(strchuyen);
                            cls.UpdateDataText(strchuyen1);
                            MessageBox.Show("Đã chuyển "+label.Content+" đến phòng "+CboPhongPass.SelectedValue+" !", "Thông báo", MessageBoxButton.OK,MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }

        private void dgvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    ////DataRow dtr = dt.Rows[0];
                    ////DataRow dr = (DataRow) dgvData.SelectedItems[0];
                    DataRowView dr = (DataRowView)dgvData.SelectedItems[0];
                    label.Content = dr["MA_TS"].ToString();
                    ma_ts = dr["MA_TS"].ToString();
                    mats = dr["MA_NHANHIEU_TS"].ToString();
                    tents = dr["TEN_TS"].ToString();
                    tenphong = dr["TENPHONG"].ToString();
                    ch=dr["MOTA"].ToString();
                    if (mats == "TI11" || mats == "TI12" || mats == "MM11" || mats == "MM12")
                    {
                        //TxtNd.Text = "";
                        TxtNd.Text = ch;
                        TxtCB.Text = BienBll.NdTen;
                    }
                    else
                    {
                        TxtNd.Text = tents;
                        TxtCB.Text = tenphong;
                    }
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

        private void Ration1_Checked(object sender, RoutedEventArgs e)
        {
            lblPhongPass.IsEnabled = false;
            CboPhongPass.IsEnabled = false;
        }

        private void Ration2_Checked(object sender, RoutedEventArgs e)
        {
            lblPhongPass.IsEnabled = true;
            CboPhongPass.IsEnabled = true;
        }

        private void CboCanBo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TxtCB.Text = CboCanBo.SelectedValue.ToString().Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

}
