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
using System.IO;
using System.Globalization;
using DAL;
using BLL;
using System.Data;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDvut.xaml
    /// </summary>
    public partial class WpfChamCong : Window
    {
        public WpfChamCong()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ToolBll bll = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        DataTable dtxa = new DataTable();
        DataTable dtcs = new DataTable();
        private string str = "",mau="";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtpNgay.SelectedDate=DateTime.Now;
            cls.ClsConnect();
            //string sql = "select PO_MA,PO_TEN from DMPOS where PO_MA=" + "'" + BienBll.NdMadv.Trim() + "'";
            var sql = BienBll.NdCapbc.Trim() == "02" ? string.Format("select PO_MA,PO_TEN from DMPOS where PO_MA='{0}'", BienBll.NdMadv.Trim()) : "select PO_MA,PO_TEN from DMPOS where right(PO_MA,2)<>'00'";
            var dtpos = cls.LoadDataText(sql);
            for (int i = 0; i < dtpos.Rows.Count; i++)
            {
                CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
            }
//            CboPos.SelectedIndex = 0;
            CboPB.Items.Clear();
            if (BienBll.NdMadv.Trim() == BienBll.MainPos.Trim())
                dtxa = cls.LoadDataText("select * from DM_PHONGBAN where MA in ('18','19','20','21','22') order by MA");
            else dtxa = cls.LoadDataText("select * from DM_PHONGBAN where MA in ('29','30','31') order by MA");
            for (int i = 0; i < dtxa.Rows.Count; i++)
            {
                CboPB.Items.Add(dtxa.Rows[i][0].ToString().Trim() + " | " + dtxa.Rows[i][1]);
            }
            cls.DongKetNoi();
            btnIn.IsEnabled = false;
            if (BienBll.Ndma.Trim() == "TUTM0001") btnXoa.IsEnabled = true;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
            //MessageBox.Show(bll.Left(comboBoxMonth.SelectedValue.ToString().Trim(),2));
            //MessageBox.Show(comboBoxYear.SelectedValue.ToString().Trim());
        }
        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string thang = dtpNgay.SelectedDate.Value.Month.ToString();
                string nam = dtpNgay.SelectedDate.Value.Year.ToString();
                string phong = bll.Left(CboPB.SelectedValue.ToString().Trim(), 2);
                if (Ration1.IsChecked == true)
                    mau = "CC";
                else if (Ration2.IsChecked == true)
                    mau = "LT";
                else mau = "NB";
                cls.ClsConnect();
                var strsql= "delete from LUUCHAMCONG where THANG = '" + thang + "' and NAM = '" + nam + "' and ND_PHONGBAN = '" + phong + "' and MA='"+mau+"'";
                cls.UpdateDataText(strsql);
                MessageBox.Show("Đã xóa !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string thang = dtpNgay.SelectedDate.Value.Month.ToString();
            string nam = dtpNgay.SelectedDate.Value.Year.ToString();
            string phong = bll.Left(CboPB.SelectedValue.ToString().Trim(), 2);
            if (Ration1.IsChecked == true)
                mau = "CC";
            else if (Ration2.IsChecked == true)
                mau = "LT";
            else mau = "NB";
            try
            {
                dtNew = dt.GetChanges();
                cls.ClsConnect();
                if (dtNew != null)
                    foreach (DataRow dr in dtNew.Rows)
                    {
                        str = "update LUUCHAMCONG set [1]=upper('" + dr["1"] + "') " +
                              ",[2] = upper('" + dr["2"] + "') " +
                              ",[3] = upper('" + dr["3"] + "') " +
                              ",[4] = upper('" + dr["4"] + "') " +
                              ",[5] = upper('" + dr["5"] + "') " +
                              ",[6] = upper('" + dr["6"] + "') " +
                              ",[7] = upper('" + dr["7"] + "') " +
                              ",[8] = upper('" + dr["8"] + "') " +
                              ",[9] = upper('" + dr["9"] + "') " +
                              ",[10] = upper('" + dr["10"] + "') " +
                              ",[11] = upper('" + dr["11"] + "') " +
                              ",[12] = upper('" + dr["12"] + "') " +
                              ",[13] = upper('" + dr["13"] + "') " +
                              ",[14] = upper('" + dr["14"] + "') " +
                              ",[15] = upper('" + dr["15"] + "') " +
                              ",[16] = upper('" + dr["16"] + "') " +
                              ",[17] = upper('" + dr["17"] + "') " +
                              ",[18] = upper('" + dr["18"] + "') " +
                              ",[19] = upper('" + dr["19"] + "') " +
                              ",[20] = upper('" + dr["20"] + "') " +
                              ",[21] = upper('" + dr["21"] + "') " +
                              ",[22] = upper('" + dr["22"] + "') " +
                              ",[23] = upper('" + dr["23"] + "') " +
                              ",[24] = upper('" + dr["24"] + "') " +
                              ",[25] = upper('" + dr["25"] + "') " +
                              ",[26] = upper('" + dr["26"] + "') " +
                              ",[27] = upper('" + dr["27"] + "') " +
                              ",[28] = upper('" + dr["28"] + "') " +
                              ",[29] = upper('" + dr["29"] + "') " +
                              ",[30] = upper('" + dr["30"] + "') " +
                              ",[31] = upper('" + dr["31"] + "') " +
                              "where THANG='" +thang+"' and NAM='"+nam+"' and ND_PHONGBAN='"+phong+"' and upper(ND_MA)=upper('"+ dr["ND_MA"] + "') and ND_MA<>'00' and MA='"+dr["MA"]+"'";
                       // MessageBox.Show("Update OK! "+str, "Thông báo", MessageBoxButton.OK,MessageBoxImage.Information);
                        cls.LoadDataText(str);
                    }
                string strghichu = "update LUUCHAMCONG set GHICHU=N'"+txtGhiChu.Text.Trim()+ "' where THANG='" + thang + "' and NAM='" + nam + "' and ND_PHONGBAN='" + phong + "' and MA='" + mau + "'";
                //MessageBox.Show("Update OK! " + strghichu, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                cls.UpdateDataText(strghichu);
                //if (Ration2.IsChecked==true)
                //{ 
                string strup = "update LUUCHAMCONG set HL=(case when [1] not in ('TS','O','CD') and [1]<>'' then 1 else 0 end)"
                + "+ (case when [2] not in ('TS','O','CD') and [2]<>'' then 1 else 0 end)"
                + "+ (case when [3] not in ('TS','O','CD') and [3]<>'' then 1 else 0 end)"
                + "+ (case when [4] not in ('TS','O','CD') and [4]<>'' then 1 else 0 end)"
                + "+ (case when [5] not in ('TS','O','CD') and [5]<>'' then 1 else 0 end)"
                + "+ (case when [6] not in ('TS','O','CD') and [6]<>'' then 1 else 0 end)"
                + "+ (case when [7] not in ('TS','O','CD') and [7]<>'' then 1 else 0 end)"
                + "+ (case when [8] not in ('TS','O','CD') and [8]<>'' then 1 else 0 end)"
                + "+ (case when [9] not in ('TS','O','CD') and [9]<>'' then 1 else 0 end)"
                + "+ (case when [10] not in ('TS','O','CD') and [10]<>'' then 1 else 0 end)"
                + "+ (case when [11] not in ('TS','O','CD') and [11]<>'' then 1 else 0 end)"
                + "+ (case when [12] not in ('TS','O','CD') and [12]<>'' then 1 else 0 end)"
                + "+ (case when [13] not in ('TS','O','CD') and [13]<>'' then 1 else 0 end)"
                + "+ (case when [14] not in ('TS','O','CD') and [14]<>'' then 1 else 0 end)"
                + "+ (case when [15] not in ('TS','O','CD') and [15]<>'' then 1 else 0 end)"
                + "+ (case when [16] not in ('TS','O','CD') and [16]<>'' then 1 else 0 end)"
                + "+ (case when [17] not in ('TS','O','CD') and [17]<>'' then 1 else 0 end)"
                + "+ (case when [18] not in ('TS','O','CD') and [18]<>'' then 1 else 0 end)"
                + "+ (case when [19] not in ('TS','O','CD') and [19]<>'' then 1 else 0 end)"
                + "+ (case when [20] not in ('TS','O','CD') and [20]<>'' then 1 else 0 end)"
                + "+ (case when [21] not in ('TS','O','CD') and [21]<>'' then 1 else 0 end)"
                + "+ (case when [22] not in ('TS','O','CD') and [22]<>'' then 1 else 0 end)"
                + "+ (case when [23] not in ('TS','O','CD') and [23]<>'' then 1 else 0 end)"
                + "+ (case when [24] not in ('TS','O','CD') and [24]<>'' then 1 else 0 end)"
                + "+ (case when [25] not in ('TS','O','CD') and [25]<>'' then 1 else 0 end)"
                + "+ (case when [26] not in ('TS','O','CD') and [26]<>'' then 1 else 0 end)"
                + "+ (case when [27] not in ('TS','O','CD') and [27]<>'' then 1 else 0 end)"
                + "+ (case when [28] not in ('TS','O','CD') and [28]<>'' then 1 else 0 end)"
                + "+ (case when [29] not in ('TS','O','CD') and [29]<>'' then 1 else 0 end)"
                + "+ (case when [30] not in ('TS','O','CD') and [30]<>'' then 1 else 0 end)"
                + "+ (case when [31] not in ('TS','O','CD') and [31]<>'' then 1 else 0 end)"
                + ", NP = (case when[1] = 'P' then 1 else 0 end)"
                    + "+ (case when [2]='P' then 1 else 0 end)"
                    + "+ (case when [3]='P' then 1 else 0 end)"
                    + "+ (case when [4]='P' then 1 else 0 end)"
                    + "+ (case when [5]='P' then 1 else 0 end)"
                    + "+ (case when [6]='P' then 1 else 0 end)"
                    + "+ (case when [7]='P' then 1 else 0 end)"
                    + "+ (case when [8]='P' then 1 else 0 end)"
                    + "+ (case when [9]='P' then 1 else 0 end)"
                    + "+ (case when [10]='P' then 1 else 0 end)"
                    + "+ (case when [11]='P' then 1 else 0 end)"
                    + "+ (case when [12]='P' then 1 else 0 end)"
                    + "+ (case when [13]='P' then 1 else 0 end)"
                    + "+ (case when [14]='P' then 1 else 0 end)"
                    + "+ (case when [15]='P' then 1 else 0 end)"
                    + "+ (case when [16]='P' then 1 else 0 end)"
                    + "+ (case when [17]='P' then 1 else 0 end)"
                    + "+ (case when [18]='P' then 1 else 0 end)"
                    + "+ (case when [19]='P' then 1 else 0 end)"
                    + "+ (case when [20]='P' then 1 else 0 end)"
                    + "+ (case when [21]='P' then 1 else 0 end)"
                    + "+ (case when [22]='P' then 1 else 0 end)"
                    + "+ (case when [23]='P' then 1 else 0 end)"
                    + "+ (case when [24]='P' then 1 else 0 end)"
                    + "+ (case when [25]='P' then 1 else 0 end)"
                    + "+ (case when [26]='P' then 1 else 0 end)"
                    + "+ (case when [27]='P' then 1 else 0 end)"
                    + "+ (case when [28]='P' then 1 else 0 end)"
                    + "+ (case when [29]='P' then 1 else 0 end)"
                    + "+ (case when [30]='P' then 1 else 0 end)"
                    + "+ (case when [31]='P' then 1 else 0 end)"
		           + ",NB = (case when[1] = 'NB' then 1 else 0 end)"
                   + "+ (case when [2]='NB' then 1 else 0 end)"
                   + "+ (case when [3]='NB' then 1 else 0 end)"
                   + "+ (case when [4]='NB' then 1 else 0 end)"
                   + "+ (case when [5]='NB' then 1 else 0 end)"
                   + "+ (case when [6]='NB' then 1 else 0 end)"
                   + "+ (case when [7]='NB' then 1 else 0 end)"
                   + "+ (case when [8]='NB' then 1 else 0 end)"
                   + "+ (case when [9]='NB' then 1 else 0 end)"
                   + "+ (case when [10]='NB' then 1 else 0 end)"
                   + "+ (case when [11]='NB' then 1 else 0 end)"
                   + "+ (case when [12]='NB' then 1 else 0 end)"
                   + "+ (case when [13]='NB' then 1 else 0 end)"
                   + "+ (case when [14]='NB' then 1 else 0 end)"
                   + "+ (case when [15]='NB' then 1 else 0 end)"
                   + "+ (case when [16]='NB' then 1 else 0 end)"
                   + "+ (case when [17]='NB' then 1 else 0 end)"
                   + "+ (case when [18]='NB' then 1 else 0 end)"
                   + "+ (case when [19]='NB' then 1 else 0 end)"
                   + "+ (case when [20]='NB' then 1 else 0 end)"
                   + "+ (case when [21]='NB' then 1 else 0 end)"
                   + "+ (case when [22]='NB' then 1 else 0 end)"
                   + "+ (case when [23]='NB' then 1 else 0 end)"
                   + "+ (case when [24]='NB' then 1 else 0 end)"
                   + "+ (case when [25]='NB' then 1 else 0 end)"
                   + "+ (case when [26]='NB' then 1 else 0 end)"
                   + "+ (case when [27]='NB' then 1 else 0 end)"
                   + "+ (case when [28]='NB' then 1 else 0 end)"
                   + "+ (case when [29]='NB' then 1 else 0 end)"
                   + "+ (case when [30]='NB' then 1 else 0 end)"
                   + "+ (case when [31]='NB' then 1 else 0 end)"
		           + ",HOC = (case when[1] = 'H' then 1 else 0 end)"
                   + "+ (case when [2]='H' then 1 else 0 end)"
                   + "+ (case when [3]='H' then 1 else 0 end)"
                   + "+ (case when [4]='H' then 1 else 0 end)"
                   + "+ (case when [5]='H' then 1 else 0 end)"
                   + "+ (case when [6]='H' then 1 else 0 end)"
                   + "+ (case when [7]='H' then 1 else 0 end)"
                   + "+ (case when [8]='H' then 1 else 0 end)"
                   + "+ (case when [9]='H' then 1 else 0 end)"
                   + "+ (case when [10]='H' then 1 else 0 end)"
                   + "+ (case when [11]='H' then 1 else 0 end)"
                   + "+ (case when [12]='H' then 1 else 0 end)"
                   + "+ (case when [13]='H' then 1 else 0 end)"
                   + "+ (case when [14]='H' then 1 else 0 end)"
                   + "+ (case when [15]='H' then 1 else 0 end)"
                   + "+ (case when [16]='H' then 1 else 0 end)"
                   + "+ (case when [17]='H' then 1 else 0 end)"
                   + "+ (case when [18]='H' then 1 else 0 end)"
                   + "+ (case when [19]='H' then 1 else 0 end)"
                   + "+ (case when [20]='H' then 1 else 0 end)"
                   + "+ (case when [21]='H' then 1 else 0 end)"
                   + "+ (case when [22]='H' then 1 else 0 end)"
                   + "+ (case when [23]='H' then 1 else 0 end)"
                   + "+ (case when [24]='H' then 1 else 0 end)"
                   + "+ (case when [25]='H' then 1 else 0 end)"
                   + "+ (case when [26]='H' then 1 else 0 end)"
                   + "+ (case when [27]='H' then 1 else 0 end)"
                   + "+ (case when [28]='H' then 1 else 0 end)"
                   + "+ (case when [29]='H' then 1 else 0 end)"
                   + "+ (case when [30]='H' then 1 else 0 end)"
                   + "+ (case when [31]='H' then 1 else 0 end)"
	               + ",BHXH = (case when[1] in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [2]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [3]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [4]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [5]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [6]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [7]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [8]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [9]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [10]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [11]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [12]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [13]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [14]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [15]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [16]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [17]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [18]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [19]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [20]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [21]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [22]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [23]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [24]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [25]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [26]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [27]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [28]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [29]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [30]in ('O','TS') then 1 else 0 end)"
                   + "+ (case when [31]in ('O','TS') then 1 else 0 end)"
                   + ",NVR = (case when[1] = 'VR' then 1 else 0 end)"
                   + "+ (case when [2]='VR' then 1 else 0 end)"
                   + "+ (case when [3]='VR' then 1 else 0 end)"
                   + "+ (case when [4]='VR' then 1 else 0 end)"
                   + "+ (case when [5]='VR' then 1 else 0 end)"
                   + "+ (case when [6]='VR' then 1 else 0 end)"
                   + "+ (case when [7]='VR' then 1 else 0 end)"
                   + "+ (case when [8]='VR' then 1 else 0 end)"
                   + "+ (case when [9]='VR' then 1 else 0 end)"
                   + "+ (case when [10]='VR' then 1 else 0 end)"
                   + "+ (case when [11]='VR' then 1 else 0 end)"
                   + "+ (case when [12]='VR' then 1 else 0 end)"
                   + "+ (case when [13]='VR' then 1 else 0 end)"
                   + "+ (case when [14]='VR' then 1 else 0 end)"
                   + "+ (case when [15]='VR' then 1 else 0 end)"
                   + "+ (case when [16]='VR' then 1 else 0 end)"
                   + "+ (case when [17]='VR' then 1 else 0 end)"
                   + "+ (case when [18]='VR' then 1 else 0 end)"
                   + "+ (case when [19]='VR' then 1 else 0 end)"
                   + "+ (case when [20]='VR' then 1 else 0 end)"
                   + "+ (case when [21]='VR' then 1 else 0 end)"
                   + "+ (case when [22]='VR' then 1 else 0 end)"
                   + "+ (case when [23]='VR' then 1 else 0 end)"
                   + "+ (case when [24]='VR' then 1 else 0 end)"
                   + "+ (case when [25]='VR' then 1 else 0 end)"
                   + "+ (case when [26]='VR' then 1 else 0 end)"
                   + "+ (case when [27]='VR' then 1 else 0 end)"
                   + "+ (case when [28]='VR' then 1 else 0 end)"
                   + "+ (case when [29]='VR' then 1 else 0 end)"
                   + "+ (case when [30]='VR' then 1 else 0 end)"
                   + "+ (case when [31]='VR' then 1 else 0 end)"
                   + ",NKL = (case when[1] = 'NO' then 1 else 0 end)"
                   + "+ (case when [2]='NO' then 1 else 0 end)"
                   + "+ (case when [3]='NO' then 1 else 0 end)"
                   + "+ (case when [4]='NO' then 1 else 0 end)"
                   + "+ (case when [5]='NO' then 1 else 0 end)"
                   + "+ (case when [6]='NO' then 1 else 0 end)"
                   + "+ (case when [7]='NO' then 1 else 0 end)"
                   + "+ (case when [8]='NO' then 1 else 0 end)"
                   + "+ (case when [9]='NO' then 1 else 0 end)"
                   + "+ (case when [10]='NO' then 1 else 0 end)"
                   + "+ (case when [11]='NO' then 1 else 0 end)"
                   + "+ (case when [12]='NO' then 1 else 0 end)"
                   + "+ (case when [13]='NO' then 1 else 0 end)"
                   + "+ (case when [14]='NO' then 1 else 0 end)"
                   + "+ (case when [15]='NO' then 1 else 0 end)"
                   + "+ (case when [16]='NO' then 1 else 0 end)"
                   + "+ (case when [17]='NO' then 1 else 0 end)"
                   + "+ (case when [18]='NO' then 1 else 0 end)"
                   + "+ (case when [19]='NO' then 1 else 0 end)"
                   + "+ (case when [20]='NO' then 1 else 0 end)"
                   + "+ (case when [21]='NO' then 1 else 0 end)"
                   + "+ (case when [22]='NO' then 1 else 0 end)"
                   + "+ (case when [23]='NO' then 1 else 0 end)"
                   + "+ (case when [24]='NO' then 1 else 0 end)"
                   + "+ (case when [25]='NO' then 1 else 0 end)"
                   + "+ (case when [26]='NO' then 1 else 0 end)"
                   + "+ (case when [27]='NO' then 1 else 0 end)"
                   + "+ (case when [28]='NO' then 1 else 0 end)"
                   + "+ (case when [29]='NO' then 1 else 0 end)"
                   + "+ (case when [30]='NO' then 1 else 0 end)"
                   + "+ (case when [31]='NO' then 1 else 0 end)"
                   + ",OM = (case when[1] = 'Ô' then 1 else 0 end)"
                   + "+ (case when [2]='Ô' then 1 else 0 end)"
                   + "+ (case when [3]='Ô' then 1 else 0 end)"
                   + "+ (case when [4]='Ô' then 1 else 0 end)"
                   + "+ (case when [5]='Ô' then 1 else 0 end)"
                   + "+ (case when [6]='Ô' then 1 else 0 end)"
                   + "+ (case when [7]='Ô' then 1 else 0 end)"
                   + "+ (case when [8]='Ô' then 1 else 0 end)"
                   + "+ (case when [9]='Ô' then 1 else 0 end)"
                   + "+ (case when [10]='Ô' then 1 else 0 end)"
                   + "+ (case when [11]='Ô' then 1 else 0 end)"
                   + "+ (case when [12]='Ô' then 1 else 0 end)"
                   + "+ (case when [13]='Ô' then 1 else 0 end)"
                   + "+ (case when [14]='Ô' then 1 else 0 end)"
                   + "+ (case when [15]='Ô' then 1 else 0 end)"
                   + "+ (case when [16]='Ô' then 1 else 0 end)"
                   + "+ (case when [17]='Ô' then 1 else 0 end)"
                   + "+ (case when [18]='Ô' then 1 else 0 end)"
                   + "+ (case when [19]='Ô' then 1 else 0 end)"
                   + "+ (case when [20]='Ô' then 1 else 0 end)"
                   + "+ (case when [21]='Ô' then 1 else 0 end)"
                   + "+ (case when [22]='Ô' then 1 else 0 end)"
                   + "+ (case when [23]='Ô' then 1 else 0 end)"
                   + "+ (case when [24]='Ô' then 1 else 0 end)"
                   + "+ (case when [25]='Ô' then 1 else 0 end)"
                   + "+ (case when [26]='Ô' then 1 else 0 end)"
                   + "+ (case when [27]='Ô' then 1 else 0 end)"
                   + "+ (case when [28]='Ô' then 1 else 0 end)"
                   + "+ (case when [29]='Ô' then 1 else 0 end)"
                   + "+ (case when [30]='Ô' then 1 else 0 end)"
                   + "+ (case when [31]='Ô' then 1 else 0 end)"
                   + ",CS = (case when[1] = 'CS' then 1 else 0 end)"
                   + "+ (case when [2]='CS' then 1 else 0 end)"
                   + "+ (case when [3]='CS' then 1 else 0 end)"
                   + "+ (case when [4]='CS' then 1 else 0 end)"
                   + "+ (case when [5]='CS' then 1 else 0 end)"
                   + "+ (case when [6]='CS' then 1 else 0 end)"
                   + "+ (case when [7]='CS' then 1 else 0 end)"
                   + "+ (case when [8]='CS' then 1 else 0 end)"
                   + "+ (case when [9]='CS' then 1 else 0 end)"
                   + "+ (case when [10]='CS' then 1 else 0 end)"
                   + "+ (case when [11]='CS' then 1 else 0 end)"
                   + "+ (case when [12]='CS' then 1 else 0 end)"
                   + "+ (case when [13]='CS' then 1 else 0 end)"
                   + "+ (case when [14]='CS' then 1 else 0 end)"
                   + "+ (case when [15]='CS' then 1 else 0 end)"
                   + "+ (case when [16]='CS' then 1 else 0 end)"
                   + "+ (case when [17]='CS' then 1 else 0 end)"
                   + "+ (case when [18]='CS' then 1 else 0 end)"
                   + "+ (case when [19]='CS' then 1 else 0 end)"
                   + "+ (case when [20]='CS' then 1 else 0 end)"
                   + "+ (case when [21]='CS' then 1 else 0 end)"
                   + "+ (case when [22]='CS' then 1 else 0 end)"
                   + "+ (case when [23]='CS' then 1 else 0 end)"
                   + "+ (case when [24]='CS' then 1 else 0 end)"
                   + "+ (case when [25]='CS' then 1 else 0 end)"
                   + "+ (case when [26]='CS' then 1 else 0 end)"
                   + "+ (case when [27]='CS' then 1 else 0 end)"
                   + "+ (case when [28]='CS' then 1 else 0 end)"
                   + "+ (case when [29]='CS' then 1 else 0 end)"
                   + "+ (case when [30]='CS' then 1 else 0 end)"
                   + "+ (case when [31]='CS' then 1 else 0 end)"

                   + " where THANG = '" +thang+"' and NAM = '"+nam+"' and ND_PHONGBAN = '"+phong+"' and MA='CC'";
                   cls.UpdateDataText(strup);
                //}
                MessageBox.Show("Lưu thành công !", "Thông báo", MessageBoxButton.OK,MessageBoxImage.Information);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
            btnIn.IsEnabled = true;
        }
   

    


 

        private void BtnIn_OnClick(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            {
                try
                {
                    MessageBox.Show("Để số ngày hưởng cá đúng do có nghỉ bù và làm thêm trong tháng, cần chấm làm thêm trước khi in!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    if (Ration1.IsChecked == true)
                        mau = "CC";
                    else if (Ration2.IsChecked == true)
                        mau = "LT";
                    else mau = "NB";
                    int thamso = 4;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    bien[1] = "@Ngay";
                    if (dtpNgay.SelectedDate != null) giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                    bien[2] = "@Phong";
                    giatri[2] = bll.Left(CboPB.SelectedValue.ToString().Trim(), 2);
                    bien[3] = "@Mau";
                    giatri[3] = mau;

                    //---------------------------
                    if (Ration1.IsChecked == true) cls.UpdateDataProcPara("usp_ChamCong02", bien, giatri, thamso);
                    if (Ration2.IsChecked == true) cls.UpdateDataProcPara("usp_ChamCong01", bien, giatri, thamso);
                    //----------------------------
                    if (mau == "CC")
                    {
                        str = "select * from LUUCHAMCONG where ND_MADV='" +
                              bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "' and THANG='" +
                              dtpNgay.SelectedDate.Value.Month + "' and NAM='" +
                              dtpNgay.SelectedDate.Value.Year + "' and ND_PHONGBAN='" +
                              bll.Left(CboPB.SelectedValue.ToString().Trim(), 2) + "' and ND_MA<>'00' and MA='" + mau +
                              "' order by STT";
                        string str1 = "select * from LUUCHAMCONG where ND_MADV='" +
                              bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "' and THANG='" +
                              dtpNgay.SelectedDate.Value.Month + "' and NAM='" +
                              dtpNgay.SelectedDate.Value.Year + "' and ND_PHONGBAN='" +
                              bll.Left(CboPB.SelectedValue.ToString().Trim(), 2) + "' and ND_MA<>'00' and MA='" + mau +
                              "' and CS>0 order by STT";
                        dtcs = cls.LoadDataText(str1);

                    }
                    else
                    {
                        str = "select * from LUUCHAMCONG where ND_MADV='" +
                              bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "' and THANG='" +
                              dtpNgay.SelectedDate.Value.Month + "' and NAM='" +
                              dtpNgay.SelectedDate.Value.Year + "' and ND_PHONGBAN='" +
                              bll.Left(CboPB.SelectedValue.ToString().Trim(), 2) + "' and MA='" + mau +
                              "' and [32]+[33]+[34]+[35]>0  order by STT";
                    }
                    dt = cls.LoadDataText(str);
                    //MessageBox.Show(str);
                    if (mau == "CC")
                    {
                        rpt_ChamCong rpt = new rpt_ChamCong();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                        if (dtcs.Rows.Count > 0)
                        {
                            rpt_ChamCong07 rpt1 = new rpt_ChamCong07();
                            RPUtility.ShowRp(rpt1, dtcs, this, srv.DbSourceSerVer(), srv.DbNameSerVer(),
                                srv.DbUserSerVer(), srv.DbPassSerVer());

                            string str2 = "select '"+BienBll.NdTen.Trim()+"' TENCB,c.TEN TENCHUCVU,b.ND_CHUCVU,a.* from LUUCHAMCONG a"
                                          + " left join DM_CANBO b on a.ND_MA = b.MA_CIF "
                                          + " left join DM_CHUCVU c on b.ND_CHUCVU = c.MA"
                                          +
                                          " where a.ND_MADV = '"+ bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) 
                                          + "' and a.THANG = '"+ dtpNgay.SelectedDate.Value.Month 
                                          + "' and a.NAM = '"+ dtpNgay.SelectedDate.Value.Year 
                                          + "' and a.ND_PHONGBAN = '"+ bll.Left(CboPB.SelectedValue.ToString().Trim(), 2) 
                                          + "' and a.ND_MA <> '00' and a.MA = '"+mau+"' and a.CS>0 order by a.STT";
                            var dttr = cls.LoadDataText(str2);
                            rpt_ChamCong08 rpt2 = new rpt_ChamCong08();
                            RPUtility.ShowRp(rpt2, dttr, this, srv.DbSourceSerVer(), srv.DbNameSerVer(),
                                srv.DbUserSerVer(), srv.DbPassSerVer());

                        }

                    }
                    else
                    {
                        
                        rpt_ChamCong01 rpt = new rpt_ChamCong01();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                        //RPUtility.ShowRpOnePara(rpt, dt,txtGhiChu.Text.Trim(), this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi, kiểm tra lại thông tin " + ex.Message, "Thông báo", MessageBoxButton.OK,
                        MessageBoxImage.Error);

                }

            }
            cls.DongKetNoi();
            btnIn.IsEnabled = false;
        }

        private void Ration2_Checked(object sender, RoutedEventArgs e)
        {
            txtGhiChu.IsEnabled = true;
        }

        private void Ration1_Checked(object sender, RoutedEventArgs e)
        {
            //txtGhiChu.IsEnabled = false;
        }

        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //MessageBox.Show(bll.Left(cboPos.SelectedValue.ToString().Trim(),6));
                CboPB.Items.Clear();
                cls.ClsConnect();
                if (bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) == BienBll.MainPos.Trim())
                    dtxa = cls.LoadDataText("select * from DM_PHONGBAN where MA in ('18','19','20','21','22') order by MA");
                else dtxa = cls.LoadDataText("select * from DM_PHONGBAN where MA in ('29','30','31') order by MA");
                for (int i = 0; i < dtxa.Rows.Count; i++)
                {
                    CboPB.Items.Add(dtxa.Rows[i][0].ToString().Trim() + " | " + dtxa.Rows[i][1]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }

        private void LblGetData_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

                cls.ClsConnect();
                int thamso = 5;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@MaPos";
                giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                bien[1] = "@Ngay";
                if (dtpNgay.SelectedDate != null) giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                bien[2] = "@Phong";
                giatri[2] = bll.Left(CboPB.SelectedValue.ToString().Trim(), 2);
                bien[3] = "@Mau";
                if (Ration1.IsChecked == true)
                    giatri[3] = "CC";
                else if (Ration2.IsChecked == true)
                    giatri[3] = "LT";
                else giatri[3] = "NB";
                bien[4] = "@GhiChu";
                giatri[4] = txtGhiChu.Text.Trim();

                dt = cls.LoadDataProcPara("usp_ChamCong", bien, giatri, thamso);
                //rpt_kt740_01 rpt = new rpt_kt740_01();
                if (dt.Rows.Count > 0)
                {
                    dgvSource.ItemsSource = dt.DefaultView;
                    txtGhiChu.Text = dt.Rows[0]["GHICHU"].ToString();
                    // rpt_SkeTo rpt = new rpt_SkeTo();
                    // RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                    // string filename = "C:\\Tam\\" + str.Left(cboTo.SelectedValue.ToString().Trim(), 7) + ".xlsx";
                    // bll.WriteDataTableToExcel(dt, "Person Details", filename, "Details");
                    //dtNew = dt.GetChanges();
                }
                else
                {
                    MessageBox.Show("Chưa có số liệu", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chưa chọn Phòng / Tổ "+ ex.Message,"Thông báo",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
            cls.DongKetNoi();


        }
    }
}
