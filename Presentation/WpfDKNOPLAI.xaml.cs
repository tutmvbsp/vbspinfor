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
    public partial class WpfDKNOPLAI : Window
    {
        public WpfDKNOPLAI()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ToolBll bll  = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                DataTable dtng = new DataTable();
                dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
                dtpTuNgay.SelectedDate = DateTime.Parse("01/01/" + DateTime.Now.AddYears(-1).ToString("yyyy"));
                dtpDenNgay.SelectedDate =dtpTuNgay.SelectedDate.Value.AddMonths(6);//DateTime.Now.AddDays(-1);
                DataTable dtpos = new DataTable();
                string sql = "select PO_MA,PO_TEN from DMPOS order by PO_MA";
                dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                CboPos.SelectedIndex = 0;
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
                string sql = "select MA,TEN from DMXA where PGD_QL= " + "'" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "'" + " order by MA";
                dtxa = cls.LoadDataText(sql);
                for (int i = 0; i < dtxa.Rows.Count; i++)
                {
                    CboXa.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                }
                CboXa.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }

        }



        private void btnOk_Click(object sender, RoutedEventArgs e)
        {

            cls.ClsConnect();
            try
            {
                int thamso = 4;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                /*
                bien[0] = "@Mato";
                if (CboTo != null)
                    giatri[0] = bll.Left(CboTo.SelectedValue.ToString().Trim(), 7);
                else
                {
                    MessageBox.Show("Chọn Tổ", "Mess");
                    return;
                }
                 */
                bien[0] = "@Ngay";
                if (dtpNgay.SelectedDate.Value == null)
                {
                    MessageBox.Show("Chưa chọn ngày ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                }

                bien[1] = "@TuNgay";
                if (dtpTuNgay.SelectedDate.Value == null)
                {
                    MessageBox.Show("Chưa chọn ngày ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    giatri[1] = dtpTuNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                }
                bien[2] = "@DenNgay";
                if (dtpDenNgay.SelectedDate.Value == null)
                {
                    MessageBox.Show("Chưa chọn ngày ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    giatri[2] = dtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                }
                bien[3] = "@MaXa";
                giatri[3] = bll.Left(CboXa.SelectedValue.ToString().Trim(),6);
               // MessageBox.Show(giatri[0] + " "+giatri[1] + " " + giatri[2] + " " + giatri[3] + " " + giatri[4] + " " + giatri[5] + " " );
                dt = cls.LoadDataProcPara("usp_DKTRALAI", bien, giatri, thamso);
                if (dt.Rows.Count > 0)
                {
                    rpt_DKNOPLAI rpt = new rpt_DKNOPLAI();
                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                    //MessageBox.Show("OK ", "Thông báo",MessageBoxButton.OK,MessageBoxImage.Information);
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
        }

        private void dtpTuNgay_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dtpDenNgay.SelectedDate = dtpTuNgay.SelectedDate.Value.AddMonths(6);//DateTime.Now.AddDays(-1);
        }
    }
}
