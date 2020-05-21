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
    /// Interaction logic for WpfKhGnTn.xaml
    /// </summary>
    public partial class WpfKhGnTn : Window
    {
        public WpfKhGnTn()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ToolBll str = new ToolBll();
        DataTable dtpos = new DataTable();
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadXa();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                UpdateTableLuu("usp_UpdateLuuKhGnTn");
                UpdateKhac();
                MessageBox.Show("OK","Mess");

            }
            catch(Exception ex)
            {
                MessageBox.Show("Lổi : " + ex.Message);
            }
        }

        private void LoadXa()
        {
            try
            {
                cls.ClsConnect();
                int thamso = 2;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Ngay";
                if (dtpNgay.SelectedDate != null) giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                bien[1] = "@MaPos";
                giatri[1] = str.Left(CboPos.SelectedValue.ToString(), 6);
                dt = cls.LoadDataProcPara("usp_KH_GN_TN", bien, giatri, thamso);
                CboXa.ItemsSource = dt.DefaultView;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnChon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dtNew = dt.Clone();
                foreach (DataRow dr in dt.Rows)
                {
                    if ((bool)dr[0] == true)
                    {
                        dtNew.ImportRow(dr);
                    }
                }
                InsertToTableLuu("usp_Insert_GNTNTL");
                if (dtNew == null || dtNew.Rows.Count == 0)
                {
                    MessageBox.Show("Chưa chọn xã nào ", "Mess");
                }
                else
                {
                    dgvTarGet.ItemsSource = dtNew.DefaultView;
                    //dataGrid1.ItemsSource = dtNew.DefaultView;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void InsertToTableLuu (String str)
            {
            try
            {
                foreach (DataRow dr in dtNew.Rows)
                {
                    int thamso = 15;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@Ngay";
                    giatri[0] = dr[1];
                    bien[1] = "@Nam";
                    giatri[1] = dr[2];
                    bien[2] = "@Quy";
                    giatri[2] = dr[3];
                    bien[3] = "@Thang";
                    giatri[3] = dr[4];
                    bien[4] = "@Tuan";
                    giatri[4] = dr[5];
                    bien[5] = "@Thu";
                    giatri[5] = dr[6];
                    bien[6] = "@MonthOfWeek";
                    giatri[6] = dr[7];
                    bien[7] = "@Maxa";
                    giatri[7] = dr[8];
                    bien[8] = "@Tenxa";
                    giatri[8] = dr[9];
                    bien[9] = "@Giaingan";
                    giatri[9] = dr[10];
                    bien[10] = "@ChiKhac";
                    giatri[10] = dr[11];
                    bien[11] = "@ThuNo";
                    giatri[11] = dr[12];
                    bien[12] = "@ThuLai";
                    giatri[12] = dr[13];
                    bien[13] = "@ThuTK";
                    giatri[13] = dr[14];
                    bien[14] = "@ThuKhac";
                    giatri[14] = dr[15];
                    //MessageBox.Show(dr[1] + "  " + dr[14]);
                    DataTable kt = new DataTable();
                    string sql = "select * from LuuKhGnTn where Ngay = " + "'" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and Maxa= '" +
                                 dr[8].ToString().Trim() + "'";

                    kt = cls.LoadDataText(sql);
                    if (kt.Rows.Count > 0)
                    {
                        MessageBox.Show("Đã có dữ liệu xã : " + dr[8].ToString().Trim() + "  Chọn sửa dữ liệu ",
                                        "Mess");
                    }
                    else
                    {
                        cls.UpdateDataProcPara(str, bien, giatri, thamso);
                    }
                }

                }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            }

        private void UpdateTableLuu(String str)
        {
            try
            {
                foreach (DataRow dr in dtNew.Rows)
                {
                    int thamso = 8;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@Ngay";
                    giatri[0] = dr[1];
                    bien[1] = "@Maxa";
                    giatri[1] = dr[8];
                    bien[2] = "@Giaingan";
                    giatri[2] = dr[10];
                    bien[3] = "@ChiKhac";
                    giatri[3] = dr[11];
                    bien[4] = "@ThuNo";
                    giatri[4] = dr[12];
                    bien[5] = "@ThuLai";
                    giatri[5] = dr[13];
                    bien[6] = "@ThuTK";
                    giatri[6] = dr[14];
                    bien[7] = "@ThuKhac";
                    giatri[7] = dr[15];
                    //MessageBox.Show("Xa "+ giatri[1]+"  GN : "+giatri[2]+ "Chi khac : "+giatri[3]);
                    cls.UpdateDataProcPara(str, bien, giatri, thamso);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mess");
            }
        }


        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dtNew = dt.Clone();
                foreach (DataRow dr in dt.Rows)
                {
                    if ((bool)dr[0] == true)
                    {
                        dtNew.ImportRow(dr);
                    }
                }
                dgvTarGet.ItemsSource = dtNew.DefaultView;
                //dataGrid1.ItemsSource = dtNew.DefaultView;
                if (dtNew.Rows.Count > 0)
                {
                    cls.ClsConnect();
                    foreach (DataRow dr in dtNew.Rows)
                    {
                        string sql = "delete from LuuKhGnTn where ngay = " + "'" +
                                         dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and maxa= '" +
                                         dr[8].ToString().Trim() + "'";
                        //MessageBox.Show(sql);
                        cls.UpdateDataText(sql);
                        //MessageBox.Show("Delete OK xã " + dr[8].ToString().Trim(), "Mess");
                    }
                    MessageBox.Show("Delete OK", "Mess");
                    dgvTarGet.ItemsSource = dtNew.DefaultView;
                    //dataGrid1.ItemsSource = dtNew.DefaultView;
                    dgvTarGet.Items.Refresh();
                    cls.DongKetNoi();
                }
                else
                {
                    MessageBox.Show("Chưa chọn xã để xóa", "Mess");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            LoadXaLuu();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            dtNew = null;
            dgvTarGet.ItemsSource = null;
            dgvTarGet.Items.Refresh();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          
                dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
                //MessageBox.Show(str.Left(str.Right(dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"),8),1));
                string kitu = str.Left(str.Right(dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"), 8), 1);
                //MessageBox.Show(kitu);
                if (str.Left(str.Right(dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"), 8), 1)!="/")
                {
                    MessageBox.Show("Vào Control Panel định dạng lại ngày tháng theo  dd/MM/yyyy","Eror",MessageBoxButton.OK,MessageBoxImage.Error);
                    //WpfKhGnTn f = new WpfKhGnTn();
                    this.Close();
                   // return;
                }
                CboPos.SelectedIndex = 5;
                try
                {
                    cls.ClsConnect();
                    //string sql = "select PO_MA,PO_TEN from DMPOS ";
                    string sql = "select PO_MA,PO_TEN from DMPOS where PO_MA= '" + BienBll.NdMadv + "'";
                    dtpos = cls.LoadDataText(sql);
                    for (int i = 0; i < dtpos.Rows.Count; i++)
                    {
                        CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
                }
                cls.DongKetNoi();
        }

        private void UpdateKhac()
        {
            try
            {
                cls.ClsConnect();
                string sql = "update LuuKhGnTn set GiaiNgan=ISNULL(GiaiNgan,0),ChiKhac=ISNULL(ChiKhac,0),ThuNo=ISNULL(ThuNo,0),ThuLai=ISNULL(ThuLai,0),ThuTK=ISNULL(ThuTK,0),ThuKhac=ISNULL(ThuKhac,0)";
                string sql1 = "update LuuKhGnTn set TongChi=GiaiNgan+ChiKhac, TongThu=ThuNo+ThuLai+ThuTK+ThuKhac";
                string sql2 = "update LuuKhGnTn set ChenhLech=TongThu-TongChi,MaPos=left(Maxa,4),Chon=0";
                cls.UpdateDataText(sql);
                cls.UpdateDataText(sql1);
                cls.UpdateDataText(sql2);
                cls.DongKetNoi();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Lổi  " + ex.Message, "Mess");
            }

        }

        private void LoadXaLuu()
        {
            try
            {
                cls.ClsConnect();
                string Ngay = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                string MaPos = str.Right(str.Left(CboPos.SelectedValue.ToString(), 6), 4);
                string sql = "select * from LuuKhGnTn where Ngay = '" + Ngay + "' and Mapos = '" + MaPos + "' order by MAXA";
                //MessageBox.Show(sql);
                dtNew = cls.LoadDataText(sql);
                //CboXa.ItemsSource = dt.DefaultView;
                dgvTarGet.ItemsSource = dtNew.DefaultView;
                //dataGrid1.ItemsSource = dtNew.DefaultView;
                dgvTarGet.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lổi  " + ex.Message, "Mess");
            }
        }

    }
}
