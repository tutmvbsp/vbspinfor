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
using System.Data.SqlClient;
using BLL;
using DAL;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfKhb.xaml
    /// </summary>
    public partial class WpfKhb : Window
    {
        public WpfKhb()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ServerInfor srv = new ServerInfor();
        ToolBll bll = new ToolBll();
        DataTable dt4 = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        string Thumuc = "C:\\Saoke";
        private string FileName = "";
        private void bntClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void bntOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bll.TaoThuMuc(Thumuc);
                cls.ClsConnect();
                int thamso = 2;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Ngay";
                if (dtpNgay.SelectedDate != null)
                {
                    giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    bien[1] = "@MaPos";
                    giatri[1] = bll.Left(cboPos.SelectedValue.ToString().Trim(), 6);
                    //MessageBox.Show(giatri[0] + "   " + giatri[1]);
                    //dt = cls.LoadDataProcPara("usp_KHB", bien, giatri, thamso);
                    dt1 = cls.LoadDataProcPara("usp_khb1", bien, giatri, thamso);
                    //dt2 = cls.LoadDataProcPara("usp_khb2", bien, giatri, thamso);
                    rpt_khb1 rpt1 = new rpt_khb1();
                    RPUtility.ShowRp(rpt1, dt1, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                    //rpt_khb2 rpt2 = new rpt_khb2();
                    //RPUtility.ShowRp(rpt2, dt2, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                    dt3 = cls.LoadDataProcPara("usp_khb3", bien, giatri, thamso);
                    rpt_khb3 rpt3 = new rpt_khb3();
                    RPUtility.ShowRp(rpt3, dt3, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());

                    dt4 = cls.LoadDataProcPara("usp_KHB4", bien, giatri, thamso);
                    rpt_khb4 rpt4 = new rpt_khb4();
                    RPUtility.ShowRp(rpt4, dt4, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());

                    // FileName = Thumuc + "\\KHB03_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";

                }
               // bll.WriteDataTableToExcel(dt2, "Person Details", FileName, "Details");
               // MessageBox.Show("Copy Excel to : " + FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                cls.DongKetNoi();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

           // var now = BienBll.EndOfYearBefor.AddMonths(DateTime.Now.Month-2);//DateTime.Now.AddMonths(-1);
            //dtpNgayTr.SelectedDate = BienBll.EndOfYearBefor.AddMonths(now.Month);
            //dtpNgay.SelectedDate = dtpNgayTr.SelectedDate.Value.AddMonths(1);
            dtpNgay.SelectedDate = DateTime.Parse(DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month).ToString());
            try
            {
                cls.ClsConnect();
                DataTable dtpos = new DataTable();
                string sql = "select PO_MA,PO_TEN from DMPOS where PO_MACN=" + "'" + BienBll.MainPos + "'" + " order by PO_MA";
                dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    cboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                cboPos.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();

        }

        private void LblManual_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (BienBll.Ndma == "TUTM0001")
            {
                WpfAdd_KHB f = new WpfAdd_KHB();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Sorry,Task is not for you !", "Mess",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
        }
    }
}
