using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Data;
using DAL;
using BLL;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDelete.xaml
    /// </summary>
    public partial class WpfDelete : Window
    {
        public WpfDelete()
        {
            InitializeComponent();
        }
        readonly ClsServer _cls = new ClsServer();
        ToolBll bll = new ToolBll();
        DataTable _dt = new DataTable();
        //private BackgroundWorker _worker;
        
        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
               //_dt = _cls.LoadDataText("select * from HSKH");

                for (int i = 1; i <= dtpNgay.SelectedDate.Value.Month; i++)
                {
                    for (int j = 1; j < 31; j++)
                    {
                        _cls.ClsConnect();
                        //string ngay = bll.Right(dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"), 4) + "-" + i.ToString("00") + "-" + j.ToString("00") ;
                        //string sql = "delete from QT_HSTG where NGAYBC='"+ngay+"'";
                        //MessageBox.Show(sql);
                        //_cls.UpdateDataText(sql);
                        const int thamso = 1;
                        string[] bien = new string[thamso];
                        object[] giatri = new object[thamso];
                        bien[0] = "@Ngay";
                        if (dtpNgay.SelectedDate != null) giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                        //MessageBox.Show(giatri[0].ToString() + "   " + giatri[1].ToString());
                        _cls.LoadDataProcPara("usp_PSSL", bien, giatri, thamso);

                        _cls.DongKetNoi();
                    }
                }
                MessageBox.Show("OK", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _cls.DongKetNoi();
            }
            /*
            _worker = new BackgroundWorker { WorkerReportsProgress = true };
            //_worker.DoWork += (obj, ea)=>ProgressOk();
            _worker.DoWork += ProgressOk;//new DoWorkEventHandler(ProgressOk);
            _worker.ProgressChanged += ProgressReport;
            _worker.RunWorkerCompleted += WorkerComplete;
            _worker.RunWorkerAsync();
             */
        }
        private void ProgressOk(object sender, DoWorkEventArgs e)
        {
            
            _cls.ClsConnect();
            //_dt = _cls.LoadDataText("select * from HSKH");
            
            for (int i = 1; i <12; i++)
            {
                for (int j = 1; j < 27; j++)
                {
                    int perCent = (int) (j/(float) (j - 1)*100);
                    string mess = string.Format("Iteration {0} of {1}", j, j - 1);
                    //_worker.ReportProgress(perCent, mess);
                    Dispatcher.Invoke(new Action(() => LblPerCent.Content = perCent.ToString() + "%"));
                }
            }
            _cls.DongKetNoi();           
             
        }
        private void ProgressReport(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;
        }
        private void WorkerComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Progress Complete", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void WpfDelete_OnLoaded(object sender, RoutedEventArgs e)
        {
            dtpNgay.SelectedDate = DateTime.Parse(DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month).ToString());
        }


        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
