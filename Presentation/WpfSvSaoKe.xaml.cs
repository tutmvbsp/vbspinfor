using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfLaiTon.xaml
    /// </summary>
    public partial class WpfSvSaoke : Window
    {
        public WpfSvSaoke()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ToolBll bll  = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        private string FileName = "";
        string Thumuc = "C:\\SaoKe";
        
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            dtpTuNgay.SelectedDate = DateTime.Now.AddMonths(-1);//DateTime.Parse("01/01/" + DateTime.Now.AddYears(-1).ToString("yyyy"));
            dtpDenNgay.SelectedDate = DateTime.Now.AddDays(-1);
            try
            {
                cls.ClsConnect();
                string sql = "select PO_MA,PO_TEN from DMPOS order by PO_MA";
                var dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                CboPos.SelectedIndex = 0;
                var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();

        }

        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CboXa.Items.Clear();
                cls.ClsConnect();
                string sql = "select MA,TEN from DMXA where PGD_QL= " + "'" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "'" + " order by MA";
                var dtxa = cls.LoadDataText(sql);
                for (int i = 0; i < dtxa.Rows.Count; i++)
                {
                    CboXa.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                }
                CboXa.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }

        }

    

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                const int thamso = 4;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Ngay";
                if (dtpNgay.SelectedDate != null)
                {
                    giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    bien[1] = "@Maxa";
                    giatri[1] = bll.Left(CboXa.SelectedValue.ToString(),6);
                    bien[2] = "@TuNgay";
                    if (dtpTuNgay.SelectedDate != null)
                    {
                        giatri[2] = dtpTuNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                        bien[3] = "@DenNgay";
                        if (dtpDenNgay.SelectedDate != null)
                            giatri[3] = dtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    }
                }
                dt = cls.LoadDataProcPara("usp_SkeSvRaTruong", bien, giatri, thamso);
                if (dt.Rows.Count > 0)
                {
                    FileName = Thumuc + "\\" + giatri[0] + "_" + giatri[1] + "_Sao kê sinh viên ra trường_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                    bll.ExportToExcel(dt, FileName);
                    //bll.ExportDTToExcel(dt,FileName);
                    //bll.WriteDataTableToExcel(dt, "Details", FileName, "tutm : 0985165777");
                    //bll.ToCSV(dt, sw, true);
                    MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    //bll.OpenCSVWithExcel(FileName);
                    bll.OpenExcel(FileName);

                    //rpt_DinhSv rpt = new rpt_DinhSv();
                    //RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
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
            cls.DongKetNoi();
        }
        

        /*
        private void Ration3_Checked(object sender, RoutedEventArgs e)
        {
            Close();
            WpfDinhSv f = new WpfDinhSv();
            f.ShowDialog();
        }

        private void Ration2_Checked(object sender, RoutedEventArgs e)
        {
            Close();
            WpfDinhSv f = new WpfDinhSv();
            f.ShowDialog();
        }
         */

        private void LblThKe_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                bll.TaoThuMuc(Thumuc);
                const int thamso = 5;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@MaPos";
                giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                bien[1] = "@MaXa";
                if (bll.Right(giatri[0].ToString(), 2) == "00")
                {
                    giatri[1] = giatri[0];
                }
                else
                {
                    giatri[1] = bll.Left(CboXa.SelectedValue.ToString().Trim(), 6);
                }
                bien[2] = "@Ngay";
                if (dtpNgay.SelectedDate != null)
                {
                    giatri[2] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    bien[3] = "@TuNgay";
                    if (dtpTuNgay.SelectedDate != null)
                    {
                        giatri[3] = dtpTuNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                        bien[4] = "@DenNgay";
                        if (dtpDenNgay.SelectedDate != null)
                            giatri[4] = dtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    }
                }
                //MessageBox.Show(giatri[0].ToString() + giatri[1].ToString() + giatri[2].ToString() + giatri[3].ToString() + giatri[4].ToString());
                dt = cls.LoadDataProcPara("usp_DinhSvThKe", bien, giatri, thamso);
                if (dt.Rows.Count > 0)
                {
                    FileName = Thumuc + "\\" + giatri[0]+"_"+giatri[1] + "_THONGKE_HSSV_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                    bll.ExportToExcel(dt,FileName);
                    //bll.ExportDTToExcel(dt,FileName);
                    //bll.WriteDataTableToExcel(dt, "Details", FileName, "tutm : 0985165777");
                    //bll.ToCSV(dt, sw, true);
                    MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    //bll.OpenCSVWithExcel(FileName);
                    bll.OpenExcel(FileName);

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
            cls.DongKetNoi(); 
        }
    }
}
