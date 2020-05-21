using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfKhtc01.xaml
    /// </summary>
    public partial class WpfQt14
    {
        public WpfQt14()
        {
            InitializeComponent();
        }

        private readonly ClsServer _cls = new ClsServer();
        private DataTable _dt = new DataTable();
        private readonly ToolBll _str = new ToolBll();
        private string sql = "";
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show(BienBll.Ndma);
            ServerInfor srv = new ServerInfor();
            _cls.ClsConnect();
            int thamso = 3;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@Ngay";
            if (dtpNgay.SelectedDate != null) giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
            bien[1] = "@MaPos";
            giatri[1] = _str.Left(cboPos.SelectedValue.ToString().Trim(), 6);
            bien[2] = "@Mau";
            if (Ration4.IsChecked != true)
            {
                if (Ration1.IsChecked == true)  //Mau 14
                {
                    giatri[2] = "1";
                    _dt = _cls.LoadDataProcPara(giatri[1].ToString() == "003000" ? "usp_Khtc05_th" : "usp_Khtc05", bien,
                        giatri, thamso);
                }
                else if (Ration2.IsChecked==true) // Mau 14A
                {
                    giatri[2] = "2";
                    _dt = _cls.LoadDataProcPara(giatri[1].ToString() == "003000" ? "usp_Khtc05_th" : "usp_Khtc05", bien,
                        giatri, thamso);
                }
            }
            else
            {
                giatri[2] = "1";
                _dt = _cls.LoadDataProcPara("usp_Khtc05_cd", bien,giatri, thamso);               
            }
            if (_dt.Rows.Count > 0)
            {
                if (Ration4.IsChecked == true)
                {
                    rpt_QT14_CD rpt = new rpt_QT14_CD();
                    RPUtility.ShowRp(rpt, _dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());

                }
                else
                {

                    if (Ration1.IsChecked == true)
                    {
                        rpt_QT14 rpt = new rpt_QT14();
                        RPUtility.ShowRp(rpt, _dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                    }
                    else
                    {
                        rpt_QT14A rpt = new rpt_QT14A();
                        RPUtility.ShowRp(rpt, _dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                    }
                }
            }
            else
            {
                MessageBox.Show("Chưa có số liệu", "Thông báo");
            }
            _cls.DongKetNoi();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dtpNgay.SelectedDate =DateTime.Parse(DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-" +DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month).ToString());
            try
            {
                _cls.ClsConnect();
                if (BienBll.NdMadv == BienBll.MainPos)
                {
                    sql = "select PO_MA,PO_TEN from DMPOS order by PO_MA";
                }
                else
                {
                    sql = "select PO_MA,PO_TEN from DMPOS where PO_MA='" + BienBll.NdMadv + "'";
                }
                var dtpos = _cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    cboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                cboPos.SelectedIndex = 1;
                var dtng = _cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            _cls.DongKetNoi();
        }

        private void LblManual_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            string sql = "";
            bool ok = false;
            DateTime NgayDau = new DateTime();
            //NgayDau = DateTime.Parse(DateTime.Now.AddYears(-1).ToString("yyyy") + "-12-31");
            NgayDau = DateTime.Parse(dtpNgay.SelectedDate.Value.AddYears(-1).ToString("yyyy") + "-12-31");
            #region

            try
            {
                int mm = dtpNgay.SelectedDate.Value.Month;
                for (int i = 1; i <= mm; i++)
                {
                    NgayDau = NgayDau.AddMonths(1);
                    NgayDau =DateTime.Parse(NgayDau.ToString("yyyy-MM") + "-" +DateTime.DaysInMonth(NgayDau.Year, NgayDau.Month).ToString());
                    if (dtpNgay.SelectedDate.Value.ToString("MM") == i.ToString("00") && NgayDau.ToString("yyyy") == dtpNgay.SelectedDate.Value.ToString("yyyy"))
                    {
                        NgayDau = DateTime.Parse(dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd"));
                    }
                    _cls.ClsConnect();
                    if (Ration3.IsChecked == true)
                    {
                        if (Ration1.IsChecked == true)
                        {
                            sql = "Select * from QT14 where ngay= '" + NgayDau.ToString("yyyy-MM-dd") + "'";
                        }
                        else if(Ration2.IsChecked==true)
                        {
                            sql = "Select * from QT14A where ngay= '" + NgayDau.ToString("yyyy-MM-dd") + "'";
                        }
                    }
                    else if (Ration4.IsChecked==true)
                    {
                        sql = "Select * from QT14_CD where ngay= '" + NgayDau.ToString("yyyy-MM-dd") + "'";
                    }
                    _dt = _cls.LoadDataText(sql);
                    if (_dt.Rows.Count == 0)
                    {
                        MessageBoxResult Result =
                            MessageBox.Show(
                                "Chưa có số liệu ngày : " + NgayDau.ToString("dd/MM/yyyy") + " Có muốn tạo không ?",
                                "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (Result == MessageBoxResult.Yes)
                        {
                            const int thamso = 1;
                            string[] bien = new string[thamso];
                            object[] giatri = new object[thamso];
                            bien[0] = "@Ngay";
                            if (Ration4.IsChecked == true)
                            {
                                giatri[0] = NgayDau.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                giatri[0] = NgayDau.ToString("yyyy-MM-dd");
                            }
                            if (Ration3.IsChecked == true)
                            {
                                if (Ration1.IsChecked == true)
                                {
                                    _cls.UpdateLdbf("usp_QT14", bien, giatri, thamso);
                                }
                                else if (Ration2.IsChecked==true)
                                {
                                    _cls.UpdateLdbf("usp_QT14A", bien, giatri, thamso);
                                }
                            }
                            else if (Ration4.IsChecked==true)
                            {
                               _cls.UpdateLdbf("usp_QT14_CD", bien, giatri, thamso);
                            }
                            MessageBox.Show("Tạo xong số liệu ngày : " + NgayDau.ToString("dd/MM/yyyy"), "Thông báo",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            ok = true;
                        }
                        else
                        {
                            MessageBox.Show(
                                "Bảng quyết toán sẽ không đúng khi không tạo số liệu ngày :  " +
                                NgayDau.ToString("dd/MM/yyyy"), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                            ok = false;
                        }
                    }
                    else
                    {
                        ok = true;
                    }
                }
                _cls.DongKetNoi();
                if (ok)
                {
                    MessageBox.Show("Đã có đủ số liêu", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Kiểm tra lại, chưa đủ số liệu", "Thông báo", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            #endregion

        }

        private void LblXoa_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Ration3.IsChecked == true)
            {
                if (Ration1.IsChecked == true)
                {
                    try
                    {
                        //Xoa mau 14
                        sql = "delete from QT14 where NGAY='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "'";
                        MessageBox.Show(sql);
                        _cls.ClsConnect();
                        _cls.UpdateDataText(sql);
                        _cls.DongKetNoi();
                        MessageBox.Show("Đã xóa số liêu QT14 ngày " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"),
                            "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    try
                    {
                        //Xoa mau 14
                        sql = "delete from QT14A where NGAY='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "'";
                        MessageBox.Show(sql);
                        _cls.ClsConnect();
                        _cls.UpdateDataText(sql);
                        _cls.DongKetNoi();
                        MessageBox.Show("Đã xóa số liêu QT14A ngày " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"),
                            "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
            else
            {
                try
                {
                    //Xoa mau 14
                    sql = "delete from QT14_CD where NGAY='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "'";
                    MessageBox.Show(sql);
                    _cls.ClsConnect();
                    _cls.UpdateDataText(sql);
                    _cls.DongKetNoi();
                    MessageBox.Show("Đã xóa số liêu QT14_CD ngày " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"),
                        "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }
    }
}