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
    public partial class WpfKTSDVV : Window
    {
        public WpfKTSDVV()
        {
            InitializeComponent();
        }

        ClsServer cls = new ClsServer();
        ToolBll bll = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Ration1.IsChecked = true;
            dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
            dtpTuNgay.SelectedDate = DateTime.Now.AddDays(-30);
            dtpDenNgay.SelectedDate = DateTime.Now.AddDays(-1);
            try
            {
                cls.ClsConnect();
                DataTable dtpos = new DataTable();
                string sql = "select PO_MA,PO_TEN from DMPOS order by PO_MA";
                dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                DataTable dtng = new DataTable();
                dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
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
                CboXa.SelectedIndex = 0;
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
                //MessageBox.Show(sql);
                dtto = cls.LoadDataText(sql);
                for (int i = 0; i < dtto.Rows.Count; i++)
                {
                    CboTo.Items.Add(dtto.Rows[i][0] + " | " + dtto.Rows[i][1]);
                }
                CboTo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //                @MaPos = N'003005',
                //@Ngay = N'31/07/2015',
                //@Maxa = N'300512',
                //@Mato = N'0137149',
                //@TuNgay = N'01/01/2014',
                //@DenNgay = N'30/06/2015'

                cls.ClsConnect();
                DataTable dt = new DataTable();
                int thamso = 5;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Mato";
                if (Ration3.IsChecked==true || Ration1.IsChecked == true) giatri[0] = bll.Left(CboXa.SelectedValue.ToString().Trim(), 6);
                else giatri[0] = bll.Left(CboTo.SelectedValue.ToString().Trim(), 7);
                bien[1] = "@Ngay";
                if (dtpNgay.SelectedDate == null)
                {
                    MessageBox.Show("Chưa chọn ngày ", "Mess");
                    return;
                }
                else { giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");}
                bien[2] = "@TuNgay";
                if (dtpTuNgay.SelectedDate == null)
                {
                    MessageBox.Show("Chưa chọn ngày ", "Mess");
                    return;
                }
                else { giatri[2] = dtpTuNgay.SelectedDate.Value.ToString("yyyy-MM-dd"); }
                bien[3] = "@DenNgay";
                if (dtpDenNgay.SelectedDate == null)
                {
                    MessageBox.Show("Chưa chọn ngày ", "Mess");
                    return;
                }
                else { giatri[3] = dtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd"); }
                bien[4] = "@Mau";
                if (Ration1.IsChecked == true) giatri[4] = "1";
                else if (Ration2.IsChecked == true) giatri[4] = "2";
                else giatri[4] = "3";
                // MessageBox.Show(giatri[0] + "  " + giatri[1]);
                dt = cls.LoadDataProcPara("usp_KTSDVV", bien, giatri, thamso);
                if (dt.Rows.Count > 0)
                {
                    rpt_KTSDVV rpt = new rpt_KTSDVV();
                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                     srv.DbPassSerVer());
                }
                else
                {
                    MessageBox.Show("Không có bản ghi nào ", "Mess");
                }

            }
            catch ( Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private void Ration2_Checked(object sender, RoutedEventArgs e)
        {
            lblTo.IsEnabled = true;
            CboTo.IsEditable = true;
        }

        private void Ration1_Checked(object sender, RoutedEventArgs e)
        {
            lblTo.IsEnabled = false;
            CboTo.IsEditable = false;
        }

        private void Ration3_Checked(object sender, RoutedEventArgs e)
        {
            lblTo.IsEnabled = false;
            CboTo.IsEditable = false;

        }
    }
}
