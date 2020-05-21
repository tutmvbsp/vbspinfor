using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using DAL;
using System.Diagnostics;
using BLL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfTest.xaml
    /// </summary>
    public partial class WpfTest
    {
        public WpfTest()
        {
            InitializeComponent();
        }

        readonly ClsServer _cls = new ClsServer();
        ClsOracle cls = new ClsOracle();
        DataTable _dt = new DataTable();
        ToolBll s = new ToolBll();
        private BackgroundWorker _worker;
        private void Btnclose_OnClick(object sender, RoutedEventArgs e)
        {
            if (dtpNgay.SelectedDate != null)
            {
                DateTime ngay = dtpNgay.SelectedDate.Value; //DateTime.Now; // hay ngày nào đó trong CSDL?
                bool cuoiThang = (ngay.Month != ngay.AddDays(1).Month);
                MessageBox.Show(cuoiThang ? "Cuoi thang" : "khong phai Cuoi thang");
            }
            Close();
        }

        private void BtnOK_OnClick(object sender, RoutedEventArgs e)
        {
            _worker = new BackgroundWorker {WorkerReportsProgress = true};
            //_worker.DoWork += (obj, ea)=>ProgressOk();
            _worker.DoWork += ProgressOk;//new DoWorkEventHandler(ProgressOk);
            //_worker.ProgressChanged+=ProgressReport;
            _worker.RunWorkerCompleted+=WorkerComplete;
            _worker.RunWorkerAsync();
        }
        private void BtnPdf_OnClick(object sender, RoutedEventArgs e)
        {
            String fileName = "C:\\TEXT\\So Tay Toan-Ly-Hoa Cap 2 (NXB Dai Hoc Quoc Gia 2010) - Duong Duc Kim, 335 Trang.pdf";
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = fileName;
            process.Start();
            process.WaitForExit();
        }
        private void WorkerComplete(object sender,RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Progress Complete","Mess",MessageBoxButton.OK,MessageBoxImage.Information);
            LblMess.Content = "";
        }
        private void ProgressReport(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;
        }

        #region Thuc hien khi nhan OK
        private void ProgressOk(object sender, DoWorkEventArgs e)
        {
           
            _cls.ClsConnect();
            Dispatcher.Invoke(new Action(() => LblMess.Content = "Đang lấy dữ liệu..."));
            _dt = _cls.LoadDataText("select * from HSKH");
            int sodong = _dt.Rows.Count;
            for (int i = 0; i < sodong; i++)
            {
                int perCent = (int)(i / (float)(sodong - 1) * 100);
                //int perCent = (i / (sodong - 1) * 100);
                //string mess = string.Format("Iteration {0} of {1}", i, sodong - 1);
                //int perCent = (i / sodong);
                string mess = string.Format("Iteration {0} of {1}", i, sodong - 1);
                _worker.ReportProgress(perCent,mess);
                //_worker.ReportProgress(perCent);
                Dispatcher.Invoke(new Action(() => LblPerCent.Content = perCent.ToString() + "%"));
            }
            _cls.DongKetNoi();
        }

        #endregion

        private void ComBoBox_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _cls.ClsConnect();
                _dt = _cls.LoadDataText("select * from DMPOS");
                dgvData.ItemsSource = _dt.DefaultView;
                CboBox.ItemsSource = _dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                _cls.DongKetNoi();
            }
        }


        private void RadioButton1_Checked(object sender, RoutedEventArgs e)
        {

            Process.Start("IExplore.exe", "http://nhcsxh.quangbinh.gov.vn");
        }

        private void RadioButton2_Checked(object sender, RoutedEventArgs e)
        {
            Process.Start("IExplore.exe", "http://www.vbsp.org.vn");
        }

        private void BtnOKOra_OnClick(object sender, RoutedEventArgs e)
        {
            ClsOracle cls = new ClsOracle();
            cls.ClsConnect();
            int thamso = 2;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "P_MAPGD";
            giatri[0] = CboPos.SelectedValue;
            bien[1] = "P_NGAYBC";
            //if (dtpNgay.SelectedDate != null) giatri[1] = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
            giatri[1] = dtpNgay.SelectedDate.Value;
            MessageBox.Show("POS : " + giatri[0] + "    ,   Ngay : " + giatri[1]);
            var dt = cls.LoadDataProcPara("uspQB_SaoKe", bien, giatri, thamso);
            dgvData.ItemsSource = dt.DefaultView;
            cls.DongKetNoi();
        }

        private void CboPos_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //cls.ClsConnect();
            ////string sql = "select PO_MA,PO_TEN from DMPOS where PO_MA=" + "'" + BienBll.NdMadv.Trim() + "'";
            //var sql = "select * from dmpos";
            //var dtpos = cls.LoadDataText(sql);
            //dgvData.ItemsSource = dtpos.DefaultView;
            //CboPos.ItemsSource = dtpos.DefaultView;
            //CboPos.DisplayMemberPath = "PO_TEN";
            //CboPos.SelectedValuePath = "PO_MA";
            ////for (int i = 0; i < dtpos.Rows.Count; i++)
            ////{
            ////    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
            ////}
            //cls.DongKetNoi();
        }

        private void btnLoaPos_Click(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            //string sql = "select PO_MA,PO_TEN from DMPOS where PO_MA=" + "'" + BienBll.NdMadv.Trim() + "'";
            var sql = "select * from dmpos";
            var dtpos = cls.LoadDataText(sql);
            CboPos.ItemsSource = dtpos.DefaultView;
            CboPos.DisplayMemberPath = "PO_TEN";// trung voi ben TOAD nhe
            CboPos.SelectedValuePath = "PO_MA";
            //for (int i = 0; i < dtpos.Rows.Count; i++)
            //{
            //    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
            //}
            CboPos.SelectedIndex = 4;
            cls.DongKetNoi();
            dtpNgay.SelectedDate=DateTime.Now.AddDays(-1);
        }
    }
}
