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
    /// Interaction logic for WpfDienbao.xaml
    /// </summary>
    public partial class WpfDonGia : Window
    {
        public WpfDonGia()
        {
            InitializeComponent();
        }

        private ClsServer cls = new ClsServer();
        private ServerInfor srv = new ServerInfor();
        private ToolBll bll = new ToolBll();
        DataTable dt= new DataTable();
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                const int thamso = 2;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Ngay";
                if (dtpNgay.SelectedDate != null) giatri[0] = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
                bien[1] = "@MaPos";
                giatri[1] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                //MessageBox.Show(giatri[0].ToString() + "   " + giatri[1].ToString());
                dt = cls.LoadDataProcPara("usp_DonGia", bien, giatri, thamso);
                //MessageBox.Show(BienBll.NdMadv+"  "+BienBll.MainPos);
                if (BienBll.NdMadv==BienBll.MainPos )
                {
                     rpt_DonGia rpt = new rpt_DonGia();
                     RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                }
                else
                {
                    rpt_DonGiaHuyen rpt = new rpt_DonGiaHuyen();
                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                }
                cls.DongKetNoi();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lổi, liên hệ phòng tin học" + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                    
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
            dtpNgay.SelectedDate = DateTime.Parse("31/12/" + DateTime.Now.AddYears(-1).ToString("yyyy"));
            try
            {
                cls.ClsConnect();
                string sql = "";
                DataTable dtpos = new DataTable();
                if (BienBll.NdMadv == BienBll.MainPos)
                {
                    sql = "select PO_MA,PO_TEN from DMPOS order by PO_MA";
                }
                else
                {
                    sql = "select PO_MA,PO_TEN from DMPOS where PO_MA='"+BienBll.NdMadv+"'";
                }
                dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                CboPos.SelectedIndex = 5;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();

        }

        private void btnManual_Click(object sender, RoutedEventArgs e)
        {
            WpfDonGiaNhapTay f = new WpfDonGiaNhapTay();
            f.ShowDialog();
        }
    }
}
