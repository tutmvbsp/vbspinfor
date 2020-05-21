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
using System.Data;
using DAL;
using BLL;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfKhtc01.xaml
    /// </summary>
    public partial class WpfD15NHNN : Window
    {
        public WpfD15NHNN()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ToolBll bll = new ToolBll();
        string Thumuc = "C:\\SaoKe";
        private string FileName = "";
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            bll.TaoThuMuc(Thumuc);
            try
            {

                ServerInfor srv = new ServerInfor();
                cls.ClsConnect();
                DataTable dt = new DataTable();
                int thamso = 2;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Ngay";
                if (dtpNgay.SelectedDate != null) giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                bien[1] = "@MaPos";
                giatri[1] = bll.Left(cboPos.SelectedValue.ToString().Trim(), 6);
                if (radioButton1.IsChecked == true)
                {
                    FileName = Thumuc + "\\D15NHNN_" + bll.Left(cboPos.SelectedValue.ToString().Trim(), 6) + "_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                    dt = cls.LoadDataProcPara("usp_D15NHNN", bien, giatri, thamso);
                } 
                else if (radioButton2.IsChecked==true)
                {
                        FileName = Thumuc + "\\D20NHNN_" + bll.Left(cboPos.SelectedValue.ToString().Trim(), 6) + "_" +
                                   dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".xlsx";
                        dt = cls.LoadDataProcPara("usp_D20NHNN", bien, giatri, thamso);
                }
                else
                {
                    dt =
                        cls.LoadDataText(
                            "select CS_MAPGD,COUNT(distinct CS_MAKH) DEM from casa where CS_NGAYBC='"+dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd")+"' and CS_SODU_TK>0 group by CS_MAPGD");
                }

                if (dt.Rows.Count > 0)
                  {
                      if (radioButton1.IsChecked == true || radioButton2.IsChecked == true)
                      {
                          bll.WriteDataTableToExcel(dt, "Details", FileName, "TUTM");
                          MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK,
                                          MessageBoxImage.Information);
                      }
                      if (radioButton1.IsChecked == true)
                        {
                            rpt_D15NHNN rpt = new rpt_D15NHNN();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                             srv.DbPassSerVer());
                        }
                        else if (radioButton2.IsChecked==true)
                        {
                            rpt_D20NHNN rpt = new rpt_D20NHNN();
                            RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                                             srv.DbPassSerVer());
                        }
                        else
                        {
                            MessageBox.Show("CN01 : "+dt.Rows[0]["DEM"].ToString()+" | "+"CN02 : "+dt.Rows[1]["DEM"].ToString()+" | "+"CN03 : "+dt.Rows[2]["DEM"].ToString()+" | "+"CN04 : "+dt.Rows[3]["DEM"].ToString()+" | "+"CN05 : "+dt.Rows[4]["DEM"].ToString()+" | "+"CN06 : "+dt.Rows[5]["DEM"].ToString()+" | "+"CN07 : "+dt.Rows[6]["DEM"].ToString()+" | "+"CN08 : "+dt.Rows[7]["DEM"].ToString(),"Số hộ huy động vốn",MessageBoxButton.OK,MessageBoxImage.Information);
                        }

                    } 
                else
                    {
                     MessageBox.Show("Chưa có số liệu", "Thông báo");
                    }
                cls.DongKetNoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lổi "+ex.Message , "Thông báo",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            DataTable dtng = new DataTable();
            dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
            dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
            try
            {
                
                DataTable dtpos = new DataTable();
                //string sql = "select PO_MA,PO_TEN from DMPOS where PO_MACN=" + "'" + BienBll.MainPos + "'" + " order by PO_MA";
                dtpos = cls.LoadDataText("select PO_MA,PO_TEN from DMPOS order by PO_MA");
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    cboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                cboPos.SelectedIndex = 1;
            }
            catch 
            {
                MessageBox.Show("Tiếp tục", "Mess",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            cls.DongKetNoi();
        }
    }
}
