using System;
using System.Data;
using System.Windows;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfLaiTon.xaml
    /// </summary>
    public partial class WpfNhapTGDC
    {
        public WpfNhapTGDC()
        {
            InitializeComponent();
        }

        ToolBll s = new ToolBll();
        private readonly ClsServer cls = new ClsServer();
        private DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        ServerInfor srv = new ServerInfor();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();

                dt = cls.LoadDataText("select cast(0 as bit) CHON,*,cast(tgdc as varchar(10)) ST from NG_DUNG where ND_TTHAI='A' and ND_MADV='" + BienBll.NdMadv.Trim() + "' order by ND_CMT");
                if (dt.Rows.Count > 0) dgvData.ItemsSource = dt.DefaultView;
                else MessageBox.Show("Không có dữ liệu !", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
            
        }

        
        private void btnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtNew.Rows.Count > 0)
                {
                    cls.ClsConnect();
                    foreach (DataRow dr in dtNew.Rows)
                    {
                        string strsql = "update NG_DUNG set TGDC_PRE=" + dr["TGDC_PRE"] + ",TGDC =" + dr["TGDC"]+ " where ND_CMT='" + dr["ND_CMT"].ToString().Trim() + "'";
                        cls.UpdateDataText(strsql);
                    }
                    MessageBox.Show("Update Ok", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                    dtNew.Clear();
                    //dgvTarGet.ItemsSource = dtNew.DefaultView;
                }
                else
                {
                    MessageBox.Show("Xem lại. Chưa có dữ liệu", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
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


        private void BtnUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            dtNew = dt.Clone();
            foreach (DataRow dr in dt.Rows)
            {
                if ((bool)dr[0] == true)
                {
                    dtNew.ImportRow(dr);
                }
            }
            if (dtNew == null || dtNew.Rows.Count == 0)
            {
                MessageBox.Show("Chưa chọn xã nào ", "Mess",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show("Cập nhật thành công, Chọn 'Lưu' để lưu lại dữ liệu! ", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                // dgvTarGet.ItemsSource = dtNew.DefaultView;
                //rpt_01TG rpt = new rpt_01TG();
                //RPUtility.ShowRp(rpt, dtNew, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
            }
            // dtNew.RejectChanges();
            // dtNew = null;            
        }
        private void ChkAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach (DataRow dr in dt.Rows)
            {
                //MessageBox.Show(dr[0].ToString() + "  " + dr[1].ToString());
                //if ((bool) dr[0] == false)
                //{
                dr[0] = true;
                //}
            }
        }

        private void ChkAll_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (DataRow dr in dt.Rows)
            {
                ////MessageBox.Show(dr[0].ToString() + "  " + dr[1].ToString());
                //if ((bool) dr[0] == false)
                //{
                dr[0] = false;
                //}
            }

        }
    }
}
