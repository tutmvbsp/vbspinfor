using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfLaiTon.xaml
    /// </summary>
    public partial class WpfTscc02
    {
        public WpfTscc02()
        {
            InitializeComponent();
        }
        
        ClsServer cls = new ClsServer();
        DataTable _dt = new DataTable();       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //   // dtpNgay.SelectedDate = DateTime.Now;

                cls.ClsConnect();
                var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString()).AddMonths(-1);
                DateTime lastMonth = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month,DateTime.DaysInMonth(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month));
                dtpNgay.SelectedDate = lastMonth;
                string sql;
                if (BienBll.NdMadv.Trim() == BienBll.MainPos.Trim())
                    sql = "select PO_MA MA,PO_TEN TEN from DMPOS where right(po_ma,2)<>'00' order by PO_MA";
                else
                {
                    sql = "select PO_MA MA,PO_TEN TEN from DMPOS where right(po_ma,2)<>'00' and PO_MA=" + "'" + BienBll.NdMadv.Trim() + "'";
                }
                var dtpos = cls.LoadDataText(sql);
                CboPos.ItemsSource = dtpos.DefaultView;
                CboPos.DisplayMemberPath = "TEN";
                CboPos.SelectedValuePath = "MA";
                CboPos.SelectedIndex = 0;

                string sqlpb = BienBll.NdMadv.Trim() == BienBll.MainPos.Trim() ? "select * from DM_PHONGBAN where LEN(MA) = 2 and MA not in ('29','30','31') order by MA" : "select * from DM_PHONGBAN where LEN(MA) = 2 and MA in ('29','30','31') order by MA";
                var dtpb = cls.LoadDataText(sqlpb);
                CboPhongTo.ItemsSource = dtpb.DefaultView;
                CboPhongTo.DisplayMemberPath = "TEN";
                CboPhongTo.SelectedValuePath = "MA";
                CboPhongTo.SelectedIndex = 1;


            //var dtloaitsct = cls.LoadDataText("select * from LOAI_TS_CHITIET");
            //CboLoaiTSCT.ItemsSource = dtloaitsct.DefaultView;
            //CboLoaiTSCT.DisplayMemberPath = "TEN";
            //CboLoaiTSCT.SelectedValuePath = "MA";
            //CboLoaiTSCT.SelectedIndex = 1;

            //CboLoaiTSCT.ItemsSource = dtloaitsct.DefaultView;
            //CboLoaiTSCT.DisplayMemberPath = "TEN";
            //CboLoaiTSCT.SelectedValuePath = "MA";
            //CboLoaiTSCT.SelectedIndex = 1;
            //var dtcb = cls.LoadDataText("select ND_MA,ND_TEN from NG_DUNG where ND_MADV='"+CboPos.SelectedValue+"' and ND_TTHAI='A' and ND_MADV='"+CboPos.SelectedValue+"' order by ND_TEN");
            //CboCanBo.ItemsSource = dtcb.DefaultView;
            //CboCanBo.DisplayMemberPath = "ND_TEN";
            //CboCanBo.SelectedValuePath = "ND_MA";
            //CboCanBo.SelectedIndex = 1;

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            //}
            cls.DongKetNoi();
        }



        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void dgvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (_dt.Rows.Count > 0)
                {
                    ////DataRow dtr = dt.Rows[0];
                    ////DataRow dr = (DataRow) dgvData.SelectedItems[0];
                    DataRowView dr = (DataRowView) dgvData.SelectedItems[0];
                    TxtNd.Text = dr["MA_TS"].ToString();
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

        private void ShowGrid_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                string sqlload =
                    "select a.MA_TS,a.MA_NHANHIEU_TS,a.TEN_TS,a.SO_LUONG,a.NGUYEN_GIA,a.NGAY_MUA,a.TENPHONG from QT_TSCC a " +
                    "where a.NGAYBC='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and a.POS_CD='" +
                    CboPos.SelectedValue + "' and LOAI_TS_CHITIET='" + CboLoaiTSCT.SelectedValue + "' and right(a.MAPHONG,2)='" + CboPhongTo.SelectedValue + "' order by a.MA_NHANHIEU_TS,a.MA_TS";
                _dt = cls.LoadDataText(sqlload);
                if (_dt.Rows.Count > 0)
                {
                    dgvData.ItemsSource = _dt.DefaultView;
                }
                else
                    MessageBox.Show("Không có dữ liệu !", "Thông báo", MessageBoxButton.OK,
                        MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ok");
        }

        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                string sqlpb = CboPos.SelectedValue.ToString().Trim() == BienBll.MainPos.Trim() ? "select * from DM_PHONGBAN where LEN(MA) = 2 and MA not in ('29','30','31') order by MA" : "select * from DM_PHONGBAN where LEN(MA) = 2 and MA in ('29','30','31') order by MA";
                var dtpb = cls.LoadDataText(sqlpb);
                CboPhongTo.ItemsSource = dtpb.DefaultView;
                CboPhongTo.DisplayMemberPath = "TEN";
                CboPhongTo.SelectedValuePath = "MA";
                CboPhongTo.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }

        private void CboPhongTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                var dtcb = cls.LoadDataText("select ND_MA,ND_TEN from NG_DUNG where ND_MADV='" + CboPos.SelectedValue + "' and ND_TTHAI='A' and ND_PHONGBAN='" + CboPhongTo.SelectedValue + "' order by ND_TEN");
                CboCanBo.ItemsSource = dtcb.DefaultView;
                CboCanBo.DisplayMemberPath = "ND_TEN";
                CboCanBo.SelectedValuePath = "ND_MA";
                CboCanBo.SelectedIndex = 1;


            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi : " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }
    }

}
