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
using System.IO;
using System.Globalization;
using DAL;
using BLL;
using System.Data;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDvut.xaml
    /// </summary>
    public partial class WpfVppBC : Window
    {
        public WpfVppBC()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ToolBll bll = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        DataTable dtSua = new DataTable();
        DataTable dtXoa = new DataTable();
        DataTable dtxa = new DataTable();
        private string strsql = "";
   

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateMonthsAndYears();
            Option1_Checked(null, null);
            load();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
            //MessageBox.Show(bll.Left(comboBoxMonth.SelectedValue.ToString().Trim(),2));
            //MessageBox.Show(comboBoxYear.SelectedValue.ToString().Trim());
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                int thamso = 7;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@MaPos";
                giatri[0] = BienBll.NdMadv.Trim();
                bien[1] = "@Thang";
                giatri[1] = bll.Left(comboBoxMonth.SelectedValue.ToString().Trim(), 2);
                bien[2] = "@Nam";
                giatri[2] = comboBoxYear.SelectedValue.ToString().Trim();
                bien[3] = "@DGiao";
                giatri[3] = CboDgiao.SelectedValue.ToString().Trim();
                bien[4] = "@Giao";
                giatri[4] = CboGiao.SelectedValue.ToString().Trim();
                bien[5] = "@Nhan";
                giatri[5] = CboNhan.SelectedValue.ToString().Trim();
                bien[6] = "@Phong";
                giatri[6] = bll.Left(CboPhong.SelectedValue.ToString().Trim(),2);
                if (Option1.IsChecked==true)
                    dt = cls.LoadDataProcPara("usp_Vpp02", bien, giatri, thamso);
                else if (Option2.IsChecked == true)
                    dt = cls.LoadDataProcPara("usp_Vpp03", bien, giatri, thamso);
                else
                    dt = cls.LoadDataProcPara("usp_Vpp04", bien, giatri, thamso);
                if (dt.Rows.Count > 0)
                {
                    if (Option1.IsChecked == true)
                    {
                        if (BienBll.NdMadv.Trim() == BienBll.MainPos.Trim())
                        {
                            rpt_Vpp02 rpt = new rpt_Vpp02();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                srv.DbPassSerVer());
                        }
                        else
                        {
                            rpt_Vpp03 rpt = new rpt_Vpp03();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                srv.DbPassSerVer());
                        }
                    } else if (Option2.IsChecked == true)
                    {
                        rpt_Vpp04 rpt = new rpt_Vpp04();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                            srv.DbPassSerVer());
                    }
                    else
                    {
                        rpt_Vpp05 rpt = new rpt_Vpp05();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                            srv.DbPassSerVer());

                    }
                } else MessageBox.Show("Không có bản ghi nào !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
            
        }

    

        private void PopulateMonthsAndYears()
        {
            //comboBoxMonth.ItemsSource = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames.Take(12).ToList();
            //comboBoxMonth.SelectedItem = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames[DateTime.Now.AddMonths(-1).Month - 1];
            for (int x = 0; x < 12; x++)
            {
                comboBoxMonth.Items.Add
                (
                   (x + 1).ToString("00")
                   + " "
                   + CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.GetValue(x)
                 );
            }
            comboBoxMonth.SelectedIndex = 0;
            comboBoxYear.ItemsSource = Enumerable.Range(DateTime.Now.Year,5).ToList();
            comboBoxYear.SelectedItem = DateTime.Now.Year;
            comboBoxYear.SelectedIndex = 0;
        }

        private void Option2_Checked(object sender, RoutedEventArgs e)
        {
            lblDGiao.IsEnabled = false;
            CboDgiao.IsEnabled = false;
            lblGiao.IsEnabled = false;
            CboGiao.IsEnabled = false;
            lblNhan.IsEnabled = false;
            CboNhan.IsEnabled = false;
            lblPhong.IsEnabled = false;
            CboPhong.IsEnabled = false;
        }

        private void Option3_Checked(object sender, RoutedEventArgs e)
        {
            lblDGiao.IsEnabled = true;
            CboDgiao.IsEnabled = true;
            lblGiao.IsEnabled = true;
            CboGiao.IsEnabled = true;
            lblNhan.IsEnabled = true;
            CboNhan.IsEnabled = true;
            lblPhong.IsEnabled = true;
            CboPhong.IsEnabled = true;
        }

        private void Option1_Checked(object sender, RoutedEventArgs e)
        {
            lblDGiao.IsEnabled = false;
            CboDgiao.IsEnabled = false;
            lblGiao.IsEnabled = false;
            CboGiao.IsEnabled = false;
            lblNhan.IsEnabled = false;
            CboNhan.IsEnabled = false;
            lblPhong.IsEnabled = false;
            CboPhong.IsEnabled = false;

        }

        private void load()
        {
            try
            {
                cls.ClsConnect();
                if (BienBll.NdMadv.Trim() == BienBll.MainPos.Trim())
                    dtxa = cls.LoadDataText("select * from DM_PHONGBAN where MA in ('17','18','19','20','21','22') order by MA");
                else dtxa = cls.LoadDataText("select * from DM_PHONGBAN where MA in ('29','30','31') order by MA");
                for (int i = 0; i < dtxa.Rows.Count; i++)
                {
                    CboPhong.Items.Add(dtxa.Rows[i][0].ToString().Trim() + " | " + dtxa.Rows[i][1]);
                }
                CboPhong.SelectedIndex = 0;

                strsql = "select ND_MA MA,ND_TEN TEN from NG_DUNG where ND_MADV='"+BienBll.NdMadv.Trim()+"' order by ND_TEN";
                var dtDgiao = cls.LoadDataText(strsql);
                CboDgiao.ItemsSource = dtDgiao.DefaultView;
                CboDgiao.SelectedValuePath = "MA";
                CboDgiao.DisplayMemberPath = "TEN";
                CboDgiao.SelectedIndex = 1;

                strsql = "select ND_MA MA,ND_TEN TEN from NG_DUNG where ND_MADV='" + BienBll.NdMadv.Trim() + "' order by ND_TEN";
                var dtgiao = cls.LoadDataText(strsql);
                CboGiao.ItemsSource = dtgiao.DefaultView;
                CboGiao.SelectedValuePath = "MA";
                CboGiao.DisplayMemberPath = "TEN";
                CboGiao.SelectedIndex = 1;

                strsql = "select ND_MA MA,ND_TEN TEN from NG_DUNG where ND_MADV='" + BienBll.NdMadv.Trim() + "' order by ND_TEN";
                var dtnhan = cls.LoadDataText(strsql);
                CboNhan.ItemsSource = dtnhan.DefaultView;
                CboNhan.SelectedValuePath = "MA";
                CboNhan.DisplayMemberPath = "TEN";
                CboNhan.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }
    }
}
