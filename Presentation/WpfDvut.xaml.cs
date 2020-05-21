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
    public partial class WpfDvut : Window
    {
        public WpfDvut()
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
            DataTable dtdvut = new DataTable();
            dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
            dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
            //string sql = "select PO_MA,PO_TEN from DMPOS where PO_MACN=" + "'" + BienBll.MainPos + "'" + " order by PO_MA";
            var sql = BienBll.NdCapbc.Trim() == "1" ? string.Format("select PO_MA,PO_TEN from DMPOS where PO_MA='{0}'", BienBll.NdMadv.Trim()) : "select PO_MA,PO_TEN from DMPOS";
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

           cls.ClsConnect();
           DataTable dt = new DataTable();
           int thamso = 5;
           string[] bien = new string[thamso];
           object[] giatri = new object[thamso];
           bien[0] = "@MaPos";
           giatri[0] = str.Left(CboPos.SelectedValue.ToString().Trim(), 6);
           bien[1] = "@Ngay";
           if (dtpNgay.SelectedDate== null)
               {
                 MessageBox.Show("Chưa chọn ngày", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                 return;
               }
           else
               {
                 giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");    
               }
           bien[2] = "@MaXa";
           giatri[2] = str.Left(CboXa.SelectedValue.ToString().Trim(), 6);
            bien[3] = "@Mau";
            if (radioButton4.IsChecked == true)
            {
                giatri[3] = "1";
            }
            else
            {
                giatri[3] = "2";
            }
            
            bien[4] = "@Ky";
            if (radioButton1.IsChecked == true)
            {
                giatri[4] = "1";
            } else if (radioButton2.IsChecked == true)
            {
                giatri[4] = "2";
            }
            else if (radioButton3.IsChecked==true)
            {
                giatri[4] = "3";
            }
            //MessageBox.Show(giatri[0] + "  " + giatri[1] + "  " + giatri[2] + "  " + giatri[3] + "  " + giatri[4]);
                dt = cls.LoadDataProcPara("usp_Dvut", bien, giatri, thamso);
                //rpt_kt740_01 rpt = new rpt_kt740_01();
                if (dt.Rows.Count > 0)
                {
                    rpt_Dvut rpt = new rpt_Dvut();
                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                    //dataGrid1.ItemsSource = dt.DefaultView;
                   // FileName = Thumuc + "\\" + str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_" + str.Left(CboDvut.SelectedValue.ToString().Trim(), 2) + "_" + str.Left(CboXa.SelectedValue.ToString().Trim(), 6) + "_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                   // str.WriteDataTableToExcel(dt, "Person Details", FileName, "Details");
                    //MessageBox.Show("Copy Excel to : " + FileName);

                }
                else
                {
                    MessageBox.Show("Chưa có số liệu", "Thông báo");
                }
                cls.DongKetNoi();



            
        }

        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //MessageBox.Show(str.Left(cboPos.SelectedValue.ToString().Trim(),6));
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
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }

   
    }
}
