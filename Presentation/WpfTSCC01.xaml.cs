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
    public partial class WpfTSCC01 : Window
    {
        public WpfTSCC01()
        {
            InitializeComponent();
        }

        private ToolBll s = new ToolBll();
        HardwareInfo infor= new HardwareInfo();
        ServerInfor srv = new ServerInfor();
        private readonly ClsServer cls = new ClsServer();
        private DataTable dt = new DataTable();
        string Thumuc = "C:\\SaoKe";
        private string FileName = "";
        string strpos = "";
        string strphong = "";
        private string str = "";
        private string str1 = "";
        private string str2 = "";
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                var dtng = cls.LoadDataText("select MAX(NGAYBC) as NGMAX from QT_TSCC");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
                //DateTime lastMonth = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month, DateTime.DaysInMonth(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month));
                if (BienBll.NdMadv == BienBll.MainPos)
                {
                    strpos = "select PO_MA MA,PO_TEN TEN from DMPOS  order by PO_MA";
                    //strphong = "select * from DM_PHONGBAN where ma in ('17','18','19','20','21','22','34')";
                    strphong = "select * from DM_PHONGBAN where ma not in ('98','99')";
                }
                else
                {
                    strpos = "select PO_MA MA,PO_TEN TEN from DMPOS where PO_MA='" + CboPos.SelectedValue.ToString().Trim() + "'";
                    strphong = "select * from DM_PHONGBAN where ma not in ('17','18','19','20','21','22','34')";
                }
                //strpos = "select PO_MA MA,PO_TEN TEN from DMPOS where PO_MA='" + BienBll.NdMadv + "'";
                //if (BienBll.NdMadv == BienBll.MainPos)
                //    strphong = "select * from DM_PHONGBAN where ma in ('17','18','19','20','21','22','34')";
                //else strphong = "select * from DM_PHONGBAN where ma not in ('17','18','19','20','21','22','34')";

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
                string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                DateTime newdate = new DateTime(dtpNgay.SelectedDate.Value.AddYears(-1).Year + 1, 1, 1);
                DateTime LastDayYear = new DateTime(dtpNgay.SelectedDate.Value.Year, 12, 31);
                string mau = ""; //1 đang dùng , 2 : thanh lý . 3 ; mua mới
                //Enday off year
                //MessageBox.Show(newdate.AddDays(-1).ToString("yyyy-MM-dd"));
                cls.ClsConnect();
                if (Ration1.IsChecked==true) //Đang dùng
                {
                    //dt.Clear();
                    //string sqlload =
                    //    "select a.MA_TS,a.MA_NHANHIEU_TS,a.TEN_TS,a.SO_LUONG,a.NGUYEN_GIA,a.NGAY_MUA,a.TENPHONG from QT_TSCC a " +
                    //    "where a.NGAYBC='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and a.POS_CD='" +
                    //    CboPos.SelectedValue +
                    //    "' and LOAI_TS_CHITIET='" + CboLoaiTSCT.SelectedValue + "' and right(MAPHONG,2)='"+s.Right(CboPhong.SelectedValue.ToString().Trim(),2)+"' order by a.MA_NHANHIEU_TS,a.MA_TS";
                    //MessageBox.Show(sqlload);
                    //,b.PO_TEN  from LUU_TSCC a left join DMPOS b on a.POS_CD=b.PO_MA
                    mau = "1";
                    string sqlload =
                        "select '"+mau+"' MAU,'"+ng+ "' NGAY,a.*,b.PO_TEN from LUU_TSCC a left join DMPOS b on a.POS_CD=b.PO_MA " +
                        "where a.TRANGTHAI='A' and a.POS_CD='" + CboPos.SelectedValue +"' and right(MAPHONG,2)='" + s.Right(CboPhong.SelectedValue.ToString().Trim(), 2) + "' order by a.LOAI_TS_CHITIET,a.MA_NHANHIEU_TS,a.MA_TS";
                    //MessageBox.Show(sqlload);
                    dt = cls.LoadDataText(sqlload);
                    if (dt.Rows.Count > 0)
                    {
                        dgvData.ItemsSource = dt.DefaultView;
                    }
                    else
                        MessageBox.Show("Không có dữ liệu !", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Information);}
                else if (Ration2.IsChecked == true) // thanh ly
                {
                    //// dt.Clear();
                    // string str =
                    //     "select a.* from QT_TSCC a " +
                    //     "where a.NGAYBC='" + newdate.AddDays(-1).ToString("yyyy-MM-dd") + "' and a.POS_CD='" +
                    //     CboPos.SelectedValue + "'and LOAI_TS_CHITIET='" + CboLoaiTSCT.SelectedValue + "' and right(MAPHONG,2)='" + s.Right(CboPhong.SelectedValue.ToString().Trim(), 2) + "' and a.MA_TS not in " +
                    //     " (select MA_TS from QT_TSCC  where NGAYBC='" +
                    //     dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' " +
                    //     "and POS_CD='" + CboPos.SelectedValue +
                    //     "' and  MA_TS=a.MA_TS) order by a.MA_NHANHIEU_TS,a.MA_TS";
                    // //MessageBox.Show(str);
                    // dt = cls.LoadDataText(str);
                    // if (dt.Rows.Count > 0)
                    // {
                    //     dgvData.ItemsSource = dt.DefaultView;
                    // }
                    // else
                    //     MessageBox.Show("Không có dữ liệu !", "Thông báo", MessageBoxButton.OK,
                    //         MessageBoxImage.Information);

                    mau = "2";
                    string str = "select '" + mau + "' MAU,'" + ng + "' NGAY,a.*,b.PO_TEN from LUU_TSCC a left join DMPOS b on a.POS_CD=b.PO_MA where a.POS_CD='" +
                         CboPos.SelectedValue + "' and right(MAPHONG,2)='" + s.Right(CboPhong.SelectedValue.ToString().Trim(), 2) + "'" +
                                 " and a.TRANGTHAI='C' and a.MA_TS " +
                                 " in (select MA_TS from QT_TSCC where NGAYBC = '" + newdate.AddDays(-1).ToString("yyyy-MM-dd") + "')" +
                                 " and a.MA_TS not in (select MA_TS from QT_TSCC where NGAYBC = '" + LastDayYear.ToString("yyyy-MM-dd") + "') order by a.LOAI_TS_CHITIET,a.MA_NHANHIEU_TS,a.MA_TS";
                    dt = cls.LoadDataText(str);
                    if (dt.Rows.Count > 0)
                    {
                        dgvData.ItemsSource = dt.DefaultView;
                    }
                    else
                        MessageBox.Show("Không có dữ liệu !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    //MessageBox.Show(newdate.AddDays(-1).ToString("yyyy-MM-dd")+"    "+ LastDayYear.ToString("yyyy-MM-dd"));
                }
                else // mua moi
                {
                    // dt.Clear();
                    //string strmoi =
                    //"select a.* from QT_TSCC a " +
                    //"where a.NGAYBC='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and a.POS_CD='" +
                    //CboPos.SelectedValue + "'and LOAI_TS_CHITIET='" + CboLoaiTSCT.SelectedValue + "' and right(MAPHONG,2)='" + s.Right(CboPhong.SelectedValue.ToString().Trim(), 2) + "' and a.MA_TS not in " +
                    //" (select MA_TS from QT_TSCC  where NGAYBC='" +
                    //newdate.AddDays(-1).ToString("yyyy-MM-dd") + "' " +
                    //"and POS_CD='" + CboPos.SelectedValue +
                    //"' and  MA_TS=a.MA_TS) order by a.MA_NHANHIEU_TS,a.MA_TS";
                    mau = "3";
                    string str = "select '" + mau + "' MAU,'" + ng + "' NGAY,a.*,b.PO_TEN  from LUU_TSCC a left join DMPOS b on a.POS_CD=b.PO_MA where a.POS_CD='" +
                               CboPos.SelectedValue + "' and right(a.MAPHONG,2)='" + s.Right(CboPhong.SelectedValue.ToString().Trim(), 2)+ "' and year(a.NGAY_MUA)='"+ dtpNgay.SelectedDate.Value.ToString("yyyy") + "' order by a.LOAI_TS_CHITIET,a.MA_NHANHIEU_TS,a.MA_TS";
                    //MessageBox.Show(str);
                    dt = cls.LoadDataText(str);
                    if (dt.Rows.Count > 0)
                    {
                        dgvData.ItemsSource = dt.DefaultView;
                    }
                    else
                        MessageBox.Show("Không có dữ liệu !", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void TongHop_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                string pos = CboPos.SelectedValue.ToString().Trim();
                DateTime newdate = new DateTime(dtpNgay.SelectedDate.Value.AddYears(-1).Year + 1, 1, 1);
                DateTime LastDayYear = new DateTime(dtpNgay.SelectedDate.Value.Year, 12, 31);
                cls.ClsConnect();
                str = "select *,(select po_ten from DMPOS where po_ma=pos) po_ten from( "
                            + " select NGAYBC NGAY,'" + pos+"' POS, MA_TS, LOAI_TS, TEN_LOAI_TS, MA_NHANHIEU_TS, TEN_NHANHIEU_TS, right(MAPHONG, 2) maph from LUU_TSCC where TRANGTHAI = 'A' and POS_CD = '"+pos+"'"
                            +" ) as nguon pivot (count(ma_ts) for maph in ([17],[18],[19], [20], [21], [22], [29], [30], [31], [34])) as dich"
                            +" where [17] +[18] + [19] + [20] + [21] + [22] + [29] + [30] + [31] + [34] > 0  order by LOAI_TS,MA_NHANHIEU_TS";
                str1 = "select *,N'Tổng Hợp' po_ten from ( "
                        +" select NGAYBC NGAY,N'Tổng Hợp' POS, MA_TS, LOAI_TS, TEN_LOAI_TS, MA_NHANHIEU_TS, TEN_NHANHIEU_TS, right(MAPHONG, 2) maph from LUU_TSCC where TRANGTHAI = 'A'"
                        +" and LOAI_TS_CHITIET in ('MM1','TI1') and MA_NHANHIEU_TS in ('MM11','MM12','TI11','TI12','TI13','TI19')) as nguon pivot(count(ma_ts) for maph in ([17],[18],[19], [20], [21], [22], [29], [30], [31], [34])) as dich"
                        +" where[17] + [18] + [19] + [20] +  [21] +  [22] + [29] + [30] + [31] + [34] > 0  order by LOAI_TS,MA_NHANHIEU_TS";
                //MessageBox.Show(str);
                dt = cls.LoadDataText(str);
                var dt1 = cls.LoadDataText(str1);
                if (dt.Rows.Count > 0)
                {
                    rpt_TSCC_TH rpt = new rpt_TSCC_TH();
                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                    RPUtility.ShowRp(rpt, dt1, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());

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
        private void Excel_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                string pos = CboPos.SelectedValue.ToString().Trim();
                cls.ClsConnect();
                if (pos.Trim() == "003000")
                {
                    str = "select '" + ng +
                                 "' NGAY,a.POS_CD,(case when a.MA_NHANHIEU_TS in ('MM11','MM12') then N'Máy Chủ'"
                                 + " when a.MA_NHANHIEU_TS = 'TI11' then N'Máy tính để bàn (PC)'"
                                 + " when a.MA_NHANHIEU_TS = 'TI12' then N'Máy tính xách tay'"
                                 + " when a.MA_NHANHIEU_TS = 'TI13' then N'Máy in'"
                                 + " when a.MA_NHANHIEU_TS = 'TI19' then N'Thiết bị khác'"
                                 +
                                 " end ) LOAITB,a.MA_TS,a.TEN_TS,a.SO_LUONG,a.NGUYEN_GIA,NGAY_MUA,MOTA CAUHINH, isnull(MAPHONG,'') MAPHONG,isnull(TENPHONG, '') TENPHONG,CB_QUANLY,a.TRANGTHAI"
                                 + " ,(case when a.TRANGTHAI = 'C' then 'X' else '' end) THANHLY"
                                 + " ,(case when DATEPART(YEAR, a.NGAY_MUA)= DATEPART(YEAR, '" + ng +
                                 "') then 'X'  else '' end) MUAMOI"
                                 +
                                 " ,DATEPART(YEAR, a.NGAY_MUA) NAM from LUU_TSCC a where LOAI_TS_CHITIET in ('MM1','TI1') and MA_NHANHIEU_TS in ('MM11','MM12','TI11','TI12','TI13','TI19')"
                                 + " order by a.MA_NHANHIEU_TS,a.POS_CD,a.MAPHONG,a.NGAY_MUA";
                    str1 = "select '" + ng +
                                  "' NGAY,a.POS_CD,(case when a.MA_NHANHIEU_TS in ('MM11','MM12') then N'Máy Chủ'"
                                  + " when a.MA_NHANHIEU_TS = 'TI11' then N'Máy tính để bàn (PC)'"
                                  + " when a.MA_NHANHIEU_TS = 'TI12' then N'Máy tính xách tay'"
                                  + " when a.MA_NHANHIEU_TS = 'TI13' then N'Máy in'"
                                  + " when a.MA_NHANHIEU_TS = 'TI19' then N'Thiết bị khác'"
                                  +
                                  " end ) LOAITB,a.MA_TS,a.TEN_TS,a.SO_LUONG,a.NGUYEN_GIA,NGAY_MUA,MOTA CAUHINH, isnull(MAPHONG,'') MAPHONG,isnull(TENPHONG, '') TENPHONG,CB_QUANLY,a.TRANGTHAI"
                                  + " ,(case when a.TRANGTHAI = 'C' then 'X' else '' end) THANHLY"
                                  + " ,(case when DATEPART(YEAR, a.NGAY_MUA)= DATEPART(YEAR, '" + ng +
                                  "') then 'X'  else '' end) MUAMOI"
                                  +
                                  " ,DATEPART(YEAR, a.NGAY_MUA) NAM from LUU_TSCC a where a.TRANGTHAI<>'C' and LOAI_TS_CHITIET in ('MM1','TI1') and MA_NHANHIEU_TS in ('MM11','MM12','TI11','TI12','TI13','TI19')"
                                  + " order by a.MA_NHANHIEU_TS,a.POS_CD,a.MAPHONG,a.NGAY_MUA";
                    str2 = "select '" + ng +
                                  "' NGAY,a.POS_CD,(case when a.MA_NHANHIEU_TS in ('MM11','MM12') then N'Máy Chủ'"
                                  + " when a.MA_NHANHIEU_TS = 'TI11' then N'Máy tính để bàn (PC)'"
                                  + " when a.MA_NHANHIEU_TS = 'TI12' then N'Máy tính xách tay'"
                                  + " when a.MA_NHANHIEU_TS = 'TI13' then N'Máy in'"
                                  + " when a.MA_NHANHIEU_TS = 'TI19' then N'Thiết bị khác'"
                                  +
                                  " end ) LOAITB,a.MA_TS,a.TEN_TS,a.SO_LUONG,a.NGUYEN_GIA,NGAY_MUA,MOTA CAUHINH, isnull(MAPHONG,'') MAPHONG,isnull(TENPHONG, '') TENPHONG,CB_QUANLY,a.TRANGTHAI"
                                  + " ,(case when a.TRANGTHAI = 'C' then 'X' else '' end) THANHLY"
                                  + " ,(case when DATEPART(YEAR, a.NGAY_MUA)= DATEPART(YEAR, '" + ng +
                                  "') then 'X'  else '' end) MUAMOI"
                                  +
                                  " ,DATEPART(YEAR, a.NGAY_MUA) NAM from LUU_TSCC a where a.TRANGTHAI<>'C' and LOAI_TS_CHITIET in ('MM1','TI1') and MA_NHANHIEU_TS in ('MM11','MM12','TI11','TI12','TI13','TI19')"
                                  + " and DATEPART(YEAR, a.NGAY_MUA)= DATEPART(YEAR, '" + ng +"') order by a.MA_NHANHIEU_TS,a.POS_CD,a.MAPHONG,a.NGAY_MUA";

                }
                else // theo pos
                {
                    str = "select '" + ng +
                                 "' NGAY,a.POS_CD,(case when a.MA_NHANHIEU_TS in ('MM11','MM12') then N'Máy Chủ'"
                                 + " when a.MA_NHANHIEU_TS = 'TI11' then N'Máy tính để bàn (PC)'"
                                 + " when a.MA_NHANHIEU_TS = 'TI12' then N'Máy tính xách tay'"
                                 + " when a.MA_NHANHIEU_TS = 'TI13' then N'Máy in'"
                                 + " when a.MA_NHANHIEU_TS = 'TI19' then N'Thiết bị khác'"
                                 +
                                 " end ) LOAITB,a.MA_TS,a.TEN_TS,a.SO_LUONG,a.NGUYEN_GIA,NGAY_MUA,MOTA CAUHINH, isnull(MAPHONG,'') MAPHONG,isnull(TENPHONG, '') TENPHONG,CB_QUANLY,a.TRANGTHAI"
                                 + " ,(case when a.TRANGTHAI = 'C' then 'X' else '' end) THANHLY"
                                 + " ,(case when DATEPART(YEAR, a.NGAY_MUA)= DATEPART(YEAR, '" + ng +
                                 "') then 'X'  else '' end) MUAMOI"
                                 +
                                 " ,DATEPART(YEAR, a.NGAY_MUA) NAM from LUU_TSCC a where a.POS_CD='"+pos+"' and LOAI_TS_CHITIET in ('MM1','TI1') and MA_NHANHIEU_TS in ('MM11','MM12','TI11','TI12','TI13','TI19')"
                                 + " order by a.MA_NHANHIEU_TS,a.POS_CD,a.MAPHONG,a.NGAY_MUA";
                    str1 = "select '" + ng +
                                  "' NGAY,a.POS_CD,(case when a.MA_NHANHIEU_TS in ('MM11','MM12') then N'Máy Chủ'"
                                  + " when a.MA_NHANHIEU_TS = 'TI11' then N'Máy tính để bàn (PC)'"
                                  + " when a.MA_NHANHIEU_TS = 'TI12' then N'Máy tính xách tay'"
                                  + " when a.MA_NHANHIEU_TS = 'TI13' then N'Máy in'"
                                  + " when a.MA_NHANHIEU_TS = 'TI19' then N'Thiết bị khác'"
                                  +
                                  " end ) LOAITB,a.MA_TS,a.TEN_TS,a.SO_LUONG,a.NGUYEN_GIA,NGAY_MUA,MOTA CAUHINH, isnull(MAPHONG,'') MAPHONG,isnull(TENPHONG, '') TENPHONG,CB_QUANLY,a.TRANGTHAI"
                                  + " ,(case when a.TRANGTHAI = 'C' then 'X' else '' end) THANHLY"
                                  + " ,(case when DATEPART(YEAR, a.NGAY_MUA)= DATEPART(YEAR, '" + ng +
                                  "') then 'X'  else '' end) MUAMOI"
                                  +
                                  " ,DATEPART(YEAR, a.NGAY_MUA) NAM from LUU_TSCC a where a.POS_CD='" + pos + "' and a.TRANGTHAI<>'C' and LOAI_TS_CHITIET in ('MM1','TI1') and MA_NHANHIEU_TS in ('MM11','MM12','TI11','TI12','TI13','TI19')"
                                  + " order by a.MA_NHANHIEU_TS,a.POS_CD,a.MAPHONG,a.NGAY_MUA";
                    str2 = "select '" + ng +
                                  "' NGAY,a.POS_CD,(case when a.MA_NHANHIEU_TS in ('MM11','MM12') then N'Máy Chủ'"
                                  + " when a.MA_NHANHIEU_TS = 'TI11' then N'Máy tính để bàn (PC)'"
                                  + " when a.MA_NHANHIEU_TS = 'TI12' then N'Máy tính xách tay'"
                                  + " when a.MA_NHANHIEU_TS = 'TI13' then N'Máy in'"
                                  + " when a.MA_NHANHIEU_TS = 'TI19' then N'Thiết bị khác'"
                                  +
                                  " end ) LOAITB,a.MA_TS,a.TEN_TS,a.SO_LUONG,a.NGUYEN_GIA,NGAY_MUA,MOTA CAUHINH, isnull(MAPHONG,'') MAPHONG,isnull(TENPHONG, '') TENPHONG,CB_QUANLY,a.TRANGTHAI"
                                  + " ,(case when a.TRANGTHAI = 'C' then 'X' else '' end) THANHLY"
                                  + " ,(case when DATEPART(YEAR, a.NGAY_MUA)= DATEPART(YEAR, '" + ng +
                                  "') then 'X'  else '' end) MUAMOI"
                                  +
                                  " ,DATEPART(YEAR, a.NGAY_MUA) NAM from LUU_TSCC a where a.POS_CD='" + pos + "' and a.TRANGTHAI<>'C' and LOAI_TS_CHITIET in ('MM1','TI1') and MA_NHANHIEU_TS in ('MM11','MM12','TI11','TI12','TI13','TI19')"
                                  + " and DATEPART(YEAR, a.NGAY_MUA)= DATEPART(YEAR, '" + ng +"') order by a.MA_NHANHIEU_TS,a.POS_CD,a.MAPHONG,a.NGAY_MUA";

                }
                //MessageBox.Show(str);
                dt = cls.LoadDataText(str);
                var dt1 = cls.LoadDataText(str1);
                var dt2 = cls.LoadDataText(str2);
                if (dt.Rows.Count > 0)
                {
                    FileName = Thumuc + "\\" + pos + "_" + "Sao kê thiết bị"+ng+"_" + ".xlsx";
                    string FileName1 = Thumuc + "\\" + pos + "_" + "Sao kê thiết bị đang dùng " + ng + "_" + ".xlsx";
                    string FileName2 = Thumuc + "\\" + pos + "_" + "Sao kê thiết bị mua mới trong năm " + ng + "_" + ".xlsx";
                    s.ExportToExcel(dt, FileName);
                    s.ExportToExcel(dt1, FileName1);
                    s.ExportToExcel(dt2, FileName2);
                    MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    s.OpenExcel(FileName);
                    s.OpenExcel(FileName1);
                    s.OpenExcel(FileName2);
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
            try
            {
                DateTime newdate = new DateTime(dtpNgay.SelectedDate.Value.AddYears(-1).Year + 1, 1, 1);
                if (dt.Rows.Count > 0)
                {
                    rpt_TSCC_SaoKe rpt = new rpt_TSCC_SaoKe();
                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                        srv.DbPassSerVer());
                } else
                    MessageBox.Show("Không có dữ liệu !", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Information);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Không thực hiện cập nhật tại đây !", "Thông báo", MessageBoxButton.OK,MessageBoxImage.Information);
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Không thực hiện xóa tại đây !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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
                string strsql = "select distinct LOAI_TS MA,TEN_LOAI_TS TEN from QT_TSCC where NGAYBC='" + ng +
                                "' and POS_CD='" + pos + "' and RIGHT(MAPHONG,2)='"+CboPhong.SelectedValue.ToString().Trim()+"' order by LOAI_TS";
                var dtloaits = cls.LoadDataText(strsql);
                //CboLoaiTS.ItemsSource = dtloaits.DefaultView;
                //CboLoaiTS.DisplayMemberPath = "TEN";
                //CboLoaiTS.SelectedValuePath = "MA";
                //CboLoaiTS.SelectedIndex = 1;

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

        private void ShowAdd_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                var dtup =
                    cls.LoadDataText(
                        "select *,N'' MOTA,N'' CB_QUANLY,'A' TRANGTHAI from QT_TSCC where NGAYBC='"+ dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and MA_TS not in (select MA_TS from LUU_TSCC)");
                if (dtup.Rows.Count > 0)
                {
                    cls.UpdateDataText(
                        " insert into LUU_TSCC select *, N'' MOTA, N'' CB_QUANLY ,'A' TRANGTHAI,'"+ dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' NGAY,'0' MAU,'0' GDX,'0' DG_THANHLY,'' NG_DG_THANHLY,'0' DG_BAOTRI,'' NG_DG_BAOTRI,'' MA_CIF,'' DE_NGHI from QT_TSCC where NGAYBC = '"+ dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and MA_TS not in (select MA_TS from LUU_TSCC)");
                    cls.UpdateDataText("update LUU_TSCC set NGAYBC='"+ dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "'");
                    MessageBox.Show("OK đã cập nhật dữ liệu mới nhất !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else MessageBox.Show("OK dữ liệu đã đầy đủ !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                cls.UpdateDataText("with lst1 as ( "
                                   +
                                   " select * from LUU_TSCC where MA_TS not in (select MA_TS from QT_TSCC where NGAYBC = (select max(ngaybc) from QT_TSCC))"
                                   + ") update a set a.TRANGTHAI = 'C' from LUU_TSCC a, LST1 b where a.MA_TS = b.MA_TS");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }
    }

}
