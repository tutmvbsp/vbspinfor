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
    /// Interaction logic for WpfDvut.xaml
    /// </summary>
    public partial class WpfPnkt : Window
    {
        public WpfPnkt()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ToolBll str = new ToolBll();
        ServerInfor srv = new ServerInfor();
        string Thumuc = "C:\\KT740";
        private string FileName = "";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                str.TaoThuMuc(Thumuc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lổi tạo thư mục " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            cls.ClsConnect();
            DataTable dtng = new DataTable();
            DataTable dtpos = new DataTable();
            dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
            dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
            string sql = "select PO_MA,PO_TEN from DMPOS where PO_MACN=" + "'" + BienBll.MainPos + "'" + " order by PO_MA";
            dtpos = cls.LoadDataText(sql);
            for (int i = 0; i < dtpos.Rows.Count; i++)
            {
                CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
            }
            CboPos.SelectedIndex = 0;

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
           int thamso = 4;
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
                 giatri[1] = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");    
               }
           bien[2] = "@MaXa";
           giatri[2] = str.Left(CboXa.SelectedValue.ToString().Trim(), 6);
           bien[3] = "@Mau";
            if (radioButton1.IsChecked == true)
            {
                giatri[3] = '1';
            }
            else
            {
                giatri[3] = '2';
            }
            dt = cls.LoadDataProcPara("usp_Pnkt", bien, giatri, thamso);
                if (dt.Rows.Count > 0)
                {
                    if (radioButton1.IsChecked == true)
                    {
                        FileName = Thumuc + "\\" + str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_PNKT_" +
                                   dtpNgay.SelectedDate.Value.ToString("ddMMyyyy_") + str.Right(CboXa.SelectedValue.ToString().Trim(), 2)+ ".csv";
                    }
                    else
                    {
                        FileName = Thumuc + "\\" + str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_CHTRINH_" +
                                   dtpNgay.SelectedDate.Value.ToString("ddMMyyyy_") + str.Right(CboXa.SelectedValue.ToString().Trim(), 2) + ".csv";
                    }
                    FileStream fs = new FileStream(FileName, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
                    //bll.WriteDataTableToExcel(dt, "Details", FileName, "tutm : 0985165777");
                    str.ToCSV(dt, sw, true);
                    MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    string sql = "select MA,TEN from DMXA where PGD_QL= " + "'" +
                                 str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "'" + " order by MA";
                    var dtxa = cls.LoadDataText(sql);
                    /*
                    for (int i = 0; i < dtxa.Rows.Count; i++)
                    {
                        CboXa.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                    }
                     */
                    CboXa.ItemsSource = dtxa.DefaultView;
                    CboXa.DisplayMemberPath = "TEN";
                    CboXa.SelectedValuePath = "MA";
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
