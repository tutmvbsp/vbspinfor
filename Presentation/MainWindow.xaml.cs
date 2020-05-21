using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml;
using System.Windows.Input;
using BLL;
using DAL;
using MohammadDayyanCalendar;

namespace Presentation
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        private readonly ClsServer cls = new ClsServer();
        private readonly ToolBll str = new ToolBll();
        System.Timers.Timer timer = new System.Timers.Timer(1000);

        public MainWindow()
        {
            InitializeComponent();
            MDCalendar mdCalendar = new MDCalendar();
            DateTime date = DateTime.Now;
            TimeZone time = TimeZone.CurrentTimeZone;
            TimeSpan difference = time.GetUtcOffset(date);
            uint currentTime = mdCalendar.Time() + (uint)difference.TotalSeconds;
            //persianCalendar.Content = mdCalendar.Date("Y/m/D  W", currentTime, true);
            christianityCalendar.Content = mdCalendar.Date("P Z/e/d", currentTime, false);

            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = true;

        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //http://thispointer.spaces.live.com/blog/cns!74930F9313F0A720!252.entry?_c11_blogpart_blogpart=blogview&_c=blogpart#permalink
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
            {
                secondHand.Angle = DateTime.Now.Second * 6;
                minuteHand.Angle = DateTime.Now.Minute * 6;
                hourHand.Angle = (DateTime.Now.Hour * 30) + (DateTime.Now.Minute * 0.5);
            }));
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        //He Thong
        private void mnuThoat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                var str = "update NG_DUNG set ND_LOGIN='F' where ND_MA='" + BienBll.Ndma.Trim() + "'";
                cls.UpdateDataText(str);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }

        private void mnuServices_Click(object sender, RoutedEventArgs e)
        {
            //ServiceController serviceController = new ServiceController("MSSQL$SQLEXPRESS");
            var serviceController = new ServiceController("MSSQL$SQLEXPRESS");
            try
            {
                if ((serviceController.Status.Equals(ServiceControllerStatus.Running)) ||
                    (serviceController.Status.Equals(ServiceControllerStatus.StartPending)))
                {
                    serviceController.Stop();
                }
                serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
                serviceController.Start();
                serviceController.WaitForStatus(ServiceControllerStatus.Running);
                MessageBox.Show("Services Restart OK", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Error Services Restart", "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            // MessageBox.Show("Services Restart","Mess",MessageBoxButton.OK,MessageBoxImage.Information);
        }

        private void mnuBack_Click(object sender, RoutedEventArgs e)
        {
            if (BienBll.Quyen.Trim() == "1")
            {
                var f = new WpfBack();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Sorry ! Is not for you. Reture now", "Mess", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void mnuImportText_Click(object sender, RoutedEventArgs e)
        {
            if (BienBll.Quyen.Trim() == "1")
            {
                var f = new WpfImportText();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Sorry ! Is not for you. Reture now", "Mess", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void mnuImportDS_Click(object sender, RoutedEventArgs e)
        {
            if (BienBll.Quyen.Trim() == "1")
            {
                var f = new WpfImpPortDS();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Sorry ! Is not for you. Reture now", "Mess", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void MnuUpdateData_Click(object sender, RoutedEventArgs e)
        {
            if (BienBll.Quyen.Trim() == "1")
            {
                var f = new WpfUpdateSL();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Sorry ! Is not for you. Reture now", "Mess", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void mnuUpdateOffline_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfUpdateOFN();
            f.ShowDialog();
        }

        private void mnuExcel_Click(object sender, RoutedEventArgs e)
        {
            if (BienBll.Quyen.Trim() == "1")
            {
                var f = new WpfExcel();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Sorry ! Is not for you. Reture now", "Mess", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void mnuWord_Click(object sender, RoutedEventArgs e)
        {
            if (BienBll.Quyen.Trim() == "1")
            {
                var f = new WpfWord();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Sorry ! Is not for you. Reture now", "Mess", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void mnuLogOut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                var str = "update NG_DUNG set ND_LOGIN='F' where ND_MA='" + BienBll.Ndma.Trim() + "'";
                cls.UpdateDataText(str);
                Close();
                var f = new LogIn();
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();
        }

        //End He Thong

        //Ke Toan
        private void mnuPstk_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfPstk();
            f.ShowDialog();
        }

        private void mnuKhtc01_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfKhtc01();
            f.ShowDialog();
        }

        private void mnuKhtc02_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfKhtc02();
            f.ShowDialog();
        }

        private void mnuKhtc03_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfKhtc03();
            f.ShowDialog();
        }

        private void mnuKhtc04_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfKhtc04();
            f.ShowDialog();
        }

        private void mnuKt_Khtc_Click(object sender, RoutedEventArgs e)
        {
            if (BienBll.ChucVu.Trim() == "1")
            {
                var f = new WpfQt_Khtc();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền vào mục này", "Thông báo", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void mnuDonGia_Click(object sender, RoutedEventArgs e)
        {
            if (BienBll.ChucVu.Trim() == "1")
            {
                var f = new WpfDonGia();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền vào mục này", "Thông báo", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void mnuLuong01_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfLuong01();
            f.ShowDialog();
        }

        private void mnuLuong02_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfLuong02();
            f.ShowDialog();
        }

        private void mnuDienbao_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfDienbao();
            f.ShowDialog();
        }

        private void mnuKHB_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfKhb();
            f.ShowDialog();
        }

        //End Ke Toan

        //Tin Dung
        private void mnuSkeKu_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfSkeKu();
            f.ShowDialog();
        }
        
        private void mnuSkeTo_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfSkeTo();
            f.ShowDialog();
        }
        private void mnuSkePLN_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfPLN();
            f.ShowDialog();
        }

        private void mnuSvRaTruong_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfSvRaTruong();
            f.ShowDialog();
        }

        private void mnu01TG_Click(object sender, RoutedEventArgs e)
        {
            var f = new Wpf01TG();
            f.ShowDialog();
        }

        private void mnuKTSDVV_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfKTSDVV();
            f.ShowDialog();
        }

        private void mnuDoiChieu_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfDoiChieu();
            f.ShowDialog();
        }

        private void mnuDKNOPLAI_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfDKNOPLAI();
            f.ShowDialog();
        }

        private void mnuDvut_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfDvut();
            f.ShowDialog();
        }

        private void mnuPhanTichSL_Click(object sender, RoutedEventArgs e)
        {
            //WpfPhanTichSL f = new WpfPhanTichSL();
            var f = new WpfQlyNguon();
            f.ShowDialog();
        }

        private void mnuPhanTichSL_CT_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfPhanTichSL_CT();
            f.ShowDialog();
        }

        private void mnuKhGnTn_Click(object sender, RoutedEventArgs e)
        {
            if (BienBll.Quyen.Trim() != "1")
            {
                // MessageBox.Show("Sorry,Bạn không có quyền vào mục này");
                MessageBox.Show("Sorry,Bạn không có quyền vào mục này", "Thông báo", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            else
            {
                var f = new WpfKhGnTn();
                f.ShowDialog();
            }
        }

        private void mnuInKhGnTn_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfInKhGnTn();
            f.ShowDialog();
        }

        private void mnuQtKhGnTn_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfQtKhGnTn();
            f.ShowDialog();
        }

        private void mnuLaiTon_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfLaiThang();
            f.ShowDialog();
        }

        private void mnuLaiThang_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfLaiTon();
            f.ShowDialog();
        }

        private void mnuDCPTNO_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfDCPTNO();
            f.ShowDialog();
        }

        private void mnuDinhSv_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfDinhSv();
            f.ShowDialog();
        }

        //End Tin Dung

        //Kiem Soat
        private void mnuKt740_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfKt740();
            f.ShowDialog();
        }
        private void mnuKt740New_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfKt740New();
            f.ShowDialog();
        }

        private void mnuDanhsach_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfDanhSach();
            f.ShowDialog();
        }

        private void mnuD15NHNN_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfD15NHNN();
            f.ShowDialog();
        }

        private void mnuKTNN_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfKTNN();
            f.ShowDialog();
        }

        // End kiem soat
  

        private void IMPVBSP_Loaded(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            //dt = cls.LoadDataText("select MAX(convert(date,left(GL_NGAYBC,10),105)) as NGAY from GL_VBSP");
            //dt1 = cls.LoadDataText("select MAX(convert(date,left(NG_CAPNHAT,10),105)) as NGAY from HSTO");
            var dt = cls.LoadDataText("select MAX(convert(date,ngay,105)) as NGAY from U_CANDOI");
            var dt1 =
                cls.LoadDataText(
                    "select MAX(convert(date,ngayku,105)) as NGAY,MAX(convert(date,ngaybt,105)) as NGAYBT from U_HSTD");
            txtUserName.Text = BienBll.NdTen.Trim() + "     " + BienBll.NdMadv.Trim() + "    " + BienBll.NdDiachi.Trim() +
                               "    " + "Ngân Hàng Chính Sách Xã Hội Tỉnh Quảng Bình      Số liệu CD :    "
                               + str.Left(dt.Rows[0]["NGAY"].ToString(), 10) + "  HSTDCT : " +
                               str.Left(dt1.Rows[0]["NGAY"].ToString(), 10);
            //+ "   HSTDCT Kết Hợp : " + str.Left(dt1.Rows[0]["NGAYBT"].ToString(), 10);

            //txtPos.Text = BienBll.NdMadv.ToString();
            //txtDc.Text = BienBll.NdDiachi;
            //txtDv.Text = "Ngân Hàng Chính Sách Xã Hội Tỉnh Quảng Bình " + "Số liệu ngày :" + dt.Rows[0]["NGAY"];
            //txtNgay.Text = "Số liệu ngày :" + dt.Rows[0]["NGAY"];
            cls.DongKetNoi();
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
            //
      
        }

        //Begiin Other
        public static Version GetPublishedVersion()
        {
            var xmlDoc = new XmlDocument();
            var asmCurrent = Assembly.GetExecutingAssembly();
            var executePath = new Uri(asmCurrent.GetName().CodeBase).LocalPath;

            xmlDoc.Load(executePath + ".manifest");
            var retval = string.Empty;
            if (xmlDoc.HasChildNodes)
            {
                retval = xmlDoc.ChildNodes[1].ChildNodes[0].Attributes.GetNamedItem("version").Value;
            }
            return new Version(retval);
        }

        private void mnuHelp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("Help\\VBSP_Helper.chm");
                // System.Windows.Forms.Help.ShowHelp(null, "VBSP_Helper.chm");
                //System.Windows.Forms.Help.ShowHelp(null,"VBSP_Helper.chm");
                // string appPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                // MessageBox.Show(appPath);
                // Process.Start(appPath + "\\VBSP_Helper.chm");
                //string appPath = System.IO.Path.GetFullPath(Process.GetCurrentProcess().MainModule.FileName);
                //MessageBox.Show(appPath);
                //System.Diagnostics.Process.Start(Application.StartupPath + \\Help Document.chm);

                //System.Windows.Forms.Help.ShowHelp(null, @"VBSP_Helper.chm");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnuAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ngân hàng CSXH Tỉnh Quảng Bình\r\nPhòng Tin Học");
        }

        private void mnuSupport_Click(object sender, RoutedEventArgs e)
        {
            //var f = new WpfTest();
            var f = new WpfNhatKy();
            f.ShowDialog();
        }

        private void mnuTest_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfTest();
            //WpfNhatKy f = new WpfNhatKy();
            f.ShowDialog();
        }

        //End other

        private void mnuDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if (BienBll.Quyen.Trim() == "1")
            {
                var f = new WpfDelete();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Sorry ! Is not for you. Reture now", "Mess", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void mnuDbf_OnClick(object sender, RoutedEventArgs e)
        {
            if (BienBll.Quyen.Trim() == "1")
            {
                var f = new WpfDbf();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Sorry ! Is not for you. Reture now", "Mess", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void mnuSkeNdh_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfSkeNdh();
            f.ShowDialog();
        }

        private void MnuQt14_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfQt14();
            f.ShowDialog();
        }

        private void MnuChamDiemGDX_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfChamDiemGDX();
            f.ShowDialog();
        }

        private void Mnukhtc06_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfKhtc06();
            f.ShowDialog();
        }

        private void MnuSkeKhoanh_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfSkeKhoanh();
            f.ShowDialog();
        }

        private void MnuUser_OnClick(object sender, RoutedEventArgs e)
        {
            if (BienBll.Quyen.Trim() == "1")
            {
                var f = new WpfUser();
                f.ShowDialog();
            }
            else
                MessageBox.Show("Sorry ! Is not for you. Reture now", "Mess", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
        }

        private void MnuQTCBTD_OnClick(object sender, RoutedEventArgs e)
        {
            if (BienBll.Quyen.Trim() == "1")
            {
                var f = new WpfCbtd();
                f.ShowDialog();
            }
            else
                MessageBox.Show("Sorry ! Is not for you. Reture now", "Mess", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
        }

        private void MnuCbtd_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfKhCbtd();
            f.ShowDialog();
        }

        private void MnuCbtdM_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfKhCbtdM();
            f.ShowDialog();
        }

        private void MnuXato_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfXaTo();
            f.ShowDialog();
        }

        private void MnuNhapNguon_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfNhapNguon();
            f.ShowDialog();
        }

        private void MnuNHNN_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfNHNN();
            f.ShowDialog();
        }

        private void MnuCanDoi_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfCanDoi();
            f.ShowDialog();
        }

        private void MnuTTKU_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfTTKU();
            f.ShowDialog();
        }

        private void MnuTTKH_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfTTKH();
            f.ShowDialog();
        }

        private void MnuChangePass_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfChangePass();
            f.ShowDialog();
        }

        private void MnuPnkt_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfPnkt();
            f.ShowDialog();
        }

        private void MnuHscb_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfHscb();
            f.ShowDialog();
        }

        private void mnuThiNV_OnClick(object sender, RoutedEventArgs e) //cai nay luyen thi
        {
            var f = new WpfLuyenThiNV();
            f.ShowDialog();
        }

        private void mnuThi_OnClick(object sender, RoutedEventArgs e) // cai nay thi chinh thuc
        {
            //WpfLuyenThiNV f = new WpfLuyenThiNV();
            //f.ShowDialog();
            MessageBox.Show("Menu này sẽ bổ sung sau !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void mnuNhapCH_OnClick(object sender, RoutedEventArgs e) // cai nay nhap cau hoi
        {
            if (BienBll.PhongBan.Trim() == "5") //Phòng tin học
            {
                var f = new WpfNhapCauHoi();
                f.ShowDialog();
            }
            else
                MessageBox.Show("Hãy mail nội dung chỉnh sửa hoặc đáp án về tutm.vbsp@gmail.com ", "Thông báo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void MnuSkePnkt_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfSkePnkt();
            f.ShowDialog();
        }

        private void MnuDongCasa_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfDongCasa();
            f.ShowDialog();
        }

        private void MnuChkUpOffline_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfChkUpOffline();
            f.ShowDialog();
        }

        private void mnuNOXH_Click(object sender, RoutedEventArgs e)
        {
            //var f = new WpfSkeKuCt();
            var f = new WpfNOXH();
            f.ShowDialog();
        }

        private void MnuPhanTichDaily_OnClick(object sender, RoutedEventArgs e)
        {
            //WpfSlDaily f= new WpfSlDaily();
            var f = new WpfDoanhSo();
            f.ShowDialog();
        }

        private void MnuSLVungBien_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfSlVungBien();
            f.ShowDialog();
        }

        private void MnuSkeHssv_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfSvSaoke();
            f.ShowDialog();
        }

        private void MnuPdf_OnClick(object sender, RoutedEventArgs e)
        {
            if (BienBll.Quyen.Trim() == "1")
            {
                var f = new WpfPdf();
                f.ShowDialog();
            }
            else
                MessageBox.Show("Sorry ! Is not for you. Reture now", "Mess", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
        }

        private void MnuUyThac_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfDvutMoi();
            f.ShowDialog();
        }

        private void MnuSkeNqh_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfSkeNqh();
            f.ShowDialog();
        }

        private void MnuGiaoNhanTV_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfGiaoNhanTV();
            f.ShowDialog();
        }

        private void MnuTienIch_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfTienIch();
            f.ShowDialog();
        }

        private void MnuNguonTT_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfNhapNguonTT();
            f.ShowDialog();
        }

        private void MnuNqh_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfNqh();
            f.ShowDialog();
        }

        private void MnuQlyGqvl_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfQlyGqvl();
            f.ShowDialog();
        }

        private void MnuCtCanDoi_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfCtCanDoi();
            f.ShowDialog();
        }

        private void MnuNdhCt_OnClick(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            var f = new WpfNdhCt();
            f.ShowDialog();
        }

        private void MnuNguonCqlv_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfNguonCqlv();
            f.ShowDialog();
        }

        private void MnuKTHDT_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfKTHDT();
            f.ShowDialog();
        }
        private void MnuKT3502_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfKt3502();
            f.ShowDialog();
        }

        private void MnuTbGhn_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfTbGhn();
            f.ShowDialog();
        }

        private void MnuNguonCqlvTinh_OnClick(object sender, RoutedEventArgs e)
        {
            /*
            if (BienBll.NdCapbc.Trim() == "2" && BienBll.PhongBan.Trim()=="3")
            {
                WpfNguonCqlvTinh f = new WpfNguonCqlvTinh();
                f.ShowDialog();
            }
            else MessageBox.Show("Bạn không có quyền vào mục này !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
             */
            var f = new WpfGqvlTInh();
            f.ShowDialog();
        }

        private void mnuTkXa_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfTkXa();
            f.ShowDialog();
        }

        private void mnuKHTN_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfKHTN();
            f.ShowDialog();
        }

        private void mnuChotNguon_Click(object sender, RoutedEventArgs e)
        {
            if (BienBll.Quyen.Trim() == "1")
            {
                var f = new WpfChotNguon();
                f.ShowDialog();
            }
            else
                MessageBox.Show("Sorry ! Is not for you. Reture now", "Mess", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
        }

        private void mnuTLGDXA_Click(object sender, RoutedEventArgs e)
        {
            if (BienBll.Quyen.Trim() == "1")
            {
                var f = new WpfTLGDXA();
                f.ShowDialog();
            }
            else
                MessageBox.Show("Sorry ! Is not for you. Reture now", "Mess", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
        }

        private void mnuTLNQH_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfTyLeNQH();
            f.ShowDialog();
        }

        private void mnuMau06_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfMau06();
            f.ShowDialog();
        }

        private void mnuXLN_M1_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfXLN_M1();
            f.ShowDialog();
        }

        private void mnuXLN_M3_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfXLN_M3();
            f.ShowDialog();
        }

        private void mnuXLN_M4_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfXLN_M4();
            f.ShowDialog();
        }

        private void mnuNVBQ_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfNVBQ();
            f.ShowDialog();
        }

        private void mnuNVKHB_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfNVKHB();
            f.ShowDialog();
        }

        private void mnuKHTC_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfKHTC();
            f.ShowDialog();
        }

        private void mnuKSDT_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfKhaoSat();
            f.ShowDialog();
        }

        private void MnuLuuPdf_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfCutFile();
            f.ShowDialog();
        }

        private void MnuNhapCHTN_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfCauHoi();
            f.ShowDialog();
        }

        private void MnuBaoCao_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfBaoCao();
            f.ShowDialog();
        }

        private void MnuThiTN_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfThiTN();
            f.ShowDialog();
        }

        private void MnuDanhGia_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfDanhGiaGV();
            f.ShowDialog();
        }

        private void mnuVpp_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfVpp();
            f.ShowDialog();
        }

        private void MnuBaoCaoVpp_Onclick(object sender, RoutedEventArgs e)
        {
            var f = new WpfVppBC();
            f.ShowDialog();
        }

        private void MnuDonGia_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfVppCapNhat();
            f.ShowDialog();
        }

        private void mnuTdThem_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("Chưa có chương trình !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            var f = new WpfThiDuaThem();
            f.ShowDialog();
        }
        private void mnuTdCapNhat_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("Chưa có chương trình !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            var f = new WpfThiDuaCapNhat();
            f.ShowDialog();
        }

        private void mnuTdChamDiem_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfThiDuaChamPGD();
            f.ShowDialog();
        }

        private void mnuTdTongHop_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Chưa có chương trình !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            var f = new WpfThiDuaTH();
            f.ShowDialog();
        }

        private void mnuTsccNhap_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Chưa có chương trình !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            var f = new WpfTSCCNhap();
            f.ShowDialog();
        }
        private void mnuTsccGDX_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Chưa có chương trình !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            var f = new WpfTSCCGDX();
            f.ShowDialog();
        }

        private void mnuTsccSaoKe_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Chưa có chương trình !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            var f = new WpfTSCC01();
            f.ShowDialog();
        }

        private void mnuTsccSuaChua_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Chưa có chương trình !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            var f = new WpfTSCC03();
            f.ShowDialog();
        }

        private void mnuTsccThanhLy_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Chưa có chương trình !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            var f = new WpfTSCCTLY();
            f.ShowDialog();
        }

        private void mnuTsccBaoTri_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Chưa có chương trình !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            var f = new WpfTSCCBaoTri();
            f.ShowDialog();
        }
        private void mnuTsccKiemKe_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Chưa có chương trình !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            var f = new WpfTSCCKiemKe();
            f.ShowDialog();
        }
        private void mnuTsccBc_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Chưa có chương trình !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            var f = new WpfTSCCBC();
            f.ShowDialog();
        }

        private void MnuKHTD_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfKHTD();
            f.ShowDialog();
        }

        private void MnuTTDSHN_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfChkDoiTuongTH();
            f.ShowDialog();
        }
        private void MnuTTDSHNEXCEL_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfChkDoiTuongThke();
            f.ShowDialog();
        }

        private void mnuTTTO_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfTTTO();
            f.ShowDialog();
        }

        private void MnuTTDSHNTH_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfChkDoiTuong();
            f.ShowDialog();
        }

        /// <summary>
        ///     BAK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuLuuBAK_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfLuuOffline();
            f.ShowDialog();
        }

        /// <summary>
        ///     Camera
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCamera_Click(object sender, RoutedEventArgs e)
        {
            var f= new WpfCamera();
            f.ShowDialog();
        }
        private void mnuCamera01_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfCamera01();
            f.ShowDialog();
        }

        private void mnuTuyenTruyen_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfTuyenTruyen();
            f.ShowDialog();
        }

        private void mnuKSV_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfKhaoSatVay();
            f.ShowDialog();
        }

        //End Kiem Soat
        private void mnuPL07_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfKhtc07();
            f.ShowDialog();
        }
        private void mnuPtsl_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfPtsl();
            f.ShowDialog();
        }
        private void mnuUpPhep_Click(object sender, RoutedEventArgs e)
        {
            if (BienBll.ChucVu.Trim() == "3" || BienBll.ChucVu.Trim() == "4")
            {
                var f = new WpfNhapPhep();
                f.ShowDialog();
                
            }
            else
                MessageBox.Show("Bạn không vào mục này !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void mnuDon_Click(object sender, RoutedEventArgs e)
        {
            //var f = new WpfChamCong();
            //f.ShowDialog();
            MessageBox.Show("Chưa thực hiện !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void mnuChamCong_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfChamCong();
            f.ShowDialog();
        }

        private void mnuChamCongChk_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfChamCongChk();
            f.ShowDialog();
        }

        private void mnuChamCongTK_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfChamCongTK();
            f.ShowDialog();
        }
        private void mnuChamCongSet_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfChamCongSet();
            f.ShowDialog();
        }

        private void mnuPhanLoaiKH_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfPhanLoaiKH();
            f.ShowDialog();
        }
        private void mnuChovayNangmuc_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfChovayNangmuc();
            f.ShowDialog();
        }
        private void mnuChovayNangmuc01_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfChovayNangmuc01();
            f.ShowDialog();
        }

        private void mnuTGTKTO_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfTGTKTO();
            f.ShowDialog();
        }
        private void mnuCLT_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfClt();
            f.ShowDialog();
        }
        private void mnuKTNB01_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfKTNB01();
            f.ShowDialog();
        }
        private void mnuKTNB02_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfKTNB02();
            f.ShowDialog();
        }
        private void mnuKTNB03_OnClick(object sender, RoutedEventArgs e)
        {
            var f = new WpfKTNB03();
            f.ShowDialog();
        }
        private void mnuKTNB_Set_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfKTNB_Set();
            f.ShowDialog();
        }

        private void mnuLeThuy_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfLeThuy();
            f.ShowDialog();
        }
        private void mnuRsGqvl_Click(object sender, RoutedEventArgs e)
        {
            var f = new WpfRsGqvl();
            f.ShowDialog();
        }
        public class NegatingConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is double)
                {
                    return -((double) value);
                }
                return value;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is double)
                {
                    return +(double) value;
                }
                return value;
            }
        }
    }
}