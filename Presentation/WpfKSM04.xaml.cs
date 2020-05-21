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
using System.IO;
using DAL;
using BLL;
using System.Data;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDvut.xaml
    /// </summary>
    public partial class WpfKSM04 : Window
    {
        public WpfKSM04(string Mau,string CT)
        {
            InitializeComponent();
            _mau = Mau;
            _ct = CT;
        }
        ClsServer cls = new ClsServer();
        ToolBll str = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        string Thumuc = "C:\\Saoke";
        private string sql = "";
        private string _mau = "";
        private string _ct = "";
        private bool upda = false;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
            dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
            //string sql = "select PO_MA,PO_TEN from DMPOS where PO_MACN=" + "'" + BienBll.MainPos + "'" + " order by PO_MA";
            //var sql = BienBll.NdCapbc.Trim() == "1" ? string.Format("select PO_MA,PO_TEN from DMPOS where PO_MA='{0}'", BienBll.NdMadv.Trim()) : "select PO_MA,PO_TEN from DMPOS where right(PO_MA,2)<>'00'";
            sql = "select PO_MA,PO_TEN from DMPOS where PO_MA='" + BienBll.NdMadv.Trim() + "'";
            var dtpos = cls.LoadDataText(sql);
            for (int i = 0; i < dtpos.Rows.Count; i++)
            {
                CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
            }
            CboPos.SelectedIndex = 0;
            cls.DongKetNoi();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void LblGetData_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                upda = false;
                cls.ClsConnect();
                if (dtpNgay.SelectedDate != null)
                {
                    var dtchk =
                        cls.LoadDataText("select * from KHAOSAT where  MAU='"+_mau+"' and POS='" +
                                        str.Right(str.Left(CboPos.SelectedValue.ToString().Trim(), 6),4) + "' and NAM=" + dtpNgay.SelectedDate.Value.ToString("yyyy"));
                    if (dtchk.Rows.Count > 0)
                    {
                        sql = "select * from KHAOSAT where MAU='" + _mau + "' and POS='" +
                              str.Right(str.Left(CboPos.SelectedValue.ToString().Trim(), 6), 4) + "' and NAM=" + dtpNgay.SelectedDate.Value.ToString("yyyy");
                        upda = true;
                    }
                    else if (dtpNgay.SelectedDate != null)
                        sql = "select '" + _mau + "' MAU," + dtpNgay.SelectedDate.Value.ToString("yyyy") + " NAM," + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + " NGAY" +
                              "," + str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + " POS,'" + _ct + "' CHTR,b.MA MAXA" +
                              ",ROW_NUMBER() OVER (Order by b.MA) COT1,b.TEN COT2,cast(0 as NUMERIC(18,0)) COT3,cast(0 as NUMERIC(18,0)) COT4" +
                              ",cast(0 as NUMERIC(18,0)) COT5 ,cast(0 as NUMERIC(18, 0)) COT6,cast(0 as NUMERIC(18, 0)) COT7,cast(0 as NUMERIC(18, 0)) COT8" +
                              ",cast(0 as NUMERIC(18, 0)) COT9,cast(0 as NUMERIC(18, 0)) COT10,cast(0 as NUMERIC(18, 0)) COT11,cast(0 as NUMERIC(18, 0)) COT12" +
                              ",cast(0 as NUMERIC(18, 0)) COT13,cast(0 as NUMERIC(18, 0)) COT14 from DMXA b " +
                              " where b.PGD_QL='" + str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "' and right(b.MA,2)<>'00'";
                }
                dt = cls.LoadDataText(sql);
                if (dt.Rows.Count > 0)
                    dgvSource.ItemsSource = dt.DefaultView;
                else
                    MessageBox.Show("Chưa có số liệu", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();


        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                str.TaoThuMuc(Thumuc);
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Chưa có dữ liệu !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    cls.ClsConnect();
                    if (upda == false)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dtpNgay.SelectedDate != null)
                            {
                                string strluu =
                                    "insert into KHAOSAT(MAU,NAM,NGAY,POS,CHTR,MAXA,COT1,COT2,COT3,COT4,COT5,COT6,COT7,COT8,COT9,COT10,COT11,COT12,COT13,COT14)" +
                                    " Values('"+_mau+"','" + dtpNgay.SelectedDate.Value.ToString("yyyy") + "','" +
                                    dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "',N'" + dr["POS"] +
                                    "','" + dr["CHTR"] + "',N'" + dr["MAXA"] +"',"+dr["COT1"] + ",N'" + dr["COT2"] 
                                    + "'," + dr["COT3"] + "," + dr["COT4"] + "," + dr["COT5"] + "," + dr["COT6"] + "," + dr["COT7"]
                                    + "," + dr["COT8"] + "," + dr["COT9"] + "," + dr["COT10"] + "," + dr["COT11"]
                                    + "," + dr["COT12"] + "," + dr["COT13"] + "," + dr["COT14"]+ ")";
                                  cls.LoadDataText(strluu);
                            }
                        }
                    }
                    else
                    {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (dtpNgay.SelectedDate != null)
                                {
                                    string strluu =
                                        "update KHAOSAT set COT3=" + dr["COT3"] + "" +
                                        ",COT4=" + dr["COT4"] + ",COT5=" + dr["COT5"] + ",COT6=" + dr["COT6"] + "" +
                                        ",COT7=" + dr["COT7"] + ",COT8=" + dr["COT8"] + ",COT9=" + dr["COT9"] + "" +
                                        ",COT10=" + dr["COT10"] + ",COT11=" + dr["COT11"] + ",COT12=" + dr["COT12"] +
                                        ",COT13="+ dr["COT13"]+",COT14 = "+ dr["COT14"]+
                                        " where MAU='"+_mau+"' and NAM=" + dtpNgay.SelectedDate.Value.ToString("yyyy")+" and MAXA='"+dr["MAXA"]+"'";
                                    cls.LoadDataText(strluu);
                                }
                            }
                        
                    }
                    if (upda==false) MessageBox.Show("Đã lưu thành công " , "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    else MessageBox.Show("Cập nhật thành công " , "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    dgvSource.ItemsSource = null;
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
            try
            {
                if (upda == true)
                {
                    cls.ClsConnect();
                    if (dtpNgay.SelectedDate != null)
                        cls.LoadDataText("delete from KHAOSAT where MAU='M01' and POS='" +
                                         str.Right(str.Left(CboPos.SelectedValue.ToString().Trim(), 6), 4) + "' and NAM=" + dtpNgay.SelectedDate.Value.ToString("yyyy"));
                    MessageBox.Show("Đã xóa !", "Thông báo",MessageBoxButton.OK,MessageBoxImage.Information);
                    dgvSource.ItemsSource = null;
                } else MessageBox.Show("Chưa có dữ liệu lưu để xóa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();
        }

 
    }
}
