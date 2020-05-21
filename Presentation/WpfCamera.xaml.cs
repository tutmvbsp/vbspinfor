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
using System.Data;
using DAL;
using BLL;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDienbao.xaml
    /// </summary>
    public partial class WpfCamera : Window
    {
        public WpfCamera()
        {
            InitializeComponent();
        }

        private ClsServer _cls = new ClsServer();
        private ClsOracle ora = new ClsOracle();
        private ServerInfor srv = new ServerInfor();
        private ToolBll _str = new ToolBll();
        DataTable _dt = new DataTable();
        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtpNgay.SelectedDate = DateTime.Parse(DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month));
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            _cls.ClsConnect();
            //ora.ClsConnect();
            try
            {
                string nam = dtpNgay.SelectedDate.Value.ToString("yyyy");
                string thang = dtpNgay.SelectedDate.Value.ToString("MM");
                string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                #region
                /*            
                string strdel= "delete from DULIEU_NT where YEAR(NGAYBC)='"+dtpNgay.SelectedDate.Value.ToString("yyyy")+"' and MONTH(NGAYBC)='"+ dtpNgay.SelectedDate.Value.ToString("MM")+"'";
                //MessageBox.Show(strdel);
                _cls.UpdateDataText(strdel);
                string strsql = "select KHOA, THUTU, TT_HIENTHI, MA, TEN,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC, NAMBC, MAPGD, CO_TONGHOP, MACN, NGUOI_NHAP,to_char(NGAY_NHAP,'YYYY-MM-DD') NGAY_NHAP, NGUOI_DUYET, NGAY_DUYET, D1"
                              + ", D2, D3, D4, D5, D6, D7, D8, D9, D10, D11, D12, D13, D14, D15, D16, D17, D18, D19, D20, D21, D22, D23, D24, D25, D26, D27, D28, D29"
                              +", D30, NHAPTAY, FONTFORMAT, KIEUIN from dulieu_nt where khoa = 'GSCMR_001' and to_char(NGAYBC, 'MM/YYYY') = '"+ dtpNgay.SelectedDate.Value.ToString("MM/yyyy") + "' ";
                //MessageBox.Show(strsql);
                _dt = ora.LoadDataText(strsql);
                foreach (DataRow dr in _dt.Rows)
                {
                    string sqladd =
                        "insert into DULIEU_NT(KHOA,THUTU,TT_HIENTHI,MA,TEN,NGAYBC,NAMBC,MAPGD,CO_TONGHOP,MACN,NGUOI_NHAP,NGAY_NHAP,NGUOI_DUYET,NGAY_DUYET "
                        +
                        ", D1, D2, D3, D4, D5, D6, D7, D8, D9, D10, D11, D12, D13, D14, D15, D16, D17, D18, D19, D20, D21, D22, D23, D24, D25 "
                        + ", D26, D27, D28, D29, D30, NHAPTAY, FONTFORMAT, KIEUIN)" + " Values('" + dr["KHOA"]
                        + "','" + _str.Right(dr["MA"].ToString(),2) + "','" + dr["TT_HIENTHI"] + "','" + dr["MA"] +
                        "',N'" + dr["TEN"] + "','" + dr["NGAYBC"] + "','" + dr["NAMBC"] + "','" + dr["MAPGD"] + "','" +
                        dr["CO_TONGHOP"] +
                        "','" + dr["MACN"] + "','" + dr["NGUOI_NHAP"] + "','" + dr["NGAY_NHAP"] + "','" +
                        dr["NGUOI_DUYET"] + "','" + dr["NGAY_DUYET"] + "',N'" + dr["D1"] + "','" + dr["D2"] + "',N'" +
                        dr["D3"] + "',N'"
                        + dr["D4"] + "','" + dr["D5"] + "','" + dr["D6"] + "','" + dr["D7"] + "','" + dr["D8"] + "','" +
                        dr["D9"] + "','" + dr["D10"] + "','" + dr["D11"] + "','" + dr["D12"] + "','" + dr["D13"] + "','" +
                        dr["D14"]
                        + "','" + dr["D15"] + "','" + dr["D16"] + "','" + dr["D17"] + "','" + dr["D18"] + "','" +
                        dr["D19"] + "','" + dr["D20"] + "','" + dr["D21"] + "','" + dr["D22"] + "','" + dr["D23"] +
                        "','" + dr["D24"] + "','" + dr["D25"]
                        + "','" + dr["D26"] + "','" + dr["D27"] + "','" + dr["D28"] + "','" + dr["D29"] + "','" +
                        dr["D30"] + "','" + dr["NHAPTAY"] + "','" + dr["FONTFORMAT"] + "',0)";
                     _cls.UpdateDataText(sqladd);
                }
                string strin = "with CT2 as "
                     +" (select * from(select a.NGAYBC, a.D5 MAXA, b.CVI_DESC TENXA, a.THUTU, a.D3 from DULIEU_NT a, TXN_POINT_INFO b"
                      + " where YEAR(a.NGAYBC) = '" + dtpNgay.SelectedDate.Value.ToString("yyyy") + "' and MONTH(a.NGAYBC) = '" + dtpNgay.SelectedDate.Value.ToString("MM") + "'"
                      + " and a.D5 = b.CVI_TXN_POINT_ID"
                      +" ) p pivot(max(D3) for thutu in ([01], [02], [03], [04], [05], [61],[62], [07], [08], [09], [10]) ) d )"
                      +" , lst1 as"
                      +" ("
                     +" select SUBSTRING(a.MAXA, 5, 4) MAPOS,b.PO_TEN,a.NGAYBC,a.MAXA,a.TENXA,isnull(a.[01], '')[01]"
                     +" ,isnull(a.[02], '')[02],isnull(a.[03], '')[03],isnull(a.[04], '')[04]"
                     +" ,isnull(a.[05], '')[05],isnull(a.[61], '')[61],isnull(a.[62], '')[62]"
                     +" ,isnull(a.[07], '')[07],isnull(a.[08], '')[08],isnull(a.[09], '')[09]"
                     +" ,isnull(a.[10], '')[10],(select D4 from DULIEU_NT"
                      + " where YEAR(NGAYBC) = '" + dtpNgay.SelectedDate.Value.ToString("yyyy") + "' and MONTH(NGAYBC) = '" + dtpNgay.SelectedDate.Value.ToString("MM") + "' and D5 = a.MAXA and MA = 'CMR01') LYDO"
                         + " from CT2 a, DMPOS b where SUBSTRING(a.MAXA, 5, 4) = SUBSTRING(b.PO_MA, 3, 4)"
                      +" )"
                      +" select a.MAPOS,a.PO_TEN,a.NGAYBC,a.MAXA,a.TENXA,a.[01],a.[02],a.[03],a.[04],a.[05],a.[61],a.[62]"
                      + " ,a.[07],a.[08],a.[09],a.[10],(case when a.LYDO = '' then N'Đạt : '+ cast((CAST([01] AS int)+CAST([02] AS int)+CAST([03] AS int)"
                      + " +CAST([04] AS int)+CAST([05] AS int)+CAST([61] AS int)+CAST([62] AS int)+CAST([07] AS int)"
                      + " +CAST([08] AS int)+CAST([09] AS int)++CAST([10] AS int))*100 / 11 as nvarchar(10))+'%'  else a.LYDO end) LYDO from lst1 a order by a.MAXA";
                */
                #endregion

                //         string strin = " with CT2 as (select * from(select * from LUU_CAMERA where NAM = '"+nam+"' and thang = '"+thang+"') p pivot(max(D3) for thutu in ([1], [2], [3], [4], [5], [6],[7], [8], [9], [10],[11]) ) d ) , lst1 as"
                //               +" (select SUBSTRING(a.MAXA, 5, 4) MAPOS,b.PO_TEN,a.MAXA,a.TPI_DESC TENXA, sum(isnull(CAST([1] AS int), 0)) [01]"    
                //               +" ,sum(isnull(CAST([2] AS int), 0)) [02],sum(isnull(CAST([3] AS int), 0)) [03],sum(isnull(CAST([4] AS int), 0)) [04] "
                //               +" ,sum(isnull(CAST([5] AS int), 0)) [05],sum(isnull(CAST([6] AS int), 0)) [06],sum(isnull(CAST([7] AS int), 0)) [07] "
                //+" ,sum(isnull(CAST([8] AS int), 0)) [08],sum(isnull(CAST([9] AS int), 0)) [09],sum(isnull(CAST([10] AS int), 0)) [10] "
                //+" ,sum(isnull(CAST([11] AS int), 0))[11],a.D4 LYDO from CT2 a, DMPOS b where left(a.MAXA, 4) = right(b.PO_MA, 4) "
                //               +" group by SUBSTRING(a.MAXA, 5, 4),b.PO_TEN,a.MAXA,a.TPI_DESC,a.D4 ) select '"+ng+"' NGAYBC,* from lst1 order by MAXA";
                int thamso = 1;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Ngay";
                giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                var dtin = _cls.LoadDataProcPara("usp_Camera01", bien, giatri, thamso);
                rpt_Camera01 rpt = new rpt_Camera01();
                RPUtility.ShowRp(rpt, dtin, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _cls.DongKetNoi();
            ora.DongKetNoi();
        }
    }
}
