using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfLaiTon.xaml
    /// </summary>
    public partial class WpfKTHDT
    {
        public WpfKTHDT()
        {
            InitializeComponent();
        }

        ToolBll s = new ToolBll();
        private readonly ClsServer cls = new ClsServer();
        private DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        ServerInfor srv = new ServerInfor();
        private string strsql = "";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                
                //DataTable dtpos;
                var sql = BienBll.NdCapbc.Trim() == "1" ? string.Format("select PO_MA,PO_TEN from DMPOS where PO_MA='{0}'", BienBll.NdMadv.Trim()) : "select PO_MA,PO_TEN from DMPOS";
                //var sql = "select PO_MA,PO_TEN from DMPOS where PO_MA='"+BienBll.NdMadv+"'";
                var dtpos = cls.LoadDataText(sql);
                for (var i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                //CboPos.SelectedIndex = BienBll.NdCapbc.Trim() == "1" ? 0 : 5;
                var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());

                //DtpDenNgay.SelectedDate = DateTime.Parse(DtpNgay.SelectedDate.Value.ToString("yyyy-MM") + "-" + DateTime.DaysInMonth(DtpNgay.SelectedDate.Value.Year, DtpNgay.SelectedDate.Value.Month).ToString());
                var dvut = cls.LoadDataText("select * from dvut where dvut in ('11','12','13','14') order by DVUT");
                for (var i = 0; i < dvut.Rows.Count; i++)
                {
                    CboDvut.Items.Add(dvut.Rows[i][0] + " | " + dvut.Rows[i][1]);
                }

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
                        if (Ration1.IsChecked == true)
                            strsql = "update KTHDT set SOTOKT=" + dr["SOTOKT"] + ",SOTODC=" + dr["SOTODC"]
                                     + " where NAM='"+ dtpNgay.SelectedDate.Value.ToString("yyyy") + "' and PO_MA='" + dr["PO_MA"] + "' and MAU='" + dr["MAU"] + "' and TO_DVUT='" +
                                     dr["TO_DVUT"] + "' and MAXA='"+dr["MAXA"]+"'";
                        else
                            strsql = "update KTHDT set SOTOKT=" + dr["SOTOKT"] + ",SOTODC=" + dr["SOTODC"]
                                     + " where NAM='" + dtpNgay.SelectedDate.Value.ToString("yyyy") + "' and PO_MA='" + s.Left(CboPos.SelectedValue.ToString().Trim(), 6)
                                     + "' and MAU='2' and TO_DVUT='" + dr["TO_DVUT"] + "'";
                        cls.UpdateDataText(strsql);
                    }
                    dtNew.Clear();
                    MessageBox.Show("Lưu thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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


        private void LblGetData_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            dt = null;
            dgvData.ItemsSource = null;
            try
            {
                cls.ClsConnect();
                int thamso = 4;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@MaPos";
                giatri[0] = s.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                bien[1] = "@Ngay";
                if (dtpNgay.SelectedDate != null) giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                bien[2] = "@Hoi";
                giatri[2] = s.Left(CboDvut.SelectedValue.ToString().Trim(), 2);
                bien[3] = "@Mau";
                if (Ration1.IsChecked == true) giatri[3] = "1";
                else giatri[3] = "2";
                dt = cls.LoadDataProcPara("usp_KT_HDT", bien, giatri, thamso);
                if (dt.Rows.Count > 0) dgvData.ItemsSource = dt.DefaultView;
                else MessageBox.Show("Không có xã nào !", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }

        private void BtnIn_OnClick(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            try
            {
                if (Ration1.IsChecked == true)
                    strsql = "select * from KTHDT where NAM='"+dtpNgay.SelectedDate.Value.ToString("yyyy")+"' and PO_MA='" + s.Left(CboPos.SelectedValue.ToString().Trim(), 6)
                             + "' and MAU='1' and TO_DVUT='" +
                             s.Left(CboDvut.SelectedValue.ToString().Trim(), 2) + "' order by MAXA";
                else
                    strsql = "select * from KTHDT where PO_MA='" + s.Left(CboPos.SelectedValue.ToString().Trim(), 6)
                             + "' and MAU='2' order by TO_DVUT";

                var dtin = cls.LoadDataText(strsql);
                rpt_KTHDT rpt = new rpt_KTHDT();
                RPUtility.ShowRp(rpt, dtin, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                    srv.DbPassSerVer());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }
    }
}
