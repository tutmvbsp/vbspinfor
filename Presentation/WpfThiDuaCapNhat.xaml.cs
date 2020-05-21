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
    public partial class WpfThiDuaCapNhat : Window
    {
        public WpfThiDuaCapNhat()
        {
            InitializeComponent();
        }
        readonly ClsServer _cls = new ClsServer();
        ToolBll bll = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable _dt = new DataTable();
        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _cls.ClsConnect();
                foreach (DataRow dr in _dt.Rows)
                {
                    string strup = "update MAU_THIDUA set DIEM='" + dr["DIEM"] + "',TT='" + dr["TT"] + "', CHITIEU= N'" +dr["CHITIEU"] 
                        + "',INDAM='"+ dr["INDAM"] + "',NGHIENG='"+ dr["NGHIENG"] + "',CONG='"+ dr["CONG"] 
                        + "' where NAM='" + comboBoxYear.SelectedValue + "' and QUY='" + CboQuy.SelectionBoxItem.ToString().Trim() 
                        + "' and DOT='" + bll.Left(RadCboDot.SelectedValue.ToString(), 1) + "' and STT="+ dr["STT"] 
                        + " and CHUYENDE='" + RadCboChDe.SelectedValue.ToString().Trim()+"'";
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
                _cls.ClsConnect();
                string strchk1 = "select top 1 * from MAU_THIDUA where NAM='" + comboBoxYear.SelectedValue + "' and QUY='" +
                                    CboQuy.SelectionBoxItem.ToString().Trim() + "' and DOT='" +
                                    bll.Left(RadCboDot.SelectedValue.ToString(), 1) + "' and CHUYENDE='"+RadCboChDe.SelectedValue.ToString().Trim()+"'";
                var dtchk1 = _cls.LoadDataText(strchk1);
                if (dtchk1.Rows.Count == 0)
                    MessageBox.Show(
                        "Chưa có chỉ tiêu tại MAU_THIDUA Quý : " + CboQuy.SelectionBoxItem.ToString().Trim() +
                        ", Năm " + comboBoxYear.SelectedValue + " Đợt : " + RadCboDot.SelectedValue, "Thông báo",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                else
                {
                    string str = "select * from MAU_THIDUA where NAM='" +
                                 comboBoxYear.SelectedValue + "' and QUY='" + CboQuy.SelectionBoxItem.ToString().Trim() +
                                 "' and DOT='" + bll.Left(RadCboDot.SelectedValue.ToString(), 1) + "'and CHUYENDE='" + RadCboChDe.SelectedValue.ToString().Trim() + "' order by STT";
                    _dt = _cls.LoadDataText(str);
                    dgvTarGet.ItemsSource = _dt.DefaultView;
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
