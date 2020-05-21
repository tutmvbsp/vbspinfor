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
    public partial class WpfKhaoSatVay : Window
    {
        public WpfKhaoSatVay()
        {
            InitializeComponent();
        }

        private ToolBll s = new ToolBll();
        private readonly ClsServer cls = new ClsServer();
        private DataTable dt = new DataTable();
        private DataTable dtNew = new DataTable();                
        ServerInfor srv = new ServerInfor();
        string mau = "";
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
               // dtpNgay.SelectedDate = DateTime.Now;

                cls.ClsConnect();
                var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString()).AddMonths(-1);
                //DateTime lastMonth = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month,
                //DateTime.DaysInMonth(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month));
                //dtpNgay.SelectedDate = lastMonth;
                string sql;
                sql = "select PO_MA MA,PO_TEN TEN from DMPOS where PO_MA=" + "'" + BienBll.NdMadv.Trim() + "'";
                var dtpos = cls.LoadDataText(sql);
                CboPos.ItemsSource = dtpos.DefaultView;
                CboPos.DisplayMemberPath = "TEN";
                CboPos.SelectedValuePath = "MA";
                CboPos.SelectedIndex = 0;
                var sqlpb = "select a.MA,a.TEN from DMXA a where a.PGD_QL='"+CboPos.SelectedValue+"' and right(a.MA,2)<>'00' order by a.MA";
                var dtloaits = cls.LoadDataText(sqlpb);
                CboXa.ItemsSource = dtloaits.DefaultView;
                CboXa.DisplayMemberPath = "TEN";
                CboXa.SelectedValuePath = "MA";
                CboXa.SelectedIndex = 0;

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
                cls.ClsConnect();
                //string strchk=
                //"select * from LUU_KHAOSATVAY where DOT='" + CboDot.SelectionBoxItem + "' and NAM='" + dtpNgay.SelectedDate.Value.ToString("yyyy") + "'";
                //var dtchk = cls.LoadDataText(strchk);
                //if (dtchk.Rows.Count == 0)
                //{
                //    string strins = "insert into LUU_KHAOSATVAY select '" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' TUNGAY,'" +
                //                    dtpNgay.SelectedDate.Value.AddMonths(6).ToString("yyyy-MM-dd") +
                //                    "' DENNGAY,a.MAPOS,'UBND '+d.TEN TENHUYEN,a.MAXA,b.TEN TENXA,a.MATHON,c.TEN TENTHON,a.MATO,a.TEN,a.TEN_TO "
                //                    + " ,0 SH01,0 ST01,0 SH19,0 ST19,0 SH09,0 ST09,0 SH06,0 ST06,0 SH10,0 ST10,'"+ CboDot.SelectionBoxItem + "' DOT "
                //                    +",'"+ dtpNgay.SelectedDate.Value.ToString("yyyy") + "' NAM ,0 SH_CHUA,0 SH_KHONG, a.TRANGTHAI from TTTO a, DMXA b,DMTHON c, DMHUYEN d "
                //                    + " where a.MAXA = b.MA and a.MATHON = c.MA and a.MAPOS = d.MA order by a.MATHON,a.MATO";
                //    cls.UpdateDataText(strins);
                //}
                string strins = "insert into LUU_KHAOSATVAY select '" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' TUNGAY,'" + dtpNgay.SelectedDate.Value.AddMonths(6).ToString("yyyy-MM-dd") + "' DENNGAY, "
                    + " a.TO_MAPGD MAPOS,'UBND ' + (select TEN from DMHUYEN where MAPOS = a.TO_MAPGD) TENHUYEN,LEFT(a.TO_MADP, 6) MAXA "
                    + " ,(select TEN from DMXA where MA = left(a.TO_MADP, 6)) TENXA,a.TO_MADP MATHON,(select TEN from DMTHON where MA = a.TO_MADP) TENTHON "
                    + " ,a.TO_MATO MATO, a.TO_TENTT TEN,(select TEN from DMTHON where MA = a.TO_MADP) TEN_TO,0 SH01,0 ST01,0 SH19,0 ST19,0 SH09 "
                    + " ,0 ST09,0 SH06,0 ST06,0 SH10,0 ST10, '" + CboDot.SelectionBoxItem + "' DOT,'" + dtpNgay.SelectedDate.Value.ToString("yyyy") + "' NAM,0 SH_CHUA,0 SH_KHONG,a.TRANGTHAI "
                    + " from HSTO a where a.TO_MATO not in (select MATO from LUU_KHAOSATVAY where MATO = a.TO_MATO) and a.TRANGTHAI <> 'C' ";
                cls.UpdateDataText(strins);
                string str = "select * from LUU_KHAOSATVAY where DOT='" + CboDot.SelectionBoxItem + "' and NAM='" + dtpNgay.SelectedDate.Value.ToString("yyyy") + "' and MAXA='"+CboXa.SelectedValue+"' and TRANGTHAI='A'";
                dt = cls.LoadDataText(str);
                if (dt.Rows.Count > 0)
                {
                    dgvData.ItemsSource = dt.DefaultView;
                } else
                    MessageBox.Show("Không có dữ liệu !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                dtNew = dt.GetChanges();
                if (dtNew == null)
                    MessageBox.Show("Không có thay đổi nào !", "Thông báo", MessageBoxButton.OK,MessageBoxImage.Warning);
                else
                {
                    foreach (DataRow dr in dtNew.Rows)
                    {
                        string upd = "update LUU_KHAOSATVAY set SH01='" + dr["SH01"] + "',ST01='" + dr["ST01"] + "',SH09='" + dr["SH09"] + "',ST09='" + dr["ST09"]
                            + "',SH19='" + dr["SH19"] + "',ST19='" + dr["ST19"] + "',SH06='" + dr["SH06"] + "',ST06='" + dr["ST06"]
                            + "',SH10='" + dr["SH10"] + "',ST10='" + dr["ST10"] + "',SH_CHUA='"+ dr["SH_CHUA"] + "',SH_KHONG='"+ dr["SH_KHONG"] + "' where DOT='" + CboDot.SelectionBoxItem + "' and NAM='" + dtpNgay.SelectedDate.Value.ToString("yyyy") + "' and MATO='" + dr["MATO"] + "'";
                        cls.UpdateDataText(upd);
                    }
                    MessageBox.Show("Cập nhật thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                if (opt1.IsChecked == true)
                {
                    mau = "2";
                    string strin = "select * from LUU_KHAOSATVAY where DOT='" + CboDot.SelectionBoxItem + "' and NAM='" +
                                   dtpNgay.SelectedDate.Value.ToString("yyyy") + "' and MAXA='" + CboXa.SelectedValue +
                                   "'";
                    var dtin = cls.LoadDataText(strin);
                    if (dtin.Rows.Count > 0)
                    {
                        rpt_ksVay01 rpt = new rpt_ksVay01();
                        RPUtility.ShowRp(rpt, dtin, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                            srv.DbPassSerVer());
                    }
                    else MessageBox.Show("Không có dữ liệu !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                } if (opt2.IsChecked == true) //Mau 03
                {
                    mau = "3";
                    string strin = "select '" + mau + "' MAU,N'nhcsxh tỉnh quảng bình' title1,b.PO_TEN title2,a.DOT,a.NAM,a.MAPOS,b.PO_TEN,a.MAXA,a.TENXA,COUNT(distinct MATHON) SOTHON,COUNT(distinct MATO) SOTO,SUM(a.SH01) SH01,SUM(a.ST01) ST01 "
                                    + " , SUM(a.SH19)SH19, SUM(a.ST19) ST19, SUM(a.SH09) SH09, SUM(a.ST09) ST09, SUM(a.SH06) SH06, SUM(a.ST06) ST06 "
                                    + " , SUM(a.SH10) SH10, SUM(a.ST10) ST10, SUM(a.SH_CHUA) SH_CHUA, SUM(a.SH_KHONG) SH_KHONG "
                                    + " from LUU_KHAOSATVAY a, DMPOS b where a.TRANGTHAI='A' and a.MAPOS = b.PO_MA and a.DOT = '" + CboDot.SelectionBoxItem + "' and a.MAPOS = '" + CboPos.SelectedValue + "' and a.NAM = '" + dtpNgay.SelectedDate.Value.ToString("yyyy") + "' "
                                    + " group by a.DOT, a.NAM, a.MAPOS, b.PO_TEN, a.MAXA, a.TENXA order by a.MAXA";
                    var dtin = cls.LoadDataText(strin);
                    if (dtin.Rows.Count > 0)
                    {
                        rpt_ksVay02 rpt = new rpt_ksVay02();
                        RPUtility.ShowRp(rpt, dtin, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                            srv.DbPassSerVer());
                    }
                    else MessageBox.Show("Không có dữ liệu !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
                else // mau 4
                {
                    mau = "4";
                    string strin = "select '"+mau+"' MAU,N'ngân hàng chinh sách xã hội' title1,N'chi nhánh tỉnh quảng bình' title2,a.DOT,a.NAM,a.MAPOS,b.PO_TEN,a.MAPOS MAXA,b.PO_TEN TENXA,COUNT(distinct MATHON) SOTHON,COUNT(distinct MATO) SOTO,SUM(a.SH01) SH01,SUM(a.ST01) ST01 "
                                    + " , SUM(a.SH19)SH19, SUM(a.ST19) ST19, SUM(a.SH09) SH09, SUM(a.ST09) ST09, SUM(a.SH06) SH06, SUM(a.ST06) ST06 "
                                    + " , SUM(a.SH10) SH10, SUM(a.ST10) ST10, SUM(a.SH_CHUA) SH_CHUA, SUM(a.SH_KHONG) SH_KHONG "
                                    + " from LUU_KHAOSATVAY a, DMPOS b where a.TRANGTHAI='A' and a.MAPOS = b.PO_MA and a.DOT = '" + CboDot.SelectionBoxItem + "' and a.NAM = '" + dtpNgay.SelectedDate.Value.ToString("yyyy") + "' "
                                    + " group by a.DOT, a.NAM, a.MAPOS, b.PO_TEN order by a.MAPOS";
                    var dtin = cls.LoadDataText(strin);
                    if (dtin.Rows.Count > 0)
                    {
                        rpt_ksVay02 rpt = new rpt_ksVay02();
                        RPUtility.ShowRp(rpt, dtin, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                            srv.DbPassSerVer());
                    }
                    else MessageBox.Show("Không có dữ liệu !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }

    }

}
