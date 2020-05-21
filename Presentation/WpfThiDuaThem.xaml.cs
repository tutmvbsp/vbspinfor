using System;
using System.Data;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Globalization;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDelete.xaml
    /// </summary>
    public partial class WpfThiDuaThem : Window
    {
        public WpfThiDuaThem()
        {
            InitializeComponent();
        }
        readonly ClsServer _cls = new ClsServer();
        ToolBll bll = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable _dt = new DataTable();
        private string str = "";
        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _cls.ClsConnect();
                foreach (DataRow dr in _dt.Rows)
                {
                    string strup = "update MAU_THIDUA set DIEM='" + dr["DIEM"] + "',TT='" + dr["TT"] + "', CHITIEU= N'" +dr["CHITIEU"] 
                        + "',INDAM='"+ dr["INDAM"] + "',CHONIN='"+ dr["CHONIN"] + "',NGHIENG='"+ dr["NGHIENG"] + "',CONG='"+ dr["CONG"] + "',INPUT='" + dr["INPUT"] + "',NHOM='" + dr["NHOM"]
                        + "' where NAM='" + comboBoxYear.SelectedValue + "' and QUY='" + CboQuy.SelectionBoxItem.ToString().Trim() 
                        + "' and DOT='" + bll.Left(RadCboDot.SelectedValue.ToString(), 1) + "' and STT="+ dr["STT"] 
                        + " and SUBTT="+dr["SUBTT"]+" and CHUYENDE='" + RadCboChDe.SelectedValue.ToString().Trim()+"'";
                    //MessageBox.Show(strup);
                   _cls.UpdateDataText(strup);
                }
                MessageBox.Show("Lưu thành công !","Mess",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _cls.DongKetNoi();
            }

        }
        private void Print_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _cls.ClsConnect();
                if (chkth.IsChecked==true)
                str = "select * from MAU_THIDUA where NAM='" +
                             comboBoxYear.SelectedValue + "' and QUY='" + CboQuy.SelectionBoxItem.ToString().Trim() +
                             "' and DOT='" + bll.Left(RadCboDot.SelectedValue.ToString(), 1) + "' and CHONIN='1' order by CHUYENDE,STT";
                else
                    str = "select * from MAU_THIDUA where NAM='" +
                                 comboBoxYear.SelectedValue + "' and QUY='" + CboQuy.SelectionBoxItem.ToString().Trim() +
                                 "' and DOT='" + bll.Left(RadCboDot.SelectedValue.ToString(), 1) + "'and CHUYENDE='" + RadCboChDe.SelectedValue.ToString().Trim() + "' and CHONIN='1' order by STT";

                var dtin = _cls.LoadDataText(str);
                if (chkth.IsChecked == true)
                {
                    rpt_ThiDua04 rpt = new rpt_ThiDua04();
                    RPUtility.ShowRp(rpt, dtin, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                        srv.DbPassSerVer());
                }
                else
                {
                    rpt_ThiDua rpt = new rpt_ThiDua();
                    RPUtility.ShowRp(rpt, dtin, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                        srv.DbPassSerVer());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _cls.DongKetNoi();
            }

        }



        private void WpfTdChamDiem_OnLoaded(object sender, RoutedEventArgs e)
        {
            PopulateMonthsAndYears();
            dtpNgay.SelectedDate = DateTime.Parse(DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month).ToString());
            try
            {

                _cls.ClsConnect();
                var dtpos = _cls.LoadDataText("select PO_MA,PO_TEN from DMPOS where PO_MA='"+BienBll.NdMadv.Trim()+"'");
                RadCboPos.ItemsSource = dtpos.DefaultView;
                RadCboPos.DisplayMemberPath = "PO_TEN";
                RadCboPos.SelectedValuePath = "PO_MA";
                RadCboPos.SelectedIndex = 0;
                var dtdot = _cls.LoadDataText("select * from DOT_THIDUA order by DOT");
                for (int i = 0; i < dtdot.Rows.Count; i++)
                {
                    RadCboDot.Items.Add(dtdot.Rows[i][0].ToString().Trim() + " | " + dtdot.Rows[i][1]);
                }
                RadCboDot.SelectedIndex = 0;
                var dtchde =
                    _cls.LoadDataText(
                        "select * from CHUYENDE order by MA");
                RadCboChDe.ItemsSource = dtchde.DefaultView;
                RadCboChDe.DisplayMemberPath = "TEN";
                RadCboChDe.SelectedValuePath = "MA";
                RadCboChDe.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            _cls.DongKetNoi();
        }

        private void LblManual_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string quy = ((int.Parse(dtpNgay.SelectedDate.Value.ToString("MM")) - 1) / 3 + 1).ToString();
                //string nam = dtpNgay.SelectedDate.Value.ToString("yyyy");
                _cls.ClsConnect();
                //* string strchk1 = "select * from MAU_THIDUA where NAM='" + comboBoxYear.SelectedValue + "' and QUY='2' and DOT='0' and CHUYENDE='"+RadCboChDe.SelectedValue.ToString().Trim()+"'"; *//
                string strchk1 = "select top 1 * from MAU_THIDUA where NAM='" + comboBoxYear.SelectedValue + "' and QUY='" +
                     CboQuy.SelectionBoxItem.ToString().Trim() + "' and DOT='" + bll.Left(RadCboDot.SelectedValue.ToString(), 1) + "'";

                var dtchk1 = _cls.LoadDataText(strchk1);
                if (dtchk1.Rows.Count == 0)
                {
                    MessageBox.Show("Chưa có chỉ tiêu tại MAU_THIDUA Quý : " + CboQuy.SelectionBoxItem.ToString().Trim() +", Năm " + comboBoxYear.SelectedValue + " Đợt : " + RadCboDot.SelectedValue, "Thông báo", MessageBoxButton.OK,MessageBoxImage.Information);
                    string strinsert;
                    if (quy != "1")
                    {
                         strinsert = "insert into MAU_THIDUA select STT, TT,'" +
                                           dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' NGAY, MAPOS ,'" +
                                           dtpNgay.SelectedDate.Value.ToString("yyyy") + "' NAM,'" +
                                           CboQuy.SelectionBoxItem.ToString().Trim() + "' QUY,'" +
                                           bll.Left(RadCboDot.SelectedValue.ToString(), 1) + "' DOT, CHITIEU, DIEM"
                                           +
                                           " , P0701, P0702, P0703, P0801, P0802, P0803, P0101, P0102, P0103, P0201, P0202 "
                                           +
                                           " , P0203, P0301, P0302, P0303, P0601, P0602, P0603, P0401, P0402, P0403, P0501"
                                           +
                                           " , P0502, P0503, PHONG, INDAM, TIEUDE, ND_MA, CHOT01, CHOT02, CHOT03, CHOT04, CHOT05"
                                           +
                                           " , CHOT06, CHOT07, CHOT08, '' NGNH01,'' NGNH02,'' NGNH03,'' NGNH04,'' NGNH05,'' NGNH06,'' NGNH07,'' NGNH08"
                                           +
                                           " , NGHIENG, CONG, TENCHUYENDE, TENPHONG, CHONIN, CHUYENDE, '' GHICHU,SUBTT,INPUT,NHOM from MAU_THIDUA where NAM = '" +comboBoxYear.SelectedValue + "' and QUY = '" +
                                           (int.Parse(CboQuy.SelectionBoxItem.ToString().Trim()) - 1) +
                                           "' and DOT = '0'";
                    }
                    else
                    {
                         strinsert = "insert into MAU_THIDUA select STT, TT,'" +
                                           dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' NGAY, MAPOS ,'" +
                                           dtpNgay.SelectedDate.Value.ToString("yyyy") + "' NAM,'" +
                                           CboQuy.SelectionBoxItem.ToString().Trim() + "' QUY,'" +
                                           bll.Left(RadCboDot.SelectedValue.ToString(), 1) + "' DOT, CHITIEU, DIEM"
                                           +
                                           " , P0701, P0702, P0703, P0801, P0802, P0803, P0101, P0102, P0103, P0201, P0202 "
                                           +
                                           " , P0203, P0301, P0302, P0303, P0601, P0602, P0603, P0401, P0402, P0403, P0501"
                                           +
                                           " , P0502, P0503, PHONG, INDAM, TIEUDE, ND_MA, CHOT01, CHOT02, CHOT03, CHOT04, CHOT05"
                                           +
                                           " , CHOT06, CHOT07, CHOT08, '' NGNH01,'' NGNH02,'' NGNH03,'' NGNH04,'' NGNH05,'' NGNH06,'' NGNH07,'' NGNH08"
                                           +
                                           " , NGHIENG, CONG, TENCHUYENDE, TENPHONG, CHONIN, CHUYENDE,'' GHICHU,SUBTT,INPUT,NHOM from MAU_THIDUA where NAM ='" + (int.Parse(comboBoxYear.SelectedValue.ToString())-1) + "' and QUY='4' and DOT = '0'";
                    }
                    _cls.UpdateDataText(strinsert);
                    MessageBox.Show("Insert New OK");
                    //_dt = _cls.LoadDataText("select * from MAU_THIDUA where NAM = '" + comboBoxYear.SelectedValue + "' and QUY = '2' and DOT = '0' and CHUYENDE = '"+RadCboChDe.SelectedValue.ToString().Trim()+"'");
                    //dgvTarGet.ItemsSource = _dt.DefaultView;
                }
                    string str = "select * from MAU_THIDUA where NAM='" +
                                 comboBoxYear.SelectedValue + "' and QUY='" + CboQuy.SelectionBoxItem.ToString().Trim() +
                                 "' and DOT='" + bll.Left(RadCboDot.SelectedValue.ToString(), 1) + "'and CHUYENDE='" + RadCboChDe.SelectedValue.ToString().Trim() + "' order by STT";
                    _dt = _cls.LoadDataText(str);
                    dgvTarGet.ItemsSource = _dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _cls.DongKetNoi();
            }
         
        }
        private void lblReset_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (BienBll.Ndma.ToUpper() == "TUTM0001")
            {
                try
                {
                    //string quy = ((int.Parse(dtpNgay.SelectedDate.Value.ToString("MM")) - 1) / 3 + 1).ToString();
                    // string nam = dtpNgay.SelectedDate.Value.ToString("yyyy");
                    _cls.ClsConnect();
                    string strup = "update MAU_THIDUA set P0101 = 0,P0102 = 0,P0103 = 0,P0201 = 0,P0202 = 0,P0203 = 0,P0301 = 0,P0302 = 0,P0303 = 0"
                                    + ",P0401 = 0,P0402 = 0,P0403 = 0,P0501 = 0,P0502 = 0,P0503 = 0,P0601 = 0,P0602 = 0,P0603 = 0,P0701 = 0,P0702 = 0,P0703 = 0"
                                    + " ,P0801 = 0,P0802 = 0,P0803 = 0,CHOT01='F',CHOT02='F',CHOT03='F',CHOT04='F',CHOT05='F',CHOT06='F',CHOT07='F',CHOT08='F' where  NAM='" +
                                    comboBoxYear.SelectedValue + "' and QUY='" +
                                    CboQuy.SelectionBoxItem.ToString().Trim() + "' and DOT='" +
                                    bll.Left(RadCboDot.SelectedValue.ToString(), 1) + "' and CHUYENDE='" + RadCboChDe.SelectedValue.ToString().Trim() + "'";
                    _cls.UpdateDataText(strup);
                    MessageBox.Show("Reset OK", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error \n" + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    _cls.DongKetNoi();
                }
            } else MessageBox.Show("Bạn không có quyền thực hiện việc này !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void lblResetChot_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                _cls.ClsConnect();
                string strup = "update MAU_THIDUA set CHOT01='F',CHOT02='F',CHOT03='F',CHOT04='F',CHOT05='F',CHOT06='F',CHOT07='F',CHOT08='F' where NAM='" + comboBoxYear.SelectedValue + "' and QUY='" +
                                CboQuy.SelectionBoxItem.ToString().Trim() + "' and DOT='" + bll.Left(RadCboDot.SelectedValue.ToString(), 1) + "' and CHUYENDE='" + RadCboChDe.SelectedValue.ToString().Trim() + "'";
                string strup1 = "update MAU_THIDUA set P0101 = 0,P0102 = 0,P0103 = 0,P0201 = 0,P0202 = 0,P0203 = 0,P0301 = 0,P0302 = 0,P0303 = 0"
                               +
                               ",P0401 = 0,P0402 = 0,P0403 = 0,P0501 = 0,P0502 = 0,P0503 = 0,P0601 = 0,P0602 = 0,P0603 = 0,P0701 = 0,P0702 = 0,P0703 = 0"
                               + " ,P0801 = 0,P0802 = 0,P0803 = 0 where CHONIN = 1 and NAM='" +
                               comboBoxYear.SelectedValue + "' and QUY='" +
                               CboQuy.SelectionBoxItem.ToString().Trim() + "' and DOT='" +
                               bll.Left(RadCboDot.SelectedValue.ToString(), 1) + "'  and CHUYENDE='" + RadCboChDe.SelectedValue.ToString().Trim() + "'";
               // MessageBox.Show(strup);
                _cls.UpdateDataText(strup);
               // MessageBox.Show(strup1);
                _cls.UpdateDataText(strup1);
                MessageBox.Show("Reset OK", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _cls.DongKetNoi();
            }

        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private void PopulateMonthsAndYears()
        {
            //comboBoxMonth.ItemsSource = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames.Take(12).ToList();
            //comboBoxMonth.SelectedItem = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames[DateTime.Now.AddMonths(-1).Month - 1];
            //for (int x = 0; x < 12; x++)
            //{
            //    comboBoxMonth.Items.Add
            //    (
            //       (x + 1).ToString("00")
            //       + " "
            //       + CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.GetValue(x)
            //     );
            //}
            //comboBoxMonth.SelectedIndex = 0;
            comboBoxYear.ItemsSource = Enumerable.Range(2017, DateTime.Now.Year - 2017 + 5).ToList();
            comboBoxYear.SelectedItem = DateTime.Now.Year;
            //comboBoxYear.SelectedIndex = 7;
        }

  
    }
}
