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
    public partial class WpfTSCCGDX : Window
    {
        public WpfTSCCGDX()
        {
            InitializeComponent();
        }

        private ToolBll s = new ToolBll();
        HardwareInfo infor= new HardwareInfo();
        private readonly ClsServer cls = new ClsServer();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        string Thumuc = "C:\\KT740";
        private string FileName = "";
        string strpos = "";
        string strphong = "";
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
                //if (BienBll.NdMadv == BienBll.MainPos)
                //    strphong = "select * from DM_PHONGBAN where ma  in ('17','18','19','20','21','22','34') order by MA";
                //else strphong = "select * from DM_PHONGBAN where ma not in ('17','18','19','20','21','22','34','98','99')";
                //strphong = "select * from DM_PHONGBAN where MA='"+BienBll.PhongBan+"'";
                var dtpos = cls.LoadDataText(strpos);
                CboPos.ItemsSource = dtpos.DefaultView;
                CboPos.DisplayMemberPath = "TEN";
                CboPos.SelectedValuePath = "MA";
                //var dtphong = cls.LoadDataText(strphong);
                //CboPhong.ItemsSource = dtphong.DefaultView;
                //CboPhong.DisplayMemberPath = "TEN";
                //CboPhong.SelectedValuePath = "MA";
    

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
                        "where a.TRANGTHAI='A' and a.POS_CD='" +CboPos.SelectedValue +
                        "' and right(MAPHONG,2)='"+s.Right(CboPhong.SelectedValue.ToString().Trim(),2)+"' order by a.MA_NHANHIEU_TS,a.MA_TS";
                    //MessageBox.Show(sqlload);
                    dt = cls.LoadDataText(sqlload);
                    if (dt.Rows.Count > 0) dgvData.ItemsSource = dt.DefaultView;
                    else MessageBox.Show("Không có dữ liệu !", "Thông báo", MessageBoxButton.OK,MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

     

        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Thông báo ", MessageBoxButton.OK, MessageBoxImage.Error);
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
 

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();

        }

     

    

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            string strup = "";
            try
            {
                cls.ClsConnect();
                dtNew = dt.GetChanges();
                if (dtNew == null || dtNew.Rows.Count == 0)
                {
                    MessageBox.Show("Chưa có thay đổi ngày nào !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    foreach (DataRow dr in dtNew.Rows)
                    {
                        if ((bool) dr["GDX"]) strup="update LUU_TSCC set GDX=1 where MA_TS='" + dr["MA_TS"] + "'";
                        else strup = "update LUU_TSCC set GDX=0 where MA_TS='" + dr["MA_TS"] + "'";
                        cls.UpdateDataText(strup);
                        string strupmt = "update LUU_TSCC set DE_NGHI=N'"+ dr["DE_NGHI"] + "' where MA_TS='" + dr["MA_TS"] + "'";
                       // MessageBox.Show(strupmt);
                        cls.UpdateDataText(strupmt);
                    }
                    MessageBox.Show("Cập nhật thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                string mau = "";
                mau = "4";
                string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                string sqlload =
                    "select '" + mau + "' MAU,'" + ng + "' NGAY,a.*,b.PO_TEN from LUU_TSCC a left join DMPOS b on a.POS_CD=b.PO_MA " +
                    "where a.GDX=1 and a.TRANGTHAI='A' and a.POS_CD='" + CboPos.SelectedValue + "' order by a.MAPHONG,a.LOAI_TS_CHITIET,a.MA_NHANHIEU_TS,a.MA_TS";
                string sqlth =
                    "select '" + mau + "' MAU,'" + ng + "' NGAY,a.*,b.PO_TEN from LUU_TSCC a left join DMPOS b on a.POS_CD=b.PO_MA " +
                    "where a.LOAI_TS_CHITIET='TI1' and a.GDX=1 and a.TRANGTHAI='A' and a.MA_NHANHIEU_TS in ('TI11','TI12','TI13','TI19','MM19') order by a.LOAI_TS_CHITIET,a.MA_NHANHIEU_TS,a.MA_TS";

                var dtin = cls.LoadDataText(sqlload);
                var dtth = cls.LoadDataText(sqlth);
                if (dtin.Rows.Count > 0)
                {
                    if (chkTh.IsChecked == true)
                    {
                        rpt_TSCC_GDX01 rpt = new rpt_TSCC_GDX01();
                        RPUtility.ShowRp(rpt, dtth, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        //RPUtility.ShowRp(rpt, dtxa, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                    }
                    else
                    {
                        rpt_TSCC_GDX rpt = new rpt_TSCC_GDX();
                        RPUtility.ShowRp(rpt, dtin, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        //RPUtility.ShowRp(rpt, dtxa, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                    }
                }
                else
                    MessageBox.Show("Không có dữ liệu !", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    textBlock.Text = dr["MOTA"].ToString();
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

 
    }

}
