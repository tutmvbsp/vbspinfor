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
    public partial class WpfNhapPhep
    {
        public WpfNhapPhep()
        {
            InitializeComponent();
        }

        ToolBll s = new ToolBll();
        private readonly ClsServer cls = new ClsServer();
        private DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        DataTable dtxa = new DataTable();
        ServerInfor srv = new ServerInfor();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dtpNgay.SelectedDate = DateTime.Now;
                cls.ClsConnect();
                string sql = "select PO_MA,PO_TEN from DMPOS where PO_MA=" + "'" + BienBll.NdMadv.Trim() + "'";
                //var sql = BienBll.NdCapbc.Trim() == "1" ? string.Format("select PO_MA,PO_TEN from DMPOS where PO_MA='{0}'", BienBll.NdMadv.Trim()) : "select PO_MA,PO_TEN from DMPOS where right(PO_MA,2)<>'00'";
                var dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                CboPos.SelectedIndex = 0;
                CboPB.Items.Clear();
                if (BienBll.NdMadv.Trim() == BienBll.MainPos.Trim())
                   dtxa = cls.LoadDataText("select * from DM_PHONGBAN where MA in ('18','19','20','21','22') order by MA");
                else dtxa = cls.LoadDataText("select * from DM_PHONGBAN where MA in ('29','30','31') order by MA");
                for (int i = 0; i < dtxa.Rows.Count; i++)
                {
                    CboPB.Items.Add(dtxa.Rows[i][0].ToString().Trim() + " | " + dtxa.Rows[i][1]);
                }
                cls.DongKetNoi();
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
                dtNew = dt.GetChanges();
                if (dtNew.Rows.Count > 0)
                {
                    cls.ClsConnect();
                    foreach (DataRow dr in dtNew.Rows)
                    {
                        string strsql = "update DM_CANBO set THG_LV='"+dr["THG_LV"] +"',STT=" + dr["STT"] + ",Z_PHEPPRE=" + dr["Z_PHEPPRE"] + ",Z_LAMTHEMPRE =" + dr["Z_LAMTHEMPRE"]+ ", NG_UP_PHEP='"+dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd")+"',GDXA=upper('"+dr["GDXA"]+"') where MA_CIF='" + dr["MA_CIF"].ToString().Trim() + "'";
                        cls.UpdateDataText(strsql);
                    }
                    MessageBox.Show("Lưu dữ liệu thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    dtNew.Clear();
                }
                else
                {
                    MessageBox.Show("Xem lại. Chưa có dữ liệu hoặc chưa nhấn nút Cập nhật !", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                MessageBox.Show("Chưa chọn cán bộ nào ", "Thông báo",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show("Cập nhật thành công, Chọn 'Lưu' để lưu lại dữ liệu! ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void CboPB_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                dtpNgay.SelectedDate = DateTime.Now;
                cls.ClsConnect();
                string str = "select cast(0 as bit) CHON,a.* from DM_CANBO a where ND_MADV='" + s.Left(CboPos.SelectedValue.ToString().Trim(), 6) + 
                             "' and a.ND_PHONGBAN='" + s.Left(CboPB.SelectedValue.ToString().Trim(), 2) + "' and a.ND_TTHAI<>'C' order by a.STT";
                //MessageBox.Show(str);
                dt = cls.LoadDataText(str);
                if (dt.Rows.Count > 0)
                    dgvData.ItemsSource = dt.DefaultView;
                else
                    MessageBox.Show("Chưa có số liệu", "Thông báo");
                cls.DongKetNoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();

        }
    }
}
