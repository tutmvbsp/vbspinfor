using System;
using System.Data;
using System.Windows;
using System.Text;
using System.IO;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for Wpf_kt740.xaml
    /// </summary>
    public partial class WpfKt740 : Window
    {
     
        public WpfKt740()
        {
            InitializeComponent();
        }
        ToolBll bll = new ToolBll();
        ServerInfor srv = new ServerInfor();
        ClsServer cls = new ClsServer();
        DataTable dt = new DataTable();
        private string FileName = "";
        string Thumuc = "C:\\KT740";
        
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //
            try
            {
                bll.TaoThuMuc(Thumuc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lổi tạo thư mục "+ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (Ration01.IsChecked == true)
            {
               //MessageBox.Show("Ratio01");
                // kiem tra khach hang nhieu CASA 105
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    dt = cls.LoadDataProcPara("usp_KT74001", bien, giatri, thamso);
                    if (dt.Rows.Count > 0)
                    {
                        if (Opt1.IsChecked == true)
                        {
                            rpt_kt740_01 rpt = new rpt_kt740_01();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                        }
                        else
                        {
                            FileName = Thumuc + "\\" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_CASA105_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                            //bll.WriteDataTableToExcel(dt, "Details", FileName, "tutm : 0985165777");
                            bll.ExportToExcel(dt,FileName);
                            bll.OpenExcel(FileName);
                            MessageBox.Show("Copy Excel to : " + FileName,"Thông báo",MessageBoxButton.OK,MessageBoxImage.Information);

                        }
                    }
                    else
                    {
                        MessageBox.Show("Không có bản ghi nào", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    cls.DongKetNoi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
            else if (Ration02.IsChecked == true)
            {
               // MessageBox.Show("Ratio02");
                //kiểm tra KH nhiều CIF
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    dt = cls.LoadDataProcPara("usp_KT74002", bien, giatri, thamso);
                    if (dt.Rows.Count > 0)
                    {
                        if (Opt1.IsChecked == true)
                        {
                            rpt_kt740_02 rpt = new rpt_kt740_02();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        }
                        else
                        {
                            FileName = Thumuc + "\\" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_CIF_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                            //bll.WriteDataTableToExcel(dt, "Details", FileName, "tutm : 0985165777");
                            bll.ExportToExcel(dt, FileName);
                            bll.OpenExcel(FileName);
                            MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        }
                    }
                    else
                    {
                        MessageBox.Show("Không có bản ghi nào", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    cls.DongKetNoi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi : "+ex.Message);
                }

            }
            else if (Ration03.IsChecked == true) //sao ma to CASA 105 va KU
            {
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    dt = cls.LoadDataProcPara("usp_KT74003", bien, giatri, thamso);
                    if (dt.Rows.Count > 0)
                    {
                        if (Opt1.IsChecked == true)
                        {
                            rpt_kt74003 rpt = new rpt_kt74003();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        }
                        else
                        {
                            FileName = Thumuc + "\\" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_MATO_KU_KHAC_CASA_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                            //bll.WriteDataTableToExcel(dt, "Details", FileName, "tutm : 0985165777");
                            bll.ExportToExcel(dt, FileName);
                            bll.OpenExcel(FileName);
                            MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        }
                    }
                    else
                    {
                        MessageBox.Show("Không có bản ghi nào", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    cls.DongKetNoi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi : "+ex.Message);
                }
            }
            else if (Ration04.IsChecked == true) //kiem tra phat sinh thu lai
            {
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    dt = cls.LoadDataProcPara("usp_KT74004", bien, giatri, thamso);
                    //rpt_kt740_04 rpt = new rpt_kt740_04();
                    //RPUtility.ShowRp(rpt, dt, this, dbs.GetAppSetting("DATABASENAME"), dbs.GetAppSetting("SERVERNAME"), dbs.GetAppSetting("USENAME"), dbs.GetAppSetting("PASS"));
                    //RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                    if (dt.Rows.Count > 0)
                    {
                        //rpt_kt740_06 rpt = new rpt_kt740_06();
                        // RPUtility.ShowRp(rpt, dt, this, dbs.GetAppSetting("DATABASENAME"), dbs.GetAppSetting("SERVERNAME"), dbs.GetAppSetting("USENAME"), dbs.GetAppSetting("PASS"));
                        //RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                        if (Opt1.IsChecked == true)
                        {
                            //rpt_kt740_05 rpt = new rpt_kt740_05();
                            //RPUtility.ShowRp(rpt, dt, this, dbs.GetAppSetting("DATABASENAME"), dbs.GetAppSetting("SERVERNAME"), dbs.GetAppSetting("USENAME"), dbs.GetAppSetting("PASS"));
                            //RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                            MessageBox.Show("Chưa có Report", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            FileName = Thumuc + "\\" + giatri[0] + "_PS_THULAI_TO_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                            bll.ExportToExcel(dt, FileName);
                            //bll.ExportDTToExcel(dt,FileName);
                            //bll.WriteDataTableToExcel(dt, "Details", FileName, "tutm : 0985165777");
                            //bll.ToCSV(dt, sw, true);
                            MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            //bll.OpenCSVWithExcel(FileName);
                            bll.OpenExcel(FileName);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không có trường hợp nào ", "Thông báo");
                    }

                    cls.DongKetNoi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi : " + ex.Message);
                }
            }
            else if (Ration05.IsChecked == true) // kiểm tra thu tiet kiem
            {
                try
                {
                    cls.ClsConnect();
                    int thamso = 3;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@TuNgay";
                    giatri[1] = "31/01/" + DtpNgay.SelectedDate.Value.ToString("yyyy");
                    bien[2] = "@DenNgay";
                    giatri[2] = DtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
                    dt = cls.LoadDataProcPara("usp_KT74005", bien, giatri, thamso);
                    if (dt.Rows.Count > 0)
                    {
                        //rpt_kt740_06 rpt = new rpt_kt740_06();
                        // RPUtility.ShowRp(rpt, dt, this, dbs.GetAppSetting("DATABASENAME"), dbs.GetAppSetting("SERVERNAME"), dbs.GetAppSetting("USENAME"), dbs.GetAppSetting("PASS"));
                        //RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                        if (Opt1.IsChecked == true)
                        {
                            //rpt_kt740_05 rpt = new rpt_kt740_05();
                            //RPUtility.ShowRp(rpt, dt, this, dbs.GetAppSetting("DATABASENAME"), dbs.GetAppSetting("SERVERNAME"), dbs.GetAppSetting("USENAME"), dbs.GetAppSetting("PASS"));
                            //RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                            MessageBox.Show("Chưa có Report", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            FileName = Thumuc + "\\" + giatri[0] + "_PS_THUTK_TO_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                            bll.ExportToExcel(dt, FileName);
                            //bll.ExportDTToExcel(dt,FileName);
                            //bll.WriteDataTableToExcel(dt, "Details", FileName, "tutm : 0985165777");
                            //bll.ToCSV(dt, sw, true);
                            MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK,MessageBoxImage.Information);
                            //bll.OpenCSVWithExcel(FileName);
                            bll.OpenExcel(FileName);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không có trường hợp nào ", "Thông báo");
                    }

                    cls.DongKetNoi();
                    //MessageBox.Show("Chưa thực hiện");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi : " + ex.Message);
                }
            }
            else if (Ration06.IsChecked == true) /* kiểm tra KU có tổ khac nhau*/
            {
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    dt = cls.LoadDataProcPara("usp_KT74006", bien, giatri, thamso);
                    if (dt.Rows.Count > 0)
                    {
                        //rpt_kt740_06 rpt = new rpt_kt740_06();
                        // RPUtility.ShowRp(rpt, dt, this, dbs.GetAppSetting("DATABASENAME"), dbs.GetAppSetting("SERVERNAME"), dbs.GetAppSetting("USENAME"), dbs.GetAppSetting("PASS"));
                        //RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                        if (Opt1.IsChecked == true)
                        {
                            rpt_kt740_06 rpt = new rpt_kt740_06();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                            //MessageBox.Show("Chưa có Report", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            FileName = Thumuc + "\\" + giatri[0] + "_VAY_NHIEU_TO_" +DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                            bll.ExportToExcel(dt, FileName);
                            //bll.ExportDTToExcel(dt,FileName);
                            //bll.WriteDataTableToExcel(dt, "Details", FileName, "tutm : 0985165777");
                            //bll.ToCSV(dt, sw, true);
                            MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK,
                                MessageBoxImage.Information);
                            //bll.OpenCSVWithExcel(FileName);
                            bll.OpenExcel(FileName);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không có trường hợp nào ", "Thông báo");
                    }

                    cls.DongKetNoi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi : " + ex.Message);
                }
            }
            else if (Ration07.IsChecked == true) /* kiểm tra vay vuot */
            {
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    dt = cls.LoadDataProcPara("usp_KT74007", bien, giatri, thamso);
                    if (dt.Rows.Count > 0)
                    {
                        //rpt_kt740_07 rpt = new rpt_kt740_07();
                        // RPUtility.ShowRp(rpt, dt, this, dbs.GetAppSetting("DATABASENAME"), dbs.GetAppSetting("SERVERNAME"), dbs.GetAppSetting("USENAME"), dbs.GetAppSetting("PASS"));
                        //RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                        if (Opt1.IsChecked == true)
                        {
                            rpt_kt740_07 rpt = new rpt_kt740_07();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                srv.DbPassSerVer());
                            //MessageBox.Show("Chưa có Report", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            FileName = Thumuc + "\\" + giatri[0] + "_VAY_VUOT_" +
                                       DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                            bll.ExportToExcel(dt, FileName);
                            //bll.ExportDTToExcel(dt,FileName);
                            //bll.WriteDataTableToExcel(dt, "Details", FileName, "tutm : 0985165777");
                            //bll.ToCSV(dt, sw, true);
                            MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK,
                                MessageBoxImage.Information);
                            //bll.OpenCSVWithExcel(FileName);
                            bll.OpenExcel(FileName);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không có trường hợp nào ", "Thông báo");
                    }
                    cls.DongKetNoi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi : " + ex.Message);
                }
            }
            else if (Ration08.IsChecked == true) /* kiểm tra to khong dat yeu cau */
            {
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    dt = cls.LoadDataProcPara("usp_KT74008", bien, giatri, thamso);
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có trường hợp nào");
                    }
                    else
                    {
                        //rpt_kt74008 rpt = new rpt_kt74008();
                        //RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        if (Opt1.IsChecked == true)
                        {
                            rpt_kt740_09 rpt = new rpt_kt740_09();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        }
                        else
                        {
                            FileName = Thumuc + "\\" + giatri[0] + "_TO_KHONG_DAT_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                            bll.ExportToExcel(dt, FileName);
                            MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            bll.OpenExcel(FileName);
                        }

                    }
                    cls.DongKetNoi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi : " + ex.Message);
                }
            }
            else if (Ration9.IsChecked == true) /* kiểm tra chong cheo */
            {
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    dt = cls.LoadDataProcPara("usp_KT74009", bien, giatri, thamso);
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có trường hợp nào");
                    }
                    else
                    {
                        // rpt_kt740_09 rpt = new rpt_kt740_09();
                        //RPUtility.ShowRp(rpt, dt, this, dbs.GetAppSetting("DATABASENAME"), dbs.GetAppSetting("SERVERNAME"), dbs.GetAppSetting("USENAME"), dbs.GetAppSetting("PASS"));
                        // RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        if (Opt1.IsChecked == true)
                        {
                            rpt_kt740_09 rpt = new rpt_kt740_09();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                        }
                        else
                        {
                            FileName = Thumuc + "\\" + giatri[0] + "_VAY_CHONG_CHEO_" +DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                            bll.ExportToExcel(dt, FileName);
                            MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK,MessageBoxImage.Information);
                            bll.OpenExcel(FileName);
                        }
                    }
                    cls.DongKetNoi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi : " + ex.Message);
                }
            }

            else if (Ration10.IsChecked == true) /* kiểm tra to truong co vo/chong vay */
            {
                try
                    
                {
                    
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    dt = cls.LoadDataProcPara("usp_KT74010", bien, giatri, thamso);
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có trường hợp nào");
                    }
                    else
                    {
                        // radGridView1.ItemsSource = dt;
                        // rpt_kt74010 rpt = new rpt_kt74010();
                        // RPUtility.ShowRp(rpt, dt, this, dbs.GetAppSetting("DATABASENAME"), dbs.GetAppSetting("SERVERNAME"), dbs.GetAppSetting("USENAME"), dbs.GetAppSetting("PASS"));                        
                        // RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        // string filename = "C:\\Tam\\VoChongTT" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + ".xlsx";
                        // bll.WriteDataTableToExcel(dt, "Person Details", filename, "Details");
                        if (Opt1.IsChecked == true)
                        {

                            rpt_kt74022 rpt = new rpt_kt74022();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        }
                        else
                        {
                            FileName = Thumuc + "\\" + giatri[0] + "_TOTRUONG_CO_VOCHONG_VAY_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                            bll.ExportToExcel(dt, FileName);
                            //bll.ExportDTToExcel(dt,FileName);
                            //bll.WriteDataTableToExcel(dt, "Details", FileName, "tutm : 0985165777");
                            //bll.ToCSV(dt, sw, true);
                            MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            //bll.OpenCSVWithExcel(FileName);
                            bll.OpenExcel(FileName);

                        }


                    }
                    cls.DongKetNoi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi : " + ex.Message);
                }
            }

            else if (Ration11.IsChecked == true) /* khe uoc 3 thang khong hoat dong */
            {
                //_Pos = radCboPos.SelectedValue.ToString().Trim();
                //MessageBox.Show(_Pos);
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    dt = cls.LoadDataProcPara("usp_KT74011", bien, giatri, thamso);
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có trường hợp nào");
                    }
                    else
                    {
                        //rpt_kt74011 rpt = new rpt_kt74011();
                        //RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        if (Opt1.IsChecked == true)
                        {

                            rpt_kt74011 rpt = new rpt_kt74011();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        }
                        else
                        {
                            FileName = Thumuc + "\\" + giatri[0] + "_KU_3T_KHONG_HD_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                            bll.ExportToExcel(dt, FileName);
                            MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            bll.OpenExcel(FileName);

                        }
                    }
                    cls.DongKetNoi();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi : " + ex.Message);
                }
            }
            else if (Ration12.IsChecked == true) /* kiem tra CASA 105 co KU het du no nhung casa con tien */
            {
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    dt = cls.LoadDataProcPara("usp_KT74012", bien, giatri, thamso);
                    //radGridView1.ItemsSource = dt;
                    
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có trường hợp nào");
                    }
                    else
                    {
                        // rpt_kt74012 rpt = new rpt_kt74012();
                        // RPUtility.ShowRp(rpt, dt, this, dbs.GetAppSetting("DATABASENAME"), dbs.GetAppSetting("SERVERNAME"), dbs.GetAppSetting("USENAME"), dbs.GetAppSetting("PASS"));
                        // RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        if (Opt1.IsChecked == true)
                        {

                            rpt_kt74012 rpt = new rpt_kt74012();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        }
                        else
                        {
                            FileName = Thumuc + "\\" + giatri[0] + "_HET_DUNO_CON_CASA105_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                            bll.ExportToExcel(dt, FileName);
                            MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            bll.OpenExcel(FileName);

                        }

                    }
                    cls.DongKetNoi();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi : " + ex.Message);
                }
            }
            else if (Ration13.IsChecked == true) /* kiem tra CAP QQLV GQVL */
            {
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    dt = cls.LoadDataProcPara("usp_KT74013", bien, giatri, thamso);
                    //radGridView1.ItemsSource = dt;
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có trường hợp nào");
                    }
                    else
                    {
                        //rpt_kt74013 rpt = new rpt_kt74013();
                        //RPUtility.ShowRp(rpt, dt, this, dbs.GetAppSetting("DATABASENAME"), dbs.GetAppSetting("SERVERNAME"), dbs.GetAppSetting("USENAME"), dbs.GetAppSetting("PASS"));
                        //RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        FileName = Thumuc + "\\" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_THIEU_CAPQLV_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                        bll.ExportToExcel(dt, FileName);
                        bll.OpenExcel(FileName);
                        MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);


                    }
                    cls.DongKetNoi();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi : " + ex.Message);
                }
            }
            else if (Ration14.IsChecked == true) /* kiem tra ho co du no tren 100 tr */
            {
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    dt = cls.LoadDataProcPara("usp_KT74014", bien, giatri, thamso);
                   // radGridView1.ItemsSource = dt;
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có trường hợp nào");
                    }
                    else
                    {
                        // rpt_kt74014 rpt = new rpt_kt74014();
                        // RPUtility.ShowRp(rpt, dt, this, dbs.GetAppSetting("DATABASENAME"), dbs.GetAppSetting("SERVERNAME"), dbs.GetAppSetting("USENAME"), dbs.GetAppSetting("PASS"));
                        // RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        FileName = Thumuc + "\\" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_KH_DN_TREN_100TR_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                        bll.ExportToExcel(dt, FileName);
                        bll.OpenExcel(FileName);
                        MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);


                    }
                    cls.DongKetNoi();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi : " + ex.Message);
                }
            }
            else if (Ration15.IsChecked == true) /* kiem tra ku dang ky tren 3 thang khong giai ngan */
            {
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    dt = cls.LoadDataProcPara("usp_KT74015", bien, giatri, thamso);
                    //radGridView1.ItemsSource = dt;
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có trường hợp nào");
                    }
                    else
                    {
                        //rpt_kt74015 rpt = new rpt_kt74015();
                        // RPUtility.ShowRp(rpt, dt, this, dbs.GetAppSetting("DATABASENAME"), dbs.GetAppSetting("SERVERNAME"), dbs.GetAppSetting("USENAME"), dbs.GetAppSetting("PASS"));
                        // RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        FileName = Thumuc + "\\" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_DK_KU_3T_KHONG_GN_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                        bll.ExportToExcel(dt, FileName);
                        bll.OpenExcel(FileName);
                        MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);


                    }
                    cls.DongKetNoi();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi : " + ex.Message);
                }
            } 
            else if (Ration17.IsChecked == true) /* kiem tra Nhiều KU cùng chtrinh vay */
            {
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    dt = cls.LoadDataProcPara("usp_KT74016", bien, giatri, thamso);
                   // MessageBox.Show(giatri[0] + "   " + giatri[1]);
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có trường hợp nào");
                    }
                    else
                    {
                        //FileName = Thumuc + "\\" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_VAYCHUNGCHTR_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                        //FileStream fs = new FileStream(FileName, FileMode.Create);
                        //StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
                        //bll.ToCSV(dt, sw, true);
                        //MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        FileName = Thumuc + "\\" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_1_CHTR_CO_NHIEU_KU_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                        bll.ExportToExcel(dt, FileName);
                        bll.OpenExcel(FileName);
                        MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    cls.DongKetNoi();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi : " + ex.Message);
                }
               
            }
            else if (Ration16.IsChecked == true) /* kiem tra gia han tre */
            {
                //ClsOracle clsora = new ClsOracle();
                try
                {
                  //  string Ngay = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
                   // string MaPos = bll.Left(cboPos.SelectedValue.ToString().Trim(), 6);
                    //DataTable dt = new DataTable();
                   // clsora.ClsConnect();
                    //MessageBox.Show(tk);

                    //string sql = "select b.ku_soku,b.ku_ngaydhan_2,a.SBT,a.NGAYBC from hsbt a,hsku b where a.sbt=b.ku_soku and substr(a.tk_co,2,2) in ('13','15','16','19') and substr(a.tk_co,5,1) in ('6','7')" +
                    //             "and to_char(b.ku_ngaybc,'dd/mm/yyyy')='"+Ngay+"' and to_char(a.ng_capnhat,'dd/mm/yyyy')='"+Ngay+"' and a.MAPGD='"+MaPos+"' order by a.sbt ";
                    //string sql = "select distinct c.kh_makh,c.kh_tenkh,b.ku_mato,b.ku_soku,to_char(b.ku_ngaydhan_1,'dd/MM/yyyy') as DH_GOC,to_char(b.ku_ngaydhan_2,'dd/MM/yyyy') as DENHAN" +
                    //             ",a.SBT,to_char(a.NGAYBC,'dd/MM/yyyy') as NG_GIAHAN from hsbt a,hsku b,hskh c where b.ku_makh=c.kh_makh and a.sbt=b.ku_soku" +
                    //             " and substr(a.tk_co,2,2) in ('13','15','16','19') and substr(a.tk_co,5,1) in ('6','7')" +
                    //             "and to_char(b.ku_ngaybc,'dd/mm/yyyy')='" + Ngay + "' and a.MAPGD='" + MaPos + "' order by a.sbt ";

                    //MessageBox.Show(sql);
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    dt = cls.LoadDataProcPara("usp_KT74020", bien, giatri, thamso);
                    // MessageBox.Show(giatri[0] + "   " + giatri[1]);
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có trường hợp nào");
                    }
                    else
                    {
                        //rpt_kt74020 rpt = new rpt_kt74020();
                        //RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        // dataGrid1.ItemsSource = dt.DefaultView;
                        FileName = Thumuc + "\\" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_GIA_HAN_TRE_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                        bll.ExportToExcel(dt, FileName);
                        bll.OpenExcel(FileName);
                        MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    cls.DongKetNoi();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi : " + ex.Message);
                }
                
            }

            else if (Ration18.IsChecked == true) /* kiem tra giai ngan va thu tiet kiem to */
            {
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    dt = cls.LoadDataProcPara("usp_KT74017", bien, giatri, thamso);
                    // MessageBox.Show(giatri[0] + "   " + giatri[1]);
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có trường hợp nào");
                    }
                    else
                    {
                        //MessageBox.Show("Có số liệu");
                        //rpt_kt74017 rpt = new rpt_kt74017();
                        //RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                        //dataGrid1.ItemsSource = dt.DefaultView;
                        FileName = Thumuc + "\\" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_GiaiNgan_Thu_TKTO_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                        //bll.WriteDataTableToExcel(dt, "Details", FileName, "tutm : 0985165777");
                        bll.ExportToExcel(dt, FileName);
                        bll.OpenExcel(FileName);
                        MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);


                    }
                    cls.DongKetNoi();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi : " + ex.Message);
                }
            }

            else if (Ration19.IsChecked == true) /* to truong vay tai to khac */
            {
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    dt = cls.LoadDataProcPara("usp_KT74018", bien, giatri, thamso);
                    // MessageBox.Show(giatri[0] + "   " + giatri[1]);
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có trường hợp nào");
                    }
                    else
                    {
                        //rpt_kt74018 rpt = new rpt_kt74018();
                        //RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        //dataGrid1.ItemsSource = dt.DefaultView;
                        FileName = Thumuc + "\\" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_TOTRUONG_VAY_TOKHAC_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                        //bll.WriteDataTableToExcel(dt, "Details", FileName, "tutm : 0985165777");
                        bll.ExportToExcel(dt, FileName);
                        bll.OpenExcel(FileName);
                        MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);


                    }
                    cls.DongKetNoi();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi : " + ex.Message);
                }
            }
            else if (Ration20.IsChecked == true) /* gửi tk tổ <10.000 */
            {
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    if (DtpNgay.SelectedDate != null) giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    dt = cls.LoadDataProcPara("usp_KT74019", bien, giatri, thamso);
                    // MessageBox.Show(giatri[0] + "   " + giatri[1]);
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có trường hợp nào");
                    }
                    else
                    {
                       
                        rpt_kt74019 rpt = new rpt_kt74019();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        //dataGrid1.ItemsSource = dt.DefaultView;

                    }
                    cls.DongKetNoi();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi : " + ex.Message);
                }
            }
            else if (Ration21.IsChecked == true) /* Co du no nhung khong co CASA 105*/
            {
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    if (DtpNgay.SelectedDate != null)
                        if (DtpNgay.SelectedDate != null) giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    dt = cls.LoadDataProcPara("usp_KT74022", bien, giatri, thamso);
                    // MessageBox.Show(giatri[0] + "   " + giatri[1]);
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có trường hợp nào");
                    }
                    else
                    {

                        if (Opt1.IsChecked == true)
                        {

                           // rpt_kt74022 rpt = new rpt_kt74022();
                           // RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        }
                        else
                        {
                            FileName = Thumuc + "\\" + giatri[0] + "_NOCASA105_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                            bll.ExportToExcel(dt, FileName);
                            //bll.ExportDTToExcel(dt,FileName);
                            //bll.WriteDataTableToExcel(dt, "Details", FileName, "tutm : 0985165777");
                            //bll.ToCSV(dt, sw, true);
                            MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            //bll.OpenCSVWithExcel(FileName);
                            bll.OpenExcel(FileName);

                        }

                    }
                    cls.DongKetNoi();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi : " + ex.Message);
                }
            }
            else if (Ration22.IsChecked == true)
            {
                    // kiem tra sinh viên có nhiều KU
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    if (DtpNgay.SelectedDate != null)
                    {
                       
                            giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                            dt = cls.LoadDataProcPara("usp_KT74023", bien, giatri, thamso);
                            if (dt.Rows.Count > 0)
                            {
                                if (Opt1.IsChecked == true)
                                {
                                    rpt_kt740_01 rpt = new rpt_kt740_01();
                                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                                }
                                else
                                {
                                    FileName = Thumuc + "\\" + giatri[0] + "_HSSV_CO_NHIEU_KU_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                    bll.ExportToExcel(dt, FileName);
                                    MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                    bll.OpenExcel(FileName);
                                }
                        }
                            else
                            {
                                MessageBox.Show("Không có bản ghi nào", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                       
                    }
                    cls.DongKetNoi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else if (Ration23.IsChecked == true)
            {
                // kiểm tra thông tin gia hạn
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    if (DtpNgay.SelectedDate != null)
                    {

                        giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                        dt = cls.LoadDataProcPara("usp_KT74024", bien, giatri, thamso);
                        if (dt.Rows.Count > 0)
                        {
                            if (Opt1.IsChecked == true)
                            {
                               // rpt_kt740_01 rpt = new rpt_kt740_01();
                               // RPUtility.ShowRp(rpt, dt, this, srv.DbSourceLocal(), srv.DbNameLocal(), srv.DbUserLocal(),srv.DbPassLocal());
                                MessageBox.Show("Chưa có Report", "Thông báo", MessageBoxButton.OK,MessageBoxImage.Warning);
                            }
                            else
                            {
                                FileName = Thumuc + "\\" + giatri[0] + "_THONGTIN_GIA_HHAN_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                bll.ExportToExcel(dt, FileName);
                                MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                bll.OpenExcel(FileName);

                            }
                        }
                        else
                        {
                            MessageBox.Show("Không có bản ghi nào", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                    }
                    cls.DongKetNoi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else if (Ration24.IsChecked == true)
            {
                // kiểm tra tăng phiên GDX
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    if (DtpNgay.SelectedDate != null)
                    {

                        giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                        dt = cls.LoadDataProcPara("usp_TangPhien", bien, giatri, thamso);
                        if (dt.Rows.Count > 0)
                        {
                            if (Opt1.IsChecked == true)
                            {
                                // rpt_kt740_01 rpt = new rpt_kt740_01();
                                // RPUtility.ShowRp(rpt, dt, this, srv.DbSourceLocal(), srv.DbNameLocal(), srv.DbUserLocal(),srv.DbPassLocal());
                                MessageBox.Show("Chưa có Report", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            else
                            {
                                FileName = Thumuc + "\\" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_TANGPHIEN_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                                bll.ExportToExcel(dt, FileName);
                                MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                bll.OpenExcel(FileName);

                            }
                        }
                        else
                        {
                            MessageBox.Show("Không có bản ghi nào", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                    }
                    cls.DongKetNoi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else if (Ration25.IsChecked == true)
            {
                #region
                // kiểm tra phân kỳ trả nợ HSSV
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    if (DtpNgay.SelectedDate != null)
                    {

                        giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                        dt = cls.LoadDataProcPara("usp_kt74025", bien, giatri, thamso);
                        if (dt.Rows.Count > 0)
                        {
                            if (Opt1.IsChecked == true)
                            {
                                // rpt_kt740_01 rpt = new rpt_kt740_01();
                                // RPUtility.ShowRp(rpt, dt, this, srv.DbSourceLocal(), srv.DbNameLocal(), srv.DbUserLocal(),srv.DbPassLocal());
                                MessageBox.Show("Chưa có Report", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            else
                            {
                                FileName = Thumuc + "\\" + giatri[0] + "_HSSV_CHUA_PKTN_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                bll.ExportToExcel(dt, FileName);
                                MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                bll.OpenExcel(FileName);

                            }
                        }
                        else
                        {
                            MessageBox.Show("Không có bản ghi nào", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                    }
                    cls.DongKetNoi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                #endregion
            } else if (Ration26.IsChecked == true)
            {
                #region
                // kiểm tra thu TGTKTO bang TM truoc khi toat toan KU
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    if (DtpNgay.SelectedDate != null)
                    {

                        giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                        dt = cls.LoadDataProcPara("usp_kt74027", bien, giatri, thamso);
                        if (dt.Rows.Count > 0)
                        {
                            if (Opt1.IsChecked == true)
                            {
                                // rpt_kt740_01 rpt = new rpt_kt740_01();
                                // RPUtility.ShowRp(rpt, dt, this, srv.DbSourceLocal(), srv.DbNameLocal(), srv.DbUserLocal(),srv.DbPassLocal());
                                MessageBox.Show("Chưa có Report", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            else
                            {
                                FileName = Thumuc + "\\" + giatri[0] + "_THU_TGTK105_TM_TATTOAN_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                bll.ExportToExcel(dt, FileName);
                                //bll.ExportDTToExcel(dt,FileName);
                                //bll.WriteDataTableToExcel(dt, "Details", FileName, "tutm : 0985165777");
                                //bll.ToCSV(dt, sw, true);
                                MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                //bll.OpenCSVWithExcel(FileName);
                                bll.OpenExcel(FileName);

                            }
                        }
                        else
                        {
                            MessageBox.Show("Không có bản ghi nào", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                    }
                    cls.DongKetNoi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                #endregion
            }
            else if (Ration27.IsChecked == true)
            {
                #region
                // Kiểm tra chủ hộ và thừa kế cùng vay
                try
                {
                    cls.ClsConnect();
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    if (DtpNgay.SelectedDate != null)
                    {

                        giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                        dt = cls.LoadDataProcPara("usp_kt74028", bien, giatri, thamso);
                        if (dt.Rows.Count > 0)
                        {
                            if (Opt1.IsChecked == true)
                            {
                                // rpt_kt740_01 rpt = new rpt_kt740_01();
                                // RPUtility.ShowRp(rpt, dt, this, srv.DbSourceLocal(), srv.DbNameLocal(), srv.DbUserLocal(),srv.DbPassLocal());
                                MessageBox.Show("Chưa có Report", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            else
                            {
                                FileName = Thumuc + "\\" + giatri[0] + "_CHUHO_THUAKE_CUNGVAY_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                bll.ExportToExcel(dt, FileName);
                                //bll.ExportDTToExcel(dt,FileName);
                                //bll.WriteDataTableToExcel(dt, "Details", FileName, "tutm : 0985165777");
                                //bll.ToCSV(dt, sw, true);
                                MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                //bll.OpenCSVWithExcel(FileName);
                                bll.OpenExcel(FileName);

                            }
                        }
                        else
                        {
                            MessageBox.Show("Không có bản ghi nào", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                    }
                    cls.DongKetNoi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                #endregion
            }
            else if (Ration28.IsChecked == true)
            {
                #region
                // Tỷ lệ giao dịch
                try
                {
                    cls.ClsConnect();
                    int thamso = 1;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@Ngay";
                    if (DtpNgay.SelectedDate != null)
                    {

                        giatri[0] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                        dt = cls.LoadDataProcPara("usp_TLGD", bien, giatri, thamso);
                        if (dt.Rows.Count > 0)
                        {
                            if (Opt1.IsChecked == true)
                            {
                                 rpt_TLGD rpt = new rpt_TLGD();
                                 RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                                //MessageBox.Show("Chưa có Report", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            else
                            {
                                FileName = Thumuc + "\\" + giatri[0] + "_TYLE_GD_" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                                bll.ExportToExcel(dt, FileName);
                                //bll.ExportDTToExcel(dt,FileName);
                                //bll.WriteDataTableToExcel(dt, "Details", FileName, "tutm : 0985165777");
                                //bll.ToCSV(dt, sw, true);
                                MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                //bll.OpenCSVWithExcel(FileName);
                                bll.OpenExcel(FileName);

                            }
                        }
                        else
                        {
                            MessageBox.Show("Không có bản ghi nào", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                    }
                    cls.DongKetNoi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                #endregion
            }




        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           try
            {
                cls.ClsConnect();
                string sql = "select PO_MA,PO_TEN from DMPOS where PO_MACN=" + "'" + BienBll.MainPos + "'" + " order by PO_MA";
                var dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    //comboBox1.Items.Add(ds.Tables[0].Rows[i][0] + " " + ds.Tables[0].Rows[i][1] + " " + ds.Tables[0].Rows[i][2]);
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                CboPos.SelectedIndex = 1;
                var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                DtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }

    
  
   
   
    }
}
