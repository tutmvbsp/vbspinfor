using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using DAL;
using BLL;
using System.Data;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDvut.xaml
    /// </summary>
    public partial class WpfKt740New : Window
    {
        public WpfKt740New()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ClsOracle ora = new ClsOracle();
        ToolBll str = new ToolBll();
        //ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        string Thumuc = "C:\\KT740";
        private string FileName = "";
        private string strstr = "";
        private string proc = "0";
        private string sql = "0";
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
            dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
            dtpDNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
            //string sql = "select PO_MA,PO_TEN from DMPOS where PO_MACN=" + "'" + BienBll.MainPos + "'" + " order by PO_MA";
            var sql = BienBll.NdCapbc.Trim() == "1" ? string.Format("select PO_MA,PO_TEN from DMPOS where PO_MA='{0}'", BienBll.NdMadv.Trim()) : "select PO_MA,PO_TEN from DMPOS";
            var dtpos = cls.LoadDataText(sql);
            for (int i = 0; i < dtpos.Rows.Count; i++)
            {
                CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
            }
            //CboPos.SelectedIndex = 1;
            //CboPos_SelectionChanged(null, null);
            //if (cls.KiemTraKetNoi() == false) cls.ClsConnect();
            string sqlMau = "select * from Kt740 order by TT";
            var dtMau = cls.LoadDataText(sqlMau);
            for (int i = 0; i < dtMau.Rows.Count; i++)
            {
                CboMau.Items.Add(dtMau.Rows[i][1] + " | " + dtMau.Rows[i][2]);
            }
            CboMau.SelectedIndex = 0;
            cls.DongKetNoi();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            str.TaoThuMuc(Thumuc);

            try
            {
                int thamso = 2;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@MaPos";
                giatri[0] = str.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                bien[1] = "@Ngay";
                giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                if (dtpNgay.SelectedDate != null)
                {
                    string nam = dtpNgay.SelectedDate.Value.ToString("yyyy");
                    string thang = dtpNgay.SelectedDate.Value.ToString("MM");
                    string pos = str.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    DateTime lastMonth = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month, DateTime.DaysInMonth(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month));
                    var lastDayOfTheYear = new DateTime(dtpNgay.SelectedDate.Value.Year, 12, 31);
                    string Enddayofyear = lastDayOfTheYear.ToString("yyyy-MM-dd");                    
                    string EnddayofyearPre = lastDayOfTheYear.AddYears(-1).ToString("yyyy-MM-dd");
                    string LastMonthPre= dtpNgay.SelectedDate.Value.AddMonths(-1).ToString("yyyy-MM-dd");
                    // DateTime LastWeek = dtpNgay.SelectedDate.Value.AddDays(-(int)dtpNgay.SelectedDate.Value.DayOfWeek-2);
                    if (dtpNgay.SelectedDate != null)
                    {
                        cls.ClsConnect();
                        ora.ClsConnect();
                        string mau = str.Left(CboMau.SelectedValue.ToString(), 3);
                        string ng = dtpNgay.SelectedDate.Value.ToString("dd/MMM/yyyy");
                        string ngsql = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                        string strsql="";
                        switch (mau)
                        {
                            case "M01":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") ==lastMonth.ToString("yyyy-MM-dd"))
                                {
                                    strsql = "select b.cs_mapgd,(select po_ten from dmpos where po_ma=b.cs_mapgd) TENPOS "
                                            +" ,substr(b.cs_madp, 1, 6) maxa,(select ten from dmxa where ma = substr(b.cs_madp, 1, 6)) tenxa "
                                            +" ,b.cs_mato,(select to_tentt from hsto where to_mato = b.cs_mato) tentt,b.cs_makh,kh_tenkh,b.cs_tentk "
                                            + " ,concat(chr(39),b.cs_so_tk2) TK, b.cs_sodu_tk,b.cs_ttso_tk,b.cs_sp_tk from "
                                            + " ( select cs_makh, count(cs_makh)dem from casa where cs_ngaybc ='"+ng+"' and cs_sp_tk = '105' and cs_ttso_tk = 'A' group by cs_makh having count(cs_makh) > 1) a, "
                                            + " (select * from casa where cs_ngaybc ='" + ng + "' and cs_sp_tk = '105' and cs_ttso_tk = 'A') b left join hskh on kh_makh = b.cs_makh "
                                            + " where a.cs_makh = b.cs_makh order by substr(b.cs_madp, 1, 6),b.cs_mato, b.cs_makh";
                                }
                                else
                                {
                                    strsql = "select b.cs_mapgd,(select po_ten from dmpos where po_ma=b.cs_mapgd) TENPOS "
                                            + " ,substr(b.cs_madp, 1, 6) maxa,(select ten from dmxa where ma = substr(b.cs_madp, 1, 6)) tenxa "
                                            + " ,b.cs_mato,(select to_tentt from hsto where to_mato = b.cs_mato) tentt,b.cs_makh,kh_tenkh,b.cs_tentk "
                                            + " ,concat(chr(39),b.cs_so_tk2) TK, b.cs_sodu_tk,b.cs_ttso_tk,b.cs_sp_tk from "
                                            + " ( select cs_makh, count(cs_makh)dem from casa_daily where cs_ngaybc ='" + dtpNgay.SelectedDate.Value.ToString("dd/MMM/yyyy") + "' and cs_sp_tk = '105' and cs_ttso_tk = 'A' group by cs_makh having count(cs_makh) > 1) a, "
                                            + " (select * from casa_daily where cs_ngaybc ='" + dtpNgay.SelectedDate.Value.ToString("dd/MMM/yyyy") + "' and cs_sp_tk = '105' and cs_ttso_tk = 'A') b left join hskh on kh_makh = b.cs_makh "
                                            + " where a.cs_makh = b.cs_makh order by substr(b.cs_madp, 1, 6),b.cs_mato, b.cs_makh";
                                }
                                break;
                            case "M02":
                                    strsql = "with lst1 as ("
                                              +" select kh_cmt, count(kh_cmt) dem from hskh where kh_ttrang <> 'C' and substr(kh_madp, 1, 4) = substr(kh_mapgd, 3, 4) group by kh_cmt having count(kh_cmt) > 1"
                                              +" ) select a.kh_mapgd,substr(a.kh_madp, 1, 6) MAXA,C.TEN TENXA , a.kh_makh,a.kh_tenkh,a.kh_cmt,a.kh_ttrang from hskh a, lst1 b,dmxa c where a.kh_cmt = b.kh_cmt and substr(a.kh_madp, 1, 6)= c.ma "
                                              +" order by a.kh_cmt";
                                break;
                            case "M03":
                                strsql = "select a.ku_mapgd,substr(a.ku_madp,1,6) maxa,a.ku_makh,a.ku_mato,b.cs_mato from "
                                        +" (select * from hscv_daily where ku_ngaybc ='"+ng+"' and ku_ttmonvay <> 'CLOSE' and ku_dnothan + ku_dnoqhan + ku_dnokhoanh > 0 and ku_mato is not null) a, "
                                        + " (select * from casa_daily where cs_ngaybc ='" + ng + "' and cs_ttso_tk <> 'C' and cs_sp_tk = '105' and cs_mato is not null) b "
                                        + " where a.ku_makh = b.cs_makh and a.ku_mato<> b.cs_mato";
                                break;
                            case "M04":
                                strsql = "select  a.ku_mapgd,substr(a.ku_madp,1,6) maxa,a.ku_makh,a.ku_mato mato1,b.ku_mato mato2 from"
                                        + "(select * from hscv_daily where ku_ngaybc ='" + ng + "' and ku_ttmonvay <> 'CLOSE' and ku_dnothan + ku_dnoqhan + ku_dnokhoanh > 0) a,"
                                        + " (select ku_makh, ku_mato from hscv_daily where ku_ngaybc='" + ng + "' and ku_ttmonvay <> 'CLOSE' and ku_dnothan+ku_dnoqhan + ku_dnokhoanh > 0 ) b"
                                        + " where a.ku_makh = b.ku_makh and a.ku_mato<> b.ku_mato";
                                break;
                            case "M06":
                                if (pos=="003000")
                                strsql = "with lst1 as ( select a.ku_mapgd, substr(a.ku_madp, 1, 6) maxa, a.ku_mato, count(a.ku_makh) dem from hscv_daily a where a.ku_ngaybc = '"+ng+"' and a.ku_ttmonvay <> 'CLOSE' and a.ku_dnothan + a.ku_dnoqhan + a.ku_dnokhoanh > 0 and a.ku_mato is not null and a.ku_hthucvay = '3' group by a.ku_mapgd, a.ku_madp, a.ku_mato having count(a.ku_makh) <= 5 or count(a.ku_makh) > 60 ) select a.ku_mapgd,b.po_ten,a.maxa,c.ten tenxa, a.ku_mato,d.to_tentt,a.dem sotv from lst1 a left join dmpos b on a.ku_mapgd = b.po_ma left join dmxa c on a.maxa = c.ma left join hsto d on a.ku_mato = d.to_mato order by a.maxa,a.ku_mato";
                                else
                                    strsql = "with lst1 as ( select a.ku_mapgd, substr(a.ku_madp, 1, 6) maxa, a.ku_mato, count(a.ku_makh) dem from hscv_daily a where a.ku_ngaybc = '" + ng + "' and a.ku_mapgd='"+pos+"' and a.ku_ttmonvay <> 'CLOSE' and a.ku_dnothan + a.ku_dnoqhan + a.ku_dnokhoanh > 0 and a.ku_mato is not null and a.ku_hthucvay = '3' group by a.ku_mapgd, a.ku_madp, a.ku_mato having count(a.ku_makh) <= 5 or count(a.ku_makh) > 60 ) select a.ku_mapgd,b.po_ten,a.maxa,c.ten tenxa, a.ku_mato,d.to_tentt,a.dem sotv from lst1 a left join dmpos b on a.ku_mapgd = b.po_ma left join dmxa c on a.maxa = c.ma left join hsto d on a.ku_mato = d.to_mato order by a.maxa,a.ku_mato";
                                break;
                            case "M07":
                                if (pos == "003000")
                                    strsql = "select c.ku_mapgd,substr(c.ku_madp,1,6) maxa,f.ten  tenxa,c.ku_mato,g.to_tentt,e.kh_makh,e.kh_tenkh,c.ku_chtrinh,c.ku_maqd, d.ku_chtrinh chtr,d.ku_maqd MAQD from (select a.* from hscv_daily a where a.KU_NGAYBC = '"+ng+"' and a.KU_TTMONVAY <> 'CLOSE' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0) c left join hskh e on c.ku_makh = e.kh_makh left join dmxa f on substr(c.ku_madp, 1, 6)= f.ma left join hsto g on substr(c.ku_mato, 1, 6)= g.to_mato ,(select b.* from hscv_daily b where b.KU_NGAYBC = '"+ng+"' and b.KU_TTMONVAY <> 'CLOSE' and b.KU_DNOTHAN + b.KU_DNOQHAN + b.KU_DNOKHOANH > 0) d where c.KU_MAKH = d.KU_MAKH and((c.KU_CHTRINH = '01' and d.KU_CHTRINH in ('09', '10', '15', '19')) or(c.KU_CHTRINH = '09' and d.KU_CHTRINH in ('10', '15', '19')) or(c.KU_CHTRINH = '19' and d.KU_CHTRINH in ('10', '15'))) order by c.ku_madp,c.ku_mato";
                                else
                                    strsql = "select c.ku_mapgd,substr(c.ku_madp,1,6) maxa,f.ten  tenxa,c.ku_mato,g.to_tentt,e.kh_makh,e.kh_tenkh,c.ku_chtrinh,c.ku_maqd, d.ku_chtrinh chtr,d.ku_maqd MAQD from (select a.* from hscv_daily a where a.ku_mapgd='"+pos+"' and a.KU_NGAYBC = '" + ng + "' and a.KU_TTMONVAY <> 'CLOSE' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0) c left join hskh e on c.ku_makh = e.kh_makh left join dmxa f on substr(c.ku_madp, 1, 6)= f.ma left join hsto g on substr(c.ku_mato, 1, 6)= g.to_mato ,(select b.* from hscv_daily b where b.ku_mapgd='" + pos + "' and b.KU_NGAYBC = '" + ng+"' and b.KU_TTMONVAY <> 'CLOSE' and b.KU_DNOTHAN + b.KU_DNOQHAN + b.KU_DNOKHOANH > 0) d where c.KU_MAKH = d.KU_MAKH and((c.KU_CHTRINH = '01' and d.KU_CHTRINH in ('09', '10', '15', '19')) or(c.KU_CHTRINH = '09' and d.KU_CHTRINH in ('10', '15', '19')) or(c.KU_CHTRINH = '19' and d.KU_CHTRINH in ('10', '15'))) order by c.ku_madp,c.ku_mato";
                                break;

                            case "M08":
                                if (pos == "003000")
                                    strsql = "select c.KH_MAPGD POS,substr(c.KH_MADP,1,6) MAXA,(select TEN from DMXA where MA=substr(c.KH_MADP,1,6)) TENXA ,b.CS_MATO,(select TO_TENTT from HSTO where TO_MATO = b.CS_MATO)TENTT, c.KH_MAKH,c.KH_TENKH,a.DUNO,b.CS_MAKH,b.CS_SODU_TK from ( select a.KU_MAKH, sum(a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH) DUNO from HSCV_DAILY a where a.KU_NGAYBC = '"+ng+"'  group by a.KU_MAKH having sum(a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH) = 0 ) a , ( select a.CS_MATO,a.CS_MAKH,a.CS_SODU_TK from CASA_DAILY a where a.CS_NGAYBC = '"+ng+"' and a.CS_SP_TK = '105' and a.CS_TTSO_TK <> 'C' and a.CS_MATO is not null ) b,HSKH c where a.KU_MAKH = b.CS_MAKH and a.KU_MAKH = c.KH_MAKH order by c.KH_MADP,b.CS_MATO";
                                else
                                    strsql = "select c.KH_MAPGD POS,substr(c.KH_MADP,1,6) MAXA,(select TEN from DMXA where MA=substr(c.KH_MADP,1,6)) TENXA ,b.CS_MATO,(select TO_TENTT from HSTO where TO_MATO = b.CS_MATO)TENTT, c.KH_MAKH,c.KH_TENKH,a.DUNO,b.CS_MAKH,b.CS_SODU_TK from ( select a.KU_MAKH, sum(a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH) DUNO from HSCV_DAILY a where a.KU_NGAYBC = '" + ng + "' and a.KU_MAPGD = '" + pos + "' group by a.KU_MAKH having sum(a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH) = 0 ) a , ( select a.CS_MATO,a.CS_MAKH,a.CS_SODU_TK from CASA_DAILY a where a.CS_NGAYBC = '" + ng + "' and a.CS_MAPGD = '" + pos + "' and a.CS_SP_TK = '105' and a.CS_TTSO_TK <> 'C' and a.CS_MATO is not null ) b,HSKH c where a.KU_MAKH = b.CS_MAKH and a.KU_MAKH = c.KH_MAKH order by c.KH_MADP,b.CS_MATO";
                                break;
                            case "M09":
                                if (pos == "003000")
                                    strsql = "select a.ku_mapgd,c.po_ten,a.MAXA,d.ten tenxa,a.KU_MATO,e.to_tentt,b.KH_MAKH,b.KH_TENKH,concat(chr(39),a.KU_SOKU) SOKU,a.DUNO,a.KU_CAPQLV from ( select substr(a.KU_MADP, 1, 6) MAXA, a.ku_mapgd, a.ku_makh, a.KU_MATO, a.ku_soku, a.KU_CAPQLV, a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO from HSCV_DAILY a where a.KU_NGAYBC = '"+ng+"' and a.KU_TTMONVAY <> 'CLOSE' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 and a.KU_CHTRINH = '03' and a.KU_CAPQLV is null ) a left join dmpos c on a.ku_mapgd = c.po_ma left join dmxa d on a.maxa = d.ma left join hsto e on a.ku_mato = e.to_mato ,HSKH b where a.KU_MAKH = b.KH_MAKH order by a.KU_MAPGD,a.MAXA,a.KU_MATO";
                                else
                                    strsql = "select a.ku_mapgd,c.po_ten,a.MAXA,d.ten tenxa,a.KU_MATO,e.to_tentt,b.KH_MAKH,b.KH_TENKH,concat(chr(39),a.KU_SOKU) SOKU,a.DUNO,a.KU_CAPQLV from ( select substr(a.KU_MADP, 1, 6) MAXA, a.ku_mapgd, a.ku_makh, a.KU_MATO, a.ku_soku, a.KU_CAPQLV, a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO from HSCV_DAILY a where a.ku_mapgd='"+pos+"' and a.KU_NGAYBC = '" + ng + "' and a.KU_TTMONVAY <> 'CLOSE' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 and a.KU_CHTRINH = '03' and a.KU_CAPQLV is null ) a left join dmpos c on a.ku_mapgd = c.po_ma left join dmxa d on a.maxa = d.ma left join hsto e on a.ku_mato = e.to_mato ,HSKH b where a.KU_MAKH = b.KH_MAKH order by a.KU_MAPGD,a.MAXA,a.KU_MATO";
                                break;
                            case "M10":
                                dt = cls.LoadDataProcPara("usp_KT74010", bien, giatri, thamso);
                                proc = "1";
                                break;
                            case "M11":
                                dt = cls.LoadDataProcPara("usp_KT74023", bien, giatri, thamso);
                                proc = "1";
                                break;

                            case "M12":
                                strsql = "select  cs_mapgd,substr(cs_madp,1,6) maxa,ten TENXA,cs_makh,kh_tenkh,cs_mato,concat(chr(39),cs_so_tk2) sotk,"
                                        + " cs_sodu_tk ,cs_ttso_tk ,to_date(cs_ngayroito, 'DD-MM-YYYY') NG_ROITO,"
                                        +" N'KH đã rời tổ quá 90 ngày,đề nghị rà soát theo VB3497/NHCS-TDNN' GhiChu"
                                        +" from casa_daily, dmxa, hskh where cs_ngaybc = '"+ng+ "' and to_date('" + ng + "') - cs_ngayroito > 90"
                                        + " and cs_sodu_tk >= 0 and cs_sodu_tk<= 100000 and NVL(cs_ttso_tk, 'A') <> 'C' and cs_mato is null"
                                        +" and substr(cs_madp,1, 6)= ma and cs_makh = kh_makh order by substr(cs_madp, 1, 6),cs_makh";
                                break;
                            case "M13":
                                strsql = "with lst1 as ( select ku_makh, ku_mato, sum(NVL(ku_dnothan, 0) + NVL(ku_dnoqhan, 0) + NVL(ku_dnokhoanh, 0)) duno "
                                        + " from hscv_daily where ku_ngaybc ='" + ng + "' and ku_ttmonvay <> 'CLOSE' and NVL(ku_dnothan, 0) + NVL(ku_dnoqhan, 0) + NVL(ku_dnokhoanh, 0) > 0 "
                                        + " group by ku_makh, ku_mato ) select ROW_NUMBER() OVER(PARTITION BY a.kh_mapgd ORDER BY a.kh_mapgd) STT, a.kh_mapgd, a.kh_madp, b.ku_mato, c.to_tentt, a.kh_makh, a.kh_tenkh, a.kh_diachi, b.duno, a.kh_ngaycap "
                                        + " , round((TO_DATE('" + ng + "') -TO_DATE(a.kh_ngaycap))/ 365,0) sonam "
                                        + " from hskh a, lst1 b left join hsto c on b.ku_mato = c.to_mato "
                                        + " where a.kh_makh = b.ku_makh and TO_DATE ('" + ng + "') -TO_DATE(a.kh_ngaycap) > 15 * 365 "
                                        + " order by a.kh_mapgd,a.kh_madp,b.ku_mato,a.kh_makh";
                                break;
                            case "M14":
                                strsql = "select ROW_NUMBER() OVER(PARTITION BY a.ku_mapgd ORDER BY a.ku_mapgd) STT,a.ku_mapgd,a.ku_madp,C.TEN TENXA"
                                        +" ,a.ku_mato,d.to_tentt ,a.ku_makh,b.kh_tenkh,concat(chr(39), a.ku_soku) SOKU,a.ku_ngayvay,a.ku_dnothan,a.ku_dnoqhan,a.ku_dnokhoanh "
                                        + " ,round((TO_DATE('" + ng + "') - TO_DATE(a.ku_ngayvay)) / 30, 0) sothang,a.ku_ttmonvay,a.ku_maqd,e.giatri from hscv_daily a"
                                        + " left join hskh b on a.ku_makh = b.kh_makh left join dmxa c on substr(a.ku_madp, 1, 6)= c.ma "
                                        + " left join hsto d on a.ku_mato = d.to_mato left join (select * from dmkhac where khoa_1='07') e on A.KU_MAQD=e.khoa_2" 
                                        + " where a.ku_ngaybc ='" + ng + "' and a.ku_ttmonvay <> 'CLOSE' and a.ku_gngan = 0"
                                        + " and TO_DATE ('" + ng + "') -TO_DATE(a.ku_ngayvay) > 90 order by a.ku_mapgd,a.ku_madp,a.ku_mato,a.ku_makh";
                                break;

                            case "M15":
                                strsql = "with lst1 as ("
                                        +" select a.KU_MAPGD, KU_MADP, d.TEN TENXA, KU_MATO, c.TO_TENTT, b.KH_MAKH, b.KH_TENKH, concat(chr(39), a.KU_SOKU) SOKU, a.KU_NGAYVAY, a.KU_DNOTHAN, a.KU_DNOQHAN, a.KU_DNOKHOANH, a.KU_MANDT, a.KU_CAPQLV, a.KU_NGUONVON, a.KU_HTHUCVAY, a.PL_NGUONVON_BS "
                                        +", (case when a.KU_NGUONVON = '1' and a.PL_NGUONVON_BS = '01' and a.KU_MANDT <> 'INV0107190050391' then 'F'"
                                        + " when a.KU_NGUONVON = '1' and a.PL_NGUONVON_BS = '02' and (a.KU_MANDT is not null or a.KU_MANDT<>'') then 'F'"
                                        + " when a.KU_NGUONVON = '1' and a.PL_NGUONVON_BS = '01' and (a.KU_MANDT is null or a.KU_MANDT='') then 'F'"
                                        + " when a.KU_NGUONVON = '2' and (a.KU_MANDT = 'INV0107190050391' or a.KU_MANDT is null)  then 'F'"
                                        +" else 'T'end) TT from HSCV_DAILY a left join hskh b on a.KU_MAKH = b.KH_MAKH"
                                        +" left join hsto c on a.KU_MATO = c.TO_MATO left join DMXA d on substr(a.KU_MADP, 1, 6)= d.MA"
                                        +" where a.KU_NGAYBC = '"+ng+"' and a.KU_CHTRINH = '03' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 and a.KU_TTMONVAY <> 'CLOSE'"
                                        +" ) select* from lst1 where tt = 'F' order by KU_MADP,KU_MATO";
                                break;
                            case "M16":
                                strsql = "with lst1 as ("
                                +" select ROW_NUMBER() OVER(PARTITION BY a.KU_MAKH ORDER BY a.KU_MADP, a.ku_makh) STT, a.ku_mapgd, substr(a.ku_madp, 1, 6) MAXA, concat(chr(39), "+" a.ku_soku) soku , a.ku_mato, a.ku_makh, a.ku_dnothan, a.ku_dnoqhan, a.ku_dnokhoanh, a.ku_laitonthan + a.ku_laitonqhan LAITON "
                                 +" from hscs_daily a where a.ku_ngaybc = '"+ng+"' and ku_ttmonvay <> 'CLOSE' and a.ku_laitonthan + a.ku_laitonqhan > 100000 "
                                +" ),lst2 as ( select a.*,c.du from lst1 a"
                                    +" left join (select b.cs_makh, sum(b.cs_sodu_tk) du from casa_daily b where b.cs_ngaybc = '"+ng+"' and cs_sp_tk = '105' group by b.cs_makh "+" having sum(b.cs_sodu_tk) >= 100000) c on a.ku_makh = c.cs_makh and a.stt = 1 ), lst3 as "
                                +" ( select a.* from lst2 a where a.du > 0 )"
                                +" select a.stt,a.ku_mapgd,d.po_ten,a.maxa,e.ten tenxa, a.ku_mato,f.to_tentt,a.ku_makh,g.kh_tenkh,a.soku,"
                                +" a.ku_dnothan,a.ku_dnoqhan,a.ku_dnokhoanh,a.LAITON,a.du DUTK105 from lst2 a left "
                                                               +" join dmpos d on a.ku_mapgd = d.po_ma "
                                                          +" left join dmxa e on a.maxa = e.ma "
                                                          +" left join hsto f on a.ku_mato = f.to_mato "
                                                          +" left join hskh g on a.ku_makh = g.kh_makh "
                                +" where a.ku_makh in (select b.ku_makh from lst3 b where a.ku_makh = b.ku_makh) order by a.maxa,a.ku_mato,a.ku_makh,a.stt";
                                break;
                            case "M17":
                                strsql = "with lst1 as ( select a.KU_MAPGD, substr(a.KU_MADP, 1, 6) MAXA, a.KU_MADP, a.KU_MATO, a.KU_MAKH, a.KU_SOKU, a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO from HSCV_DAILY a where a.ku_ngaybc = '"+ng+"' and a.KU_MATO is not null ), lst2 as ( select a.CS_MAPGD,substr(a.CS_MADP, 1, 6) MAXA,a.CS_MADP,a.CS_MATO,a.cs_MAKH,a.CS_SO_TK,a.CS_SODU_TK DUTK from CASA_DAILY a where a.cs_ngaybc = '"+ng+"' and a.CS_SP_TK = '105' and a.CS_MATO is not null ), lst3 as ( select a.KU_MAPGD,a.MAXA,a.KU_MATO,sum(a.DUNO) DUNO from lst1 a group by a.KU_MAPGD,a.MAXA,a.KU_MATO having sum(a.DUNO) = 0 ), lst4 as ( select a.CS_MAPGD,a.MAXA,a.CS_MATO,sum(a.DUTK) DUTK from lst2 a group by a.CS_MAPGD,a.MAXA,a.CS_MATO having sum(a.DUTK) = 0 ) select a.*,b.CS_MATO,b.DUTK from lst3 a, lst4 b where a.KU_MATO = b.CS_MATO";
                                break;
                            case "M18":
                                strsql = "with lst1 as ( select a.KU_MAPGD, substr(a.KU_MADP, 1, 6) MAXA, a.KU_MADP, a.KU_MATO, a.KU_MAKH, a.KU_SOKU, a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO from HSCV_DAILY a where a.ku_ngaybc = '" + ng + "' and a.KU_MATO is not null ), lst2 as ( select a.CS_MAPGD,substr(a.CS_MADP, 1, 6) MAXA,a.CS_MADP,a.CS_MATO,a.cs_MAKH,a.CS_SO_TK,a.CS_SODU_TK DUTK from CASA_DAILY a where a.cs_ngaybc = '" + ng + "' and a.CS_SP_TK = '105' and a.CS_MATO is not null ), lst3 as ( select a.KU_MAPGD,a.MAXA,a.KU_MATO,sum(a.DUNO) DUNO from lst1 a group by a.KU_MAPGD,a.MAXA,a.KU_MATO having sum(a.DUNO) = 0 ), lst4 as ( select a.CS_MAPGD,a.MAXA,a.CS_MATO,sum(a.DUTK) DUTK from lst2 a group by a.CS_MAPGD,a.MAXA,a.CS_MATO having sum(a.DUTK) > 0 ) select a.*,b.CS_MATO,b.DUTK from lst3 a, lst4 b where a.KU_MATO = b.CS_MATO";
                                break;
                            case "M19":
                                strsql = "with lst1 as ( select a.KU_MAPGD, substr(a.KU_MADP, 1, 6) MAXA, a.KU_MADP, a.KU_MATO, a.KU_MAKH, a.KU_SOKU, a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO from HSCV_DAILY a where a.ku_ngaybc = '" + ng + "' and a.KU_MATO is not null ), lst2 as ( select a.CS_MAPGD,substr(a.CS_MADP, 1, 6) MAXA,a.CS_MADP,a.CS_MATO,a.cs_MAKH,a.CS_SO_TK,a.CS_SODU_TK DUTK from CASA_DAILY a where a.cs_ngaybc = '" + ng + "' and a.CS_SP_TK = '105' and a.CS_MATO is not null ), lst3 as ( select a.KU_MAPGD,a.MAXA,a.KU_MATO,sum(a.DUNO) DUNO from lst1 a group by a.KU_MAPGD,a.MAXA,a.KU_MATO having sum(a.DUNO) > 0 ), lst4 as ( select a.CS_MAPGD,a.MAXA,a.CS_MATO,sum(a.DUTK) DUTK from lst2 a group by a.CS_MAPGD,a.MAXA,a.CS_MATO having sum(a.DUTK) = 0 ) select a.*,b.CS_MATO,b.DUTK from lst3 a, lst4 b where a.KU_MATO = b.CS_MATO";
                                break;
                            case "M20":
                                strsql = "with lst1 as ( select a.KU_MAPGD, substr(a.KU_MADP, 1, 6) MAXA, a.KU_MADP, a.KU_MATO, a.KU_MAKH, a.KU_SOKU, a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO from HSCV_DAILY a where a.ku_ngaybc = '"+ng+"' and a.KU_MATO is not null and a.ku_maqd <> '26' ), lst2 as ( select a.CS_MAPGD,substr(a.CS_MADP, 1, 6) MAXA,a.CS_MADP,a.CS_MATO,a.cs_MAKH,a.CS_SO_TK,a.CS_SODU_TK DUTK from CASA_DAILY a where a.cs_ngaybc = '"+ng+"' and a.CS_SP_TK = '105' and a.CS_MATO is not null), lst3 as ( select a.KU_MAPGD,a.MAXA,a.KU_MATO,a.KU_MAKH,sum(a.DUNO) DUNO from lst1 a group by a.KU_MAPGD,a.MAXA,a.KU_MATO,a.ku_makh ), lst4 as ( select a.CS_MAPGD,a.MAXA,a.CS_MATO,a.CS_MAKH,sum(a.DUTK) DUTK from lst2 a group by a.CS_MAPGD,a.MAXA,a.CS_MATO,a.cs_makh ) select a.ku_mapgd,c.po_ten,a.maxa,d.ten tenxa, a.ku_mato,e.to_tentt,a.ku_makh,f.KH_TENKH,a.duno,a.duno / 10 DU_PT,b.DUTK from lst3 a left join dmpos c on a.ku_mapgd = c.po_ma left join dmxa d on a.maxa = d.ma left join hsto e on a.ku_mato = e.to_mato left join hskh f on a.ku_makh = f.kh_makh , lst4 b  where a.KU_MATO = b.CS_MATO and a.ku_makh = b.cs_makh and b.dutk > a.duno / 10 order by a.maxa,a.ku_mato,a.ku_makh";
                                break;
                            case "M21":
                                strsql = "with lst1 as ( select a.KU_MAPGD, substr(a.KU_MADP, 1, 6) MAXA, a.KU_MADP, a.KU_MATO, a.KU_MAKH, a.KU_SOKU, a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO from HSCV_DAILY a where a.ku_ngaybc = '"+ng+"' and a.KU_MATO is not null and a.ku_maqd <> '26' ), lst2 as ( select a.CS_MAPGD,substr(a.CS_MADP, 1, 6) MAXA,a.CS_MADP,a.CS_MATO,a.cs_MAKH,a.CS_SO_TK,a.CS_SODU_TK DUTK from CASA_DAILY a where a.cs_ngaybc = '"+ng+"' and a.CS_SP_TK = '105' and a.CS_MATO is not null ), lst3 as ( select a.KU_MAPGD,a.MAXA,a.KU_MATO,a.KU_MAKH,sum(a.DUNO) DUNO from lst1 a group by a.KU_MAPGD,a.MAXA,a.KU_MATO,a.ku_makh having sum(a.DUNO) > 0 ), lst4 as ( select a.CS_MAPGD,a.MAXA,a.CS_MATO,a.CS_MAKH,sum(a.DUTK) DUTK from lst2 a group by a.CS_MAPGD,a.MAXA,a.CS_MATO,a.cs_makh having sum(a.DUTK) = 0 ) select a.ku_mapgd,c.po_ten,a.maxa,d.ten tenxa, a.ku_mato,e.to_tentt,a.ku_makh,f.KH_TENKH,a.duno,b.DUTK from lst3 a left join dmpos c on a.ku_mapgd = c.po_ma left join dmxa d on a.maxa = d.ma left join hsto e on a.ku_mato = e.to_mato left join hskh f on a.ku_makh = f.kh_makh , lst4 b  where a.KU_MATO = b.CS_MATO and a.ku_makh = b.cs_makh order by a.maxa,a.ku_mato,a.ku_makh";
                                break;
                            case "M22":
                                dt = cls.LoadDataProcPara("usp_KT74004", bien, giatri, thamso);
                                proc = "1";
                                break;
                            case "M23":
                                dt = cls.LoadDataProcPara("usp_KT74005", bien, giatri, thamso);
                                proc = "1";
                                break;
                            case "M24":
                                dt = cls.LoadDataProcPara("usp_KT74025", bien, giatri, thamso);
                                proc = "1";
                                break;
                            case "M25":
                                dt = cls.LoadDataProcPara("usp_KT74018", bien, giatri, thamso);
                                proc = "1";
                                break;
                            case "M26":
                                dt = cls.LoadDataProcPara("usp_KT74019", bien, giatri, thamso);
                                proc = "1";
                                break;
                            case "M27":
                                dt = cls.LoadDataProcPara("usp_KT74023", bien, giatri, thamso);
                                proc = "1";
                                break;
                            case "M28":
                                dt = cls.LoadDataProcPara("usp_KT74024", bien, giatri, thamso);
                                proc = "1";
                                break;
                            case "M29":
                                dt = cls.LoadDataProcPara("usp_TangPhien", bien, giatri, thamso);
                                proc = "1";
                                break;
                            case "M30":
                                dt = cls.LoadDataProcPara("usp_kt74027", bien, giatri, thamso);
                                proc = "1";
                                break;
                            case "M31":
                                dt = cls.LoadDataProcPara("usp_kt74028", bien, giatri, thamso);
                                proc = "1";
                                break;
                            case "M32":
                                strsql = "select a.*, b.tong_duno, dnth, dnqh, dnkh from (SELECT substr(cs_madp, 1, 6) maxa, ten tenxa,cs_mato mato,to_tentt tentt,cs_makh makh,cs_tentk tenkh,sum(CS_SODU_TK) du_tk,cs_ngaybc ngaybc FROM casa_daily, hsto, dmxa WHERE cs_mapgd = '"+pos+"' AND CS_SP_TK = '105' AND cs_ngaybc = '"+ng+"' and cs_mato = to_mato and to_loaito = '01' and CS_TTSO_TK = 'A' and substr(cs_madp, 1, 6) = ma group by substr(cs_madp, 1, 6), ten, cs_mato, to_tentt, cs_makh, cs_tentk, cs_ngaybc having sum(CS_SODU_TK) >= 5000000) a left join (select SUBSTR(KU_MADP, 1, 6) MAXA, KU_MATO mato, to_tentt tentt, ku_makh makh, SUM(KU_DNOTHAN) + SUM(KU_DNOQHAN) + SUM(KU_DNOKHOANH) TONG_DUNO, SUM(KU_DNOTHAN) DNTH, SUM(KU_DNOQHAN) DNQH,SUM(KU_DNOKHOANH) DNKH FROM HSCV_DAILY, hsto WHERE KU_NGAYBC = '"+ng+"' AND KU_MApgd = '"+pos+"' and ku_mato = to_mato AND KU_TTMONVAY <> 'CLOSE' and trangthai = 'A' GROUP BY SUBSTR(KU_MADP, 1, 6), KU_MATO, to_tentt, ku_makh) B on a.makh = b.makh order by a.maxa, a.mato, a.makh";
                                break;
                            case "M33":
                                strsql = "select a.KU_MAPGD,LEFT(a.KU_MADP,6) MAXA,b.TEN TENXA,a.KU_MATO,c.TO_TENTT,a.KU_MAKH,d.KH_TENKH,char(39)+a.KU_SOKU SOKU,a.KU_NGAYGNDT NG_VAY,a.KU_DNOTHAN,a.KU_DNOQHAN,a.KU_DNOKHOANH,a.KU_MAQD,e.GIATRI CHTRINH from HSCV_DAILY a left join DMXA b on b.MA = LEFT(a.KU_MADP, 6) and b.TRANGTHAI = 'A' left join HSTO c on c.TO_MATO = a.KU_MATO and c.TRANGTHAI = 'A' left join hskh d on d.KH_MAKH = a.KU_MAKH left join (select * from DMKHAC where KHOA_1 = '07') e on a.KU_MAQD = e.KHOA_2 where a.KU_NGAYBC = '"+ngsql+"' and a.KU_MAPGD = '"+pos+"'  and a.KU_TTMONVAY <> 'CLOSE' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 and a.KU_SOKU in (select SOKU from MAU06 where SOKU = A.KU_SOKU and MAPOS = '"+pos+"' and MAPOS = a.KU_MAPGD and TRANGTHAI1 = 1 and NGAY_VAY >= '2018-12-31') order by a.KU_MADP,a.KU_MATO,a.KU_MAKH";
                                sql = "1";
                                break;

                        }
                        //MessageBox.Show(mau);

                        if (proc=="0")
                            if (sql=="1") dt = cls.LoadDataText(strsql);
                            else dt = ora.LoadDataText(strsql);
                        if (dt.Rows.Count > 0)
                        {
                            FileName = Thumuc + "\\" + pos + "_" + CboMau.SelectedValue.ToString().Substring(5, CboMau.SelectedValue.ToString().Length - 5) + "_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                            if (mau == "M12" || mau == "M20" || mau == "M21")
                            {
                                FileStream fs = new FileStream(FileName, FileMode.Create);
                                StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
                                str.ToCSV(dt, sw, true);
                            }
                            else
                                str.ExportToExcel(dt, FileName);
                            MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK,MessageBoxImage.Information);
                            str.OpenExcel(FileName);
                        }
                        else
                        {
                            MessageBox.Show("Không có số liệu", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
            
        }

        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

   
    }
}
