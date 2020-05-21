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
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfLaiTon.xaml
    /// </summary>
    public partial class WpfChangePass : Window
    {
        public WpfChangePass()
        {
            InitializeComponent();
        }

        ToolBll s = new ToolBll();
        private readonly ClsServer cls = new ClsServer();
        private DataTable dt = new DataTable();
 
   
        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
            /*
            cls.ClsConnect();
            string sql = "update NG_DUNG set ND_MATKHAU='"+s.Encrypt("123",true)+"'";
            cls.UpdateDataText(sql);
            cls.DongKetNoi();
             */
        }

        private void BtnOK_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                    DataTable dtPass = new DataTable();
                    string sql = "select * from NG_DUNG where ND_MA='" + BienBll.Ndma.Trim() + "' and ND_MATKHAU='" +
                                 s.Encrypt(CurPasswordBox.Password.Trim(), true) + "'";
                    cls.ClsConnect();
                    dtPass = cls.LoadDataText(sql);
                    if (dtPass.Rows.Count > 0)
                    {
                        if (PasswordBox.Password.Trim()=="")
                        {
                            MessageBox.Show("Bạn chưa nhập mật khẩu mới !", "Thông báo",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else if (s.KiemTraKyTuTv(PasswordBox.Password.Trim()) > 0)
                        {
                            MessageBox.Show("Mật khẩu mới có ký tự tiếng việt !", "Thông báo",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                        } else
                        {
                            if (PasswordBox.Password.Trim() != RePasswordBox.Password.Trim())
                            {
                                MessageBox.Show("Mật khẩu mới không khới nhau", "Thông báo",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);

                            }
                            else
                            {
                                string sqlup = "update NG_DUNG set ND_MATKHAU='" + s.Encrypt(PasswordBox.Password.Trim(),true)+"' where ND_MA='"+BienBll.Ndma.Trim()+"'";
                                cls.UpdateDataText(sqlup);
                                MessageBox.Show("Đổi mật khẩu thành công !", "Thông báo",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu củ không đúng, hoặc chưa chọn người cần đổi", "Thông báo",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            cls.DongKetNoi();
        }
    }
}
