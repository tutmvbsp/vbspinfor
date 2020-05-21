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
    public partial class WpfTienIch : Window
    {
        public WpfTienIch()
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
        private string FileName1 = "";
        private string FileName2 = "";
        private string strstr1 = "";
        private string strstr2 = "";

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
            string sqlMau = "select * from TIENICH order by TT";
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
                if (dtpNgay.SelectedDate != null)
                {
                    string den_ng = dtpDNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    string ngora = dtpNgay.SelectedDate.Value.ToString("dd/MMM/yyyy");
                    string den_ngora = dtpDNgay.SelectedDate.Value.ToString("dd/MMM/yyyy");
                    string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    string nam = dtpNgay.SelectedDate.Value.ToString("yyyy");
                    string thang = dtpNgay.SelectedDate.Value.ToString("MM");
                    string pos = str.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    string thg = textBox.Text;
                    bien[0] = "@MaPos";
                    giatri[0] = pos;
                    bien[1] = "@Ngay";
                    giatri[1] = ng;
                    DateTime lastMonth = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month, DateTime.DaysInMonth(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month));
                    var lastDayOfTheYear = new DateTime(dtpNgay.SelectedDate.Value.Year, 12, 31);
                    string Enddayofyear = lastDayOfTheYear.ToString("yyyy-MM-dd");                    
                    string EnddayofyearPre = lastDayOfTheYear.AddYears(-1).ToString("yyyy-MM-dd");
                    string LastMonthPre= dtpNgay.SelectedDate.Value.AddMonths(-1).ToString("yyyy-MM-dd");
                    string enddayoffirstmonth = lastDayOfTheYear.AddYears(-1).AddMonths(1).ToString("yyyy-MM-dd");
                    
                    // DateTime LastWeek = dtpNgay.SelectedDate.Value.AddDays(-(int)dtpNgay.SelectedDate.Value.DayOfWeek-2);
                    if (dtpNgay.SelectedDate != null)
                    {
                        cls.ClsConnect();
                        ora.ClsConnect();
                        string mau = str.Left(CboMau.SelectedValue.ToString(), 3);
                        string strsql;
                        string FileName2;
                        string FileName1;
                        switch (mau)
                        {
                            case "M01":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") ==
                                    lastMonth.ToString("yyyy-MM-dd"))
                                {
                                    strsql = "select a.TO_MAPGD,a.TO_MADP,a.TO_MATO,a.TO_MATT,a.TO_TENTT from (select a.TO_MATT CS_MAKH,a.* from HSTO a where a.TO_MAPGD='" + pos +
                                             "' and a.TRANGTHAI='A') a where a.CS_MAKH not in (select b.CS_MAKH from casa b where left(b.CS_NGAYBC,10)='"
                                             + ng + "' and b.CS_MAPGD='" + pos +
                                             "' and b.CS_SP_TK='105' and a.CS_MAKH =b.CS_MAKH ) order by a.TO_MADP";
                                }
                                else
                                {
                                    strsql = "select a.TO_MAPGD,a.TO_MADP,a.TO_MATO,a.TO_MATT,a.TO_TENTT from (select a.TO_MATT CS_MAKH,a.* from HSTO a where a.TO_MAPGD='" + pos +
                                             "' and a.TRANGTHAI='A') a where a.CS_MAKH not in (select b.CS_MAKH from CASA_DAILY b where left(b.CS_NGAYBC,10)='"
                                             + ng + "' and b.CS_MAPGD='" + pos +
                                             "' and b.CS_SP_TK='105' and a.CS_MAKH =b.CS_MAKH ) order by a.TO_MADP";
                                }
                                dt = cls.LoadDataText(strsql);
                                //FileName = Thumuc + "\\" + pos + "_TT_NO_CASA105_" +dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M02":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") ==lastMonth.ToString("yyyy-MM-dd"))
                                {
                                    strsql = "select left(a.CS_MADP,6) MAXA,(select TEN from DMXA where MA=left(a.CS_MADP,6)) TENXA,a.CS_MATO,(select TO_TENTT from HSTO where TO_MATO = a.CS_MATO) TENTT "
                                             + " ,b.KH_MAKH,b.KH_TENKH,char(39) + a.CS_SO_TK TK, a.CS_SODU_TK "
                                             + " from CASA a, HSKH b where a.CS_NGAYBC = '" + ng +
                                             "' and a.CS_MAPGD = '" + pos +
                                             "' and a.CS_SP_TK = '105' and a.CS_TTSO_TK <> 'C' and a.CS_SODU_TK >=" + thg
                                             +
                                             " and a.CS_MAKH = b.KH_MAKH order by left(a.CS_MADP, 6), a.CS_MATO, a.CS_MAKH";
                                }
                                else
                                {
                                    strsql = "select left(a.CS_MADP,6) MAXA,(select TEN from DMXA where MA=left(a.CS_MADP,6)) TENXA,a.CS_MATO,(select TO_TENTT from HSTO where TO_MATO = a.CS_MATO) TENTT "
                                             + " ,b.KH_MAKH,b.KH_TENKH,char(39) + a.CS_SO_TK TK, a.CS_SODU_TK "
                                             + " from CASA_DAILY a, HSKH b where a.CS_NGAYBC = '" + ng +
                                             "' and a.CS_MAPGD = '" + pos +
                                             "' and a.CS_SP_TK = '105' and a.CS_TTSO_TK <> 'C' and a.CS_SODU_TK >=" + thg+
                                             " and a.CS_MAKH = b.KH_MAKH order by left(a.CS_MADP, 6), a.CS_MATO, a.CS_MAKH";
                                }
                                dt = cls.LoadDataText(strsql);
                                //FileName = Thumuc + "\\" + pos + "_CASA105_"+ten+"_"+thg+"_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M03":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") == lastMonth.ToString("yyyy-MM-dd"))
                                {
                                    strsql = "select cs_mapgd POS,substr(cs_madp,1,6) MAXA"
                                    + ",(select ten from dmxa where ma = substr(cs_madp, 1, 6)) TENXA,cs_makh MAKH, kh_tenkh, cs_so_tk2 TK,cs_sodu_tk SODU, cs_ttso_tk "
                                    + ", to_char(cs_ngayroito,'dd/MM/yyyy') NG_ROITO from casa,hskh where cs_ngaybc ='" + dtpNgay.SelectedDate.Value.ToString("dd/MMM/yyyy") + "'"
                                    + " and cs_sp_tk = '105' and cs_ttso_tk = 'A' and cs_mato is null and to_date('" + dtpNgay.SelectedDate.Value.ToString("dd/MMM/yyyy") + "') - cs_ngayroito > 90 and cs_sodu_tk < 100000"
                                    + " and cs_makh = kh_makh order by substr(cs_madp, 1, 6), cs_makh";
                                }
                                else
                                {
                                    strsql = "select cs_mapgd,(select po_ten from dmpos where po_ma=cs_mapgd) POSTEN,count(cs_makh) Z_TK "
                                            +" from casa_daily left join hsto on cs_mato = to_mato left join hskh on cs_makh = kh_makh "
                                            + " where cs_ngaybc ='" + dtpNgay.SelectedDate.Value.ToString("dd/MMM/yyyy") + "' and cs_sp_tk = '105' and cs_ttso_tk = 'A' and cs_mato is not null"
                                            + " and cs_makh in ( select ku_makh from ( select ku_makh, sum(ku_dnothan + ku_dnoqhan + ku_dnokhoanh) duno"
                                            + " from hscv_daily where ku_ngaybc ='" + dtpNgay.SelectedDate.Value.ToString("dd/MMM/yyyy") + "' group by ku_makh"
                                            + " having sum(ku_dnothan + ku_dnoqhan + ku_dnokhoanh) = 0 )) group by cs_mapgd order by cs_mapgd ";
                                }
                                dt = ora.LoadDataText(strsql);
                                break;
                            case "M04":
                                dt = cls.LoadLdbf("usp_CungCoToThKe", bien, giatri, thamso);
                                //if (dt.Rows.Count > 0)
                                //   // FileName = Thumuc + "\\" + pos + "_ThongKe_ThanhVienTo_" +dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                //else
                                //    MessageBox.Show("Không có KH nào", "Mess", MessageBoxButton.OK,
                                //        MessageBoxImage.Information);
                                break;
                            case "M05":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") ==
                                    lastMonth.ToString("yyyy-MM-dd"))
                                {
                                    strsql = "select b.MA N'Mã Xã',b.TEN N'Tên Xã',c.TO_MATO N'Mã Tổ',c.TO_TENTT N'Tên Tổ Trưởng'"
                                             +
                                             ",d.KH_MAKH N'Mã khách hàng',d.KH_TENKH N'Tên Khách hàng',a.CS_M_GUITK N'Gửi trong tháng',a.CS_SODU_TK N'Số dư' from CASA a,DMXA b,HSTO c,HSKH d"
                                             + " where a.CS_NGAYBC='" + ng + "' and a.CS_MAPGD=" + pos +
                                             " and a.CS_SP_TK='105' and a.CS_TTSO_TK<>'C'"
                                             + " and a.CS_MATO is not null and a.CS_M_GUITK=0"
                                             +
                                             " and LEFT(a.CS_MADP,6)=b.MA and a.CS_MATO=c.TO_MATO and a.CS_MAKH=d.KH_MAKH"
                                             + " order by b.MA,a.CS_MATO,a.CS_MAKH";
                                }
                                else
                                {
                                    strsql = "select b.MA N'Mã Xã',b.TEN N'Tên Xã',c.TO_MATO N'Mã Tổ',c.TO_TENTT N'Tên Tổ Trưởng'"
                                             +
                                             ",d.KH_MAKH N'Mã khách hàng',d.KH_TENKH N'Tên Khách hàng',a.CS_M_GUITK N'Gửi trong tháng',a.CS_SODU_TK N'Số dư' from CASA_DAILY a,DMXA b,HSTO c,HSKH d"
                                             + " where a.CS_NGAYBC='" + ng + "' and a.CS_MAPGD=" + pos +
                                             " and a.CS_SP_TK='105' and a.CS_TTSO_TK<>'C'"
                                             + " and a.CS_MATO is not null and a.CS_M_GUITK=0"
                                             +
                                             " and LEFT(a.CS_MADP,6)=b.MA and a.CS_MATO=c.TO_MATO and a.CS_MAKH=d.KH_MAKH"
                                             + " order by b.MA,a.CS_MATO,a.CS_MAKH";
                                }
                                dt = cls.LoadDataText(strsql);
                                //FileName = Thumuc + "\\" + pos + "_TV_KGUI_CASA105_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M06":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") ==
                                    lastMonth.ToString("yyyy-MM-dd"))
                                {
                                    strsql = "select a.ku_mapgd,substr(a.ku_madp,1,6) MAXA,a.KU_MATO,(select to_tentt from hsto where to_mato=a.ku_mato) tentt "
                                            +" ,substr(ku_sprd_cd, 4, 1) LOAITH,b.kh_makh,b.kh_tenkh,a.ku_soku,a.ku_maqd,(select giatri from dmkhac where khoa_1 = '07' and khoa_2 = a.ku_maqd) CHTR,a.ku_dnothan,a.ku_dnoqhan,a.ku_dnokhoanh "
                                            +" ,a.ku_mapnkt51,(select giatri from dmkhac where khoa_1 = '25' and khoa_2 = a.ku_mapnkt51) PNKT1 "
                                            +" ,a.ku_mapnkt52,(select giatri from dmkhac where khoa_1 = '25' and khoa_2 = a.ku_mapnkt52) PNKT2 "
                                            + " from hsku a, hskh b where a.ku_ngaybc = '"+dtpNgay.SelectedDate.Value.ToString("dd/MMM/yyyy")+"' and a.ku_ttmonvay <> 'CLOSE' and a.ku_dnothan + a.ku_dnoqhan + a.ku_dnokhoanh > 0 "
                                            +" and a.ku_makh = b.kh_makh order by a.ku_madp,a.ku_mato,a.ku_mapnkt51";
                                }
                                else
                                {
                                    strsql = "select a.ku_mapgd,substr(a.ku_madp,1,6) MAXA,a.KU_MATO,(select to_tentt from hsto where to_mato=a.ku_mato) tentt "
                                            + " ,substr(ku_sprd_cd, 4, 1) LOAITH,b.kh_makh,b.kh_tenkh,a.ku_soku,a.ku_maqd,(select giatri from dmkhac where khoa_1 = '07' and khoa_2 = a.ku_maqd) CHTR,a.ku_dnothan,a.ku_dnoqhan,a.ku_dnokhoanh "
                                            + " ,a.ku_mapnkt51,(select giatri from dmkhac where khoa_1 = '25' and khoa_2 = a.ku_mapnkt51) PNKT1 "
                                            + " ,a.ku_mapnkt52,(select giatri from dmkhac where khoa_1 = '25' and khoa_2 = a.ku_mapnkt52) PNKT2 "
                                            + " from hscv_daily a, hskh b where a.ku_ngaybc = '" + dtpNgay.SelectedDate.Value.ToString("dd/MMM/yyyy") + "' and a.ku_ttmonvay <> 'CLOSE' and a.ku_dnothan + a.ku_dnoqhan + a.ku_dnokhoanh > 0 "
                                            + " and a.ku_makh = b.kh_makh order by a.ku_madp,a.ku_mato,a.ku_mapnkt51";
                                }
                                //MessageBox.Show(strsql);
                                dt = ora.LoadDataText(strsql);
                                //FileName = Thumuc + "\\" + pos + "_SKE_PNKT_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M07":
                                strsql = "select b.MA N'Ma xã',b.TEN N'Tên Xã',sum(a.KU_DNOTHAN+a.KU_DNOQHAN+a.KU_DNOKHOANH) DUNO "
                                         +"from HSKU a,DMXA b where a.KU_NGAYBC='"+ng+"' and a.KU_TTMONVAY<>'CLOSE' "
                                         +"and a.KU_DNOTHAN+a.KU_DNOQHAN+a.KU_DNOKHOANH>0 and a.KU_CHTRINH='06'"
                                         +" and LEFT(a.KU_MADP,6)=b.MA and b.DA_CHOBA='T' "
                                         +" group by b.MA,b.TEN order by b.MA ";
                                dt = cls.LoadDataText(strsql);
                                //FileName = Thumuc + "\\" + pos + "_SKE_CHOBA_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M08":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") ==
                                    lastMonth.ToString("yyyy-MM-dd"))
                                {
                                    strsql = "select a.KU_MAPGD,a.MAXA,c.TEN TENXA,a.DUNO,a.KU_MATO,b.TO_TENTT,b.TRANGTHAI,b.TO_LOAITO from "
                                             +
                                             " (select a.KU_MAPGD,LEFT(a.KU_MADP,6) MAXA,a.KU_MATO,SUM(a.KU_DNOTHAN+a.KU_DNOQHAN+a.KU_DNOKHOANH) DUNO "
                                             + " from HSKU a where a.KU_NGAYBC='" + ng +
                                             "' and a.KU_MATO is not null "
                                             +
                                             " group by a.KU_MAPGD,LEFT(a.KU_MADP,6),a.KU_MATO having SUM(a.KU_DNOTHAN+a.KU_DNOQHAN+a.KU_DNOKHOANH)=0 "
                                             +
                                             " ) a,HSTO b,DMXA c where a.KU_MATO=b.TO_MATO and a.MAXA=c.MA and b.TRANGTHAI='A' "
                                             + " order by a.MAXA,a.KU_MATO";
                                }
                                else
                                {
                                    strsql = "select a.KU_MAPGD,a.MAXA,c.TEN TENXA,a.DUNO,a.KU_MATO,b.TO_TENTT,b.TRANGTHAI,b.TO_LOAITO from "
                                             +
                                             " (select a.KU_MAPGD,LEFT(a.KU_MADP,6) MAXA,a.KU_MATO,SUM(a.KU_DNOTHAN+a.KU_DNOQHAN+a.KU_DNOKHOANH) DUNO "
                                             + " from HSCV_DAILY a where a.KU_NGAYBC='" + ng +
                                             "' and a.KU_MATO is not null "
                                             +
                                             " group by a.KU_MAPGD,LEFT(a.KU_MADP,6),a.KU_MATO having SUM(a.KU_DNOTHAN+a.KU_DNOQHAN+a.KU_DNOKHOANH)=0 "
                                             +
                                             " ) a,HSTO b,DMXA c where a.KU_MATO=b.TO_MATO and a.MAXA=c.MA and b.TRANGTHAI='A' "
                                             + " order by a.MAXA,a.KU_MATO";
                                }
                                dt = cls.LoadDataText(strsql);
                                //FileName = Thumuc + "\\" + pos + "_TO_HET_DUNO_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M09":
                                //MessageBox.Show(giatri1[0].ToString() + " " + giatri1[1].ToString());
                                dt = cls.LoadLdbf("usp_NotCasa105", bien, giatri, thamso);
                                //if (dt.Rows.Count > 0)
                                //   // FileName = Thumuc + "\\" + pos + "_NotCasa105_" +dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                //else
                                //    MessageBox.Show("Không có KH nào", "Mess", MessageBoxButton.OK,
                                //        MessageBoxImage.Information);
                                break;
                            case "M10":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd")== lastMonth.ToString("yyyy-MM-dd")) //|| (int)dtpNgay.SelectedDate.Value.DayOfWeek == 5)
                                    strsql = "select b.KH_MAKH,b.KH_TENKH,char(39)+a.KU_SOKU SOKU,a.KU_TON_RPA,a.KU_TTMONVAY from HSKU a,HSKH b where a.KU_MAKH=b.KH_MAKH and a.KU_TON_RPA>0 and a.KU_NGAYBC='" + ng+"' and a.KU_MAPGD='"+pos+"'";
                                else strsql = "select b.KH_MAKH,b.KH_TENKH,char(39)+a.KU_SOKU SOKU,a.KU_TON_RPA,a.KU_TTMONVAY from HSCV_DAILY a,HSKH b where a.KU_MAKH=b.KH_MAKH and a.KU_TON_RPA>0 and a.KU_NGAYBC='" + ng + "' and a.KU_MAPGD='" + pos + "'";
                                dt = cls.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_TON_RPA_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M11":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") == lastMonth.ToString("yyyy-MM-dd"))
                                {
                                    strsql = "select c.KH_MAPGD,substr(a.KU_MADP,1,6) MAXA ,(select TEN from DMXA where MA=substr(a.KU_MADP,1,6)) TENXA,a.KU_MATO,(select TO_TENTT from HSTO where TO_MATO = a.KU_MATO) TENTT,a.KU_MAKH MAKH, c.KH_TENKH TENKH,a.KU_SOKU , a.KU_CHTRINH,a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO, b.CS_MATO, b.CS_SO_TK, b.CS_SODU_TK from (select * from HSKU   where KU_NGAYBC = '" + ngora + "' and KU_MAPGD = '" + pos + "' and KU_TTMONVAY <> 'CLOSE' and KU_DNOTHAN + KU_DNOQHAN + KU_DNOKHOANH > 0 and KU_HTHUCVAY = '3') a, (select * from CASA where CS_NGAYBC = '" + ngora + "' and CS_MAPGD = '" + pos + "' and CS_SP_TK = '105' and CS_MATO is null and CS_TTSO_TK <> 'C') b ,HSKH c where a.KU_MAKH = b.CS_MAKH and a.KU_MAKH = c.KH_MAKH and b.CS_MAKH = c.KH_MAKH order by substr(a.KU_MADP, 1, 6),a.KU_MATO";
                                }
                                else
                                {
                                    strsql = "select c.KH_MAPGD,substr(a.KU_MADP,1,6) MAXA ,(select TEN from DMXA where MA=substr(a.KU_MADP,1,6)) TENXA,a.KU_MATO,(select TO_TENTT from HSTO where TO_MATO = a.KU_MATO) TENTT,a.KU_MAKH MAKH, c.KH_TENKH TENKH,a.KU_SOKU , a.KU_CHTRINH,a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO, b.CS_MATO, b.CS_SO_TK, b.CS_SODU_TK from (select * from HSCV_DAILY   where KU_NGAYBC = '"+ngora+ "' and KU_MAPGD = '" + pos + "' and KU_TTMONVAY <> 'CLOSE' and KU_DNOTHAN + KU_DNOQHAN + KU_DNOKHOANH > 0 and KU_HTHUCVAY = '3') a, (select * from CASA_DAILY where CS_NGAYBC = '" + ngora + "' and CS_MAPGD = '" + pos + "' and CS_SP_TK = '105' and CS_MATO is null and CS_TTSO_TK <> 'C') b ,HSKH c where a.KU_MAKH = b.CS_MAKH and a.KU_MAKH = c.KH_MAKH and b.CS_MAKH = c.KH_MAKH order by substr(a.KU_MADP, 1, 6),a.KU_MATO";
                                }
                        
                                dt = ora.LoadDataText(strsql);
                                //if (dt.Rows.Count > 0)
                                //    FileName = Thumuc + "\\" + pos + "_GanMatoChoCasa105_" +dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                //else
                                //    MessageBox.Show("Không có KH nào", "Mess", MessageBoxButton.OK,
                                //        MessageBoxImage.Information);
                                break;
                            case "M12": //(dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") == LastMonth.ToString("yyyy-MM-dd") || (int)dtpNgay.SelectedDate.Value.DayOfWeek == 5)
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") == lastMonth.ToString("yyyy-MM-dd"))
                                    strsql = "select b.KU_MAPGD N'Mã Phòng GD',count(b.KU_MATO) N'Tổng số tổ',sum( case when b.DNQH=0 then 1 else 0 end) N'Số tổ không có nợ quá hạn',"
                                             +" count(b.KU_MATO) - sum( case when b.DNQH = 0 then 1 else 0 end) N'Số tổ có nợ quá hạn' from"
                                             +" (select a.KU_MAPGD, a.KU_MATO, sum(a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH) DUNO, sum(a.KU_DNOQHAN) DNQH from HSKU a"
                                             + " where a.KU_NGAYBC = '"+ng+ "' and a.KU_TTMONVAY <> 'CLOSE' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0  group by a.KU_MAPGD, a.KU_MATO"
                                             +" ) b group by b.KU_MAPGD ";
                                else strsql = "select b.KU_MAPGD N'Mã Phòng GD',count(b.KU_MATO) N'Tổng số tổ',sum( case when b.DNQH=0 then 1 else 0 end) N'Số tổ không có nợ quá hạn',"
                                              + " count(b.KU_MATO) - sum( case when b.DNQH = 0 then 1 else 0 end) N'Số tổ có nợ quá hạn' from"
                                              + " (select a.KU_MAPGD, a.KU_MATO, sum(a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH) DUNO, sum(a.KU_DNOQHAN) DNQH from HSCV_DAILY a"
                                              + " where a.KU_NGAYBC = '" + ng + "' and a.KU_TTMONVAY <> 'CLOSE' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0  group by a.KU_MAPGD, a.KU_MATO"
                                              + " ) b group by b.KU_MAPGD ";
                                dt = cls.LoadDataText(strsql);
                                //if (dt.Rows.Count > 0)
                                //    FileName = Thumuc + "\\" + pos + "_TO_KHONG_CO_NQH_" +
                                //               dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                //else
                                //    MessageBox.Show("Không có tổ nào", "Mess", MessageBoxButton.OK,
                                //        MessageBoxImage.Information);

                                break;
                            case "M13":
                                dt = cls.LoadLdbf("usp_ChkAddHSKH", bien, giatri, thamso);
                                //if (dt.Rows.Count > 0)
                                //    FileName = Thumuc + "\\" + pos + "_BoSung_TT_VC_" +
                                //               dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                //else
                                //    MessageBox.Show("Không có KH nào", "Mess", MessageBoxButton.OK,
                                //        MessageBoxImage.Information);
                                break;
                            case "M14":
                                strsql = "select a.NGAYGD,char(39)+a.TK N'Tài khoản',b.CS_MATO N'Mã tô',(select TO_TENTT from HSTO where TO_MATO=b.CS_MATO) N'Tên TT' "
                                         +" ,c.KH_MAKH N'Mã KH',c.KH_TENKH N'Tên KH',a.SOTIEN N'Số tiền ',a.TK_NO N'TK nợ',a.TK_CO N'TK Có' "
                                         +" from HSBT a,CASA b, HSKH c where a.MAPGD = '"+pos+"' and b.CS_MAPGD = '"+pos+"' and a.MOD_CD in ('CT','FP') and a.NOCO = 'D' and b.CS_NGAYBC = '"+ng+"' and b.CS_SP_TK = '105' and a.TK = b.CS_SO_TK "
                                         +" and b.CS_MAKH = c.KH_MAKH order by a.NGAYGD,b.CS_MATO,c.KH_MAKH";
                                //MessageBox.Show(strsql);
                                dt = cls.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_RUT_CASA105_TM_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M15":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") ==lastMonth.ToString("yyyy-MM-dd"))
                                {
                                    strsql = "select a.KU_MAPGD ,left(a.KU_MADP,6) MAXA,(select TEN from DMXA where MA=left(a.KU_MADP,6)) N'Tên Xã',"
                                             +
                                             " a.KU_MATO,(select TO_TENTT from HSTO where TO_MATO = a.KU_MATO) N'Tên TT',b.KH_MAKH N'Mã KH',b.KH_TENKH N'Tên KH',char(39) + a.KU_SOKU N'Số KU', a.KU_CHTRINH N'Chương Trình',"
                                             +
                                             " a.KU_DNOTHAN DNTH, a.KU_DNOQHAN DNQH, a.KU_DNOKHOANH DNKH, a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO, a.KU_LAITONTHAN + a.KU_LAITONQHAN LAITON, a.KU_NGAYGDGN N'Ngày GD ngần nhất', DATEDIFF(mm, a.KU_NGAYGDGN, '" +
                                             ng + "') N'Số Tháng' "
                                             + " from HSKU a, HSKH b where a.KU_NGAYBC = '" + ng +
                                             "' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 and a.KU_TTMONVAY <> 'CLOSE' and a.KU_MAPGD = '" +
                                             pos + "' and DATEDIFF(mm, a.KU_NGAYGDGN,'" + ng + "') >= " + thg +
                                             " and a.KU_CHTRINH not in ('02', '07', '11', '18') "
                                             + " and a.KU_MAKH = b.KH_MAKH and a.KU_CHTRINH not in ('02','07','18') "
                                             + " order by left(a.KU_MADP, 6),a.KU_MATO,b.KH_MAKH";
                                }
                                else
                                {
                                    strsql = "select a.KU_MAPGD ,left(a.KU_MADP,6) MAXA,(select TEN from DMXA where MA=left(a.KU_MADP,6)) N'Tên Xã',"
                                             +
                                             " a.KU_MATO,(select TO_TENTT from HSTO where TO_MATO = a.KU_MATO) N'Tên TT',b.KH_MAKH N'Mã KH',b.KH_TENKH N'Tên KH',char(39) + a.KU_SOKU N'Số KU', a.KU_CHTRINH N'Chương Trình',"
                                             +
                                             " a.KU_DNOTHAN DNTH, a.KU_DNOQHAN DNQH, a.KU_DNOKHOANH DNKH, a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO, a.KU_LAITONTHAN + a.KU_LAITONQHAN LAITON, a.KU_NGAYGDGN N'Ngày GD ngần nhất', DATEDIFF(mm, a.KU_NGAYGDGN, '" +
                                             ng + "') N'Số Tháng' "
                                             + " from HSCV_DAILY a, HSKH b where a.KU_NGAYBC = '" + ng +
                                             "' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 and a.KU_TTMONVAY <> 'CLOSE' and a.KU_MAPGD = '" +
                                             pos + "' and DATEDIFF(mm, a.KU_NGAYGDGN,'" + ng + "') >= " + thg +
                                             " and a.KU_CHTRINH not in ('02', '07', '11', '18') "
                                             + " and a.KU_MAKH = b.KH_MAKH and a.KU_CHTRINH not in ('02','07','18') "
                                             + " order by left(a.KU_MADP, 6),a.KU_MATO,b.KH_MAKH";
                                }
                                dt = cls.LoadDataText(strsql);
                                //FileName = Thumuc + "\\" + pos + "_KU_"+thg+"_THANG_KHONG_HD" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M16":
                                strsql = "select a.KH_MAPGD,left(a.KH_MADP,6) N'Mã Xã',(select TEN from DMXA where MA=left(a.KH_MADP,6)) N'Tên Xã',a.KH_MADP N'Mã Thôn',"
                                         +" (select TEN from DMTHON where MA = a.KH_MADP) N'Tên Thôn' "
                                         +" ,a.KH_MAKH N'Mã ',a.KH_TENKH N'Tên KH',a.KH_DIACHI N'Đại chỉ',a.KH_CMT N'CMT',a.KH_NGAYCAP N'Ngày cấp'" 
                                         +" ,DATEDIFF(YYYY, a.KH_NGAYCAP, '"+ng+"') N'Số năm' "
                                         +" from HSKH a,(select * from HSKU where KU_NGAYBC = '"+ng+"' and KU_DNOTHAN+KU_DNOQHAN + KU_DNOKHOANH > 0 and KU_TTMONVAY<> 'CLOSE' "
                                         +" and KU_MAPGD = '"+pos+"') b where DATEDIFF(YYYY, a.KH_NGAYCAP, '"+ng+"') > 15 and a.KH_MAKH = b.KU_MAKH order by a.KH_MADP";
                                // MessageBox.Show(strsql);
                                dt = cls.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_SAOKE_CMT_HETHAN_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M17":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") == lastMonth.ToString("yyyy-MM-dd"))
                                {
                                    strsql = "select a.KH_MAPGD,left(a.KH_MADP,6) N'Mã Xã',(select TEN from DMXA where MA=left(a.KH_MADP,6)) N'Tên Xã',a.KH_MADP N'Mã Thôn', "
                                             + " (select TEN from DMTHON where MA = a.KH_MADP) N'Tên Thôn'"
                                             + " ,count(a.KH_MAKH) N'Tổng số KH' "
                                             + " from HSKH a,(select * from HSKU where KU_NGAYBC = '" + ng +
                                             "' and KU_DNOTHAN+KU_DNOQHAN + KU_DNOKHOANH > 0 and KU_TTMONVAY<> 'CLOSE' "
                                             + " and KU_MAPGD = '" + pos + "') b where DATEDIFF(YYYY, a.KH_NGAYCAP, '" +
                                             ng +
                                             "') > 15 and a.KH_MAKH = b.KU_MAKH group by a.KH_MAPGD,left(a.KH_MADP,6),a.KH_MADP";
                                }
                                else
                                {
                                    strsql = "select a.KH_MAPGD,left(a.KH_MADP,6) N'Mã Xã',(select TEN from DMXA where MA=left(a.KH_MADP,6)) N'Tên Xã',a.KH_MADP N'Mã Thôn', "
                                             + " (select TEN from DMTHON where MA = a.KH_MADP) N'Tên Thôn'"
                                             + " ,count(a.KH_MAKH) N'Tổng số KH' "
                                             + " from HSKH a,(select * from HSCV_DAILY where KU_NGAYBC = '" + ng +
                                             "' and KU_DNOTHAN+KU_DNOQHAN + KU_DNOKHOANH > 0 and KU_TTMONVAY<> 'CLOSE' "
                                             + " and KU_MAPGD = '" + pos + "') b where DATEDIFF(YYYY, a.KH_NGAYCAP, '" +
                                             ng +
                                             "') > 15 and a.KH_MAKH = b.KU_MAKH group by a.KH_MAPGD,left(a.KH_MADP,6),a.KH_MADP";
                                }
                                dt = cls.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_SAOKE_CMT_HETHAN_TONGHOP" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M18":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") == lastMonth.ToString("yyyy-MM-dd"))
                                {
                                    strsql = "select a.KU_MAPGD N'POS',(select GIATRI from DMKHAC where KHOA_1='17' and KHOA_2=d.TO_DVUT) N'Tên DVUT',c.MA N'Mã Xã',c.TEN N'Tên Xã',d.TO_MATO N'Mã Tổ',d.TO_TENTT N'Tên TT',b.KH_MAKH N'Mã KH' "
                                             + " ,b.KH_TENKH N'Tên KH',char(39)+a.KU_SOKU N'Số KU',a.KU_NGAYVAY N'Ngày vay',a.KU_NGAYDHAN_3 N'Đến hạn',a.KU_DNOTHAN N'Dư nợ TH' "
                                             +" ,a.KU_DNOQHAN N'Dư nợ QH',a.KU_DNOKHOANH N'Dư nợ khoanh',a.KU_LAITONTHAN N'Lãi tồn TH',a.KU_LAITONQHAN N'Lãi tồn QH',a.KU_LAITONTHAN + a.KU_LAITONQHAN N'Lãi tồn' "
                                             +" from HSKU a,HSKH b, DMXA c,HSTO d where a.KU_MATO=d.TO_MATO and a.KU_MAPGD = '"+pos+"' and a.KU_NGAYBC = '"+ng+"' and a.KU_TTMONVAY <> 'CLOSE' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 and a.KU_LAITONTHAN + a.KU_LAITONQHAN > "+thg
                                             +" and a.KU_CHTRINH not in ('02','07','18') and a.KU_MAKH = b.KH_MAKH and left(a.KU_MADP, 6)= c.MA order by c.MA,d.TO_MATO";
                                }
                                else
                                {
                                    strsql = "select a.KU_MAPGD N'POS',(select GIATRI from DMKHAC where KHOA_1='17' and KHOA_2=d.TO_DVUT) N'Tên DVUT',c.MA N'Mã Xã',c.TEN N'Tên Xã',d.TO_MATO N'Mã Tổ',d.TO_TENTT N'Tên TT',b.KH_MAKH N'Mã KH' "
                                             + " ,b.KH_TENKH N'Tên KH',char(39)+a.KU_SOKU N'Số KU',a.KU_NGAYVAY N'Ngày vay',a.KU_NGAYDHAN_3 N'Đến hạn',a.KU_DNOTHAN N'Dư nợ TH' "
                                             + " ,a.KU_DNOQHAN N'Dư nợ QH',a.KU_DNOKHOANH N'Dư nợ khoanh',a.KU_LAITONTHAN N'Lãi tồn TH',a.KU_LAITONQHAN N'Lãi tồn QH',a.KU_LAITONTHAN + a.KU_LAITONQHAN N'Lãi tồn' "
                                             + " from HSCV_DAILY a,HSKH b, DMXA c,HSTO d where a.KU_MATO=d.TO_MATO and a.KU_MAPGD = '" + pos + "' and a.KU_NGAYBC = '" + ng + "' and a.KU_TTMONVAY <> 'CLOSE' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 and a.KU_LAITONTHAN + a.KU_LAITONQHAN > " + thg
                                             + " and a.KU_CHTRINH not in ('02','07','18') and a.KU_MAKH = b.KH_MAKH and left(a.KU_MADP, 6)= c.MA order by c.MA,d.TO_MATO";
                                }
                                dt = cls.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_LAI_TON_LON_HON_"+thg.Trim()+"_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M19":
                                strsql = "select b.KH_MAPGD,c.MA N'Mã Xã',c.TEN N'Tên Xã',b.KH_MAKH N'Mã KH',b.KH_TENKH N'Tên KH',char(39)+a.CS_SO_TK N'Tài khoản',a.CS_TENTK N'Tên Tài khoản',a.CS_SODU_TK N'Số dư',a.CS_TTSO_TK N'Tình trạng' "
                                            +" from CASA a,hskh b, DMXA c "
                                            +" where a.CS_NGAYBC = '"+ng+"' and CS_SP_TK = '104' and a.CS_MAPGD = '"+pos+"' and a.CS_MAKH = b.KH_MAKH and c.MA = left(a.CS_MADP, 6) "
                                            +" order by c.MA,b.KH_MAKH";
                                dt = cls.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_CASA_104_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M20":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") == lastMonth.ToString("yyyy-MM-dd"))
                                {
                                    if (pos == "003000")
                                        strsql = "select a.CS_MAPGD N'Mã POS',(select PO_TEN from DMPOS where PO_MA=a.CS_MAPGD) N'Tên',COUNT(distinct a.CS_MATO) N'Số tổ',COUNT(distinct a.CS_MAKH) N'Số KH' "
                                        + " from casa a where a.CS_NGAYBC = '"+ng+"' and a.CS_SP_TK = '105' and a.CS_TTSO_TK <> 'C' and a.CS_SODU_TK > 0 "
                                        + " and a.CS_MATO is not null group by a.CS_MAPGD";
                                    else
                                        strsql = "select a.MAXA N'Mã Xã',(select TEN from DMXA where MA=a.MAXA) N'Tên Xã',count(distinct a.CS_MATO) N'Số tổ' "
                                                + " ,count(distinct a.CS_MAKH) N'Số KH' ,sum(a.CS_SODU_TK) N'Số dư' from "
                                                + " (select left(a.CS_MADP, 6) MAXA, a.CS_MATO, a.CS_MAKH, a.CS_SODU_TK "
                                                + " from CASA a, HSKH b where a.CS_NGAYBC = '" + ng + "' and a.CS_MAPGD = '" + pos + "' "
                                                + " and a.CS_SP_TK = '105' and a.CS_TTSO_TK <> 'C' and a.CS_MATO is not null and a.CS_MAKH = b.KH_MAKH "
                                                + " ) a group by a.MAXA order by a.MAXA";
                                }
                                else
                                {
                                    if (pos == "003000")
                                        strsql = "select a.CS_MAPGD N'Mã POS',(select PO_TEN from DMPOS where PO_MA=a.CS_MAPGD) N'Tên',COUNT(distinct a.CS_MATO) N'Số tổ',COUNT(distinct a.CS_MAKH) N'Số KH' "
                                        + " from CASA_DAILY a where a.CS_NGAYBC = '" + ng + "' and a.CS_SP_TK = '105' and a.CS_TTSO_TK <> 'C' and a.CS_SODU_TK > 0 "
                                        + " and a.CS_MATO is not null group by a.CS_MAPGD";
                                    else
                                        strsql = "select a.MAXA N'Mã Xã',(select TEN from DMXA where MA=a.MAXA) N'Tên Xã',count(distinct a.CS_MATO) N'Số tổ' "
                                            + " ,count(distinct a.CS_MAKH) N'Số KH' ,sum(a.CS_SODU_TK) N'Số dư' from "
                                            + " (select left(a.CS_MADP, 6) MAXA, a.CS_MATO, a.CS_MAKH, a.CS_SODU_TK "
                                            + " from CASA_DAILY a, HSKH b where a.CS_NGAYBC = '" + ng + "' and a.CS_MAPGD = '" + pos + "' "
                                            + " and a.CS_SP_TK = '105' and a.CS_TTSO_TK <> 'C' and a.CS_MATO is not null and a.CS_MAKH = b.KH_MAKH "
                                            + " ) a group by a.MAXA order by a.MAXA";
                                }
                                dt = cls.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_TIEN_GUI_TO_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M21":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") == lastMonth.ToString("yyyy-MM-dd"))
                                {
                                    strsql = "select a.NGAYGD,left(c.KH_MADP,6) MAXA,(select TEN from DMXA where MA = left(c.KH_MADP, 6)) N'Tên Xã' "
                                            +" ,a.CS_MATO,(select TO_TENTT from HSTO where TO_MATO = a.CS_MATO) N'Tên TT' "
                                            +" ,b.MAKH N'Mã KH',c.KH_TENKH N'Tên KH',char(39)+a.TK N'Tài khoản',a.SOTIEN N'Số tiền nộp CASA 105',b.SOTIEN N'Trả lãi',a.SOTIEN - b.SOTIEN N'Nộp TK' from "
                                            +" ( select a.NGAYGD, b.CS_MATO, b.CS_MAKH, a.TK, a.SOTIEN from HSBT a, (select * from CASA where CS_NGAYBC = '"+ng+"' and CS_MAPGD = '"+pos+"' and CS_SP_TK = '105') b "
                                            +" where NGAYGD > '2016-08-01' and substring(tk_co,1, 6)= '922105' and MOD_CD<> 'AA' and MAPGD = '"+pos+"' and SOTIEN> 2000000 "
                                            +" and a.TK = b.CS_SO_TK) a,( select substring(GHICHU_2, 1, 10) MAKH,sum(SOTIEN) SOTIEN "
                                            +" from HSBT where  substring(tk_co, 1, 4) = '9402' and NGAYGD> '2016-08-01' and MAPGD = '"+pos+"' "
                                            +" group by substring(GHICHU_2, 1, 10)) b,HSKH c "
                                            +" where a.CS_MAKH = b.MAKH and a.SOTIEN - b.SOTIEN > 2000000 and b.MAKH = c.KH_MAKH and a.CS_MAKH = c.KH_MAKH";
                                }
                                else
                                {
                                    strsql = "select a.NGAYGD,left(c.KH_MADP,6) MAXA,(select TEN from DMXA where MA = left(c.KH_MADP, 6)) N'Tên Xã' "
                                            + " ,a.CS_MATO,(select TO_TENTT from HSTO where TO_MATO = a.CS_MATO) N'Tên TT' "
                                            + " ,b.MAKH N'Mã KH',c.KH_TENKH N'Tên KH',char(39)+a.TK N'Tài khoản',a.SOTIEN N'Số tiền nộp CASA 105',b.SOTIEN N'Trả lãi',a.SOTIEN - b.SOTIEN N'Nộp TK' from "
                                            + " ( select a.NGAYGD, b.CS_MATO, b.CS_MAKH, a.TK, a.SOTIEN from HSBT a, (select * from CASA_DAILY where CS_NGAYBC = '"+ng+"' and CS_MAPGD = '"+pos+"' and CS_SP_TK = '105') b "
                                            + " where NGAYGD > '2016-08-01' and substring(tk_co,1, 6)= '922105' and MOD_CD<> 'AA' and MAPGD = '" + pos + "' and SOTIEN> 2000000 "
                                            + " and a.TK = b.CS_SO_TK) a,( select substring(GHICHU_2, 1, 10) MAKH,sum(SOTIEN) SOTIEN "
                                            + " from HSBT where  substring(tk_co, 1, 4) = '9402' and NGAYGD> '2016-08-01' and MAPGD = '" + pos + "' "
                                            + " group by substring(GHICHU_2, 1, 10)) b,HSKH c "
                                            + " where a.CS_MAKH = b.MAKH and a.SOTIEN - b.SOTIEN > 2000000 and b.MAKH = c.KH_MAKH and a.CS_MAKH = c.KH_MAKH";
                                }
                                // MessageBox.Show(strsql);
                                dt = cls.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_TIEN_GUI_TO_TREN_2_TRIEU_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M22":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") == lastMonth.ToString("yyyy-MM-dd"))
                                {
                                    strsql = "select b.KH_MAPGD,left(b.KH_MADP,6) N'Mã xã',(select TEN from DMXA where MA=left(b.KH_MADP,6)) N'Tên Xã' "
                                            +" ,a.DEM N'Số lượng TK',N'KH có nhiều CASA 105 có SK chi tiết KT740' N'Mô tả' from "
                                            +" (select CS_MAKH, count(CS_MAKH) DEM "
                                            +" from CASA where CS_MAPGD = '"+pos+"' and CS_NGAYBC = '"+ng+"' and CS_SP_TK = '105' and CS_TTSO_TK <> 'C' "
                                            +" group by CS_MAKH having count(CS_MAKH) > 1) a,HSKH b where a.CS_MAKH = b.KH_MAKH order by left(b.KH_MADP, 6)";
                                }
                                else
                                {
                                    strsql = "select b.KH_MAPGD,left(b.KH_MADP,6) N'Mã xã',(select TEN from DMXA where MA=left(b.KH_MADP,6)) N'Tên Xã' "
                                            + " ,a.DEM N'Số lượng TK',N'KH có nhiều CASA 105 có SK chi tiết KT740' N'Mô tả' from "
                                            + " (select CS_MAKH, count(CS_MAKH) DEM "
                                            + " from CASA_DAILY where CS_MAPGD = '" + pos + "' and CS_NGAYBC = '" + ng + "' and CS_SP_TK = '105' and CS_TTSO_TK <> 'C' "
                                            + " group by CS_MAKH having count(CS_MAKH) > 1) a,HSKH b where a.CS_MAKH = b.KH_MAKH order by left(b.KH_MADP, 6)";
                                }
                                // MessageBox.Show(strsql);
                                dt = cls.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_TH_KH_NHIEU_CASA_105_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M23":
                                strsql = "select * from ( select a.PGD_QL, a.MA, a.TEN, b.DEM from DMXA a left join ( select a.MAXA, COUNT(a.MAXA) DEM from "
                                    +" (select CONVERT(varchar(2), NGAYGD, 103) NGGDXA, SUBSTRING(GHICHU_1, charindex('TXN', GHICHU_1, 1) + 4, 6) as MAXA "
                                    +" , NGAYGD, SOTIEN, MAPGD, SBT, TK, GHICHU_1, GDV, KSV "
                                    +" from HSBT where MAPGD = '"+pos+"' and NGAYGD >= DATEADD(yy, DATEDIFF(yy, 0, '"+ng+"'), 0) and NGAYGD <= '"+ng+"' and GHICHU_2 = 'VLT_TO_ATM' "
                                    +" and TK_CO = '9100007043') a, DMXA b where a.MAXA = b.MA and a.NGGDXA <> b.NGAYGDX "
                                    +" group by a.MAXA) b on a.MA = b.MAXA) a where a.PGD_QL = '"+pos+"' and right(a.MA, 2)<> '00' order by a.MA";
                                MessageBox.Show(strsql);
                                dt = cls.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_TANG_PHIEN_NAM_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M24":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") == lastMonth.ToString("yyyy-MM-dd"))
                                {
                                    strsql = "select a.*,(select TO_TENTT from HSTO where TO_MATO=a.MATO) TENTT from ( "
                                            +" select a.*, (select CS_MATO from CASA where CS_NGAYBC = DATEADD(yy, -2, DATEADD(dd, -1, DATEADD(yy, DATEDIFF(yy, 0, '"+ng+"'), 0))) "
                                            +" and CS_MAPGD = '"+pos+"' and CS_SP_TK = '105' and CS_MAKH = a.KH_MAKH and CS_SO_TK = a.CS_SO_TK) MATO from "
                                            +" ( select b.KH_MAPGD, left(b.KH_MADP, 6) MAXA, (select TEN from DMXA where MA = left(b.KH_MADP, 6)) TENXA, "
                                            +" b.KH_MAKH,b.KH_TENKH,a.CS_SO_TK,char(39) + a.CS_SO_TK SOTK , a.CS_SODU_TK, a.CS_TTSO_TK, a.CS_NGAYROITO, a.CS_NGAYTT from CASA a, HSKH b "
                                            +" where a.CS_NGAYBC = '"+ng+"' and a.CS_MAPGD = '"+ pos + "' and a.CS_SP_TK = '105' and a.CS_MATO is null "
                                            +" and a.CS_MAKH = b.KH_MAKH) a) a order by a.MAXA,a.MATO,a.KH_MAKH";
                                }
                                else
                                {
                                    strsql = "select a.*,(select TO_TENTT from HSTO where TO_MATO=a.MATO) TENTT from ( "
                                            + " select a.*, (select CS_MATO from CASA where CS_NGAYBC = DATEADD(yy, -2, DATEADD(dd, -1, DATEADD(yy, DATEDIFF(yy, 0, '" + ng + "'), 0))) "
                                            + " and CS_MAPGD = '" + pos + "' and CS_SP_TK = '105' and CS_MAKH = a.KH_MAKH and CS_SO_TK = a.CS_SO_TK) MATO from "
                                            + " ( select b.KH_MAPGD, left(b.KH_MADP, 6) MAXA, (select TEN from DMXA where MA = left(b.KH_MADP, 6)) TENXA, "
                                            + " b.KH_MAKH,b.KH_TENKH,a.CS_SO_TK,char(39) + a.CS_SO_TK SOTK , a.CS_SODU_TK, a.CS_TTSO_TK, a.CS_NGAYROITO, a.CS_NGAYTT from CASA_DAILY a, HSKH b "
                                            + " where a.CS_NGAYBC = '" + ng + "' and a.CS_MAPGD = '" + pos + "' and a.CS_SP_TK = '105' and a.CS_MATO is null "
                                            + " and a.CS_MAKH = b.KH_MAKH) a) a order by a.MAXA,a.MATO,a.KH_MAKH";
                                }
                                dt = cls.LoadDataText(strsql);
                              //  FileName = Thumuc + "\\" + pos + "_CASA_105_DA_GO_MA_TO_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M25":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") == lastMonth.ToString("yyyy-MM-dd"))
                                {
                                    strsql = "select c.MA N'Mã Xã',c.TEN N'Tên Xã',d.TO_MATO N'Mã Tổ',d.TO_TENTT N'Tên TT' "+
	                                            ",b.KH_MAKH N'Mã KH',b.KH_TENKH N'Tên KH',b.KH_TENVC N'Tên VC',b.KH_CMT_VC N'CMT VC' "+
	                                            " ,sum(a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH) N'Dư nợ' " +
                                                " from HSKU a,HSKH b, DMXA c,HSTO d "+
                                                " where a.KU_NGAYBC = '"+ng+"' and a.KU_MAPGD = '"+pos+"' and a.KU_MAKH = b.KH_MAKH and left(a.KU_MADP, 6)= c.MA "+
                                                " and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 and a.KU_TTMONVAY <> 'CLOSE' and a.KU_MATO = d.TO_MATO "+
                                                " and(b.KH_TENVC is null or b.KH_CMT_VC is null) " +
                                                " group by c.MA,c.TEN,d.TO_MATO,d.TO_TENTT,b.KH_MAKH,b.KH_TENKH,b.KH_TENVC,b.KH_CMT_VC order by c.MA,d.TO_MATO";
                                }
                                else
                                {
                                    strsql = "select c.MA N'Mã Xã',c.TEN N'Tên Xã',d.TO_MATO N'Mã Tổ',d.TO_TENTT N'Tên TT' " +
                                                ",b.KH_MAKH N'Mã KH',b.KH_TENKH N'Tên KH',b.KH_TENVC N'Tên VC',b.KH_CMT_VC N'CMT VC' " +
                                                " ,sum(a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH) N'Dư nợ' " +
                                                " from HSCV_DAILY a,HSKH b, DMXA c,HSTO d " +
                                                " where a.KU_NGAYBC = '" + ng + "' and a.KU_MAPGD = '" + pos + "' and a.KU_MAKH = b.KH_MAKH and left(a.KU_MADP, 6)= c.MA " +
                                                " and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 and a.KU_TTMONVAY <> 'CLOSE' and a.KU_MATO = d.TO_MATO " +
                                                " and(b.KH_TENVC is null or b.KH_CMT_VC is null) " +
                                                " group by c.MA,c.TEN,d.TO_MATO,d.TO_TENTT,b.KH_MAKH,b.KH_TENKH,b.KH_TENVC,b.KH_CMT_VC order by c.MA,d.TO_MATO";
                                }

                                //MessageBox.Show(strsql);
                                dt = cls.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_Sao kê thiếu TT vợ chồng_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M29":
                                //if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") == lastMonth.ToString("yyyy-MM-dd"))
                                //{
                                //    strsql = "select a.KU_MAPGD,left(a.KU_MADP,6) MAXA,d.TEN TENXA,a.KU_MATO,(select TO_TENTT from HSTO where TO_MATO=a.KU_MATO) TENTT"
                                //            + " ,c.KH_MAKH,c.KH_TENKH,a.KU_SOKU + char(39) SOKU,a.KU_DNOTHAN,a.KU_DNOQHAN,a.KU_DNOKHOANH,a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO "
                                //            + " ,(case when b.PL_NGUONVON_BS = '01' then 'QG' else 'NHCS' end) NGUON from HSKU a, PLKT b,HSKH c, DMXA d "
                                //            + " where a.KU_NGAYBC = '" + ng + "' and a.KU_MAPGD = '" + pos + "' and a.KU_SOKU = b.PL_SOKU and a.KU_CHTRINH = '03' "
                                //            + " and a.KU_TTMONVAY <> 'CLOSE' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 "
                                //            + " and a.KU_MAKH = c.KH_MAKH and left(a.KU_MADP, 6)= d.MA order by b.PL_NGUONVON_BS,d.MA,a.ku_mato";
                                //}
                                //else
                                //{
                                    strsql = "select a.KU_MAPGD,left(a.KU_MADP,6) MAXA,d.TEN TENXA,a.KU_MATO,(select TO_TENTT from HSTO where TO_MATO=a.KU_MATO) TENTT"
                                            + " ,c.KH_MAKH,c.KH_TENKH,a.KU_SOKU + char(39) SOKU,a.KU_NGAYVAY,a.KU_NGAYDHAN_1,a.KU_NGAYDHAN_2,a.KU_NGAYDHAN_3,a.KU_DNOTHAN,a.KU_DNOQHAN,a.KU_DNOKHOANH,a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO "
                                            + " ,(case when b.PL_NGUONVON_BS = '01' then 'QG' else 'NHCS' end) NGUON from HSCV_DAILY a, PLKT b,HSKH c, DMXA d " 
                                            +" where a.KU_NGAYBC = '"+ng+"' and a.KU_MAPGD = '"+pos+"' and a.KU_SOKU = b.PL_SOKU and a.KU_CHTRINH = '03' " 
                                            + " and a.KU_NGUONVON='1' and a.KU_TTMONVAY <> 'CLOSE' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 "
                                            + " and a.KU_MAKH = c.KH_MAKH and left(a.KU_MADP, 6)= d.MA order by b.PL_NGUONVON_BS,d.MA,a.ku_mato";
                                //}

                                //MessageBox.Show(strsql);
                                dt = cls.LoadDataText(strsql);
                              //  FileName = Thumuc + "\\" + pos + "_Sao kê GQLV_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;

                            case "M26":
                                dt = cls.LoadLdbf("usp_BC_LSUAT", bien, giatri, thamso);
                                //if (dt.Rows.Count > 0)
                                //    FileName = Thumuc + "\\" + pos + "_LAISUAT_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                //else
                                //    MessageBox.Show("Không có dữ liệu", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                                break;
                            case "M27":
                                dt = cls.LoadLdbf("usp_NHOMNO_NHNN", bien, giatri, thamso);
                                //if (dt.Rows.Count > 0)
                                //    FileName = Thumuc + "\\" + pos + "_PL02_NHNN_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                //else
                                //    MessageBox.Show("Không có dữ liệu", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                                break;
                            case "M28":
                                dt = cls.LoadLdbf("usp_PNKT_NHNN", bien, giatri, thamso);
                                //if (dt.Rows.Count > 0)
                                //    FileName = Thumuc + "\\" + pos + "_PL04_PNKT_NHNN_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                //else
                                //    MessageBox.Show("Không có dữ liệu", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                                break;
                            case "M30":
                                //if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") == lastMonth.ToString("yyyy-MM-dd"))
                                //{
                                //    strsql = "select a.KU_MAPGD,left(a.KU_MADP,6) MAXA,d.TEN TENXA,a.KU_MATO,(select TO_TENTT from HSTO where TO_MATO=a.KU_MATO) TENTT"
                                //            + " ,c.KH_MAKH,c.KH_TENKH,a.KU_SOKU + char(39) SOKU,a.KU_DNOTHAN,a.KU_DNOQHAN,a.KU_DNOKHOANH,a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO "
                                //            + " ,(case when b.PL_NGUONVON_BS = '01' then 'QG' else 'NHCS' end) NGUON from HSKU a, PLKT b,HSKH c, DMXA d "
                                //            + " where a.KU_NGAYBC = '" + ng + "' and a.KU_MAPGD = '" + pos + "' and a.KU_SOKU = b.PL_SOKU and a.KU_CHTRINH = '03' "
                                //            + " and a.KU_TTMONVAY <> 'CLOSE' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 "
                                //            + " and a.KU_MAKH = c.KH_MAKH and left(a.KU_MADP, 6)= d.MA order by b.PL_NGUONVON_BS,d.MA,a.ku_mato";
                                //}
                                //else
                                //{
                                strsql = "select left(a.CS_MADP,6) MAXA,b.TEN TENXA,sum(a.CS_SODU_TK) SODU from CASA a,DMXA b "
                                          +" where a.CS_NGAYBC = '"+ng+"' and left(a.CS_MADP, 4)= right("+pos+", 4) and left(a.CS_MADP, 6)= b.MA and right(b.MA, 2)<> '00' and a.CS_MATO is null "
                                          + " and a.CS_SP_TK='105' and a.CS_TTSO_TK='A' group by left(a.CS_MADP, 6),b.TEN order by left(a.CS_MADP, 6)";
                                //}

                                //MessageBox.Show(strsql);
                                dt = cls.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_Sao kê CASA 105 đã gỡ mã tổ_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M32":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") == lastMonth.ToString("yyyy-MM-dd"))
                                    strsql = "with lst1 as ( select a.ku_mapgd, a.KU_MAKH, a.ku_dnothan DNTH, a.ku_dnoqhan DNQH, a.ku_dnokhoanh DNKH, (case when a.KU_NGAYDHAN_3 <= '"+den_ng+"' and a.KU_DNOTHAN > 0 then a.KU_DNOTHAN else 0 end ) DNDH "
                                                        +" from hsku a where a.ku_ngaybc = '"+ng+"' and a.ku_ttmonvay <> 'CLOSE' and a.ku_dnothan + a.ku_dnoqhan + a.ku_dnokhoanh > 0 ) "
                                                        +" select b.po_ten N'Tên PGD',sum(a.DNTH) N'Dư nợ trong hạn',sum(a.DNQH) N'Dư nợ quá hạn',sum(a.DNKH) N'Dư nợ khoanh',sum(a.DNDH) N'Dư nợ đến hạn', "
                                                        +" sum(case when a.DNDH > 0 then 1 else 0 end) N'Số hộ đến hạn' from lst1 a,DMPOS b where a.KU_MAPGD = b.PO_MA group by b.PO_TEN";
                                else
                                    strsql = "with lst1 as ( select a.ku_mapgd, a.KU_MAKH, a.ku_dnothan DNTH, a.ku_dnoqhan DNQH, a.ku_dnokhoanh DNKH, (case when a.KU_NGAYDHAN_3 <= '" + den_ng + "' and a.KU_DNOTHAN > 0 then a.KU_DNOTHAN else 0 end ) DNDH "
                                                        + " from hscv_daily a where a.ku_ngaybc = '" + ng + "' and a.ku_ttmonvay <> 'CLOSE' and a.ku_dnothan + a.ku_dnoqhan + a.ku_dnokhoanh > 0 ) "
                                                        + " select b.po_ten N'Tên PGD',sum(a.DNTH) N'Dư nợ trong hạn',sum(a.DNQH) N'Dư nợ quá hạn',sum(a.DNKH) N'Dư nợ khoanh',sum(a.DNDH) N'Dư nợ đến hạn', "
                                                        + " sum(case when a.DNDH > 0 then 1 else 0 end) N'Số hộ đến hạn' from lst1 a,DMPOS b where a.KU_MAPGD = b.PO_MA group by b.PO_TEN";

                                dt = cls.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_Tổng hợp nợ đến hạn_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M33":
                                strsql = "select a.CS_MAPGD N'Mã POS',a.CS_MAKH N'Mã khách hàng',a.CS_TENTK N'Tên TK',a.CS_SP_TK N'Sản phẩm',a.CS_SODU_TK N'Số dư' from CASA_DAILY a "
                                        +" where a.CS_NGAYBC = '"+ng+"' and a.CS_SP_TK in ('102','103','104') and a.CS_TTSO_TK = 'A' and a.CS_SODU_TK < 50000 and a.CS_MAPGD = '"+pos+"' order by a.CS_SP_TK,a.CS_MAKH ";
                                dt = cls.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_CASA_102_103_104_dưới 50 Ngàn_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M34":
                                //strsql = "with lst1 as ( select distinct to_char(NGAY_NHAP, 'DD/MM/YYYY') NGAY_NHAP, NGUOI_NHAP, D5 DIEM_GDX, D6 NGAY_GDX "
                                //          +" from dulieu_nt where khoa = 'GSCMR_001' and to_char(NGAYBC, 'MM/YYYY') = '"+ dtpNgay.SelectedDate.Value.ToString("MM/yyyy") + "' "
                                //          +" ) select c.PO_TEN,b.CVI_TXN_POINT_ID DIEM_GDX, b.TPI_DESC TENXA, a.NGAY_NHAP,a.NGUOI_NHAP,d.ND_TEN,a.NGAY_GDX "
                                //          +" from lst1 a, TXN_POINT_INFO b,DMPOS c, ng_dung d "
                                //          +" where a.DIEM_GDX = B.CVI_TXN_POINT_ID and substr(a.DIEM_GDX, 5, 4)= substr(c.PO_MA, 3, 4) and a.NGUOI_NHAP = d.ND_MA "
                                //          +" order by b.CVI_TXN_POINT_ID";
                                //dt = ora.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_CAMERA_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                strsql = "with lst1 as (select * from LUU_CAMERA where NAM = '"+nam+"' and THANG = '"+thang+"')" 
                                           +" select c.PO_MA,c.PO_TEN,b.CVI_TXN_POINT_ID DIEM_GDX, b.TPI_DESC TENXA, a.NGUOI_NHAP,d.ND_TEN,b.TPI_DATE,D3 DAT, D4 LYDO "
										   +" ,(case when a.D3 = 0 and a.D4 = '' then N'Kiểm tra lại không đạt mà không có lý do' else '' end) GHICHU "
                                           +" from lst1 a, TXN_POINT_INFO b,DMPOS c, ng_dung d "
                                           +" where a.THUTU = 1 and a.CVI_TXN_POINT_ID = B.CVI_TXN_POINT_ID  and a.NGUOI_NHAP = d.ND_MA  and SUBSTRING(a.CVI_TXN_POINT_ID, 5, 4)= right(c.PO_MA, 4) order by b.CVI_TXN_POINT_ID";
                                dt = cls.LoadDataText(strsql);
                                break;
                            case "M35":
                                strsql = "with lst1 as ( select b.KH_LOAIKH,b.KH_GIOITINH LOAI,a.KU_HTHUCVAY,substring(a.KU_SPRD_CD,4,1) LOAI_TH,sum(a.KU_DNOTHAN+a.KU_DNOQHAN+a.KU_DNOKHOANH) DUNO "
                                        +" from HSKU a,HSKH b where a.KU_NGAYBC = '"+ng+"' and a.KU_TTMONVAY <> 'CLOSE' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 and a.KU_MAKH = b.KH_MAKH "
                                        +" group by b.KH_LOAIKH,b.KH_GIOITINH,a.KU_HTHUCVAY,substring(a.KU_SPRD_CD, 4, 1) "
                                        +" ), lst2 as (select b.TEN,a.* from lst1 a left join LOAI_KH b on a.LOAI = b.MA and a.KH_LOAIKH not in ('11','12')), lst3 as ( "
                                        +" select(case when a.KH_LOAIKH in ('11','12') then N'Hộ KD, Cá nhân' else a.TEN end) LOAIKH,KU_HTHUCVAY,LOAI_TH,DUNO from lst2 a "
                                        +" ), lst4 as(select a.LOAIKH,a.LOAI_TH,SUM(DUNO) DUNO from lst3 a group by a.LOAIKH,a.LOAI_TH),lst5 as(select * from(select * from lst4) src "
                                        +" pivot (sum(DUNO)  for LOAI_TH in ([S], [M], [L])) piv "
                                        +" ) select LOAIKH N'Loại KH',isnull(S, 0) + isnull(M, 0) + isnull(L, 0) N'Tổng dư nợ', isnull(S, 0) N'Ngắn hạn', isnull(M, 0) N'Trung hạn', isnull(L, 0) N'Dài hạn' from lst5 ";
                                dt = cls.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_Dư nợ theo loại khách hàng_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M36":
                                strsql = "with lst1 as ( "
                                           +" select a.KU_MAPGD, a.KU_MAQD, sum(a.KU_DNOTHAN) DUNO from HSKU a where a.KU_NGAYBC = '"+ng+ "' and a.KU_DNOTHAN > 0 and a.KU_TTMONVAY <> 'CLOSE' and a.KU_NGAYDHAN_3 <= '" + den_ng+"' "
                                           +" group by a.KU_MAPGD, a.KU_MAQD ), lst2 as ( SELECT * FROM lst1 "
                                           +" PIVOT (MAX(DUNO) FOR KU_MAQD in ([01],[02],[03],[04],[06],[07],[09],[10],[11],[15],[16],[17],[19],[22])) AS pvt ) "
                                            +" select '"+ng+"' N'Ngày Số liệu','"+den_ng+"' N'Đến ngày',b.PO_TEN N'Đơn vị',isnull(a.[01], 0) N'Hộ Nghèo',isnull(a.[02], 0) N'HSSV',isnull(a.[03], 0) N'GQVL' "
                                            +",isnull(a.[04], 0) N'LĐXK',isnull(a.[06], 0) N'NSVSMT',isnull(a.[07], 0) N'Nhà ở' "
                                            +" ,isnull(a.[09], 0) N'DTTS QĐ4',isnull(a.[10], 0) N'Hộ SXKD',isnull(a.[11], 0) N'DTTS QĐ32' "
                                            +" ,isnull(a.[15], 0) N'TN VKK',isnull(a.[16], 0) N'LĐXK QĐ71',isnull(a.[17], 0) N'DTTS QĐ755' "
                                            +",isnull(a.[19], 0) N'Hộ Cân nghèo',isnull(a.[22], 0) N'Hộ mới thoát Nghèo' from lst2 a,DMPOS b where a.KU_MAPGD = b.PO_MA order by a.KU_MAPGD ";
                                //MessageBox.Show(ng.ToString()+"     "+Enddayofyear.ToString()+"         "+den_ng.ToString());
                                dt = cls.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_Nợ đến hạn theo chương trình_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M37":
                                strsql = "with lst1 as ( select  substr(a.ghichu_2, length(ghichu_2) - 16, 16) thamch, a.* from hsbt a where substr(a.tk, 1, 4) = '9910' and a.NOCO = 'D' and a.KSV <> 'SYSTEM' and a.MAPGD='"+pos+"' )"
                                        +" select b.mapgd,b.tk,b.sbt,b.mod_cd,b.ngaygd,b.sotien,b.ghichu_1,b.ghichu_2,b.gdv,(select IU_TEN from i_user where IU_MA = b.GDV) TEN_GDV "
                                        +" ,b.ksv,(select IU_TEN from i_user where IU_MA = b.KSV) TEN_KSV from lst1 b order by b.mapgd,b.ngaygd,b.sbt";
                                dt = ora.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_Các lệnh thanh thanh toán_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M38":
                                strsql = "with lst1 as ( select  substr(a.ghichu_2, length(ghichu_2) - 16, 16) thamch, a.* from hsbt a where tk='9910047044' and a.NOCO = 'D' and a.KSV <> 'SYSTEM' and a.MAPGD='" + pos + "' )"
                                        + " select b.mapgd,b.tk,b.sbt,b.mod_cd,b.ngaygd,b.sotien,b.ghichu_1,b.ghichu_2,b.gdv,(select IU_TEN from i_user where IU_MA = b.GDV) TEN_GDV "
                                        + " ,b.ksv,(select IU_TEN from i_user where IU_MA = b.KSV) TEN_KSV from lst1 b order by b.mapgd,b.ngaygd,b.sbt";
                                dt = ora.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_Lệnh điều chuyển vốn huyện với tỉnh_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M39":
                                strsql = "with lst1 as ( select  substr(a.ghichu_2, length(ghichu_2) - 16, 16) thamch, a.* from hsbt a where tk_no='9910027048' and a.NOCO = 'D' and a.KSV <> 'SYSTEM' and a.MAPGD='" + pos + "' )"
                                        + " select b.mapgd,b.tk,b.sbt,b.mod_cd,b.ngaygd,b.sotien,b.ghichu_1,b.ghichu_2,b.gdv,(select IU_TEN from i_user where IU_MA = b.GDV) TEN_GDV "
                                        + " ,b.ksv,(select IU_TEN from i_user where IU_MA = b.KSV) TEN_KSV from lst1 b order by b.mapgd,b.ngaygd,b.sbt";
                                dt = ora.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_Lệnh điều chuyển vốn tỉnh với huyện_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M40":
                                strsql = "with lst0 as ( select distinct KU_MAKH, KU_MATO from HSCV_DAILY where KU_NGAYBC = '"+ng+"' and KU_TTMONVAY <> 'CLOSE' ), "
                                    +" lst1 as ( select LEFT(b.KH_MADP, 4) MAPOS,LEFT(b.KH_MADP, 6) MAXA,b.KH_MADP MATHON, a.KU_MATO,b.KH_MAKH,b.KH_TENKH,'0' + SUBSTRING(b.KH_MOBILE, 6, LEN(b.KH_MOBILE)) MOBILE "
                                    +" from lst0 a, HSKH b where  a.KU_MAKH = b.KH_MAKH and b.KH_MOBILE is not null and LEN(b.KH_MOBILE) > 5 and b.KH_TTRANG = 'A' "
                                    +" ),lst2 as ( select a.MOBILE,COUNT(a.MOBILE) DEM from lst1 a group by a.MOBILE having COUNT(a.MOBILE) > 1 )"
                                    +" select a.MAPOS N'POS',(select TEN from DMHUYEN where MA = a.MAPOS) N'Tên PGD',a.MAXA N' Mã Xã' "
                                    +" ,(select TEN from DMXA where MA = a.MAXA) N'Tên Xã',a.MATHON N'Mã Thôn',(select TEN from DMTHON where MA = a.MATHON) N'Tên Thôn' "
                                    +" ,a.KU_MATO N'Mã tổ',(select TO_TENTT from HSTO where TO_MATO = a.KU_MATO) N'Tên TT' "
                                    +" ,KH_MAKH N'Mã KH', a.KH_TENKH N'Tên KH',a.MOBILE N'Điện Thoại' from lst1 a,lst2 b where a.MOBILE = b.MOBILE "
                                    +" order by a.MATHON,a.MOBILE";
                                dt = cls.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_Số điện thoại trùng_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M41":
                                strsql = "select (select TEN from DMHUYEN where MA=left(CVI_COMMUNE_ID,4)) N'Đơn vị',CVI_COMMUNE_ID N'Mã Xã' "
                                        +",(select TEN from dmxa where MA = CVI_COMMUNE_ID) N'Tên Xã',(select c.TEN_CBTD from CBTD c, DMXA d where c.CMT_CBTD = d.CMT_CBTD and d.MA = CVI_COMMUNE_ID) N'Tên CBTD' "
                                        +",isnull([06],'') N06,isnull([07],'') N07,isnull([08],'') N08,isnull([09],'') N09,isnull([10],'') N10,isnull([11],'') N11 "
                                        +",isnull([12],'') N12,isnull([13],'') N13,isnull([14],'') N14,isnull([15],'') N15,isnull([16],'') N16,isnull([17],'') N17,isnull([18],'') N18 "
                                        +",isnull([19],'') N19,isnull([20],'') N20,isnull([21],'') N21,isnull([22],'') N22 from TXN_POINT_INFO "
                                        +"pivot (max(CVI_TXN_POINT_ID) for TPI_DATE in ([06],[07],[08],[09],[10],[11],[12],[13],[14],[15],[16],[17] "
                                        +",[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30])) as p order by CVI_COMMUNE_ID";
                                dt = cls.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_Điểm giao dịch xã_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M42":
                                if (pos == "003000")
                                    strsql = "select pln_mapgd MAPOS,(select PO_TEN from dmpos a where a.PO_MA=PLN_MAPGD) TEN_POS "
                                        + ",count(distinct substr(PLN_MADP, 1, 6)) TONG_SO_XA,count(distinct PLN_MADP) TONG_SO_THON "
                                        +",count(distinct PLN_MATO) TONG_SO_TO "
                                        +",count(distinct PLN_MAKH) SOKH,count(pln_soku) TONG_SO_MON "
                                        +",sum(pln_dnothan + pln_dnoqhan + pln_dnokhoanh) TONG_DU_NO,sum(PLN_TONGLAI_TT) LAITHU,sum(PLN_TONGLAITON) LAI_TON "
                                        +",sum(case when PLN_TRANGTHAI = 'S' then 1 else 0 end) SO_MON_DC "
                                        + " from PLN_KNTN_CL where pln_dnothan+pln_dnoqhan+pln_dnokhoanh>0 and PLN_TT_MONVAY<>'CLOSE' and pln_ngaybc = to_date(" + "'" + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") + "'" + ", " + "'dd/MM/yyyy" + "') and PLN_TT_MONVAY<>'CLOSE' group by pln_mapgd ";
                                else
                                    strsql = "select pln_mapgd, (select PO_TEN from dmpos a where a.PO_MA=PLN_MAPGD) TENPOS " 
                                            +",substr(PLN_MADP, 1, 6) MA_XA,(select TEN from DMXA a where a.MA = substr(PLN_MADP, 1, 6)) TEN_XA "
                                            +",count(distinct PLN_MADP) SOTHON "
                                            +",count(distinct PLN_MATO) SOTO "
                                            +",count(distinct PLN_MAKH) SOKH,count(pln_soku) somon "
                                            +",sum(pln_dnothan + pln_dnoqhan + pln_dnokhoanh) duno,sum(PLN_TONGLAI_TT) LAITHU,sum(PLN_TONGLAITON) LAITON "
                                            +",sum(case when PLN_TRANGTHAI = 'S' then 1 else 0 end) somon_dc "                                           
                                            + " from PLN_KNTN_CL where pln_dnothan+pln_dnoqhan+pln_dnokhoanh>0 and PLN_TT_MONVAY<>'CLOSE' and PLN_MAPGD='" + pos+"' and pln_ngaybc = to_date(" + "'" + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") + "'" + ", " + "'dd/MM/yyyy" + "') and PLN_TT_MONVAY<>'CLOSE' group by pln_mapgd,substr(PLN_MADP, 1, 6) ";
                                string strstr = "select pln_mapgd, (select PO_TEN from dmpos a where a.PO_MA=PLN_MAPGD) TENPOS "
                                        + ",substr(PLN_MADP, 1, 6) MA_XA,(select TEN from DMXA a where a.MA = substr(PLN_MADP, 1, 6)) TEN_XA "
                                        + ",count(distinct PLN_MADP) SOTHON "
                                        + ",count(distinct PLN_MATO) SOTO "
                                        + ",count(distinct PLN_MAKH) SOKH,count(pln_soku) somon "
                                        + ",sum(pln_dnothan + pln_dnoqhan + pln_dnokhoanh) duno,sum(PLN_TONGLAI_TT) LAITHU,sum(PLN_TONGLAITON) LAITON "
                                        + ",sum(case when PLN_TRANGTHAI = 'S' then 1 else 0 end) somon_dc "
                                        + " from PLN_KNTN_CL where pln_dnothan+pln_dnoqhan+pln_dnokhoanh>0 and PLN_TT_MONVAY<>'CLOSE' and pln_ngaybc = to_date(" + "'" + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") + "'" + ", " + "'dd/MM/yyyy" + "') and PLN_TT_MONVAY<>'CLOSE' group by pln_mapgd,substr(PLN_MADP, 1, 6) ";
                                strstr1 = "select pln_mapgd, (select PO_TEN from dmpos a where a.PO_MA=PLN_MAPGD) TENPOS "
                                        + ",substr(PLN_MADP, 1, 6) MA_XA,(select TEN from DMXA a where a.MA = substr(PLN_MADP, 1, 6)) TEN_XA,pln_mato,(select to_tentt from hsto a where a.to_mato=pln_mato) TOTRUONG "
                                        + ",count(distinct PLN_MADP) SOTHON "
                                        + ",count(distinct PLN_MATO) SOTO "
                                        + ",count(distinct PLN_MAKH) SOKH,count(pln_soku) somon "
                                        + ",sum(pln_dnothan + pln_dnoqhan + pln_dnokhoanh) duno,sum(PLN_TONGLAI_TT) LAITHU,sum(PLN_TONGLAITON) LAITON "
                                        + ",sum(case when PLN_TRANGTHAI = 'S' then 1 else 0 end) somon_dc "
                                        + " from PLN_KNTN_CL where pln_dnothan+pln_dnoqhan+pln_dnokhoanh>0 and PLN_TT_MONVAY<>'CLOSE' and pln_ngaybc = to_date(" + "'" + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") + "'" + ", " + "'dd/MM/yyyy" + "') and PLN_TT_MONVAY<>'CLOSE' group by pln_mapgd,substr(PLN_MADP, 1, 6),pln_mato ";

                                //MessageBox.Show(strsql);
                                dt = ora.LoadDataText(strsql);
                                var dtall = ora.LoadDataText(strstr);
                                var dtto = ora.LoadDataText(strstr1);
                               // FileName = Thumuc + "\\" + pos + "_Thống kê đối chiếu phân loại nợ_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                                FileName1 = Thumuc + "\\" + pos + "_Tổng hợp xã - Thống kê đối chiếu phân loại nợ_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                                FileName2 = Thumuc + "\\" + pos + "_Tổng hợp tổ - Thống kê đối chiếu phân loại nợ_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                                str.ExportToExcel(dtall, FileName1);
                                //str.ExportToExcel(dt, FileName);
                                str.ExportToExcel(dtto, FileName2);
                                break;
                            case "M43":
                                strsql = "with lst1 as "
                                          + "( "
                                          + " select row_number() over(partition by a.ku_mato order by a.ku_mato desc) STT,a.KU_MAPGD,LEFT(a.KU_MADP, 6) MAXA,a.KU_MADP MATHON, a.KU_MATO,a.KU_MAQD,SUM(a.KU_DNOTHAN) DNTH "
                                          + " ,SUM(a.KU_DNOQHAN) DNQH,sum(a.KU_DNOKHOANH) DNKH,sum(a.KU_LAITHAN + a.KU_LAIQHAN) LAITHU,SUM(a.KU_A_LAI_TT) A_LAITHU,sum(a.KU_LAITONTHAN + a.KU_LAITONQHAN) LAITON "
                                          + ",SUM(a.KU_LAI_DT) LAIDUTHU "
                                          + " from HSCV_DAILY a where a.KU_MAPGD='" + pos + "' and a.KU_NGAYBC = '" + ng + "' and a.KU_MATO is not null "
                                          + " group by a.KU_MAPGD,LEFT(a.KU_MADP, 6) ,a.KU_MADP,a.KU_MATO,a.KU_MAQD "
                                          + " ), lst2 as "
                                          + " ( "
                                          + " select a.CS_MATO,SUM(a.CS_A_GUITK) A_GUI,SUM(a.CS_A_RUTTK) A_RUT,SUM(a.CS_SODU_TK) SODU "
                                          + " from CASA_DAILY a where a.CS_MAPGD='" + pos + "' and a.CS_NGAYBC = '" + ng + "' and a.CS_SP_TK = '105' and a.CS_TTSO_TK <> 'C' and a.cs_MATO is not null "
                                          + " group by a.CS_MATO "
                                          + " ) "
                                          + "  select a.STT,a.KU_MAPGD POS,(select PO_TEN from DMPOS where PO_MA = a.KU_MAPGD) N'Đơn vị' "
                                          + "  ,a.MAXA N'Mã xã',(select TEN from DMXA where MA = a.MAXA) N'Tên Xã',a.MATHON N'Mã Thôn' "
                                          + "  ,(select TEN from DMTHON where MA = a.MATHON) N'Tên Thôn',a.KU_MATO N'Mã Tổ' "
                                          + "  ,(select TO_TENTT from HSTO where TO_MATO = a.KU_MATO) N'Tổ Trưởng',a.KU_MAQD N'Chương trình' "
                                          + "  ,(select GIATRI from DMKHAC where KHOA_1 = '07' and KHOA_2 = a.KU_MAQD) N'Tên CHTR' "
                                          + "  ,a.DNTH N'Dư nợ TH',a.DNQH N'Dư nợ QH',a.DNKH N'Dư nợ KH',a.A_LAITHU N'Lãi thu trong năm',a.LAITON N'Lãi Tồn' "
                                          + "  ,a.LAIDUTHU N'Lãi dự thu',isnull((select SODU from lst2 where CS_MATO = a.KU_MATO and a.STT = 1),0) N'Dư TGTK Tổ' "
                                          + "  from lst1 a order by a.MAXA,a.KU_MATO,a.STT,a.KU_MAQD";
                                dt = cls.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_Số liệu tổng hợp theo tổ_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M44":
                                strsql = "with lst1 as ( select  substr(a.ghichu_2, length(ghichu_2) - 16, 16) thamch, a.* from hsbt a where ghichu_2='VLT_TO_XCH' and NOCO='C' and a.MAPGD='" + pos + "' )"
                                        + " select b.mapgd,b.tk,b.sbt,b.mod_cd,b.ngaygd,b.sotien,b.ghichu_1,b.ghichu_2,b.gdv,(select IU_TEN from i_user where IU_MA = b.GDV) TEN_GDV "
                                        + " ,b.ksv,(select IU_TEN from i_user where IU_MA = b.KSV) TEN_KSV from lst1 b order by b.mapgd,b.ngaygd,b.sbt";
                                dt = ora.LoadDataText(strsql);
                              //  FileName = Thumuc + "\\" + pos + "_Nộp tiền vào NHNo_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;

                            case "M45":
                                strsql = "with lst1 as ( "
                                            +" select a.KU_MAPGD, left(a.KU_MADP,6) MAXA, a.KU_SOKU, a.KU_GNGAN, a.KU_A_GNGAN GN_NAM, a.KU_TNTH + a.KU_TNQH THUNO "
                                            +" , a.KU_A_TNTHAN + a.KU_A_TNQHAN + a.KU_A_TNKHOANH A_THUNO, a.KU_DNOTHAN, a.KU_DNOQHAN, a.KU_DNOKHOANH, a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO "
                                            +" , b.SV_LOAIHDT, (select GIATRI from dmkhac where khoa_1 = '02' and KHOA_2 = b.SV_LOAIHDT) LOAIDT "
                                            +" ,b.SV_LOAIHCS ,b.SV_HEDTAO,(select GIATRI from dmkhac where khoa_1 = '03' and KHOA_2 = b.SV_HEDTAO) HEDT "
                                            +" ,b.SV_NGANHDT,(select GIATRI from dmkhac where khoa_1 = '04' and KHOA_2 = b.SV_NGANHDT) NGANHDT "
                                            +" ,b.SV_DTSV,(select GIATRI from dmkhac where khoa_1 = '05' and KHOA_2 = b.SV_DTSV) DTSV,b.SV_CMT_SV "
                                            +" from HSKU a, HSSV b, DMXA c "
                                            +" where a.KU_NGAYBC = '"+ng+"' and a.KU_CHTRINH = '02' and a.KU_TTMONVAY <> 'CLOSE' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 "
                                            + " and a.KU_SOKU = b.SV_SOKU and left(a.KU_MADP,6)=c.MA and c.XAPHUONG='T') "
                                            + " select a.KU_MAPGD POS,(select po_ten from dmpos where po_ma = a.ku_mapgd) N'Tên POS' "
                                            +" ,a.SV_HEDTAO N'Mã hệ ĐT',a.HEDT N'Tên hệ ĐT',COUNT(distinct a.SV_CMT_SV) N'Tổng số SV' "
                                            +" ,count(distinct(case when a.gn_nam > 0 then a.sv_cmt_sv end)) N'Số SV GN trong năm' "
                                            +" ,SUM(a.KU_GNGAN) N'Tổng số GN',SUM(a.GN_NAM) N'Tổng số GN trong năm',SUM(a.THUNO) N'Tổng thu nợ' "
                                            +" ,SUM(a.A_THUNO) N'tổng thu nợ trong năm',sum(a.DUNO) N'Dư nợ' from lst1 a group by a.KU_MAPGD,a.SV_HEDTAO,a.HEDT "
                                            +" order by a.SV_HEDTAO,a.KU_MAPGD ";
                                dt = cls.LoadDataText(strsql);
                               // FileName = Thumuc + "\\" + pos + "_Tổng hợp dư nợ sinh viên_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M46":
                                strsql = "with lst1 as"
                                            +" (select '00' + LEFT(b.KH_MADP, 4) MAPOS, LEFT(b.KH_MADP, 6) MAXA, b.KH_MADP MATHON, b.KH_MAKH, b.KH_TENKH, '0' + SUBSTRING(b.KH_MOBILE, 6, LEN(b.KH_MOBILE)) MOBILE "
                                            +" from HSKH b where b.KH_MOBILE is not null and LEN(b.KH_MOBILE) > 5 and b.KH_TTRANG = 'A' "
                                            +" ), lst2 as (select a.MAPOS,a.MOBILE,COUNT(a.MOBILE) DEM from lst1 a group by a.MAPOS,a.MOBILE having COUNT(a.MOBILE) > 1 ),lst3 as ( "
                                            +" select a.MAPOS,SUM(DEM) DEM from lst2 a group by MAPOS ) "
                                            +" select a.mapos N'Mã POS', (select po_ten from DMPOS where po_ma = a.mapos) N'Tên POS',a.dem N'SĐT Trùng' from lst3 a order by MAPOS";
                                dt = cls.LoadDataText(strsql);
                                //FileName = Thumuc + "\\" + pos + "Tổng Hợp_Số điện thoại trùng_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M47":
                                strsql = "select a.PLN_MAPGD,LEFT(a.PLN_MADP,6) N'Mã Xã',(select TEN from DMXA where MA=LEFT(a.PLN_MADP,6)) N'Tên xã' "
                                        +" ,a.PLN_MATO N' Mã tổ',a.PLN_TENTT N'Tên tổ',a.PLN_MAKH N'Mã KH',a.PLN_TENKH N'Tên KH',char(39)+a.PLN_SOKU N'Số KU',a.PLN_DNOTHAN N'Dư nợ TH',a.PLN_DNOQHAN N'Dư nợ QH',a.PLN_DNOKHOANH N'Dư nợ KH' "
                                        +" ,a.PLN_K_KNTN_SD01 N'Đủ ĐK xử lý nợ theo QĐ',a.PLN_K_KNTN_SD02 N'LĐNN bị rủi ro',a.PLN_K_KNTN_SD03 N'Người vay đi tù' "
                                        +" ,a.PLN_K_KNTN_SD04 N'Bỏ đi khỏi nơi cư trú',a.PLN_K_KNTN_SD05 N'SXKD thua lỗ',a.PLN_K_KNTN_SD06 N'Hộ GĐ bị rủi ro' "
                                        +" ,a.PLN_K_KNTN_SD07 N'RR không làm hồ sơ kịp thời',a.PLN_K_KNTN_SD08 N'Không có người nhận nợ',a.PLN_K_KNTN_SD09 N'KH không nhận nợ' "
                                        +" ,a.PLN_K_KNTN_SD10 N'Tham ô, chiếm dụng',a.PLN_K_KNTN_SD11 N'Nguyên nhân khác',a.PLN_LAITONQHAN + a.PLN_LAITONTHAN N'Tổng lãi chưa thu' "
                                         +" from PLN_KNTN_CL a "
                                        +" where a.PLN_NGAYBC = '"+ng+"' and a.PLN_TRANGTHAI = 'S' and a.PLN_TT_MONVAY <> 'CLOSE' and a.PLN_K_KNTN_SODU > 0"
                                        +" order by a.PLN_MADP,a.PLN_MATO,a.PLN_MAKH";
                                dt = cls.LoadDataText(strsql);
                                //FileName = Thumuc + "\\" + pos + "Sao kê nợ không có khả năng thu hồi theo đối chiếu, phân loại nợ_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M48":
                                strsql = "with lst1 as ( "
                                            + " select a.* from HSCV_DAILY a where a.KU_NGAYBC = '" + ng + "' and a.KU_LAITONTHAN + a.KU_LAITONQHAN > 0 and a.KU_TTMONVAY <> 'CLOSE' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 and a.KU_MAPGD = '"+pos+"' "
                                            + " ), lst2 as ( "
                                            + " select a.* from CASA_DAILY a where a.CS_NGAYBC = '" + ng + "' and a.CS_SP_TK = '105' and a.CS_SODU_TK > 0 and a.CS_MAPGD = '"+pos+"' "
                                            + " ) select LEFT(a.KU_MADP, 6) MAXA,(select ten from dmxa where ma = LEFT(a.KU_MADP, 6)) TENXA,a.KU_MATO,(select to_tentt from hsto where to_mato = a.ku_mato) TENTT,a.KU_MAKH,c.KH_TENKH "
                                            +" ,char(39) + a.KU_SOKU SOKU, a.KU_DNOTHAN, a.KU_DNOQHAN, a.KU_DNOKHOANH, a.KU_LAITONTHAN, a.KU_LAITONQHAN, b.CS_SODU_TK, (select GIATRI from DMKHAC where KHOA_1 = '07' and a.KU_MAQD = KHOA_2)  CHTRINH "
                                            +" from lst1 a, lst2 b,HSKH c where a.KU_MAKH = b.CS_MAKH and a.KU_MAKH = c.KH_MAKH order by a.KU_MADP,a.KU_MATO,a.KU_MAKH";
                                dt = cls.LoadDataText(strsql);
                                break;
                            case "M49":
                                strsql = "with lst1 as"
                                 +" (select * from HSCV_DAILY a where a.KU_NGAYBC = '"+ng+"' and a.KU_MAQD = '26' and a.KU_TTMONVAY <> 'CLOSE' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0) "
                                 +", lst2 as ( select a.CS_MAKH,a.CS_SO_TK,a.CS_M_GUITK,a.CS_M_RUTTK,a.CS_SODU_TK from CASA_DAILY a where a.CS_NGAYBC = '"+ng+"' and a.CS_SP_TK = '145' ) "
                                 +",lst3 as ( select c.KH_MAPGD POS, d.PO_TEN N'POS Tên',a.KU_MAKH N'Mã KH',c.KH_TENKH N'Tên KH',CHAR(39) + a.KU_SOKU N'Mã món vay',char(39) + b.CS_SO_TK N'Số TK', " +" a.KU_DNOTHAN DNTH, a.KU_DNOQHAN DNQH, a.KU_DNOKHOANH DNKH "
                                +" , a.KU_LAITONTHAN N'Lãi tồn TH', a.KU_LAITONQHAN N'Lãi tồn QH', (case when b.CS_M_GUITK > 0 then b.CS_M_GUITK else 0 end) N'Gửi Trong tháng', "
                                +" (case when b.CS_M_GUITK < 0 then b.CS_M_GUITK else 0 end) N'Chuyển trả nợ',b.CS_SODU_TK N'Số dư 145',a.KU_SOKU,a.KU_NGAYGNDT N'Ng giải ngân ĐT',a.KU_NGAYDHAN_3 N'Ng Đến hạn' from lst1 a,lst2 b, HSKH c,DMPOS d"
                                +" where a.KU_MAKH = b.CS_MAKH and a.KU_MAKH = c.KH_MAKH and a.KU_MAPGD = d.PO_MA ), lst4 as ("
                                    +" select a.* from KHTN a, (select min(KH_NGDHAN) NGMIN,KH_SOKU from KHTN group by KH_SOKU) b where a.KH_SOKU = b.KH_SOKU and a.KH_NGDHAN = b.NGMIN )"
                                +" select a.*,b.KH_NGDHAN N'Ng bắt đầu trả nợ',b.KH_GOCDHAN N'Số tiền trả nợ' from lst3 a left join lst4 b on a.KU_SOKU = b.KH_SOKU order by a.POS";
                                dt = cls.LoadDataText(strsql);
                                break;
                            case "M50":
                                strsql = "with lst1 as ( select a.KU_MAPGD, LEFT(a.KU_MADP, 6) MAXA, a.KU_MATO, (select TO_DVUT from HSTO where TO_MATO = a.KU_MATO) DVUT,SUM(a.KU_A_CHUYENQH) CHUYENQH "
	                                            +" ,SUM(a.KU_DNOQHAN) DNQH,sum(a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH) DUNO,SUM(case when a.KU_NGAY_TLAI > '"+LastMonthPre+"' and a.KU_MAQD in ('02','07','23','17','18','21','25') then 0  else a.KU_LAITONTHAN + a.KU_LAITONQHAN end) LAITON "
                                                +" from HSKU a where a.KU_NGAYBC = '"+Enddayofyear+"' and a.KU_TTMONVAY <> 'CLOSE' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 and a.KU_MATO is not null group by a.KU_MAPGD,LEFT(a.KU_MADP, 6),a.KU_MATO), lst2 as ("
                                                +" select a.KU_MAPGD,a.MAXA,a.DVUT,SUM(a.DUNO) DUNO,SUM(a.DNQH) DNQH,SUM(a.CHUYENQH) CHUYENQH,SUM(a.LAITON) LAITON from lst1 a  group by a.KU_MAPGD,a.MAXA,a.DVUT "
                                                +" ) select a.KU_MAPGD,(select PO_TEN from DMPOS where PO_MA = a.KU_MAPGD) TENPO,a.MAXA,(select TEN from DMXA where MA = a.MAXA) TENXA,a.DVUT,(select TENDV from DVUT where DVUT = a.DVUT) TEN_DVUT "
                                                +" ,a.DUNO,a.DNQH,a.CHUYENQH,a.LAITON from lst2 a where a.DNQH = 0 and a.CHUYENQH = 0 and a.LAITON = 0 order by a.MAXA,a.DVUT";
                                dt = cls.LoadDataText(strsql);
                                break;
                            case "M51":
                                //MessageBox.Show(lastDayOfTheYear+"       "+ EnddayofyearPre+"     "+Enddayofyear);
                                strsql = "select a.KU_MAPGD,a.KU_MANDT,a.KU_CAPQLV,(select GIATRI from DMKHAC where KHOA_1='19' and KHOA_2=a.KU_CAPQLV) TEN_CAPQLV "
                                        + ",(select to_dvut from hsto where to_mato = a.ku_mato) DVUT,(select c.GIATRI from DMKHAC c, hsto b where c.KHOA_1 = '17' and c.KHOA_2 = b.to_dvut and b.to_mato = a.ku_mato) TEN_DVUT, "
                                        +" (select DT_TENDT from HSDT where DT_MADT = a.KU_MANDT and DT_MAPGD = a.KU_MAPGD) TEN_NDT,LEFT(a.KU_MADP, 6) MAXA,(select TEN from dmxa where MA = LEFT(a.KU_MADP, 6)) TENXA,a.KU_MAKH,b.KH_TENKH,a.KU_MATO,(select to_tentt from hsto where TO_MATO = a.ku_mato) TENTT "
                                        +" ,char(39) + a.KU_SOKU SOKU, a.KU_NGAYVAY, a.KU_NGAYDHAN_1, a.KU_NGAYDHAN_2, a.KU_NGAYDHAN_3, a.KU_DNOTHAN, a.KU_DNOQHAN, a.KU_DNOKHOANH "
                                        +" from HSCV_DAILY a, HSKH b where a.KU_NGAYBC = '"+ng+"' and a.KU_CHTRINH = '03' and a.KU_TTMONVAY <> 'CLOSE' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 "
                                        +" and a.KU_MAKH = b.KH_MAKH order by a.KU_MANDT, b.KH_MADP, a.KU_MATO, b.KH_TENKH ";
                                dt = cls.LoadDataText(strsql);
                                break;
                            case "M52":
                                strsql = "select '"+ng+ "' NGAY,a.KU_NGUONVON,KU_MANDT,(select DT_TENDT from HSDT where DT_MADT=a.KU_MANDT and DT_MAPGD=a.KU_MAPGD) TEN_NDT,a.KU_MAPGD,a.KU_CHTRINH,(select GIATRI from DMKHAC where KHOA_1='07' and KHOA_2=a.KU_CHTRINH) TEN_CHTR"
                                         + ",sum(case when a.KU_A_GNGAN>0 then 1 else 0 end) SH_CV,sum(a.KU_A_GNGAN) GNGAN,sum(a.KU_A_TNTHAN+a.KU_A_TNQHAN+a.KU_A_TNKHOANH) THUNO,sum(a.KU_DNOTHAN+a.KU_DNOQHAN+a.KU_DNOKHOANH) "
                                         + " DUNO,sum(case when a.KU_DNOTHAN+a.KU_DNOQHAN+a.KU_DNOKHOANH>0 then 1 else 0 end) SH_DUNO from HSKU a where a.KU_NGAYBC = '" + ng+ "' and a.KU_NGUONVON = '2' group by a.KU_CHTRINH,a.KU_MAPGD,a.KU_NGUONVON,a.KU_MANDT order by a.KU_CHTRINH,a.KU_MANDT,a.KU_MAPGD  ";
                                dt = cls.LoadDataText(strsql);
                                break;
                            case "M53":
                                dt = cls.LoadLdbf("usp_NdhTheoThang", bien, giatri, thamso);
                                break;
                            case "M54":
                                strsql = "with lst1 as ("
                                           +" select left(a.ku_madp, 6) maxa, a.*, b.KH_TENKH, a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO from HSCV_DAILY a, HSKH b "
                                           +" where a.KU_NGAYBC = '"+ng+"' and a.KU_TTMONVAY <> 'CLOSE' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 and a.KU_CHTRINH in ('01', '09', '19') "
                                           +" and a.KU_MAKH = b.KH_MAKH ), lst2 as ( select a.ku_makh,sum(a.DUNO) DUNO from lst1 a group by a.ku_makh having sum(a.DUNO) > 50000000 ), lst3 as ("
                                           +" select a.KU_MAPGD,a.maxa,a.KU_MATO,a.KU_MAKH,a.KU_MAQD CHTR, sum(a.DUNO)DUNO , sum(case when a.KU_NGAYGNDT < '2019-03-01' then a.DUNO else 0 end) DUNO_TR "
                                           +" ,sum(case when a.KU_NGAYGNDT >= '2019-03-01' then a.DUNO else 0 end) VAY_BS  ,count(a.KU_SOKU) Z_SOKU from lst1 a, lst2 b where a.KU_MAKH = b.KU_MAKH group by a.KU_MAPGD,a.maxa,a.KU_MATO,a.KU_MAKH,a.KU_MAQD "
                                           +" ) select a.KU_MAPGD N'Mã POS',(select PO_TEN from DMPOS where PO_MA = a.KU_MAPGD) N'Tên POS' ,a.maxa N'Mã Xã',(select TEN from DMXA where MA = a.maxa) N'Tên Xã' "
                                           +" ,COUNT(a.KU_MAKH) N'Số KH dư nợ trên 50tr',sum(a.DUNO) N'Tổng dư nợ trên 50 tr' ,sum(a.DUNO) N'Tổng dư nợ trên 50 tr',sum(a.DUNO_TR) N'Tổng dư nợ trước khi vay bổ sung' "
                                           +" ,count(case when a.Z_SOKU > 1 then a.KU_MAKH end) N'Số KH vay bổ sung' ,sum(case when a.Z_SOKU > 1 then a.VAY_BS else 0 end) N'Dư nợ KH vay bổ sung' "
                                           +" ,count(case when a.Z_SOKU = 1 then a.KU_MAKH end) N' Số KH vay mới trên 50tr' ,sum(case when a.Z_SOKU = 1 then a.VAY_BS else 0 end) N' Dư nợ KH vay mới trên 50tr' "
                                           +" from lst3 a group by a.KU_MAPGD,a.maxa order by a.maxa ";
                                dt = cls.LoadDataText(strsql);
                                strstr1 = "with lst1 as ("
                                           + " select left(a.ku_madp, 6) maxa, a.*, b.KH_TENKH, a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO from HSCV_DAILY a, HSKH b "
                                           + " where a.KU_NGAYBC = '" + ng + "' and a.KU_TTMONVAY <> 'CLOSE' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 and a.KU_CHTRINH in ('01', '09', '19') "
                                           + " and a.KU_MAKH = b.KH_MAKH ), lst2 as ( select a.ku_makh,sum(a.DUNO) DUNO from lst1 a group by a.ku_makh having sum(a.DUNO) > 50000000 ), lst3 as ("
                                           + " select a.KU_MAPGD,a.maxa,a.KU_MATO,a.KU_MAKH,a.KU_MAQD CHTR, sum(a.DUNO)DUNO , sum(case when a.KU_NGAYGNDT < '2019-03-01' then a.DUNO else 0 end) DUNO_TR "
                                           + " ,sum(case when a.KU_NGAYGNDT >= '2019-03-01' then a.DUNO else 0 end) VAY_BS  ,count(a.KU_SOKU) Z_SOKU from lst1 a, lst2 b where a.KU_MAKH = b.KU_MAKH group by a.KU_MAPGD,a.maxa,a.KU_MATO,a.KU_MAKH,a.KU_MAQD "
                                           + " ) select a.KU_MAPGD N'Mã POS',(select PO_TEN from DMPOS where PO_MA = a.KU_MAPGD) N'Tên POS' "
                                           + " ,COUNT(a.KU_MAKH) N'Số KH dư nợ trên 50tr',sum(a.DUNO) N'Tổng dư nợ trên 50 tr' ,sum(a.DUNO) N'Tổng dư nợ trên 50 tr',sum(a.DUNO_TR) N'Tổng dư nợ trước khi vay bổ sung' "
                                           + " ,count(case when a.Z_SOKU > 1 then a.KU_MAKH end) N'Số KH vay bổ sung' ,sum(case when a.Z_SOKU > 1 then a.VAY_BS else 0 end) N'Dư nợ KH vay bổ sung' "
                                           + " ,count(case when a.Z_SOKU = 1 then a.KU_MAKH end) N' Số KH vay mới trên 50tr' ,sum(case when a.Z_SOKU = 1 then a.VAY_BS else 0 end) N' Dư nợ KH vay mới trên 50tr' "
                                           + " from lst3 a group by a.KU_MAPGD order by a.KU_MAPGD ";
                                strstr2 = "with lst1 as ( "
                                            +" select left(a.ku_madp, 6) maxa, a.KU_MAPGD,a.KU_SOKU,a.KU_MATO,a.KU_MAKH,a.KU_MAQD,a.KU_NGAYGNDT, b.KH_TENKH, a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO "
                                            +" ,(select PLMD from DM_PNKT where PNKT = a.KU_MAPNKT51) PLMD "
                                            +" from HSCV_DAILY a, HSKH b where a.KU_NGAYBC = '"+ng+"' and a.KU_TTMONVAY <> 'CLOSE' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 and a.KU_CHTRINH in ('01', '09', '19')  "
                                            +" and a.KU_MAKH = b.KH_MAKH ) "
                                            +" , lst2 as (select a.ku_makh,sum(a.DUNO) DUNO from lst1 a group by a.ku_makh having sum(a.DUNO) > 50000000 ) "
                                            +" , lst3 as ( "
                                            +" select a.KU_MAPGD,a.maxa,a.KU_MATO,a.KU_MAKH,a.KU_MAQD CHTR, a.PLMD, sum(a.DUNO)DUNO , sum(case when a.KU_NGAYGNDT < '2019-03-01' then a.DUNO else 0 end) DUNO_TR "
                                            +" ,sum(case when a.KU_NGAYGNDT >= '2019-03-01' then a.DUNO else 0 end) VAY_BS  ,count(a.KU_SOKU) Z_SOKU from lst1 a, lst2 b where a.KU_MAKH = b.KU_MAKH group by a.KU_MAPGD,a.maxa,a.KU_MATO,a.KU_MAKH,a.KU_MAQD,a.PLMD ) "
                                            +" select a.PLMD,(select GIATRI from DMKHAC where KHOA_1 = '24' and KHOA_2 = a.PLMD) N'Tên PLMD' "
                                            +" ,COUNT(a.KU_MAKH) N'Số KH dư nợ trên 50tr',sum(a.DUNO) N'Tổng dư nợ trên 50 tr' ,sum(a.DUNO) N'Tổng dư nợ trên 50 tr',sum(a.DUNO_TR) N'Tổng dư nợ trước khi vay bổ sung' "
                                            +" ,count(case when a.Z_SOKU > 1 then a.KU_MAKH end) N'Số KH vay bổ sung' ,sum(case when a.Z_SOKU > 1 then a.VAY_BS else 0 end) N'Dư nợ KH vay bổ sung' "
                                            +" ,count(case when a.Z_SOKU = 1 then a.KU_MAKH end) N' Số KH vay mới trên 50tr' ,sum(case when a.Z_SOKU = 1 then a.VAY_BS else 0 end) N' Dư nợ KH vay mới trên 50tr' "
                                            +" from lst3 a group by a.PLMD order by a.PLMD";

                                //var dtall = ora.LoadDataText(strstr);
                                var dt1 = cls.LoadDataText(strstr1);
                                var dt2 = cls.LoadDataText(strstr2);
                                // FileName = Thumuc + "\\" + pos + "_Thống kê đối chiếu phân loại nợ_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                                FileName1 = Thumuc + "\\" + pos + "_Tổng hợp POS - HN_HCN_HMTN trên 50 tr_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                                FileName2 = Thumuc + "\\" + pos + "Theo nghành kinh tế_HN_HCN_HMTN trên 50 tr_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                                str.ExportToExcel(dt1, FileName1);
                                str.ExportToExcel(dt2, FileName2);
                                break;
                            case "M55":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") ==lastMonth.ToString("yyyy-MM-dd"))
                                {
                                    strsql = "select cs_mapgd POS,substr(cs_madp,1,6) MAXA,(select ten from dmxa where ma=substr(cs_madp,1,6)) TENXA,cs_makh MAKH,kh_tenkh TENKH "
                                            + " ,cs_mato MATO, to_tentt TENTT,cs_so_tk2 TK, cs_sodu_tk SODU,to_char(cs_ngayroito, 'dd/MM/yyyy') NG_ROITO,cs_ttso_tk TINHTRANG "
                                            + " from casa left join hsto on cs_mato = to_mato left join hskh on cs_makh = kh_makh where cs_ngaybc ='" + dtpNgay.SelectedDate.Value.ToString("dd/MMM/yyyy") + "'"
                                            + "and cs_sp_tk = '105' and cs_ttso_tk = 'A' and cs_mato is not null and cs_makh in (select ku_makh from "
                                            + " ( select ku_makh, sum(ku_dnothan + ku_dnoqhan + ku_dnokhoanh) duno from HSKU where ku_ngaybc ='" + dtpNgay.SelectedDate.Value.ToString("dd/MMM/yyyy") + "' "
                                            + " group by ku_makh having sum(ku_dnothan + ku_dnoqhan + ku_dnokhoanh) = 0 )"
                                            + " ) order by substr(cs_madp, 1, 6),cs_mato,cs_makh";
                                }
                                else
                                {
                                    strsql = "select cs_mapgd POS,substr(cs_madp,1,6) MAXA,(select ten from dmxa where ma=substr(cs_madp,1,6)) TENXA,cs_makh MAKH,kh_tenkh TENKH "
                                            +" ,cs_mato MATO, to_tentt TENTT,cs_so_tk2 TK, cs_sodu_tk SODU,to_char(cs_ngayroito, 'dd/MM/yyyy') NG_ROITO,cs_ttso_tk TINHTRANG "
                                            + " from casa_daily left join hsto on cs_mato = to_mato left join hskh on cs_makh = kh_makh where cs_ngaybc ='" + dtpNgay.SelectedDate.Value.ToString("dd/MMM/yyyy") + "'"
                                            + "and cs_sp_tk = '105' and cs_ttso_tk = 'A' and cs_mato is not null and cs_makh in (select ku_makh from "
                                            + " ( select ku_makh, sum(ku_dnothan + ku_dnoqhan + ku_dnokhoanh) duno from hscv_daily where ku_ngaybc ='" + dtpNgay.SelectedDate.Value.ToString("dd/MMM/yyyy") + "' "
                                            + " group by ku_makh having sum(ku_dnothan + ku_dnoqhan + ku_dnokhoanh) = 0 )"
                                            +" ) order by substr(cs_madp, 1, 6),cs_mato,cs_makh";
                                }
                                //MessageBox.Show(strsql);
                                dt = ora.LoadDataText(strsql);
                                break;
                            case "M56":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") == lastMonth.ToString("yyyy-MM-dd"))
                                {
                                    strsql = "select cs_mapgd POS,substr(cs_madp,1,6) MAXA"
                                    + ",(select ten from dmxa where ma = substr(cs_madp, 1, 6)) TENXA,cs_makh MAKH, kh_tenkh, cs_so_tk2 TK,cs_sodu_tk SODU, cs_ttso_tk "
                                    + ", to_char(cs_ngayroito,'dd/MM/yyyy') NG_ROITO from casa,hskh where cs_ngaybc ='" + dtpNgay.SelectedDate.Value.ToString("dd/MMM/yyyy") + "'"
                                    + " and cs_sp_tk = '105' and cs_ttso_tk = 'A' and cs_mato is null and to_date('" + dtpNgay.SelectedDate.Value.ToString("dd/MMM/yyyy") + "') - cs_ngayroito > 90 and cs_sodu_tk < 100000"
                                    + " and cs_makh = kh_makh order by substr(cs_madp, 1, 6), cs_makh";
                                }
                                else
                                {
                                    strsql = "select cs_mapgd POS,substr(cs_madp,1,6) MAXA"
                                    +",(select ten from dmxa where ma = substr(cs_madp, 1, 6)) TENXA,cs_makh MAKH, kh_tenkh, cs_so_tk2 TK,cs_sodu_tk SODU, cs_ttso_tk "
                                    + ", to_char(cs_ngayroito,'dd/MM/yyyy') NG_ROITO from casa_daily,hskh where cs_ngaybc ='" + dtpNgay.SelectedDate.Value.ToString("dd/MMM/yyyy") + "'"
                                    + " and cs_sp_tk = '105' and cs_ttso_tk = 'A' and cs_mato is null and to_date('" + dtpNgay.SelectedDate.Value.ToString("dd/MMM/yyyy") + "') - cs_ngayroito > 90 and cs_sodu_tk < 100000"
                                    + " and cs_makh = kh_makh order by substr(cs_madp, 1, 6), cs_makh";
                                }
                                //MessageBox.Show(strsql);
                                dt = ora.LoadDataText(strsql);
                                break;
                            case "M57":
                                strsql = "select * from saokect_cdtt where cdtt_ngaybc='"+ dtpNgay.SelectedDate.Value.ToString("dd/MMM/yyyy") + "' order by cdtt_mapgd";
                                dt = ora.LoadDataText(strsql);
                                // FileName = Thumuc + "\\" + pos + "_Các lệnh thanh thanh toán_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M58":
                                strsql = "with lst1 as ( select ku_mapgd, substr(ku_madp, 1, 6) maxa, ten tenxa , ku_dnothan + ku_dnoqhan + ku_dnokhoanh DUNO "
                                    +" , (case when substr(ku_sprd_cd, 4, 1) = 'S' then ku_dnothan+ku_dnoqhan + ku_dnokhoanh end) DNNH ,(case when substr(ku_sprd_cd, 4, 1) = 'M' then ku_dnothan+ku_dnoqhan + ku_dnokhoanh end) DNTH "
                                    +" ,(case when substr(ku_sprd_cd, 4, 1) = 'L' then ku_dnothan+ku_dnoqhan + ku_dnokhoanh end) DNDH from hsku left join dmxa on substr(ku_madp,1, 6)= ma"
                                    +" where ku_ngaybc ='"+ dtpNgay.SelectedDate.Value.ToString("dd/MMM/yyyy") + "' and ku_ttmonvay <> 'CLOSE' and ku_dnothan+ku_dnoqhan + ku_dnokhoanh > 0 and ku_mapnkt51 in ('01451','01452') ) "
                                    +" select ku_mapgd, maxa, tenxa, sum(duno)TONGDUNO, sum(DNNH) NGANHAN, sum(DNTH) TRUNGHAN, sum(DNDH) DAIHAN from lst1 "
                                    +" group by ku_mapgd, maxa, tenxa order by maxa ";

                                dt = ora.LoadDataText(strsql);
                                // FileName = Thumuc + "\\" + pos + "_Các lệnh thanh thanh toán_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M59":
                                strsql = "with lst1 as ( select a.KU_MAPGD, a.KU_MADP, d.TEN TENXA, a.KU_MAKH, c.KH_TENKH, char(39) + a.KU_SOKU SOKU, a.KU_NGAYVAY, a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO , a.KU_MANDT, b.DT_TENDT, a.KU_MAQD, a.PL_NGUONVON_BS, (case when a.KU_NGUONVON = '1' and a.PL_NGUONVON_BS = '01' and a.KU_MANDT <> 'INV0107190050391' then 'F' when a.KU_NGUONVON = '2' and a.KU_MANDT = 'INV0107190050391' then 'F' else 'T' end) TT from HSCV_DAILY a left join (select distinct DT_MADT, DT_TENDT, DT_MAPGD from HSDT) b on a.KU_MANDT = b.DT_MADT and a.KU_MAPGD = b.DT_MAPGD left join hskh c on a.KU_MAKH = c.KH_MAKH left join DMXA d on left(a.KU_MADP, 6)= d.MA where a.KU_NGAYBC = '"+ng+"' ) select a.*from lst1 a where a.tt = 'F' order by a.KU_MAPGD,a.KU_MADP,a.KU_MANDT";
                                dt = cls.LoadDataText(strsql);
                                break;
                            case "M60":
                                strsql = "with lst1 as ( SELECT a.ku_mapgd, substr(a.ku_madp, 1, 6) maxa, a.ku_makh, concat(chr(39), a.ku_soku) soku, a.ku_soku"
                                +" , a.ku_ngayvay, a.ku_ngaydhan_3, a.ku_dnothan, a.ku_dnoqhan, a.ku_dnokhoanh , a.ku_maqd FROM hscv_daily   a, dm_product   b WHERE a.ku_ngaybc = '"+ngora+"'"
                                +" AND a.ku_prod_cd = b.pr_ma AND pr_cnqhflg = 'P' and a.ku_ttmonvay <> 'CLOSE' and a.ku_dnothan + a.ku_dnoqhan + a.ku_dnokhoanh > 0"
                                +" ), lst2 as ( select a.* from khtn a, lst1 b where a.kh_soku = b.ku_soku and a.kh_ngdhan >= '"+ngora+"' and a.kh_ngdhan <= '"+den_ngora+"'"
                                +" ) select a.ku_mapgd,a.maxa,d.ten tenxa, a.ku_makh ,(case when c.kh_tenkh is null then(select dn_ten from hskh_dn where dn_ma = a.ku_makh) end) tenkh,a.soku"
                                +" ,a.ku_ngayvay,a.ku_ngaydhan_3,a.ku_dnothan,a.ku_dnoqhan,a.ku_dnokhoanh ,b.kh_ngdhan,b.kh_gocdhan,a.ku_maqd,e.giatri ten_chtr from lst1 a " 
                                +"left join hskh c on a.ku_makh = c.kh_makh left join dmxa d on a.maxa = d.ma left join (select *from dmkhac where khoa_1 = '07') e on a.ku_maqd = e.khoa_2"
                                +",lst2 b where a.ku_soku = b.kh_soku order by a.maxa,a.ku_soku,b.kh_ngdhan";
                                dt = ora.LoadDataText(strsql);
                                // FileName = Thumuc + "\\" + pos + "_Các lệnh thanh thanh toán_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M61":
                                dt = cls.LoadLdbf("usp_ThuNoPhanKy", bien, giatri, thamso);
                                break;
                            case "M62":
                                dt = cls.LoadLdbf("usp_NdhTheoThangPos", bien, giatri, thamso);
                                break;
                        }
                        if (dt.Rows.Count > 0)
                        {
                            FileName = Thumuc + "\\" + pos + "_" + CboMau.SelectedValue.ToString().Substring(5, CboMau.SelectedValue.ToString().Length - 5) + "_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                            if (mau == "M02" || mau == "M06" || mau == "M55" || mau == "M56")
                            {
                                FileStream fs = new FileStream(FileName, FileMode.Create);
                                StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
                                str.ToCSV(dt, sw, true);
                            }
                            else str.ExportToExcel(dt, FileName);
                            //switch (mau)
                            //{

                            //    case "M02":
                            //        str.ToCSV(dt, sw, true);
                            //        break;
                            //    case "M06"://        str.ToCSV(dt, sw, true);
                            //        break;
                            //    default:
                            //        MessageBox.Show(FileName);
                            //        str.ExportToExcel(dt, FileName);
                            //        break;
                            //}
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
            try
            {
                //MessageBox.Show(str.Left(cboPos.SelectedValue.ToString().Trim(),6));
                if (str.Left(CboPos.SelectedValue.ToString().Trim(), 6) != "003000")
                {
                    CboXa.Items.Clear();
                    cls.ClsConnect();
                    string sql = "select MA,TEN from DMXA where PGD_QL= " + "'" +
                                 str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "'" + " order by MA";
                    var dtxa = cls.LoadDataText(sql);
                    for (int i = 0; i < dtxa.Rows.Count; i++)
                    {
                        CboXa.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                    }                   
                }
                else
                {
                    CboXa.Items.Add("003000 | Tất cả");
                }
                CboXa.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
            ora.DongKetNoi();
        }

   
    }
}
