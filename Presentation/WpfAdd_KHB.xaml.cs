using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Data;
using DAL;
using BLL;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDelete.xaml
    /// </summary>
    public partial class WpfAdd_KHB : Window
    {
        public WpfAdd_KHB()
        {
            InitializeComponent();
        }
        readonly ClsServer _cls = new ClsServer();
        ToolBll bll = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable _dt = new DataTable();
        DataTable dtNew = new DataTable();
  
        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _cls.ClsConnect();
                dtNew = _dt.GetChanges();
                if (dtNew==null) MessageBox.Show("Chưa có giá trị nào thay đổi !", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    foreach (DataRow dr in dtNew.Rows)
                    {
                        string sql="update MAUKHB set DUCUOI="+dr["DUCUOI"]+" where DT_MAPGD='"+dr["DT_MAPGD"]+ "' and DT_CAPDT='" + dr["DT_CAPDT"]+"' and  NGAY='"+dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd")+"' and DT_MAPGD='"+ bll.Left(cboPos.SelectedValue.ToString().Trim(), 6) + "' and KU_CHTRINH='"+ dr["KU_CHTRINH"] + "'";
                        _cls.UpdateDataText(sql);
                    }
                    MessageBox.Show("Save data OK", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _cls.DongKetNoi();
            }

        }
   

        private void LblManual_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                _cls.ClsConnect();
                string sql = "select * from MAUKHB where NGAY='"+dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd")+"' and DT_MAPGD='"+ bll.Left(cboPos.SelectedValue.ToString().Trim(), 6) + "' order by DT_CAPDT,DT_MAPGD";
                _dt = _cls.LoadDataText(sql);
                if (_dt.Rows.Count > 0)
                {
                    dgvTarGet.ItemsSource = _dt.DefaultView;
                }
                else
                {
                    MessageBox.Show("Không có bản ghi nào", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                //MessageBox.Show("OK", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _cls.DongKetNoi();
            }
         
        }

        private void WpfAdd_KHB_OnLoaded(object sender, RoutedEventArgs e)
        {
            dtpNgay.SelectedDate = DateTime.Parse(DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month).ToString());
            try
            {
                _cls.ClsConnect();
                DataTable dtpos = new DataTable();
                string sql = "select PO_MA,PO_TEN from DMPOS where PO_MACN=" + "'" + BienBll.MainPos + "'" + " order by PO_MA";
                dtpos = _cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    cboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                cboPos.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            _cls.DongKetNoi();
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
