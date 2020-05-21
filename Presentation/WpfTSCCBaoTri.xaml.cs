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
    public partial class WpfTSCCBaoTri : Window
    {
        public WpfTSCCBaoTri()
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
                //var dtng = cls.LoadDataText("select MAX(NGAYBC) as NGMAX from QT_TSCC");
                dtpNgay.SelectedDate = DateTime.Now;
                //DateTime lastMonth = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month, DateTime.DaysInMonth(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month));
                //if (BienBll.NdMadv == BienBll.MainPos)
                //{
                //    strpos = "select PO_MA MA,PO_TEN TEN from DMPOS  order by PO_MA";
                //    strphong = "select * from DM_PHONGBAN order by MA";
                //}
                //else
                //{
                //    strpos = "select PO_MA MA,PO_TEN TEN from DMPOS where PO_MA='"+CboPos.SelectedValue.ToString().Trim()+"'";
                //    strphong = "select * from DM_PHONGBAN where ma not in ('17','18','19','20','21','22','34')";
                //}
                strpos = "select PO_MA MA,PO_TEN TEN from DMPOS where PO_MA='" + BienBll.NdMadv + "'";
                if (BienBll.NdMadv == BienBll.MainPos)
                    strphong = "select * from DM_PHONGBAN where ma  in ('17','18','19','20','21','22','34') order by MA";
                else strphong = "select * from DM_PHONGBAN where ma not in ('17','18','19','20','21','22','34')";
                //strphong = "select * from DM_PHONGBAN where MA='"+BienBll.PhongBan+"'";
                var dtpos = cls.LoadDataText(strpos);
                CboPos.ItemsSource = dtpos.DefaultView;
                CboPos.DisplayMemberPath = "TEN";
                CboPos.SelectedValuePath = "MA";
                var dtphong = cls.LoadDataText(strphong);
                CboPhong.ItemsSource = dtphong.DefaultView;
                CboPhong.DisplayMemberPath = "TEN";
                CboPhong.SelectedValuePath = "MA";
    

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
                string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                cls.ClsConnect();
                string sqlload =
                    "select *,'"+ng+ "' NG_DG_SC,'' LYDO,'' DENGHI ,'0' MNV,'" + ng + "' NG_BAOTRI from LUU_TSCC " +
                    "where LOAI_TS_CHITIET='TI1' and TRANGTHAI='A' and POS_CD='" + CboPos.SelectedValue + "' and right(MAPHONG,2)='" + s.Right(CboPhong.SelectedValue.ToString().Trim(), 2) + "' order by LOAI_TS_CHITIET,MA_NHANHIEU_TS,MA_TS";
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
                        //strup = "insert into TSCC_HIS SELECT *,@Ngay NG_DG_SC,'' LYDO,'' DENGHI ,'0' MNV,@Ngay NG_BAOTRI FROM LUU_TSCC WHERE MA_TS = @MaTs";
                        if ((bool)dr["DG_BAOTRI"]) strup = "update LUU_TSCC set DG_BAOTRI=1,NG_DG_BAOTRI='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where MA_TS='" + dr["MA_TS"] + "'";
                        else strup = "update LUU_TSCC set DG_BAOTRI=0,NG_DG_BAOTRI='' where MA_TS='" + dr["MA_TS"] + "'";
                        cls.UpdateDataText(strup);
                    }
                    MessageBox.Show("Cập nhật thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                int thamso = 2;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Ngay";
                giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                bien[1] = "@Phong";
                giatri[1] = CboPhong.SelectedValue.ToString().Trim();
                var dtin = cls.LoadDataProcPara("usp_TSCC_BAOTRI", bien, giatri, thamso);
                if (dtin.Rows.Count > 0)
                {
                    rpt_TSCC_Hist rpt = new rpt_TSCC_Hist();
                    RPUtility.ShowRp(rpt, dtin, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                        srv.DbPassSerVer());
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
