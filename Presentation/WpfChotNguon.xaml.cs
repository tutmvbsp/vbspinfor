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
    public partial class WpfChotNguon : Window
    {
        public WpfChotNguon()
        {
            InitializeComponent();
        }

        private readonly ClsServer cls = new ClsServer();
        readonly ToolBll _str = new ToolBll();
   
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
            try
            {
                cls.ClsConnect();
                const string sql = "select PO_MA,PO_TEN from DMPOS order by PO_MA";
                var dtpos = cls.LoadDataText(sql);
                /*
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                 */
                CboPos.ItemsSource = dtpos.DefaultView;
                CboPos.SelectedValuePath = "PO_MA";
                CboPos.DisplayMemberPath = "PO_TEN";
                CboPos.SelectedIndex = 1;
                /*       
                const string sqlload = "select * from CBTD ";
                dt = cls.LoadDataText(sqlload);
                dgvData.ItemsSource = dt.DefaultView;
                 */
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                int thamso = 2;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Ngay";
                if (dtpNgay.SelectedDate != null) giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                bien[1] = "@MaPos";
                giatri[1] = _str.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                var dt = cls.LoadDataProcPara("usp_ChotNguon", bien, giatri, thamso);
                //dataGrid.ItemsSource = dt.DefaultView;
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        //MessageBox.Show(dr["NG_MATO"].ToString()+"      "+dr["A01"].ToString());
                        string strsql = "update NGUON_UT set A01=" + dr["A01"] + ",A02=" + dr["A02"] + ",A03=" +
                                        dr["A03"] + ",A04=" + dr["A04"] + ",A06=" + dr["A06"] + ",A07=" + dr["A07"]
                                        + ",A09=" + dr["A09"] + ",A10=" + dr["A10"] + ",A11=" +
                                        dr["A11"] + ",A15=" + dr["A15"] + ",A16=" + dr["A16"] + ",A17=" + dr["A17"] +
                                        ",A18=" + dr["A18"] + ",A19=" + dr["A19"] + ",B03T=" + dr["B03T"]
                                        + ",B03H=" + dr["B03H"] + ",B19T=" + dr["B19T"] + ",B19H=" + dr["B19H"]
                                        + " where NG_MATO='" + dr["KU_MATO"].ToString().Trim() + "'";
                        cls.UpdateDataText(strsql);
                        //MessageBox.Show(strsql);
                    }
                    if (dtpNgay.SelectedDate != null)
                    {
                        string str="update NGUON_UT set NGAY=" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd")+" where NG_MAPGD='"+ _str.Left(CboPos.SelectedValue.ToString().Trim(), 6)+"'";
                        cls.UpdateDataText(str);
                    }
                    MessageBox.Show("Update Ok", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Xem lại. Chưa có dữ liệu", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
