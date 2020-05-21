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
using DAL;
using BLL;
using System.Data;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfLaiTon.xaml
    /// </summary>
    public partial class WpfTTKH : Window
    {
        public WpfTTKH()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ToolBll bll  = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
            try
            {
                cls.ClsConnect();
                DataTable dtng = new DataTable();
                dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();

        }





        private void lblXem_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                DataTable dt = new DataTable();
                int thamso = 2;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Ngay";
                bien[1] = "@Makh";
                if (dtpNgay.SelectedDate == null)
                {
                    MessageBox.Show("Chưa chọn ngày", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                }
                if (txtMakh.Text == null)
                {
                    MessageBox.Show("Chưa nhập MAKH", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    giatri[1] = txtMakh.Text.Trim();
                }
                dt = cls.LoadDataProcPara("usp_TTKH", bien, giatri, thamso);
                if (dt.Rows.Count > 0)
                {

                    rpt_TTKH rpt = new rpt_TTKH();
                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                }
                else
                {
                    MessageBox.Show("Không có thông tin khách hàng !", "Thông báo");
                }
                cls.DongKetNoi();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }

        private void LblTimkiem_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            var f = new WpfTimKiem();
            f.ShowDialog();
        }

        private void LblThoat_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void txtTenKh_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                string strten = "select KH_MAKH,KH_TENKH,KH_CMT from hskh where KH_MAPGD='"+BienBll.NdMadv.Trim()+"' and KH_TENKH like N'%" + txtTenKh.Text.Trim() + "' order by KH_TENKH";
                //MessageBox.Show(strten);
                var dtten = cls.LoadDataText(strten);
                if (dtten == null)
                    MessageBox.Show("Không thấy !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    for (int i = 0; i < dtten.Rows.Count; i++)
                    {
                        CboTenKh.Items.Add(dtten.Rows[i][0] + " | " + dtten.Rows[i][1] + " | " + dtten.Rows[i][2]);
                    }

                    //CboTenKh.ItemsSource = dtten.DefaultView;
                    //CboTenKh.SelectedValuePath = "KH_MAKH";
                    //CboTenKh.DisplayMemberPath = "KH_TENKH";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }

        private void CboTenKh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtMakh.Text = bll.Left(CboTenKh.SelectedValue.ToString().Trim(),10);
        }
    }
}
