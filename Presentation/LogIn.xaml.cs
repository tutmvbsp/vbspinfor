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
using System.Configuration;
using System.Data;
using System.IO;
using System.Xml;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        public LogIn()
        {
            InitializeComponent();
        }
        ToolBll sBll = new ToolBll();
        ClsServer cls = new ClsServer();
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (cls.KiemTraKetNoi())
            {
                #region

                try
                {
                    cls.ClsConnect();
                    DataTable dt = new DataTable();
                    String userName = txtUserName.Text.Trim();
                    String passWord = sBll.Encrypt(PassBox.Password.Trim(),true);
                    string sql = "select * from NG_DUNG where ND_MA= " + "'" + userName + "' and ND_MATKHAU= " + "'" +
                                 passWord + "'";
                    dt = cls.LoadDataText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dtr = dt.Rows[0];
                        BienBll.Ndma = (string) dtr["ND_MA"];
                        BienBll.NdTen = (string) dtr["ND_TEN"];
                        BienBll.NdDiachi = (string) dtr["ND_DIACHI"];
                        BienBll.NdCapbc = (string) dtr["ND_CAPBC"];
                        BienBll.NdMadv = (string) dtr[9];
                        BienBll.Quyen = (string) dtr["ND_QUYEN"];
                        BienBll.NdTrangThai = (string)dtr["ND_TTHAI"];
                        BienBll.MainPos = ConfigurationManager.AppSettings["MainPos"];
                        BienBll.LogIn = (string)dtr["ND_LOGIN"];
                        BienBll.ChucVu = (string)dtr["ND_CHUCVU"];
                        BienBll.PhongBan = (string)dtr["ND_PHONGBAN"];
                        BienBll.ChamCong = (string)dtr["CHAMCONG"];
                        BienBll.EndOfYearBefor = DateTime.Parse("31/12/" + DateTime.Now.AddYears(-1).ToString("yyyy"));
                        if (BienBll.NdTrangThai == "A")
                        {
                            /*
                            if (BienBll.LogIn.Trim() == "T")
                            {
                                MessageBox.Show(BienBll.NdTen.Trim()+" đang Login ở máy khác !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                                Close();
                            }
                            else
                            {
                                string str = "update NG_DUNG set ND_LOGIN='T' where ND_MA='" + BienBll.Ndma.Trim() + "'";
                                cls.UpdateDataText(str);
                                var f = new MainWindow();
                                Hide();
                                f.ShowDialog();
                                Close();
                            }
                             */
                            var f = new MainWindow();
                            Hide();
                            f.ShowDialog();
                            Close();

                        }
                        else
                        {
                            MessageBox.Show("Các Anh / Chị đang sử dụng phần mềm này thông cảm, phần mềm đóng từ ngày 16/06/2016 !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("UserName or PassWord not correct", "Thông Báo", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        txtUserName.Text = "";
                        PassBox.Password = "";
                    }
                    cls.DongKetNoi();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Kiểm tra kết nối máy chủ " + ex.Message+" Định dạng ngày tháng dd/MM/yyyy ","Mess",MessageBoxButton.OK,MessageBoxImage.Error);
                }

                #endregion
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUserName.Text = "tutm0001";
            PassBox.Password = "tutm@";
            txtIp.Text = sBll.LocalIPAddress();
        }

        private void ChkConnect(object sender, MouseButtonEventArgs e)
        {

            if (cls.KiemTraKetNoi())
            {
                BienBll.LogOn = true;
                MessageBox.Show("Kết nối thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                BienBll.LogOn = false;
            }
            /*
            chkConnetBll chk = new chkConnetBll();
            if (chk.chkConnect(txtIp.Text.Trim()))
            {
                BienBll.LogOn = true;
                MessageBox.Show("Kết nối thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            } else
            {
                MessageBox.Show("Thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                BienBll.LogOn = false;
            }
             */
        }

        private void LblCheck_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            //bool retVal = false;
            //try
            //{
            //    string FILE_NAME = string.Concat(Directory.GetCurrentDirectory(), "\\", App.Current, ".exe.Config"); //the application configuration file name
            //    XmlTextReader reader = new XmlTextReader(FILE_NAME);
            //    XmlDocument doc = new XmlDocument();
            //    doc.Load(reader);
            //    reader.Close();
            //    string nodeRoute = string.Concat("connectionStrings/add");

            //    XmlNode cnnStr = null;
            //    XmlElement root = doc.DocumentElement;
            //    XmlNodeList Settings = root.SelectNodes(nodeRoute);

            //    for (int i = 0; i < Settings.Count; i++)
            //    {
            //        cnnStr = Settings[i];
            //        if (cnnStr.Attributes["name"].Value.Equals(Name))
            //            break;
            //        cnnStr = null;
            //    }

            //    cnnStr.Attributes["connectionString"].Value = value;
            //    cnnStr.Attributes["providerName"].Value = providerName;
            //    doc.Save(FILE_NAME);
            //    retVal = true;
            //    MessageBox.Show(FILE_NAME);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    //retVal = false;
            //    //Handle the Exception as you like
            //}
            ////return retVal;
        }
    }
}
