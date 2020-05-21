using System;
using System.Collections;
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
using System.Data;
using DAL;
using BLL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfImportText.xaml
    /// </summary>
    public partial class WpfImportText : Window
    {
        public WpfImportText()
        {
            InitializeComponent();
        }

        private FileStream _fw;
        ToolBll bll = new ToolBll();
        DataTable dtFile = new DataTable();
        DataTable dt = new DataTable();
        ClsOracle cls = new ClsOracle();
        ClsServer cnn = new ClsServer();
        DataTable dtchk = new DataTable();
        private string FileName = "";
        string Thumuc = "C:\\KT740";
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /* xac dinh ngay cuoi thang
           DateTime ngay = dtpNgay.SelectedDate.Value; //DateTime.Now; // hay ngày nào đó trong CSDL?
                bool cuoiThang = (ngay.Month != ngay.AddDays(1).Month);
                MessageBox.Show(cuoiThang ? "Cuoi thang" : "khong phai Cuoi thang");
         */
        private void LoadSingle()
        {
            cls.ClsConnect();
            string sql = "";
            string[] arrStr = cboFile.SelectedValue.ToString().Trim().Split('|');
            if (dtpNgay.SelectedDate != null)
            {
                string ngay = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
                string TuNgay = dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy");
                string DenNgay = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
                string toantu = "";
                if (Ration1.IsChecked == true)
                {
                    toantu = "=";
                }
                else if (Ration2.IsChecked == true)
                {
                    toantu = ">";
                }
                else if (Ration3.IsChecked == true)
                {
                    toantu = "<";
                }

                //string sql = "select * from " + arrStr[0] + " where to_char(" + arrStr[1] + ",'dd/MM/yyyy') = " + "'" + datePicker1.SelectedDate.Value.ToString("dd/MM/yyyy") + "'";
                String FileName = arrStr[0].Trim();
                String FieldNgay = arrStr[1].Trim();
                switch (FileName)
                {
                    case "CASA_DCVON":
                        sql = "select  BR_CD,AC_NO,IBAN_AC,AC_BR_CD,to_char(OPEN_DT,'YYYY-MM-DD') OPEN_DT,to_char(CLS_DT,'YYYY-MM-DD') CLS_DT,NO_HLDRS,STAFF_FLG"
                                + ",LEG_ST,PRD_CD,CCY_CD,DLQ_ST,AC_CAT,PAM_CD,COST_CTR,BUS_SEG,CUST_ST"
                                + ",DOM_CD,LOC_CD,FIN_ST,FIN_SUB_ST,CUR_BAL,LCY_CUR_BAL,OP_BAL,LCY_OP_BAL"
                                + ",OFF_TURN,LCY_OFF_TURN,EAR_AMT,REC_ST,TOT_LINE_AMT,AAFA_FLG,UFD_FLG"
                                + ",MD_BAL,MD_FLG,AS_BAL,AS_FLG,UNAUTH_CR_AMT,UNAUTH_DR_AMT,SHORT_CD"
                                + ",MICR_NO,UNCOL_BAL,GROUP_NO,LAST_CB_NO,LAST_CRD_NO,DR_ADV_FLG,CR_ADV_FLG"
                                + ",HOLD_MAIL,DLQ_PRD_STR,to_char(DT_LST_DR,'YYYY-MM-DD') DT_LST_DR,to_char(DT_LST_CR,'YYYY-MM-DD') DT_LST_CR,INACT_ST" 
                                + ",to_char(LST_APP_DT,'YYYY-MM-DD') LST_APP_DT,NBCP_ELG_FLG"
                                + ",OP_GROUP_NO,to_char(EOD_DATE,'YYYY-MM-DD') EOD_DATE,to_char(CA_LST_DLQ_DT,'YYYY-MM-DD') CA_LST_DLQ_DT"
                                + ",to_char(LST_DLQ_DT,'YYYY-MM-DD') LST_DLQ_DT,CHECKSUM,LST_APP_AMT"
                                + ",to_char(LST_ACC_DT,'YYYY-MM-DD') LST_ACC_DT,SIGN_REQ,SEC_REF_NO,SCHEME_CD,to_char(SCHEME_CHANGE_DT,'YYYY-MM-DD') SCHEME_CHANGE_DT,NET_ID"
                                + ",APPLN_AC_NO,to_char(RATE_RESET_DATE,'YYYY-MM-DD') RATE_RESET_DATE,ONLINE_APP_FLG,MKR_ID,to_char(MKR_DT,'YYYY-MM-DD') MKR_DT,AUTH_ID"
                                + ",to_char(AUTH_DT,'YYYY-MM-DD') AUTH_DT,AC_NAME,POS_CD,GUAR_FLG,COVER_INSTR,CR_APPLN_AC_NO,DR_APPLN_AC_NO"
                                + ",TXN_BAL,LCY_TXN_BAL,LAST_TXN_NO,LL_NAME,NO_NOTICE_DAYS,AC_PROD_FEATURES"
                                + ",STMT_CYCLE,STMT_FREQ,STMT_DLRY_MD,PAM_CD_2,ACC_NAME_TC3,to_char(LST_BAL_CHG_DT,'YYYY-MM-DD') LST_BAL_CHG_DT" +
                                ",to_char(NGAY_BC,'YYYY-MM-DD') NGAY_BC from "
                                + FileName + " where " + FieldNgay + " " + toantu + " " 
                                + "to_date(" + "'" + ngay + "'" + "," + "'dd/MM/yyyy" + "')"; 
                        break;
                    case "CASA" :
                        sql = "select  CS_MAPGD ,CS_MAKH ,CS_MATO ,CS_SO_TK ,CS_SO_TK2 ,CS_TENTK ,CS_SODU_TK ,CS_SP_TK ,CS_M_GUITK ,CS_M_RUTTK ,"
                        + "CS_Q_GUITK ,CS_Q_RUTTK ,CS_A_GUITK ,CS_A_RUTTK ,CS_TTSO_TK , to_char(CS_NGAYBC,'YYYY-MM-DD') CS_NGAYBC ,CS_MACN ,"
                        + " to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT ,CS_MADP ,to_char(CS_NGAYDKY,'YYYY-MM-DD') CS_NGAYDKY ,"
                        + "to_char(CS_NGAYTT,'YYYY-MM-DD') CS_NGAYTT ,to_char(CS_NGAYROITO,'YYYY-MM-DD') CS_NGAYROITO from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/MM/yyyy" + "') order by CS_MAPGD,CS_MADP,CS_MATO,CS_MAKH "; ;
                        break;
                    case "CASA_DAILY":
                        sql = "select CS_MACN , CS_MAPGD , CS_MAKH , CS_MATO , CS_SO_TK , CS_SO_TK2 , CS_TENTK , CS_SP_TK , CS_SODU_TK , CS_M_GUITK "
                               +", CS_M_RUTTK , CS_Q_GUITK , CS_Q_RUTTK , CS_A_GUITK , CS_A_RUTTK , CS_TTSO_TK , CS_MADP ,to_char(CS_NGAYBC,'YYYY-MM-DD') CS_NGAYBC "
                                +", to_char(CS_NGAYDKY,'YYYY-MM-DD') CS_NGAYDKY, to_char(CS_NGAYTT,'YYYY-MM-DD') CS_NGAYTT ,to_char(CS_NGAYROITO,'YYYY-MM-DD') CS_NGAYROITO  from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/MM/yyyy" + "') order by CS_MAPGD,CS_MADP,CS_MATO,CS_MAKH "; 
                        break;
                    case "HSTO":
                        sql = "select distinct TO_MATO , TO_LOAITO , TO_MATT , TO_TENTT , TO_DVUT , TO_HTUNTG , TO_HTUNTV , TO_KYDG , TO_MADP , TO_TKHH , TO_MAPGD, "
                        + " TO_MACN , TRANGTHAI ,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT from TMP_HSTO where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "') ";
                        break;
                    case "TMP_HSTG":
                        sql = "select MACN,MAPGD,GL_TK,SOTK,SOTK_0,MAKH,TENKH,MASP,SODU_SK,SODU_HD,LAIDUTHU,LAIDATRA"
                                + ",KYHAN,KYHAN_DV,PHANHE,INACT_ST,TRANGTHAI,NAMQT,to_char(NGAYTAO,'YYYY-MM-DD') NGAYTAO,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,LOAITIEN,LAISUAT"
                                + ",to_char(NGAYGUI,'YYYY-MM-DD') NGAYGUI,to_char(NGAYDENHAN,'YYYY-MM-DD') NGAYDENHAN,GOCTINHLAI,GOCDENHAN"
                                + ",to_char(NGAYDUTHUCUOI,'YYYY-MM-DD') NGAYDUTHUCUOI,to_char(NGAYTATTOAN,'YYYY-MM-DD') NGAYTATTOAN,LAINHAPGOC,MADP"
                                + " from TMP_HSTG where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "') ";
                        break;
                    case "QT_HSTG":
                        sql = "select MACN,MAPGD,GL_TK,SOTK,SOTK_0,MAKH,TENKH,MASP,SODU_SK,SODU_HD,LAIDUTHU,LAIDATRA"
                                + ",KYHAN,KYHAN_DV,PHANHE,INACT_ST,TRANGTHAI,NAMQT,to_char(NGAYTAO,'YYYY-MM-DD') NGAYTAO,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,LOAITIEN,LAISUAT"
                                + ",to_char(NGAYGUI,'YYYY-MM-DD') NGAYGUI,to_char(NGAYDENHAN,'YYYY-MM-DD') NGAYDENHAN,GOCTINHLAI,GOCDENHAN"
                                + ",to_char(NGAYDUTHUCUOI,'YYYY-MM-DD') NGAYDUTHUCUOI,to_char(NGAYTATTOAN,'YYYY-MM-DD') NGAYTATTOAN,LAINHAPGOC,MAPGD MADP"
                                + " from QT_HSTG where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "') ";
                        break;
                    case "HSKH":
                        sql = "select KH_MAKH , KH_TENKH ,to_char(KH_NGAYSINH,'YYYY-MM-DD') KH_NGAYSINH , KH_LOAIKH , KH_GIOITINH , KH_DANTOC , KH_CMT , KH_NOICAP ,"
                            + "to_char(KH_NGAYCAP,'YYYY-MM-DD') KH_NGAYCAP , KH_TENVC "
                            + ", KH_CMT_VC , KH_DIACHI , KH_MADP , KH_MOBILE , KH_TTRANG , KH_MAPGD , KH_MACN ,"
                            + "to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT  from hskh "
                            + "union select DN_MA as KH_MAKH ,DN_TEN as KH_TENKH ,to_char(DN_NGAYTL,'YYYY-MM-DD') as KH_NGAYSINH ,	DN_LOAIKH as KH_LOAIKH"
                            + ",DN_PLOAI as KH_GIOITINH,	'' as KH_DANTOC,DN_MST as KH_CMT,'' as KH_NOICAP,to_char(DN_NGAYTL,'YYYY-MM-DD') as KH_NGAYCAP,DN_TGD as KH_TENVC,'' as KH_CMT_VC"
                            + ",DN_DIACHI as KH_DIACHI,DN_MADP as KH_MADP,'' as KH_MOBILE,DN_TTRANG as KH_TTRANG ,DN_MAPGD as KH_MAPGD,DN_MACN as KH_MACN,"
                            + "to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT from HSKH_DN ";
                        //and " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "') 
                        //and " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "') 
                        break;
                    case "PLKT":
                        sql = "select PL_SOKU,PL_TTRANG,PL_MAPNKT51,PL_MAPNKT52,PL_HQDT_CD,PL_HQDT_VAL1,PL_HQDT_VAL2,PL_HQDT_UNIT1"
                                + ",PL_HQDT_UNIT2,PL_MAPGD,PL_MACN,to_char(NG_CAPNHAT, 'YYYY-MM-DD') NG_CAPNHAT,PL_MDNHA,PL_MD30A,PL_MADA,PL_NGUONVON_BS,PL_SOLDLAPN"
                                +",PL_SOLDLANKT,PL_SOLDLANTS,PL_GTRIVONVAY,PL_HQDT_CD2,PL_HQDT_CD3,PL_HQDT_CD4,PL_HQDT_CD5,PL_HQDT_CD6"
                                +",PL_MAPNKT53,PL_MAPNKT54,PL_MAPNKT55,PL_MAPNKT56,PL_HQDT_VAL3,PL_HQDT_VAL4,PL_HQDT_VAL5,PL_HQDT_VAL6"
                                +",PL_HQDT_UNIT3,PL_HQDT_UNIT4,PL_HQDT_UNIT5,PL_HQDT_UNIT6 from "
                                + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')"; 
                        break;    
                    case "HSQH":
                        sql = "select QH_SOKU,to_char(QH_NGAYCQH,'YYYY-MM-DD') QH_NGAYCQH ,QH_GOCCQH,QH_LOAINN,QH_NGNHAN"
                             + ",QH_TRANGTHAI,QH_MAPGD,QH_MACN,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT from " 
                             + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;

                    case "HSCV_DAILY":
                        sql = "select TT , KU_MAKH ,KU_SOKU , KU_MATO , to_char(KU_NGAYVAY,'YYYY-MM-DD') KU_NGAYVAY, to_char(KU_NGAYDHAN_1,'YYYY-MM-DD') KU_NGAYDHAN_1, "
                                + "to_char(KU_NGAYDHAN_2,'YYYY-MM-DD') KU_NGAYDHAN_2,to_char(KU_NGAYDHAN_3,'YYYY-MM-DD') KU_NGAYDHAN_3,KU_HTHUCVAY , KU_SPRD_CD ,KU_CAPQLV ,KU_NGAYGDLD , "
                                + "KU_LSUAT ,KU_DTTH ,KU_MANDT ,to_char(KU_NGAY_TGOC,'YYYY-MM-DD') KU_NGAY_TGOC,to_char(KU_NGAY_TLAI,'YYYY-MM-DD') KU_NGAY_TLAI , "
                                + "to_char(KU_NGKTAHSV,'YYYY-MM-DD') KU_NGKTAHSV , KU_MAPNKT51 ,KU_MAPNKT52 ,KU_HQDT_CD ,	KU_HQDT_VAL1 , KU_HQDT_VAL2 , KU_MUCVAY ,	KU_GNGAN , "
                                + "to_char(KU_NGAYGNDT,'YYYY-MM-DD') KU_NGAYGNDT , to_char(KU_NGAYGNCC,'YYYY-MM-DD') KU_NGAYGNCC , KU_DNOTHAN ,KU_DNOQHAN , KU_DNOKHOANH , "
                                + "KU_TNOTHAN , KU_GOCDHAN ,	KU_GOCDTRA , KU_GOCXOA , KU_LAIXOA , KU_LAITHAN , KU_LAITONTHAN , KU_LAIQHAN , KU_LAITONQHAN , "
                                + "KU_LAI_DT , KU_M_LAI_DT , KU_LAI_TT , KU_M_LAI_TT , KU_M_LAI_PB , KU_Q_LAI_PB , KU_A_LAI_PB , KU_M_LAI_KH , KU_Q_LAI_KH , "
                                + "KU_A_LAI_KH , KU_LCDHAN_DT , KU_M_GNGAN , KU_GHANNO , KU_M_GHANNO , KU_CHUYENQH , to_char(KU_NGAYCNQH,'YYYY-MM-DD') KU_NGAYCNQH , "
                                + "KU_M_CHUYENQH , KU_M_DKCHUYENQH , KU_CHUYENKH , KU_M_CHUYENKH ,	KU_TON_RPA , to_char(KU_NGAYGDGN,'YYYY-MM-DD') KU_NGAYGDGN , "
                                + "KU_M_GOCXOA , to_char(KU_NGAY_DTHU,'YYYY-MM-DD') KU_NGAY_DTHU , KU_LAIHT_TONG ,	KU_LAIHT_CHT , KU_M_LUUVU , KU_M_DKGNGAN , KU_TTHAINO , KU_TTMONVAY , "
                                + "KU_TKTHAN , KU_TKQHAN , KU_TKKHOANH , KU_TKTHULAI , KU_M_TNTHAN , KU_M_TNQHAN , KU_M_TNKHOANH , KU_SCHEM_CD , KU_PROD_CD , KU_NGUONVON , KU_CHTRINH , KU_MAQD , "
                                + "KU_KYQUYFLG , KU_Q_GNGAN , KU_Q_LUUVU , KU_Q_DKGNGAN , KU_Q_GHANNO , KU_Q_CHUYENQH , KU_Q_CHUYENKH , KU_Q_TNTHAN , KU_Q_TNQHAN , KU_Q_TNKHOANH , KU_Q_GOCXOA , KU_Q_LAI_DT , "
                                + "KU_Q_LAI_TT , KU_A_GNGAN , KU_A_LUUVU , KU_A_DKGNGAN , KU_A_GHANNO , KU_A_CHUYENQH , KU_A_CHUYENKH , KU_A_TNTHAN , KU_A_TNQHAN , KU_A_TNKHOANH , KU_A_GOCXOA , KU_A_LAI_DT , "
                                + "KU_A_LAI_TT , KU_M_LAITHAN , KU_Q_LAITHAN , KU_A_LAITHAN , KU_M_LAIQHAN , KU_Q_LAIQHAN , KU_A_LAIQHAN , KU_TNTH , KU_TNQH , KU_TNKH , to_char(KU_LASTDUECRDT,'YYYY-MM-DD') KU_LASTDUECRDT , "
                                + " to_char(KU_NGAYHHKH,'YYYY-MM-DD') KU_NGAYHHKH , KU_GOCHHKH , to_char(KU_NGAYLUUVU,'YYYY-MM-DD') KU_NGAYLUUVU , "
                                + "KU_MAPGD , KU_MACN , to_char(KU_NGAYBC,'YYYY-MM-DD') KU_NGAYBC , KU_MADP , KU_CHUANNDP , KU_M_KHOANHCQHAN , KU_M_KHOANHCTHAN , "
                                + "KU_M_THOAILAI , CS_SO_TK , CS_SO_TK2 , CS_TENTK , CS_SODU_TK , CS_M_GUITK , CS_M_RUTTK , CS_Q_GUITK , CS_Q_RUTTK , CS_A_GUITK , "
                                + "CS_A_RUTTK , CS_TTSO_TK , to_char(CS_NGAYDKY,'YYYY-MM-DD') CS_NGAYDKY , to_char(CS_NGAYTT,'YYYY-MM-DD') CS_NGAYTT , "
                                + "to_char(CS_NGAYROITO,'YYYY-MM-DD') CS_NGAYROITO , KU_M_DAOKHOANTNO,PL_NGUONVON_BS  from " + FileName + " where substr(ku_soku,1,1)='6' and " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "') order by KU_MAPGD,KU_MADP,KU_MATO,KU_MAKH";
                        break;
                    case "HSKU":
                        sql = "	select KU_MAKH ,KU_SOKU , KU_MATO , "
                               + "to_char(KU_NGAYVAY,'YYYY-MM-DD') KU_NGAYVAY, to_char(KU_NGAYDHAN_1,'YYYY-MM-DD') KU_NGAYDHAN_1, "
                               + "to_char(KU_NGAYDHAN_2,'YYYY-MM-DD') KU_NGAYDHAN_2, to_char(KU_NGAYDHAN_3,'YYYY-MM-DD') KU_NGAYDHAN_3, "
                               + "KU_HTHUCVAY , KU_SPRD_CD ,KU_CAPQLV ,KU_NGAYGDLD ,KU_LSUAT ,KU_DTTH ,KU_MANDT ,  to_char(KU_NGAY_TGOC,'YYYY-MM-DD') KU_NGAY_TGOC, "
                               + "to_char(KU_NGAY_TLAI,'YYYY-MM-DD') KU_NGAY_TLAI ,  to_char(KU_NGKTAHSV,'YYYY-MM-DD') KU_NGKTAHSV ,  KU_MAPNKT51 ,"
                               + "KU_MAPNKT52 ,KU_HQDT_CD ,	KU_HQDT_VAL1 ,KU_HQDT_VAL2 , KU_MUCVAY ,	KU_GNGAN ,  to_char(KU_NGAYGNDT,'YYYY-MM-DD') KU_NGAYGNDT , "
                               + "to_char(KU_NGAYGNCC,'YYYY-MM-DD') KU_NGAYGNCC ,  KU_DNOTHAN ,KU_DNOQHAN , KU_DNOKHOANH ,  KU_TNOTHAN , KU_GOCDHAN ,	KU_GOCDTRA , "
                               + "KU_GOCXOA , KU_LAIXOA , KU_LAITHAN ,  KU_LAITONTHAN , KU_LAIQHAN , KU_LAITONQHAN ,  KU_LAI_DT , KU_LAI_TT ,KU_LCDHAN_DT , KU_M_GNGAN ,  "
                               + "KU_GHANNO , KU_M_GHANNO , KU_CHUYENQH ,  to_char(KU_NGAYCNQH,'YYYY-MM-DD') KU_NGAYCNQH ,  KU_M_CHUYENQH , KU_CHUYENKH ,  KU_M_CHUYENKH ,"
                               + "KU_TON_RPA , to_char(KU_NGAYGDGN,'YYYY-MM-DD') KU_NGAYGDGN ,  KU_M_GOCXOA , to_char(KU_NGAY_DTHU,'YYYY-MM-DD') KU_NGAY_DTHU ,  "
                               + "KU_LAIHT_TONG ,	KU_LAIHT_CHT , KU_M_LUUVU , KU_M_DKGNGAN , KU_TTHAINO , KU_TTMONVAY ,  KU_TKTHAN , KU_TKQHAN , KU_TKKHOANH , "
                               + "KU_TKTHULAI , KU_M_TNTHAN , KU_M_TNQHAN ,  KU_M_TNKHOANH , KU_SCHEM_CD , KU_PROD_CD , KU_NGUONVON , KU_CHTRINH , KU_MAQD ,  "
                               + "KU_KYQUYFLG , KU_Q_GNGAN , KU_Q_LUUVU , KU_Q_DKGNGAN , KU_Q_GHANNO , KU_Q_CHUYENQH ,  KU_Q_CHUYENKH , KU_Q_TNTHAN , KU_Q_TNQHAN , "
                               + "KU_Q_TNKHOANH , KU_Q_GOCXOA , KU_Q_LAI_DT ,  KU_Q_LAI_TT , KU_A_GNGAN , KU_A_LUUVU , KU_A_DKGNGAN , KU_A_GHANNO , KU_A_CHUYENQH ,  "
                               + "KU_A_CHUYENKH , KU_A_TNTHAN , KU_A_TNQHAN , KU_A_TNKHOANH , KU_A_GOCXOA , KU_A_LAI_DT ,  KU_A_LAI_TT , KU_M_LAITHAN , KU_Q_LAITHAN , "
                               + "KU_A_LAITHAN , KU_M_LAIQHAN , KU_Q_LAIQHAN ,  KU_A_LAIQHAN , KU_TNTH , KU_TNQH , KU_TNKH ,  KU_MAPGD , KU_MACN , "
                               + "to_char(KU_NGAYBC,'YYYY-MM-DD') KU_NGAYBC , KU_MADP , KU_CHUANNDP ,  to_char(KU_NGAYHHKH,'YYYY-MM-DD') KU_NGAYHHKH , KU_GOCHHKH , "
                               + "to_char(KU_NGAYLUUVU,'YYYY-MM-DD') KU_NGAYLUUVU  from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "') order by KU_MAPGD,KU_MADP,KU_MATO,KU_MAKH ";
                        break;
                    case "HSSV":
                        sql = "select SV_SOKU , SV_MASV , SV_TENSV , to_char(SV_NGSINH_SV,'YYYY-MM-DD') SV_NGSINH_SV , SV_GTINH_SV , SV_CMT_SV "
                                +", SV_MATRUONG , SV_LOAIHDT , SV_LOAIHCS , SV_HEDTAO , SV_NGANHDT , SV_DTHOCPHI , to_char(SV_NGNHAPHOC,'YYYY-MM-DD') SV_NGNHAPHOC "
                                +", to_char(SV_NGRTRUONG,'YYYY-MM-DD') SV_NGRTRUONG , SV_SO_ATM , SV_DVCAPTHE , SV_DTSV , SV_TTHAISV , SV_MAPGD , SV_MACN "
                                +", SV_REC_ST , to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT , SV_CLASS , SV_COURCE , SV_FACULTY , SV_IDNO  from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "KHTN":
                        sql = "select  KH_SOKU , KH_LANTNO , to_char(KH_NGDHAN,'YYYY-MM-DD') KH_NGDHAN , KH_GOCDHAN , KH_LAIDHAN , KH_LAITONPB , KH_DUNO , KH_GOCDTRA , KH_LAIDTRA "
                               + ", KH_STHTRO , KH_MAPGD , KH_MACN ,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT   from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "KHGN":
                        sql = "select KH_SOKU , KH_LANGNGAN ,to_char(KH_NGGNGAN,'YYYY-MM-DD') KH_NGGNGAN , KH_STGNGAN , KH_LSUAT , KH_MAHTLS "
                                + ",to_char(KH_NGAYBDHT,'YYYY-MM-DD') KH_NGAYBDHT ,to_char(KH_NGAYKTHT,'YYYY-MM-DD') KH_NGAYKTHT , KH_LSUATHT , KH_NGUONHT , KH_MAPGD , KH_MACN "
                                + ",to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT , KH_DGNGAN_FLG  from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "QT_MS02TL":
                        sql = "select MACN,MAPGD,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,D2,D3,D4,D5,D6,D7,D8,D9,D10,D11,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT "
                                + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "QT_MS04TL":
                        sql = "select MACN ,MAPGD,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,CHTRINH,D1,D2,D3,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT "
                                + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "QT_MS14":
                        sql = "select MAPGD,NGAYBC,KEY,D1,D2,D3,D4,D5,D6,D7,D8,D9,D10,D11,D12,D13"
                               +",D14,D15,D16,D17,D18,D19,D20,D21,D22,D23,D24,D25,D26"
                               + ",D27,D28,D29,D30,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT,MACN,D31,D32"
                                + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "CT_VBSP":
                        sql = "select CT_MACT,CT_KIEUGIATRI,CT_GIATRI,to_char(CT_NGAYBC,'YYYY-MM-DD') CT_NGAYBC,CT_MAPGD,CT_MACN,to_char(NGAY_TAO,'YYYY-MM-DD') NGAY_TAO,CT_CAPTH,CT_IDCTG"
                                + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "GL_VBSP":
                        sql = "select GL_TK,GL_TENTK,GL_TKCAP3,GL_SL,GL_LOAITIEN,GL_DD_NO,GL_DD_CO,GL_PS_NO,GL_PS_CO,GL_DC_NO,GL_DC_CO"
                            + ",GL_DD_NO_NT,GL_DD_CO_NT,GL_PS_NO_NT,GL_PS_CO_NT,GL_DC_NO_NT,GL_DC_CO_NT,to_char(GL_NGAYBC,'YYYY-MM-DD') GL_NGAYBC,GL_MAPGD,GL_MACN"
                                + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "GL_VBSP_TH":
                        sql = "select GL_TK,GL_TENTK,GL_TKCAP3,GL_SL,GL_LOAITIEN,GL_DD_NO,GL_DD_CO,GL_PS_NO,GL_PS_CO,GL_DC_NO"
                            + ",GL_DC_CO,GL_DD_NO_NT,GL_DD_CO_NT,GL_PS_NO_NT,GL_PS_CO_NT,GL_DC_NO_NT,GL_DC_CO_NT,to_char(GL_NGAYBC,'YYYY-MM-DD') GL_NGAYBC"
                            + ",GL_CAPTH,GL_KYBC,GL_MAPGD,GL_MACN"
                                + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "HSBT":
                        sql = "select SBT,TK,TK_NO,TK_CO,NOCO,MOD_CD,TXN_CD,SUBTXN_CD,to_char(NGAYGD,'YYYY-MM-DD') NGAYGD,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,TIENTE,ST_NGUYENTE"
                            + ",SOTIEN,GHICHU_1,GHICHU_2,GDV,KSV,MAPGD,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT"
                                + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "ONLINE_01TL":
                        sql = "select POS_CD,GROUP_ID,MASS_ORG,PROD_DESC,CUST_NAME,CUST_ADRS,GROUP_NAME,LEGACY_ID,REF_NO,TXN_TYPE,to_char(TXN_DATE,'YYYY-MM-DD') TXN_DATE"
                            + ",DISB_AMT,PRIN_PAID,INT_PAID,INT_RT,PRIN_OS,AUTH_ID,MAKER_ID,to_char(MAKER_DT,'YYYY-MM-DD') MAKER_DT,LOAN_PGM,CUST_ID,CIVIL_ID,to_char(ISSUE_DT,'YYYY-MM-DD') ISSUE_DT,ISSUE_PLC,to_char(EOD_DT,'YYYY-MM-DD') EOD_DT,COMMUNE_ID"
                                + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "HSLV_HISTORY":
                        sql = "select SOKU,SPRD_CD,MAPGD,to_char(NGAYDK_1,'YYYY-MM-DD') NGAYDK_1,to_char(NGAYDH_1,'YYYY-MM-DD') NGAYDH_1,to_char(NGAYDK_2,'YYYY-MM-DD') NGAYDK_2"
                              + " ,to_char(NGAYDH_2,'YYYY-MM-DD') NGAYDH_2,LSUAT_1,LSUAT_2,ST_DHAN,ST_LUUVU"
                              + " ,to_char(NGAYHL,'YYYY-MM-DD') NGAYHL,to_char(NGAYTAO,'YYYY-MM-DD') NGAYTAO,SOTHANG,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT,MACN,TRANGTHAI"
                              + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "HSGH_HISTORY":
                        sql = "select MAPGD,SOKU,MAKH,MACN,CHEQ_HIST,GH_TSLAN,GH_LAN,to_char(GH_NGAY,'YYYY-MM-DD') GH_NGAY,GH_SOTIEN,GH_SOTHG,GH_TSOTHG "
                            + ",GH_TSOTIEN,GH_LOAINV,SPRD_CD,GH_MAQD,to_char(GH_NGAYQD,'YYYY-MM-DD') GH_NGAYQD,GH_NGNHAN,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT "
                            + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "TXN_POINT_INFO_MB":
                        sql = "select TPI_ID,TPI_DATE,TPI_DESC,FILE_GEN_FLAG,TPI_POS,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT"
                            + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "TXNPOINT_DETAIL":
                        sql = "select POS_CODE,POS_DESC,TXNPOINT_ID,TPI_DESC,MAKER_ID,MAKER_DT,CMUNE_VISIT_DATE,UPL_CHECK,CMUNE_VISIT_FLAG"
                                +",CMUNE_FLAG_CHNG_BY,UPL_TIME,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC"
                            + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "DG_CASA105_DATA":
                        sql = "select MAPGD,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,MAKH,TK,TK1,SODUDAUKY,GUITK,CKTRANO_TONGSO,CKTRALAI,CKTRAGOC,CKTRAGOC_TUSDKYTRC,CKTRALAI_TUSDKYTRC "
                               + ",RUTTK,LAINHAPGOC,SODUCUOIKY,CHENHLECHSODU"
                            + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "QT_TSCC":
                        sql = "select MA_TS,TEN_TS,LOAI_TS,TEN_LOAI_TS,LOAI_TS_CHITIET,TEN_LOAI_TS_CHITIET,MA_NHANHIEU_TS,TEN_NHANHIEU_TS,NGUYEN_GIA"
                                + ",SO_LUONG,VON_TW,VON_DP,VON_KHAC,HAOMON_LK,POS_CD,MAIN_POS,NAMQT,to_char(NGAYTAO,'YYYY-MM-DD') NGAYTAO,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,to_char(NGAY_MUA,'YYYY-MM-DD') NGAY_MUA,MAPHONG,TENPHONG"
                            + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "QT_TSTL":
                        sql = "select MA_TS,TEN_TS,LOAI_TS,TEN_LOAI_TS,LOAI_TS_CHITIET,TEN_LOAI_TS_CHITIET,MA_NHANHIEU_TS,to_char(NGAY_SDUNG,'YYYY-MM-DD') NGAY_SDUNG,THOIGIAN_SD,NGUYEN_GIA,HAOMON_LK "
                                + ",to_char(NGAY_TLY,'YYYY-MM-DD') NGAY_TLY,CHIPHI_TLY,THUTU_TLY,POS_CD,MAIN_POS,NAMQT,to_char(NGAYTAO,'YYYY-MM-DD') NGAYTAO,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,to_char(NGAY_MUA,'YYYY-MM-DD') NGAY_MUA"
                            + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "TTLDDDAILYLOAN":
                        sql = "select KU_MAKH ,KU_SOKU,KU_MATO,to_char(KU_NGAYVAY,'YYYY-MM-DD') KU_NGAYVAY,to_char(KU_NGAYDHAN_1,'YYYY-MM-DD') KU_NGAYDHAN_1"
                              + ",to_char(KU_NGAYDHAN_2,'YYYY-MM-DD') KU_NGAYDHAN_2,to_char(KU_NGAYDHAN_3,'YYYY-MM-DD') KU_NGAYDHAN_3 "
                              + ",KU_HTHUCVAY,KU_SPRD_CD,KU_CAPQLV,KU_NGAYGDLD,KU_LSUAT,KU_DTTH,KU_MANDT,to_char(KU_NGAY_TGOC,'YYYY-MM-DD') KU_NGAY_TGOC"
                              + ",to_char(KU_NGAY_TLAI,'YYYY-MM-DD') KU_NGAY_TLAI,to_char(KU_NGKTAHSV,'YYYY-MM-DD') KU_NGKTAHSV,KU_MAPNKT51,KU_MAPNKT52"
                              +",KU_HQDT_CD,KU_HQDT_VAL1,KU_HQDT_VAL2"
                              + ",KU_MUCVAY,KU_GNGAN,to_char(KU_NGAYGNDT,'YYYY-MM-DD') KU_NGAYGNDT,to_char(KU_NGAYGNCC,'YYYY-MM-DD') KU_NGAYGNCC,KU_DNOTHAN,KU_DNOQHAN,KU_DNOKHOANH"
                              + ",KU_TNOTHAN,KU_GOCDHAN,KU_GOCDTRA,KU_GOCXOA,KU_LAIXOA,KU_LAITHAN,KU_LAITONTHAN"
                              +",KU_LAIQHAN,KU_LAITONQHAN,KU_LAI_DT,KU_M_LAI_DT,KU_LAI_TT,KU_M_LAI_TT,KU_M_LAI_PB"
                              +",KU_Q_LAI_PB,KU_A_LAI_PB,KU_M_LAI_KH,KU_Q_LAI_KH,KU_A_LAI_KH,KU_LCDHAN_DT,KU_M_GNGAN"
                              + ",KU_GHANNO,KU_M_GHANNO,KU_CHUYENQH,to_char(KU_NGAYCNQH,'YYYY-MM-DD') KU_NGAYCNQH,KU_M_CHUYENQH,KU_M_DKCHUYENQH,KU_CHUYENKH"
                              + ",KU_M_CHUYENKH,KU_TON_RPA,to_char(KU_NGAYGDGN,'YYYY-MM-DD') KU_NGAYGDGN,KU_M_GOCXOA,to_char(KU_NGAY_DTHU,'YYYY-MM-DD') KU_NGAY_DTHU,KU_LAIHT_TONG,KU_LAIHT_CHT"
                              + ",KU_M_LUUVU,KU_M_DKGNGAN,KU_TTHAINO,KU_TTMONVAY,KU_TKTHAN,KU_TKQHAN,KU_TKKHOANH,KU_TKTHULAI"
                              +",KU_M_TNTHAN,KU_M_TNQHAN,KU_M_TNKHOANH,KU_SCHEM_CD,KU_PROD_CD,KU_NGUONVON,KU_CHTRINH"
                              +",KU_MAQD,KU_KYQUYFLG,KU_Q_GNGAN,KU_Q_LUUVU,KU_Q_DKGNGAN,KU_Q_GHANNO,KU_Q_CHUYENQH,KU_Q_CHUYENKH"
                              +",KU_Q_TNTHAN,KU_Q_TNQHAN,KU_Q_TNKHOANH,KU_Q_GOCXOA,KU_Q_LAI_DT,KU_Q_LAI_TT,KU_A_GNGAN,KU_A_LUUVU"
                              +",KU_A_DKGNGAN,KU_A_GHANNO,KU_A_CHUYENQH,KU_A_CHUYENKH,KU_A_TNTHAN,KU_A_TNQHAN,KU_A_TNKHOANH"
                              +",KU_A_GOCXOA,KU_A_LAI_DT,KU_A_LAI_TT,KU_M_LAITHAN,KU_Q_LAITHAN,KU_A_LAITHAN,KU_M_LAIQHAN"
                              + ",KU_Q_LAIQHAN,KU_A_LAIQHAN,KU_TNTH,KU_TNQH,KU_TNKH,to_char(KU_LASTDUECRDT,'YYYY-MM-DD') KU_LASTDUECRDT,to_char(KU_NGAYHHKH,'YYYY-MM-DD') KU_NGAYHHKH,KU_GOCHHKH"
                              + ",to_char(KU_NGAYLUUVU,'YYYY-MM-DD') KU_NGAYLUUVU,KU_MAPGD,KU_MACN,to_char(KU_NGAYBC,'YYYY-MM-DD') KU_NGAYBC,KU_MADP,KU_CHUANNDP,KU_M_KHOANHCQHAN,KU_M_KHOANHCTHAN"
                              + ",KU_M_THOAILAI,KU_M_DAOKHOANTNO,PL_NGUONVON_BS"
                            + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "PLN_KNTN_CL":
                        sql = "select PLN_SOKU,PLN_MAKH,PLN_TENKH,PLN_MATO,PLN_DVUT,PLN_MADP,PLN_NGUONVON,PLN_SPRD_CD,PLN_CHTRINH,PLN_MAQD,PLN_DNOTHAN,PLN_DNOQHAN "
                              +" ,PLN_DNOKHOANH,PLN_LAITHAN_TT,PLN_LAIQHAN_TT,PLN_TONGLAI_TT,PLN_LAITONTHAN,PLN_LAITONQHAN,PLN_TONGLAITON,PLN_C_KNTN_SODU "
                              +" ,PLN_K_KNTN_SODU,PLN_K_KNTN_SD01,PLN_K_KNTN_SD02,PLN_K_KNTN_SD03,PLN_K_KNTN_SD04,PLN_K_KNTN_SD05,PLN_K_KNTN_SD06,PLN_K_KNTN_SD07 "
                              +" ,PLN_K_KNTN_SD08,PLN_K_KNTN_SD09,PLN_K_KNTN_SD10,PLN_K_KNTN_SD11,PLN_K_NGNHAN_KH,PLN_QUANHE_KH,PLN_TRANGTHAI,PLN_NOGOC_CLECH "
                              + " ,PLN_NOLAI_CLECH,PLN_NGNHAN_CLECH,PLN_TT_MONVAY,to_char(PLN_NGAYBC,'YYYY-MM-DD') PLN_NGAYBC,PLN_NGUOI_PLN"
                              + " ,to_char(PLN_NGAY_PLN,'YYYY-MM-DD') PLN_NGAY_PLN,PLN_MAPGD,PLN_MACN,to_char(PLN_NGAYCN,'YYYY-MM-DD') PLN_NGAYCN,PLN_TENTT "
                              + " ,PLN_TRANGTHAINO,PLN_MATT,PLN_LOAITO,PLN_TRANGTHAITT,to_char(PLN_NGAY_TT,'YYYY-MM-DD') PLN_NGAY_TT"
                            + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "DG_DATA":
                        sql = "select REPORT_KEY,MAIN_POS,POS_CD,POS_FLAG,to_char(REPORT_DT,'YYYY-MM-DD') REPORT_DT,TERM_FLG,COMMUNE_ID,GROUP_ID,INDICATOR,round(VALUE,2) VALUE,OUTPOINT,MARK"
                            + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        // Chú ý : đối với kiểm có quá nhiều sô thập phân phải dùng round nếu không xuất sẽ lỗi
                        break;
                    case "DG_MARK":
                        sql = "select REPORT_KEY,MAIN_POS,POS_CD,POS_FLAG,to_char(REPORT_DT,'YYYY-MM-DD') REPORT_DT,TERM_FLG,COMMUNE_ID,GROUP_ID,TOTAL_MARK,CLASSIFICATION,DESCRIPTION"
                             + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "I_USER":
                        sql = "select IU_MA,IU_TEN,IU_MAPGD,IU_TTRANG,IU_NV,CMT,to_char(NGAYHH,'YYYY-MM-DD') NGAYHH,NGUOITAO,to_char(NGAYTAO,'YYYY-MM-DD') NGAYTAO" +
                              ",NGUOIKT,to_char(NGAYKT,'YYYY-MM-DD') NGAYKT,CHUCVU,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC"
                             + " from " + FileName ;
                        break;
                    case "HSRR_WB":
                        sql = "select RR_SOKU,RR_MAKH,RR_TENKH,RR_DIACHI,RR_CHTRINH,RR_SPRD_CD,RR_DQ_STAT_CD,RR_DNGOC,RR_LAITH"
                                 + " ,RR_LAIQH,RR_DNGHI_DNO,RR_DNGHI_LAI,RR_XL_DUNO,RR_XL_LAI,RR_HT_DNO,RR_HT_LAI,to_char(RR_NGAYVAY,'YYYY-MM-DD') RR_NGAYVAY"
                                 + " ,to_char(RR_NGAYDH,'YYYY-MM-DD') RR_NGAYDH,RR_THOIHANVAY,RR_MDTHIETHAI,to_char(RR_NGAYRR,'YYYY-MM-DD') RR_NGAYRR,RR_DNGHI_TG,RR_PDUYET_TG,RR_NGUYENNHAN"
                                 + " ,RR_MOTANN,RR_TRANGTHAI,to_char(RR_PDUYET_NGAY_CN,'YYYY-MM-DD') RR_PDUYET_NGAY_CN,RR_PDUYET_NGUOI_CN,to_char(RR_PDUYET_NGAY_TW,'YYYY-MM-DD') RR_PDUYET_NGAY_TW,RR_PDUYET_NGUOI_TW"
                                 + " ,RR_TAOLAP_NGUOI,to_char(RR_TAOLAP_NGAY,'YYYY-MM-DD') RR_TAOLAP_NGAY,RR_MAQD,RR_TENQD,RR_NHOMRR,RR_MAPGD,RR_MACN,to_char(RR_NGAYBC,'YYYY-MM-DD') RR_NGAYBC,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT "
                                 + " ,to_char(RR_NGAYTAO,'YYYY-MM-DD') RR_NGAYTAO,RR_NGUOITAO,RR_PDUYET_CAP,to_char(RR_NGAYHL,'YYYY-MM-DD') RR_NGAYHL,RR_HT_TKXOANO,RR_NGUONVON,RR_DOTRR,RR_MADP"
                                 + " ,RR_MATO,RR_SOLANXL,RR_PDUYET_PGD,to_char(RR_NGAY_PDUYET_PGD,'YYYY-MM-DD') RR_NGAY_PDUYET_PGD,RR_NGUYENNHAN_TUCHOI,to_char(RR_INT_PDUYET_NGAY_CN,'YYYY-MM-DD') RR_INT_PDUYET_NGAY_CN"
                                 + " ,RR_INT_PDUYET_NGUOI_CN,RR_NGUYENNHAN_TC_CN,RR_KHOA"
                             + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')"
                             ; break;
                    case "HSXN_HISTORY":
                        sql = "select MAPGD,SOKU,MAKH,MACN,ST_GOC,ST_LAITH,to_char(NGAYRR,'YYYY-MM-DD') NGAYRR,TTMONVAY,ST_LAIQH"
                              + ",SPRD_CD,to_char(NGAYTAO,'YYYY-MM-DD') NGAYTAO,to_char(NGAYHL,'YYYY-MM-DD') NGAYHL,MAQD,to_char(NGAYQD,'YYYY-MM-DD') NGAYQD,NGNHAN,TRANGTHAI,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT"
                             + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')"
                             ; break;
                    case "DULIEU_NT":
                        sql = "select KHOA,THUTU,TT_HIENTHI,MA,TEN,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,NAMBC,MAPGD,CO_TONGHOP,MACN,NGUOI_NHAP,to_char(NGAY_NHAP,'YYYY-MM-DD')  NGAY_NHAP,NGUOI_DUYET,to_char(NGAY_DUYET,'YYYY-MM-DD')  NGAY_DUYET,D1,D2,D3,D4,D5,D6,D7,D8,D9      ,D10,D11,D12,D13,D14,D15,D16,D17,D18,D19,D20,D21,D22,D23,D24,D25,D26,D27,D28,D29,D30,NHAPTAY,FONTFORMAT,KIEUIN,D31,D32,D33,D34,D35,D36,D37,D38,D39,D40,D41,D42,D43,D44,D45,D46,D47,D48,D49,D50 from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                                break;
                    case "HSBT_145NO":
                        sql = "select SBT,TK,TK_NO,TK_CO,NOCO,MOD_CD,TXN_CD,SUBTXN_CD,to_char(NGAYGD,'YYYY-MM-DD') NGAYGD,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,TIENTE,ST_NGUYENTE, "
                               + " SOTIEN,'Nop tien vao TK145 ' GHICHU_1, '-' GHICHU_2,GDV,KSV,MAPGD,to_char(NG_CAPNHAT, 'YYYY-MM-DD') NG_CAPNHAT,concat(TK, to_char(NGAYGD, 'YYYY-MM-DD')) KHOA "
                               + " from HSBT where  ngaygd between " + "to_date(" + "'" + TuNgay + "'" + "," + "'dd/mm/yyyy" + "')" + " and " + "to_date(" + "'" + DenNgay + "'" + "," + "'dd/mm/yyyy" + "')" + " and mod_cd = 'CT' "
                               + " and tk in (select cs_so_tk from casa_daily where cs_sp_tk = '145' and CS_NGAYBC = (select max(CS_NGAYBC)   from casa_daily))";
                        break;

                    default:
                        sql = "select * from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + ngay + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;

                }
                //MessageBox.Show(sql, "Thông báo");
                dt = cls.LoadDataText(sql);
                //MessageBox.Show("Load OK", "Thông báo");
            }
            else
            {
                MessageBox.Show("Chưa chọn ngày ! ");
            }
            if (dt.Rows.Count > 0)
            {
                dgvNguon.ItemsSource = dt.DefaultView;
            }
            else
            {
                MessageBox.Show("Số liệu ngày " + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + " Chưa có !", "Thông báo");
            }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lổi kết nối Oracle :  "+ex.Message);
            //}

            cls.DongKetNoi();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                bll.TaoThuMuc(txtPath.Text.Trim());
                cnn.ClsConnect();
                //DataTable dtver = new DataTable();
                var dtver =cnn.LoadDataText(
                        "select MAX(CONVERT(date,NGAYKU,105)) as NGKUMAX,MAX(CONVERT(date,NGAYBT,105)) as NGBTMAX from U_HSTD");
                dtpNgayKu.SelectedDate = Convert.ToDateTime(dtver.Rows[0]["NGKUMAX"]); //DateTime.Now.AddDays(-3);
                var dtvercd = cnn.LoadDataText(
                "select MAX(CONVERT(date,NGAY,105)) as NGCDMAX from U_CANDOI");
                dtpNgay.SelectedDate = Convert.ToDateTime(dtvercd.Rows[0]["NGCDMAX"]); //DateTime.Now.AddDays(-3);
                //dtpNgay.SelectedDate = DateTime.Now.AddDays(-1); //Convert.ToDateTime(dtver.Rows[0]["NGBTMAX"]); //
                Ration1.IsChecked = true;
                Ration2.IsEnabled = false;
                Ration3.IsEnabled = false;
                Ration4.IsChecked = true;
                Ration6.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cnn.DongKetNoi();
            //Ration7.IsEnabled = false;
            // btnExportText.IsEnabled = false;
            // btnLoad.IsEnabled = false;
            //btnInsertSql.IsEnabled = false;
        }

        private void btnExportText_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnInsertSql_Click(object sender, RoutedEventArgs e)
        {
            
            cboFile.Items.Clear();
            string sql = "";
            if (Ration4.IsChecked == true)
            {
                sql = "select * from systable where ky='D' order by table_name ";
                LoadFile(dtFile, sql);
            }
            else if (Ration5.IsChecked == true)
            {
                sql = "select * from systable where ky='M' order by table_name ";
                LoadFile(dtFile, sql);
            }
            else if (Ration6.IsChecked == true)
            {
                sql = "select * from systable where ky='L' order by table_name ";
                LoadFile(dtFile, sql);
            }
            else if (Ration7.IsChecked == true)
            {
                sql = "select * from systable where ky='B' order by table_name ";
                LoadFile(dtFile, sql);
            }

                dgvNguon.ItemsSource = dtFile.DefaultView;
                string ngay = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                foreach (DataRow dr in dtFile.Rows)
                {
                    try
                    {
                       Insert_Text(dr[0].ToString().Trim(), dr[1].ToString().Trim(), ngay);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                MessageBox.Show("Insert text OK");
            btnBTPS_Click(null, null);
            btnKhtn_Click(null, null);
            btnMau06_Click(null, null);
            DATA_UYTHAC();
            DULIEU_TO();


        }

        private void WriteText(String fileName)
        {
            System.Text.Encoding encode = System.Text.Encoding.BigEndianUnicode;
            _fw = new System.IO.FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            StreamWriter sw = new StreamWriter(_fw, encode);
            //TextWriter sw = new StreamWriter(expFile);
            foreach (DataRow row in dt.Rows)
            {
                //foreach (DataColumn col in dt.Columns)
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i + 1 < dt.Columns.Count)
                    {
                        //sw.Write(row[col].ToString() + "#");
                        //sw.Write(row[i].ToString() + "#");
                        sw.Write(row[i].ToString() + "$");
                    }
                    else
                    {
                        sw.Write(row[i].ToString());
                    }
                }
                sw.WriteLine();
            }
            sw.Close();
           // MessageBox.Show("Export text OK");
        }

  
       private void LoadFile(DataTable dbFile, string sql)
        {
            ClsServer cls = new ClsServer();
            cls.ClsConnect();
            dbFile = cls.LoadDataText(sql);
            for (int i = 0; i < dbFile.Rows.Count; i++)
            {
                cboFile.Items.Add(dbFile.Rows[i][0] + " | " + dbFile.Rows[i][1]);
            }
            dgvNguon.ItemsSource = dbFile.DefaultView;
            dtFile = dbFile;
            cboFile.SelectedIndex = 0;
            cls.DongKetNoi();
        }

        private void Ration4_Checked(object sender, RoutedEventArgs e)
        {
            btnReadFile.IsEnabled = true;
            btnInsertSql.IsEnabled = true;

        }

        private void Ration6_Checked(object sender, RoutedEventArgs e)
        {
            btnReadFile.IsEnabled = false;
            btnInsertSql.IsEnabled = false;
            btnHskh.IsEnabled = false;
            ClsServer cls = new ClsServer();
            cls.ClsConnect();
            dtFile = cls.LoadDataText("select * from SYSTABLE order by table_name");
            for (int i = 0; i < dtFile.Rows.Count; i++)
            {
                cboFile.Items.Add(dtFile.Rows[i][0] + " | " + dtFile.Rows[i][1]);
            }
            cls.DongKetNoi();
        }

        private void ReadFile(String FileName,String FieldNgay,String NgayBc,String toantu)
        {
            cls.ClsConnect();
            string sql = "";
            string TuNgay = dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy");
            string DenNgay = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
            //string[] arrStr = cboFile.SelectedValue.ToString().Trim().Split('|');
            if (NgayBc != null)
            {
                switch (FileName)
                {
                    case "CASA_DCVON":
                        sql = "select  BR_CD,AC_NO,IBAN_AC,AC_BR_CD,to_char(OPEN_DT,'YYYY-MM-DD') OPEN_DT,to_char(CLS_DT,'YYYY-MM-DD') CLS_DT,NO_HLDRS,STAFF_FLG"
                                + ",LEG_ST,PRD_CD,CCY_CD,DLQ_ST,AC_CAT,PAM_CD,COST_CTR,BUS_SEG,CUST_ST"
                                + ",DOM_CD,LOC_CD,FIN_ST,FIN_SUB_ST,CUR_BAL,LCY_CUR_BAL,OP_BAL,LCY_OP_BAL"
                                + ",OFF_TURN,LCY_OFF_TURN,EAR_AMT,REC_ST,TOT_LINE_AMT,AAFA_FLG,UFD_FLG"
                                + ",MD_BAL,MD_FLG,AS_BAL,AS_FLG,UNAUTH_CR_AMT,UNAUTH_DR_AMT,SHORT_CD"
                                + ",MICR_NO,UNCOL_BAL,GROUP_NO,LAST_CB_NO,LAST_CRD_NO,DR_ADV_FLG,CR_ADV_FLG"
                                + ",HOLD_MAIL,DLQ_PRD_STR,to_char(DT_LST_DR,'YYYY-MM-DD') DT_LST_DR,to_char(DT_LST_CR,'YYYY-MM-DD') DT_LST_CR,INACT_ST"
                                + ",to_char(LST_APP_DT,'YYYY-MM-DD') LST_APP_DT,NBCP_ELG_FLG"
                                + ",OP_GROUP_NO,to_char(EOD_DATE,'YYYY-MM-DD') EOD_DATE,to_char(CA_LST_DLQ_DT,'YYYY-MM-DD') CA_LST_DLQ_DT"
                                + ",to_char(LST_DLQ_DT,'YYYY-MM-DD') LST_DLQ_DT,CHECKSUM,LST_APP_AMT"
                                + ",to_char(LST_ACC_DT,'YYYY-MM-DD') LST_ACC_DT,SIGN_REQ,SEC_REF_NO,SCHEME_CD,to_char(SCHEME_CHANGE_DT,'YYYY-MM-DD') SCHEME_CHANGE_DT,NET_ID"
                                + ",APPLN_AC_NO,to_char(RATE_RESET_DATE,'YYYY-MM-DD') RATE_RESET_DATE,ONLINE_APP_FLG,MKR_ID,to_char(MKR_DT,'YYYY-MM-DD') MKR_DT,AUTH_ID"
                                + ",to_char(AUTH_DT,'YYYY-MM-DD') AUTH_DT,AC_NAME,POS_CD,GUAR_FLG,COVER_INSTR,CR_APPLN_AC_NO,DR_APPLN_AC_NO"
                                + ",TXN_BAL,LCY_TXN_BAL,LAST_TXN_NO,LL_NAME,NO_NOTICE_DAYS,AC_PROD_FEATURES"
                                + ",STMT_CYCLE,STMT_FREQ,STMT_DLRY_MD,PAM_CD_2,ACC_NAME_TC3,to_char(LST_BAL_CHG_DT,'YYYY-MM-DD') LST_BAL_CHG_DT" +
                                ",to_char(NGAY_BC,'YYYY-MM-DD') NGAY_BC from "
                                + FileName + " where " + FieldNgay + " " + toantu + " "
                                + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/MM/yyyy" + "')";
                        break;
                    case "CASA":
                        sql = "select  CS_MAPGD ,CS_MAKH ,CS_MATO ,CS_SO_TK ,CS_SO_TK2 ,CS_TENTK ,CS_SODU_TK ,CS_SP_TK ,CS_M_GUITK ,CS_M_RUTTK ,"
                              +
                              "CS_Q_GUITK ,CS_Q_RUTTK ,CS_A_GUITK ,CS_A_RUTTK ,CS_TTSO_TK , to_char(CS_NGAYBC,'YYYY-MM-DD') CS_NGAYBC ,CS_MACN ,"
                              +
                              " to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT ,CS_MADP ,to_char(CS_NGAYDKY,'YYYY-MM-DD') CS_NGAYDKY ,"
                              +
                              "to_char(CS_NGAYTT,'YYYY-MM-DD') CS_NGAYTT ,to_char(CS_NGAYROITO,'YYYY-MM-DD') CS_NGAYROITO from " +
                              FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" +
                              "," + "'dd/MM/yyyy" + "') order by CS_MAPGD,CS_MADP,CS_MATO,CS_MAKH ";
                        ;
                        break;
                    case "CASA_DAILY":
                        sql = "select CS_MACN , CS_MAPGD , CS_MAKH , CS_MATO , CS_SO_TK , CS_SO_TK2 , CS_TENTK , CS_SP_TK , CS_SODU_TK , CS_M_GUITK "
                               + ", CS_M_RUTTK , CS_Q_GUITK , CS_Q_RUTTK , CS_A_GUITK , CS_A_RUTTK , CS_TTSO_TK , CS_MADP ,to_char(CS_NGAYBC,'YYYY-MM-DD') CS_NGAYBC "
                                + ", to_char(CS_NGAYDKY,'YYYY-MM-DD') CS_NGAYDKY, to_char(CS_NGAYTT,'YYYY-MM-DD') CS_NGAYTT ,to_char(CS_NGAYROITO,'YYYY-MM-DD') CS_NGAYROITO  from " 
                               + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" +
                              "," + "'dd/MM/yyyy" + "') order by CS_MAPGD,CS_MADP,CS_MATO,CS_MAKH ";
                        break;
                        
                    case "HSTO":
                        sql = "select distinct TO_MATO , TO_LOAITO , TO_MATT , TO_TENTT , TO_DVUT , TO_HTUNTG , TO_HTUNTV , TO_KYDG , TO_MADP , TO_TKHH , TO_MAPGD, "
                              + " TO_MACN , TRANGTHAI ,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT from TMP_HSTO where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "') ";
                        break;
                    case "TMP_HSTG":
                        sql = "select MACN,MAPGD,GL_TK,SOTK,SOTK_0,MAKH,TENKH,MASP,SODU_SK,SODU_HD,LAIDUTHU,LAIDATRA"
                                + ",KYHAN,KYHAN_DV,PHANHE,INACT_ST,TRANGTHAI,NAMQT,to_char(NGAYTAO,'YYYY-MM-DD') NGAYTAO,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,LOAITIEN,LAISUAT"
                                + ",to_char(NGAYGUI,'YYYY-MM-DD') NGAYGUI,to_char(NGAYDENHAN,'YYYY-MM-DD') NGAYDENHAN,GOCTINHLAI,GOCDENHAN"
                                + ",to_char(NGAYDUTHUCUOI,'YYYY-MM-DD') NGAYDUTHUCUOI,to_char(NGAYTATTOAN,'YYYY-MM-DD') NGAYTATTOAN,LAINHAPGOC,MADP"
                                + " from TMP_HSTG where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "') ";
                        break;
                    case "QT_HSTG":
                        sql = "select MACN,MAPGD,GL_TK,SOTK,SOTK_0,MAKH,TENKH,MASP,SODU_SK,SODU_HD,LAIDUTHU,LAIDATRA"
                                + ",KYHAN,KYHAN_DV,PHANHE,INACT_ST,TRANGTHAI,NAMQT,to_char(NGAYTAO,'YYYY-MM-DD') NGAYTAO,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,LOAITIEN,LAISUAT"
                                + ",to_char(NGAYGUI,'YYYY-MM-DD') NGAYGUI,to_char(NGAYDENHAN,'YYYY-MM-DD') NGAYDENHAN,GOCTINHLAI,GOCDENHAN"
                                + ",to_char(NGAYDUTHUCUOI,'YYYY-MM-DD') NGAYDUTHUCUOI,to_char(NGAYTATTOAN,'YYYY-MM-DD') NGAYTATTOAN,LAINHAPGOC,MAPGD MADP"
                                + " from QT_HSTG where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "') ";
                        break;

                    case "HSKH":
                        sql = "select KH_MAKH , KH_TENKH ,to_char(KH_NGAYSINH,'YYYY-MM-DD') KH_NGAYSINH , KH_LOAIKH , KH_GIOITINH , KH_DANTOC , KH_CMT , KH_NOICAP ,"
                              + "to_char(KH_NGAYCAP,'YYYY-MM-DD') KH_NGAYCAP , KH_TENVC "
                              + ", KH_CMT_VC , KH_DIACHI , KH_MADP , KH_MOBILE , KH_TTRANG , KH_MAPGD , KH_MACN ,"
                              + "to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT  from hskh  "
                              +
                              "union select DN_MA as KH_MAKH ,DN_TEN as KH_TENKH ,to_char(DN_NGAYTL,'YYYY-MM-DD') as KH_NGAYSINH ,	DN_LOAIKH as KH_LOAIKH"
                              +
                              ",DN_PLOAI as KH_GIOITINH,	'' as KH_DANTOC,DN_MST as KH_CMT,'' as KH_NOICAP,to_char(DN_NGAYTL,'YYYY-MM-DD') as KH_NGAYCAP,DN_TGD as KH_TENVC,'' as KH_CMT_VC"
                              +
                              ",DN_DIACHI as KH_DIACHI,DN_MADP as KH_MADP,'' as KH_MOBILE,DN_TTRANG as KH_TTRANG ,DN_MAPGD as KH_MAPGD,DN_MACN as KH_MACN,"
                              + "to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT from HSKH_DN  ";
                        break;
                    case "PLKT":
                        sql = "select PL_SOKU,PL_TTRANG,PL_MAPNKT51,PL_MAPNKT52,PL_HQDT_CD,PL_HQDT_VAL1,PL_HQDT_VAL2,PL_HQDT_UNIT1"
                                + ",PL_HQDT_UNIT2,PL_MAPGD,PL_MACN,to_char(NG_CAPNHAT, 'YYYY-MM-DD') NG_CAPNHAT,PL_MDNHA,PL_MD30A,PL_MADA,PL_NGUONVON_BS,PL_SOLDLAPN"
                                + ",PL_SOLDLANKT,PL_SOLDLANTS,PL_GTRIVONVAY,PL_HQDT_CD2,PL_HQDT_CD3,PL_HQDT_CD4,PL_HQDT_CD5,PL_HQDT_CD6"
                                + ",PL_MAPNKT53,PL_MAPNKT54,PL_MAPNKT55,PL_MAPNKT56,PL_HQDT_VAL3,PL_HQDT_VAL4,PL_HQDT_VAL5,PL_HQDT_VAL6"
                                + ",PL_HQDT_UNIT3,PL_HQDT_UNIT4,PL_HQDT_UNIT5,PL_HQDT_UNIT6 from "
                                + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;

                    case "HSQH":
                        sql = "select QH_SOKU , to_char(QH_NGAYCQH,'YYYY-MM-DD') QH_NGAYCQH ,QH_GOCCQH,QH_LOAINN,QH_NGNHAN"
                             + ",QH_TRANGTHAI,QH_MAPGD,QH_MACN,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT from "
                             + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;

                    case "HSCV_DAILY":
                        sql = "select TT , KU_MAKH ,KU_SOKU , KU_MATO , to_char(KU_NGAYVAY,'YYYY-MM-DD') KU_NGAYVAY, to_char(KU_NGAYDHAN_1,'YYYY-MM-DD') KU_NGAYDHAN_1, "
                              +
                              "to_char(KU_NGAYDHAN_2,'YYYY-MM-DD') KU_NGAYDHAN_2,to_char(KU_NGAYDHAN_3,'YYYY-MM-DD') KU_NGAYDHAN_3,KU_HTHUCVAY , KU_SPRD_CD ,KU_CAPQLV ,KU_NGAYGDLD , "
                              +
                              "KU_LSUAT ,KU_DTTH ,KU_MANDT ,to_char(KU_NGAY_TGOC,'YYYY-MM-DD') KU_NGAY_TGOC,to_char(KU_NGAY_TLAI,'YYYY-MM-DD') KU_NGAY_TLAI , "
                              +
                              "to_char(KU_NGKTAHSV,'YYYY-MM-DD') KU_NGKTAHSV , KU_MAPNKT51 ,KU_MAPNKT52 ,KU_HQDT_CD ,	KU_HQDT_VAL1 , KU_HQDT_VAL2 , KU_MUCVAY ,	KU_GNGAN , "
                              +
                              "to_char(KU_NGAYGNDT,'YYYY-MM-DD') KU_NGAYGNDT , to_char(KU_NGAYGNCC,'YYYY-MM-DD') KU_NGAYGNCC , KU_DNOTHAN ,KU_DNOQHAN , KU_DNOKHOANH , "
                              +
                              "KU_TNOTHAN , KU_GOCDHAN ,	KU_GOCDTRA , KU_GOCXOA , KU_LAIXOA , KU_LAITHAN , KU_LAITONTHAN , KU_LAIQHAN , KU_LAITONQHAN , "
                              +
                              "KU_LAI_DT , KU_M_LAI_DT , KU_LAI_TT , KU_M_LAI_TT , KU_M_LAI_PB , KU_Q_LAI_PB , KU_A_LAI_PB , KU_M_LAI_KH , KU_Q_LAI_KH , "
                              +
                              "KU_A_LAI_KH , KU_LCDHAN_DT , KU_M_GNGAN , KU_GHANNO , KU_M_GHANNO , KU_CHUYENQH , to_char(KU_NGAYCNQH,'YYYY-MM-DD') KU_NGAYCNQH , "
                              +
                              "KU_M_CHUYENQH , KU_M_DKCHUYENQH , KU_CHUYENKH , KU_M_CHUYENKH ,	KU_TON_RPA , to_char(KU_NGAYGDGN,'YYYY-MM-DD') KU_NGAYGDGN , "
                              +
                              "KU_M_GOCXOA , to_char(KU_NGAY_DTHU,'YYYY-MM-DD') KU_NGAY_DTHU , KU_LAIHT_TONG ,	KU_LAIHT_CHT , KU_M_LUUVU , KU_M_DKGNGAN , KU_TTHAINO , KU_TTMONVAY , "
                              +
                              "KU_TKTHAN , KU_TKQHAN , KU_TKKHOANH , KU_TKTHULAI , KU_M_TNTHAN , KU_M_TNQHAN , KU_M_TNKHOANH , KU_SCHEM_CD , KU_PROD_CD , KU_NGUONVON , KU_CHTRINH , KU_MAQD , "
                              +
                              "KU_KYQUYFLG , KU_Q_GNGAN , KU_Q_LUUVU , KU_Q_DKGNGAN , KU_Q_GHANNO , KU_Q_CHUYENQH , KU_Q_CHUYENKH , KU_Q_TNTHAN , KU_Q_TNQHAN , KU_Q_TNKHOANH , KU_Q_GOCXOA , KU_Q_LAI_DT , "
                              +
                              "KU_Q_LAI_TT , KU_A_GNGAN , KU_A_LUUVU , KU_A_DKGNGAN , KU_A_GHANNO , KU_A_CHUYENQH , KU_A_CHUYENKH , KU_A_TNTHAN , KU_A_TNQHAN , KU_A_TNKHOANH , KU_A_GOCXOA , KU_A_LAI_DT , "
                              +
                              "KU_A_LAI_TT , KU_M_LAITHAN , KU_Q_LAITHAN , KU_A_LAITHAN , KU_M_LAIQHAN , KU_Q_LAIQHAN , KU_A_LAIQHAN , KU_TNTH , KU_TNQH , KU_TNKH , to_char(KU_LASTDUECRDT,'YYYY-MM-DD') KU_LASTDUECRDT , "
                              +
                              " to_char(KU_NGAYHHKH,'YYYY-MM-DD') KU_NGAYHHKH , KU_GOCHHKH , to_char(KU_NGAYLUUVU,'YYYY-MM-DD') KU_NGAYLUUVU , "
                              +
                              "KU_MAPGD , KU_MACN , to_char(KU_NGAYBC,'YYYY-MM-DD') KU_NGAYBC , KU_MADP , KU_CHUANNDP , KU_M_KHOANHCQHAN , KU_M_KHOANHCTHAN , "
                              +
                              "KU_M_THOAILAI , CS_SO_TK , CS_SO_TK2 , CS_TENTK , CS_SODU_TK , CS_M_GUITK , CS_M_RUTTK , CS_Q_GUITK , CS_Q_RUTTK , CS_A_GUITK , "
                              +
                              "CS_A_RUTTK , CS_TTSO_TK , to_char(CS_NGAYDKY,'YYYY-MM-DD') CS_NGAYDKY , to_char(CS_NGAYTT,'YYYY-MM-DD') CS_NGAYTT , "
                              + "to_char(CS_NGAYROITO,'YYYY-MM-DD') CS_NGAYROITO , KU_M_DAOKHOANTNO,PL_NGUONVON_BS  from " + FileName +
                              " where substr(ku_soku,1,1)='6' and " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," +
                              "'dd/mm/yyyy" + "') order by KU_MAPGD,KU_MADP,KU_MATO,KU_MAKH";
                        break;
                    case "HSKU":
                        sql = "	select KU_MAKH ,KU_SOKU , KU_MATO , "
                              +
                              "to_char(KU_NGAYVAY,'YYYY-MM-DD') KU_NGAYVAY, to_char(KU_NGAYDHAN_1,'YYYY-MM-DD') KU_NGAYDHAN_1, "
                              +
                              "to_char(KU_NGAYDHAN_2,'YYYY-MM-DD') KU_NGAYDHAN_2, to_char(KU_NGAYDHAN_3,'YYYY-MM-DD') KU_NGAYDHAN_3, "
                              +
                              "KU_HTHUCVAY , KU_SPRD_CD ,KU_CAPQLV ,KU_NGAYGDLD ,KU_LSUAT ,KU_DTTH ,KU_MANDT ,  to_char(KU_NGAY_TGOC,'YYYY-MM-DD') KU_NGAY_TGOC, "
                              +
                              "to_char(KU_NGAY_TLAI,'YYYY-MM-DD') KU_NGAY_TLAI ,  to_char(KU_NGKTAHSV,'YYYY-MM-DD') KU_NGKTAHSV ,  KU_MAPNKT51 ,"
                              +
                              "KU_MAPNKT52 ,KU_HQDT_CD ,	KU_HQDT_VAL1 ,KU_HQDT_VAL2 , KU_MUCVAY ,	KU_GNGAN ,  to_char(KU_NGAYGNDT,'YYYY-MM-DD') KU_NGAYGNDT , "
                              +
                              "to_char(KU_NGAYGNCC,'YYYY-MM-DD') KU_NGAYGNCC ,  KU_DNOTHAN ,KU_DNOQHAN , KU_DNOKHOANH ,  KU_TNOTHAN , KU_GOCDHAN ,	KU_GOCDTRA , "
                              +
                              "KU_GOCXOA , KU_LAIXOA , KU_LAITHAN ,  KU_LAITONTHAN , KU_LAIQHAN , KU_LAITONQHAN ,  KU_LAI_DT , KU_LAI_TT ,KU_LCDHAN_DT , KU_M_GNGAN ,  "
                              +
                              "KU_GHANNO , KU_M_GHANNO , KU_CHUYENQH ,  to_char(KU_NGAYCNQH,'YYYY-MM-DD') KU_NGAYCNQH ,  KU_M_CHUYENQH , KU_CHUYENKH ,  KU_M_CHUYENKH ,"
                              +
                              "KU_TON_RPA , to_char(KU_NGAYGDGN,'YYYY-MM-DD') KU_NGAYGDGN ,  KU_M_GOCXOA , to_char(KU_NGAY_DTHU,'YYYY-MM-DD') KU_NGAY_DTHU ,  "
                              +
                              "KU_LAIHT_TONG ,	KU_LAIHT_CHT , KU_M_LUUVU , KU_M_DKGNGAN , KU_TTHAINO , KU_TTMONVAY ,  KU_TKTHAN , KU_TKQHAN , KU_TKKHOANH , "
                              +
                              "KU_TKTHULAI , KU_M_TNTHAN , KU_M_TNQHAN ,  KU_M_TNKHOANH , KU_SCHEM_CD , KU_PROD_CD , KU_NGUONVON , KU_CHTRINH , KU_MAQD ,  "
                              +
                              "KU_KYQUYFLG , KU_Q_GNGAN , KU_Q_LUUVU , KU_Q_DKGNGAN , KU_Q_GHANNO , KU_Q_CHUYENQH ,  KU_Q_CHUYENKH , KU_Q_TNTHAN , KU_Q_TNQHAN , "
                              +
                              "KU_Q_TNKHOANH , KU_Q_GOCXOA , KU_Q_LAI_DT ,  KU_Q_LAI_TT , KU_A_GNGAN , KU_A_LUUVU , KU_A_DKGNGAN , KU_A_GHANNO , KU_A_CHUYENQH ,  "
                              +
                              "KU_A_CHUYENKH , KU_A_TNTHAN , KU_A_TNQHAN , KU_A_TNKHOANH , KU_A_GOCXOA , KU_A_LAI_DT ,  KU_A_LAI_TT , KU_M_LAITHAN , KU_Q_LAITHAN , "
                              +
                              "KU_A_LAITHAN , KU_M_LAIQHAN , KU_Q_LAIQHAN ,  KU_A_LAIQHAN , KU_TNTH , KU_TNQH , KU_TNKH ,  KU_MAPGD , KU_MACN , "
                              +
                              "to_char(KU_NGAYBC,'YYYY-MM-DD') KU_NGAYBC , KU_MADP , KU_CHUANNDP ,  to_char(KU_NGAYHHKH,'YYYY-MM-DD') KU_NGAYHHKH , KU_GOCHHKH , "
                              + "to_char(KU_NGAYLUUVU,'YYYY-MM-DD') KU_NGAYLUUVU  from " + FileName + " where " +
                              FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" +
                              "') order by KU_MAPGD,KU_MADP,KU_MATO,KU_MAKH ";
                        break;
                    case "HSSV":
                        sql = "select SV_SOKU , SV_MASV , SV_TENSV , to_char(SV_NGSINH_SV,'YYYY-MM-DD') SV_NGSINH_SV , SV_GTINH_SV , SV_CMT_SV "
                                + ", SV_MATRUONG , SV_LOAIHDT , SV_LOAIHCS , SV_HEDTAO , SV_NGANHDT , SV_DTHOCPHI , to_char(SV_NGNHAPHOC,'YYYY-MM-DD') SV_NGNHAPHOC "
                                + ", to_char(SV_NGRTRUONG,'YYYY-MM-DD') SV_NGRTRUONG , SV_SO_ATM , SV_DVCAPTHE , SV_DTSV , SV_TTHAISV , SV_MAPGD , SV_MACN "
                                + ", SV_REC_ST , to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT , SV_CLASS , SV_COURCE , SV_FACULTY , SV_IDNO  from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "KHTN":
                        sql = "select  KH_SOKU , KH_LANTNO , to_char(KH_NGDHAN,'YYYY-MM-DD') KH_NGDHAN , KH_GOCDHAN , KH_LAIDHAN , KH_LAITONPB , KH_DUNO , KH_GOCDTRA , KH_LAIDTRA "
                               + ", KH_STHTRO , KH_MAPGD , KH_MACN ,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT   from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "KHGN":
                        sql = "select KH_SOKU , KH_LANGNGAN ,to_char(KH_NGGNGAN,'YYYY-MM-DD') KH_NGGNGAN , KH_STGNGAN , KH_LSUAT , KH_MAHTLS "
                                + ",to_char(KH_NGAYBDHT,'YYYY-MM-DD') KH_NGAYBDHT ,to_char(KH_NGAYKTHT,'YYYY-MM-DD') KH_NGAYKTHT , KH_LSUATHT , KH_NGUONHT , KH_MAPGD , KH_MACN "
                                + ",to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT , KH_DGNGAN_FLG  from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "QT_MS02TL":
                        sql = "select MACN,MAPGD,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,D2,D3,D4,D5,D6,D7,D8,D9,D10,D11,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT "
                                + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "QT_MS04TL":
                        sql = "select MACN ,MAPGD,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,CHTRINH,D1,D2,D3,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT "
                                + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "QT_MS14":
                        sql = "select MAPGD,NGAYBC,KEY,D1,D2,D3,D4,D5,D6,D7,D8,D9,D10,D11,D12,D13"
                               + ",D14,D15,D16,D17,D18,D19,D20,D21,D22,D23,D24,D25,D26"
                               + ",D27,D28,D29,D30,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT,MACN,D31,D32"
                                + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "CT_VBSP":
                        sql = "select CT_MACT,CT_KIEUGIATRI,CT_GIATRI,to_char(CT_NGAYBC,'YYYY-MM-DD') CT_NGAYBC,CT_MAPGD,CT_MACN,to_char(NGAY_TAO,'YYYY-MM-DD') NGAY_TAO,CT_CAPTH,CT_IDCTG"
                                + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "GL_VBSP":
                        sql = "select GL_TK,GL_TENTK,GL_TKCAP3,GL_SL,GL_LOAITIEN,GL_DD_NO,GL_DD_CO,GL_PS_NO,GL_PS_CO,GL_DC_NO,GL_DC_CO"
                            + ",GL_DD_NO_NT,GL_DD_CO_NT,GL_PS_NO_NT,GL_PS_CO_NT,GL_DC_NO_NT,GL_DC_CO_NT,to_char(GL_NGAYBC,'YYYY-MM-DD') GL_NGAYBC,GL_MAPGD,GL_MACN"
                                + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "GL_VBSP_TH":
                        sql = "select GL_TK,GL_TENTK,GL_TKCAP3,GL_SL,GL_LOAITIEN,GL_DD_NO,GL_DD_CO,GL_PS_NO,GL_PS_CO,GL_DC_NO"
                            + ",GL_DC_CO,GL_DD_NO_NT,GL_DD_CO_NT,GL_PS_NO_NT,GL_PS_CO_NT,GL_DC_NO_NT,GL_DC_CO_NT,to_char(GL_NGAYBC,'YYYY-MM-DD') GL_NGAYBC"
                            + ",GL_CAPTH,GL_KYBC,GL_MAPGD,GL_MACN"
                                + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "HSBT":
                        sql = "select SBT,TK,TK_NO,TK_CO,NOCO,MOD_CD,TXN_CD,SUBTXN_CD,to_char(NGAYGD,'YYYY-MM-DD') NGAYGD,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,TIENTE,ST_NGUYENTE"
                            + ",SOTIEN,GHICHU_1,GHICHU_2,GDV,KSV,MAPGD,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT"
                                + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "ONLINE_01TL":
                        sql = "select POS_CD,GROUP_ID,MASS_ORG,PROD_DESC,CUST_NAME,CUST_ADRS,GROUP_NAME,LEGACY_ID,REF_NO,TXN_TYPE,to_char(TXN_DATE,'YYYY-MM-DD') TXN_DATE"
                            + ",DISB_AMT,PRIN_PAID,INT_PAID,INT_RT,PRIN_OS,AUTH_ID,MAKER_ID,to_char(MAKER_DT,'YYYY-MM-DD') MAKER_DT,LOAN_PGM,CUST_ID,CIVIL_ID,to_char(ISSUE_DT,'YYYY-MM-DD') ISSUE_DT,ISSUE_PLC,to_char(EOD_DT,'YYYY-MM-DD') EOD_DT,COMMUNE_ID"
                                + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "HSLV_HISTORY":
                        sql = "select SOKU,SPRD_CD,MAPGD,to_char(NGAYDK_1,'YYYY-MM-DD') NGAYDK_1,to_char(NGAYDH_1,'YYYY-MM-DD') NGAYDH_1,to_char(NGAYDK_2,'YYYY-MM-DD') NGAYDK_2"
                              + " ,to_char(NGAYDH_2,'YYYY-MM-DD') NGAYDH_2,LSUAT_1,LSUAT_2,ST_DHAN,ST_LUUVU"
                              + " ,to_char(NGAYHL,'YYYY-MM-DD') NGAYHL,to_char(NGAYTAO,'YYYY-MM-DD') NGAYTAO,SOTHANG,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT,MACN,TRANGTHAI"
                              + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "HSGH_HISTORY":
                        sql = "select MAPGD,SOKU,MAKH,MACN,CHEQ_HIST,GH_TSLAN,GH_LAN,to_char(GH_NGAY,'YYYY-MM-DD') GH_NGAY,GH_SOTIEN,GH_SOTHG,GH_TSOTHG "
                            + ",GH_TSOTIEN,GH_LOAINV,SPRD_CD,GH_MAQD,to_char(GH_NGAYQD,'YYYY-MM-DD') GH_NGAYQD,GH_NGNHAN,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT "
                            + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')"; 
                        break;
                    case "TXN_POINT_INFO_MB":
                        sql = "select TPI_ID,TPI_DATE,TPI_DESC,FILE_GEN_FLAG,TPI_POS,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT"
                            + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "TXNPOINT_DETAIL":
                        sql = "select POS_CODE,POS_DESC,TXNPOINT_ID,TPI_DESC,MAKER_ID,MAKER_DT,CMUNE_VISIT_DATE,UPL_CHECK,CMUNE_VISIT_FLAG"
                                + ",CMUNE_FLAG_CHNG_BY,UPL_TIME,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC"
                            + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "DG_CASA105_DATA":
                        sql = "select MAPGD,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,MAKH,TK,TK1,SODUDAUKY,GUITK,CKTRANO_TONGSO,CKTRALAI,CKTRAGOC,CKTRAGOC_TUSDKYTRC,CKTRALAI_TUSDKYTRC "
                               + ",RUTTK,LAINHAPGOC,SODUCUOIKY,CHENHLECHSODU"
                            + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "QT_TSCC":
                        sql = "select MA_TS,TEN_TS,LOAI_TS,TEN_LOAI_TS,LOAI_TS_CHITIET,TEN_LOAI_TS_CHITIET,MA_NHANHIEU_TS,TEN_NHANHIEU_TS,NGUYEN_GIA"
                                + ",SO_LUONG,VON_TW,VON_DP,VON_KHAC,HAOMON_LK,POS_CD,MAIN_POS,NAMQT,to_char(NGAYTAO,'YYYY-MM-DD') NGAYTAO,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,to_char(NGAY_MUA,'YYYY-MM-DD') NGAY_MUA,MAPHONG,TENPHONG"
                            + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "QT_TSTL":
                        sql = "select MA_TS,TEN_TS,LOAI_TS,TEN_LOAI_TS,LOAI_TS_CHITIET,TEN_LOAI_TS_CHITIET,MA_NHANHIEU_TS,to_char(NGAY_SDUNG,'YYYY-MM-DD') NGAY_SDUNG,THOIGIAN_SD,NGUYEN_GIA,HAOMON_LK "
                                + ",to_char(NGAY_TLY,'YYYY-MM-DD') NGAY_TLY,CHIPHI_TLY,THUTU_TLY,POS_CD,MAIN_POS,NAMQT,to_char(NGAYTAO,'YYYY-MM-DD') NGAYTAO,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,to_char(NGAY_MUA,'YYYY-MM-DD') NGAY_MUA"
                            + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "TTLDDDAILYLOAN":
                        sql = "select KU_MAKH ,KU_SOKU,KU_MATO,to_char(KU_NGAYVAY,'YYYY-MM-DD') KU_NGAYVAY,to_char(KU_NGAYDHAN_1,'YYYY-MM-DD') KU_NGAYDHAN_1"
                              + ",to_char(KU_NGAYDHAN_2,'YYYY-MM-DD') KU_NGAYDHAN_2,to_char(KU_NGAYDHAN_3,'YYYY-MM-DD') KU_NGAYDHAN_3 "
                              + ",KU_HTHUCVAY,KU_SPRD_CD,KU_CAPQLV,KU_NGAYGDLD,KU_LSUAT,KU_DTTH,KU_MANDT,to_char(KU_NGAY_TGOC,'YYYY-MM-DD') KU_NGAY_TGOC"
                              + ",to_char(KU_NGAY_TLAI,'YYYY-MM-DD') KU_NGAY_TLAI,to_char(KU_NGKTAHSV,'YYYY-MM-DD') KU_NGKTAHSV,KU_MAPNKT51,KU_MAPNKT52"
                              + ",KU_HQDT_CD,KU_HQDT_VAL1,KU_HQDT_VAL2"
                              + ",KU_MUCVAY,KU_GNGAN,to_char(KU_NGAYGNDT,'YYYY-MM-DD') KU_NGAYGNDT,to_char(KU_NGAYGNCC,'YYYY-MM-DD') KU_NGAYGNCC,KU_DNOTHAN,KU_DNOQHAN,KU_DNOKHOANH"
                              + ",KU_TNOTHAN,KU_GOCDHAN,KU_GOCDTRA,KU_GOCXOA,KU_LAIXOA,KU_LAITHAN,KU_LAITONTHAN"
                              + ",KU_LAIQHAN,KU_LAITONQHAN,KU_LAI_DT,KU_M_LAI_DT,KU_LAI_TT,KU_M_LAI_TT,KU_M_LAI_PB"
                              + ",KU_Q_LAI_PB,KU_A_LAI_PB,KU_M_LAI_KH,KU_Q_LAI_KH,KU_A_LAI_KH,KU_LCDHAN_DT,KU_M_GNGAN"
                              + ",KU_GHANNO,KU_M_GHANNO,KU_CHUYENQH,to_char(KU_NGAYCNQH,'YYYY-MM-DD') KU_NGAYCNQH,KU_M_CHUYENQH,KU_M_DKCHUYENQH,KU_CHUYENKH"
                              + ",KU_M_CHUYENKH,KU_TON_RPA,to_char(KU_NGAYGDGN,'YYYY-MM-DD') KU_NGAYGDGN,KU_M_GOCXOA,to_char(KU_NGAY_DTHU,'YYYY-MM-DD') KU_NGAY_DTHU,KU_LAIHT_TONG,KU_LAIHT_CHT"
                              + ",KU_M_LUUVU,KU_M_DKGNGAN,KU_TTHAINO,KU_TTMONVAY,KU_TKTHAN,KU_TKQHAN,KU_TKKHOANH,KU_TKTHULAI"
                              + ",KU_M_TNTHAN,KU_M_TNQHAN,KU_M_TNKHOANH,KU_SCHEM_CD,KU_PROD_CD,KU_NGUONVON,KU_CHTRINH"
                              + ",KU_MAQD,KU_KYQUYFLG,KU_Q_GNGAN,KU_Q_LUUVU,KU_Q_DKGNGAN,KU_Q_GHANNO,KU_Q_CHUYENQH,KU_Q_CHUYENKH"
                              + ",KU_Q_TNTHAN,KU_Q_TNQHAN,KU_Q_TNKHOANH,KU_Q_GOCXOA,KU_Q_LAI_DT,KU_Q_LAI_TT,KU_A_GNGAN,KU_A_LUUVU"
                              + ",KU_A_DKGNGAN,KU_A_GHANNO,KU_A_CHUYENQH,KU_A_CHUYENKH,KU_A_TNTHAN,KU_A_TNQHAN,KU_A_TNKHOANH"
                              + ",KU_A_GOCXOA,KU_A_LAI_DT,KU_A_LAI_TT,KU_M_LAITHAN,KU_Q_LAITHAN,KU_A_LAITHAN,KU_M_LAIQHAN"
                              + ",KU_Q_LAIQHAN,KU_A_LAIQHAN,KU_TNTH,KU_TNQH,KU_TNKH,to_char(KU_LASTDUECRDT,'YYYY-MM-DD') KU_LASTDUECRDT,to_char(KU_NGAYHHKH,'YYYY-MM-DD') KU_NGAYHHKH,KU_GOCHHKH"
                              + ",to_char(KU_NGAYLUUVU,'YYYY-MM-DD') KU_NGAYLUUVU,KU_MAPGD,KU_MACN,to_char(KU_NGAYBC,'YYYY-MM-DD') KU_NGAYBC,KU_MADP,KU_CHUANNDP,KU_M_KHOANHCQHAN,KU_M_KHOANHCTHAN"
                              + ",KU_M_THOAILAI,KU_M_DAOKHOANTNO,PL_NGUONVON_BS "
                            + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "PLN_KNTN_CL":
                        sql = "select PLN_SOKU,PLN_MAKH,PLN_TENKH,PLN_MATO,PLN_DVUT,PLN_MADP,PLN_NGUONVON,PLN_SPRD_CD,PLN_CHTRINH,PLN_MAQD,PLN_DNOTHAN,PLN_DNOQHAN "
                              + " ,PLN_DNOKHOANH,PLN_LAITHAN_TT,PLN_LAIQHAN_TT,PLN_TONGLAI_TT,PLN_LAITONTHAN,PLN_LAITONQHAN,PLN_TONGLAITON,PLN_C_KNTN_SODU "
                              + " ,PLN_K_KNTN_SODU,PLN_K_KNTN_SD01,PLN_K_KNTN_SD02,PLN_K_KNTN_SD03,PLN_K_KNTN_SD04,PLN_K_KNTN_SD05,PLN_K_KNTN_SD06,PLN_K_KNTN_SD07 "
                              + " ,PLN_K_KNTN_SD08,PLN_K_KNTN_SD09,PLN_K_KNTN_SD10,PLN_K_KNTN_SD11,PLN_K_NGNHAN_KH,PLN_QUANHE_KH,PLN_TRANGTHAI,PLN_NOGOC_CLECH "
                              + " ,PLN_NOLAI_CLECH,PLN_NGNHAN_CLECH,PLN_TT_MONVAY,to_char(PLN_NGAYBC,'YYYY-MM-DD') PLN_NGAYBC,PLN_NGUOI_PLN"
                              + " ,to_char(PLN_NGAY_PLN,'YYYY-MM-DD') PLN_NGAY_PLN,PLN_MAPGD,PLN_MACN,to_char(PLN_NGAYCN,'YYYY-MM-DD') PLN_NGAYCN,PLN_TENTT "
                              + " ,PLN_TRANGTHAINO,PLN_MATT,PLN_LOAITO,PLN_TRANGTHAITT,to_char(PLN_NGAY_TT,'YYYY-MM-DD') PLN_NGAY_TT"
                            + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "DG_DATA":
                        sql = "select REPORT_KEY,MAIN_POS,POS_CD,POS_FLAG,to_char(REPORT_DT,'YYYY-MM-DD') REPORT_DT,TERM_FLG,COMMUNE_ID,GROUP_ID,INDICATOR,VALUE,OUTPOINT,MARK"
                             + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "DG_MARK":
                        sql = "select REPORT_KEY,MAIN_POS,POS_CD,POS_FLAG,to_char(REPORT_DT,'YYYY-MM-DD') REPORT_DT,TERM_FLG,COMMUNE_ID,GROUP_ID,TOTAL_MARK,CLASSIFICATION,DESCRIPTION"
                             + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                    case "I_USER":
                        sql = "select IU_MA,IU_TEN,IU_MAPGD,IU_TTRANG,IU_NV,CMT,to_char(NGAYHH,'YYYY-MM-DD') NGAYHH,NGUOITAO,to_char(NGAYTAO,'YYYY-MM-DD') NGAYTAO" +
                              ",NGUOIKT,to_char(NGAYKT,'YYYY-MM-DD') NGAYKT,CHUCVU,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC"
                             + " from " + FileName;
                        break;
                    case "HSRR_WB":
                        sql = "select RR_SOKU,RR_MAKH,RR_TENKH,RR_DIACHI,RR_CHTRINH,RR_SPRD_CD,RR_DQ_STAT_CD,RR_DNGOC,RR_LAITH"
                                 + " ,RR_LAIQH,RR_DNGHI_DNO,RR_DNGHI_LAI,RR_XL_DUNO,RR_XL_LAI,RR_HT_DNO,RR_HT_LAI,to_char(RR_NGAYVAY,'YYYY-MM-DD') RR_NGAYVAY"
                                 + " ,to_char(RR_NGAYDH,'YYYY-MM-DD') RR_NGAYDH,RR_THOIHANVAY,RR_MDTHIETHAI,to_char(RR_NGAYRR,'YYYY-MM-DD') RR_NGAYRR,RR_DNGHI_TG,RR_PDUYET_TG,RR_NGUYENNHAN"
                                 + " ,RR_MOTANN,RR_TRANGTHAI,to_char(RR_PDUYET_NGAY_CN,'YYYY-MM-DD') RR_PDUYET_NGAY_CN,RR_PDUYET_NGUOI_CN,to_char(RR_PDUYET_NGAY_TW,'YYYY-MM-DD') RR_PDUYET_NGAY_TW,RR_PDUYET_NGUOI_TW"
                                 + " ,RR_TAOLAP_NGUOI,to_char(RR_TAOLAP_NGAY,'YYYY-MM-DD') RR_TAOLAP_NGAY,RR_MAQD,RR_TENQD,RR_NHOMRR,RR_MAPGD,RR_MACN,to_char(RR_NGAYBC,'YYYY-MM-DD') RR_NGAYBC,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT "
                                 + " ,to_char(RR_NGAYTAO,'YYYY-MM-DD') RR_NGAYTAO,RR_NGUOITAO,RR_PDUYET_CAP,to_char(RR_NGAYHL,'YYYY-MM-DD') RR_NGAYHL,RR_HT_TKXOANO,RR_NGUONVON,RR_DOTRR,RR_MADP"
                                 + " ,RR_MATO,RR_SOLANXL,RR_PDUYET_PGD,to_char(RR_NGAY_PDUYET_PGD,'YYYY-MM-DD') RR_NGAY_PDUYET_PGD,RR_NGUYENNHAN_TUCHOI,to_char(RR_INT_PDUYET_NGAY_CN,'YYYY-MM-DD') RR_INT_PDUYET_NGAY_CN"
                                 + " ,RR_INT_PDUYET_NGUOI_CN,RR_NGUYENNHAN_TC_CN,RR_KHOA"
                             + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')"
                             ; break;
                    case "HSXN_HISTORY":
                        sql = "select MAPGD,SOKU,MAKH,MACN,ST_GOC,ST_LAITH,to_char(NGAYRR,'YYYY-MM-DD') NGAYRR,TTMONVAY,ST_LAIQH"
                              + ",SPRD_CD,to_char(NGAYTAO,'YYYY-MM-DD') NGAYTAO,to_char(NGAYHL,'YYYY-MM-DD') NGAYHL,MAQD,to_char(NGAYQD,'YYYY-MM-DD') NGAYQD,NGNHAN,TRANGTHAI,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT"
                             + " from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')"
                             ; break;
                    case "HSBT_145NO":
                        sql = "select SBT,TK,TK_NO,TK_CO,NOCO,MOD_CD,TXN_CD,SUBTXN_CD,to_char(NGAYGD,'YYYY-MM-DD') NGAYGD,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,TIENTE,ST_NGUYENTE, "
                               + " SOTIEN,'Nop tien vao TK145 ' GHICHU_1, '-' GHICHU_2,GDV,KSV,MAPGD,to_char(NG_CAPNHAT, 'YYYY-MM-DD') NG_CAPNHAT,concat(TK, to_char(NGAYGD, 'YYYY-MM-DD')) KHOA "
                               + " from HSBT where  ngaygd between " + "to_date(" + "'" + TuNgay + "'" + "," + "'dd/mm/yyyy" + "')" + " and " + "to_date(" + "'" + DenNgay + "'" + "," + "'dd/mm/yyyy" + "')" + " and mod_cd = 'CT' "
                               + " and tk in (select cs_so_tk from casa_daily where cs_sp_tk = '145' and CS_NGAYBC = (select max(CS_NGAYBC)   from casa_daily))";
                        break;
                    case "DULIEU_NT":
                        sql = "select KHOA,THUTU,TT_HIENTHI,MA,TEN,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,NAMBC,MAPGD,CO_TONGHOP,MACN,NGUOI_NHAP,to_char(NGAY_NHAP,'YYYY-MM-DD')  NGAY_NHAP,NGUOI_DUYET,to_char(NGAY_DUYET,'YYYY-MM-DD')  NGAY_DUYET,D1,D2,D3,D4,D5,D6,D7,D8,D9      ,D10,D11,D12,D13,D14,D15,D16,D17,D18,D19,D20,D21,D22,D23,D24,D25,D26,D27,D28,D29,D30,NHAPTAY,FONTFORMAT,KIEUIN,D31,D32,D33,D34,D35,D36,D37,D38,D39,D40,D41,D42,D43,D44,D45,D46,D47,D48,D49,D50 from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" + "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;

                    default:
                        sql = "select * from " + FileName + " where " + FieldNgay + " " + toantu + " " + "to_date(" +
                              "'" + NgayBc + "'" + "," + "'dd/mm/yyyy" + "')";
                        break;
                }
            try
                {
                   // MessageBox.Show(sql);
                    dt = cls.LoadDataText(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }               
                //MessageBox.Show("Load OK", "Thông báo");
                finally
                {
                    cls.DongKetNoi();
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn ngày ! ");
            }
            if (dt.Rows.Count > 0)
            {
                dgvNguon.ItemsSource = dt.DefaultView;
            }
            else
            {
                MessageBox.Show("Số liệu "+FileName+" ngày  " + NgayBc + " Chưa có !","Thông báo");
            }
            cls.DongKetNoi();

        }

        private void btnReadFile_Click(object sender, RoutedEventArgs e)
        {
            cboFile.Items.Clear();
            string sql = "";
            if (Ration4.IsChecked == true)
            {
                sql = "select * from systable where ky='D' order by table_name ";
                LoadFile(dtFile, sql);
            }
            else if (Ration5.IsChecked == true)
            {
                sql = "select * from systable where ky='M' order by table_name ";
                LoadFile(dtFile, sql);
            } else if (Ration6.IsChecked == true)
            {
                sql = "select * from systable where ky='L' order by table_name ";
                LoadFile(dtFile, sql);
            }
            else if (Ration7.IsChecked == true)
            {
                sql = "select * from systable where ky='B' order by table_name ";
                LoadFile(dtFile, sql);
            }
            foreach (DataRow dr in dtFile.Rows)
            {
                //dgvDich.ItemsSource = dtFile.DefaultView;
                //MessageBox.Show(dr[0].ToString() + "   " + dr[1].ToString());
                string ngay = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
                string toantu = "";
                if (Ration1.IsChecked == true)
                {
                    toantu = "=";
                }
                else if (Ration2.IsChecked == true)
                {
                    toantu = ">";
                }
                else if (Ration3.IsChecked == true)
                {
                    toantu = "<";
                }
                string expFile = txtPath.Text.Trim() + bll.XoaHetKyTu(dr[0].ToString().Trim(), "_") + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".txt";
                if (File.Exists(expFile))
                {
                    if (MessageBox.Show("Đã có file : " + expFile + "OverWrite ? ", "Question", MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                       // MessageBox.Show("You has Select No");
                    }
                    else
                    {
                        ReadFile(dr[0].ToString().Trim(), dr[1].ToString().Trim(), ngay, toantu);
                        if (dt.Rows.Count > 0) WriteText(expFile);
                    }
                }
                else
                {
                    ReadFile(dr[0].ToString().Trim(), dr[1].ToString().Trim(), ngay, toantu);
                    if (dt.Rows.Count > 0) WriteText(expFile);
                }

                //Insert_Text(dr[0].ToString().Trim(), dr[1].ToString().Trim(), ngay);
            }
            MessageBox.Show("Export text OK");
        }

        private void Insert_Text(String FileName, String FieldNgay,String NgayBc)
        {
            string exitsFile = txtPath.Text.Trim() + bll.XoaHetKyTu(FileName, "_") +dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".txt";
            if (File.Exists(exitsFile))
            {
                try
                {
                    #region

                    string sql = "";
                    ClsServer cls = new ClsServer();
                    cls.ClsConnect();
                    DataTable dtkt = new DataTable();
                    if (FileName == "QT_MS14")
                    {
                        sql = "select top 1 * from QT_MS14  where NGAYBC = " + "'" +
                              bll.Right(dtpNgay.SelectedDate.Value.ToString("MMyyyy"), 6) + "'";
                    }
                    else
                    {
                        //sql = "select top 1 * from " + FileName + " where " + "left(" + FieldNgay + ",10) = " + "'" +NgayBc + "'";
                        sql = "select top 1 * from " + FileName + " where "+FieldNgay+ "='" + NgayBc + "'";
                    }
                    // MessageBox.Show(sql);
                    dtkt = cls.LoadDataText(sql);
                    dgvDich.ItemsSource = dtkt.DefaultView;
                    if (dtkt.Rows.Count > 0)
                    {
                        #region

                        string mess = "Đã có số liệu "+FileName+" ngày : " + NgayBc + " Có muốn Insert ?";
                        if (MessageBox.Show(mess, "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) ==
                            MessageBoxResult.No)
                        {
                            //do no stuff
                            //MessageBox.Show("Select No");
                            File.Delete(txtPath.Text.Trim() + bll.XoaHetKyTu(FileName, "_") +dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".txt");
                        }
                        else
                        {
                            // lbl.Content = "Đang Insert ...";
                            //do yes stuff
                            int thamso = 3;
                            string[] bien = new string[thamso];
                            object[] giatri = new object[thamso];
                            bien[0] = "@FileName";
                            giatri[0] = FileName;
                            bien[1] = "@PathDir";
                            giatri[1] = txtPath.Text.Trim() + bll.XoaHetKyTu(FileName, "_") +
                                        dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".txt";
                            bien[2] = "@Ngay";
                            giatri[2] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                            if (File.Exists(giatri[1].ToString().Trim()))
                            {
                                //MessageBox.Show(giatri[0] + "  " + giatri[1]);
                                //Doan nay phai xu ly xoa so lieu
                                // MessageBox.Show(FileName + "  " + FieldNgay + "   " + NgayBc);
                                DeleteData(FileName, FieldNgay, NgayBc);
                                //cls.UpdateDataProcPara("usp_InsertText", bien, giatri, thamso);
                                cls.UpdateLdbf("usp_InsertText", bien, giatri, thamso);
                                File.Delete(giatri[1].ToString().Trim());
                                //==============04/11/2016 bo sung them phan chinh sua KH co MAPOS<>MADP trong CASA
                                if (giatri[0].ToString().Trim() == "CASA" || giatri[0].ToString().Trim() == "CASA_DAILY")
                                {
                                    string sqlcd = "update CASA set CS_MAPGD='00'+LEFT(CS_MADP,4) where CS_NGAYBC='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and CS_TTSO_TK='A' and CS_SP_TK='105' and CS_MAPGD<>'00'+LEFT(CS_MADP,4)";
                                    cls.UpdateDataText(sqlcd);
                                }
                                if (giatri[0].ToString().Trim() == "QT_HSTG")
                                {
                                    string sqltg = "update a set a.MADP=b.KH_MADP from QT_HSTG a,HSKH b where a.MAKH=b.KH_MAKH and a.NGAYBC='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "'";
                                    cls.UpdateDataText(sqltg);
                                }
                                if (giatri[0].ToString().Trim() == "HSKH")
                                {
                                    string sqlkh = "with lst1 as ( select KH_MAKH, COUNT(KH_MAKH)  DEM from HSKH group by KH_MAKH having COUNT(KH_MAKH) > 1) "
                                                    +" delete a from HSKH a,lst1 b where a.KH_MAKH = b.KH_MAKH and LEFT(a.KH_MADP, 4)<> RIGHT(a.KH_MAPGD, 4)";
                                    cls.UpdateDataText(sqlkh);
                                }

                                MessageBox.Show("Insert OK : " + giatri[1]);
                            }
                            else
                            {
                                MessageBox.Show(" Chưa có file : " + giatri[1].ToString().Trim());
                            }
                            // lbl.Content = "";
                        }

                        #endregion
                    }
                    else
                    {
                        #region

                        //do yes stuff
                        int thamso = 3;
                        string[] bien = new string[thamso];
                        object[] giatri = new object[thamso];
                        bien[0] = "@FileName";
                        giatri[0] = FileName;
                        bien[1] = "@PathDir";
                        giatri[1] = txtPath.Text.Trim() + bll.XoaHetKyTu(FileName, "_") +dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".txt";
                        bien[2] = "@Ngay";
                        giatri[2] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                        if (File.Exists(giatri[1].ToString().Trim()))
                        {
                            //cls.UpdateDataProcPara("usp_InsertText", bien, giatri, thamso);
                            cls.UpdateLdbf("usp_InsertText", bien, giatri, thamso);
                            File.Delete(giatri[1].ToString().Trim());
                        }
                        if (giatri[0].ToString().Trim() == "CT_VBSP")
                        {
                            string sqlcd = "insert into U_CANDOI (NGAY)  values ('" +
                                           dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") + "')";
                            cls.UpdateDataText(sqlcd);
                        }
                        if (giatri[0].ToString().Trim() == "HSKU" || giatri[0].ToString().Trim() == "HSCV_DAILY")
                        {
                            string sqlcd = "insert into U_HSTD (NGAYKU)  values ('" +
                                           dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") + "')";
                            cls.UpdateDataText(sqlcd);
                        }
                        if (giatri[0].ToString().Trim() == "CASA" || giatri[0].ToString().Trim() == "CASA_DAILY")
                        {
                            string sqlcd = "update CASA set CS_MAPGD='00'+LEFT(CS_MADP,4) where CS_NGAYBC='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and CS_TTSO_TK='A' and CS_SP_TK='105' and CS_MAPGD<>'00'+LEFT(CS_MADP,4)";
                            cls.UpdateDataText(sqlcd);
                        }
                        //MessageBox.Show("Insert OK : " + giatri[1]);

                        #endregion
                    }
                    cls.DongKetNoi();

                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Chưa xuất file TEXT : " + exitsFile, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

    private void DeleteData(String FileName, String FieldNgay,String NgayBc)
        {
            try
            {
                ClsServer cls = new ClsServer();
                cls.ClsConnect();
                string sql = "delete from " + FileName + " where " + FieldNgay + " = " + "'" + NgayBc +"'";
                //MessageBox.Show(sql);
                cls.UpdateDataText(sql);
                MessageBox.Show("delete ok");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

        private void btnHskh_Click(object sender, RoutedEventArgs e)
        {
            /*
            cls.LoadDataProc("usp_UpdateHskh");
             */
            ClsServer cls = new ClsServer();
            cls.ClsConnect();
            int thamso = 1;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@Ngay";
            if (dtpNgay.SelectedDate != null) giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
            dt = cls.LoadLdbf("usp_UpdateHskh", bien, giatri, thamso);
            MessageBox.Show("Update HSKH OK", "Mess");
            cls.DongKetNoi();
        }

        private void btnInsertOne_Click(object sender, RoutedEventArgs e)
        {
            string ngay = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
            string[] arrStr = cboFile.SelectedValue.ToString().Trim().Split('|');
            //string expFile = txtPath.Text.Trim() + bll.XoaHetKyTu(arrStr[0], "_") +dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".txt";
            try
            {
                cnn.ClsConnect();
                Insert_Text(arrStr[0].Trim(), arrStr[1].Trim(), ngay);
                if (arrStr[0].ToString().Trim() == "HSKH")
                {
                    string sqlkh = "with lst1 as ( select KH_MAKH, COUNT(KH_MAKH)  DEM from HSKH group by KH_MAKH having COUNT(KH_MAKH) > 1) "
                                    + " delete a from HSKH a,lst1 b where a.KH_MAKH = b.KH_MAKH and LEFT(a.KH_MADP, 4)<> RIGHT(a.KH_MAPGD, 4)";
                    cnn.UpdateDataText(sqlkh);
                }
                MessageBox.Show("Insert file : " + arrStr[0].Trim()+" OK ","Mess",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.DongKetNoi();

        }

        private void btnExporttOne_Click(object sender, RoutedEventArgs e)
        {
            LoadSingle();
            if (dt.Rows.Count > 0)
            {
                string[] arrStr = cboFile.SelectedValue.ToString().Trim().Split('|');
                string expFile = txtPath.Text.Trim() + bll.XoaHetKyTu(arrStr[0], "_") +dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".txt";
                if (File.Exists(expFile))
                {
                    string mess = "Đã có file : " + expFile + "OverWrite ? ";
                    if (MessageBox.Show(mess, "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) ==
                        MessageBoxResult.No)
                    {
                       // MessageBox.Show("You has Select No");
                    }
                    else
                    {
                        WriteText(expFile);
                        MessageBox.Show("Export OK" + expFile, "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }
                else
                {
                    WriteText(expFile);
                    MessageBox.Show("Export OK" + expFile, "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu ngày " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"));
            }

        }

        private void Ration5_Checked(object sender, RoutedEventArgs e)
        {
            btnReadFile.IsEnabled = true;
            btnInsertSql.IsEnabled = true;
            btnHskh.IsEnabled = true;
        }

        private void btnKhtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClsServer cls = new ClsServer();
                cls.ClsConnect();
                var dtchku = cls.LoadDataText("select * from LUU_KHTN where NAM='"+ dtpNgay.SelectedDate.Value.AddDays(1).ToString("yyyy") + "'");
                if (dtchku.Rows.Count == 0)
                {
                    var dtku = "insert into LUU_KHTN select "+ dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + " NGAY, KU_MAPGD, KU_MATO, left(KU_MADP,6)MAXA,KU_CHTRINH,KU_SOKU,KU_DNOTHAN,KU_DNOQHAN,KU_DNOKHOANH"
                                + ",KU_NGAYDHAN_2,KU_NGAYDHAN_3,0 DN01,0 TN01,0 DN02,0 TN02,0 DN03,0 TN03,0 DN04,0 TN04,0 DN05,0 TN05,0 DN06"
                                + ",0 TN06,0 DN07,0 TN07,0 DN08,0 TN08,0 DN09,0 TN09,0 DN10,0 TN10,0 DN11,0 TN11,0 DN12,0 TN12,"+ dtpNgay.SelectedDate.Value.AddDays(1).ToString("yyyy") + " from HSKU where KU_NGAYBC = '" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and KU_TTMONVAY<> 'CLOSE'";
                    cls.UpdateDataText(dtku);
                    MessageBox.Show("Tạo số liệu OK !", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                string insku = "insert into LUU_KHTN select '" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' NGAY, KU_MAPGD, KU_MATO," 
                             + " left(KU_MADP,6)MAXA,KU_CHTRINH,KU_SOKU,KU_DNOTHAN,KU_DNOQHAN,KU_DNOKHOANH"
                             + ",KU_NGAYVAY,KU_NGAYDHAN_1,KU_NGAYDHAN_2,KU_NGAYDHAN_3,0 DN01,0 TN01,0 DN02,0 TN02,0 DN03,0 TN03,0 DN04,0 TN04,0 DN05"
                             + ",0 TN05,0 DN06,0 TN06,0 DN07,0 TN07,0 DN08,0 TN08,0 DN09,0 TN09,0 DN10,0 TN10,0 DN11,0 TN11,0 DN12,0 TN12"
                             + ",'" + dtpNgay.SelectedDate.Value.ToString("yyyy") + "' NAM, KU_TTMONVAY from HSCV_DAILY a "
                             + " where a.KU_NGAYBC = '" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "'"
                             + " and a.KU_SOKU not in (select KU_SOKU from LUU_KHTN where NAM = '" + dtpNgay.SelectedDate.Value.ToString("yyyy") + "' and KU_SOKU = a.KU_SOKU)";
                cls.UpdateDataText(insku);
                string updn = "update a set a.KU_TTMONVAY=b.KU_TTMONVAY,a.DN" + dtpNgay.SelectedDate.Value.ToString("MM") + "=b.ST from LUU_KHTN a,(select KU_SOKU,KU_DNOTHAN+KU_DNOQHAN+KU_DNOKHOANH ST,KU_TTMONVAY from HSCV_DAILY where KU_NGAYBC='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "') b where a.KU_SOKU=b.KU_SOKU and a.NAM='" + dtpNgay.SelectedDate.Value.ToString("yyyy") + "' and a.KU_DNOTHAN+a.KU_DNOQHAN+a.KU_DNOKHOANH<>b.ST";
                cls.UpdateDataText(updn);

                var dtdata = cls.LoadDataText("select * from BT_PSINH where NGAYGD='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and MNV in ('12','13')");
                if (dtdata.Rows.Count > 0)
                {
                    var dtcheck = cls.LoadDataText("select * from U_KHTN where NGAY='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "'");
                    if (dtcheck.Rows.Count > 0)
                        MessageBox.Show("Đã lưu lịch sử thu nợ vào LUU_KHTN ngày " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") + " rồi !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                    {
                        string tn = "update a set a.TN" + dtpNgay.SelectedDate.Value.ToString("MM") + "=a.TN" + dtpNgay.SelectedDate.Value.ToString("MM") + "+b.ST from LUU_KHTN a,(select NGAYGD,SOKU,SUM(SOTIEN) ST from BT_PSINH where NGAYGD='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and MNV in ('12','13') group by NGAYGD,SOKU) b where a.KU_SOKU=b.SOKU and a.NAM='" + dtpNgay.SelectedDate.Value.ToString("yyyy") + "'";
                        cls.UpdateDataText(tn);
                        string instr = "insert into U_KHTN(NGAY) values ('" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "')";
                        cls.UpdateDataText(instr);
                        MessageBox.Show("Cập nhật lịch sử thu nợ thành công ngày " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else MessageBox.Show("Không thu nợ món vay nào !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnULdbf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblMess.Content = "Đang xử lý ....";
                ClsServer cls = new ClsServer();
                cls.ClsConnect();
                string upd = "update ldbf set CS_M_GUITK=0,CS_Q_GUITK=0,CS_A_GUITK=0,CS_M_RUTTK=0,CS_Q_RUTTK=0,CS_A_RUTTK=0,CS_SODU_TK=0 where STT>1 and NGAYBC='"+dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy")+"'";
                cls.UpdateDataText(upd);
                MessageBox.Show("Update OK","Mess",MessageBoxButton.OK,MessageBoxImage.Information);
                lblMess.Content = "";
                cls.DongKetNoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }


        private void BtnManual_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {

                cls.ClsConnect();
                //string sql = "select * from " + arrStr[0];
                string sql = "";
                string[] arrStr = cboFile.SelectedValue.ToString().Trim().Split('|');
                string expFile = txtPath.Text.Trim() + bll.XoaHetKyTu(arrStr[0], "_") + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".txt";
                String fileName = arrStr[0].Trim();
                switch (fileName)
                {
                    case "CASA_DCVON":
                        sql = "select  BR_CD,AC_NO,IBAN_AC,AC_BR_CD,to_char(OPEN_DT,'YYYY-MM-DD') OPEN_DT,to_char(CLS_DT,'YYYY-MM-DD') CLS_DT,NO_HLDRS,STAFF_FLG"
                                + ",LEG_ST,PRD_CD,CCY_CD,DLQ_ST,AC_CAT,PAM_CD,COST_CTR,BUS_SEG,CUST_ST"
                                + ",DOM_CD,LOC_CD,FIN_ST,FIN_SUB_ST,CUR_BAL,LCY_CUR_BAL,OP_BAL,LCY_OP_BAL"
                                + ",OFF_TURN,LCY_OFF_TURN,EAR_AMT,REC_ST,TOT_LINE_AMT,AAFA_FLG,UFD_FLG"
                                + ",MD_BAL,MD_FLG,AS_BAL,AS_FLG,UNAUTH_CR_AMT,UNAUTH_DR_AMT,SHORT_CD"
                                + ",MICR_NO,UNCOL_BAL,GROUP_NO,LAST_CB_NO,LAST_CRD_NO,DR_ADV_FLG,CR_ADV_FLG"
                                + ",HOLD_MAIL,DLQ_PRD_STR,to_char(DT_LST_DR,'YYYY-MM-DD') DT_LST_DR,to_char(DT_LST_CR,'YYYY-MM-DD') DT_LST_CR,INACT_ST"
                                + ",to_char(LST_APP_DT,'YYYY-MM-DD') LST_APP_DT,NBCP_ELG_FLG"
                                + ",OP_GROUP_NO,to_char(EOD_DATE,'YYYY-MM-DD') EOD_DATE,to_char(CA_LST_DLQ_DT,'YYYY-MM-DD') CA_LST_DLQ_DT"
                                + ",to_char(LST_DLQ_DT,'YYYY-MM-DD') LST_DLQ_DT,CHECKSUM,LST_APP_AMT"
                                + ",to_char(LST_ACC_DT,'YYYY-MM-DD') LST_ACC_DT,SIGN_REQ,SEC_REF_NO,SCHEME_CD,to_char(SCHEME_CHANGE_DT,'YYYY-MM-DD') SCHEME_CHANGE_DT,NET_ID"
                                + ",APPLN_AC_NO,to_char(RATE_RESET_DATE,'YYYY-MM-DD') RATE_RESET_DATE,ONLINE_APP_FLG,MKR_ID,to_char(MKR_DT,'YYYY-MM-DD') MKR_DT,AUTH_ID"
                                + ",to_char(AUTH_DT,'YYYY-MM-DD') AUTH_DT,AC_NAME,POS_CD,GUAR_FLG,COVER_INSTR,CR_APPLN_AC_NO,DR_APPLN_AC_NO"
                                + ",TXN_BAL,LCY_TXN_BAL,LAST_TXN_NO,LL_NAME,NO_NOTICE_DAYS,AC_PROD_FEATURES"
                                + ",STMT_CYCLE,STMT_FREQ,STMT_DLRY_MD,PAM_CD_2,ACC_NAME_TC3,to_char(LST_BAL_CHG_DT,'YYYY-MM-DD') LST_BAL_CHG_DT" +
                                ",to_char(NGAY_BC,'YYYY-MM-DD') NGAY_BC from "
                                + fileName ;
                        break;
                    case "CASA":
                        sql = " select  CS_MAPGD ,CS_MAKH ,CS_MATO ,CS_SO_TK ,CS_SO_TK2 ,CS_TENTK ,CS_SODU_TK ,CS_SP_TK ,CS_M_GUITK ,CS_M_RUTTK ,"
                              + "CS_Q_GUITK ,CS_Q_RUTTK ,CS_A_GUITK ,CS_A_RUTTK ,CS_TTSO_TK , to_char(CS_NGAYBC,'YYYY-MM-DD') CS_NGAYBC ,CS_MACN ,"
                              + " to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT ,CS_MADP ,to_char(CS_NGAYDKY,'YYYY-MM-DD') CS_NGAYDKY ,"
                              + "to_char(CS_NGAYTT,'YYYY-MM-DD') CS_NGAYTT ,to_char(CS_NGAYROITO,'YYYY-MM-DD') CS_NGAYROITO from " +fileName;
                        break;
                    case "CASA_DAILY":
                        sql = "select CS_MACN , CS_MAPGD , CS_MAKH , CS_MATO , CS_SO_TK , CS_SO_TK2 , CS_TENTK , CS_SP_TK , CS_SODU_TK , CS_M_GUITK "
                               + ", CS_M_RUTTK , CS_Q_GUITK , CS_Q_RUTTK , CS_A_GUITK , CS_A_RUTTK , CS_TTSO_TK , CS_MADP ,to_char(CS_NGAYBC,'YYYY-MM-DD') CS_NGAYBC "
                                + ", to_char(CS_NGAYDKY,'YYYY-MM-DD') CS_NGAYDKY, to_char(CS_NGAYTT,'YYYY-MM-DD') CS_NGAYTT " +
                                ",to_char(CS_NGAYROITO,'YYYY-MM-DD') CS_NGAYROITO  from " + fileName ;
                        break;
                    case "HSCV_DAILY":
                        sql = "select TT , KU_MAKH ,KU_SOKU , KU_MATO , to_char(KU_NGAYVAY,'YYYY-MM-DD') KU_NGAYVAY, to_char(KU_NGAYDHAN_1,'YYYY-MM-DD') KU_NGAYDHAN_1, "
                                + "to_char(KU_NGAYDHAN_2,'YYYY-MM-DD') KU_NGAYDHAN_2,to_char(KU_NGAYDHAN_3,'YYYY-MM-DD') KU_NGAYDHAN_3,KU_HTHUCVAY , KU_SPRD_CD ,KU_CAPQLV ,KU_NGAYGDLD , "
                                + "KU_LSUAT ,KU_DTTH ,KU_MANDT ,to_char(KU_NGAY_TGOC,'YYYY-MM-DD') KU_NGAY_TGOC,to_char(KU_NGAY_TLAI,'YYYY-MM-DD') KU_NGAY_TLAI , "
                                + "to_char(KU_NGKTAHSV,'YYYY-MM-DD') KU_NGKTAHSV , KU_MAPNKT51 ,KU_MAPNKT52 ,KU_HQDT_CD ,	KU_HQDT_VAL1 , KU_HQDT_VAL2 , KU_MUCVAY ,	KU_GNGAN , "
                                + "to_char(KU_NGAYGNDT,'YYYY-MM-DD') KU_NGAYGNDT , to_char(KU_NGAYGNCC,'YYYY-MM-DD') KU_NGAYGNCC , KU_DNOTHAN ,KU_DNOQHAN , KU_DNOKHOANH , "
                                + "KU_TNOTHAN , KU_GOCDHAN ,	KU_GOCDTRA , KU_GOCXOA , KU_LAIXOA , KU_LAITHAN , KU_LAITONTHAN , KU_LAIQHAN , KU_LAITONQHAN , "
                                + "KU_LAI_DT , KU_M_LAI_DT , KU_LAI_TT , KU_M_LAI_TT , KU_M_LAI_PB , KU_Q_LAI_PB , KU_A_LAI_PB , KU_M_LAI_KH , KU_Q_LAI_KH , "
                                + "KU_A_LAI_KH , KU_LCDHAN_DT , KU_M_GNGAN , KU_GHANNO , KU_M_GHANNO , KU_CHUYENQH , to_char(KU_NGAYCNQH,'YYYY-MM-DD') KU_NGAYCNQH , "
                                + "KU_M_CHUYENQH , KU_M_DKCHUYENQH , KU_CHUYENKH , KU_M_CHUYENKH ,	KU_TON_RPA , to_char(KU_NGAYGDGN,'YYYY-MM-DD') KU_NGAYGDGN , "
                                + "KU_M_GOCXOA , to_char(KU_NGAY_DTHU,'YYYY-MM-DD') KU_NGAY_DTHU , KU_LAIHT_TONG ,	KU_LAIHT_CHT , KU_M_LUUVU , KU_M_DKGNGAN , KU_TTHAINO , KU_TTMONVAY , "
                                + "KU_TKTHAN , KU_TKQHAN , KU_TKKHOANH , KU_TKTHULAI , KU_M_TNTHAN , KU_M_TNQHAN , KU_M_TNKHOANH , KU_SCHEM_CD , KU_PROD_CD , KU_NGUONVON , KU_CHTRINH , KU_MAQD , "
                                + "KU_KYQUYFLG , KU_Q_GNGAN , KU_Q_LUUVU , KU_Q_DKGNGAN , KU_Q_GHANNO , KU_Q_CHUYENQH , KU_Q_CHUYENKH , KU_Q_TNTHAN , KU_Q_TNQHAN , KU_Q_TNKHOANH , KU_Q_GOCXOA , KU_Q_LAI_DT , "
                                + "KU_Q_LAI_TT , KU_A_GNGAN , KU_A_LUUVU , KU_A_DKGNGAN , KU_A_GHANNO , KU_A_CHUYENQH , KU_A_CHUYENKH , KU_A_TNTHAN , KU_A_TNQHAN , KU_A_TNKHOANH , KU_A_GOCXOA , KU_A_LAI_DT , "
                                + "KU_A_LAI_TT , KU_M_LAITHAN , KU_Q_LAITHAN , KU_A_LAITHAN , KU_M_LAIQHAN , KU_Q_LAIQHAN , KU_A_LAIQHAN , KU_TNTH , KU_TNQH , KU_TNKH , to_char(KU_LASTDUECRDT,'YYYY-MM-DD') KU_LASTDUECRDT , "
                                + " to_char(KU_NGAYHHKH,'YYYY-MM-DD') KU_NGAYHHKH , KU_GOCHHKH , to_char(KU_NGAYLUUVU,'YYYY-MM-DD') KU_NGAYLUUVU , "
                                + "KU_MAPGD , KU_MACN , to_char(KU_NGAYBC,'YYYY-MM-DD') KU_NGAYBC , KU_MADP , KU_CHUANNDP , KU_M_KHOANHCQHAN , KU_M_KHOANHCTHAN , "
                                + "KU_M_THOAILAI , CS_SO_TK , CS_SO_TK2 , CS_TENTK , CS_SODU_TK , CS_M_GUITK , CS_M_RUTTK , CS_Q_GUITK , CS_Q_RUTTK , CS_A_GUITK , "
                                + "CS_A_RUTTK , CS_TTSO_TK , to_char(CS_NGAYDKY,'YYYY-MM-DD') CS_NGAYDKY , to_char(CS_NGAYTT,'YYYY-MM-DD') CS_NGAYTT , "
                                + "to_char(CS_NGAYROITO,'YYYY-MM-DD') CS_NGAYROITO , KU_M_DAOKHOANTNO,PL_NGUONVON_BS  from " + fileName ;
                        break;
                    case "HSKU":
                        sql = "	select KU_MAKH ,KU_SOKU , KU_MATO , "
                               + "to_char(KU_NGAYVAY,'YYYY-MM-DD') KU_NGAYVAY, to_char(KU_NGAYDHAN_1,'YYYY-MM-DD') KU_NGAYDHAN_1, "
                               + "to_char(KU_NGAYDHAN_2,'YYYY-MM-DD') KU_NGAYDHAN_2, to_char(KU_NGAYDHAN_3,'YYYY-MM-DD') KU_NGAYDHAN_3, "
                               + "KU_HTHUCVAY , KU_SPRD_CD ,KU_CAPQLV ,KU_NGAYGDLD ,KU_LSUAT ,KU_DTTH ,KU_MANDT ,  to_char(KU_NGAY_TGOC,'YYYY-MM-DD') KU_NGAY_TGOC, "
                               + "to_char(KU_NGAY_TLAI,'YYYY-MM-DD') KU_NGAY_TLAI ,  to_char(KU_NGKTAHSV,'YYYY-MM-DD') KU_NGKTAHSV ,  KU_MAPNKT51 ,"
                               + "KU_MAPNKT52 ,KU_HQDT_CD ,	KU_HQDT_VAL1 ,KU_HQDT_VAL2 , KU_MUCVAY ,	KU_GNGAN ,  to_char(KU_NGAYGNDT,'YYYY-MM-DD') KU_NGAYGNDT , "
                               + "to_char(KU_NGAYGNCC,'YYYY-MM-DD') KU_NGAYGNCC ,  KU_DNOTHAN ,KU_DNOQHAN , KU_DNOKHOANH ,  KU_TNOTHAN , KU_GOCDHAN ,	KU_GOCDTRA , "
                               + "KU_GOCXOA , KU_LAIXOA , KU_LAITHAN ,  KU_LAITONTHAN , KU_LAIQHAN , KU_LAITONQHAN ,  KU_LAI_DT , KU_LAI_TT ,KU_LCDHAN_DT , KU_M_GNGAN ,  "
                               + "KU_GHANNO , KU_M_GHANNO , KU_CHUYENQH ,  to_char(KU_NGAYCNQH,'YYYY-MM-DD') KU_NGAYCNQH ,  KU_M_CHUYENQH , KU_CHUYENKH ,  KU_M_CHUYENKH ,"
                               + "KU_TON_RPA , to_char(KU_NGAYGDGN,'YYYY-MM-DD') KU_NGAYGDGN ,  KU_M_GOCXOA , to_char(KU_NGAY_DTHU,'YYYY-MM-DD') KU_NGAY_DTHU ,  "
                               + "KU_LAIHT_TONG ,	KU_LAIHT_CHT , KU_M_LUUVU , KU_M_DKGNGAN , KU_TTHAINO , KU_TTMONVAY ,  KU_TKTHAN , KU_TKQHAN , KU_TKKHOANH , "
                               + "KU_TKTHULAI , KU_M_TNTHAN , KU_M_TNQHAN ,  KU_M_TNKHOANH , KU_SCHEM_CD , KU_PROD_CD , KU_NGUONVON , KU_CHTRINH , KU_MAQD ,  "
                               + "KU_KYQUYFLG , KU_Q_GNGAN , KU_Q_LUUVU , KU_Q_DKGNGAN , KU_Q_GHANNO , KU_Q_CHUYENQH ,  KU_Q_CHUYENKH , KU_Q_TNTHAN , KU_Q_TNQHAN , "
                               + "KU_Q_TNKHOANH , KU_Q_GOCXOA , KU_Q_LAI_DT ,  KU_Q_LAI_TT , KU_A_GNGAN , KU_A_LUUVU , KU_A_DKGNGAN , KU_A_GHANNO , KU_A_CHUYENQH ,  "
                               + "KU_A_CHUYENKH , KU_A_TNTHAN , KU_A_TNQHAN , KU_A_TNKHOANH , KU_A_GOCXOA , KU_A_LAI_DT ,  KU_A_LAI_TT , KU_M_LAITHAN , KU_Q_LAITHAN , "
                               + "KU_A_LAITHAN , KU_M_LAIQHAN , KU_Q_LAIQHAN ,  KU_A_LAIQHAN , KU_TNTH , KU_TNQH , KU_TNKH ,  KU_MAPGD , KU_MACN , "
                               + "to_char(KU_NGAYBC,'YYYY-MM-DD') KU_NGAYBC , KU_MADP , KU_CHUANNDP ,  to_char(KU_NGAYHHKH,'YYYY-MM-DD') KU_NGAYHHKH , KU_GOCHHKH , "
                               + "to_char(KU_NGAYLUUVU,'YYYY-MM-DD') KU_NGAYLUUVU  from " + fileName ;
                        break;
                    case "KHTN":
                        sql = "select  KH_SOKU , KH_LANTNO , to_char(KH_NGDHAN,'YYYY-MM-DD') KH_NGDHAN , KH_GOCDHAN , KH_LAIDHAN , KH_LAITONPB , KH_DUNO , KH_GOCDTRA , KH_LAIDTRA "
                               + ", KH_STHTRO , KH_MAPGD , KH_MACN ,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT   from " + fileName ;
                        break;
                    case "KHGN":
                        sql = "select KH_SOKU , KH_LANGNGAN ,to_char(KH_NGGNGAN,'YYYY-MM-DD') KH_NGGNGAN , KH_STGNGAN , KH_LSUAT , KH_MAHTLS "
                                + ",to_char(KH_NGAYBDHT,'YYYY-MM-DD') KH_NGAYBDHT ,to_char(KH_NGAYKTHT,'YYYY-MM-DD') KH_NGAYKTHT , KH_LSUATHT , KH_NGUONHT , KH_MAPGD , KH_MACN "
                                + ",to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT , KH_DGNGAN_FLG  from " + fileName ;
                        break;
                    case "QT_MS02TL":
                        sql = "select MACN,MAPGD,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,D2,D3,D4,D5,D6,D7,D8,D9,D10,D11,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT "
                                + " from " + fileName ;
                        break;
                    case "QT_MS04TL":
                        sql = "select MACN ,MAPGD,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,CHTRINH,D1,D2,D3,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT "
                                + " from " + fileName ;
                        break;
                    case "QT_MS14":
                        sql = "select MAPGD,NGAYBC,KEY,D1,D2,D3,D4,D5,D6,D7,D8,D9,D10,D11,D12,D13"
                               + ",D14,D15,D16,D17,D18,D19,D20,D21,D22,D23,D24,D25,D26"
                               + ",D27,D28,D29,D30,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT,MACN,D31,D32"
                                + " from " + fileName ;
                        break;
                    case "HSGH_HISTORY":
                        sql = "select MAPGD,SOKU,MAKH,MACN,CHEQ_HIST,GH_TSLAN,GH_LAN,to_char(GH_NGAY,'YYYY-MM-DD') GH_NGAY,GH_SOTIEN,GH_SOTHG,GH_TSOTHG "
                            + ",GH_TSOTIEN,GH_LOAINV,SPRD_CD,GH_MAQD,to_char(GH_NGAYQD,'YYYY-MM-DD') GH_NGAYQD,GH_NGNHAN,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT "
                            + " from " + fileName ;
                        break;
                    case "HSQH":
                        sql = "select QH_SOKU , to_char(QH_NGAYCQH,'YYYY-MM-DD') QH_NGAYCQH ,QH_GOCCQH,QH_LOAINN,QH_NGNHAN"
                             + ",QH_TRANGTHAI,QH_MAPGD,QH_MACN,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT from " +  fileName ;
                        break;
                    case "TXN_POINT_INFO_MB":
                        sql = "select TPI_ID,TPI_DATE,TPI_DESC,FILE_GEN_FLAG,TPI_POS,to_char(NG_CAPNHAT,'YYYY-MM-DD') NG_CAPNHAT"
                            + " from " + fileName;
                        break;
                    case "TXNPOINT_DETAIL":
                        sql = "select POS_CODE,POS_DESC,TXNPOINT_ID,TPI_DESC,MAKER_ID,MAKER_DT,CMUNE_VISIT_DATE,UPL_CHECK,CMUNE_VISIT_FLAG"
                                + ",CMUNE_FLAG_CHNG_BY,UPL_TIME,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC"
                            + " from " + fileName;
                        break;
                    case "DG_CASA105_DATA":
                        sql = "select MAPGD,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,MAKH,TK,TK1,SODUDAUKY,GUITK,CKTRANO_TONGSO,CKTRALAI,CKTRAGOC,CKTRAGOC_TUSDKYTRC,CKTRALAI_TUSDKYTRC "
                               + ",RUTTK,LAINHAPGOC,SODUCUOIKY,CHENHLECHSODU"
                            + " from " + fileName;
                        break;
                    case "QT_TSCC":
                        sql = "select MA_TS,TEN_TS,LOAI_TS,TEN_LOAI_TS,LOAI_TS_CHITIET,TEN_LOAI_TS_CHITIET,MA_NHANHIEU_TS,TEN_NHANHIEU_TS,NGUYEN_GIA"
                                + ",SO_LUONG,VON_TW,VON_DP,VON_KHAC,HAOMON_LK,POS_CD,MAIN_POS,NAMQT,to_char(NGAYTAO,'YYYY-MM-DD') NGAYTAO,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,to_char(NGAY_MUA,'YYYY-MM-DD') NGAY_MUA,MAPHONG,TENPHONG"
                            + " from " + fileName;
                        break;
                    case "QT_TSTL":
                        sql = "select MA_TS,TEN_TS,LOAI_TS,TEN_LOAI_TS,LOAI_TS_CHITIET,TEN_LOAI_TS_CHITIET,MA_NHANHIEU_TS,to_char(NGAY_SDUNG,'YYYY-MM-DD') NGAY_SDUNG,THOIGIAN_SD,NGUYEN_GIA,HAOMON_LK "
                                + ",to_char(NGAY_TLY,'YYYY-MM-DD') NGAY_TLY,CHIPHI_TLY,THUTU_TLY,POS_CD,MAIN_POS,NAMQT,to_char(NGAYTAO,'YYYY-MM-DD') NGAYTAO,to_char(NGAYBC,'YYYY-MM-DD') NGAYBC,to_char(NGAY_MUA,'YYYY-MM-DD') NGAY_MUA"
                            + " from " + fileName;
                        break;
                    default:
                        sql = "select * from " + fileName ;
                        break;

                }

                dt = cls.LoadDataText(sql);
                WriteText(expFile);
                MessageBox.Show("Export OK" + expFile, "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error"+ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            cls.DongKetNoi();
        }

        private void Ration7_OnChecked(object sender, RoutedEventArgs e)
        {
            btnReadFile.IsEnabled = true;
            btnInsertSql.IsEnabled = true;
        }

        private void BtnHstd_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                    ClsServer cnn = new ClsServer();
                    cnn.ClsConnect();
                    int thamso = 1;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@Ngay";
                    if (dtpNgayKu.SelectedDate != null) giatri[0] = dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy");
                    string sql = " select top 1 * from HSTD where ngay='" +
                                 dtpNgayKu.SelectedDate.Value.ToString("yyyy-MM-dd") + "'";
                    dt = cnn.LoadDataText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show(
                            "Đã tồn tại dữ liệu ngày " + dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy") +
                            " trong HSTD",
                            "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        DataTable dtchk = new DataTable();
                        string strchk = "select * from U_HSTD where NGAYKU='" +
                                        dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy") + "'";
                        dtchk = cnn.LoadDataText(strchk);
                        if (dtchk.Rows.Count > 0)
                        {
                            cnn.LoadLdbf("usp_PhanTich", bien, giatri, thamso);
                            MessageBox.Show("Update HSTD OK", "Mess");
                        }
                        else
                        {
                            MessageBox.Show(
                                "Không có dữ liệu chi tiết ngày " + dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy"),
                                "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                    }
                
                cnn.DongKetNoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void BtnHsbt_OnClick(object sender, RoutedEventArgs e)
        {
            cnn.ClsConnect();
            try
            {
                var dtchkcd=cnn.LoadDataText("select * from U_CANDOI where NGAY= '" + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") + "'");
                if (dtchkcd.Rows.Count != 0)
                {
                    #region

                    if (DateTime.Parse(dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy")) <=
                        DateTime.Parse(dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy")))
                    {
                        MessageBox.Show("Ngày GD không được nhỏ hơn ngày KU", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }
                    else
                    {
                        DataTable dt_chk = new DataTable();
                        //dt_chk =
                        //    cnn.LoadDataText("select * from U_HSTD where NGAYBT= '" +
                        //                     dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") + "' and NGAYKU='" +
                        //                     dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy") + "'");
                        dt_chk =
                            cnn.LoadDataText("select * from U_HSTD where NGAYBT= '" +dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") + "'");

                        #region

                        if (dt_chk.Rows.Count > 0)
                        {
                            MessageBox.Show("Kiểm tra lại số liệu, đã thực hiện trước đó ngày : " +
                                            dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"));
                        }
                        else
                        {
                            int thamso = 2;
                            string[] bien = new string[thamso];
                            object[] giatri = new object[thamso];
                            bien[0] = "@Ngaygd";
                            giatri[0] = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
                            bien[1] = "@Ngayku";
                            giatri[1] = dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy");
                            //cnn.UpdateLdbf("usp_UpdateData", bien, giatri, thamso);
                            cnn.LoadDataProcPara("usp_UpdateData", bien, giatri, thamso);
                            string sqlcd = "insert into U_HSTD (NGAYKU,NGAYBT)  values ('" +
                                           dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy") + "','" +
                                           dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") + "')";
                            //MessageBox.Show(sqlcd);
                            cnn.UpdateDataText(sqlcd);
                            string sqlck = "select * from PSHSBT where NGAY='" +
                                           dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and MAXA is null and MA='KU'";
                            var dtck = cnn.LoadDataText(sqlck);
                            if (dtck.Rows.Count > 0)
                            {
                                MessageBox.Show("Có KU giải ngân mới", "Mess", MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                                WpfPdf f = new WpfPdf();
                                f.ShowDialog();
                            }
                            else MessageBox.Show("OK", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        #endregion
                    }

                    #endregion
                }
                else
                {
                    MessageBox.Show("Chưa có số liệu bút toán ngày : " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"), "Thông báo", MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.DongKetNoi();
        }

        private void btnGNGAN_Click(object sender, RoutedEventArgs e)
        {
            cnn.ClsConnect();
            try
            {
                 #region

                var dt_chk = cnn.LoadDataText("select * from U_HSTD where NGAYGN= '" +dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") + "'");

                 #region

                        if (dt_chk.Rows.Count > 0)
                        {
                            MessageBox.Show("Kiểm tra lại số liệu, đã thực hiện trước đó ngày : " +
                                            dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"));
                        }
                        else
                        {
                            int thamso = 2;
                            string[] bien = new string[thamso];
                            object[] giatri = new object[thamso];
                            bien[0] = "@Ngaygd";
                            giatri[0] = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
                            bien[1] = "@Ngayku";
                            giatri[1] = dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy");
                            cnn.LoadDataProcPara("usp_UpPSTinDung", bien, giatri, thamso);
                            string sqlcd = "insert into U_HSTD (NGAYGN)  values ('" + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") + "')";
                            cnn.UpdateDataText(sqlcd);
                            MessageBox.Show("Insert PSHSKU,PSCASA OK", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);

                        }

                        #endregion

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.DongKetNoi();

        }

        private void btnCheckSl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                if (dtpNgay.SelectedDate != null)
                {
                    var anyDt =dtpNgay.SelectedDate.Value;//DateTime.Now;
                    var lastDayOfMonth = anyDt.AddDays(1 - anyDt.Day).AddMonths(1).AddDays(-1).Date;
                    //MessageBox.Show(lastDayOfMonth.ToString());
                    //if (dtpNgay.SelectedDate.Value==lastDayOfMonth)
                    //    MessageBox.Show("CT YES");
                    //else MessageBox.Show("CT NO");
                    string str = "";
                    var ngay = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
                    if (dtpNgay.SelectedDate.Value == lastDayOfMonth)
                    {
                        str = " select a.CD,b.DN,a.CD - b.DN CL from "
                                     +
                                     "( select round(sum(nvl(CT_GIATRI, 0)), 0) CD from CT_VBSP where CT_NGAYBC = to_date('" +
                                     ngay +
                                     "', 'DD/MM/YYYY') and substr(CT_MACT, 1, 3) = '1B1' and CT_CAPTH = 'S' ) a, "
                                     +
                                     " ( select round(sum(KU_DNOTHAN + KU_DNOQHAN + KU_DNOKHOANH) / 1000000, 0) DN from HSKU where KU_NGAYBC = to_date('" +
                                     ngay + "', 'DD/MM/YYYY') and KU_TTMONVAY<> 'CLOSE') b";
                    }
                    else
                    {
                        str = " select a.CD,b.DN,a.CD - b.DN CL from "
                                     +
                                     "( select round(sum(nvl(CT_GIATRI, 0)), 0) CD from CT_VBSP where CT_NGAYBC = to_date('" +
                                     ngay +
                                     "', 'DD/MM/YYYY') and substr(CT_MACT, 1, 3) = '1B1' and CT_CAPTH = 'S' ) a, "
                                     +
                                     " ( select round(sum(KU_DNOTHAN + KU_DNOQHAN + KU_DNOKHOANH) / 1000000, 0) DN from HSCV_DAILY where KU_NGAYBC = to_date('" +
                                     ngay + "', 'DD/MM/YYYY') and KU_TTMONVAY<> 'CLOSE') b";
                    }
                    //MessageBox.Show(str);
                    var dtchk = cls.LoadDataText(str);
                    //MessageBox.Show(dtchk.Rows.Count > 0 ? dtchk.Rows[0]["CL"].ToString() : "Chua co so lieu");
                    if ((decimal)dtchk.Rows[0]["CD"] == 0 || (decimal)dtchk.Rows[0]["DN"] == 0)
                        MessageBox.Show("Chưa có số liệu ngày " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else if ((decimal)dtchk.Rows[0]["CL"] != 0)
                        MessageBox.Show("Có chênh lệch CD và HSTD " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") + "   " + dtchk.Rows[0]["CL"], "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else MessageBox.Show("OK, SL Đúng", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                    /*
                    string str = "select isnull(a.CD,0) CD,isnull(b.DUNO,0) DN,isnull(a.CD,0)-isnull(b.DUNO,0) CL from "
                                 +" (select round(sum(CONVERT(numeric(18, 2), CT_GIATRI)), 0) CD from CT_VBSP where CT_NGAYBC = '"
                                 + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and left(CT_MACT, 3) = '1B1' and CT_CAPTH = 'S') a,"
                                 + "(select round(sum(KU_DNOTHAN + KU_DNOQHAN + KU_DNOKHOANH) / 1000000, 0) DUNO from HSCV_DAILY where KU_NGAYBC = '"
                                 + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and KU_TTMONVAY<> 'CLOSE') b";
                    // MessageBox.Show(str);
                    var dtchk = cnn.LoadDataText(str);
                    //MessageBox.Show(dtchk.Rows.Count > 0 ? dtchk.Rows[0]["CL"].ToString() : "Chua co so lieu");
                    if ((decimal)dtchk.Rows[0]["CD"] == 0 || (decimal)dtchk.Rows[0]["DN"] == 0)
                         MessageBox.Show("Chưa có số liệu ngày "+ dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"),"Thông báo",MessageBoxButton.OK,MessageBoxImage.Warning);
                    else if ((decimal)dtchk.Rows[0]["CL"] != 0)
                        MessageBox.Show("Có chênh lệch CD và HSTD " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy")+"   "+ dtchk.Rows[0]["CL"], "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else MessageBox.Show("OK, SL Đúng", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    */
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }
        private void btnCheckSlSQL_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime lastMonth = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month, DateTime.DaysInMonth(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month));
                string str = "";
                cnn.ClsConnect();
                if (dtpNgay.SelectedDate != null)
                {
                    if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") == lastMonth.ToString("yyyy-MM-dd"))
                    {
                        str = "select isnull(a.CD,0) CD,isnull(b.DUNO,0) DN,isnull(a.CD,0)-isnull(b.DUNO,0) CL from "
                                     +
                                     "(select round(sum(CONVERT(numeric(18, 2), CT_GIATRI)), 0) CD from CT_VBSP where CT_NGAYBC ='"
                                     + dtpNgay.SelectedDate.Value.ToString("yyyy - MM - dd") +
                                     "' and left(CT_MACT, 3) = '1B1' and CT_CAPTH = 'S') a, "
                                     +
                                     " (select round(sum(a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH) / 1000000, 0) DUNO from "
                                     + " (select * from HSKU where KU_NGAYBC = '" +
                                     dtpNgay.SelectedDate.Value.ToString("yyyy - MM - dd") +
                                     "' and KU_TTMONVAY <> 'CLOSE') a "
                                     + ",(select * from HSKH) b where a.KU_MAKH = b.KH_MAKH) b";
                    }
                    else
                    {
                        str = "select isnull(a.CD,0) CD,isnull(b.DUNO,0) DN,isnull(a.CD,0)-isnull(b.DUNO,0) CL from "
                                     +
                                     "(select round(sum(CONVERT(numeric(18, 2), CT_GIATRI)), 0) CD from CT_VBSP where CT_NGAYBC ='"
                                     + dtpNgay.SelectedDate.Value.ToString("yyyy - MM - dd") +
                                     "' and left(CT_MACT, 3) = '1B1' and CT_CAPTH = 'S') a, "
                                     +
                                     " (select round(sum(a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH) / 1000000, 0) DUNO from "
                                     + " (select * from HSCV_DAILY where KU_NGAYBC = '" +
                                     dtpNgay.SelectedDate.Value.ToString("yyyy - MM - dd") +
                                     "' and KU_TTMONVAY <> 'CLOSE') a "
                                     + ",(select * from HSKH) b where a.KU_MAKH = b.KH_MAKH) b";
                    }
                    // MessageBox.Show(str);
                    var dtchk = cnn.LoadDataText(str);
                    if ((decimal)dtchk.Rows[0]["CD"] == 0 || (decimal)dtchk.Rows[0]["DN"] == 0)
                        MessageBox.Show("Chưa có số liệu ngày " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else if ((decimal)dtchk.Rows[0]["CL"] != 0)
                        MessageBox.Show("Có chênh lệch CD và HSTD " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") + "   " + dtchk.Rows[0]["CL"], "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else MessageBox.Show("OK, SL Đúng", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cnn.DongKetNoi();
        }
        private void btnCheckTable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                string str = "select  MODULE, TEN_BANG MABANG, MTS_TABLE_DESC TENBANG,TO_CHAR(NGAYBC, 'DD/MM/YYYY')   NGAYBC,"
                            + " (select count(*) from dumm_log_cn a where a.TEN_BANG = b.TEN_BANG and a.NGAY_BC = b.NGAYBC) SO_PGD "
                            +" from(select MTS_TIMES_SYN,case    when MTS_TIMES_SYN like '%C%' then 'Báo cáo quyết toán' "
                                    +" when MTS_TIMES_SYN like '%D%' then 'Số liệu kỳ ngày' "
                                    +" when MTS_TIMES_SYN like '%W%' then 'Số liệu kỳ tuần' "
                                    +" when MTS_TIMES_SYN like '%T%' then 'Báo cáo tín dụng' "
                                    +" when MTS_TIMES_SYN like '%M%' then 'Báo cáo tháng' "
                                    +" when MTS_TIMES_SYN like '%O%' then 'Số liệu cấm điểm tổ' "        
                            +" else 'Khác' end Module, "
                            +" case    when MTS_TIMES_SYN like '%C%' then '5' "
                                    +" when MTS_TIMES_SYN like '%D%' then '1' "
                                    +" when MTS_TIMES_SYN like '%W%' then '2' "
                                    +" when MTS_TIMES_SYN like '%T%' then '4' "
                                    +" when MTS_TIMES_SYN like '%M%' then '3' "
                                    +" when MTS_TIMES_SYN like '%O%' then '6' "
                            +" else 'Khác' end sapxep, "
                            +" MTS_TABLE_DESC, TEN_BANG, max(NGAY_BC) ngaybc from dumm_log_cn, "
                            +" (select * from master_table_syn where MTS_TIMES_SYN not in ('/SYS/', '/HIST/') "
                            +" and MTS_FLAG_SYN = 'Y') where TEN_BANG = MTS_TABLE_NAME "
                            +" group by TEN_BANG, MTS_TIMES_SYN,MTS_TABLE_NAME,MTS_TABLE_DESC) b "
                            +" order by sapxep, MTS_TIMES_SYN, TEN_BANG";
                var dtcheck=cls.LoadDataText(str);
                FileName = Thumuc + "\\" +"Table_"+ dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                bll.WriteDataTableToExcel(dtcheck, "Details", FileName, "tutm : 0985165777");
                bll.OpenExcel(FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }

        private void btnBTPS_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cnn.ClsConnect();
                if (dtpNgay.SelectedDate != null)
                {
                    // var str = "select top 1 * from hsbt where ngaygd='"+dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd")+"'";
                    // MessageBox.Show(str);
                    dtchk = cnn.LoadDataText("select top 1 * from hsbt where ngaygd='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "'");
                    //MessageBox.Show(dtchk.Rows.Count > 0 ? dtchk.Rows[0]["CL"].ToString() : "Chua co so lieu");
                    if (dtchk.Rows.Count == 0)
                        MessageBox.Show("Chưa có số liệu HSBT ngày " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"),
                            "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else
                    {
                        //str = "select top 1 * from BT_PSINH where ngaygd='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "'";
                        //MessageBox.Show(str);
                        dtchk = cnn.LoadDataText("select top 1 * from BT_PSINH where ngaygd='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "'");
                        //MessageBox.Show(dtchk.Rows.Count > 0 ? dtchk.Rows[0]["CL"].ToString() : "Chua co so lieu");
                        if (dtchk.Rows.Count == 0)
                        {
                            const int thamso = 1;
                            string[] bien = new string[thamso];
                            object[] giatri = new object[thamso];
                            bien[0] = "@Ngay";
                            if (dtpNgay.SelectedDate != null)
                                giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                            dt = cnn.LoadLdbf("usp_PSSL", bien, giatri, thamso);
                            MessageBox.Show(
                                "Insert to BT_PSINH date " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") +
                                " OK", "Mess");
                        }
                        else
                        {
                            if (
                                MessageBox.Show(
                                    "Đã có số liệu BT_PSINH ngày " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") +
                                    "Insert Again?",
                                    "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                            {
                               // MessageBox.Show("Select No");
                            }
                            else
                            {
                                cnn.LoadDataText("delete from BT_PSINH where NGAYGD='" +
                                                 dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "'");
                                MessageBox.Show("Delete OK");
                                const int thamso = 1;
                                string[] bien = new string[thamso];
                                object[] giatri = new object[thamso];
                                bien[0] = "@Ngay";
                                if (dtpNgay.SelectedDate != null)
                                    giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                                dt = cnn.LoadLdbf("usp_PSSL", bien, giatri, thamso);
                                MessageBox.Show(
                                    "Insert to BT_PSINH date " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") +
                                    " OK", "Mess");
                                
                            }
                        }
                    }
                }
                cnn.DongKetNoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cnn.DongKetNoi();
        }



        private void btnMau06_Click(object sender, RoutedEventArgs e)
        {
            string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
            try
            {
                cnn.ClsConnect();
                if (dtpNgay.SelectedDate != null)
                {
                    //string str = "insert into MAU06 select a.* from ( "
                    //            + " select c.KH_MAPGD MAPOS, d.PO_TEN TENPOS, c.KH_MAKH MAKH, c.KH_TENKH TENKH, left(c.KH_MADP, 6) MAXA "
                    //            +" , (select TEN from DMXA where MA = left(c.KH_MADP, 6)) TENXA,a.SBT SOKU, b.KU_CHTRINH CHTRINH, b.KU_MATO MATO "
                    //            +" ,(select TO_TENTT from HSTO where TO_MATO = b.KU_MATO) TENTT,a.SOTIEN DUNO, a.SOTIEN DNOTHAN, b.KU_NGAYGNCC NGAY_VAY "
                    //            +" ,0 TRANGTHAI1,0 TRANGTHAI2,0 TRANGTHAI3,b.KU_MAPNKT51 PLMD ,(select GIATRI from DMKHAC where KHOA_1 = '25' and KHOA_2 = b.KU_MAPNKT51) TEN_PLMD,a.NGAYGD from "
                    //            +" (select * from HSBT where NGAYGD = '"+ng+"' and ghichu_1 = 'DISBNORMAL' and NOCO = 'D' and substring(tk_no, 1, 2) = '91' ) a "
                    //         +" ,(select * from HSCV_DAILY where KU_NGAYBC = '"+ng+"' and KU_DNOTHAN+KU_DNOQHAN + KU_DNOKHOANH > 0) b,HSKH c, DMPOS d "
                    //            +" where a.SBT = b.KU_SOKU and b.KU_MAKH = b.KU_MAKH and a.MAPGD = d.PO_MA and b.KU_MAPGD = d.PO_MA and c.KH_MAPGD = d.PO_MA "
                    //            +" and b.KU_MAKH = c.KH_MAKH) a where a.SOKU not in (select SOKU from MAU06 where SOKU = a.SOKU) order by a.NGAY_VAY";
                    string str = "insert into MAU06 select a.KU_MAPGD MAPOS,(select PO_TEN from DMPOS where PO_MA = a.KU_MAPGD) TENPOS "
                                 +
                                 " , b.KH_MAKH MAKH, b.KH_TENKH TENKH, c.MA MAXA, c.TEN TENXA, a.KU_SOKU SOKU, a.KU_CHTRINH CHTRINH "
                                 + " , a.KU_MATO MATO,(select TO_TENTT from HSTO where TO_MATO = a.KU_MATO) TENTT, "
                                 +
                                 " a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO,a.KU_DNOTHAN DNOTHAN, a.KU_NGAYVAY NGAY_VAY, "
                                 + " 0 TRANGTHAI1,0 TRANGTHAI2,0 TRANGTHAI3,a.KU_MAPNKT51 PLMD "
                                 +
                                 " ,(select GIATRI from DMKHAC where KHOA_1 = '25' and KHOA_2 = a.KU_MAPNKT51) TEN_PLMD "
                                 + " ,a.KU_NGAYGNDT NGAYGD from HSCV_DAILY a,HSKH b, DMXA c "
                                 + " where a.KU_NGAYBC = '" + ng + "' and a.KU_MAKH = b.KH_MAKH "
                                 + " and c.MA = left(b.KH_MADP, 6) and a.KU_TTMONVAY <> 'CLOSE' and a.KU_GNGAN > 0 "
                                 + " and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 "
                                 + " and a.KU_SOKU not in (select SOKU from MAU06 where SOKU = a.KU_SOKU) ";
                    cnn.LoadDataText(str);
                    MessageBox.Show("insert to MAU06 is OK !", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cnn.DongKetNoi();
        }

        private void DATA_UYTHAC()
        {
            string str = "";
            string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
            DateTime lastMonth = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month, DateTime.DaysInMonth(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month));
            try
            {
                cnn.ClsConnect();
                var dtchk = cnn.LoadDataText("select  top 1 * from data_uythac where ngay='" + ng + "'");
                if (dtchk.Rows.Count == 0)
                {
                    if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") == lastMonth.ToString("yyyy-MM-dd"))
                    {
                        str = " with lst1 as ( select '00' + left(a.KU_MADP, 4) POS,(select PO_TEN from DMPOS where PO_MA = '00' + left(a.KU_MADP, 4)) TENPOS,left(a.KU_MADP, 6) MAXA "
                              + " ,(select TEN from DMXA where MA = left(a.KU_MADP, 6)) TENXA "
                              +
                              ",a.KU_MADP,(select TEN from DMTHON where MA = a.KU_MADP) TENTHON,(select TO_DVUT from HSTO where TO_MATO = a.KU_MATO) DVUT,(select TENDV from DVUT where DVUT = (select TO_DVUT from HSTO where TO_MATO = a.KU_MATO)) TEN_DVUT "
                              + " ,a.KU_MATO,(select TO_TENTT from HSTO where TO_MATO = a.KU_MATO) TENTT "
                              +
                              " ,isnull(SUM(a.KU_DNOTHAN), 0) DNTH,isnull(SUM(a.KU_DNOQHAN), 0) DNQH,isnull(SUM(a.KU_DNOKHOANH), 0) DNKH "
                              +
                              " ,isnull(SUM(a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH), 0) DUNO,isnull(sum(a.KU_A_CHUYENQH), 0) A_CNQH,isnull(sum(a.KU_A_CHUYENKH), 0) A_CNKH "
                              +
                              " ,isnull(SUM(a.KU_A_GNGAN), 0) A_GNGAN,isnull(SUM(a.KU_A_GHANNO), 0) A_GHANNO,ISNULL(SUM(a.KU_A_TNTHAN), 0) A_TNTH,ISNULL(SUM(a.KU_A_TNQHAN), 0) A_TNQH "
                              +
                              " ,ISNULL(SUM(a.KU_A_LAITHAN), 0) A_LAITH,ISNULL(SUM(a.KU_A_LAIQHAN), 0) A_LAIQH,ISNULL(SUM(a.KU_LAITONQHAN + a.KU_LAITONQHAN), 0)  LAITON "
                              + " from HSKU a where a.KU_NGAYBC = '" + ng + "' and a.KU_MATO is not null "
                              + " group by left(a.KU_MADP, 4),left(a.KU_MADP, 6),a.KU_MADP,a.KU_MATO "
                              +
                              " ), lst2 as ( select a.CS_MATO,isnull(SUM(a.CS_SODU_TK), 0) DU from CASA a where a.CS_NGAYBC = '" +
                              ng + "' and a.CS_SP_TK = '105' group by a.CS_MATO "
                              +
                              " ) insert into DATA_UYTHAC select '" + ng +
                              "' NGAY,a.*,(select DU from lst2 where CS_MATO = a.KU_MATO) DU_105  from lst1 a order by a.KU_MATO";
                    }
                    else
                    {
                        str = " with lst1 as ( select '00' + left(a.KU_MADP, 4) POS,(select PO_TEN from DMPOS where PO_MA = '00' + left(a.KU_MADP, 4)) TENPOS,left(a.KU_MADP, 6) MAXA "
                              + " ,(select TEN from DMXA where MA = left(a.KU_MADP, 6)) TENXA "
                              +
                              ",a.KU_MADP,(select TEN from DMTHON where MA = a.KU_MADP) TENTHON,(select TO_DVUT from HSTO where TO_MATO = a.KU_MATO) DVUT,(select TENDV from DVUT where DVUT = (select TO_DVUT from HSTO where TO_MATO = a.KU_MATO)) TEN_DVUT "
                              + " ,a.KU_MATO,(select TO_TENTT from HSTO where TO_MATO = a.KU_MATO) TENTT "
                              +
                              " ,isnull(SUM(a.KU_DNOTHAN), 0) DNTH,isnull(SUM(a.KU_DNOQHAN), 0) DNQH,isnull(SUM(a.KU_DNOKHOANH), 0) DNKH "
                              +
                              " ,isnull(SUM(a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH), 0) DUNO,isnull(sum(a.KU_A_CHUYENQH), 0) A_CNQH,isnull(sum(a.KU_A_CHUYENKH), 0) A_CNKH "
                              +
                              " ,isnull(SUM(a.KU_A_GNGAN), 0) A_GNGAN,isnull(SUM(a.KU_A_GHANNO), 0) A_GHANNO,ISNULL(SUM(a.KU_A_TNTHAN), 0) A_TNTH,ISNULL(SUM(a.KU_A_TNQHAN), 0) A_TNQH "
                              +
                              " ,ISNULL(SUM(a.KU_A_LAITHAN), 0) A_LAITH,ISNULL(SUM(a.KU_A_LAIQHAN), 0) A_LAIQH,ISNULL(SUM(a.KU_LAITONQHAN + a.KU_LAITONQHAN), 0)  LAITON "
                              + " from HSCV_DAILY a where a.KU_NGAYBC = '" + ng + "' and a.KU_MATO is not null "
                              + " group by left(a.KU_MADP, 4),left(a.KU_MADP, 6),a.KU_MADP,a.KU_MATO "
                              +
                              " ), lst2 as ( select a.CS_MATO,isnull(SUM(a.CS_SODU_TK), 0) DU from CASA_DAILY a where a.CS_NGAYBC = '" +
                              ng + "' and a.CS_SP_TK = '105' group by a.CS_MATO "
                              +
                              " ) insert into DATA_UYTHAC select '" + ng +
                              "' NGAY,a.*,(select DU from lst2 where CS_MATO = a.KU_MATO) DU_105  from lst1 a order by a.KU_MATO";

                    }
                    cnn.LoadDataText(str);
                    MessageBox.Show("insert to DATA_UYTHAC is OK !", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                    //dgvDich.ItemsSource = ut.DefaultView;
                } else MessageBox.Show("Đã có số liệu ngày : "+ng, "Mess", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cnn.DongKetNoi();
        }

        private void DULIEU_TO()
        {
            try
            {
                int thamso = 1;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Ngay";
                giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                cnn.ClsConnect();
                cnn.UpdateDataProcPara("AA_TAO_DULIEUTO", bien, giatri, thamso);
                MessageBox.Show("Update DULIEU_TO Ok", "Mess");
                cls.DongKetNoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi DULIEU_TO"+ ex.Message,"Thông báo",MessageBoxButton.OK,MessageBoxImage.Error);
            }

        }
    }
}
