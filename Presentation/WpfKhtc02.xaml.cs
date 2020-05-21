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
using System.Data;
using DAL;
using BLL;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfKhtc02.xaml
    /// </summary>
    public partial class WpfKhtc02 : Window
    {
        public WpfKhtc02()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ToolBll str = new ToolBll();
        DataTable dt=new DataTable();
        ServerInfor srv = new ServerInfor();
        private string sql = "";
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           //MessageBox.Show(DateTime.Now.AddYears(-1).ToString("yyyy"));
           //dtpTuNgay.SelectedDate = DateTime.Parse("31/12/" + DateTime.Now.AddYears(-1).ToString("yyyy"));
           //dtpNgay.SelectedDate = DateTime.Now.Date;
           //dtpNgay.SelectedDate = DateTime.Parse(DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month).ToString());
            
           try
            {
                cls.ClsConnect();
                if (BienBll.NdMadv == BienBll.MainPos)
                {
                   sql = "select PO_MA,PO_TEN from DMPOS order by PO_MA";
                }
                else
                {
                   sql = "select PO_MA,PO_TEN from DMPOS where PO_MA='" + BienBll.NdMadv + "'";
                }
                var dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    cboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                cboPos.SelectedIndex = 1;
                var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (Ration4.IsChecked == true)
            {
                #region
                cls.ClsConnect();
                DataTable dt = new DataTable();
                int thamso = 2;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Ngay";
                if (dtpNgay.SelectedDate != null) giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                bien[1] = "@MaPos";
                giatri[1] = str.Left(cboPos.SelectedValue.ToString().Trim(), 6);
                if (Ration1.IsChecked == true)
                {
                    dt = cls.LoadDataProcPara("usp_Khtc02", bien, giatri, thamso);
                    if (dt.Rows.Count > 0)
                    {
                        rpt_khtc02 rpt = new rpt_khtc02();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                            srv.DbPassSerVer());
                    }
                    else
                    {
                        MessageBox.Show("Chưa có số liệu", "Thông báo");
                    }
                }
                else if (Ration1.IsChecked == true)
                {
                    dt = cls.LoadDataProcPara("usp_Khtc02_CT", bien, giatri, thamso);
                    if (dt.Rows.Count > 0)
                    {
                        rpt_khtc02_CT rpt = new rpt_khtc02_CT();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                            srv.DbPassSerVer());
                    }
                    else
                    {
                        MessageBox.Show("Chưa có số liệu", "Thông báo");
                    }
                }
                else
                {
                    int thamso1 = 3;
                    string[] bien1 = new string[thamso1];
                    object[] giatri1 = new object[thamso1];
                    bien1[0] = "@Ngay";
                    if (dtpNgay.SelectedDate != null) giatri1[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    bien1[1] = "@MaPos";
                    giatri1[1] = str.Left(cboPos.SelectedValue.ToString().Trim(), 6);
                    bien1[2] = "@Mau";
                    if (Ration4.IsChecked == true) giatri1[2] = "1";
                    else giatri1[2] = "2";
                    dt = cls.LoadDataProcPara("usp_Khtc02_CTTW", bien1, giatri1, thamso1);
                    if (dt.Rows.Count > 0)
                    {
                        rpt_khtc02_CTTW rpt = new rpt_khtc02_CTTW();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                            srv.DbPassSerVer());
                    }
                    else
                    {
                        MessageBox.Show("Chưa có số liệu", "Thông báo");
                    }

                }

                cls.DongKetNoi();

                #endregion
            }
            else
            {
            #region
                cls.ClsConnect();
                dt =
                    cls.LoadDataText("select * from QT_MS04TL where NG_CAPNHAT='" +
                                     dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "'");
                if (dt.Rows.Count == 0)
                    MessageBox.Show(
                        "Chưa có số liệu ngày : " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") + " từ TW",
                        "Thông báo",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                #region
                    int thamso = 3;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@Ngay";
                    if (dtpNgay.SelectedDate != null) giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    bien[1] = "@MaPos";
                    giatri[1] = str.Left(cboPos.SelectedValue.ToString().Trim(), 6);
                    bien[2] = "@Mau";
                    if (Ration4.IsChecked == true) giatri[2] = "1";
                    else giatri[2] = "2";

                    dt = cls.LoadDataProcPara("usp_Khtc02_CTTW", bien, giatri, thamso);
                    if (dt.Rows.Count > 0)
                    {
                        rpt_khtc02_CTTW rpt = new rpt_khtc02_CTTW();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                            srv.DbPassSerVer());
                    }
                    else
                    {
                        MessageBox.Show("Chưa có số liệu", "Thông báo");
                    }
                #endregion
                }

                #endregion
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void LblManual_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Ration4.IsChecked == true)
            {
                #region

                if (Ration1.IsChecked == true)
                {
                    string sql = "";
                    bool ok = false;
                    DateTime NgayDau = new DateTime();
                    NgayDau = DateTime.Parse(dtpNgay.SelectedDate.Value.AddYears(-1).ToString("yyyy") + "-12-31");
                    // NgayDau = DateTime.Parse(DateTime.Now.AddYears(-1).ToString("yyyy") + "-12-31");
                    //Xu ly phan thang 12 nam truoc
                    cls.ClsConnect();
                    sql = "Select * from LUU_PL04 where ngay= '" + NgayDau.ToString("yyyy-MM-dd") + "'";
                    dt = cls.LoadDataText(sql);
                    if (dt.Rows.Count == 0)
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
                            giatri[0] = NgayDau.ToString("yyyy-MM-dd");
                            cls.UpdateLdbf("usp_PL04", bien, giatri, thamso);
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
                    cls.DongKetNoi();
                    //-----------------------------------------------------------------------
                    int mm = dtpNgay.SelectedDate.Value.Month;
                    for (int i = 1; i <= mm; i++)
                    {
                        NgayDau = NgayDau.AddMonths(1);
                        NgayDau =
                            DateTime.Parse(NgayDau.ToString("yyyy-MM") + "-" +
                                           DateTime.DaysInMonth(NgayDau.Year, NgayDau.Month).ToString());
                        if (dtpNgay.SelectedDate.Value.ToString("MM") == i.ToString("00") &&
                            NgayDau.ToString("yyyy") == dtpNgay.SelectedDate.Value.ToString("yyyy"))
                        {
                            NgayDau = DateTime.Parse(dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd"));
                        }

                        //MessageBox.Show(NgayDau.ToString("yyyy-MM-dd"));
                        cls.ClsConnect();
                        sql = "Select * from LUU_PL04 where ngay= '" + NgayDau.ToString("yyyy-MM-dd") + "'";
                        dt = cls.LoadDataText(sql);
                        if (dt.Rows.Count == 0)
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
                                giatri[0] = NgayDau.ToString("yyyy-MM-dd");
                                cls.UpdateLdbf("usp_PL04", bien, giatri, thamso);
                                MessageBox.Show("Tạo xong số liệu ngày : " + NgayDau.ToString("dd/MM/yyyy"), "Thông báo",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                                ok = true;
                            }
                            else
                            {
                                MessageBox.Show(
                                    "Bảng quyết toán sẽ không đúng khi không tạo số liệu ngày :  " +
                                    NgayDau.ToString("dd/MM/yyyy"), "Thông báo", MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                                ok = false;
                            }
                        }
                        else
                        {
                            ok = true;
                        }
                    }
                    cls.DongKetNoi();
                    if (ok)
                    {
                        MessageBox.Show("Đã có đủ số liêu", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Kiểm tra lại, chưa đủ số liệu", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
                    #region

                else if (Ration2.IsChecked == true)
                {
                    string sql = "";
                    bool ok = false;
                    DateTime NgayDau = new DateTime();
                    NgayDau = DateTime.Parse(dtpNgay.SelectedDate.Value.AddYears(-1).ToString("yyyy") + "-12-31");
                    //Xu ly phan thang 12 nam truoc
                    cls.ClsConnect();
                    sql = "Select * from LUU_PL04CT where ngay= '" + NgayDau.ToString("yyyy-MM-dd") + "'";
                    dt = cls.LoadDataText(sql);
                    if (dt.Rows.Count == 0)
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
                            giatri[0] = NgayDau.ToString("yyyy-MM-dd");
                            cls.UpdateLdbf("usp_PL04CT", bien, giatri, thamso);
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
                    cls.DongKetNoi();
                    //-----------------------------------------------------------------------
                    int mm = dtpNgay.SelectedDate.Value.Month;
                    for (int i = 1; i <= mm; i++)
                    {
                        NgayDau = NgayDau.AddMonths(1);
                        NgayDau =
                            DateTime.Parse(NgayDau.ToString("yyyy-MM") + "-" +
                                           DateTime.DaysInMonth(NgayDau.Year, NgayDau.Month).ToString());
                        if (dtpNgay.SelectedDate.Value.ToString("MM") == i.ToString("00") &&
                            NgayDau.ToString("yyyy") == dtpNgay.SelectedDate.Value.ToString("yyyy"))
                        {
                            NgayDau = DateTime.Parse(dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd"));
                        }
                        //MessageBox.Show(NgayDau.ToString("yyyy-MM-dd"));
                        cls.ClsConnect();
                        sql = "Select * from LUU_PL04CT where ngay= '" + NgayDau.ToString("yyyy-MM-dd") + "'";
                        dt = cls.LoadDataText(sql);
                        if (dt.Rows.Count == 0)
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
                                giatri[0] = NgayDau.ToString("yyyy-MM-dd");
                                cls.UpdateLdbf("usp_PL04CT", bien, giatri, thamso);
                                MessageBox.Show("Tạo xong số liệu ngày : " + NgayDau.ToString("dd/MM/yyyy"), "Thông báo",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                                ok = true;
                            }
                            else
                            {
                                MessageBox.Show(
                                    "Bảng quyết toán sẽ không đúng khi không tạo số liệu ngày :  " +
                                    NgayDau.ToString("dd/MM/yyyy"), "Thông báo", MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                                ok = false;
                            }
                        }
                        else
                        {
                            ok = true;
                        }
                    }
                    cls.DongKetNoi();
                    if (ok)
                    {
                        MessageBox.Show("Đã có đủ số liêu", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Kiểm tra lại, chưa đủ số liệu", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                } 
                    #endregion

                else //=============================================================
                {
                    string sql = "";
                    bool ok = false;
                    DateTime NgayDau = new DateTime();
                    NgayDau = DateTime.Parse(dtpNgay.SelectedDate.Value.AddYears(-1).ToString("yyyy") + "-12-31");
                    //Xu ly phan thang 12 nam truoc
                    cls.ClsConnect();
                    sql = "Select * from LUU_PL04CTTW where ngay= '" + NgayDau.ToString("yyyy-MM-dd") + "'";
                    dt = cls.LoadDataText(sql);
                    if (dt.Rows.Count == 0)
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
                            giatri[0] = NgayDau.ToString("yyyy-MM-dd");
                            cls.UpdateLdbf("usp_PL04CTTW", bien, giatri, thamso);
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
                    cls.DongKetNoi();
                    //-----------------------------------------------------------------------
                    int mm = dtpNgay.SelectedDate.Value.Month;
                    for (int i = 1; i <= mm; i++)
                    {
                        NgayDau = NgayDau.AddMonths(1);
                        NgayDau =
                            DateTime.Parse(NgayDau.ToString("yyyy-MM") + "-" +
                                           DateTime.DaysInMonth(NgayDau.Year, NgayDau.Month).ToString());
                        if (dtpNgay.SelectedDate.Value.ToString("MM") == i.ToString("00") &&
                            NgayDau.ToString("yyyy") == dtpNgay.SelectedDate.Value.ToString("yyyy"))
                        {
                            NgayDau = DateTime.Parse(dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd"));
                        }
                        //MessageBox.Show(NgayDau.ToString("yyyy-MM-dd"));
                        cls.ClsConnect();
                        sql = "Select * from LUU_PL04CTTW where ngay= '" + NgayDau.ToString("yyyy-MM-dd") + "'";
                        dt = cls.LoadDataText(sql);
                        if (dt.Rows.Count == 0)
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
                                giatri[0] = NgayDau.ToString("yyyy-MM-dd");
                                cls.UpdateLdbf("usp_PL04CTTW", bien, giatri, thamso);
                                MessageBox.Show("Tạo xong số liệu ngày : " + NgayDau.ToString("dd/MM/yyyy"), "Thông báo",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                                ok = true;
                            }
                            else
                            {
                                MessageBox.Show(
                                    "Bảng quyết toán sẽ không đúng khi không tạo số liệu ngày :  " +
                                    NgayDau.ToString("dd/MM/yyyy"), "Thông báo", MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                                ok = false;
                            }
                        }
                        else
                        {
                            ok = true;
                        }
                    }
                    cls.DongKetNoi();
                    if (ok)
                    {
                        MessageBox.Show("Đã có đủ số liêu", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Kiểm tra lại, chưa đủ số liệu", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }

                #endregion
            }
            else
            {
            #region
                cls.ClsConnect();
                dt =
                    cls.LoadDataText("select * from QT_MS04TL where NG_CAPNHAT='" +
                                     dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "'");
                if (dt.Rows.Count>0)
                    MessageBox.Show("Đã có số liệu ngày : " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") + " từ TW", "Thông báo",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Chưa có số liệu ngày : " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") + " từ TW", "Thông báo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);

                #endregion
            }
        }
    }
}
