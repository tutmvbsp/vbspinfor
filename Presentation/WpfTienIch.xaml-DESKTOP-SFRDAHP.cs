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
        ToolBll str = new ToolBll();
        //ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        string Thumuc = "C:\\KT740";
        private string FileName = "";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
            dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
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
            cls.DongKetNoi();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            str.TaoThuMuc(Thumuc);
            string toantu = "";
            string ten = "";
            if (Ration1.IsChecked == true)
            {
                toantu = "=";
                ten = "Bang";
            }
            else if (Ration2.IsChecked == true)
            {
                toantu = "<=";
                ten = "Nho hon";
            }
            else if (Ration3.IsChecked == true)
            {
                toantu = ">=";
                ten = "lon hon";
            }
            try
            {
                int thamso = 2;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                if (dtpNgay.SelectedDate != null)
                {
                    string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    string pos = str.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    string thg = textBox.Text;
                    bien[0] = "@MaPos";
                    giatri[0] = pos;
                    bien[1] = "@Ngay";
                    giatri[1] = ng;
                    DateTime lastMonth = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month, DateTime.DaysInMonth(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month));
                   // DateTime LastWeek = dtpNgay.SelectedDate.Value.AddDays(-(int)dtpNgay.SelectedDate.Value.DayOfWeek-2);
                    if (dtpNgay.SelectedDate != null)
                    {
                        cls.ClsConnect();
                        string mau = str.Left(CboMau.SelectedValue.ToString(), 3);
                        string strsql;
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
                                FileName = Thumuc + "\\" + pos + "_TT_NO_CASA105_" +dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M02":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") ==lastMonth.ToString("yyyy-MM-dd"))
                                {
                                    strsql = "select left(a.CS_MADP,6) MAXA,(select TEN from DMXA where MA=left(a.CS_MADP,6)) TENXA,a.CS_MATO,(select TO_TENTT from HSTO where TO_MATO = a.CS_MATO) TENTT "
                                             + " ,b.KH_MAKH,b.KH_TENKH,char(39) + a.CS_SO_TK TK, a.CS_SODU_TK "
                                             + " from CASA a, HSKH b where a.CS_NGAYBC = '" + ng +
                                             "' and a.CS_MAPGD = '" + pos +
                                             "' and a.CS_SP_TK = '105' and a.CS_TTSO_TK <> 'C' and a.CS_SODU_TK " +
                                             toantu + " " + thg
                                             +
                                             " and a.CS_MAKH = b.KH_MAKH order by left(a.CS_MADP, 6), a.CS_MATO, a.CS_MAKH";
                                }
                                else
                                {
                                    strsql = "select left(a.CS_MADP,6) MAXA,(select TEN from DMXA where MA=left(a.CS_MADP,6)) TENXA,a.CS_MATO,(select TO_TENTT from HSTO where TO_MATO = a.CS_MATO) TENTT "
                                             + " ,b.KH_MAKH,b.KH_TENKH,char(39) + a.CS_SO_TK TK, a.CS_SODU_TK "
                                             + " from CASA_DAILY a, HSKH b where a.CS_NGAYBC = '" + ng +
                                             "' and a.CS_MAPGD = '" + pos +
                                             "' and a.CS_SP_TK = '105' and a.CS_TTSO_TK <> 'C' and a.CS_SODU_TK " +
                                             toantu + " " + thg
                                             +
                                             " and a.CS_MAKH = b.KH_MAKH order by left(a.CS_MADP, 6), a.CS_MATO, a.CS_MAKH";
                                }
                                dt = cls.LoadDataText(strsql);
                                FileName = Thumuc + "\\" + pos + "_CASA105_"+ten+"_"+thg+"_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M03":
                                dt = cls.LoadLdbf("usp_DongCasaThKe", bien, giatri, thamso);
                                if (dt.Rows.Count > 0)
                                    FileName = Thumuc + "\\" + pos + "_ThongKe_DongCasa105_" +
                                               dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                else
                                    MessageBox.Show("Không có KH nào", "Mess", MessageBoxButton.OK,
                                        MessageBoxImage.Information);
                                break;
                            case "M04":
                                dt = cls.LoadLdbf("usp_CungCoToThKe", bien, giatri, thamso);
                                if (dt.Rows.Count > 0)
                                    FileName = Thumuc + "\\" + pos + "_ThongKe_ThanhVienTo_" +
                                               dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                else
                                    MessageBox.Show("Không có KH nào", "Mess", MessageBoxButton.OK,
                                        MessageBoxImage.Information);
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
                                FileName = Thumuc + "\\" + pos + "_TV_KGUI_CASA105_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M06":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") ==
                                    lastMonth.ToString("yyyy-MM-dd"))
                                {
                                    strsql = "select g.TEN TENXA,c.KH_DIACHI,f.TO_TENTT,e.TENDV,a.KU_MAKH,c.KH_TENKH"
                                             +
                                             " ,a.KU_SOKU,char(39)+a.KU_SOKU SOKU,d.TENVT,a.KU_M_GNGAN GNGAN,a.KU_M_TNTHAN TNTH,a.KU_M_TNQHAN TNQH "
                                             +
                                             " ,a.KU_LAI_TT LAI,a.KU_DNOTHAN DNTH,a.KU_DNOQHAN DNQH,a.KU_DNOKHOANH DNKH "
                                             +
                                             " ,(a.KU_DNOTHAN+a.KU_DNOQHAN+a.KU_DNOKHOANH) DUNO,a.KU_MAPNKT51,b.GIATRI PNKT,a.KU_TTMONVAY,N'' PNKT_NEW "
                                             + " from HSKU a,DMKHAC b,HSKH c,DM_CHTRINH d,DVUT e,HSTO f,DMXA g "
                                             + " where a.KU_NGAYBC= '" + ng +
                                             "' and a.KU_MAKH=c.KH_MAKH and a.KU_MAPGD= '" + pos
                                             +
                                             "' and a.KU_MAPNKT51=b.KHOA_2 and b.KHOA_1='25' and a.KU_CHTRINH=d.CHTRINH "
                                             +
                                             " and a.KU_TTMONVAY<>'CLOSE' and a.KU_DNOTHAN+a.KU_DNOQHAN+a.KU_DNOKHOANH>0 "
                                             +
                                             " and a.KU_MATO=f.TO_MATO and e.DVUT=f.TO_DVUT and LEFT(a.KU_MADP,6)=g.MA "
                                             + " order by LEFT(a.KU_MADP,6),a.KU_MATO,a.KU_MAKH,a.KU_CHTRINH ";
                                }
                                else
                                {
                                    strsql = "select g.TEN TENXA,c.KH_DIACHI,f.TO_TENTT,e.TENDV,a.KU_MAKH,c.KH_TENKH"
                                             +
                                             " ,a.KU_SOKU,char(39)+a.KU_SOKU SOKU,d.TENVT,a.KU_M_GNGAN GNGAN,a.KU_M_TNTHAN TNTH,a.KU_M_TNQHAN TNQH "
                                             +
                                             " ,a.KU_LAI_TT LAI,a.KU_DNOTHAN DNTH,a.KU_DNOQHAN DNQH,a.KU_DNOKHOANH DNKH "
                                             +
                                             " ,(a.KU_DNOTHAN+a.KU_DNOQHAN+a.KU_DNOKHOANH) DUNO,a.KU_MAPNKT51,b.GIATRI PNKT,a.KU_TTMONVAY,N'' PNKT_NEW "
                                             + " from HSCV_DAILY a,DMKHAC b,HSKH c,DM_CHTRINH d,DVUT e,HSTO f,DMXA g "
                                             + " where a.KU_NGAYBC= '" + ng +
                                             "' and a.KU_MAKH=c.KH_MAKH and a.KU_MAPGD= '" + pos
                                             +
                                             "' and a.KU_MAPNKT51=b.KHOA_2 and b.KHOA_1='25' and a.KU_CHTRINH=d.CHTRINH "
                                             +
                                             " and a.KU_TTMONVAY<>'CLOSE' and a.KU_DNOTHAN+a.KU_DNOQHAN+a.KU_DNOKHOANH>0 "
                                             +
                                             " and a.KU_MATO=f.TO_MATO and e.DVUT=f.TO_DVUT and LEFT(a.KU_MADP,6)=g.MA "
                                             + " order by LEFT(a.KU_MADP,6),a.KU_MATO,a.KU_MAKH,a.KU_CHTRINH ";
                                }
                                dt = cls.LoadDataText(strsql);
                                FileName = Thumuc + "\\" + pos + "_SKE_PNKT_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M07":
                                strsql = "select b.MA N'Ma xã',b.TEN N'Tên Xã',sum(a.KU_DNOTHAN+a.KU_DNOQHAN+a.KU_DNOKHOANH) DUNO "
                                         +"from HSKU a,DMXA b where a.KU_NGAYBC='"+ng+"' and a.KU_TTMONVAY<>'CLOSE' "
                                         +"and a.KU_DNOTHAN+a.KU_DNOQHAN+a.KU_DNOKHOANH>0 and a.KU_CHTRINH='06'"
                                         +" and LEFT(a.KU_MADP,6)=b.MA and b.DA_CHOBA='T' "
                                         +" group by b.MA,b.TEN order by b.MA ";
                                dt = cls.LoadDataText(strsql);
                                FileName = Thumuc + "\\" + pos + "_SKE_CHOBA_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
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
                                FileName = Thumuc + "\\" + pos + "_TO_HET_DUNO_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M09":
                                //MessageBox.Show(giatri1[0].ToString() + " " + giatri1[1].ToString());
                                dt = cls.LoadLdbf("usp_NotCasa105", bien, giatri, thamso);
                                if (dt.Rows.Count > 0)
                                    FileName = Thumuc + "\\" + pos + "_NotCasa105_" +
                                               dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                else
                                    MessageBox.Show("Không có KH nào", "Mess", MessageBoxButton.OK,
                                        MessageBoxImage.Information);
                                break;
                            case "M10":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd")== lastMonth.ToString("yyyy-MM-dd")) //|| (int)dtpNgay.SelectedDate.Value.DayOfWeek == 5)
                                    strsql = "select b.KH_MAKH,b.KH_TENKH,char(39)+a.KU_SOKU SOKU,a.KU_TON_RPA,a.KU_TTMONVAY from HSKU a,HSKH b where a.KU_MAKH=b.KH_MAKH and a.KU_TON_RPA>0 and a.KU_NGAYBC='" + ng+"' and a.KU_MAPGD='"+pos+"'";
                                else strsql = "select b.KH_MAKH,b.KH_TENKH,char(39)+a.KU_SOKU SOKU,a.KU_TON_RPA,a.KU_TTMONVAY from HSCV_DAILY a,HSKH b where a.KU_MAKH=b.KH_MAKH and a.KU_TON_RPA>0 and a.KU_NGAYBC='" + ng + "' and a.KU_MAPGD='" + pos + "'";
                                dt = cls.LoadDataText(strsql);
                                FileName = Thumuc + "\\" + pos + "_TON_RPA_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M11":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") == lastMonth.ToString("yyyy-MM-dd"))
                                {
                                    strsql = "select c.KH_MAPGD,a.MAXA N'Mã Xã',(select TEN from DMXA where MA=a.MAXA) N'Tên Xã',a.KU_MATO N'Mã tổ',(select TO_TENTT from HSTO where TO_MATO=a.KU_MATO) N'Tên TT',a.KU_MAKH N'Mã KH',c.KH_TENKH N'Tên KH'," 
                                               +" char(39) + a.KU_SOKU N'Số KU', a.KU_CHTRINH N'Ch Trình', a.DUNO N'Dư nợ', b.CS_MATO N'Mã Tổ TK', char(39) + b.CS_SO_TK N'Số TK', b.CS_SODU_TK N'Dư TK' from "
                                               +" (select left(KU_MADP, 6) MAXA, *, KU_DNOTHAN + KU_DNOQHAN + KU_DNOKHOANH DUNO from HSKU  where KU_NGAYBC = '"+ng+"' and KU_MAPGD = '"+pos+"' and KU_TTMONVAY <> 'CLOSE' and KU_DNOTHAN + KU_DNOQHAN + KU_DNOKHOANH > 0 "
                                               +" and KU_HTHUCVAY = '3') a, (select * from CASA where CS_NGAYBC = '"+ng+"' and CS_SP_TK = '105' and CS_MATO is null and CS_TTSO_TK <> 'C') b "
                                               +" ,HSKH c where a.KU_MAKH = b.CS_MAKH and a.KU_MAKH = c.KH_MAKH and b.CS_MAKH = c.KH_MAKH order by a.MAXA,a.KU_MATO";}
                                else
                                {
                                    strsql = "select c.KH_MAPGD,a.MAXA N'Mã Xã',(select TEN from DMXA where MA=a.MAXA) N'Tên Xã',a.KU_MATO N'Mã tổ',(select TO_TENTT from HSTO where TO_MATO=a.KU_MATO) N'Tên TT',a.KU_MAKH N'Mã KH',c.KH_TENKH N'Tên KH',"
                                               + " char(39) + a.KU_SOKU N'Số KU', a.KU_CHTRINH N'Ch Trình', a.DUNO N'Dư nợ', b.CS_MATO N'Mã Tổ TK', char(39) + b.CS_SO_TK N'Số TK', b.CS_SODU_TK N'Dư TK' from "
                                               + " (select left(KU_MADP, 6) MAXA, *, KU_DNOTHAN + KU_DNOQHAN + KU_DNOKHOANH DUNO from HSCV_DAILY  where KU_NGAYBC = '" + ng + "' and KU_MAPGD = '" + pos + "' and KU_TTMONVAY <> 'CLOSE' and KU_DNOTHAN + KU_DNOQHAN + KU_DNOKHOANH > 0 "
                                               + " and KU_HTHUCVAY = '3') a, (select * from CASA_DAILY where CS_NGAYBC = '" + ng + "' and CS_SP_TK = '105' and CS_MATO is null and CS_TTSO_TK <> 'C') b "
                                               + " ,HSKH c where a.KU_MAKH = b.CS_MAKH and a.KU_MAKH = c.KH_MAKH and b.CS_MAKH = c.KH_MAKH order by a.MAXA,a.KU_MATO";
                                }
                        
                                dt = cls.LoadDataText(strsql);
                                if (dt.Rows.Count > 0)
                                    FileName = Thumuc + "\\" + pos + "_GanMatoChoCasa105_" +
                                               dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                else
                                    MessageBox.Show("Không có KH nào", "Mess", MessageBoxButton.OK,
                                        MessageBoxImage.Information);
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
                                if (dt.Rows.Count > 0)
                                    FileName = Thumuc + "\\" + pos + "_TO_KHONG_CO_NQH_" +
                                               dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                else
                                    MessageBox.Show("Không có tổ nào", "Mess", MessageBoxButton.OK,
                                        MessageBoxImage.Information);

                                break;
                            case "M13":
                                dt = cls.LoadLdbf("usp_ChkAddHSKH", bien, giatri, thamso);
                                if (dt.Rows.Count > 0)
                                    FileName = Thumuc + "\\" + pos + "_BoSung_TT_VC_" +
                                               dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                else
                                    MessageBox.Show("Không có KH nào", "Mess", MessageBoxButton.OK,
                                        MessageBoxImage.Information);
                                break;
                            case "M14":
                                strsql = "select a.NGAYGD,char(39)+a.TK N'Tài khoản',b.CS_MATO N'Mã tô',(select TO_TENTT from HSTO where TO_MATO=b.CS_MATO) N'Tên TT' "
                                         +" ,c.KH_MAKH N'Mã KH',c.KH_TENKH N'Tên KH',a.SOTIEN N'Số tiền ',a.TK_NO N'TK nợ',a.TK_CO N'TK Có' "
                                         +" from HSBT a,CASA b, HSKH c where a.MAPGD = '"+pos+"' and b.CS_MAPGD = '"+pos+"' and a.MOD_CD in ('CT','FP') and a.NOCO = 'D' and b.CS_NGAYBC = '"+ng+"' and b.CS_SP_TK = '105' and a.TK = b.CS_SO_TK "
                                         +" and b.CS_MAKH = c.KH_MAKH order by a.NGAYGD,b.CS_MATO,c.KH_MAKH";
                                //MessageBox.Show(strsql);
                                dt = cls.LoadDataText(strsql);
                                FileName = Thumuc + "\\" + pos + "_RUT_CASA105_TM_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M15":
                                if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") ==
                                    lastMonth.ToString("yyyy-MM-dd"))
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
                                FileName = Thumuc + "\\" + pos + "_KU_"+thg+"_THANG_KHONG_HD" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
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
                                FileName = Thumuc + "\\" + pos + "_SAOKE_CMT_HETHAN_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
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
                                FileName = Thumuc + "\\" + pos + "_SAOKE_CMT_HETHAN_TONGHOP" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
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
                                FileName = Thumuc + "\\" + pos + "_LAI_TON_LON_HON_"+thg.Trim()+"_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;
                            case "M19":
                                strsql = "select b.KH_MAPGD,c.MA N'Mã Xã',c.TEN N'Tên Xã',b.KH_MAKH N'Mã KH',b.KH_TENKH N'Tên KH',char(39)+a.CS_SO_TK N'Tài khoản',a.CS_TENTK N'Tên Tài khoản',a.CS_SODU_TK N'Số dư',a.CS_TTSO_TK N'Tình trạng' "
                                            +" from CASA a,hskh b, DMXA c "
                                            +" where a.CS_NGAYBC = '"+ng+"' and CS_SP_TK = '104' and a.CS_MAPGD = '"+pos+"' and a.CS_MAKH = b.KH_MAKH and c.MA = left(a.CS_MADP, 6) "
                                            +" order by c.MA,b.KH_MAKH";
                                dt = cls.LoadDataText(strsql);
                                FileName = Thumuc + "\\" + pos + "_CASA_104_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
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
                                FileName = Thumuc + "\\" + pos + "_TIEN_GUI_TO_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
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
                                FileName = Thumuc + "\\" + pos + "_TIEN_GUI_TO_TREN_2_TRIEU_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
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
                                FileName = Thumuc + "\\" + pos + "_TH_KH_NHIEU_CASA_105_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
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
                                FileName = Thumuc + "\\" + pos + "_TANG_PHIEN_NAM_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
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
                                FileName = Thumuc + "\\" + pos + "_CASA_105_DA_GO_MA_TO_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
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
                                FileName = Thumuc + "\\" + pos + "_Sao kê thiếu TT vợ chồng_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                break;



                        }
                        if (dt.Rows.Count > 0)
                        {
                            switch (mau)
                            {
                                case "M02":
                                    FileStream fs = new FileStream(FileName, FileMode.Create);
                                    StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
                                    str.ToCSV(dt, sw, true);
                                    break;
                                default:
                                    str.ExportToExcel(dt, FileName);
                                    break;
                            }
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
        }

   
    }
}
