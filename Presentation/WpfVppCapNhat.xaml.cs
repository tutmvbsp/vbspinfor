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
using System.Globalization;
using DAL;
using BLL;
using System.Data;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDvut.xaml
    /// </summary>
    public partial class WpfVppCapNhat : Window
    {
        public WpfVppCapNhat()
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
 
        private string str = "";
 


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
   
    
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
            //MessageBox.Show(bll.Left(comboBoxMonth.SelectedValue.ToString().Trim(),2));
            //MessageBox.Show(comboBoxYear.SelectedValue.ToString().Trim());
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                if (TxtMa.Text.Trim() == "")
                    MessageBox.Show("Hãy chọn sản phẩm cần thay đổi !", "Thông báo", MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                else
                {
                    string strkt = "select mapos, nd_ma from dmvpp where ma='" + TxtMa.Text.Trim() + "'";
                    var dtkt = cls.LoadDataText(strkt);
                    if (dtkt.Rows.Count > 0)
                    {
                        DataRow dtr = dtkt.Rows[0];
                        string ndma = (string) dtr["ND_MA"];
                        string mapos = (string) dtr["MAPOS"];
                        if (BienBll.NdMadv == mapos && BienBll.Ndma == ndma)
                        {

                            string upd = "update DMVPP set G" + bll.Right(BienBll.NdMadv.Trim(), 2) + "=" +
                                         TxtGia.Text.Trim()
                                         + ",TEN=N'" + TxtTen.Text.Trim() + "',DONVI=N'" + TxtDv.Text.Trim() +
                                         "',QUYCACH=N'" + TxtQc.Text.Trim() + "' where MA='" + TxtMa.Text.Trim() + "'";
                            // MessageBox.Show(upd);
                            cls.UpdateDataText(upd);
                            MessageBox.Show("Cập nhật thành công sản phẩm : " + TxtTen.Text, "Thông báo",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                            MessageBox.Show("Bạn không có quyền cập nhật thông tin của đơn vị hoặc người khác");
                    }
                }

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
                str= "select MA, TEN,DONVI,G" + bll.Right(BienBll.NdMadv.Trim(), 2) + " DONGIA,QUYCACH from DMVPP order by MA,TEN";
                dt = cls.LoadDataText(str);
                if (dt.Rows.Count > 0)
                {
                    dgvSource.ItemsSource = dt.DefaultView;
                }
                else MessageBox.Show("Không có bản ghi nào !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();

        }

        private void BtnThem_OnClick(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            if (TxtMa.Text != "" || TxtTen.Text != "" || TxtDv.Text != "" || TxtGia.Text != "")
            {
                try
                {
                   
                    string sql = "select * from DMVPP where MA='" + TxtMa.Text.Trim() + "'";
                    //MessageBox.Show(sql);
                    var dtkt = cls.LoadDataText(sql);
                    if (dtkt.Rows.Count > 0)
                    {
                        MessageBox.Show("Mã : " + TxtMa.Text.Trim() + " đã tồn tại", "Thông báo",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }
                    else
                    {
                        var ttmax = cls.LoadDataText("select max(TT) TTMAX from DMVPP");
                        int tt = int.Parse(ttmax.Rows[0]["TTMAX"].ToString())+1;
                        TxtMa.Text = tt.ToString();
                        string sqladd =
                            "insert into DMVPP(TT,MA,NGAY,THANG,NAM,TEN,DONVI,DONGIA,SOLUONG,G01,G02,G03,G04,G05,G06,G07,G08,QUYCACH,MAPOS,ND_MA)" +
                            " Values('" + tt + "',N'" + TxtMa.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "',N'" +
                            DateTime.Now.ToString("MM") + "','" + DateTime.Now.ToString("yyyy") + "',N'" + TxtTen.Text.Trim() +
                            "',N'" + TxtDv.Text.Trim() + "','" + TxtGia.Text.Trim() + "',0,0,0,0,0,0,0,0,0,N'" + TxtQc.Text.Trim() + "','" + BienBll.NdMadv + "','" + BienBll.Ndma + "')";
                         //MessageBox.Show(sqladd);
                        cls.UpdateDataText(sqladd);
                        MessageBox.Show("Đã thêm " + TxtTen.Text + " danh mục VPP !", "Thông báo",MessageBoxButton.OK, MessageBoxImage.Information);
                        // MessageBox.Show(tt.ToString());

                    }
                    cls.DongKetNoi();
                    ClearAll();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi, kiểm tra lại thông tin " + ex.Message, "Thông báo", MessageBoxButton.OK,
                        MessageBoxImage.Error);

                }

            }
            else
            {
                MessageBox.Show("Nhập UserName ", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            cls.DongKetNoi();
           // LblGetData_OnMouseDown(null,null);
        }

        private void BtnXoa_OnClick(object sender, RoutedEventArgs e)
        {
            if (TxtMa.Text != "")
            {
                try
                {
                    cls.ClsConnect();
                    string strkt = "select mapos, nd_ma from dmvpp where ma='" + TxtMa.Text.Trim() + "'";
                    var dtkt = cls.LoadDataText(strkt);
                    if (dtkt.Rows.Count > 0)
                    {
                        DataRow dtr = dtkt.Rows[0];
                        string ndma = (string) dtr["ND_MA"];
                        string mapos = (string) dtr["MAPOS"];
                        if (BienBll.NdMadv == mapos && BienBll.Ndma==ndma)
                        {
                            string sqldele = "delete from DMVPP where MA='" + TxtMa.Text.Trim() + "' and MAPOS='" +
                                             BienBll.NdMadv + "' and ND_MA='" + BienBll.Ndma + "'";
                            cls.UpdateDataText(sqldele);
                            MessageBox.Show("Đã Xóa " + TxtTen.Text + " !", "Thông báo", MessageBoxButton.OK,
                                MessageBoxImage.Information);
                        }
                        else
                            MessageBox.Show("Bạn không có quyền xóa thông tin của đơn vị hoặc người khác");
                    }
                    cls.DongKetNoi();
                    ClearAll();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi, kiểm tra lại thông tin " + ex.Message, "Thông báo", MessageBoxButton.OK,
                        MessageBoxImage.Error);

                }

            }
            else
            {
                MessageBox.Show("Click đúp để chọn mã ", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //LblGetData_OnMouseDown(null, null);
        }

        private void dgvSource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    //DataRow dtr = dt.Rows[0];
                    //DataRow dr = (DataRow) dgvData.SelectedItems[0];
                    DataRowView dr = (DataRowView)dgvSource.SelectedItems[0];
                    TxtMa.Text = dr["MA"].ToString();
                    TxtTen.Text = dr["TEN"].ToString();
                    TxtDv.Text = dr["DONVI"].ToString();
                    TxtGia.Text = dr["DONGIA"].ToString();
                    TxtQc.Text = dr["QUYCACH"].ToString();
                }
                else
                {
                    MessageBox.Show("Không có bản ghi nào !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ClearAll()
        {
            TxtMa.Text = "";
            TxtTen.Text = "";
            TxtDv.Text = "";
            TxtGia.Text = "";
            TxtQc.Text = "";
        }


    }
}
