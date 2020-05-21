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
    /// Interaction logic for WpfDvut.xaml
    /// </summary>
    public partial class WpfCanDoi : Window
    {
        public WpfCanDoi()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ToolBll str = new ToolBll();
        ServerInfor srv = new ServerInfor();
        //string Thumuc = "C:\\Saoke";
        //private string FileName = "";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            DataTable dtng = new DataTable();
            DataTable dtpos = new DataTable();
            /*
            dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
            dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
             */
            dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
            string sql = "select PO_MA,PO_TEN from DMPOS where PO_MACN=" + "'" + BienBll.MainPos + "'" + " order by PO_MA";
            dtpos = cls.LoadDataText(sql);
            for (int i = 0; i < dtpos.Rows.Count; i++)
            {
                CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
            }
           /* CboPos.SelectedIndex = 0;

            string sqldv = "select DVUT,TENDV from DVUT order by DVUT";
            dtdvut = cls.LoadDataText(sqldv);
            for (int i = 0; i < dtdvut.Rows.Count; i++)
            {
                CboDvut.Items.Add(dtdvut.Rows[i][0] + " | " + dtdvut.Rows[i][1]);
            }
            CboDvut.SelectedIndex = 0;
            */
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
                cls.ClsConnect();
                DataTable dt = new DataTable();
                int thamso = 4;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@MaPos";
                giatri[0] = str.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                bien[1] = "@Ngay";
                if (dtpNgay.SelectedDate == null)
                {
                    MessageBox.Show("Chưa chọn ngày", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    giatri[1] = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
                }
                bien[2] = "@KyBC";
                if (radioButton1.IsChecked == true)
                {
                    giatri[2] = 'D';
                }
                else if (radioButton2.IsChecked == true)
                {
                    giatri[2] = 'M';
                }
                else if (radioButton3.IsChecked == true)
                {
                    giatri[2] = 'Q';
                }
                else
                {
                    giatri[2] = 'Y';
                }
                bien[3] = "@LoaiBC";
                if (radioButton5.IsChecked == true)
                {
                    giatri[3] = '1';
                }
                else
                {
                    giatri[3] = '2';
                }
                //MessageBox.Show(giatri[0].ToString() + "   " + giatri[1].ToString() + "  " + giatri[2].ToString());
                dt = cls.LoadDataProcPara("usp_CanDoi", bien, giatri, thamso);
                //rpt_kt740_01 rpt = new rpt_kt740_01();
                if (dt.Rows.Count > 0)
                {
                    rpt_CanDoi rpt = new rpt_CanDoi();
                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                    //dataGrid1.ItemsSource = dt.DefaultView;
                    // FileName = Thumuc + "\\" + str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_" + str.Left(CboDvut.SelectedValue.ToString().Trim(), 2) + "_" + str.Left(CboXa.SelectedValue.ToString().Trim(), 6) + "_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                    // str.WriteDataTableToExcel(dt, "Person Details", FileName, "Details");
                    // MessageBox.Show("ok");

                }
                else
                {
                    MessageBox.Show("Chưa có số liệu", "Thông báo");
                }
                cls.DongKetNoi();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error "+ex.Message,"Mess",MessageBoxButton.OK,MessageBoxImage.Error);
            }


        }

   
    }
}
