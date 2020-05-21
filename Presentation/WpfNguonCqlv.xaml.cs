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
    public partial class WpfNguonCqlv
    {
        public WpfNguonCqlv()
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
                
                //DataTable dtpos;
                //var sql = BienBll.NdCapbc.Trim() == "1" ? string.Format("select PO_MA,PO_TEN from DMPOS where PO_MA='{0}'", BienBll.NdMadv.Trim()) : "select PO_MA,PO_TEN from DMPOS";
                var sql = "select PO_MA,PO_TEN from DMPOS where PO_MA='"+BienBll.NdMadv+"'";
                var dtpos = cls.LoadDataText(sql);
                for (var i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                //CboPos.SelectedIndex = BienBll.NdCapbc.Trim() == "1" ? 0 : 5;
                var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());

                //DtpDenNgay.SelectedDate = DateTime.Parse(DtpNgay.SelectedDate.Value.ToString("yyyy-MM") + "-" + DateTime.DaysInMonth(DtpNgay.SelectedDate.Value.Year, DtpNgay.SelectedDate.Value.Month).ToString());
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
                        //MessageBox.Show(dr["NG_MATO"].ToString()+"      "+dr["A01"].ToString());
                        string strsql = "update NGUON_CQLV set C11=" + dr["C11"] + ",C12=" + dr["C12"] + ",C13=" +
                                        dr["C13"] + ",C14=" + dr["C14"] + ",C15=" + dr["C15"] + ",C16=" + dr["C16"]
                                        + ",C17=" + dr["C17"] + ",C18=" + dr["C18"] + ",C19=" + dr["C19"]
                                        + ",C20=" + dr["C20"] + ",C21=" + dr["C21"]
                                        + " where NG_MAXA='" + dr["NG_MAXA"].ToString().Trim() + "'";
                        cls.UpdateDataText(strsql);
                    }
                    MessageBox.Show("Update Ok", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (
                        MessageBox.Show("Có muốn in số liệu ra không ?", "Question", MessageBoxButton.YesNo,
                            MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        
                        cls.ClsConnect();
                        int thamso = 1;
                        string[] bien = new string[thamso];
                        object[] giatri = new object[thamso];
                        bien[0] = "@MaPos";
                        giatri[0] = s.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                        var dtin = cls.LoadDataProcPara("usp_InNguon_Cqlv", bien, giatri, thamso);
                        rpt_Nguon_Cqlv rpt = new rpt_Nguon_Cqlv();
                        RPUtility.ShowRp(rpt, dtin, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                    }
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

        private void CboPos_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                var sql = "select CHON,NG_MAXA,NG_TENXA,C11,C12,C13,C14,C15,C16,C17,C18,C19,C20,C21 from NGUON_CQLV where NG_MAPGD='" + s.Left(CboPos.SelectedValue.ToString(), 6) + "' order by NG_MAXA";
                dt = cls.LoadDataText(sql);
                if (dt.Rows.Count > 0) dgvData.ItemsSource = dt.DefaultView;
                else MessageBox.Show("Không có xã nào !", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
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
    }
}
