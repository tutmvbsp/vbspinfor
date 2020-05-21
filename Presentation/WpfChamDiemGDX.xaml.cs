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
    public partial class WpfChamDiemGDX : Window
    {
        public WpfChamDiemGDX()
        {
            InitializeComponent();
        }
        readonly ClsServer _cls = new ClsServer();
        ToolBll bll = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable _dt = new DataTable();
  
        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _cls.ClsConnect();
                int thamso = 6;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                foreach (DataRow dr in _dt.Rows)
                {
                    bien[0] = "@MaPos";
                    giatri[0] = dr[2];
                    bien[1] = "@Nam";
                    giatri[1] = dr[0];
                    bien[2] = "@MaXa";
                    giatri[2] = dr[5];
                    bien[3] = "@STT";
                    giatri[3] = dr[7];
                    bien[4] = "@Diem";
                    giatri[4] = dr[12];
                    bien[5] = "@MOTA";
                    giatri[5] = dr[17];

                   // MessageBox.Show(dr[0].ToString());
                   // MessageBox.Show("Mapos : "+giatri[0]+"  Nam: "+giatri[1]+"   Maxa"+ giatri[2]+"  STT"+ giatri[3]+"  Diem"+giatri[4]);
                    _cls.UpdateDataProcPara("usp_UpdateCHAMDIEM_GDX", bien, giatri, thamso);
                }
                    _cls.ClsConnect();
                    int thamso1 = 3;
                    string[] bien1 = new string[thamso1];
                    object[] giatri1 = new object[thamso1];
                    bien1[0] = "@MaPos";
                    giatri1[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien1[1] = "@Nam";
                    giatri1[1] = bll.Right(dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"),4);
                    bien1[2] = "@MaXa";
                    giatri1[2] = bll.Left(CboXa.SelectedValue.ToString().Trim(), 6);
                    _cls.UpdateDataProcPara("usp_UpdateCHAMDIEMGDX", bien1, giatri1, thamso1);
                
                MessageBox.Show("Save data OK","Mess",MessageBoxButton.OK,MessageBoxImage.Information);
                
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
        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CboXa.Items.Clear();
                _cls.ClsConnect();
                DataTable dtxa;
                string sql = "select MA,TEN from DMXA where PGD_QL= " + "'" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "'" + " order by MA";
                dtxa = _cls.LoadDataText(sql);
                for (int i = 0; i < dtxa.Rows.Count; i++)
                {
                    CboXa.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                }
                CboXa.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }

        }


        private void WpfChamDiemGDX_OnLoaded(object sender, RoutedEventArgs e)
        {
            dtpNgay.SelectedDate = DateTime.Parse(DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month).ToString());
            try
            {
                _cls.ClsConnect();
                //string sql = "select PO_MA,PO_TEN from DMPOS where PO_MACN=" + "'" + BienBll.MainPos + "'" + " order by PO_MA";
                var dtpos = _cls.LoadDataText("select PO_MA,PO_TEN from DMPOS order by PO_MA");
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                CboPos.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            _cls.DongKetNoi();
        }

        private void LblManual_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                _cls.ClsConnect();
                int thamso = 3;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@MaPos";
                giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                bien[1] = "@Ngay";
                if (dtpNgay.SelectedDate != null) giatri[1] = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
                bien[2] = "@MaXa";
                giatri[2] = bll.Left(CboXa.SelectedValue.ToString().Trim(), 6);
                _dt = _cls.LoadDataProcPara("usp_ChamDiemGDX", bien, giatri, thamso);
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

        private void Print_OnClick(object sender, RoutedEventArgs e)
        {
            if (Opt1.IsChecked == true)
            {
                string sql = "select * from LUU_CHAMDIEMGDX where MAPOS='" +
                             bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "' and MAXA='" +
                             bll.Left(CboXa.SelectedValue.ToString().Trim(), 6) + "' and NAM='" +
                             bll.Right(dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"), 4) + "' order by STT";
                try
                {
                    _cls.ClsConnect();
                    _dt = _cls.LoadDataText(sql);
                    if (_dt.Rows.Count > 0)
                    {
                        rpt_ChamDiemGDX rpt = new rpt_ChamDiemGDX();
                        RPUtility.ShowRp(rpt, _dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                            srv.DbPassSerVer());
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error", "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                string sql =
                    "select MA from DMXA where PGD_QL='"+bll.Left(CboPos.SelectedValue.ToString().Trim(), 6)+"'"+
                    " and right(MA,2)<>'00' and MA not in (select MAXA from LUU_CHAMDIEMGDX where MAPOS='" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "' and NAM='" + bll.Right(dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"), 4)+"')";		
                _cls.ClsConnect();
                _dt = _cls.LoadDataText(sql);
                if (_dt.Rows.Count > 0)
                {
                    string xa = "";
                    foreach (DataRow dr in _dt.Rows)
                    {
                        //MessageBox.Show(dr[0].ToString());
                        xa = xa + "/" + dr[0].ToString();
                    }
                    MessageBox.Show("Những xã sau chưa chấm : "+xa,"Mess",MessageBoxButton.OK,MessageBoxImage.Warning);
                    
                }
                else
                {
                    try
                    {
                        _cls.ClsConnect();
                        int thamso = 2;
                        string[] bien = new string[thamso];
                        object[] giatri = new object[thamso];
                        bien[0] = "@MaPos";
                        giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                        bien[1] = "@Ngay";
                        if (dtpNgay.SelectedDate != null) giatri[1] = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
                        _dt = _cls.LoadDataProcPara("usp_ChamDiemGDX_TH", bien, giatri, thamso);
                        if (_dt.Rows.Count > 0)
                        {
                            rpt_ChamDiemGDX_TH rpt = new rpt_ChamDiemGDX_TH();
                            RPUtility.ShowRp(rpt, _dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                srv.DbPassSerVer());
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
            }
        }

        private void CboXa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgvTarGet.ItemsSource = null;
        }

        private void Opt2_Checked(object sender, RoutedEventArgs e)
        {
            OK.IsEnabled = false;
        }

        private void Opt1_Checked(object sender, RoutedEventArgs e)
        {
            OK.IsEnabled = true;
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
