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
    public partial class WpfKSM01 : Window
    {
        public WpfKSM01()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ToolBll str = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        string Thumuc = "C:\\Saoke";
        private string sql = "";
        private string FileName = "";
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
   
            cls.DongKetNoi();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
                                    "insert into LUU_KS01(NAM,NGAY,TO_MATO,TT,ChiTieu,DV,CT01,CT19,CT09,CT10,CT11,CT06,CT02,CT04,CT03,CT15,CT18,CT07,GC)" +
                                    " Values('" + dtpNgay.SelectedDate.Value.ToString("yyyy") + "','" +
                                    dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "','" + dr["TO_MATO"] +
                                    "','" + dr["TT"] + "',N'" + dr["ChiTieu"] + "',N'" + dr["DV"] + "','" + dr["CT01"] +
                                    "','" + dr["CT19"] + "','" + dr["CT09"] + "','" + dr["CT10"] + "','" + dr["CT11"] +
                                    "','" + dr["CT06"] + "','" + dr["CT02"] + "','" + dr["CT04"] +
                                    "','" + dr["CT03"] + "','" + dr["CT15"] + "','" + dr["CT18"] + "','" + dr["CT07"] +
                                    "','" + dr["GC"] + "')";
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
                                        "update LUU_KS01 set CT01=" + dr["CT01"] +
                                        ",CT19=" + dr["CT19"] + ",CT09=" + dr["CT09"] + "" +
                                        ",CT10=" + dr["CT10"] + ",CT11=" + dr["CT11"] + ",CT06=" + dr["CT06"] + "" +
                                        ",CT02=" + dr["CT02"] + ",CT04=" + dr["CT04"] + ",CT03=" + dr["CT03"] + "" +
                                        ",CT15=" + dr["CT15"] + ",CT18=" + dr["CT18"] + ",CT07=" + dr["CT07"] +
                                        " where TO_MATO='" + str.Left(CboMaTo.SelectedValue.ToString().Trim(), 7)
                                        + "' and NAM=" + dtpNgay.SelectedDate.Value.ToString("yyyy")+" and TT="+dr["TT"];
                                    cls.LoadDataText(strluu);
                                }
                            }
                        
                    }
                    if (upda==false) MessageBox.Show("Đã lưu thành công " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    else MessageBox.Show("Cập nhật thành công " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    dgvSource.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
            
        }

        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //MessageBox.Show(str.Left(cboPos.SelectedValue.ToString().Trim(),6));
                if (str.Left(CboPos.SelectedValue.ToString().Trim(), 6) != "003000")
                {
                    CboXa.Items.Clear();
                    cls.ClsConnect();
                    string sql = "select MA,TEN from DMXA where PGD_QL= " + "'" +
                                 str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "' and right(MA,2)<>'00' order by MA";
                    var dtxa = cls.LoadDataText(sql);
                    for (int i = 0; i < dtxa.Rows.Count; i++)
                    {
                        CboXa.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                    }
                }
                else
                {
                   // CboXa.Items.Add("003000 | Tất cả");
                    MessageBox.Show("Không chọn POS 003000", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
               // CboXa.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }


      

        private void CboXa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (str.Left(CboPos.SelectedValue.ToString().Trim(), 6) != "003000")
                {
                    cls.ClsConnect();
                    sql = "select TO_MATO,TO_TENTT from HSTO where LEFT(TO_MADP,6)=" + str.Left(CboXa.SelectedValue.ToString().Trim(), 6) + " and TRANGTHAI='A' order by TO_MATO";
                   // MessageBox.Show(sql);
                    var dtxa = cls.LoadDataText(sql);
                    for (int i = 0; i < dtxa.Rows.Count; i++)
                    {
                        CboMaTo.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                    }
                }
                else
                {
                   // CboXa.Items.Add("003000 | Tất cả");
                    MessageBox.Show("Không chọn POS 003000", "Mess",MessageBoxButton.OK,MessageBoxImage.Warning);
                }
               // CboXa.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
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
                    cls.LoadDataText("delete from LUU_KS01 where TO_MATO='" +
                                         str.Left(CboMaTo.SelectedValue.ToString().Trim(), 7) + "' and NAM=" + dtpNgay.SelectedDate.Value.ToString("yyyy"));
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

        private void CboMaTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                upda = false;
                cls.ClsConnect();
                if (dtpNgay.SelectedDate != null)
                {
                    var dtchk =
                        cls.LoadDataText("select * from LUU_KS01 where TO_MATO='" +
                                         str.Left(CboMaTo.SelectedValue.ToString().Trim(), 7) + "' and NAM="+ dtpNgay.SelectedDate.Value.ToString("yyyy"));
                    if (dtchk.Rows.Count > 0)
                    {
                        sql = "select * from LUU_KS01 where TO_MATO='" +
                              str.Left(CboMaTo.SelectedValue.ToString().Trim(), 7) + "' and NAM=" + dtpNgay.SelectedDate.Value.ToString("yyyy");
                        upda = true;
                    }
                    else if (dtpNgay.SelectedDate != null)
                        sql = "select DATEPART(YYYY," + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + ") NAM," +
                              dtpNgay.SelectedDate.Value.ToString("yyyy - MM - dd") +
                              " NGAY,b.TO_MATO,a.* from KS01 a,HSTO b where b.to_mato='" +
                              str.Left(CboMaTo.SelectedValue.ToString().Trim(), 7) + "' and b.TRANGTHAI='A'";
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
    }
}
