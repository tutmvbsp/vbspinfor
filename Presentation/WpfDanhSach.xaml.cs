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
using DAL;
using BLL;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDanhSach.xaml
    /// </summary>
    public partial class WpfDanhSach : Window
    {
        public WpfDanhSach()
        {
            InitializeComponent();          
        }
        ToolBll bll = new ToolBll();
        ServerInfor srv = new ServerInfor();
        ClsServer cls = new ClsServer();
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    

        private void btnChapNhan_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            cls.ClsConnect();
            int thamso = 4;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@ngay";
            giatri[0] = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");  
            bien[1] = "@MaXa";
            giatri[1] = bll.Left(cboXa.SelectedValue.ToString(),6);
            bien[2] = "@StartDate";
            giatri[2] = dtpStartDate.SelectedDate.Value.ToString("dd/MM/yyyy");
            bien[3] = "@EndDate";
            giatri[3] = dtpEndDate.SelectedDate.Value.ToString("dd/MM/yyyy");
            MessageBox.Show(giatri[0].ToString() + giatri[1].ToString() + giatri[2].ToString()+giatri[3].ToString());
            if (opt01.IsChecked.Value==true)
            {
                dt = cls.LoadDataProcPara("usp_DanhSach01", bien, giatri, thamso);
            }
            else
            {
                dt = cls.LoadDataProcPara("usp_DanhSach19", bien, giatri, thamso);
            }
            int dem = dt.Rows.Count;
            if (dem<=0)
            {
                MessageBox.Show("Không có khách hàng nào");
            }
            else
            {
                rpt_DanhSach rpt = new rpt_DanhSach();
                RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
            }
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
            try
            {
                cls.ClsConnect();
                DataTable dtpos = new DataTable();
                string sql = "select PO_MA,PO_TEN from DMPOS where PO_MACN=" + "'" + BienBll.MainPos + "'" + " order by PO_MA";
                dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++){
                    
                    cboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
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
                DataTable dtxa = new DataTable();
                string sql = "select MA,TEN from DMXA where PGD_QL= " + "'" + bll.Left(cboPos.SelectedValue.ToString().Trim(), 6) + "'" + " order by MA";
                dtxa = cls.LoadDataText(sql);
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
     

    }
}
