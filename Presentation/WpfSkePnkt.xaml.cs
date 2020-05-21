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
    /// Interaction logic for WpfLaiTon.xaml
    /// </summary>
    public partial class WpfSkePnkt : Window
    {
        public WpfSkePnkt()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ToolBll bll  = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        private string FileName = "";
        string Thumuc = "C:\\KT740";

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
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
                CboPos.SelectedIndex = 5;
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
                CboXa.SelectedIndex = 1;
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
                string sql = "select TO_MATO,TO_TENTT from HSTO where Left(TO_MADP,6) = " + bll.Left(CboXa.SelectedValue.ToString().Trim(), 6) ;
                //MessageBox.Show(sql);
                dtto = cls.LoadDataText(sql);
                for (int i = 0; i < dtto.Rows.Count; i++)
                {
                    CboTo.Items.Add(dtto.Rows[i][0] + " | " + dtto.Rows[i][1]);
                }
                CboTo.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }

        }


        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            dtNew = dt.Clone();
            foreach (DataRow dr in dt.Rows)
            {
                if ((bool)dr[0] == true)
                {
                    dtNew.ImportRow(dr);
                }
            }
            if (dtNew==null || dtNew.Rows.Count==0)
            {
                MessageBox.Show("Chưa chọn khách hàng nào ", "Mess");
            }
            else
            {
                if (Opt1.IsChecked == true)
                {
                    rpt_SkePnkt rpt = new rpt_SkePnkt();
                    RPUtility.ShowRp(rpt, dtNew, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                        srv.DbPassSerVer());
                }
                else
                {
                    FileName = Thumuc + "\\" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) +"_"+ bll.Left(CboXa.SelectedValue.ToString().Trim(), 7) + "_PNKT_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                    FileStream fs = new FileStream(FileName, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
                    //bll.WriteDataTableToExcel(dt, "Details", FileName, "tutm : 0985165777");
                    bll.ToCSV(dt, sw, true);
                    MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
        }

        private void ChkAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach (DataRow dr in dt.Rows)
            {
                //MessageBox.Show(dr[0].ToString() + "  " + dr[1].ToString());
                //if ((bool) dr[0] == false)
                //{
                dr[0] = true;
                //}
            }
        }

        private void ChkAll_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (DataRow dr in dt.Rows)
            {
                ////MessageBox.Show(dr[0].ToString() + "  " + dr[1].ToString());
                //if ((bool) dr[0] == false)
                //{
                dr[0] = false;
                //}
            }

        }


        private void LoadData_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ChkAll_Unchecked(null,null);
            cls.ClsConnect();
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
                    giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                }
                bien[2] = "@Mau";
                if (Opt1.IsChecked == true)
                {
                    giatri[2] = "1";
                }
                else
                {
                    giatri[2] = "2";
                }
                bien[3] = "@MaXa";
                giatri[3] = bll.Left(CboXa.SelectedValue.ToString().Trim(), 6);
                dt = cls.LoadDataProcPara("usp_SkePnkt", bien, giatri, thamso);    
                if (dt.Rows.Count > 0)
                {
                    dgvSource.ItemsSource = dt.DefaultView;
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
}
