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
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfLaiTon.xaml
    /// </summary>
    public partial class WpfTSCC03 : Window
    {
        public WpfTSCC03()
        {
            InitializeComponent();
        }

        private ToolBll s = new ToolBll();
        HardwareInfo infor= new HardwareInfo();
        ServerInfor srv = new ServerInfor();
        private readonly ClsServer cls = new ClsServer();
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        string Thumuc = "C:\\Saoke";
        private string FileName = "";
        string strpos = "";
        string strphong = "";
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                dtpNgay.SelectedDate = DateTime.Now;
                //var dtng = cls.LoadDataText("select MAX(NGAYBC) as NGMAX from QT_TSCC");
                //dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
                //DateTime lastMonth = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month, DateTime.DaysInMonth(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month));
                //if (BienBll.NdMadv == BienBll.MainPos)
                //{
                //    strpos = "select PO_MA MA,PO_TEN TEN from DMPOS  order by PO_MA";
                //    strphong = "select * from DM_PHONGBAN order by MA";
                //}
                //else
                //{
                //    strpos = "select PO_MA MA,PO_TEN TEN from DMPOS where PO_MA='"+CboPos.SelectedValue.ToString().Trim()+"'";
                //    strphong = "select * from DM_PHONGBAN where ma not in ('17','18','19','20','21','22','34')";
                //}
                strpos = "select PO_MA MA,PO_TEN TEN from DMPOS where PO_MA='" + BienBll.NdMadv + "'";
                if (BienBll.NdMadv == BienBll.MainPos)
                    strphong = "select * from DM_PHONGBAN where ma in ('17','18','19','20','21','22','34')";
                else strphong = "select * from DM_PHONGBAN where ma not in ('17','18','19','20','21','22','34')";
                //strphong = "select * from DM_PHONGBAN where MA='" + BienBll.PhongBan + "'";
                var dtpos = cls.LoadDataText(strpos);
                CboPos.ItemsSource = dtpos.DefaultView;
                CboPos.DisplayMemberPath = "TEN";
                CboPos.SelectedValuePath = "MA";
                var dtphong = cls.LoadDataText(strphong);
                CboPhong.ItemsSource = dtphong.DefaultView;
                CboPhong.DisplayMemberPath = "TEN";
                CboPhong.SelectedValuePath = "MA";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }



        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
     
        private void ShowGrid_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                DateTime newdate = new DateTime(dtpNgay.SelectedDate.Value.AddYears(-1).Year + 1, 1, 1);
                DateTime LastDayYear = new DateTime(dtpNgay.SelectedDate.Value.Year, 12, 31);
                cls.ClsConnect();
                    string sqlload =
                        "select CAST (0 AS bit) AS  CHON,'" + ng+ "' NGAY,a.*,b.PO_TEN,c.LYDO,c.DENGHI from LUU_TSCC a left join DMPOS b on a.POS_CD=b.PO_MA " +
                        " left join TSCC_HIS c on a.MA_TS=c.MA_TS and c.NG_DG_SC='"+ng+"' where a.LOAI_TS_CHITIET='TI1' and a.TRANGTHAI='A' and a.POS_CD='" + CboPos.SelectedValue +"' and right(a.MAPHONG,2)='" + s.Right(CboPhong.SelectedValue.ToString().Trim(), 2) + "' order by a.LOAI_TS_CHITIET,a.MA_NHANHIEU_TS,a.MA_TS";
                    //MessageBox.Show(sqlload);
                    dt = cls.LoadDataText(sqlload);
                    if (dt.Rows.Count > 0)
                    {
                        dgvData.ItemsSource = dt.DefaultView;
                    }
                    else
                        MessageBox.Show("Không có dữ liệu !", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Hist_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                DateTime newdate = new DateTime(dtpNgay.SelectedDate.Value.AddYears(-1).Year + 1, 1, 1);
                DateTime LastDayYear = new DateTime(dtpNgay.SelectedDate.Value.Year, 12, 31);
                cls.ClsConnect();
                string sqlload =
                    "select a.*,b.PO_TEN from TSCC_HIS a left join DMPOS b on a.POS_CD=b.PO_MA where a.MA_TS='" + label.Content+"' and a.MNV='0' order by a.NG_DG_SC";
                //MessageBox.Show(sqlload);
                var dthis = cls.LoadDataText(sqlload);
                if (dthis.Rows.Count > 0)
                {
                    /*
                    FileName = Thumuc + "\\" + label.Content + "_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                    s.ExportToExcel(dthis, FileName);
                    s.OpenExcel(FileName);
                    */
                    rpt_TSCC_BaoTri rpt = new rpt_TSCC_BaoTri();
                    RPUtility.ShowRp(rpt, dthis, this, srv.DbSourceSerVer(), srv.DbNameSerVer(),
                        srv.DbUserSerVer(),
                        srv.DbPassSerVer());

                }
                else
                    MessageBox.Show("Không có dữ liệu !", "Thông báo", MessageBoxButton.OK,
                        MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime newdate = new DateTime(dtpNgay.SelectedDate.Value.AddYears(-1).Year + 1, 1, 1);
                dtNew = dt.Clone();
                foreach (DataRow dr in dt.Rows)
                {
                    if ((bool)dr[0] == true)
                    {
                        dtNew.ImportRow(dr);
                    }
                }

                //dtNew = dt.GetChanges();
                if (dtNew == null || dtNew.Rows.Count == 0)
                {
                    MessageBox.Show("Chưa chọn thiết bị nào !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    if (dtNew.Rows.Count >= 2)
                        MessageBox.Show("Chỉ chọn 1 thiết bị thôi !", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    else
                    {


                        int thamso = 4;
                        string[] bien = new string[thamso];
                        object[] giatri = new object[thamso];
                        bien[0] = "@Ngay";
                        giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                        bien[1] = "@MaTs";
                        giatri[1] = dtNew.Rows[0]["MA_TS"].ToString();
                        bien[2] = "@Lydo";
                        giatri[2] = txtLyDo.Text;
                        bien[3] = "@DeNghi";
                        giatri[3] = txtDeNghi.Text;
                        var dtin = cls.LoadDataProcPara("usp_TSCC_HIS", bien, giatri, thamso);
                        if (dtin.Rows.Count > 0)
                        {
                            rpt_TSCC_Tr rpt = new rpt_TSCC_Tr();
                            RPUtility.ShowRp(rpt, dtin, this, srv.DbSourceSerVer(), srv.DbNameSerVer(),
                                srv.DbUserSerVer(),
                                srv.DbPassSerVer());
                        }
                        else
                            MessageBox.Show("Không có dữ liệu !", "Thông báo", MessageBoxButton.OK,
                                MessageBoxImage.Information);
                    }
                }




            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                DataRowView dr1 = (DataRowView)dgvData.SelectedItems[0];
                string strup= "update TSCC_HIS set LYDO = N'"+txtLyDo.Text+"',DENGHI = N'"+txtDeNghi.Text+"' WHERE MA_TS = '"+ dr1["MA_TS"] + "' and NG_DG_SC = '"+ng+"' and MNV = '0'";
                cls.UpdateDataText(strup);
                MessageBox.Show("Sửa dữ liệu thành công, chọn OK để in!", "Thông báo", MessageBoxButton.OK,MessageBoxImage.Information);
  
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        private void CboPhong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                string pos = CboPos.SelectedValue.ToString().Trim();
                string strsql = "select distinct LOAI_TS MA,TEN_LOAI_TS TEN from QT_TSCC where NGAYBC='" + ng +
                                "' and POS_CD='" + pos + "' and RIGHT(MAPHONG,2)='"+CboPhong.SelectedValue.ToString().Trim()+"' order by LOAI_TS";
                var dtloaits = cls.LoadDataText(strsql);
                //CboLoaiTS.ItemsSource = dtloaits.DefaultView;
                //CboLoaiTS.DisplayMemberPath = "TEN";
                //CboLoaiTS.SelectedValuePath = "MA";
                //CboLoaiTS.SelectedIndex = 1;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();

        }

        private void dgvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    ////DataRow dtr = dt.Rows[0];
                    ////DataRow dr = (DataRow) dgvData.SelectedItems[0];
                    DataRowView dr = (DataRowView)dgvData.SelectedItems[0];
                    label.Content = dr["MA_TS"].ToString();
                    txtLyDo.Text=dr["LYDO"].ToString();
                    txtDeNghi.Text = dr["DENGHI"].ToString();
                }
                else
                {
                    MessageBox.Show("Không có bản ghi nào !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

}
