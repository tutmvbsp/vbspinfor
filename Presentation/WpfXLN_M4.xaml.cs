using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDelete.xaml
    /// </summary>
    public partial class WpfXLN_M4
    {
        public WpfXLN_M4()
        {
            InitializeComponent();
        }
        readonly ClsServer cls = new ClsServer();
        ServerInfor srv = new ServerInfor();
        ToolBll str = new ToolBll();
        DataTable dt = new DataTable();
        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {   cls.ClsConnect();
                int thamso = 4;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@MaPos";
                giatri[0] = str.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                bien[1] = "@MaXa";
                giatri[1]= str.Left(CboXa.SelectedValue.ToString().Trim(), 6);
                bien[2] = "@Ngay";
                if (dtpNgay.SelectedDate != null) giatri[2] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                bien[3] = "@Mau";
                if (Ration1.IsChecked == true)
                    giatri[3] = "1";
                else if (Ration2.IsChecked == true)
                    giatri[3] = "2";
                else giatri[3] = "3";
                dt = cls.LoadLdbf("usp_XLN_M4", bien, giatri, thamso);
                if (dt.Rows.Count > 0)
                {
                    rpt_XLN_M4 rpt = new rpt_XLN_M4();
                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                } else MessageBox.Show("Không có khế ước nào đến hạn !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                cls.DongKetNoi();
            }
                 
        }

        private void WpfXLN_M1_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                string sql = "select PO_MA,PO_TEN from DMPOS where PO_MA='"+BienBll.NdMadv.Trim()+"'";
                var dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                //CboPos.SelectedIndex = 0;
                //var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                //dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
                dtpNgay.SelectedDate = DateTime.Parse(DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month).ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
            
        }


        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CboPos_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (str.Left(CboPos.SelectedValue.ToString().Trim(), 6) != "003000")
                {
                    CboXa.Items.Clear();
                    cls.ClsConnect();
                    DataTable dtxa = new DataTable();
                    string sql = "select MA,TEN from DMXA where right(MA,2)<>'00' and PGD_QL= " + "'" +
                                 str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "'" + " order by MA";
                    dtxa = cls.LoadDataText(sql);
                    for (int i = 0; i < dtxa.Rows.Count; i++)
                    {
                        CboXa.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                    }
                }
                else
                {
                    CboXa.Items.Add("003000 | Tất cả");
                }
                CboXa.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }

        private void Ration1_Checked(object sender, RoutedEventArgs e)
        {
            CboXa.IsEnabled = true;
        }

        private void Ration2_Checked(object sender, RoutedEventArgs e)
        {
            CboXa.IsEnabled = false;
        }

        private void Ration3_Checked(object sender, RoutedEventArgs e)
        {
            CboXa.IsEnabled = false;
        }
    }
}
