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
    public partial class WpfSlVungBien : Window
    {
        public WpfSlVungBien()
        {
            InitializeComponent();
        }

        private ClsServer cls = new ClsServer();
        private ServerInfor srv = new ServerInfor();
        private ToolBll str = new ToolBll();
        private DataTable dtpos = new DataTable();
        private DataTable dt = new DataTable();
        private string FileName = "";
        string Thumuc = "C:\\SaoKe";

        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            #region

            try
            {
                if (str.Right(str.Left(CboPos.SelectedValue.ToString().Trim(), 6), 2) == "00")
                {
                    MessageBox.Show("Không chọn POS tổng hợp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    cls.ClsConnect();
                    if (Ration1.IsChecked == true)
                    {
                        int thamso = 2;
                        string[] bien = new string[thamso];
                        object[] giatri = new object[thamso];
                        bien[0] = "@NgayKu";
                        if (dtpNgayKu.SelectedDate != null) giatri[0] = dtpNgayKu.SelectedDate.Value.ToString("yyyy-MM-dd");
                        bien[1] = "@MaPos";
                        giatri[1] = str.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                        dt = cls.LoadLdbf("usp_SlVungBien", bien, giatri, thamso);
                        if (dt.Rows.Count > 0)
                        {
                            rpt_SlVungBien rpt = new rpt_SlVungBien();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        }
                        else
                        {
                            MessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else if (Ration2.IsChecked==true)
                    {
                        int thamso1 = 3;
                        string[] bien = new string[thamso1];
                        object[] giatri = new object[thamso1];
                        bien[0] = "@NgayKu";
                        if (dtpNgayKu.SelectedDate != null) giatri[0] = dtpNgayKu.SelectedDate.Value.ToString("yyyy-MM-dd");
                        bien[1] = "@MaPos";
                        giatri[1] = str.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                        bien[2] = "@MaXa";
                        giatri[2] = str.Left(cboXa.SelectedValue.ToString().Trim(), 6);
                        dt = cls.LoadLdbf("usp_SlVungBienCT", bien, giatri, thamso1);
                        if (dt.Rows.Count > 0)
                        {
                            rpt_SlVungBienCt rpt = new rpt_SlVungBienCt();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        }
                        else
                        {
                            MessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                    } else
                    {
                        int thamso2 = 1;
                        string[] bien = new string[thamso2];
                        object[] giatri = new object[thamso2];
                        bien[0] = "@NgayKu";
                        if (dtpNgayKu.SelectedDate != null) giatri[0] = dtpNgayKu.SelectedDate.Value.ToString("yyyy-MM-dd");
                        dt = cls.LoadLdbf("usp_SlVungBienTH", bien, giatri, thamso2);
                        if (dt.Rows.Count > 0)
                        {
                            FileName = Thumuc + "\\" + str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_SoLieuXaVungBien_" + dtpNgayKu.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                            str.ExportToExcel(dt, FileName);
                            //bll.ExportDTToExcel(dt,FileName);
                            //bll.WriteDataTableToExcel(dt, "Details", FileName, "tutm : 0985165777");
                            //bll.ToCSV(dt, sw, true);
                            MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            //bll.OpenCSVWithExcel(FileName);
                            str.OpenExcel(FileName);

                        }
                        else
                        {
                            MessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();

            #endregion
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
            try
            {
                cls.ClsConnect();
                string sql = "select PO_MA,PO_TEN from DMPOS order by PO_MA";
                dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                //CboPos.SelectedIndex = 5;
                DataTable dtng = new DataTable();
                dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGKU,MAX(convert(date,NGAYBT,105)) as NGBT from U_HSTD");
                dtpNgayKu.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGKU"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Mess",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }

        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //MessageBox.Show(str.Left(cboPos.SelectedValue.ToString().Trim(),6));
                cboXa.Items.Clear();
                cls.ClsConnect();
                DataTable dtxa = new DataTable();
                string sql = "select MA,TEN from DMXA where PGD_QL= " + "'" + str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "' and Vung_Bien='1'" + " order by MA";
                dtxa = cls.LoadDataText(sql);
                for (int i = 0; i < dtxa.Rows.Count; i++)
                {
                    cboXa.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                }
                cboXa.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }

        private void Ration1_Checked(object sender, RoutedEventArgs e)
        {
            lblXa.IsEnabled = false;
            cboXa.IsEnabled = false;
        }

        private void Ration2_Checked(object sender, RoutedEventArgs e)
        {
            lblXa.IsEnabled = true;
            cboXa.IsEnabled = true;

        }

        private void Ration3_Checked(object sender, RoutedEventArgs e)
        {
            CboPos.SelectedIndex = 5;
        }
  
    }
}
