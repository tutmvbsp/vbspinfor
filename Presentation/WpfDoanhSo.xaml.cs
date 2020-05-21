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
    public partial class WpfDoanhSo : Window
    {
        public WpfDoanhSo()
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
                    cls.ClsConnect();
                    int thamso = 6;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@Ngay";
                    if (dtpNgay.SelectedDate != null) giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    bien[1] = "@MaPos";
                    giatri[1] = str.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[2] = "@MaXa";
                    giatri[2] = str.Left(CboXa.SelectedValue.ToString().Trim(), 6);
                    bien[3] = "@Ky";
                    if (Ration6.IsChecked == true)
                    {
                        giatri[3] = "1";
                    }
                    else if (Ration7.IsChecked == true)
                    {
                        giatri[3] = "2";
                    }
                    else
                    {
                        giatri[3] = "3";
                    }

                    bien[4] = "@Nguon";
                    if (Ration3.IsChecked == true)
                    {
                        giatri[4] = "1";
                    }
                    else if (Ration4.IsChecked == true)
                    {
                        giatri[4] = "2";
                    }
                    else
                    {
                        giatri[4] = "3";
                    }
                    bien[5] = "@Mau";
                    if (Ration1.IsChecked==true) giatri[5]="1";
                    else giatri[5] = "2";
                    dt = cls.LoadLdbf("usp_DoanhSo", bien, giatri, thamso);
                    if (dt.Rows.Count>0)
                        if (Ration9.IsChecked==true)
                        {
                            rpt_DoanhSo rpt = new rpt_DoanhSo();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                        }
                        else
                        {
                            FileName = Thumuc + "\\" + giatri[2] + "_" + giatri[3] + "_SLTD_XA_" +
                                       dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                            str.ExportToExcel(dt, FileName);
                            MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK,
                                MessageBoxImage.Information);
                            //str.OpenExcel(FileName);

                        }
                    else
                    {
                            MessageBox.Show("Không có số liệu", "Thông báo", MessageBoxButton.OK,MessageBoxImage.Warning);
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
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGKU"].ToString());
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
                if (str.Left(CboPos.SelectedValue.ToString().Trim(), 6) != "003000")
                {
                    CboXa.Items.Clear();
                    cls.ClsConnect();
                    DataTable dtxa = new DataTable();
                    string sql = "select MA,TEN from DMXA where PGD_QL= " + "'" +
                                 str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "'" + " order by MA";
                    dtxa = cls.LoadDataText(sql);
                    for (int i = 0; i < dtxa.Rows.Count; i++)
                    {
                        CboXa.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                    }
                }
                else
                {
                    CboXa.Items.Add("003000 | Tất cả");
                }
                CboXa.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }
  
    }
}
