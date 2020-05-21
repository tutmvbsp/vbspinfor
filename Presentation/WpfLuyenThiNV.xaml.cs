using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Windows.Threading;
using BLL;
using DAL;
using System.Diagnostics;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for Wpf_THONGBAO_DONG105.xaml
    /// </summary>
    public partial class WpfLuyenThiNV : Window
    {
        ClsServer cls = new ClsServer();
        ServerInfor srv = new ServerInfor();
        ToolBll str = new ToolBll();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        Stopwatch stopWatch = new Stopwatch();
        string currentTime = string.Empty;

        string Thumuc = "C:\\KT740";
        public WpfLuyenThiNV()
        {
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler(dt_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }

        //private FileStream _fw;
        DataTable dt =new DataTable();
        DataTable dtvong = new DataTable();
        DataTable dtsocau = new DataTable();
        private string filename = "C:\\KT740\\CAUHOI.TXT";
        private int iRows = 0;
        private int Sodong = 0;
        private string chon = "";
        private int DongHienTai = 1;
        private string dapan = "";
        private string cancu = "";
        private string vong ="";
        private string socau = "";
        private int thoigian = 5;
        private string TT = "";
        private int socausai = 0;
        private int datraloi = 0;
        //private int dem = 0;
        List<string> lst = new List<string>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    cls.ClsConnect();
            //    dtvong = cls.LoadDataText("select max(VONG) VONG,count(*) DEM from KQTHINV where USERNAME='" + BienBll.Ndma.Trim() + "' and LOAI='"+ CboChuDe.SelectionBoxItem.ToString().Trim() + "'");
            //    if (dtvong.Rows[0]["DEM"].ToString() == "0")
            //    {
            //        vong = "0";
            //        socau = "0";
            //    }
            //    else
            //    {
            //        vong = dtvong.Rows[0]["VONG"].ToString();
            //        if (CboChuDe.SelectionBoxItem.ToString().Trim() == "AL")
            //            dtsocau =cls.LoadDataText(
            //                "select cast(max(VONG) as int) VONG,count(*) DEM from KQTHINV where USERNAME='" +
            //                BienBll.Ndma.Trim() + "' and TRALOI is null ");
            //        else
            //            dtsocau = cls.LoadDataText(
            //                "select cast(max(VONG) as int) VONG,count(*) DEM from KQTHINV where USERNAME='" +
            //                BienBll.Ndma.Trim() + "' and TRALOI is null and LOAI='" + CboChuDe.SelectionBoxItem.ToString().Trim() + "'");

            //        if (dtsocau.Rows.Count > 0) socau = dtsocau.Rows[0]["DEM"].ToString();
            //        txtVong.Text = BienBll.NdTen + " Đang Thi Vòng Số : " + vong + " Số Câu Chưa Trả Lời : " + socau;
            //    }
            //    //MessageBox.Show(vong + "  " + socau);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }


        private void lblCauHoi_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                reset_clock();
                string sql = "";
                str.TaoThuMuc(Thumuc);
                cls.ClsConnect();

                dtvong = cls.LoadDataText("select max(VONG) VONG,count(*) DEM from KQTHINV where USERNAME='" + BienBll.Ndma.Trim() + "' and LOAI='" + CboChuDe.SelectionBoxItem.ToString().Trim() + "'");
                if (dtvong.Rows[0]["DEM"].ToString() == "0")
                {
                    vong = "0";
                    socau = "0";
                }
                else
                {
                    vong = dtvong.Rows[0]["VONG"].ToString();
                    if (CboChuDe.SelectionBoxItem.ToString().Trim() == "AL")
                        dtsocau = cls.LoadDataText(
                            "select cast(max(VONG) as int) VONG,count(*) DEM from KQTHINV where USERNAME='" +
                            BienBll.Ndma.Trim() + "' and VONG='"+vong+"' and TRALOI is null ");
                    else
                        dtsocau = cls.LoadDataText(
                            "select cast(max(VONG) as int) VONG,count(*) DEM from KQTHINV where USERNAME='" +
                            BienBll.Ndma.Trim() + "' and VONG='"+vong+"' and TRALOI is null and LOAI='" + CboChuDe.SelectionBoxItem.ToString().Trim() + "'");

                    if (dtsocau.Rows.Count > 0) socau = dtsocau.Rows[0]["DEM"].ToString();
                    txtVong.Text = BienBll.NdTen + " Đang Thi Vòng Số : " + vong + " Số Câu Chưa Trả Lời : " + socau;
                }

                if (vong=="0" & socau == "0")
                    {
                        if (CboChuDe.SelectionBoxItem.ToString().Trim()=="AL")
                        sql = "insert into KQTHINV select 'AL' LOAI, CAUHOI, A, B, C, D, DAPAN, TT, POS,'" +
                              BienBll.NdTen.Trim() +
                              "'  NG_NHAP,1 VONG,'" + BienBll.Ndma.Trim() + "' USERNAME, TRALOI,CANCU from CAUHOI order by TT";
                        else
                        sql = "insert into KQTHINV select LOAI, CAUHOI, A, B, C, D, DAPAN, TT, POS,'" +
                              BienBll.NdTen.Trim() +
                              "'  NG_NHAP,1 VONG,'" + BienBll.Ndma.Trim() + "' USERNAME, TRALOI,CANCU from CAUHOI where LOAI='"+ CboChuDe.SelectionBoxItem.ToString().Trim() + "' order by TT";

                    cls.LoadDataText(sql);
                    }
                else
                    {
                        if (socau == "0") //lam het cau tao vong moi
                            {
                                int i = 0;
                                string s = vong;
                                i = int.Parse(s);
                                i = Convert.ToInt32(s) + 1;
                                if (CboChuDe.SelectionBoxItem.ToString().Trim() == "AL")
                                    sql = "insert into KQTHINV select 'AL' LOAI, CAUHOI, A, B, C, D, DAPAN, TT, POS,'" +
                                              BienBll.NdTen.Trim() +
                                              "'  NG_NHAP," + i + " VONG,'" + BienBll.Ndma.Trim() +
                                              "' USERNAME, TRALOI,CANCU from CAUHOI order by TT";
                                else
                                    sql = "insert into KQTHINV select LOAI, CAUHOI, A, B, C, D, DAPAN, TT, POS,'" +
                                          BienBll.NdTen.Trim() +
                                          "'  NG_NHAP," + i + " VONG,'" + BienBll.Ndma.Trim() + "' USERNAME, TRALOI,CANCU from CAUHOI where LOAI='" + CboChuDe.SelectionBoxItem.ToString().Trim() + "' order by TT";
                             cls.LoadDataText(sql);
                            }
                    }
                //var dtsocau = cls.LoadDataText("select count(*) DEM from KQTHINV where USERNAME='" + BienBll.Ndma.Trim() + "' and TRALOI is null");
                var dtsl =cls.LoadDataText("select max(VONG) VONG,count(*) DEM from KQTHINV where USERNAME='" +BienBll.Ndma.Trim() + "' and TRALOI is null and LOAI='"+ CboChuDe.SelectionBoxItem.ToString().Trim() + "'");
                if (dtsl.Rows.Count > 0)
                {
                    vong = dtsl.Rows[0]["VONG" +
                                        ""].ToString();
                    socau = dtsl.Rows[0]["DEM"].ToString();
                    txtVong.Text = BienBll.NdTen + " Đang Thi Vòng Số : " + vong + " Số Câu Chưa Trả Lời : " + socau;
                    var dtsai =
                        cls.LoadDataText("select COUNT(*) dem from KQTHINV where VONG=" + vong + " and USERNAME='" +
                                         BienBll.Ndma.Trim() + "' and DAPAN<>TRALOI and LOAI='"+ CboChuDe.SelectionBoxItem.ToString().Trim() + "'");
                    socausai=(int)dtsai.Rows[0]["DEM"];
                    lblSai.Content = socausai.ToString();
                    lblCon.Content = datraloi.ToString();
                }
                if (CboChuDe.SelectionBoxItem.ToString().Trim() == "AL")
                    dt = cls.LoadDataText("select * from KQTHINV  where USERNAME='" + BienBll.Ndma.Trim() + "' and TRALOI is null");
                else
                    dt = cls.LoadDataText("select * from KQTHINV  where USERNAME='" + BienBll.Ndma.Trim() + "' and LOAI='"+ CboChuDe.SelectionBoxItem + "' and TRALOI is null");
                str.WriteText(dt, filename);
                StreamReader sr = new StreamReader(filename);
                string readLine = sr.ReadLine();
                if (readLine != null)
                {
                    string[] arrStr = readLine.Split('#');
                    txtCauHoi.Text = arrStr[1];
                    txtA.Text = arrStr[2];
                    txtB.Text = arrStr[3];
                    txtC.Text = arrStr[4];
                    txtD.Text = arrStr[5];
                    dapan = arrStr[6];
                    TT = arrStr[7];
                    cancu = arrStr[13];
                }
                //MessageBox.Show(sql, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                StreamReader srl = new StreamReader(filename);
                while (srl.Peek() >= 0)
                {
                    lst.Add(srl.ReadLine());
                    Sodong = Sodong + 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
            lblCauHoi.IsEnabled = false;
            lblOk.IsEnabled = false;
            Countdown(thoigian, TimeSpan.FromSeconds(1), cur => tb.Text = cur.ToString()); //tutm
            star_clock();
        }


        private void lblNext_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Countdown(thoigian, TimeSpan.FromSeconds(1), cur => tb.Text = cur.ToString()); //tutm
            Ration1.IsChecked = false;
            Ration2.IsChecked = false;
            Ration3.IsChecked = false;
            Ration4.IsChecked = false;
            try
            {
                iRows += DongHienTai;
                GetData(iRows);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void lblPre_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Ration1.IsChecked = false;
            //Ration2.IsChecked = false;
            //Ration3.IsChecked = false;
            //Ration4.IsChecked = false;
            try
            {
                iRows -= DongHienTai;
                GetData(iRows);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lblOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //StartTimer();
                //Countdown(15, TimeSpan.FromSeconds(1), cur => tb.Text = cur.ToString()); //tutm
                if (iRows < lst.Count)
                {
                    if (Ration1.IsChecked == true)
                        chon = "A";
                    else if (Ration2.IsChecked == true)
                        chon = "B";
                    if (Ration3.IsChecked == true)
                        chon = "C";
                    if (Ration4.IsChecked == true)
                        chon = "D";
                    if (dapan.Trim() == chon.Trim())
                        //MessageBox.Show("Đúng rồi ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        PlaySound("Sound\\Dung.mp3");
                    else
                    {
                        PlaySound("Sound\\Sai.mp3");
                        MessageBox.Show("Sai rồi, Đáp án là : " + dapan.Trim()+": Căn cứ "+cancu.Trim(), "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        socausai = socausai + 1;
                        lblSai.Content = socausai.ToString();
                    }
                    datraloi = datraloi + 1;
                    lblCon.Content = datraloi.ToString();
                    string strstr = "update KQTHINV set TRALOI ='" + chon.Trim() + "' where USERNAME='" +
                                    BienBll.Ndma.Trim() + "' and VONG = " + vong + " and TT=" + TT;
                    cls.ClsConnect();
                    cls.UpdateDataText(strstr);
                    cls.DongKetNoi();
                    lblNext_MouseDown(null, null);
                }
                else
                {
                    dtsocau =cls.LoadDataText("select count(case when DAPAN=TRALOI then DAPAN end) SVDUNG,count(DAPAN) VONG from KQTHINV where USERNAME='" + BienBll.Ndma.Trim() + "' and VONG='"+vong+"' and LOAI='"+ CboChuDe.SelectionBoxItem.ToString().Trim() + "'"); //tutm
                    if (dtsocau.Rows.Count > 0) socau = dtsocau.Rows[0]["DEM"].ToString();
                    MessageBox.Show("Bạn đã làm câu cuối cùng ! Kết Quả : "+ dtsocau.Rows[0]["SVDUNG"].ToString()+"/"+dtsocau.Rows[0]["VONG"].ToString(), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    stop_clock();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void lblReset_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                cls.UpdateDataText("delete from KQTHINV where USERNAME='" + BienBll.Ndma.Trim() + "'");
                MessageBox.Show("Reset OK " , "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }

        private void lblThKe_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                var dtthke = cls.LoadDataText("select b.ND_MADV N'Mã POS',c.PO_TEN N'Tên POS',a.USERNAME,b.ND_TEN N'Tên CB',a.VONG N'Số vòng thi'"
                            +", COUNT(TT) N'Số câu hỏi', count( case when a.TRALOI is not null then a.TRALOI end) N'Số câu trả lời'"
                            +", count(case when a.TRALOI = a.DAPAN then a.DAPAN end) N'Số câu đúng', N'Câu đúng chỉ mang tính chất tham khảo' N'Ghi chú' "
                            +" from KQTHINV a, NG_DUNG b, DMPOS c where a.USERNAME = b.ND_MA and b.ND_MADV = c.PO_MA "
                            +"group by a.USERNAME, a.VONG, b.ND_MADV, ND_TEN, c.PO_TEN order by b.ND_MADV, a.USERNAME, a.VONG");
                string _path = Thumuc + "\\"+"thongke.csv";
                str.ExportToExcel(dtthke, _path);
                MessageBox.Show("Copy Excel to : " + _path, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                str.OpenExcel(_path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lổi : " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }
        private void lblEnd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            stop_clock();
            lblCauHoi.IsEnabled = true;
            cls.ClsConnect();
            var dtkq =cls.LoadDataText("select count(*) DEM from KQTHINV where USERNAME='" + BienBll.Ndma.Trim() + "' and VONG = '" + vong +"' and DAPAN=TRALOI and LOAI='"+ CboChuDe.SelectionBoxItem.ToString().Trim() + "'");
            MessageBox.Show("Bạn đã kết thúc, Số câu đúng : "+ dtkq.Rows[0]["DEM"]+" /  "+socau, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            cls.DongKetNoi();
        }





        private void GetData(int rows)
        {
            try
            {
                if (rows >= lst.Count)
                {
                    rows = iRows= lst.Count;
                } else if (rows <= 0)
                {
                    rows = iRows= 0;
                }
                if (rows < lst.Count)
                {
                    //txtCauHoi.Text = lst[Rows].ToString();
                    string[] arrStr = lst[rows].Split('#');
                    //MessageBox.Show(arrStr[0].ToString());
                    txtCauHoi.Text = arrStr[1];
                    txtA.Text =arrStr[2];
                    txtB.Text =arrStr[3];
                    txtC.Text =arrStr[4];
                    txtD.Text =arrStr[5];
                    dapan = arrStr[6];
                    TT = arrStr[7];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PlaySound(string path)
        {
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = path;
            wplayer.controls.play();
        }

    

        private void Ration1_Checked(object sender, RoutedEventArgs e)
        {
            lblOk_MouseDown(null, null);
        }

        private void Ration3_Checked(object sender, RoutedEventArgs e)
        {
            lblOk_MouseDown(null, null);
        }

        private void Ration4_Checked(object sender, RoutedEventArgs e)
        {
            lblOk_MouseDown(null, null);
        }

        private void Ration2_Checked(object sender, RoutedEventArgs e)
        {
            lblOk_MouseDown(null, null);
        }
        /*
        private DispatcherTimer _timer;
        public void StartTimer()
        {
            if (_timer == null)
            {
                _timer = new DispatcherTimer();
                _timer.Tick += _timer_Tick;
            }

            _timer.Interval = TimeSpan.FromSeconds(2);
            _timer.Start();
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            MessageBox.Show("Hi there");
            _timer.Stop();
        }

        //void SelectionChangedEvent()
        //{
        //    StartTimer();
        //}
        */
        void Countdown(int count, TimeSpan interval, Action<int> ts)
        {
            var dtime = new DispatcherTimer();
            dtime.Stop();
            tb.Text = "";
            dtime.Interval = interval;
            dtime.Tick += (_, a) =>
            {
                if (count-- == 0)
                    dtime.Stop();
                else
                    ts(count);
            };
            ts(count);
            dtime.Start();
        }
        void dt_Tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                clocktxt.Text = currentTime;
            }
        }

        private void star_clock()
        {
            stopWatch.Start();
            dispatcherTimer.Start();

        }

        private void stop_clock()
        {
            if (stopWatch.IsRunning)
            {
                stopWatch.Stop();
            }
            //elapsedtimeitem.Items.Add(currentTime);
        }

        private void reset_clock()
        {
            stopWatch.Reset();
            clocktxt.Text = "00:00:00";
        }
    }
}
