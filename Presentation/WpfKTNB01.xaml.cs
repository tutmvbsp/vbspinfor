using System;
using System.Data;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Input;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfLaiTon.xaml
    /// </summary>
    public partial class WpfKTNB01
    {
        public WpfKTNB01()
        {
            InitializeComponent();
        }

        ToolBll s = new ToolBll();
        private readonly ClsServer cls = new ClsServer();
        private DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        ServerInfor srv = new ServerInfor();
        private string ma = "";
        private string strin = "";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                var sql = BienBll.NdCapbc.Trim() == "02" ? string.Format("select PO_MA,PO_TEN from DMPOS where PO_MA='{0}'", BienBll.NdMadv.Trim()) : "select PO_MA,PO_TEN from DMPOS";
                var dtpos = cls.LoadDataText(sql);
                for (var i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                dtpNgay.SelectedDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
            
        }

        
        private void btnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                string pos = s.Left(CboPos.SelectedValue.ToString(), 6);
                string thang = dtpNgay.SelectedDate.Value.ToString("MM");
                string nam = dtpNgay.SelectedDate.Value.ToString("yyyy");
                dtNew = dt.GetChanges();
                if (dtNew != null && dtNew.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtNew.Rows)
                    {
                        //MessageBox.Show(dr["NG_MATO"].ToString()+"      "+dr["A01"].ToString());
                        string strsql = "update LUUKTNB set COT03=" +dr["COT03"] + ",COT04=" + dr["COT04"] + ",COT05=" + dr["COT05"] 
                                        + ",COT06=" + dr["COT06"] + ",COT07=" + dr["COT07"]
                                        + ",COT08=" + dr["COT08"] + ",COT09=" + dr["COT09"] + ",COT10=" + dr["COT10"] + ",COT11=" +
                                        dr["COT11"] + ",COT12=" + dr["COT12"] + ",COT13=" + dr["COT13"] + ",COT14=" + dr["COT14"] +
                                        ",COT15=" + dr["COT15"] + ",COT16=" + dr["COT16"] + ",COT17=" + dr["COT17"] 
                                        + ",ND_MA='" +BienBll.Ndma + "',ND_TEN=N'" + BienBll.NdTen+ "'"
                                        +" where MAPOS='" + pos+ "' and TT='"+dr["TT"]+ "' and THANG='" +thang + "' and NAM='"+nam+"' and MAU='2'";
                        cls.UpdateDataText(strsql);
                    }
                }
                else
                {
                    MessageBox.Show("Xem lại. Chưa có thay đổi nào!", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                string strup = "update LUUKTNB set COT05= (case when COT03>0 then COT04*100/COT03 else 0 end)" +
                               ",COT08=(case when COT06>0 then COT07*100/COT06 else 0 end) " +
                               ",COT11=(case when COT09>0 then COT10*100/COT09 else 0 end) " +
                               ",COT14=(case when COT12>0 then COT13*100/COT12 else 0 end) " +
                               ",COT17=(case when COT15>0 then COT16*100/COT15 else 0 end) " +
                               "where MAPOS='" + pos + "' and THANG='" + thang + "' and NAM='" + nam + "' and MAU='2'";
                cls.UpdateDataText(strup);
                if (Option1.IsChecked == true)
                        strin = "select * from LUUKTNB where MAPOS='" + pos + "' and THANG='" + thang +
                                     "' and NAM='" + nam + "' and MA='H' and MAU='2' order by TT";
                 else
                    strin = "select * from LUUKTNB where MAPOS='" + pos + "' and THANG='" + thang +
                     "' and NAM='" + nam + "' and MA='T' and MAU='2'  order by TT";

                var dtin =
                    cls.LoadDataText(strin);
                  rpt_KTNB01 rpt = new rpt_KTNB01();
                  RPUtility.ShowRp(rpt, dtin, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                cls.DongKetNoi();
            }
          
        }

   

   


        private void LblManual_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                dt = null;
                dgvData.ItemsSource = null;
                string ng= dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                string pos = s.Left(CboPos.SelectedValue.ToString(), 6);
                string thang = dtpNgay.SelectedDate.Value.ToString("MM");
                string nam = dtpNgay.SelectedDate.Value.ToString("yyyy");
                if (Option1.IsChecked == true) ma = "H";
                else ma = "T";
                cls.ClsConnect();
                string strsql = "select * from LUUKTNB where MAPOS='" + pos + "' and THANG='" + thang + "' and NAM='" + nam + "' and MA='"+ma+"' and MAU='2'";
                var dtchk = cls.LoadDataText(strsql);
                if (dtchk.Rows.Count == 0)
                {
                    string strins =
                        "insert into LUUKTNB select '"+ng+"' NGAY,'"+thang+"' THANG,'"+nam+"' NAM,'"+pos+"' MAPOS" +
                        ",(select PO_TEN from DMPOS where PO_MA='"+pos+"') TENPOS,a.*,N'"+BienBll.Ndma+"' ND_MA" +
                        ",(select nd_ten from NG_DUNG where ND_MA='"+ BienBll.Ndma + "') ND_TEN from MAUKTNB a where MA='"+ma+"' and MAU='2'";
                    cls.UpdateDataText(strins);
                }
                dt =
                    cls.LoadDataText("select * from LUUKTNB where MAPOS='" + pos + "' and THANG='" + thang +
                                     "' and NAM='" + nam + "' and MA='"+ma+"' and MAU='2' order by TT");
                dgvData.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Mess",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }

    
    }
}
