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
using CrystalDecisions.Shared;
using DAL;
using BLL;
using System.Data;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfLaiTon.xaml
    /// </summary>
    public partial class WpfPtsl : Window
    {
        public WpfPtsl()
        {
            InitializeComponent();
        }

        private ClsServer cls = new ClsServer();
        private ToolBll bll = new ToolBll();
        private ServerInfor srv = new ServerInfor();
        private DataTable dt = new DataTable();

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
            try
            {
                cls.ClsConnect();
                //DataTable dtpos = new DataTable();
                string sql = "select PO_MA,PO_TEN from DMPOS order by PO_MA";
                var dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                //DataTable dtng = new DataTable();
                var dtng =
                    cls.LoadDataText(
                        "select MAX(convert(date,NGAYKU,105)) as NGKU,MAX(convert(date,NGAYBT,105)) as NGBT from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGKU"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();

        }

    

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {

            cls.ClsConnect();
            try
            {
                int thamso = 2;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@MaPos";
                giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                bien[1] = "@Ngay";
                if (dtpNgay.SelectedDate != null)
                    giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                else
                {
                    MessageBox.Show("Chọn Ngày", "Mess");
                    return;
                }
                if (Ration1.IsChecked==true)
                dt = cls.LoadDataProcPara("usp_Ptsl", bien, giatri, thamso);
                else dt = cls.LoadDataProcPara("usp_Ptslct", bien, giatri, thamso);
                if (dt.Rows.Count > 0)
                {
                    rpt_Ptsl rpt = new rpt_Ptsl();
                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                }
                else
                {
                    MessageBox.Show("Không có bản ghi nào ", "Mess");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();
        }
        
    }
}
