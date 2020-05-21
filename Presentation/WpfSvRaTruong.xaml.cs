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
    public partial class WpfSvRaTruong : Window
    {
        public WpfSvRaTruong()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ToolBll bll  = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Ration1.IsChecked = true;
            dtpTuNgay.SelectedDate = DateTime.Parse("01/01/" + DateTime.Now.AddYears(-1).ToString("yyyy"));
            dtpDenNgay.SelectedDate = DateTime.Now.AddDays(-1);
            try
            {
                cls.ClsConnect();
                DataTable dtpos = new DataTable();
                string sql = "select PO_MA,PO_TEN from DMPOS order by PO_MA";
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
            DataTable dtng = new DataTable();
            dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
            dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
            cls.DongKetNoi();

        }

        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CboXa.Items.Clear();
                cls.ClsConnect();
                DataTable dtxa = new DataTable();
                string sql = "select MA,TEN from DMXA where PGD_QL= " + "'" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "'" + " order by MA";
                dtxa = cls.LoadDataText(sql);
                for (int i = 0; i < dtxa.Rows.Count; i++)
                {
                    CboXa.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                }
                CboXa.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }

        }

        private void CboXa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CboTo.Items.Clear();
                cls.ClsConnect();
                DataTable dtto = new DataTable();
                string sql = "select TO_MATO,TO_TENTT from HSTO where Left(TO_MADP,6) = " + bll.Left(CboXa.SelectedValue.ToString().Trim(), 6) + " and left(NG_CAPNHAT,10)='" + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy")+ "' order by TO_MATO";
                //MessageBox.Show(sql);
                dtto = cls.LoadDataText(sql);
                for (int i = 0; i < dtto.Rows.Count; i++)
                {
                    CboTo.Items.Add(dtto.Rows[i][0] + " | " + dtto.Rows[i][1]);
                }
                CboTo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }

        }

        private void Ration1_Checked(object sender, RoutedEventArgs e)
        {
            lblTo.IsEnabled = true;
            CboTo.IsEnabled = true;
            lblXa.IsEnabled = true;
            CboXa.IsEnabled = true;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            
            cls.ClsConnect();
            if (Ration1.IsChecked == true)
            {
                try
                {


                    int thamso = 4;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@Mato";
                    if (CboTo != null)
                        giatri[0] = bll.Left(CboTo.SelectedValue.ToString().Trim(), 7);
                    else
                    {
                        MessageBox.Show("Chọn Tổ", "Mess");
                        return;
                    }
                    bien[1] = "@Ngay";
                    if (dtpNgay.SelectedDate.Value == null)
                    {
                        MessageBox.Show("Chưa chọn ngày ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        giatri[1] = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");    
                    }

                    bien[2] = "@TuNgay";
                    if (dtpTuNgay.SelectedDate.Value == null)
                    {
                        MessageBox.Show("Chưa chọn ngày ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        giatri[2] = dtpTuNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
                    }
                    bien[3] = "@DenNgay";
                    if (dtpDenNgay.SelectedDate.Value == null)
                    {
                        MessageBox.Show("Chưa chọn ngày ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        giatri[3] = dtpDenNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
                    }

                    dt = cls.LoadDataProcPara("usp_SvCT", bien, giatri, thamso);
                    if (dt.Rows.Count > 0)
                    {
                        rpt_SvCt rpt = new rpt_SvCt();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                         srv.DbPassSerVer());
                    }
                    else
                    {
                        MessageBox.Show("Không có bản ghi nào ", "Mess");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            } else if (Ration2.IsChecked == true)
            {
                try
                {

                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaXa";
                    if (CboXa != null)
                        giatri[0] = bll.Left(CboXa.SelectedValue.ToString().Trim(), 6);
                    else
                    {
                        MessageBox.Show("Chọn Xã", "Mess");
                        return;
                    }
                    bien[1] = "@Ngay";
                    if (dtpNgay.SelectedDate != null)
                        giatri[1] = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
                    else
                    {
                        MessageBox.Show("Chọn Ngày", "Mess");
                        return;
                    }
                  //  MessageBox.Show(giatri[0] + "   " + giatri[1]);
                    dt = cls.LoadDataProcPara("usp_SvTo", bien, giatri, thamso);
                    if (dt.Rows.Count > 0)
                    {
                        rpt_SvTo rpt = new rpt_SvTo();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                         srv.DbPassSerVer());
                    }
                    else
                    {
                        MessageBox.Show("Không có bản ghi nào ", "Mess");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            } else if (Ration3.IsChecked == true)
            {
                try
                {

                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    if (CboPos.SelectedValue != null)
                        giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    else
                    {
                        MessageBox.Show("Chọn POS", "Mess");
                        return;
                    }
                    bien[1] = "@Ngay";
                    if (dtpNgay.SelectedDate != null)
                        giatri[1] = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
                    else
                    {
                        MessageBox.Show("Chọn Ngày", "Mess");
                        return;
                    }
                   // MessageBox.Show(giatri[0] + "   " + giatri[1]);
                    dt = cls.LoadDataProcPara("usp_SvXa", bien, giatri, thamso);
                    if (dt.Rows.Count > 0)
                    {
                        //dataGrid1.ItemsSource = dt.DefaultView;
                        rpt_SvXa rpt = new rpt_SvXa();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                         srv.DbPassSerVer());
                    }
                    else
                    {
                        MessageBox.Show("Không có bản ghi nào ", "Mess");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {

                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    if (CboPos.SelectedValue != null)
                        giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    else
                    {
                        MessageBox.Show("Chọn POS", "Mess");
                        return;
                    }
                    bien[1] = "@Ngay";
                    if (dtpNgay.SelectedDate != null)
                        giatri[1] = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
                    else
                    {
                        MessageBox.Show("Chọn Ngày", "Mess");
                        return;
                    }
                    
                    dt = cls.LoadDataProcPara("usp_SvPos", bien, giatri, thamso);
                    if (dt.Rows.Count > 0)
                    {
                        //dataGrid1.ItemsSource = dt.DefaultView;
                        rpt_SvPos rpt = new rpt_SvPos();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                    }
                    else
                    {
                        MessageBox.Show("Không có bản ghi nào ", "Mess");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }

        }

        private void Ration2_Checked(object sender, RoutedEventArgs e)
        {
            lblTo.IsEnabled = false;
            CboTo.IsEnabled = false;
            lblXa.IsEnabled = true;
            CboXa.IsEnabled = true;
        }

        private void Ration3_Checked(object sender, RoutedEventArgs e)
        {
            lblTo.IsEnabled = false;
            CboTo.IsEnabled = false;
            lblXa.IsEnabled = false;
            CboXa.IsEnabled = false;
        }

        private void Ration4_Checked(object sender, RoutedEventArgs e)
        {
            CboPos.SelectedIndex = 4;
            lblTo.IsEnabled = false;
            CboTo.IsEnabled = false;
            lblXa.IsEnabled = false;
            CboXa.IsEnabled = false;

        }
    }
}
