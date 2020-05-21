using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Text;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for Wpf_THONGBAO_DONG105.xaml
    /// </summary>
    public partial class WpfXLN_M3 : Window
    {
        ClsServer cls = new ClsServer();
        ServerInfor srv = new ServerInfor();
        ToolBll str = new ToolBll();
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();

        public WpfXLN_M3()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                string sql = "select PO_MA,PO_TEN from DMPOS where PO_MA='"+BienBll.NdMadv.Trim()+"' order by PO_MA";
                var dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                CboPos.SelectedIndex = 0;
                dtpNgay.SelectedDate = DateTime.Parse(DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month).ToString());
                for (int i = 1; i <= 12; i++)
                {
                    CboThang.Items.Add(i.ToString("00"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error "+ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }


        private void lblChapNhan_MouseDown(object sender, MouseButtonEventArgs e)
        {

            dtNew = dt.GetChanges();
            if (dtNew == null || dtNew.Rows.Count == 0)
                MessageBox.Show("Chưa có thay đổi ngày nào !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
                dgvTarGet.ItemsSource = dtNew.DefaultView;
        }
        private void lblTuChoi_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dt.RejectChanges();
            dt = null;
            dgvData.ItemsSource = null;
            dgvData.Items.Refresh();
            dtNew = null;
            dgvTarGet.ItemsSource = null;
            dgvTarGet.Items.Refresh();

        }


        private void lblOk_MouseDown(object sender, MouseButtonEventArgs e)
        {

            try
            {
                cls.ClsConnect();
                if (dtNew.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtNew.Rows)
                    {
                        string strsql = "update KH_XLN set TRANO=" + dr["TRANO"] + ",LUUVU=" + dr["LUUVU"] + ",GIAHAN=" +
                                        dr["GIAHAN"] + ",CHUYEN_QH=" + dr["CHUYEN_QH"] + ",KH_THUNQH=" + dr["KH_THUNQH"] + ",NQH_LD=" + dr["NQH_LD"]
                                        + " where MATO='" + dr["MATO"].ToString().Trim() + "' AND THANG_KH='" + dr["THANG_KH"].ToString().Trim()+"'";
                        cls.UpdateDataText(strsql);
                        //MessageBox.Show(strsql);
                    }
                    MessageBox.Show("Update Ok", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Xem lại. Chưa có dữ liệu", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();

        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void LblGetData_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            cls.ClsConnect();
            try
            {
                string strsql = "select * from KH_XLN where MAXA='" + str.Left(CboXa.SelectedValue.ToString().Trim(), 7) +
                                "' and THANG_KH='" + CboThang.SelectedValue.ToString().Trim() + "' and TRANGTHAI<>'C' order by MATO";
                dt = cls.LoadDataText(strsql);
                if (dt.Rows.Count > 0)
                {
                    dgvData.ItemsSource = dt.DefaultView;
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

        private void CboPos_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                CboXa.Items.Clear();
                cls.ClsConnect();
                DataTable dtxa = new DataTable();
                string sql = "select MA,TEN from DMXA where PGD_QL= " + "'" + str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "'" + " order by MA";
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
    }
}
