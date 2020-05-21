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
    public partial class WpfNHNN
    {
        public WpfNHNN()
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
            try
            {   
                cls.ClsConnect();
                int thamso = 1;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Ngay";
                giatri[0] = DtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                dt = cls.LoadDataProcPara("usp_D21NHNN", bien, giatri, thamso);
                if (dt.Rows.Count > 0)
                        {
                                FileName = Thumuc + "\\" + DtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + "_SoLieu_NHNN.xlsx";
                                bll.WriteDataTableToExcel(dt, "Person Details", FileName, "Details");
                                MessageBox.Show("OK đã xuất file Excel " + FileName, "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                else
                        {
                            MessageBox.Show("Không có dữ liệu" , "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
  
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

         private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WpfNHNN_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                DtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();

        }
    }
}
