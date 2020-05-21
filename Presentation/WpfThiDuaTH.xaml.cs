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
    public partial class WpfThiDuaTH : Window
    {
        public WpfThiDuaTH()
        {
            InitializeComponent();
        }
        readonly ClsServer _cls = new ClsServer();
        ToolBll bll = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable _dt = new DataTable();
        DataTable dtnew = new DataTable();
        private string strin = "";
        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _cls.ClsConnect();
                dtnew = _dt.GetChanges();
                if (dtnew==null)
                    MessageBox.Show("Chưa có thay đổi nào về điểm !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    dgvTarGet.ItemsSource = dtnew.DefaultView;
                    foreach (DataRow dr in dtnew.Rows)
                    {
                        string strup = "update MAU_THIDUA set P0102='" + dr["P0102"] + "',P0202='" + dr["P0202"] + "',P0302='" 
                            + dr["P0302"] + "',P0402='" + dr["P0402"] + "',P0502='" + dr["P0502"] + "',P0602='" + dr["P0602"] 
                            + "',P0702='" + dr["P0702"] + "',P0802='" + dr["P0802"] + "'  where NAM='" + comboBoxYear.SelectedValue 
                            + "' and QUY='" + CboQuy.SelectionBoxItem.ToString().Trim() 
                            + "' and DOT='" + bll.Left(CboDot.SelectedValue.ToString(), 1) 
                            + "' and STT=" + dr["STT"] + " and SUBTT="+dr["SUBTT"]+" and CHUYENDE='" + RadCboChDe.SelectedValue.ToString().Trim() + "'";
                        //MessageBox.Show(strup);
                        _cls.UpdateDataText(strup);
                    }
                    //string strup1= "update LUUTHIDUA set P0103=P0102-P0101,P0203=P0202-P0201,P0303=P0302-P0301,P0403=P0402-P0401,P0503=P0502-P0501,P0603=P0602-P0601,P0703=P0702-P0701,P0803=P0802-P0801 where NAM='" + comboBoxYear.SelectedValue + "' and QUY='" + CboQuy.SelectionBoxItem.ToString().Trim() + "' and DOT='" + bll.Left(CboDot.SelectedValue.ToString(), 1) + "' and CHUYENDE='" + RadCboChDe.SelectedValue.ToString().Trim()+"'";
                    //MessageBox.Show(strup1);
                    //_cls.UpdateDataText(strup1);
                    MessageBox.Show("Save data OK", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                string strghichu = "update MAU_THIDUA set GHICHU=N'" + txtGhiChu.Text.Trim() 
                    + "' where NAM='" + comboBoxYear.SelectedValue + "' and QUY='" + CboQuy.SelectionBoxItem.ToString().Trim() 
                    + "' and DOT='" + bll.Left(CboDot.SelectedValue.ToString(), 1) + "' and CHUYENDE='" + RadCboChDe.SelectedValue.ToString().Trim() + "'";
                //MessageBox.Show("Update OK! " + strghichu, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                _cls.UpdateDataText(strghichu);

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
            if (BienBll.NdCapbc == "02") ChkTh.IsEnabled = false;
            try
            {

                _cls.ClsConnect();
                var dtpos = _cls.LoadDataText("select PO_MA,PO_TEN from DMPOS where PO_MA='"+BienBll.NdMadv.Trim()+"'");
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                CboPos.SelectedIndex = 0;
                var dtdot = _cls.LoadDataText("select * from DOT_THIDUA order by DOT");
                for (int i = 0; i < dtdot.Rows.Count; i++)
                {
                    CboDot.Items.Add(dtdot.Rows[i][0].ToString().Trim() + " | " + dtdot.Rows[i][1]);
                }
                CboDot.SelectedIndex = 0;
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
                _cls.ClsConnect();
                string strchk = "select top 1 * from MAU_THIDUA where NAM='" + comboBoxYear.SelectedValue 
                    + "' and QUY='" + CboQuy.SelectionBoxItem.ToString().Trim() 
                    + "' and DOT='" + bll.Left(CboDot.SelectedValue.ToString(), 1) 
                    + "' and CHUYENDE='" + RadCboChDe.SelectedValue.ToString().Trim()+"'";
                var dtchk = _cls.LoadDataText(strchk);
                if (dtchk.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Chưa có đơn vị nào chấm điểm Quý : " + CboQuy.SelectionBoxItem.ToString().Trim() +
                        ", Năm " + comboBoxYear.SelectedValue + " Đợt : " + CboDot.SelectedValue, "Thông báo",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                else
                {
                    string str = "select * from MAU_THIDUA where NAM='" +
                                 comboBoxYear.SelectedValue + "' and QUY='" + CboQuy.SelectionBoxItem.ToString().Trim() +
                                 "' and DOT='" + bll.Left(CboDot.SelectedValue.ToString(), 1) + "' and CHUYENDE='" + RadCboChDe.SelectedValue.ToString().Trim()+"' and CHONIN='1' order by STT";
                    _dt = _cls.LoadDataText(str);
                    dgvTarGet.ItemsSource = _dt.DefaultView;
                    txtGhiChu.Text = _dt.Rows[0]["GHICHU"].ToString();
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
                if (ChkTh.IsChecked == true)
                {
                    
                    if (bll.Left(CboDot.SelectedValue.ToString(), 1)=="0")  // đợt ngắn
                        strin = "select b.PO_TEN,a.*,P" + CboPos.SelectedValue.ToString().Substring(4, 2) +
                                       "01 CHAMDIEM ,(case when a.DOT='0' then c.MOTA+' Quý '+ a.QUY + ' Năm '+ a.NAM else c.MOTA end) TITLE,NGNH" +
                                       CboPos.SelectedValue.ToString().Substring(4, 2) +
                                       " NG_NHAN from MAU_THIDUA a,DMPOS b,DOT_THIDUA c where a.NAM='" +
                                       comboBoxYear.SelectedValue + "' and a.QUY='" +
                                       CboQuy.SelectionBoxItem.ToString().Trim() + "' and a.DOT='" +
                                       bll.Left(CboDot.SelectedValue.ToString(), 1) + "' and b.PO_MA='" +
                                       bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "' and a.DOT=c.DOT and a.CHONIN='1' order by a.CHUYENDE,a.STT";
                    else
                        strin = "select b.PO_TEN,a.*,P" + CboPos.SelectedValue.ToString().Substring(4, 2) +
                                       "01 CHAMDIEM ,(case when a.DOT='0' then c.MOTA+' Quý '+ a.QUY + ' Năm '+ a.NAM else c.MOTA end) TITLE,NGNH" +
                                       CboPos.SelectedValue.ToString().Substring(4, 2) +
                                       " NG_NHAN from MAU_THIDUA a,DMPOS b,DOT_THIDUA c where a.NAM='" +
                                       comboBoxYear.SelectedValue + "' and a.QUY='" +
                                       CboQuy.SelectionBoxItem.ToString().Trim() + "' and a.DOT='" +
                                       bll.Left(CboDot.SelectedValue.ToString(), 1) + "' and b.PO_MA='" +
                                       bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "' and a.DOT=c.DOT and  a.CHUYENDE='" + RadCboChDe.SelectedValue.ToString().Trim() + "' and a.CHONIN='1' order by a.CHUYENDE,a.STT";

                    //MessageBox.Show(strin);
                    _cls.ClsConnect();
                    _dt = _cls.LoadDataText(strin);
                    if (_dt.Rows.Count > 0)
                    {
                        rpt_ThiDua02 rpt = new rpt_ThiDua02();
                        RPUtility.ShowRp(rpt, _dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                            srv.DbPassSerVer());
                    }
                    else
                        MessageBox.Show("Không có dữ liệu để in !", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                }
                else
                {
                    string strin = "select a.* from MAU_THIDUA a where a.NAM='" +
                                  comboBoxYear.SelectedValue + "' and a.QUY='" +
                                  CboQuy.SelectionBoxItem.ToString().Trim() + "' and a.DOT='" +
                                  bll.Left(CboDot.SelectedValue.ToString(), 1) + "' and  CHUYENDE='" + RadCboChDe.SelectedValue.ToString().Trim()+"' and CHONIN='1' order by STT";
                    _cls.ClsConnect();
                    _dt = _cls.LoadDataText(strin);
                    if (_dt.Rows.Count > 0)
                    {
                        rpt_ThiDua03 rpt = new rpt_ThiDua03();
                        RPUtility.ShowRp(rpt, _dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                            srv.DbPassSerVer());
                    }
                    else
                        MessageBox.Show("Không có dữ liệu để in !", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Warning);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _cls.DongKetNoi();
            LblManual_OnMouseDown(null, null);
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
