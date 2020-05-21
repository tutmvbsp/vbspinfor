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
using DAL;
using BLL;
using System.Data;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfLaiTon.xaml
    /// </summary>
    public partial class Wpf01TG : Window
    {
        public Wpf01TG()
        {
            InitializeComponent();
        }

        ClsServer cls = new ClsServer();
        ToolBll bll = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        DataTable dtNew= new DataTable();
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
            try
            {
                cls.ClsConnect();
                DataTable dtpos = new DataTable();
                DataTable dtng = new DataTable();
                dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
                string sql = "select PO_MA,PO_TEN from DMPOS order by PO_MA";
                dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                CboPos.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();

        }

        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CboXa.Items.Clear();
                cls.ClsConnect();
                DataTable dtxa = new DataTable();
                string sql = "select MA,TEN from DMXA where PGD_QL= " + "'" +
                             bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "'" + " order by MA";
                dtxa = cls.LoadDataText(sql);
                for (int i = 0; i < dtxa.Rows.Count; i++)
                {
                    CboXa.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                }
                CboXa.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }

        }

        private void CboXa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CboTo.Items.Clear();
                cls.ClsConnect();
                DataTable dtto = new DataTable();
                string sql = "select TO_MATO,TO_TENTT from HSTO where Left(TO_MADP,6) = " + bll.Left(CboXa.SelectedValue.ToString().Trim(), 6) ;
                dtto = cls.LoadDataText(sql);
                for (int i = 0; i < dtto.Rows.Count; i++)
                {
                    CboTo.Items.Add(dtto.Rows[i][0] + " | " + dtto.Rows[i][1]);
                }
                CboTo.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {

            dtNew = dt.Clone();
            foreach (DataRow dr in dt.Rows)
            {
                if ((bool)dr[0] == true)
                {
                    dtNew.ImportRow(dr);
                }
            }
            if (dtNew==null || dtNew.Rows.Count==0)
            {
                MessageBox.Show("Chưa chọn khách hàng nào ", "Mess");
            }
            else
            {
               // dgvTarGet.ItemsSource = dtNew.DefaultView;
                rpt_01TG rpt = new rpt_01TG();
                RPUtility.ShowRp(rpt, dtNew, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
            }
           // dtNew.RejectChanges();
           // dtNew = null;
           
        }


        private void LoadGrid()
        {
            try
            {

                cls.ClsConnect();
                int thamso = 2;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Mato";
                giatri[0] = bll.Left(CboTo.SelectedValue.ToString().Trim(), 7);
                bien[1] = "@Ngay";
                if (dtpNgay.SelectedDate != null)
                    giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                else
                {
                    MessageBox.Show("Chưa chọn ngày ", "Mess");
                    return;
                }
                dt = cls.LoadDataProcPara("usp_01TG", bien, giatri, thamso);
                //rpt_kt740_01 rpt = new rpt_kt740_01();
                if (dt.Rows.Count > 0)
                {
                    dgvSource.ItemsSource = dt.DefaultView;
                    // rpt_SkeTo rpt = new rpt_SkeTo();
                    // RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                    // string filename = "C:\\Tam\\" + str.Left(cboTo.SelectedValue.ToString().Trim(), 7) + ".xlsx";
                    // bll.WriteDataTableToExcel(dt, "Person Details", filename, "Details");
                    //dtNew = dt.GetChanges();
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
        private void LoadGridMoi()
        {
            try
            {

                cls.ClsConnect();
                int thamso = 2;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Mato";
                giatri[0] = bll.Left(CboTo.SelectedValue.ToString().Trim(), 7);
                bien[1] = "@Ngay";
                if (dtpNgay.SelectedDate != null)
                    giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                else
                {
                    MessageBox.Show("Chưa chọn ngày ", "Mess");
                    return;
                }
                dt = cls.LoadDataProcPara("usp_01TGMoi", bien, giatri, thamso);
                //rpt_kt740_01 rpt = new rpt_kt740_01();
                if (dt.Rows.Count > 0)
                {
                    dgvSource.ItemsSource = dt.DefaultView;
                    // rpt_SkeTo rpt = new rpt_SkeTo();
                    // RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                    // string filename = "C:\\Tam\\" + str.Left(cboTo.SelectedValue.ToString().Trim(), 7) + ".xlsx";
                    // bll.WriteDataTableToExcel(dt, "Person Details", filename, "Details");
                    //dtNew = dt.GetChanges();
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

        private void CboTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Ration1.IsChecked==true) LoadGrid();
            else LoadGridMoi();
           
        }

        private void btnKt_Click(object sender, RoutedEventArgs e)
        {
            //dtNew.RejectChanges();
            //dtNew.Clear();
            //dgvTarGet.ItemsSource = null;
            dtNew = dt.Clone();
            foreach (DataRow dr in dt.Rows)
            {
                //MessageBox.Show(dr[0].ToString() + "  " + dr[1].ToString());
                if ((bool)dr[0] == true)
                {
                    dtNew.ImportRow(dr);
                }
            }
           // dgvTarGet.ItemsSource = dtNew.DefaultView;
        }

        private void ChkAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach (DataRow dr in dt.Rows)
                {
                    //MessageBox.Show(dr[0].ToString() + "  " + dr[1].ToString());
                    //if ((bool) dr[0] == false)
                    //{
                        dr[0] = true;
                    //}
                }
        }

        private void ChkAll_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (DataRow dr in dt.Rows)
            {
                ////MessageBox.Show(dr[0].ToString() + "  " + dr[1].ToString());
                //if ((bool) dr[0] == false)
                //{
                dr[0] = false;
                //}
            }

        }
    }
    
}
