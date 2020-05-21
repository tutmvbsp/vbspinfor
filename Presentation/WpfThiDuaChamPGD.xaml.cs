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
    public partial class WpfThiDuaChamPGD : Window
    {
        public WpfThiDuaChamPGD()
        {
            InitializeComponent();
        }
        readonly ClsServer _cls = new ClsServer();
        ToolBll bll = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable _dt = new DataTable();
        DataTable _dtNew = new DataTable();
        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_dt.Rows.Count != 0)
                {
                    _dtNew = _dt.GetChanges();
                    if (_dtNew != null)
                    {
                        _cls.ClsConnect();
                        foreach (DataRow dr in _dtNew.Rows)
                        {
                            string strup = "update MAU_THIDUA set P" + CboPos.SelectedValue.ToString().Substring(4, 2) + "01 =" +
                                           dr["DIEMCHAM"] + ",NGNH" + CboPos.SelectedValue.ToString().Substring(4, 2) +
                                           "=N'" + dr["NG_NHAN"] + "' where NAM='" + comboBoxYear.SelectedValue +
                                           "' and QUY='" + CboQuy.SelectionBoxItem.ToString().Trim() + "' and DOT='" +
                                           bll.Left(CboDot.SelectedValue.ToString(), 1) + "' and STT=" + dr["STT"] +
                                           "and SUBTT="+dr["SUBTT"]+" and CHUYENDE='" + RadCboChDe.SelectedValue.ToString().Trim() + "'";
                           // MessageBox.Show(strup);
                            _cls.UpdateDataText(strup);
                        }
                        MessageBox.Show("Lưu dữ liệu thành công !", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                    } else MessageBox.Show("Chưa có thay đổi nào !", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
                } else MessageBox.Show("Bạn chưa lấy dữ liệu !", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi \n" + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
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
                CboPos.ItemsSource = dtpos.DefaultView;
                CboPos.DisplayMemberPath = "PO_TEN";
                CboPos.SelectedValuePath = "PO_MA";
                CboPos.SelectedIndex = 0;
                var dtdot = _cls.LoadDataText("select * from DOT_THIDUA order by DOT");
                for (int i = 0; i < dtdot.Rows.Count; i++)
                {
                    CboDot.Items.Add(dtdot.Rows[i][0].ToString().Trim() + " | " + dtdot.Rows[i][1]);
                }
                CboDot.SelectedIndex = 0;
                var dtchde =
                    _cls.LoadDataText("select * from CHUYENDE order by MA");
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
                _cls.ClsConnect();
                string strchk = "select top 1 * from MAU_THIDUA where NAM='" + comboBoxYear.SelectedValue + "' and QUY='" + CboQuy.SelectionBoxItem.ToString().Trim() + "' and DOT='" + bll.Left(CboDot.SelectedValue.ToString(), 1) 
                    + "' and CHUYENDE='" + RadCboChDe.SelectedValue.ToString().Trim() + "' and CHONIN='1'";
                var dtchk = _cls.LoadDataText(strchk);
                if (dtchk.Rows.Count != 0)
                {
                    string str = "select *,P" + CboPos.SelectedValue.ToString().Substring(4, 2) + "01 DIEMCHAM,N'' NG_NHAN from MAU_THIDUA where NAM='" + comboBoxYear.SelectedValue + "' and QUY='" + CboQuy.SelectionBoxItem.ToString().Trim() 
                        + "' and DOT='" + bll.Left(CboDot.SelectedValue.ToString(), 1) + "' and CHOT" + CboPos.SelectedValue.ToString().Substring(4, 2) + "='F' and CHUYENDE='" + RadCboChDe.SelectedValue.ToString().Trim() + "' and CHONIN='1' order by STT";
                    //MessageBox.Show(str);
                    _dt = _cls.LoadDataText(str);
                    if (_dt.Rows.Count > 0)
                    {
                        dgvTarGet.ItemsSource = _dt.DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("Đơn vị đã chấm và chốt điểm, liên hệ phòng tin học để được hỗ trợ ! ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                    MessageBox.Show(
                                            "Chưa có chỉ tiêu tại MAU_THIDUA Quý : " + CboQuy.SelectionBoxItem.ToString().Trim() +
                                            ", Năm " + comboBoxYear.SelectedValue + " Đợt : " + CboDot.SelectedValue, "Thông báo",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Information);
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

        private void Print_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _cls.ClsConnect();
                int thamso = 3;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Nam";
                giatri[0] = comboBoxYear.SelectedValue;
                bien[1] = "@Quy";
                giatri[1] = CboQuy.SelectionBoxItem.ToString().Trim();
                bien[2] = "@Dot";
                giatri[2] = bll.Left(CboDot.SelectedValue.ToString(), 1);

                //MessageBox.Show(giatri[0] + "   " + giatri[1]);
                //dt = cls.LoadDataProcPara("usp_KHB", bien, giatri, thamso);
                _cls.UpdateDataProcPara("usp_CongThiDua", bien, giatri, thamso);

                //string strcong = "with lst1 as "
                //                  +" (select CHUYENDE, sum(DIEM) DIEM, sum(P0101) P0101, sum(P0102) P0102, sum(P0201) P0201, sum(P0202) P0202 "
                //                  +", sum(P0301) P0301, sum(P0302) P0302, sum(P0401) P0401, sum(P0402) P0402, sum(P0501) P0501, sum(P0502) P0502 "
                //                  +", sum(P0601) P0601, sum(P0602) P0602, sum(P0701) P0701, sum(P0702) P0702, sum(P0801) P0801, sum(P0802) P0802 "
                //                  + " from MAU_THIDUA where CONG='0' and NAM='" + comboBoxYear.SelectedValue + "' and QUY='" + CboQuy.SelectionBoxItem.ToString().Trim()
                //                  + "' and DOT='" + bll.Left(CboDot.SelectedValue.ToString(), 1) + "' and CHUYENDE='" + RadCboChDe.SelectedValue.ToString().Trim() + "' group by CHUYENDE ) "
                //                  + "update a set a.P0101 = b.P0101,a.P0102 = b.P0102,a.P0201 = b.P0201,a.P0202 = b.P0202,a.P0301 = b.P0301,a.P0302 = b.P0302, "
                //                  +" a.P0401 = b.P0401,a.P0402 = b.P0402,a.P0501 = b.P0501,a.P0502 = b.P0502,a.P0601 = b.P0601,a.P0602 = b.P0602, "
                //                  + " a.P0701 = b.P0701,a.P0702 = b.P0702,a.P0801 = b.P0801,a.P0802 = b.P0802 from MAU_THIDUA a, lst1 b " 
                //                  + " where a.NAM='" + comboBoxYear.SelectedValue + "' and a.QUY='" + CboQuy.SelectionBoxItem.ToString().Trim()
                //                  + "' and a.DOT='" + bll.Left(CboDot.SelectedValue.ToString(), 1) + "' and a.CHUYENDE='" + RadCboChDe.SelectedValue.ToString().Trim() + "' and a.CHUYENDE = b.CHUYENDE and a.CONG='1' and a.CHONIN='1'";
                //MessageBox.Show(strcong);
                // _cls.UpdateDataText(strcong);
                string strin = "";
                    if (ChkTh.IsChecked==true)
                    strin = "select b.PO_TEN,a.*,P" + CboPos.SelectedValue.ToString().Substring(4, 2) + "01 CHAMDIEM ," +
                            "( case when a.DOT=0 then c.MOTA+' Quý '+ a.QUY + ' Năm '+ a.NAM else c.MOTA end ) TITLE,NGNH"
                    + CboPos.SelectedValue.ToString().Substring(4, 2) + " NG_NHAN from MAU_THIDUA a,DMPOS b,DOT_THIDUA c where a.NAM='" 
                    + comboBoxYear.SelectedValue+ "' and a.QUY='"+CboQuy.SelectionBoxItem.ToString().Trim()+ "' and a.DOT='"
                    +bll.Left(CboDot.SelectedValue.ToString(), 1)+ "' and b.PO_MA='"+bll.Left(CboPos.SelectedValue.ToString().Trim(), 6)+ "' and a.DOT=c.DOT and a.CHONIN='1' order by a.CHUYENDE,a.STT";
                    else
                    strin = "select b.PO_TEN,a.*,P" + CboPos.SelectedValue.ToString().Substring(4, 2) + "01 CHAMDIEM ,case when a.DOT=0 then c.MOTA+' Quý '+ a.QUY + ' Năm '+ a.NAM else c.MOTA end TITLE,NGNH"
                    + CboPos.SelectedValue.ToString().Substring(4, 2) + " NG_NHAN from MAU_THIDUA a,DMPOS b,DOT_THIDUA c where a.NAM='"
                    + comboBoxYear.SelectedValue + "' and a.QUY='" + CboQuy.SelectionBoxItem.ToString().Trim() + "' and a.DOT='"
                    + bll.Left(CboDot.SelectedValue.ToString(), 1) + "' and b.PO_MA='" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "' and a.DOT=c.DOT and CHUYENDE='" + RadCboChDe.SelectedValue + "' and a.CHONIN='1' order by a.STT";

                //MessageBox.Show(strin);
                _dt = _cls.LoadDataText(strin);
                if (_dt.Rows.Count > 0)
                {
                    rpt_ThiDua01 rpt = new rpt_ThiDua01();
                    RPUtility.ShowRp(rpt, _dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                }
                else MessageBox.Show("Không có dữ liệu để in !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _cls.DongKetNoi();
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

        private void mnuGui_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_dt.Rows.Count != 0)
                {
                        _cls.ClsConnect();
                        foreach (DataRow dr in _dtNew.Rows)
                        {
                            //string strup = "update MAU_THIDUA set CHOT" +
                            //               CboPos.SelectedValue.ToString().Substring(4, 2) +
                            //               "='T' where NAM='" + comboBoxYear.SelectedValue +
                            //               "' and QUY='" + CboQuy.SelectionBoxItem.ToString().Trim() + "' and DOT='" +
                            //               bll.Left(CboDot.SelectedValue.ToString(), 1) + "' and STT=" + dr["STT"] +
                            //               " and CHUYENDE='" + RadCboChDe.SelectedValue.ToString().Trim() + "'";
                        //MessageBox.Show(strup);
                        string strup = "update MAU_THIDUA set CHOT" +
                                       CboPos.SelectedValue.ToString().Substring(4, 2) +
                                       "='T' where NAM='" + comboBoxYear.SelectedValue +
                                       "' and QUY='" + CboQuy.SelectionBoxItem.ToString().Trim() + "' and DOT='" +
                                       bll.Left(CboDot.SelectedValue.ToString(), 1) + "'";

                        _cls.UpdateDataText(strup);
                        }
                        MessageBox.Show("Đã chốt gửi dữ liệu Thành công !", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else MessageBox.Show("Bạn chưa lấy dữ liệu !", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
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
    }
}
