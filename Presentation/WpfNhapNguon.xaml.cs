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
    public partial class WpfNhapNguon
    {
        public WpfNhapNguon()
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
                        string strsql = "update NGUON_UT set A01=" + dr["A01"] + ",A02=" + dr["A02"] + ",A03=" +
                                        dr["A03"] + ",A04=" + dr["A04"] + ",A06=" + dr["A06"] + ",A07=" + dr["A07"]
                                        + ",A08=" + dr["A08"] + ",A09=" + dr["A09"] + ",A10=" + dr["A10"] + ",A11=" +
                                        dr["A11"] + ",A15=" + dr["A15"] + ",A16=" + dr["A16"] + ",A17=" + dr["A17"] +
                                        ",A18=" + dr["A18"] + ",A19=" + dr["A19"] + ",B03T=" + dr["B03T"] 
                                        + ",B03H=" +dr["B03H"] + ",B19T=" + dr["B19T"]+ ",B19H=" + dr["B19H"]+",TGTK=" + dr["TGTK"]
                                        + ",A03CS=" + dr["A03CS"] + ",A07_33=" + dr["A07_33"] + ",A20=" + dr["A20"]
                                        + " where NG_MATO='" + dr["NG_MATO"].ToString().Trim() + "'";
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
                        bien[0] = "@MaXa";
                        giatri[0] = s.Left(CboXa.SelectedValue.ToString().Trim(), 6);
                        var dtin = cls.LoadDataProcPara("usp_InNguon_UT", bien, giatri, thamso);
                        rpt_Nguon_UT rpt = new rpt_Nguon_UT();
                        RPUtility.ShowRp(rpt, dtin, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                        rpt_Nguon_UT_KHB rptkhb = new rpt_Nguon_UT_KHB();
                        RPUtility.ShowRp(rptkhb, dtin, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        //int thamso1 = 1;
                        //string[] bien1 = new string[thamso1];
                        //object[] giatri1 = new object[thamso1];
                        //bien1[0] = "@MaPos";
                        //giatri1[0] = s.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                        //var dtth = cls.LoadDataProcPara("usp_InNguon_TH", bien1, giatri1, thamso1);
                        //rpt_Nguon_UT rpt = new rpt_Nguon_UT();
                        //RPUtility.ShowRp(rpt, dtth, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());

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
                var sql = "select MA,TEN from DMXA where PGD_QL='" + BienBll.NdMadv + "' and right(MA,2)<>'00' order by MA";
                var dtxa = cls.LoadDataText(sql);
                for (var i = 0; i < dtxa.Rows.Count; i++)
                {
                    CboXa.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                }
                //CboPos.SelectedIndex = BienBll.NdCapbc.Trim() == "1" ? 0 : 5;
                //var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                //dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());

                //DtpDenNgay.SelectedDate = DateTime.Parse(DtpNgay.SelectedDate.Value.ToString("yyyy-MM") + "-" + DateTime.DaysInMonth(DtpNgay.SelectedDate.Value.Year, DtpNgay.SelectedDate.Value.Month).ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }

        private void CboXa_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                var sql = "select CHON,NG_MATO,NG_TENTT,A01,A02,A03,A04,A06,A07,A08,A09,A10,A11,A15,A16,A17,A18,A19,B03T,B03H,B19T,B19H,TGTK,A03CS,A07_33,A20 "
                    +"from NGUON_UT where NG_MAXA='" + s.Left(CboXa.SelectedValue.ToString(), 6) + "' and NG_TRANGTHAI='A' order by NG_MATO";
                dt = cls.LoadDataText(sql);
                if (dt.Rows.Count > 0)
                {
                    dgvData.ItemsSource = dt.DefaultView;
                }
                else MessageBox.Show("Không có tổ nào !", "Mess",MessageBoxButton.OK,MessageBoxImage.Warning);
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
                MessageBox.Show("Chưa chọn tổ nào ", "Mess",MessageBoxButton.OK,MessageBoxImage.Warning);
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

        private void LblManual_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            string strsql="insert into NGUON_UT select CONVERT(varchar(10),GETDATE(),126) NGAY,TO_MACN NG_MACN,TO_MAPGD NG_MAPGD ,LEFT(TO_MADP,6) NG_MAXA "
              +",TO_MATO NG_MATO,TO_TENTT NG_TENTT ,0 A01 ,0 A02 ,0 A03,0 A04,0 A06,0 A07,0 A08,0 A09,0 A10 "
              +",0 A11,0 A15,0 A16,0 A17,0 A18,0 A19,0 B01,0 B02,0 B03T,0 B04,0 B06,0 B07,0 B08,0 B09,0 B10 "
              +",0 B11,0 B15,0 B16,0 B17,0 B18 ,0 B19T,TRANGTHAI NG_TRANGTHAI,0 B03H ,0 B19H ,0 TGTK ,0 CHON,0 A03CS,0 A07_33,0 A20 "
              +" from HSTO a where a.TO_MATO not in (select b.NG_MATO from NGUON_UT b where a.TO_MATO=b.NG_MATO)";
            //MessageBox.Show(strsql);
            try
            {
                cls.ClsConnect();
                cls.UpdateDataText(strsql);
                MessageBox.Show("Update OK", "Mess", MessageBoxButton.OK, MessageBoxImage.None);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Mess",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }

        private void btnToDong_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                cls.UpdateDataText("update a set a.NG_TRANGTHAI=b.TRANGTHAI,a.NG_TENTT=b.TO_TENTT from NGUON_UT a,HSTO b where a.NG_MATO=b.TO_MATO");                
                cls.DongKetNoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error "+ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            MessageBox.Show("OK ", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
