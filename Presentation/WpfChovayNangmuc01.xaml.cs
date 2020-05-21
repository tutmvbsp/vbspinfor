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
    public partial class WpfChovayNangmuc01 : Window
    {
        public WpfChovayNangmuc01()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ToolBll str = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        string Thumuc = "C:\\Saoke";
       // private string strsql = "";
        private string FileName = "";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
            dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
            var firstDayOfMonth = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month, 1);
            var lastDay = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month, 25);
            dtpTuNgay.SelectedDate = firstDayOfMonth;
            dtpDenNgay.SelectedDate = lastDay;
            //string sql = "select PO_MA,PO_TEN from DMPOS where PO_MACN=" + "'" + BienBll.MainPos + "'" + " order by PO_MA";
            var sql = BienBll.NdCapbc.Trim() == "1" ? string.Format("select PO_MA,PO_TEN from DMPOS where PO_MA='{0}'", BienBll.NdMadv.Trim()) : "select PO_MA,PO_TEN from DMPOS where right(PO_MA,2)<>'00'";
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

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /*
                str.TaoThuMuc(Thumuc);
                dtNew = dt.Clone();
                foreach (DataRow dr in dt.Rows)
                {
                    if ((bool)dr[0] == true)
                    {
                        dtNew.ImportRow(dr);
                    }
                }
                */
                dtNew = dt.GetChanges();
                if (dtNew == null || dtNew.Rows.Count == 0) MessageBox.Show("Chưa có thay đổi ngày nào !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    cls.ClsConnect();
                    foreach (DataRow dr in dtNew.Rows)
                    {
                        string upd = "update LUU_CHOVAYNANGMUC set CHON='1', COT7=N'"+dr["COT7"]+"',COT8 = '"+dr["COT8"]+"'" +
                                     ", COT9='" + dr["COT9"] + "', COT10 = '"+dr["COT10"]+"', COT11='" + dr["COT11"] + "'" +
                                     ", GHICHU = N'"+dr["GHICHU"]+"' where KU_SOKU='" + dr["KU_SOKU"]+ "'";
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

                cls.ClsConnect();
                int thamso = 5;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@MaPos";
                giatri[0] = str.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                bien[1] = "@Ngay";
                if (dtpNgay.SelectedDate != null)
                {
                    giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    bien[2] = "@Maxa";
                    giatri[2] = str.Left(CboXa.SelectedValue.ToString().Trim(), 6);
                    bien[3] = "@TuNgay";
                    if (dtpTuNgay.SelectedDate != null)
                    {
                        giatri[3] = dtpTuNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                        bien[4] = "@DenNgay";
                        if (dtpDenNgay.SelectedDate != null)
                            giatri[4] = dtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    }
                }

                dt = cls.LoadDataProcPara("usp_ChovayNangmuc01", bien, giatri, thamso);
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
        private void LblGetSua_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

                cls.ClsConnect();
                string strsql =
                    "select a.* from luu_chovaynangmuc a where a.maxa='"+ str.Left(CboXa.SelectedValue.ToString().Trim(), 6) 
                    + "' and a.chon=1 and a.COT4 between '" + dtpTuNgay.SelectedDate.Value.ToString("yyyy-MM-dd") 
                    + "' and  '"+ dtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' order by a.mato,a.kh_makh";
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
        private void Lblin01kt_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                int thamso = 3;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Ngay";
                if (dtpNgay.SelectedDate != null)
                {
                    giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    bien[1] = "@TuNgay";
                    if (dtpTuNgay.SelectedDate != null)
                    {
                        giatri[1] = dtpTuNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                        bien[2] = "@DenNgay";
                        if (dtpDenNgay.SelectedDate != null)
                            giatri[2] = dtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    }
                }
                var dtin = cls.LoadDataProcPara("usp_ChovayNangmuc04", bien, giatri, thamso);
                if (dtin.Rows.Count > 0)
                {
                    rpt_ChovayNangmuc01a rpt = new rpt_ChovayNangmuc01a();
                    RPUtility.ShowRp(rpt, dtin, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                    FileName = Thumuc + "\\" + str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_Mẫu 01_KT " + dtpTuNgay.SelectedDate.Value.ToString("ddMMyyyy") + "_Đến ngày_" + dtpDenNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                    //MessageBox.Show(FileName);
                    str.ExportToExcel(dtin, FileName);
                    //str.WriteDataTableToExcel(dtin,"M01",FileName,"true");
                    ////FileStream fs = new FileStream(FileName, FileMode.Create);
                    ////StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
                    ////str.ToCSV(dtin, sw, true);
                    MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    str.OpenExcel(FileName);

                }
                else MessageBox.Show("Không có món vay nào !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }
        private void Lblin02kt_OnMouseDown(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                int thamso = 3;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Ngay";
                if (dtpNgay.SelectedDate != null)
                {
                    giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    bien[1] = "@TuNgay";
                    if (dtpTuNgay.SelectedDate != null)
                    {
                        giatri[1] = dtpTuNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                        bien[2] = "@DenNgay";
                        if (dtpDenNgay.SelectedDate != null)
                            giatri[2] = dtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    }
                }

                var dt02 = cls.LoadDataProcPara("usp_ChovayNangmuc02", bien, giatri, thamso);

                rpt_ChovayNangmuc02 rpt1 = new rpt_ChovayNangmuc02();
                RPUtility.ShowRp(rpt1, dt02, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                //FileName = Thumuc + "\\" + str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_Mẫu 02_KT " + dtpTuNgay.SelectedDate.Value.ToString("ddMMyyyy") + "_Đến ngày_" + dtpDenNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                ////str.ExportToExcel(dt02, FileName);
                //str.ExportDTToExcel(dt02, FileName);
                //MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                //str.OpenExcel(FileName);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : "+ex.Message,"Thông báo", MessageBoxButton.OK,MessageBoxImage.Error);
            }

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
        private void DatePicker_SelectedDateChanged(object sender, RoutedEventArgs e)
        {
            if (dtpTuNgay.SelectedDate != null)
            {
                var lastDay = new DateTime(dtpTuNgay.SelectedDate.Value.Year, dtpTuNgay.SelectedDate.Value.AddMonths(1).Month, 25);
                dtpDenNgay.SelectedDate = lastDay;
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
