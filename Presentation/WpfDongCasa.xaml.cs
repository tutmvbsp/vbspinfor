using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Text;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for Wpf_THONGBAO_DONG105.xaml
    /// </summary>
    public partial class WpfDongCasa : Window
    {
        ClsServer cls = new ClsServer();
        ServerInfor srv = new ServerInfor();
        ToolBll str = new ToolBll();
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        private string FileName = "";
        string Thumuc = "C:\\Saoke";

        public WpfDongCasa()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
                DataTable dtng = new DataTable();
                dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error "+ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }

   


        private void lblOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                dtNew = dt.Clone();
                foreach (DataRow dr in dt.Rows)
                {
                    if ((bool)dr[0] == true)
                    {
                        dtNew.ImportRow(dr);
                    }
                }

                //dtNew = dt.GetChanges();
                if (dtNew == null || dtNew.Rows.Count == 0)
                {
                    MessageBox.Show("Chưa có thay đổi ngày nào !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    if (Opt1.IsChecked == true || Opt3.IsChecked == true)
                    {
                        rpt_DongCasa rpt = new rpt_DongCasa();
                        RPUtility.ShowRp(rpt, dtNew, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        if (lblGmt.IsChecked == true)
                        {
                            rpt_RoiTo rpt1 = new rpt_RoiTo();
                            RPUtility.ShowRp(rpt1, dtNew, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                        }
                        //dt = null;
                        //dtNew = null;
                        //dgvData.ItemsSource = null;
                        /*
                        string mau = "";
                        if (Opt3.IsChecked == true)
                        {
                            mau = "1";
                        }
                        else
                        {
                            mau = "0";
                        }
                        //dgvNew.ItemsSource = dtNew.DefaultView;

                        if (Opt1.IsChecked == true || Opt2.IsChecked == true)
                        {
                            rpt_DongCasa rpt = new rpt_DongCasa();
                            //RPUtility.ShowRp(rpt, dtNew, this, srv.DbSourceLocal(), srv.DbNameLocal(), srv.DbUserLocal(), srv.DbPassLocal());
                            RPUtility.ShowRpOnePara(rpt, dtNew, mau, this, srv.DbSourceLocal(), srv.DbNameLocal(),
                                srv.DbUserLocal(), srv.DbPassLocal());
                        }
                        else
                        {
                            rpt_DongCasa rpt = new rpt_DongCasa();
                            //RPUtility.ShowRp(rpt, dtNew, this, srv.DbSourceLocal(), srv.DbNameLocal(), srv.DbUserLocal(), srv.DbPassLocal());
                            RPUtility.ShowRpOnePara(rpt, dtNew, mau, this, srv.DbSourceLocal(), srv.DbNameLocal(),
                                srv.DbUserLocal(), srv.DbPassLocal());
                        }
                         */

                    }
                    else
                    {
                        FileName = Thumuc + "\\" + str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_" + str.Left(CboTo.SelectedValue.ToString().Trim(), 7) + "_DONG105_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                        FileStream fs = new FileStream(FileName, FileMode.Create);
                        StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
                        //bll.WriteDataTableToExcel(dt, "Details", FileName, "tutm : 0985165777");
                        str.ToCSV(dtNew, sw, true);
                        MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    //dtNew = dt.Clone();
                    //dtNew = null;
                    //dt = null;
                   // dgvData.ItemsSource = null;
                   // ChkAll.IsChecked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bấm nút lấy dữ liệu !"+ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void LblGetData_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ChkAll.IsChecked = false;
            cls.ClsConnect();
            try
            {
                int thamso = 4;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Mato";
                if (CboTo != null)
                    giatri[0] = str.Left(CboTo.SelectedValue.ToString().Trim(), 7);
                else
                {
                    MessageBox.Show("Chọn Tổ", "Mess");
                    return;
                }
                bien[1] = "@Ngay";
                if (dtpNgay.SelectedDate.Value == null)
                {
                    MessageBox.Show("Chưa chọn ngày ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                }
                
                bien[2] = "@Mau";
                if (Opt1.IsChecked == true || Opt2.IsChecked == true)
                {
                    giatri[2] = "0";
                }
                else
                {
                    giatri[2] = "1";
                }
                bien[3] = "@MaPos";
                giatri[3] = str.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                dt = cls.LoadDataProcPara("usp_DongCasa", bien, giatri, thamso);
                if (dt.Rows.Count > 0)
                {
                    dgvData.ItemsSource = dt.DefaultView;
                }
                else
                {
                    MessageBox.Show("Không có bản ghi nào ", "Mess");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }
        private void LblSaoKe_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            cls.ClsConnect();
            try
            {
                str.TaoThuMuc(Thumuc);
                string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                string pos = str.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                string strsql = "with lst1 as ("
                           +" select left(a.KU_MADP, 6) MAXA, a.KU_MATO, a.KU_MAKH, sum(a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH) DUNO "
                           +" from HSCV_DAILY a where a.ku_ngaybc = '"+ng+"' and a.KU_MAPGD = '"+pos+"' group by a.KU_MAKH, left(a.KU_MADP, 6), a.KU_MATO "
                           +" having sum(a.KU_DNOTHAN + a.KU_DNOQHAN + a.KU_DNOKHOANH) = 0), lst2 as ("
                           +" select a.CS_MAKH,a.CS_SO_TK2,a.CS_MATO from CASA_DAILY a where a.CS_MATO is not null and a.CS_NGAYBC = '"+ng+"' and a.CS_SP_TK = '105' and a.CS_TTSO_TK <> 'C' and a.CS_MAPGD = '"+pos+"' "
                           +" ) select a.MAXA,c.TEN,b.CS_MATO,d.TO_TENTT,a.KU_MAKH,e.KH_TENKH,a.DUNO,char(39) + b.CS_SO_TK2 SOTK from lst1 a "
                           +" left join DMXA c on a.MAXA = c.MA "
                           + " , hskh e, lst2 b,HSTO d  where  b.CS_MATO = d.TO_MATO and a.KU_MAKH = e.KH_MAKH and a.KU_MAKH = b.CS_MAKH order by a.MAXA, a.KU_MATO, a.KU_MAKH ";
                var dtchk = cls.LoadDataText(strsql);
                if (dtchk.Rows.Count > 0)
                {
                    FileName = Thumuc + "\\" + str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "_" + str.Left(CboTo.SelectedValue.ToString().Trim(), 7) + "_DONG105_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                    str.ExportToExcel(dtchk, FileName);
                    str.OpenExcel(FileName);
                }
                else
                {
                    MessageBox.Show("Không có bản ghi nào ", "Mess");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void CboPos_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                CboXa.Items.Clear();
                cls.ClsConnect();
                DataTable dtxa = new DataTable();
                string sql = "select MA,TEN from DMXA where PGD_QL= " + "'" + str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "'" + " order by MA";
                dtxa = cls.LoadDataText(sql);
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

        private void CboXa_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                CboTo.Items.Clear();
                cls.ClsConnect();
                DataTable dtto = new DataTable();
                string sql = "select TO_MATO,TO_TENTT from HSTO where Left(TO_MADP,6) = " + str.Left(CboXa.SelectedValue.ToString().Trim(), 6) +" order by TO_MATO ";
                //MessageBox.Show(sql);
                dtto = cls.LoadDataText(sql);
                for (int i = 0; i < dtto.Rows.Count; i++)
                {
                    CboTo.Items.Add(dtto.Rows[i][0] + " | " + dtto.Rows[i][1]);
                }
                CboTo.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }


        }

        private void ChkAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach (DataRow dr in dt.Rows)
            {
                dr[0] = true;
            }
        }

        private void ChkAll_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (DataRow dr in dt.Rows)
            {
                dr[0] = false;
            }

        }

    }
}
