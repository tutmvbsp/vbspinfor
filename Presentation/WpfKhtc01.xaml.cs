﻿using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfKhtc01.xaml
    /// </summary>
    public partial class WpfKhtc01
    {
        public WpfKhtc01()
        {
            InitializeComponent();
        }

        readonly ClsServer _cls = new ClsServer();
        DataTable _dt = new DataTable();
        readonly ToolBll _str = new ToolBll();
        private string sql = "";
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show(BienBll.Ndma);
            ServerInfor srv = new ServerInfor();
            _cls.ClsConnect();
            int thamso = 3;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@Ngay";
            if (dtpNgay.SelectedDate != null) giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
            bien[1] = "@MaPos";
            giatri[1] = _str.Left(cboPos.SelectedValue.ToString().Trim(), 6);
            string mau = "";
            if (ration1.IsChecked == true) mau = "1";
            else if (ration2.IsChecked == true) mau = "2";
            else if (ration3.IsChecked == true) mau = "3";
            else mau = "4";
            bien[2] = "@Mau";
            giatri[2] = mau;
            _dt = _cls.LoadDataProcPara("usp_Khtc01", bien, giatri, thamso);
            if (_dt.Rows.Count > 0)
            {
                rpt_Khtc01 rpt = new rpt_Khtc01();
                RPUtility.ShowRp(rpt, _dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
            }
            else
            {
                MessageBox.Show("Chưa có số liệu", "Thông báo");
            }
            _cls.DongKetNoi();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dtpNgay.SelectedDate = DateTime.Parse(DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month).ToString());
            try
            {
                _cls.ClsConnect();
                if (BienBll.NdMadv == BienBll.MainPos)
                {
                    sql = "select PO_MA,PO_TEN from DMPOS order by PO_MA";
                }
                else
                {
                    sql = "select PO_MA,PO_TEN from DMPOS where PO_MA='" + BienBll.NdMadv + "'";
                }
                var dtpos = _cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    cboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                cboPos.SelectedIndex = 1;
                var dtng = _cls.LoadDataText("select MAX(convert(date,NGAY,105)) as NGMAX from U_CANDOI");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            _cls.DongKetNoi();
        }
        private void LblManual_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            string mau = "";
            if (ration1.IsChecked == true) mau = "1";
            else if (ration2.IsChecked == true) mau = "2";
            else if (ration3.IsChecked == true) mau = "3";
            else mau = "4";
            if (dtpNgay.SelectedDate != null)
            {
                string sql = "select top 1 * from LUU_PL05 where ngay='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") +
                             "' and MAU='"+mau+"'";
                _cls.ClsConnect();
                _dt = _cls.LoadDataText(sql);
                _cls.DongKetNoi();
                if (_dt.Rows.Count > 0)
                {
                    MessageBox.Show("Đã có số liệu ngày : " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"),
                        "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Chưa có số liệu ngày : " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy") + " Có muốn tạo không ?",
                 "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            _cls.ClsConnect();
                            const int thamso = 2;
                            string[] bien = new string[thamso];
                            object[] giatri = new object[thamso];
                            bien[0] = "@Ngay";
                            giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                            bien[1] = "@Mau";
                            giatri[1] = mau;
                            _cls.UpdateLdbf("usp_PL05", bien, giatri, thamso);
                            MessageBox.Show(
                                "PL05-Tạo xong số liệu ngày : " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"),
                                "Thông báo",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                            _cls.DongKetNoi();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(
                            "Bảng quyết toán sẽ không đúng khi không tạo số liệu ngày :  " +
                           dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Error: Chưa chọn ngày!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LblXoa_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
                string sql = "delete from LUU_PL05 where ngay='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") +"'";
                _cls.ClsConnect();
                _cls.LoadDataText(sql);
                _cls.DongKetNoi();
                MessageBox.Show("Đã xóa ngày "+ dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd"), "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
