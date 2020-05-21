using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDelete.xaml
    /// </summary>
    public partial class WpfSkeNdh
    {
        public WpfSkeNdh()
        {
            InitializeComponent();
        }
        readonly ClsServer cls = new ClsServer();
        ServerInfor srv = new ServerInfor();
        ToolBll bll = new ToolBll();
        DataTable dt = new DataTable();
        string Thumuc = "C:\\Saoke";
        private string FileName = "";
        
        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            bll.TaoThuMuc(Thumuc);
            try
            {   cls.ClsConnect();
                int thamso = 4;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@MaPos";
                giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                bien[1] = "@Ngay";
                if (DtpNgay.SelectedDate != null)
                {
                    giatri[1] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    bien[2] = "@DenNgay";
                    if (DtpDenNgay.SelectedDate != null)
                    {
                        giatri[2] = DtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                        bien[3] = "@Ky";
                        // MessageBox.Show(giatri[1].ToString()+"  "+giatri[2].ToString());
                        if (RadioButton1.IsChecked == true)
                        {
                            FileName = Thumuc + "\\" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_" + DtpDenNgay.SelectedDate.Value.ToString("ddMMyyyy") + "_SkeNdh_UT.csv";
                            giatri[3] = "1";
                        }
                        else if (RadioButton2.IsChecked == true)
                        {
                            FileName = Thumuc + "\\" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_" + DtpDenNgay.SelectedDate.Value.ToString("ddMMyyyy") + "_SkeNdh_TT_KyCon.csv";
                            giatri[3] = "2";
                        }                        
                        else if (RadioButton3.IsChecked == true)
                        {
                            FileName = Thumuc + "\\" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_" + DtpDenNgay.SelectedDate.Value.ToString("ddMMyyyy") + "_SkeNdh_PhanKy.csv";
                            giatri[3] = "1";
                        } 
                        else
                        {
                            FileName = Thumuc + "\\" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_" + DtpDenNgay.SelectedDate.Value.ToString("ddMMyyyy") + "_TongHop_NDH.csv";
                            giatri[3] = "1";
                        }

                        if (RadioButton1.IsChecked == true || RadioButton2.IsChecked == true)
                            dt = cls.LoadDataProcPara("usp_SkeNdh", bien, giatri, thamso);
                        else if (RadioButton3.IsChecked == true)
                            dt = cls.LoadDataProcPara("usp_SkeNdhPhanKy", bien, giatri, thamso);
                        else dt = cls.LoadDataProcPara("usp_TH_NDH", bien, giatri, thamso);
                        if (dt.Rows.Count > 0)
                        {
                           // FileStream fs = new FileStream(FileName, FileMode.Create);
                           // StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
                            //bll.ToCSV(dt, sw, true);
                            bll.ExportToExcel(dt, FileName);
                            MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            bll.OpenExcel(FileName);
                            //bll.ExportToExcel(dt, FileName);
                            //MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            //bll.OpenExcel(FileName);

                        }
                        else
                            MessageBox.Show("Không có KU đến hạn", "Mess", MessageBoxButton.OK,
                                MessageBoxImage.Information);
                    }
                }
               // bll.WriteDataTableToExcel(dt, "Person Details", FileName, "Details");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                cls.DongKetNoi();
            }
                 
        }

        private void WpfSkeNdh_OnLoaded(object sender, RoutedEventArgs e)
        {
            DtpDenNgay.SelectedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).ToString());
            try
            {
                cls.ClsConnect();
                string sql = "select PO_MA,PO_TEN from DMPOS order by PO_MA";
                var dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                CboPos.SelectedIndex = 1;
                var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                DtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
            
        }


        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
