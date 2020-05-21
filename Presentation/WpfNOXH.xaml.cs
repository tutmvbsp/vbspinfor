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
    public partial class WpfNOXH : Window
    {
        public WpfNOXH()
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
                    int thamso = 4;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@Ngay";
                    if (dtpNgay.SelectedDate != null) giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    bien[1] = "@MaPos";
                    giatri[1] = str.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[2] = "@MaXa";
                    if (str.Right(CboPos.SelectedValue.ToString(), 2) != "00")
                        giatri[2] = str.Left(CboXa.SelectedValue.ToString().Trim(), 6);
                    else giatri[2] = "";
                    bien[3] = "@Mau";
                    if (str.Right(CboPos.SelectedValue.ToString(), 2) != "00" && str.Right(CboXa.SelectedValue.ToString(), 2)!="00")
                        giatri[3] = "1";
                    else if (str.Right(CboPos.SelectedValue.ToString(), 2) != "00" && str.Right(CboXa.SelectedValue.ToString(), 2) == "00") giatri[3] = "2";
                    if (str.Right(CboPos.SelectedValue.ToString(), 2) == "00") giatri[3] = "3";
                    dt = cls.LoadLdbf("usp_NOXH", bien, giatri, thamso);
                    if (dt.Rows.Count>0)
                        if (Ration1.IsChecked==true)
                        {
                            rpt_NOXH rpt = new rpt_NOXH();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                        }
                        else
                        {
                            FileName = Thumuc + "\\" + giatri[2] + "_" + giatri[3] + "_NOXH_" +
                                       dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                            str.ExportToExcel(dt, FileName);
                            MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK,
                                MessageBoxImage.Information);
                            str.OpenExcel(FileName);

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
                var dtpos = cls.LoadDataText("select PO_MA,PO_TEN from DMPOS order by PO_MA");
               CboPos.ItemsSource = dtpos.DefaultView;
               CboPos.DisplayMemberPath = "PO_TEN";
               CboPos.SelectedValuePath = "PO_MA";
               //CboPos.SelectedIndex = 0;
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
                cls.ClsConnect();
                string sql = "select MA,TEN from DMXA where right(PGD_QL,4)= '"+str.Right(CboPos.SelectedValue.ToString(),4)+ "' and TRANGTHAI='A' order by MA";
                var dtxa = cls.LoadDataText(sql);
                CboXa.ItemsSource = dtxa.DefaultView;
                CboXa.DisplayMemberPath = "TEN";
                CboXa.SelectedValuePath = "MA";
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
