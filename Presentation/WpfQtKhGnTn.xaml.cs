using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfInKhGnTn.xaml
    /// </summary>
    public partial class WpfQtKhGnTn
    {
        public WpfQtKhGnTn()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        ServerInfor srv = new ServerInfor();
        ClsServer cls = new ClsServer();
        ToolBll bll = new ToolBll();
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(BienBll.NdMadv.Trim());
            cls.ClsConnect();
            if (ChkTongHop.IsChecked == false)
            {
                int thamso = 3;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@TuNgay";
                if (dtpTuNgay.SelectedDate != null)
                    giatri[0] = dtpTuNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                else
                {
                    MessageBox.Show("Chọn ngày : ");
                    return;
                }
                bien[1] = "@DenNgay";
                if (dtpTuNgay.SelectedDate != null)
                    giatri[1] = dtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                else
                {
                    MessageBox.Show("Chọn ngày : ");
                    return;
                }

                bien[2] = "@MaPos";

                
                if (cboPos.SelectedValue == null)
                {
                    MessageBox.Show("Chưa chọn POS");
                    return;
                }
                else
                {
                    giatri[2] = bll.Left(cboPos.SelectedValue.ToString().Trim(), 6); //BienBll.NdMadv.Trim();    
                }
                dt = cls.LoadDataProcPara("usp_KhGnTn03", bien, giatri, thamso);
                if (dt.Rows.Count > 0)
                {
                    rpt_QtKhGnTn rpt = new rpt_QtKhGnTn();
                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu !", "Mess");
                }
            }
            else
            {
                try
                {

                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@TuNgay";
                    if (dtpTuNgay.SelectedDate != null)
                        giatri[0] = dtpTuNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    else
                    {
                        MessageBox.Show("Chọn ngày : ");
                        return;
                    }
                    bien[1] = "@DenNgay";
                    if (dtpTuNgay.SelectedDate != null)
                        giatri[1] = dtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    else
                    {
                        MessageBox.Show("Chọn ngày : ");
                        return;
                    }

                    dt = cls.LoadDataProcPara("usp_KhGnTn04", bien, giatri, thamso);
                    if (dt.Rows.Count > 0)
                    {
                        //dataGrid1.ItemsSource = dt.DefaultView;
                        rpt_QtKhGnTn01 rpt = new rpt_QtKhGnTn01();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                         srv.DbPassSerVer());
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu !", "Mess");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            cls.DongKetNoi();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ChkTongHop.IsChecked = true;
            dtpTuNgay.SelectedDate = DateTime.Now.AddDays(-7);
            if (dtpTuNgay.SelectedDate.Value.ToString("dd") == "28" || dtpTuNgay.SelectedDate.Value.ToString("dd") == "29" || dtpTuNgay.SelectedDate.Value.ToString("dd") == "30" || dtpTuNgay.SelectedDate.Value.ToString("dd") == "31")
            {
                MessageBox.Show("Chú ý : Không thể thực hiện quyết toán số liệu giữa tháng khác nhau !","Mess",MessageBoxButton.OK,MessageBoxImage.Error);
                dtpTuNgay.SelectedDate = dtpTuNgay.SelectedDate.Value.AddDays(2);
                dtpDenNgay.SelectedDate = dtpTuNgay.SelectedDate.Value.AddDays(4);
            }
            try
            {
                cls.ClsConnect();
                string sql = "select PO_MA,PO_TEN from DMPOS where PO_MACN=" + "'" + BienBll.MainPos + "'" + " order by PO_MA";
                var dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    cboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();

        }

        private void dtpTuNgay_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtpTuNgay.SelectedDate.Value.ToString("dd") == "28" || dtpTuNgay.SelectedDate.Value.ToString("dd") == "29" || dtpTuNgay.SelectedDate.Value.ToString("dd") == "30" || dtpTuNgay.SelectedDate.Value.ToString("dd") == "31")
            {
                MessageBox.Show("Chú ý : Không thể thực hiện quyết toán số liệu giữa tháng khác nhau !", "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
                dtpTuNgay.SelectedDate = dtpTuNgay.SelectedDate.Value.AddDays(2);
                dtpDenNgay.SelectedDate = dtpTuNgay.SelectedDate.Value.AddDays(4);
            }
            else
            {
                dtpDenNgay.SelectedDate = dtpTuNgay.SelectedDate.Value.AddDays(6); //DateTime.Now.AddDays(-1);   
            }
            
        }

        private void ChkTongHop_Checked(object sender, RoutedEventArgs e)
        {
            cboPos.IsEnabled = false;
        }

        private void ChkTongHop_UnChecked(object sender, RoutedEventArgs e)
        {
            cboPos.IsEnabled = true;
        }

    
    }
}
