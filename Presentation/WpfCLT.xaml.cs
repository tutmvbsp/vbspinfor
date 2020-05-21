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
    public partial class WpfClt : Window
    {
        public WpfClt()
        {
            InitializeComponent();
        }

        private ClsServer cls = new ClsServer();
        private ServerInfor srv = new ServerInfor();
        private ToolBll str = new ToolBll();
        private DataTable dt = new DataTable();

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
                    int thamso = 4;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@Ngay";
                    if (dtpNgay.SelectedDate != null) giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    bien[1] = "@MaPos";
                    giatri[1] = str.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[2] = "@MaXa";
                    giatri[2] = str.Left(CboXa.SelectedValue.ToString().Trim(), 6);
                    bien[3] = "@Mau";
                    if (Ration1.IsChecked==true) giatri[3]="1";
                    else if (Ration2.IsChecked == true) giatri[3] = "2";
                    else giatri[3] = "3"; 
                    dt = cls.LoadLdbf("AA_DULIEU_TOXADVUT", bien, giatri, thamso);
                    if (dt.Rows.Count>0)
                        { 
                            //MessageBox.Show("OK", "Thông báo", MessageBoxButton.OK,MessageBoxImage.Information);
                            rpt_CLT01 rpt = new rpt_CLT01();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
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
                var dtpos = cls.LoadDataText("select PO_MA, PO_TEN from DMPOS order by PO_MA");
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGKU,MAX(convert(date,NGAYBT,105)) as NGBT from U_HSTD");
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
                    string sql = "select MA,TEN from DMXA where PGD_QL= " + "'" +
                                 str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "'" + " order by MA";
                    var dtxa = cls.LoadDataText(sql);
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
