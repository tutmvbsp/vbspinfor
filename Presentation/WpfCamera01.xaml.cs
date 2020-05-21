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
using DAL;
using BLL;
using System.Data;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDvut.xaml
    /// </summary>
    public partial class WpfCamera01 : Window
    {
        public WpfCamera01()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ToolBll str = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        //string Thumuc = "C:\\Saoke";
       // private string strsql = "";
        private string FileName = "";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
            dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
            var firstDayOfMonth = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month, 1);
            var lastDay = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month, 25);
            //string sql = "select PO_MA,PO_TEN from DMPOS where PO_MACN=" + "'" + BienBll.MainPos + "'" + " order by PO_MA";
            //var sql = BienBll.NdCapbc.Trim() == "1" ? string.Format("select PO_MA,PO_TEN from DMPOS where PO_MA='{0}'", BienBll.NdMadv.Trim()) : "select PO_MA,PO_TEN from DMPOS where right(PO_MA,2)<>'00'";
            var sql = BienBll.NdCapbc.Trim() == "02" ? string.Format("select PO_MA,PO_TEN from DMPOS where PO_MA='{0}'", BienBll.NdMadv.Trim()) : "select PO_MA,PO_TEN from DMPOS where right(PO_MA,2)<>'00'";
            var dtpos = cls.LoadDataText(sql);
            for (int i = 0; i < dtpos.Rows.Count; i++)
            {
                CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
            }
   
            cls.DongKetNoi();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                string strchk = "select * from LUU_CAMERA where MAXA='" +
                                str.Left(CboXa.SelectedValue.ToString().Trim(), 6) + "' and THANG='" +
                                dtpNgay.SelectedDate.Value.ToString("MM") + "' and NAM='" + dtpNgay.SelectedDate.Value.ToString("yyyy") + "' and NGUOI_NHAP='" + BienBll.Ndma.ToUpper() + "'";
                var dtchk = cls.LoadDataText(strchk);
                if (dtchk.Rows.Count > 0)
                {
                    string strdel = "delete from LUU_CAMERA where MAXA='" +
                                    str.Left(CboXa.SelectedValue.ToString().Trim(), 6) + "' and THANG='" +
                                    dtpNgay.SelectedDate.Value.ToString("MM") + "' and NAM='" +
                                    dtpNgay.SelectedDate.Value.ToString("yyyy") + "' and NGUOI_NHAP='" +
                                    BienBll.Ndma.ToUpper() + "'";
                    cls.UpdateDataText(strdel);
                    MessageBox.Show("Đã xóa thông tin xã : "+CboXa.SelectedValue, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else MessageBox.Show("Không thể xóa vì chưa nhập thông tin !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : "+ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                //str.TaoThuMuc(Thumuc);
                //dtNew = dt.Clone();
                //foreach (DataRow dr in dt.Rows)
                //{
                //    if ((bool)dr["D3"] == true)
                //    {
                //        dtNew.ImportRow(dr);
                //    }
                //}
                
                dtNew = dt.GetChanges();
                if (dtNew == null || dtNew.Rows.Count == 0) MessageBox.Show("Chưa có thay đổi ngày nào !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    cls.ClsConnect();
                    foreach (DataRow dr in dtNew.Rows)
                    {
                        string upd = "update LUU_CAMERA set CHON= '"+dr["CHON"]+"', D4=N'" + dr["D4"] + "' where MAXA='" +
                                     str.Left(CboXa.SelectedValue.ToString().Trim(), 6) + "' and THANG='" +
                                     dtpNgay.SelectedDate.Value.ToString("MM") + "' and NAM='" +
                                     dtpNgay.SelectedDate.Value.ToString("yyyy") + "' and THUTU='"+ dr["THUTU"] + "'";
                        cls.UpdateDataText(upd);
                    }
                    MessageBox.Show("Lưu thành công ! " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
            
        }
        private void btnIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                cls.ClsConnect();
                string upd1 = "update LUU_CAMERA set D3= (case when CHON=1 then 1 else 0 end)  where MAXA='" +
                                 str.Left(CboXa.SelectedValue.ToString().Trim(), 6) + "' and THANG='" +
                                 dtpNgay.SelectedDate.Value.ToString("MM") + "' and NAM='" +
                                 dtpNgay.SelectedDate.Value.ToString("yyyy") + "'";
                cls.UpdateDataText(upd1);
                int thamso = 2;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Maxa";
                giatri[0] = str.Left(CboXa.SelectedValue.ToString().Trim(), 6);
                bien[1] = "@Ngay";
                giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                var dtin = cls.LoadDataProcPara("usp_Camera", bien, giatri, thamso);
                if (dtin.Rows.Count > 0)
                {
                    rpt_Camera rpt = new rpt_Camera();
                    RPUtility.ShowRp(rpt, dtin, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                }
                else
                {
                    MessageBox.Show("Chưa có số liệu", "Thông báo");
                }

                    //MessageBox.Show("Lưu thành công ! " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();

        }
        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //MessageBox.Show(str.Left(cboPos.SelectedValue.ToString().Trim(),6));
                if (str.Left(CboPos.SelectedValue.ToString().Trim(), 6) != "003000")
                {
                    CboXa.Items.Clear();
                    cls.ClsConnect();
                    string sql = "select MA,TEN from DMXA where PGD_QL= " + "'" +
                                 str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "' and right(MA,2)<>'00' order by MA";
                    var dtxa = cls.LoadDataText(sql);
                    for (int i = 0; i < dtxa.Rows.Count; i++)
                    {
                        CboXa.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                    }
                }
                else
                {
                   // CboXa.Items.Add("003000 | Tất cả");
                    MessageBox.Show("Không chọn POS 003000", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
               // CboXa.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }


        private void LblGetData_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                dt = null;
                cls.ClsConnect();
                string strchk = "select * from LUU_CAMERA where MAXA='" +
                                str.Left(CboXa.SelectedValue.ToString().Trim(), 6) + "' and THANG='" +
                                dtpNgay.SelectedDate.Value.ToString("MM") + "' and NAM='" +dtpNgay.SelectedDate.Value.ToString("yyyy") + "' and NGUOI_NHAP='"+BienBll.Ndma.ToUpper()+"'";
                var dtchk =cls.LoadDataText(strchk);
                if (dtchk.Rows.Count == 0)
                {

                    string strsql =
                        "insert into LUU_CAMERA select '" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") +
                        "' NGAY,a.THUTU,a.TEN,a.D2,a.D3" +
                        ",a.D4,b.CVI_TXN_POINT_ID,b.TPI_DESC,b.TPI_DATE,'" + BienBll.Ndma.ToUpper() +
                        "' NGUOI_NHAP,'"+ dtpNgay.SelectedDate.Value.ToString("MM") + "'THANG" +
                        ",'"+ dtpNgay.SelectedDate.Value.ToString("yyyy") + "' NAM,b.CVI_COMMUNE_ID MAXA,a.CHON from CAMERA a, TXN_POINT_INFO b " +
                        "where b.CVI_COMMUNE_ID='" + str.Left(CboXa.SelectedValue.ToString().Trim(), 6) + "'";
                    cls.UpdateDataText(strsql);
                }
                dt = cls.LoadDataText(strchk);
                //rpt_kt740_01 rpt = new rpt_kt740_01();
                if (dt.Rows.Count > 0)
                {
                    dgvSource.ItemsSource = dt.DefaultView;
                    // string filename = "C:\\Tam\\" + str.Left(cboTo.SelectedValue.ToString().Trim(), 7) + ".xlsx";
                    // bll.WriteDataTableToExcel(dt, "Person Details", filename, "Details");
                    //dtNew = dt.GetChanges();
                }
                else
                {
                    MessageBox.Show("Chưa có số liệu", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();
        }
        private void LblGetSua_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

                cls.ClsConnect();
                string strsql =
                    "select a.* from luu_chovaynangmuc a where a.maxa='" +
                    str.Left(CboXa.SelectedValue.ToString().Trim(), 6);
                   
                dt = cls.LoadDataText(strsql);
                //rpt_kt740_01 rpt = new rpt_kt740_01();
                if (dt.Rows.Count > 0)
                {
                    dgvSource.ItemsSource = dt.DefaultView;
                    // rpt_SkeTo rpt = new rpt_SkeTo();
                    // RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                    // string filename = "C:\\Tam\\" + str.Left(cboTo.SelectedValue.ToString().Trim(), 7) + ".xlsx";
                    // bll.WriteDataTableToExcel(dt, "Person Details", filename, "Details");
                    //dtNew = dt.GetChanges();
                }
                else
                {
                    MessageBox.Show("Chưa có số liệu", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();
        }
  
        private void ChkAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach (DataRow dr in dt.Rows)
            {
                dr[0] = true;
            }
        }

        private void ChkAll_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (DataRow dr in dt.Rows)
            {
                dr[0] = false;
            }

        }
    }
}
