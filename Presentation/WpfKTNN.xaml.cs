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
using System.IO;
using DAL;
using BLL;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDienbao.xaml
    /// </summary>
    public partial class WpfKTNN : Window
    {
        public WpfKTNN()
        {
            InitializeComponent();
        }

        private ClsServer cls = new ClsServer();
        private ServerInfor srv = new ServerInfor();
        private ToolBll bll = new ToolBll();
        DataTable dt= new DataTable();
        string Thumuc = "C:\\Saoke";
        private string FileName = ""; string Mau = "";
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            bll.TaoThuMuc(Thumuc);
            cls.ClsConnect();
            try
            {
                    const int thamso = 5;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@Ngay";
                    if (dtpNgay.SelectedDate != null) giatri[0] = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
                    bien[1] = "@MaPos";
                    giatri[1] = bll.Left(CboPos.SelectedValue.ToString(),6);
                    bien[2] = "@ChTr";
                    giatri[2] = bll.Left(CboChTr.SelectedValue.ToString(),2);
                    bien[3] = "@Mau";
                    bien[4] = "@Ngayktah";
                    if (dtpNgayktah.SelectedDate != null) giatri[4] = dtpNgayktah.SelectedDate.Value.ToString("dd/MM/yyyy");
                if (radioButton1.IsChecked == true)
                    {
                        giatri[3] = "1";
                        Mau = "M1_";
                    } else if (radioButton2.IsChecked==true)
                        {
                            giatri[3] = "2";
                            Mau = "M2_";
                        }
                        else if (radioButton3.IsChecked == true)
                            {
                                giatri[3] = "3";
                                Mau = "M3_";
                            }
                            else
                                {
                                 giatri[3] = "4";
                                 Mau = "M4_";
                                }

                    dt = cls.LoadDataProcPara("usp_KTNN", bien, giatri, thamso);
                    if (dt.Rows.Count > 0)
                    {
                        //FileName = Thumuc + "\\"+Mau.Trim()+ bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_" +bll.Left(CboChTr.SelectedValue.ToString().Trim(), 2) + "_" +dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                        FileName = Thumuc + "\\" + Mau.Trim() + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_" + bll.Left(CboChTr.SelectedValue.ToString().Trim(), 2) + "_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                        //MessageBox.Show("Chú ý việc xuất Excel toàn bộ thực hiện khoảng 15p, cho đến khi thông báo OK : ","Thông báo",MessageBoxButton.OK,MessageBoxImage.Information);
                        FileStream fs = new FileStream(FileName, FileMode.Create);
                        StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
                        bll.ToCSV(dt, sw, true);

                        //bll.WriteDataTableToExcel(dt, "Person Details", FileName, "TUTM");
                        MessageBox.Show("Copy Excel to : " + FileName + " OK","Thông báo",MessageBoxButton.OK,MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không có bản ghi nào", "Thông báo", MessageBoxButton.OK,
                                        MessageBoxImage.Information);
                    }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Lổi, liên hệ phòng tin học  "+ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtpNgay.SelectedDate = DateTime.Parse("31/12/" + DateTime.Now.AddYears(-1).ToString("yyyy"));
            dtpNgayktah.SelectedDate = DateTime.Now.AddDays(-1);
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
                CboPos.SelectedIndex = 0;
                DataTable dtchtr = new DataTable();
                string sqlct = "select CHTRINH,TEN_CT from DM_CHTRINH order by CHTRINH";
                dtchtr = cls.LoadDataText(sqlct);
                for (int i = 0; i < dtchtr.Rows.Count; i++)
                {
                    CboChTr.Items.Add(dtchtr.Rows[i][0] + " | " + dtchtr.Rows[i][1]);
                }
                CboChTr.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();

        }

        private void radioButton4_Checked(object sender, RoutedEventArgs e)
        {
            dtpNgayktah.IsEnabled = true;
        }

        private void radioButton3_Checked(object sender, RoutedEventArgs e)
        {
            dtpNgayktah.IsEnabled = false;
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            dtpNgayktah.IsEnabled = false;
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            dtpNgayktah.IsEnabled = false;
        }

        private void radioButton5_Checked(object sender, RoutedEventArgs e)
        {
            dtpNgayktah.IsEnabled = false;
        }
    }
}
