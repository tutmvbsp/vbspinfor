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
    /// Interaction logic for WpfLuong01.xaml
    /// </summary>
    public partial class WpfNguonDB : Window
    {
        private DateTime NG = DateTime.Now;
        public WpfNguonDB(DateTime _NG)
        {
            NG = _NG;
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        DataTable dt = new DataTable();
        DataTable dtnew = new DataTable();
        ToolBll bll = new ToolBll();
        private string strsql = "";
        private string strchk = "";
        private string upd = "";
        private string ins = "";
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dtpNgay.SelectedDate=DateTime.Now;
            dtpNgay.SelectedDate = NG;
            cls.ClsConnect();
            strsql =
                "select cast(1 as bit) CHON,a.* from  NGUON_DB a,(select TT, MAX(NGAY)NGAY from NGUON_DB group by TT) b where a.NGAY = b.NGAY and a.TT = b.TT order by a.TT,a.SUBTT";
            dt = cls.LoadDataText(strsql);
            dgvData.ItemsSource = dt.DefaultView;
            cls.DongKetNoi();    
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
            dtnew = dt.Clone();
            foreach (DataRow dr in dt.Rows)
            {
                if ((bool)dr[0] == true)
                {
                    dtnew.ImportRow(dr);
                }
            }
            if (dtnew == null || dtnew.Rows.Count==0)
            {
                MessageBox.Show("Chưa chọn chỉ tiêu nào !", "Mess", MessageBoxButton.OK,
                MessageBoxImage.Information);

            }
            else
            {
                try
                {
                    cls.ClsConnect();
                    foreach (DataRow dr in dtnew.Rows)
                    {
                        strchk = "select * from NGUON_DB where NGAY='" + ng + "' and TT='" + dr["TT"] + "' and SUBTT='"+dr["SUBTT"]+"'";
                        var chk =cls.LoadDataText(strchk);
                        if (chk.Rows.Count>0)
                        {
                            upd = "update NGUON_DB set P01=" + dr["P01"]+",P02 = " + dr["P02"] + ",P03 = " + dr["P03"]
                                + ",P04 = " + dr["P04"] + ",P05 = " + dr["P05"] + ",P06 = " + dr["P06"]
                                + ",P07 = " + dr["P07"] + ",P08 = " + dr["P08"] + " where NGAY='"+ng+"' and TT="+dr["TT"]+ " and SUBTT="+dr["SUBTT"];
                            cls.UpdateDataText(upd);
                            
                        }
                        else
                        {
                            ins = "insert into NGUON_DB (TT,TENCT,P01,P02,P03,P04,P05,P06,P07,P08,TONG,NGAY,SUBTT,MUC) " +
                                  " values ('"+dr["TT"]+"',N'"+dr["TENCT"]+"',"+ dr["P01"] + ","+ dr["P02"] + "," + dr["P03"]+ "," + dr["P04"] + "," + dr["P05"] 
                                  + ", " + dr["P06"]+ "," + dr["P07"] + "," + dr["P08"] + ",0,'"+ng+"'," + dr["SUBTT"]+","+ dr["MUC"] + ")";
              
                            cls.UpdateDataText(ins);
                        }
                    }
                    MessageBox.Show("Update OK", "Mess", MessageBoxButton.OK,MessageBoxImage.Information);
                    cls.DongKetNoi();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void ChkAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach (DataRow dr in dt.Rows)
            {
                dr[0] = true;
            }
        }

        private void ChkAll_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (DataRow dr in dt.Rows)
            {
                dr[0] = false;
            }

        }

        private void DtpNgay_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dt = null;
            dgvData.ItemsSource = null;
            cls.ClsConnect();
            strsql =
                "select cast(1 as bit) CHON,a.* from  NGUON_DB a where a.NGAY = '"+dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd")+"' order by a.TT,a.SUBTT";
            dt = cls.LoadDataText(strsql);
            if (dt.Rows.Count == 0)
            {
                dt = null;
                strsql ="select cast(1 as bit) CHON,a.* from  NGUON_DB a,(select TT, MAX(NGAY)NGAY from NGUON_DB group by TT) b where a.NGAY = b.NGAY and a.TT = b.TT order by a.TT,a.SUBTT";
                dt = cls.LoadDataText(strsql);
            }
            dgvData.ItemsSource = dt.DefaultView;
            cls.DongKetNoi();


        }
    }
}
