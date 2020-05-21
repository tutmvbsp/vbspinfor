using System;
using System.Data;
using System.Security.AccessControl;
using System.Windows;
using System.Windows.Input;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfLaiTon.xaml
    /// </summary>
    public partial class WpfTbGhn
    {
        public WpfTbGhn()
        {
            InitializeComponent();
        }

        private ToolBll s = new ToolBll();
        private readonly ClsServer cls = new ClsServer();
        private DataTable dt = new DataTable();
        private DataTable dtNew = new DataTable();
        private ServerInfor srv = new ServerInfor();
        private string strsql = "";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();

                //DataTable dtpos;
                var sql = BienBll.NdCapbc.Trim() == "1"
                    ? string.Format("select PO_MA,PO_TEN from DMPOS where PO_MA='{0}'", BienBll.NdMadv.Trim())
                    : "select PO_MA,PO_TEN from DMPOS";
                //var sql = "select PO_MA,PO_TEN from DMPOS where PO_MA='"+BienBll.NdMadv+"'";
                var dtpos = cls.LoadDataText(sql);
                for (var i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                //CboPos.SelectedIndex = BienBll.NdCapbc.Trim() == "1" ? 0 : 5;
                var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
                dtpDenNgay.SelectedDate = dtpNgay.SelectedDate.Value.AddMonths(1);
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
                bien[2] = "@DenNgay";
                if (dtpDenNgay.SelectedDate != null) giatri[2] = dtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                bien[3] = "@MaTo";
                giatri[3] = s.Left(CboTo.SelectedValue.ToString().Trim(), 7);
                dt = cls.LoadDataProcPara("usp_TbNdh", bien, giatri, thamso);
                if (dt.Rows.Count > 0) dgvData.ItemsSource = dt.DefaultView;
                else
                    MessageBox.Show("Không có khách hàng nào đến hạn !", "Mess", MessageBoxButton.OK,
                        MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }

        private void BtnIn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                strsql = "select * from TBNDH where KU_MATO='" + s.Left(CboTo.SelectedValue.ToString().Trim(), 7)
                         + "' and NGAY='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and DENNGAY='"
                         + dtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and CHAPNHAN=1 or TUCHOI=1";
                cls.ClsConnect();
                var dtin = cls.LoadDataText(strsql);
                if (dtin.Rows.Count > 0)
                {
                    strsql = "select * from TBNDH where KU_MATO='" + s.Left(CboTo.SelectedValue.ToString().Trim(), 7)
                             + "' and NGAY='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and DENNGAY='"
                             + dtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and CHAPNHAN=1";
                    var dtin1 = cls.LoadDataText(strsql);
                    rpt_TBNDH rpt = new rpt_TBNDH();
                    RPUtility.ShowRp(rpt, dtin1, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                        srv.DbPassSerVer());
                    rpt_TBNDH2 rpt1 = new rpt_TBNDH2();
                    RPUtility.ShowRp(rpt1, dtin1, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                        srv.DbPassSerVer());
                    strsql = "select * from TBNDH where KU_MATO='" + s.Left(CboTo.SelectedValue.ToString().Trim(), 7)
                             + "' and NGAY='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and DENNGAY='"
                             + dtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and TUCHOI=1";
                    var dtin2 = cls.LoadDataText(strsql);
                    rpt_TBNDH1 rpt2 = new rpt_TBNDH1();
                    RPUtility.ShowRp(rpt2, dtin2, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                        srv.DbPassSerVer());

                }
                else
                    MessageBox.Show("Không có dữ liệu để in !", "Thông báo", MessageBoxButton.OK,
                        MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }
    

    //-------------------------------

        private void Update()
        {
            dtNew = dt.Clone();
            foreach (DataRow dr in dt.Rows)
            {
                if ((bool)dr["CHAPNHAN"] == true || (bool)dr["TUCHOI"] == true)
                {
                    dtNew.ImportRow(dr);
                }
            }
            if (dtNew == null || dtNew.Rows.Count == 0)
            {
                MessageBox.Show("Chưa chọn khách hàng nào !", "Thông báo",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
                /*
            else
            {
                MessageBox.Show("Cập nhật thành công !", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                //DataGrid.ItemsSource = dtNew.DefaultView;
                //rpt_01TG rpt = new rpt_01TG();
                //RPUtility.ShowRp(rpt, dtNew, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
            }
            // dtNew.RejectChanges();
            // dtNew = null;            
            */
        }

        private void Save()
        {
            try
            {
                if (dtNew.Rows.Count > 0)
                {
                    cls.ClsConnect();
                    foreach (DataRow dr in dtNew.Rows)
                    {
                        if ((bool)dr["CHAPNHAN"])

                            strsql = "update TBNDH set SOTHG_GH=" + dr["SOTHG_GH"] + ", CHAPNHAN=1"
                                     + " where KU_MATO='" + dr["KU_MATO"] + "' and NGAY='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and DENNGAY='" +
                                     dtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and SOKU='" + dr["SOKU"] + "'";
                        else
                            strsql = "update TBNDH set SOTHG_GH=" + dr["SOTHG_GH"] + ", TUCHOI=1"
                                     + " where KU_MATO='" + dr["KU_MATO"] + "' and NGAY='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and DENNGAY='" +
                                     dtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and SOKU='" + dr["SOKU"] + "'";
                        cls.UpdateDataText(strsql);
                    }
                    dtNew.Clear();
                    strsql =
                        "update tbndh set GH_DEN=DATEADD(MM,SOTHG_GH,NG_DHAN), GHDEN=convert(varchar(10),DATEADD(MM,SOTHG_GH,NG_DHAN),103)" 
                        +" where CHAPNHAN=1 and NGAY='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd")
                        + "' and DENNGAY='" + dtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and KU_MATO='" + s.Left(CboTo.SelectedValue.ToString().Trim(), 7) + "'";
                    cls.UpdateDataText(strsql);
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

        //-------------------------------

        private void CboPos_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                //MessageBox.Show(str.Left(cboPos.SelectedValue.ToString().Trim(),6));
                CboXa.Items.Clear();
                cls.ClsConnect();
                string sql = "select MA,TEN from DMXA where PGD_QL= " + "'" + s.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "'" + " order by MA";
                var dtxa = cls.LoadDataText(sql);
                for (int i = 0; i < dtxa.Rows.Count; i++)
                {
                    CboXa.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                }

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
                //MessageBox.Show(str.Left(cboXa.SelectedValue.ToString().Trim(), 8));
                CboTo.Items.Clear();
                cls.ClsConnect();
                if (dtpNgay.SelectedDate != null)
                {
                    string sql = "select TO_MATO,TO_TENTT from HSTO where TRANGTHAI='A' and Left(TO_MADP,6) = " + s.Left(CboXa.SelectedValue.ToString().Trim(), 6);
                    //MessageBox.Show(sql);
                    var dtto = cls.LoadDataText(sql);
                    for (int i = 0; i < dtto.Rows.Count; i++)
                    {
                        CboTo.Items.Add(dtto.Rows[i][0] + " | " + dtto.Rows[i][1]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }

        private void BtnUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Update();
                Save();
                MessageBox.Show("Cập nhật thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lổi "+ex.Message,"Thông báo",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
