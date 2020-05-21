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
    public partial class WpfHscb : Window
    {
        public WpfHscb()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ToolBll str = new ToolBll();
        //ServerInfor srv = new ServerInfor();
        //string Thumuc = "C:\\Saoke";
        //private string FileName = "";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            DataTable dtpos = new DataTable();
            string sql = "select PO_MA,PO_TEN from DMPOS where PO_MACN=" + "'" + BienBll.MainPos + "'" + " order by PO_MA";
            dtpos = cls.LoadDataText(sql);
            for (int i = 0; i < dtpos.Rows.Count; i++)
            {
                CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
            }
            CboPos.SelectedIndex = 1;
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
                string sql = "select hoten,ngaysinh,cmt_so,cmt_ngaycap,dienthoai_nr,dienthoai_dd,dienthoai_cq from HSCB where mapgd='"+str.Left(CboPos.SelectedValue.ToString().Trim(),6)+"'";
                dt = cls.LoadDataText(sql);
                dgvData.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();
        }
    }
}
