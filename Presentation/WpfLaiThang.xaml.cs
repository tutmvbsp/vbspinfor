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
    /// Interaction logic for WpfLaiThang.xaml
    /// </summary>
    public partial class WpfLaiThang : Window
    {
        public WpfLaiThang()
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
            Ration1.IsChecked = true;
            ChkAll.IsEnabled = false;
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
                var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGKU,MAX(convert(date,NGAYBT,105)) as NGBT from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGKU"].ToString());
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
                //DataTable dtxa = new DataTable();
                string sql = "select MA,TEN from DMXA where PGD_QL= " + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) +  " order by MA";
                var dtxa = cls.LoadDataText(sql);
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
                //DataTable dtto = new DataTable();
                string sql = "select TO_MATO,TO_TENTT from HSTO where Left(TO_MADP,6) = " + bll.Left(CboXa.SelectedValue.ToString().Trim(), 6) + " order by TO_MATO";
                //MessageBox.Show(sql);
                var dtto = cls.LoadDataText(sql);
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

        private void Ration1_Checked(object sender, RoutedEventArgs e)
        {
            lblTo.IsEnabled = true;
            CboTo.IsEnabled = true;
            lblXa.IsEnabled = true;
            CboXa.IsEnabled = true;
            ChkAll.IsEnabled = false;
            ChkAll.IsChecked = false;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            
            cls.ClsConnect();
            if (Ration1.IsChecked == true)
            {
                try
                {
                    int thamso = 3;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@Mato";
                    if (CboTo != null)
                        giatri[0] = bll.Left(CboTo.SelectedValue.ToString().Trim(), 7);
                    else
                    {
                        MessageBox.Show("Chọn Tổ", "Mess");
                        return;
                    }
                    bien[1] = "@Ngay";
                    if (dtpNgay.SelectedDate != null)
                        giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    else
                    {
                        MessageBox.Show("Chọn Ngày", "Mess");
                        return;
                    }
                    bien[2] = "@MucTon";
                    giatri[2] = txtTon.Text;
                    dt = cls.LoadDataProcPara("usp_LaiTonCT", bien, giatri, thamso);
                    if (dt.Rows.Count > 0)
                    {
                        rpt_LaiTonCt rpt = new rpt_LaiTonCt();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                         srv.DbPassSerVer());
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
            } else if (Ration2.IsChecked == true)
            {
                try
                {

                    int thamso = 3;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaXa";
                    if (CboXa != null)
                        giatri[0] = bll.Left(CboXa.SelectedValue.ToString().Trim(), 6);
                    else
                    {
                        MessageBox.Show("Chọn Xã", "Mess");
                        return;
                    }
                    bien[1] = "@Ngay";
                    if (dtpNgay.SelectedDate != null)
                        giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    else
                    {
                        MessageBox.Show("Chọn Ngày", "Mess");
                        return;
                    }
                    bien[2] = "@MucTon";
                    giatri[2] = txtTon.Text;
                    dt = cls.LoadDataProcPara("usp_LaiTonTo", bien, giatri, thamso);
                    if (dt.Rows.Count > 0)
                    {
                        rpt_LaiTonTo rpt = new rpt_LaiTonTo();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                         srv.DbPassSerVer());
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
            } else if (Ration3.IsChecked == true)
            {
                try
                {

                    int thamso = 3;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    if (CboPos.SelectedValue != null)
                        giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    else
                    {
                        MessageBox.Show("Chọn POS", "Mess");
                        return;
                    }
                    bien[1] = "@Ngay";
                    if (dtpNgay.SelectedDate != null)
                        giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    else
                    {
                        MessageBox.Show("Chọn Ngày", "Mess");
                        return;
                    }
                    bien[2] = "@MucTon";
                    giatri[2] = txtTon.Text;
                    dt = cls.LoadDataProcPara("usp_LaiTonXa", bien, giatri, thamso);
                    if (dt.Rows.Count > 0)
                    {
                        //dataGrid1.ItemsSource = dt.DefaultView;
                        rpt_LaiTonXa rpt = new rpt_LaiTonXa();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                         srv.DbPassSerVer());
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
            else if (Ration4.IsChecked==true)
            {
                try
                {

                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MucTon";
                    giatri[0] = txtTon.Text;
                    bien[1] = "@Ngay";
                    if (dtpNgay.SelectedDate != null)
                        giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    else
                    {
                        MessageBox.Show("Chọn Ngày", "Mess");
                        return;
                    }
                    
                    dt = cls.LoadDataProcPara("usp_LaiTonPos", bien, giatri, thamso);
                    if (dt.Rows.Count > 0)
                    {
                        //dataGrid1.ItemsSource = dt.DefaultView;
                        rpt_LaiTonPos rpt = new rpt_LaiTonPos();
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
                
            }
            else if (Ration5.IsChecked==true)
            {
                try
                {

                    int thamso = 3;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    if (CboPos.SelectedValue != null)
                        giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    else
                    {
                        MessageBox.Show("Chọn POS", "Mess");
                        return;
                    }
                    bien[1] = "@Ngay";
                    if (dtpNgay.SelectedDate != null)
                        giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    else
                    {
                        MessageBox.Show("Chọn Ngày", "Mess");
                        return;
                    }
                    bien[2] = "@MucTon";
                    giatri[2] = txtTon.Text;
                    if (ChkAll.IsChecked == true)
                    {
                        dt = cls.LoadDataProcPara("usp_LaiTonAll", bien, giatri, thamso);
                    }
                    else
                    {
                        dt = cls.LoadDataProcPara("usp_LaiTonChTr", bien, giatri, thamso);
                    }
                    rpt_LaiTonCTXa rpt = new rpt_LaiTonCTXa();
                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
            else if (Ration6.IsChecked == true)
            #region
            {
                try
                {
                    int thamso = 3;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@Mato";
                    if (CboTo != null)
                        giatri[0] = bll.Left(CboTo.SelectedValue.ToString().Trim(), 7);
                    else
                    {
                        MessageBox.Show("Chọn Tổ", "Mess");
                        return;
                    }
                    bien[1] = "@Ngay";
                    if (dtpNgay.SelectedDate != null)
                        giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    else
                    {
                        MessageBox.Show("Chọn Ngày", "Mess");
                        return;
                    }
                    bien[2] = "@MucTon";
                    giatri[2] = txtTon.Text.Trim();
                    dt = cls.LoadDataProcPara("usp_LaiTonGB", bien, giatri, thamso);
                    if (dt.Rows.Count > 0)
                    {
                        rpt_LaiTonGB rpt = new rpt_LaiTonGB();
                        //rpt.PrintOptions.PaperSize=PaperSize.PaperA4;
                        //rpt.PrintOptions.PaperOrientation=PaperOrientation.Portrait;
                          
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                         srv.DbPassSerVer());
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
            #endregion
            else if (Ration7.IsChecked == true)
            #region
            {
                try
                {
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    if (CboTo != null)
                        giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    else
                    {
                        MessageBox.Show("Chọn POS", "Mess");
                        return;
                    }
                    bien[1] = "@Ngay";
                    if (dtpNgay.SelectedDate != null)
                        giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    else
                    {
                        MessageBox.Show("Chọn Ngày", "Mess");
                        return;
                    }
                    dt = cls.LoadDataProcPara("usp_ThuLaiTon", bien, giatri, thamso);
                    if (dt.Rows.Count > 0)
                    {
                        rpt_ThuLaiTon rpt = new rpt_ThuLaiTon();
                        //rpt.PrintOptions.PaperSize=PaperSize.PaperA4;
                        //rpt.PrintOptions.PaperOrientation=PaperOrientation.Portrait;

                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                         srv.DbPassSerVer());
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
            #endregion
            else if (Ration8.IsChecked == true)
            #region
            {
                try
                {
                    int thamso = 3;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaXa";
                    if (CboXa != null)
                        giatri[0] = bll.Left(CboXa.SelectedValue.ToString().Trim(), 6);
                    else
                    {
                        MessageBox.Show("Chọn Xã", "Mess");
                        return;
                    }
                    bien[1] = "@Ngay";
                    if (dtpNgay.SelectedDate != null)
                        giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    else
                    {
                        MessageBox.Show("Chọn Ngày", "Mess");
                        return;
                    }
                    bien[2] = "@MucTon";
                    giatri[2] = txtTon.Text.Trim();
                    dt = cls.LoadDataProcPara("usp_KiemtraDoichieu", bien, giatri, thamso);
                    if (dt.Rows.Count > 0)
                    {
                        rpt_KiemtraDoichieu rpt = new rpt_KiemtraDoichieu();
                        //rpt.PrintOptions.PaperSize=PaperSize.PaperA4;
                        //rpt.PrintOptions.PaperOrientation=PaperOrientation.Portrait;

                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                         srv.DbPassSerVer());
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
            #endregion


        }

        private void Ration2_Checked(object sender, RoutedEventArgs e)
        {
            lblTo.IsEnabled = false;
            CboTo.IsEnabled = false;
            lblXa.IsEnabled = true;
            CboXa.IsEnabled = true;
            ChkAll.IsEnabled = false;
            ChkAll.IsChecked = false;
        }

        private void Ration3_Checked(object sender, RoutedEventArgs e)
        {
            lblTo.IsEnabled = false;
            CboTo.IsEnabled = false;
            lblXa.IsEnabled = false;
            CboXa.IsEnabled = false;
            ChkAll.IsEnabled = false;
            ChkAll.IsChecked = false;
        }

        private void Ration4_Checked(object sender, RoutedEventArgs e)
        {
            CboPos.SelectedIndex = 4;
            lblTo.IsEnabled = false;
            CboTo.IsEnabled = false;
            lblXa.IsEnabled = false;
            CboXa.IsEnabled = false;
            ChkAll.IsEnabled = false;
            ChkAll.IsChecked = false;

        }

        private void Ration5_Checked(object sender, RoutedEventArgs e)
        {
            ChkAll.IsEnabled = true;
            lblTo.IsEnabled = false;
            CboTo.IsEnabled = false;
            lblXa.IsEnabled = false;
            CboXa.IsEnabled = false;

        }

        private void ChkAll_Checked(object sender, RoutedEventArgs e)
        {
            CboPos.SelectedIndex = 0;
        }

        private void Ration6_Checked(object sender, RoutedEventArgs e)
        {
            lblTo.IsEnabled = true;
            CboTo.IsEnabled = true;
            lblXa.IsEnabled = true;
            CboXa.IsEnabled = true;
            ChkAll.IsEnabled = false;
            ChkAll.IsChecked = false;

        }
    }
}
