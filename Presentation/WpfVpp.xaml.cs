using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDvut.xaml
    /// </summary>
    public partial class WpfVpp : Window
    {
        public WpfVpp()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ToolBll bll = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        DataTable dtSua = new DataTable();
        DataTable dtXoa = new DataTable();
        DataTable dtxa = new DataTable();
        string Thumuc = "C:\\Saoke";
        private bool sua = false;


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateMonthsAndYears();

            cls.ClsConnect();
            string sql = "select PO_MA,PO_TEN from DMPOS where PO_MA=" + "'" + BienBll.NdMadv.Trim()+"'";
            //var sql = BienBll.NdCapbc.Trim() == "1" ? string.Format("select PO_MA,PO_TEN from DMPOS where PO_MA='{0}'", BienBll.NdMadv.Trim()) : "select PO_MA,PO_TEN from DMPOS where right(PO_MA,2)<>'00'";
            var dtpos = cls.LoadDataText(sql);
            for (int i = 0; i < dtpos.Rows.Count; i++)
            {
                CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
            }
            CboPos.SelectedIndex = 0;
            CboPB.Items.Clear();
            if (BienBll.NdMadv.Trim()==BienBll.MainPos.Trim())
                 dtxa = cls.LoadDataText("select * from DM_PHONGBAN where MA in ('17','18','19','20','21','22') order by MA");
            else dtxa = cls.LoadDataText("select * from DM_PHONGBAN where MA in ('29','30','31') order by MA");
            for (int i = 0; i < dtxa.Rows.Count; i++)
            {
                CboPB.Items.Add(dtxa.Rows[i][0].ToString().Trim() + " | " + dtxa.Rows[i][1]);
            }
            cls.DongKetNoi();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
            //MessageBox.Show(bll.Left(comboBoxMonth.SelectedValue.ToString().Trim(),2));
            //MessageBox.Show(comboBoxYear.SelectedValue.ToString().Trim());
        }

        private void btnIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                string str = "select * from LUUVPP where MAPOS='"+ bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) 
                    + "' and PHONGTO='"+ bll.Left(CboPB.SelectedValue.ToString().Trim(), 2) + "' and THANG = '" 
                    + bll.Left(comboBoxMonth.SelectedValue.ToString().Trim(), 2) + "' and NAM='" + comboBoxYear.SelectedValue.ToString().Trim() + "' and SOLUONG>0";
                //MessageBox.Show(str);
                var dtin = cls.LoadDataText(str);
                if (dtin.Rows.Count > 0)
                {
                    rpt_Vpp01 rpt = new rpt_Vpp01();
                    RPUtility.ShowRp(rpt, dtin, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                        srv.DbPassSerVer());
                } else MessageBox.Show("Không có bản ghi nào !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
            
        }

        private void LblGetData_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                
                cls.ClsConnect();
                int thamso = 4;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@MaPos";
                giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                bien[1] = "@Thang";
                giatri[1] = bll.Left(comboBoxMonth.SelectedValue.ToString().Trim(), 2);
                bien[2] = "@PhongTo";
                giatri[2] = bll.Left(CboPB.SelectedValue.ToString().Trim(), 2);
                bien[3] = "@Nam";
                giatri[3] = comboBoxYear.SelectedValue.ToString().Trim();

                dt = cls.LoadDataProcPara("usp_Vpp01", bien, giatri, thamso);
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

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sua == true)
                {
                    cls.ClsConnect();
                    foreach (DataRow dr in dtSua.Rows)
                    {
                        string upd = "update LUUVPP set SOLUONG=" + dr["SOLUONG"] + " where MAPOS='" +
                                     bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "' and PHONGTO='" +
                                     bll.Left(CboPB.SelectedValue.ToString().Trim(), 2) + "' and THANG = '" +
                                     bll.Left(comboBoxMonth.SelectedValue.ToString().Trim(), 2) + "' and NAM='" +
                                     comboBoxYear.SelectedValue.ToString().Trim() + "' and MA='" + dr["MA"] + "'";
                        cls.UpdateDataText(upd);
                    }
                    MessageBox.Show("Cập nhật thành công !", "Thông báo", MessageBoxButton.OK,MessageBoxImage.Warning);
                    sua = false;
                }
                else
                {
                    bll.TaoThuMuc(Thumuc);
                    dtNew = dt.Clone();
                    foreach (DataRow dr in dt.Rows)
                    {
                        if ((bool) dr[0] == true)
                        {
                            dtNew.ImportRow(dr);
                        }
                    }

                    //dtNew = dt.GetChanges();
                    if (dtNew == null || dtNew.Rows.Count == 0)
                    {
                        MessageBox.Show("Chưa có thay đổi ngày nào !", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }
                    else
                    {
                        cls.ClsConnect();
                        foreach (DataRow dr in dtNew.Rows)
                        {
                            string chk = "select * from LUUVPP where MAPOS='" +
                                         bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "' and PHONGTO='" +
                                         bll.Left(CboPB.SelectedValue.ToString().Trim(), 2) + "' and THANG = '" +
                                         bll.Left(comboBoxMonth.SelectedValue.ToString().Trim(), 2) + "' and NAM='" +
                                         comboBoxYear.SelectedValue.ToString().Trim() + "' and MA='" + dr["MA"] + "'";
                            var dtchk = cls.LoadDataText(chk);
                            if (dtchk.Rows.Count > 0)
                                MessageBox.Show(dr["TEN"] + " đã tồn tại !", "Thông báo", MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                            else
                            {

                                string strin =
                                    "insert into LUUVPP (CHON,MAPOS,TENPOS,PHONGTO,TENPB,THANG,NAM,NGAY,MA,TEN,DONVI,SOLUONG,QUYCACH,ND_MA,ND_TEN) " +
                                    "VALUES (" + 0 + ",'" + dr["MAPOS"] + "',N'" + dr["TENPOS"] + "','" + dr["PHONGTO"] +
                                    "',N'" + dr["TENPB"] + "','" + dr["THANG"] + "','" + dr["NAM"] + "','" +
                                    DateTime.Now.ToString("yyyy-MM-dd") + "','" + dr["MA"] + "',N'" +
                                    dr["TEN"] + "',N'" + dr["DONVI"] + "','" + dr["SOLUONG"] + "',N'"+ dr["QUYCACH"] + "',N'"+BienBll.Ndma+"',N'"+BienBll.NdTen+"')";
                                cls.UpdateDataText(strin);
                            }
                        }
                        MessageBox.Show("OK chọn nút in !", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();

        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sua = true;
                cls.ClsConnect();
                string str = "select * from LUUVPP where MAPOS='" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "' and PHONGTO='" 
                    + bll.Left(CboPB.SelectedValue.ToString().Trim(), 2) + "' and THANG = '" 
                    + bll.Left(comboBoxMonth.SelectedValue.ToString().Trim(), 2) + "' and NAM='" + comboBoxYear.SelectedValue.ToString().Trim()+ "'";
                dtSua = cls.LoadDataText(str);
                if (dtSua.Rows.Count > 0)
                {
                    dgvSource.ItemsSource = dtSua.DefaultView;
                }
                else
                {
                    MessageBox.Show("Chưa có số liệu", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            dtXoa = dtSua.Clone();
            foreach (DataRow dr in dtSua.Rows)
            {
                if ((bool)dr[0] == true)
                {
                    dtXoa.ImportRow(dr);
                }
            }
            cls.ClsConnect();
            foreach (DataRow dr in dtXoa.Rows)
            {
                string upd = "delete from LUUVPP where MAPOS='" +
                             bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "' and PHONGTO='" +
                             bll.Left(CboPB.SelectedValue.ToString().Trim(), 2) + "' and THANG = '" +
                             bll.Left(comboBoxMonth.SelectedValue.ToString().Trim(), 2) + "' and NAM='" +
                             comboBoxYear.SelectedValue.ToString().Trim() + "' and MA='" + dr["MA"] + "'";
                cls.UpdateDataText(upd);
            }
            cls.DongKetNoi();
            MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK,MessageBoxImage.Information);
        }

        private void PopulateMonthsAndYears()
        {
            //comboBoxMonth.ItemsSource = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames.Take(12).ToList();
            //comboBoxMonth.SelectedItem = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames[DateTime.Now.AddMonths(-1).Month - 1];
            for (int x = 0; x < 12; x++)
            {
                comboBoxMonth.Items.Add
                (
                   (x + 1).ToString("00")
                   + " "
                   + CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.GetValue(x)
                 );
            }
            comboBoxMonth.SelectedIndex = 0;
            //comboBoxYear.ItemsSource = Enumerable.Range(2019, DateTime.Now.Year - 2010 + 5).ToList();
            comboBoxYear.ItemsSource = Enumerable.Range(2019,5).ToList();
            comboBoxYear.SelectedItem = DateTime.Now.Year;
            comboBoxYear.SelectedIndex = 0;
        }

 
    }
}
