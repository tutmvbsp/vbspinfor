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
    /// Interaction logic for WpfDienbao.xaml
    /// </summary>
    public partial class WpfQt_Khtc : Window
    {
        public WpfQt_Khtc()
        {
            InitializeComponent();
        }

        private ClsServer cls = new ClsServer();
        private ServerInfor srv = new ServerInfor();
        private ToolBll bll = new ToolBll();
        DataTable dt= new DataTable();
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                const int thamso = 2;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Ngay";
                if (dtpNgay.SelectedDate != null) giatri[0] = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
                bien[1] = "@MaPos";
                giatri[1] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                //MessageBox.Show(giatri[0].ToString() + "   " + giatri[1].ToString());
                //dt = cls.LoadDataProcPara("usp_QT_KHTC", bien, giatri, thamso);
                cls.LoadDataProcPara("usp_QT_KHTC", bien, giatri, thamso);
                
                if (BienBll.NdMadv==BienBll.MainPos )
                {
                    if (bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) == "003000")
                    {
                        string str1 = "select * from TAM_KHTC where MAU='1' order by TT";
                        dt = cls.LoadDataText(str1);
                        rpt_QT_KHTC rpt = new rpt_QT_KHTC();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                            srv.DbPassSerVer());
                        string str2 = "select * from TAM_KHTC where MAU='2' order by TT";
                        dt = cls.LoadDataText(str2);
                        rpt_QT_KHTC rpt1 = new rpt_QT_KHTC();
                        RPUtility.ShowRp(rpt1, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                            srv.DbPassSerVer());
                    } else
                    {
                        string str1 = "select * from TAM_KHTC where MAU='1' order by TT";
                        dt = cls.LoadDataText(str1);
                        rpt_QT_KHTCHuyen rpt = new rpt_QT_KHTCHuyen();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        string str2 = "select * from TAM_KHTC where MAU='2' order by TT";
                        dt = cls.LoadDataText(str2);
                        rpt_QT_KHTCHuyen rpt1 = new rpt_QT_KHTCHuyen();
                        RPUtility.ShowRp(rpt1, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                    }

                }
                else
                {
                    string str1 = "select * from TAM_KHTC where MAU='1' order by TT";
                    dt = cls.LoadDataText(str1);
                    rpt_QT_KHTCHuyen rpt = new rpt_QT_KHTCHuyen();
                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                    string str2 = "select * from TAM_KHTC where MAU='2' order by TT";
                    dt = cls.LoadDataText(str2);
                    rpt_QT_KHTCHuyen rpt1 = new rpt_QT_KHTCHuyen();
                    RPUtility.ShowRp(rpt1, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());

                }
                
                cls.DongKetNoi();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lổi, liên hệ phòng tin học" + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                    
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
            //dtpNgay.SelectedDate = DateTime.Parse("30/09/" + DateTime.Now.ToString("yyyy"));
            dtpNgay.SelectedDate=DateTime.Parse(DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month).ToString());
            try
            {
                cls.ClsConnect();
                string sql = "";
                DataTable dtpos = new DataTable();
                if (BienBll.NdMadv == BienBll.MainPos)
                {
                    sql = "select PO_MA,PO_TEN from DMPOS order by PO_MA";
                }
                else
                {
                    sql = "select PO_MA,PO_TEN from DMPOS where PO_MA='"+BienBll.NdMadv+"'";
                }
                dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                CboPos.SelectedIndex = 5;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();

        }
        //
        private void btnNhapTay_Click(object sender, RoutedEventArgs e)
        {
            WpfQttcNhapTay f = new WpfQttcNhapTay();
            f.ShowDialog();
        }
        //
        private void btnManual_Click(object sender, RoutedEventArgs e)
        {
            string sql = "";
            bool ok = false;
            DateTime NgayDau = new DateTime();
            NgayDau = DateTime.Parse(DateTime.Now.AddYears(-1).ToString("yyyy")+"-12-31");
                #region
                if (Ration1.IsChecked == true)
                {
                    #region

                    if (RadioButton1.IsChecked == true) // lấy số liệu từ chương trình tự tính
                    {
                        #region
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
                                    NgayDau.ToString("dd/MM/yyyy"), "Thông báo", MessageBoxButton.OK,
                                    MessageBoxImage.Error);
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
                            //MessageBox.Show(NgayDau.ToString("yyyy-MM-dd"));
                            cls.ClsConnect();
                            sql = "Select * from LUU_PL04CTTW where ngay= '" + NgayDau.ToString("yyyy-MM-dd") + "'";
                            dt = cls.LoadDataText(sql);
                            if (dt.Rows.Count == 0)
                            {
                                MessageBoxResult Result =
                                    MessageBox.Show(
                                        "Chưa có số liệu ngày : " + NgayDau.ToString("dd/MM/yyyy") +
                                        " Có muốn tạo không ?",
                                        "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                                if (Result == MessageBoxResult.Yes)
                                {
                                    const int thamso = 1;
                                    string[] bien = new string[thamso];
                                    object[] giatri = new object[thamso];
                                    bien[0] = "@Ngay";
                                    giatri[0] = NgayDau.ToString("yyyy-MM-dd");
                                    cls.UpdateLdbf("usp_PL04CTTW", bien, giatri, thamso);
                                    MessageBox.Show("Tạo xong số liệu ngày : " + NgayDau.ToString("dd/MM/yyyy"),
                                        "Thông báo",
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

                        #endregion
                    }
                    else                               // lấy số liệu từ TW đồng bộ về
                    {
                        cls.ClsConnect();
                        sql = "Select * from QT_MS04TL where NG_CAPNHAT= '" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "'";
                        //MessageBox.Show(sql);
                        dt = cls.LoadDataText(sql);
                        if (dt.Rows.Count != 0) ok = true;
                        else
                        {
                            MessageBox.Show("Chưa có số liệu tử BDA chuyển về ngày :" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd"), "Thông báo", MessageBoxButton.OK,
                                MessageBoxImage.Information);
                            ok = false;
                        }

                    }
                    if (ok)
                    {
                        MessageBox.Show("Đã có đủ số liêu", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                        // doan nay chay store de luu gia tri vao VARMCN
                        try
                        {
                            cls.ClsConnect();
                            const int thamso = 2;
                            string[] bien = new string[thamso];
                            object[] giatri = new object[thamso];
                            bien[0] = "@Ngay";
                            //giatri[0] = NgayDau.ToString("yyyy-MM-dd");
                            giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                            bien[1] = "@NguonDl";
                            if (RadioButton1.IsChecked == true) giatri[1] = "1";
                            else giatri[1] = "2";
                            cls.UpdateDataProcPara("usp_PL04_01", bien, giatri, thamso);
                            //DataGrid.ItemsSource = dt.DefaultView;
                            MessageBox.Show("Đã nhận số liệu vào VARMCN ngày : " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"),
                                "Thông báo",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            cls.DongKetNoi();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Kiểm tra lại, chưa đủ số liệu", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }

                    #endregion
                }
                else if (Ration2.IsChecked == true)
                {
                    #region

                    //Xu ly phan thang 12 nam truoc
                    cls.ClsConnect();
                    sql = "Select * from LUU_PL03 where ngay= '" + NgayDau.ToString("yyyy-MM-dd") + "'";
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
                            giatri[0] = NgayDau.ToString("dd/MM/yyyy");
                            cls.UpdateLdbf("usp_PL03", bien, giatri, thamso);
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
                        //MessageBox.Show(NgayDau.ToString("yyyy-MM-dd"));
                        cls.ClsConnect();
                        sql = "Select * from LUU_PL03 where ngay= '" + NgayDau.ToString("yyyy-MM-dd") + "'";
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
                                giatri[0] = NgayDau.ToString("dd/MM/yyyy");
                                cls.UpdateLdbf("usp_PL03", bien, giatri, thamso);
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

                    #region

                    if (ok)
                    {
                        MessageBox.Show("Đã có đủ số liêu", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                        // doan nay chay store de luu gia tri vao VARMCN
                        try
                        {
                            cls.ClsConnect();
                            const int thamso = 1;
                            string[] bien = new string[thamso];
                            object[] giatri = new object[thamso];
                            bien[0] = "@Ngay";
                            giatri[0] = NgayDau.ToString("yyyy-MM-dd");
                            cls.UpdateDataProcPara("usp_PL03_01", bien, giatri, thamso);
                            // DataGrid.ItemsSource = dt.DefaultView;
                            MessageBox.Show("Đã nhận số liệu vào VARMCN ngày : " + NgayDau.ToString("dd/MM/yyyy"),
                                "Thông báo",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            cls.DongKetNoi();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Kiểm tra lại, chưa đủ số liệu", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }

                    #endregion

                    #endregion
                }
                else if (Ration3.IsChecked == true)
                {
                    #region

                    try
                    {
                        //cls.ClsConnect();
                        //sql = "Select * from LUU_PL05 where ngay= '" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "'";
                        //dt = cls.LoadDataText(sql);
                        //if (dt.Rows.Count == 0)
                        //{
                        cls.ClsConnect();
                        const int thamso = 2;
                        string[] bien = new string[thamso];
                        object[] giatri = new object[thamso];
                        bien[0] = "@Ngay";
                        giatri[0] = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
                        bien[1] = "@Mau";
                        giatri[1] = '1';
                        cls.UpdateLdbf("usp_PL05", bien, giatri, thamso);
                        MessageBox.Show(
                            "PL05-Tạo xong số liệu ngày : " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"),
                            "Thông báo",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        cls.DongKetNoi();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("PL05 Đã có số liệu ngày " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        // }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    #endregion
                }
                else if (Ration4.IsChecked == true)
                {
                    #region

                    try
                    {
                        int mm = dtpNgay.SelectedDate.Value.Month;
                        for (int i = 1; i <= mm; i++)
                        {
                            NgayDau = NgayDau.AddMonths(1);
                            NgayDau =
                                DateTime.Parse(NgayDau.ToString("yyyy-MM") + "-" +
                                               DateTime.DaysInMonth(NgayDau.Year, NgayDau.Month).ToString());
                            //MessageBox.Show(NgayDau.ToString("yyyy-MM-dd"));
                            cls.ClsConnect();
                            sql = "Select * from QT14 where ngay= '" + NgayDau.ToString("yyyy-MM-dd") + "'";
                            dt = cls.LoadDataText(sql);
                            if (dt.Rows.Count == 0)
                            {
                                MessageBoxResult Result =
                                    MessageBox.Show(
                                        "Chưa có số liệu ngày : " + NgayDau.ToString("dd/MM/yyyy") +
                                        " Có muốn tạo không ?",
                                        "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                                if (Result == MessageBoxResult.Yes)
                                {
                                    const int thamso = 1;
                                    string[] bien = new string[thamso];
                                    object[] giatri = new object[thamso];
                                    bien[0] = "@Ngay";
                                    giatri[0] = NgayDau.ToString("yyyy-MM-dd");
                                    cls.UpdateLdbf("usp_QT14", bien, giatri, thamso);
                                    MessageBox.Show("Tạo xong số liệu ngày : " + NgayDau.ToString("dd/MM/yyyy"),
                                        "Thông báo",
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
                            // doan nay chay store de luu gia tri vao VARMCN
                            try
                            {
                                cls.ClsConnect();
                                const int thamso = 1;
                                string[] bien = new string[thamso];
                                object[] giatri = new object[thamso];
                                bien[0] = "@Ngay";
                                giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                                cls.UpdateDataProcPara("usp_QT14_01", bien, giatri, thamso);
                                //DataGrid.ItemsSource = dt.DefaultView;
                                MessageBox.Show("Đã nhận số liệu vào VARMCN ngày : " + NgayDau.ToString("dd/MM/yyyy"),
                                    "Thông báo",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                                cls.DongKetNoi();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                            }

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
                else // phu luc 02
                {
                    #region

                    //Xu ly phan thang 12 nam truoc
                    cls.ClsConnect();
                    sql = "Select * from LUU_PL02 where ngay= '" + NgayDau.ToString("yyyy-MM-dd") + "'";
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
                            cls.UpdateLdbf("usp_PL02", bien, giatri, thamso);
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
                        //MessageBox.Show(NgayDau.ToString("yyyy-MM-dd"));
                        cls.ClsConnect();
                        sql = "Select * from LUU_PL02 where ngay= '" + NgayDau.ToString("yyyy-MM-dd") + "'";
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
                                cls.UpdateLdbf("usp_PL02", bien, giatri, thamso);
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
                        // doan nay chay store de luu gia tri vao VARMCN
                        try
                        {
                            cls.ClsConnect();
                            const int thamso = 1;
                            string[] bien = new string[thamso];
                            object[] giatri = new object[thamso];
                            bien[0] = "@Ngay";
                            giatri[0] = NgayDau.ToString("yyyy-MM-dd");
                            cls.UpdateDataProcPara("usp_PL02_01", bien, giatri, thamso);
                            //DataGrid.ItemsSource = dt.DefaultView;
                            MessageBox.Show("Đã nhận số liệu vào VARMCN ngày : " + NgayDau.ToString("dd/MM/yyyy"),
                                "Thông báo",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            cls.DongKetNoi();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Kiểm tra lại, chưa đủ số liệu", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }

                    #endregion
                }
                #endregion
        }
    }
}
