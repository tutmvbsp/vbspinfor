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
    public partial class WpfThiTN : Window
    {
        ClsServer cls = new ClsServer();
        ServerInfor srv = new ServerInfor();
        ToolBll str = new ToolBll();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        Stopwatch stopWatch = new Stopwatch();
        string currentTime = string.Empty;

        string Thumuc = "C:\\KT740";
        public WpfThiTN()
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
      
   
        private string TT = "";

        private int datraloi = 0;
        private string ng = "";
        List<string> lst = new List<string>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtpNgay.SelectedDate = DateTime.Now;
        }


        private void lblCauHoi_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dtpNgay.SelectedDate != null) ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                reset_clock();
                string sql = "";
                str.TaoThuMuc(Thumuc);
                cls.ClsConnect();
                var dtchk = cls.LoadDataText("select * from KQTHINV  where USERNAME='" + BienBll.Ndma.Trim() + "' and NGAY='" + ng + "'");
                if (dtchk.Rows.Count == 0)
                {
                    sql = "insert into KQTHINV select LOAI, CAUHOI, A, B, C, D, DAPAN, TT, POS,'" +
                                         BienBll.NdTen.Trim() + "'  NG_NHAP,0 VONG,'" + BienBll.Ndma.Trim() +
                                         "' USERNAME, TRALOI,CANCU,NGAY from CAUHOI where NGAY='" + ng + "'";
                    cls.LoadDataText(sql);
                }
                dt = cls.LoadDataText("select * from KQTHINV  where USERNAME='" + BienBll.Ndma.Trim() + "' and NGAY='" + ng + "' and TRALOI is null");
                if (dt.Rows.Count > 0)
                {
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
                    
                    StreamReader srl = new StreamReader(filename);
                    while (srl.Peek() >= 0)
                    {
                        lst.Add(srl.ReadLine());
                        Sodong = Sodong + 1;
                    }
                    cls.DongKetNoi();
                    lblCauHoi.IsEnabled = false;
                    star_clock();
                }
                else MessageBox.Show("Bạn đã làm xong bài thi của mình rồi !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
    
        }


        private void lblNext_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Countdown(thoigian, TimeSpan.FromSeconds(1), cur => tb.Text = cur.ToString()); //tutm
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
                if (dtpNgay.SelectedDate != null) ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
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
                    //if (dapan.Trim() == chon.Trim())
                    //    //MessageBox.Show("Đúng rồi ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    //    PlaySound("Sound\\Dung.mp3");
                    //else
                    //{
                    //    PlaySound("Sound\\Sai.mp3");
                    //    MessageBox.Show("Sai rồi, Đáp án là : " + dapan.Trim()+": Căn cứ "+cancu.Trim(), "Thông báo", MessageBoxButton.OK,
                    //        MessageBoxImage.Warning);
                    //    socausai = socausai + 1;
                    //}
                    datraloi = datraloi + 1;
                    string strstr = "update KQTHINV set TRALOI ='" + chon.Trim() + "' where USERNAME='" +
                                    BienBll.Ndma.Trim() + "' and NGAY = '" + ng + "' and TT=" + TT;
                    cls.ClsConnect();
                    cls.UpdateDataText(strstr);
                    cls.DongKetNoi();
                    lblNext_MouseDown(null, null);
                }
                else
                {
                    //dtsocau =cls.LoadDataText("select count(case when DAPAN=TRALOI then DAPAN end) SVDUNG,count(DAPAN) VONG from KQTHINV where USERNAME='" + BienBll.Ndma.Trim() + "' and VONG='"+vong+"' and LOAI='"+ CboChuDe.SelectionBoxItem.ToString().Trim() + "'"); //tutm
                    //if (dtsocau.Rows.Count > 0) socau = dtsocau.Rows[0]["DEM"].ToString();
                    MessageBox.Show("Bạn đã làm câu cuối cùng !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
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
     

      
        private void lblEnd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            stop_clock();
            lblCauHoi.IsEnabled = true;
            cls.ClsConnect();
           // var dtkq =cls.LoadDataText("select count(*) DEM from KQTHINV where USERNAME='" + BienBll.Ndma.Trim() + "' and VONG = '" + vong +"' and DAPAN=TRALOI and LOAI='"+ CboChuDe.SelectionBoxItem.ToString().Trim() + "'");
           // MessageBox.Show("Bạn đã kết thúc, Số câu đúng : "+ dtkq.Rows[0]["DEM"]+" /  "+socau, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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
