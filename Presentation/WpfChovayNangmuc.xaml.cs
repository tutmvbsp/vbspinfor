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
using System.IO;
using BLL;
using DAL;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfSkeTo.xaml
    /// </summary>
    public partial class WpfChovayNangmuc : Window
    {
        public WpfChovayNangmuc()
        {
            InitializeComponent();
        }
        //ClsConnectLocal cls = new ClsConnectLocal();
       // private FileStream _fw;
        ClsServer cls = new ClsServer();
        ToolBll str = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        string Thumuc = "C:\\Saoke";
        private string Mau = "";
        private string FileName = "";
        string strsql = "";
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            str.TaoThuMuc(Thumuc);
            try
            {

                cls.ClsConnect();
                int thamso = 6;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@MaPos";
                giatri[0] = str.Left(cboPos.SelectedValue.ToString().Trim(), 6);
                bien[1] = "@TuNgay";
                giatri[1] = dtpTuNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                bien[2] = "@DenNgay";
                giatri[2] = dtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                bien[3] = "@Chtr";
                giatri[3] = str.Left(CboChon.SelectedValue.ToString().Trim(), 2);
                bien[4] = "@Mau";
                if (OptSke.IsChecked == true) giatri[4] = "0";
                else if (OptChtr.IsChecked == true) giatri[4] = "1"; // chi tiet theo chuong trinh
                else if (OptM03.IsChecked==true) giatri[4] = "2";  //
                bien[5] = "@Ngay";
                giatri[5] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");

                /*
                else if (OptM01.IsChecked==true) giatri[4] = "A";
                else if (OptM02.IsChecked == true) giatri[4] = "B";//Tông hợp tất cả các chương trình theo xã
                */
                if (OptSke.IsChecked == true)
                {
                    if (str.Right(giatri[0].ToString(),2)=="00")

                        strsql = " with lst1 as ( "
                                   + "select  a.KU_MAPGD, left(a.KU_MADP, 6) MAXA, a.KU_MATO, a.KU_MAKH, a.KU_SOKU, a.KU_CHTRINH, a.KU_NGAYGNCC NG_VAY, a.KU_NGAYDHAN_1, a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO "
                                    + ", dbo.SoThang(a.KU_NGAYGNCC, a.KU_NGAYDHAN_1) SOTHANG, a.KU_MAPNKT51 from HSCV_DAILY a where  a.KU_NGAYBC = '" + giatri[5] + "' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 and a.KU_TTMONVAY <> 'CLOSE' and a.KU_CHTRINH IN('01', '09', '19', '21') "
                                    + " ), lst1a as  ( select a.* from lst1 a where a.NG_VAY between '" + giatri[1] + "' and '" + giatri[2] + "' )"
                                    + ", lst1b as ( select a.* from lst1 a, lst1a b where a.KU_MAKH = b.KU_MAKH ), lst2 as ( select a.KU_MAKH,count(a.KU_MAKH) DEM,sum(a.DUNO) DUNO from lst1b a group by a.KU_MAKH having sum(a.DUNO) > 50000000 )"
                                    + " select a.KU_MAPGD,a.MAXA,d.TEN,a.KU_MATO,f.TO_TENTT,a.KU_MAKH,c.KH_TENKH,char(39) + a.KU_SOKU SOKU, a.KU_CHTRINH,e.TENVT, a.NG_VAY, a.KU_NGAYDHAN_1 NG_DHAN, a.DUNO, a.SOTHANG, a.KU_MAPNKT51 PNKT, g.TEN TEN_PNKT"
                                    + " , (case when(a.NG_VAY < '" + giatri[1] + "' and b.DEM > 1) or (a.NG_VAY > '" + giatri[1] + "' and b.DEM = 1)  then 1 else 2 end) LAN ,(case when (a.NG_VAY<'" + giatri[1] + "' and b.DEM>1) or (a.NG_VAY>'" + giatri[1] + "' and b.DEM=1) then a.DUNO else 0 end) DN_LAN1"
                                    + " ,(case when a.NG_VAY >= '" + giatri[1] + "' and b.DEM>1 then a.DUNO else 0 end) DN_LAN2 from lst1 a, lst2 b,hskh c, DMXA d,DM_CHTRINH e, HSTO f,DM_PNKT g "
                                    + " where a.KU_MAKH = b.KU_MAKH and a.KU_MAKH = c.KH_MAKH and a.MAXA = d.MA and a.KU_CHTRINH = e.CHTRINH and a.KU_MATO = f.TO_MATO and a.KU_MAPNKT51 = g.PNKT and c.KH_TTRANG='A' order by a.MAXA,a.KU_MAKH,a.KU_CHTRINH,a.NG_VAY";
                    else
                        strsql =" with lst1 as ( "
                                   +"select  a.KU_MAPGD, left(a.KU_MADP, 6) MAXA, a.KU_MATO, a.KU_MAKH, a.KU_SOKU, a.KU_CHTRINH, a.KU_NGAYGNCC NG_VAY, a.KU_NGAYDHAN_1, a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO "
                                    +", dbo.SoThang(a.KU_NGAYGNCC, a.KU_NGAYDHAN_1) SOTHANG, a.KU_MAPNKT51 from HSCV_DAILY a where a.KU_MAPGD = '"+ giatri[0] + "' and a.KU_NGAYBC = '"+ giatri[5] + "' and a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH > 0 and a.KU_TTMONVAY <> 'CLOSE' and a.KU_CHTRINH IN('01', '09', '19', '21') "
                                    + " ), lst1a as  ( select a.* from lst1 a where a.NG_VAY between '" + giatri[1] + "' and '" + giatri[2] + "' )"
                                    + ", lst1b as ( select a.* from lst1 a, lst1a b where a.KU_MAKH = b.KU_MAKH ), lst2 as ( select a.KU_MAKH,count(a.KU_MAKH) DEM,sum(a.DUNO) DUNO from lst1b a group by a.KU_MAKH having sum(a.DUNO) > 50000000 )"		
				                    + " select a.KU_MAPGD,a.MAXA,d.TEN,a.KU_MATO,f.TO_TENTT,a.KU_MAKH,c.KH_TENKH,char(39) + a.KU_SOKU SOKU, a.KU_CHTRINH,e.TENVT, a.NG_VAY, a.KU_NGAYDHAN_1 NG_DHAN, a.DUNO, a.SOTHANG, a.KU_MAPNKT51 PNKT, g.TEN TEN_PNKT"
                                    + " , (case when(a.NG_VAY < '" + giatri[1] + "' and b.DEM > 1) or (a.NG_VAY > '" + giatri[1] + "' and b.DEM = 1)  then 1 else 2 end) LAN ,(case when (a.NG_VAY<'"+ giatri[1] + "' and b.DEM>1) or (a.NG_VAY>'" + giatri[1] + "' and b.DEM=1) then a.DUNO else 0 end) DN_LAN1"
                                    + " ,(case when a.NG_VAY >= '" + giatri[1] + "' and b.DEM>1 then a.DUNO else 0 end) DN_LAN2 from lst1 a, lst2 b,hskh c, DMXA d,DM_CHTRINH e, HSTO f,DM_PNKT g "
                                    + " where a.KU_MAKH = b.KU_MAKH and a.KU_MAKH = c.KH_MAKH and a.MAXA = d.MA and a.KU_CHTRINH = e.CHTRINH and a.KU_MATO = f.TO_MATO and a.KU_MAPNKT51 = g.PNKT and c.KH_TTRANG='A' order by a.MAXA,a.KU_MAKH,a.KU_CHTRINH,a.NG_VAY";
 
                            dt = cls.LoadDataText(strsql);
                            FileName = Thumuc + "\\" + str.Left(cboPos.SelectedValue.ToString().Trim(), 6) + "_Cho vay nâng mức_từ ngày " + dtpTuNgay.SelectedDate.Value.ToString("ddMMyyyy") + "_Đến ngày_" + dtpDenNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                            str.ExportToExcel(dt, FileName);
                            MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            str.OpenExcel(FileName);

                }
                else if (str.Right(giatri[0].ToString(), 2) == "00") dt = cls.LoadDataProcPara("usp_ChovayNangmuc03", bien, giatri, thamso);
                else dt = cls.LoadDataProcPara("usp_ChovayNangmuc", bien, giatri, thamso);
                if (dt.Rows.Count > 0)
                {
                    if (OptChtr.IsChecked == true)
                    {
                        rpt_ChovayNangmuc01 rpt = new rpt_ChovayNangmuc01();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                       // MessageBox.Show(OptChtr.Content.ToString());
                        FileName = Thumuc + "\\" + giatri[0] + "_"+ OptChtr.Content + "_"+ str.Left(CboChon.SelectedValue.ToString().Trim(), 2) + "_" + giatri[1]+"_Đến ngày_"+giatri[2] + ".xlsx";
                    } else if (OptM03.IsChecked == true)
                    {
                        rpt_ChovayNangmuc03 rpt = new rpt_ChovayNangmuc03();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                        FileName = Thumuc + "\\" + giatri[0] + "_Mãu 03BC_" + str.Left(CboChon.SelectedValue.ToString().Trim(), 2) + "_" + giatri[1] + "_Đến ngày_" + giatri[2] + ".xlsx";
                    }
                   // str.ExportToExcel(dt, FileName);
                   // str.OpenExcel(FileName);

                }
                else
                {
                    MessageBox.Show("Chưa có số liệu", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();
            
        }
        private void DatePicker_SelectedDateChanged(object sender, RoutedEventArgs e)
        {
            if (dtpTuNgay.SelectedDate != null)
            {
                var lastDay = new DateTime(dtpTuNgay.SelectedDate.Value.Year, dtpTuNgay.SelectedDate.Value.AddMonths(1).Month, 25);
                dtpDenNgay.SelectedDate = lastDay;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                cls.ClsConnect();
                var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
                var firstDayOfMonth = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month, 1);
                var lastDay = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month, 25);
                dtpTuNgay.SelectedDate = firstDayOfMonth;
                dtpDenNgay.SelectedDate = lastDay;

                var dtpos = cls.LoadDataText("select PO_MA,PO_TEN from DMPOS order by PO_MA");
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    cboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                cboPos.SelectedIndex = 5;
                var dtchon = cls.LoadDataText("select CHTR,TEN from CHTRINH order by CHTR");
                for (int i = 0; i < dtchon.Rows.Count; i++)
                {
                    CboChon.Items.Add(dtchon.Rows[i][0] + " | " + dtchon.Rows[i][1]);
                }
                CboChon.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message,"Mess");
            }
            //CboChon.Items.Add("003000 | Tất cả");
            cls.DongKetNoi();
        }
    
    }
}
