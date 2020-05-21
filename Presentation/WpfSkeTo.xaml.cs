using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfSkeTo.xaml
    /// </summary>
    public partial class WpfSkeTo
    {
        public WpfSkeTo()
        {
            InitializeComponent();
        }
        //ClsConnectLocal cls = new ClsConnectLocal();
        ClsServer cls = new ClsServer();
        ToolBll bll = new ToolBll();
        string Thumuc = "C:\\Saoke";
        DataTable dt = new DataTable();
        private string str = "";
        private string FileName = "";
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            bll.TaoThuMuc(Thumuc);
            var lastMonth = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month, DateTime.DaysInMonth(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month));
            try
            {

                ServerInfor srv = new ServerInfor();
                cls.ClsConnect();
                const int thamso = 3;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Ngay";
                if (dtpNgay.SelectedDate != null) giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                bien[1] = "@Mato";
                if (Ration3.IsChecked==true) giatri[1] = bll.Left(cboXa.SelectedValue.ToString().Trim(), 6);
                else giatri[1] = bll.Left(cboTo.SelectedValue.ToString().Trim(), 7);
                bien[2] = "@Mau";
                if (Ration1.IsChecked == true || Ration5.IsChecked == true) // Mau KT
                {
                    giatri[2] = "1";
                }
                else //Mau Tat toan
                {
                    giatri[2] = "0";
                }

                if (Ration4.IsChecked == true)
                {
                    if (dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") == lastMonth.ToString("yyyy-MM-dd"))
                    {
                        str = "select a.KU_MAPGD,left(a.KU_MADP,6) MAXA,c.TEN TENXA,a.KU_MATO,(select TO_TENTT from HSTO where TO_MATO=a.KU_MATO) TENTT,b.KH_MAKH,b.KH_TENKH,b.KH_TENVC "
                              + " ,char(39) + a.KU_SOKU SOKU, a.KU_CHTRINH,a.KU_MAQD,(select GIATRI from DMKHAC where KHOA_1='07' and KHOA_2=a.KU_MAQD) TENCHTR, a.KU_NGAYVAY, a.KU_NGAYDHAN_1, a.KU_NGAYDHAN_3 "
                              +
                              " , a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO, a.KU_LAITHAN + a.KU_LAIQHAN LAIDATHU, a.KU_LAITONTHAN + a.KU_LAITONQHAN LAITON "
                              +
                              " , (select SV_TENSV from HSSV where SV_SOKU = a.KU_SOKU) TENSV,a.KU_MAPNKT51,(select GIATRI from DMKHAC where KHOA_1 = '25' and KHOA_2 = a.KU_MAPNKT51) PNKT1 "
                              +
                              " ,a.KU_MAPNKT52,(select GIATRI from DMKHAC where KHOA_1 = '25' and KHOA_2 = a.KU_MAPNKT52) PNKT2,b.KH_DIACHI "
                              + " from HSKU a, HSKH b,DMXA c "
                              +
                              " where a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH>0 and a.KU_TTMONVAY<>'CLOSE' and a.KU_NGAYBC = '" +
                              giatri[0] + "' and a.KU_MATO = '" + giatri[1] +
                              "' and a.KU_MAKH = b.KH_MAKH and left(a.KU_MADP, 6)= c.MA order by b.KH_MAKH,a.KU_CHTRINH";
                    }
                    else
                    {
                        str = "select a.KU_MAPGD,left(a.KU_MADP,6) MAXA,c.TEN TENXA,a.KU_MATO,(select TO_TENTT from HSTO where TO_MATO=a.KU_MATO) TENTT,b.KH_MAKH,b.KH_TENKH, b.KH_TENVC "
                              + " ,char(39) + a.KU_SOKU SOKU, a.KU_CHTRINH,a.KU_MAQD,(select GIATRI from DMKHAC where KHOA_1='07' and KHOA_2=a.KU_MAQD) TENCHTR, a.KU_NGAYVAY, a.KU_NGAYDHAN_1, a.KU_NGAYDHAN_3 "
                              +
                              " , a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH DUNO, a.KU_LAITHAN + a.KU_LAIQHAN LAIDATHU, a.KU_LAITONTHAN + a.KU_LAITONQHAN LAITON "
                              +
                              " , (select SV_TENSV from HSSV where SV_SOKU = a.KU_SOKU) TENSV,a.KU_MAPNKT51,(select GIATRI from DMKHAC where KHOA_1 = '25' and KHOA_2 = a.KU_MAPNKT51) PNKT1 "
                              +
                              " ,a.KU_MAPNKT52,(select GIATRI from DMKHAC where KHOA_1 = '25' and KHOA_2 = a.KU_MAPNKT52) PNKT2,b.KH_DIACHI "
                              + " from HSCV_DAILY a, HSKH b,DMXA c "
                              +
                              " where a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH>0 and a.KU_TTMONVAY<>'CLOSE' and a.KU_NGAYBC = '" +
                              giatri[0] + "' and a.KU_MATO = '" + giatri[1] +
                              "' and a.KU_MAKH = b.KH_MAKH and left(a.KU_MADP, 6)= c.MA order by b.KH_MAKH,a.KU_CHTRINH";
                    }
                    dt = cls.LoadDataText(str);
                    FileName = Thumuc + "\\" + bll.Left(cboPos.SelectedValue.ToString().Trim(), 6) + "_SKETO_"+bll.Left(cboTo.SelectedValue.ToString().Trim(), 7)+"_"+ dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                    bll.ExportToExcel(dt, FileName);
                    MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    bll.OpenExcel(FileName);

                }
                else
                {
                    dt = cls.LoadDataProcPara(Ration3.IsChecked != true ? "usp_sketo" : "usp_SkeBsungTT", bien, giatri,thamso);
                    //rpt_kt740_01 rpt = new rpt_kt740_01();
                    if (dt.Rows.Count > 0)
                    {
                        if (Ration1.IsChecked == true)
                        {
                            rpt_SkeTo rpt = new rpt_SkeTo();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                srv.DbPassSerVer());

                        }
                        else if (Ration2.IsChecked == true)
                        {
                            rpt_SkeTo1 rpt = new rpt_SkeTo1();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                srv.DbPassSerVer());
                        }
                        else if (Ration3.IsChecked == true)
                        {
                            rpt_SkeBsungTT rpt = new rpt_SkeBsungTT();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                srv.DbPassSerVer());
                        }
                        else if (Ration5.IsChecked == true)
                        {
                            rpt_Mau06_01 rpt = new rpt_Mau06_01();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                srv.DbPassSerVer());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chưa có số liệu", "Thông báo");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

           // dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
            try
            {
                cls.ClsConnect();
                string sql = "select PO_MA,PO_TEN from DMPOS where PO_MACN=" + "'" + BienBll.MainPos + "'" +
                             " order by PO_MA";
                //MessageBox.Show(sql);
                var dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    //comboBox1.Items.Add(ds.Tables[0].Rows[i][0] + " " + ds.Tables[0].Rows[i][1] + " " + ds.Tables[0].Rows[i][2]);
                    cboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
            //cboPos.ItemsSource = dtpos.DefaultView;
                //cboPos.DisplayMemberPath = "PO_TEN";
                //cboPos.SelectedValuePath = "PO_MA";

            }
            catch(Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message,"Mess");
            }
            cls.DongKetNoi();
        }

        private void cboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //MessageBox.Show(str.Left(cboPos.SelectedValue.ToString().Trim(),6));
                cboXa.Items.Clear();
                cls.ClsConnect();
                string sql = "select MA,TEN from DMXA where PGD_QL= " + "'" + bll.Left(cboPos.SelectedValue.ToString().Trim(), 6) + "'" + " order by MA";
                var dtxa = cls.LoadDataText(sql);
                for (int i = 0; i < dtxa.Rows.Count; i++)
                {
                    cboXa.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                } 

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }

        private void cboXa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //MessageBox.Show(str.Left(cboXa.SelectedValue.ToString().Trim(), 8));
                cboTo.Items.Clear();
                cls.ClsConnect();
                if (dtpNgay.SelectedDate != null)
                {
                    string sql = "select TO_MATO,TO_TENTT from HSTO where Left(TO_MADP,6) = " + bll.Left(cboXa.SelectedValue.ToString().Trim(), 6) ;
                    //MessageBox.Show(sql);
                    var dtto = cls.LoadDataText(sql);
                    for (int i = 0; i < dtto.Rows.Count; i++)
                    {
                        cboTo.Items.Add(dtto.Rows[i][0] + " | " + dtto.Rows[i][1]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }
    }
}
