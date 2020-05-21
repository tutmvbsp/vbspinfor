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
using CrystalDecisions.Shared;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDienbao.xaml
    /// </summary>
    public partial class WpfDienbao : Window
    {
        public WpfDienbao()
        {
            InitializeComponent();
        }

        private ClsServer _cls = new ClsServer();
        private ServerInfor srv = new ServerInfor();
        private ToolBll _str = new ToolBll();
        DataTable _dt = new DataTable();
        private string sql = "";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);

        }

        private void btnReOk_Click(object sender, RoutedEventArgs e)
        {
            _cls.ClsConnect();
            sql = "delete from LUU_DIENBAO where NGAY = '" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "'";
            //MessageBox.Show(sql);
            _cls.UpdateDataText(sql);
            _cls.DongKetNoi();
            BtnOk_OnClick(null, null);

        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            _cls.ClsConnect();
            try
            {
                sql = "select * from U_HSTD where NGAYKU='" + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") + "'";
                _dt=_cls.LoadDataText(sql);
                if (_dt.Rows.Count == 0)
                {
                    MessageBox.Show("Chưa có HSTDCT ngày " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Close();
                }
                else
                {
                    sql = "select top 1 * from LUU_DIENBAO where NGAY='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "'";
                    _dt = _cls.LoadDataText(sql);
                    #region
                    if (_dt.Rows.Count == 0)
                    {
                        int thamso = 1;
                        string[] bien = new string[thamso];
                        object[] giatri = new object[thamso];
                        bien[0] = "@Ngay";
                        if (dtpNgay.SelectedDate != null) giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                        _dt = _cls.LoadDataProcPara("usp_DienBao", bien, giatri, thamso);
                    }

                    if (BienBll.NdMadv.Trim() == "003005") sql = "select * from LUU_DIENBAO where CHONIN='1' and NGAY='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' order by TT";
                    else sql = "select " + BienBll.NdMadv.Trim() + " P01, NGAY,STT,TT,TENCT,P" + _str.Right(BienBll.NdMadv.Trim(), 2) + " TONG,Indam,NHOM,SUB_NHOM from LUU_DIENBAO where CHONIN='1' and NGAY='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and P" + _str.Right(BienBll.NdMadv.Trim(), 2) + ">0 order by TT";
                    //MessageBox.Show(sql);
                    _dt = _cls.LoadDataText(sql);
                    if (BienBll.NdMadv.Trim() == "003005")
                    {
                        rpt_Dienbao rpt = new rpt_Dienbao();
                        RPUtility.ShowRp(rpt, _dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                        /*
                        ExportOptions CrExportOptions;
                        DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                        ExcelFormatOptions CrFormatTypeOptions = new ExcelFormatOptions();
                        CrDiskFileDestinationOptions.DiskFileName = "c:\\text\\csharp.net-informations.xls";
                        CrExportOptions = rpt.ExportOptions;
                        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        CrExportOptions.ExportFormatType = ExportFormatType.Excel;
                        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                        CrExportOptions.FormatOptions = CrFormatTypeOptions;
                        rpt.Export();
                        */
                        //rpt.ExportToDisk(ExportFormatType.Excel, "c:\\text\\DB.xls");
                    }
                    else
                    {
                        rpt_DienbaoH rpt = new rpt_DienbaoH();
                        RPUtility.ShowRp(rpt, _dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                            srv.DbPassSerVer());

                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                
               MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _cls.DongKetNoi();
        }

        private void LblManual_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _cls.ClsConnect();
            sql = "delete from LUU_DIENBAO where NGAY = '" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "'";
            //MessageBox.Show(sql);
            _cls.UpdateDataText(sql);
            _cls.DongKetNoi();
            BtnOk_OnClick(null, null);
        }

        private void LblNguon_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (BienBll.Ndma.Trim().ToUpper() == "THUYPTT0001" || BienBll.Ndma.Trim().ToUpper() == "TUTM0001")
            {
                DateTime NG = dtpNgay.SelectedDate.Value;
                WpfNguonDB f = new WpfNguonDB(NG);
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn không vào mục này !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
    }
}
