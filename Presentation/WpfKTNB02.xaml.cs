using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfLaiTon.xaml
    /// </summary>
    public partial class WpfKTNB02
    {
        public WpfKTNB02()
        {
            InitializeComponent();
        }

        ToolBll s = new ToolBll();
        private readonly ClsServer cls = new ClsServer();
        private DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        ServerInfor srv = new ServerInfor();
        private string ma = "";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtpNgay.SelectedDate = DateTime.Now;
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
                string pos = BienBll.NdMadv;
                string thang = dtpNgay.SelectedDate.Value.ToString("MM");
                string nam = dtpNgay.SelectedDate.Value.ToString("yyyy");
                dtNew = dt.GetChanges();
                if (dtNew != null && dtNew.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtNew.Rows)
                    {
                        //MessageBox.Show(dr["NG_MATO"].ToString()+"      "+dr["A01"].ToString());
                        string strsql = "update LUUKTNB set COT03=" +dr["COT03"] + ",COT04=" + dr["COT04"] 
                                        + ",COT06=" + dr["COT06"] + ",COT07=" + dr["COT07"]
                                        + ",ND_MA='" +BienBll.Ndma + "',ND_TEN=N'" + BienBll.NdTen+ "'"
                                        +" where MAPOS='" + pos+ "' and TT='"+dr["TT"]+ "' and THANG='" +thang + "' and NAM='"+nam+"' and MAU='1'";
                        cls.UpdateDataText(strsql);
                    }
                    MessageBox.Show("Update Ok", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Xem lại. Chưa có thay đổi nào!", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                string strup = "update LUUKTNB set COT05= (case when COT03>0 then COT04*100/COT03 else 0 end),COT08=(case when COT06>0 then COT07*100/COT06 else 0 end) where MAPOS='" + pos + "' and THANG='" + thang + "' and NAM='" + nam + "' and MAU='1'";
                cls.UpdateDataText(strup);
                var dtin =
                    cls.LoadDataText("select * from LUUKTNB where MAPOS='" + pos + "' and THANG='" + thang +
                                     "' and NAM='" + nam + "' and MAU='1' order by TT");
                rpt_KTNB02 rpt = new rpt_KTNB02();
                RPUtility.ShowRp(rpt, dtin, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());

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
            dt = null;
            dgvData.ItemsSource = null;
            string ng= dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
            string pos = BienBll.NdMadv;
            string thang = dtpNgay.SelectedDate.Value.ToString("MM");
            string nam = dtpNgay.SelectedDate.Value.ToString("yyyy");
            try
            {
                cls.ClsConnect();
                string strsql = "select * from LUUKTNB where MAPOS='" + pos + "' and THANG='" + thang + "' and NAM='" + nam + "' and MAU='1'";
                var dtchk = cls.LoadDataText(strsql);
                if (dtchk.Rows.Count == 0)
                {
                    string strins =
                        "insert into LUUKTNB select '"+ng+"' NGAY,'"+thang+"' THANG,'"+nam+"' NAM,'"+pos+"' MAPOS" +
                        ",(select PO_TEN from DMPOS where PO_MA='"+pos+"') TENPOS,a.*,N'"+BienBll.Ndma+"' ND_MA" +
                        ",(select nd_ten from NG_DUNG where ND_MA='"+ BienBll.Ndma + "') ND_TEN from MAUKTNB a where MAU='1'";
                    cls.UpdateDataText(strins);
                }
                dt =
                    cls.LoadDataText("select * from LUUKTNB where MAPOS='" + pos + "' and THANG='" + thang +
                                     "' and NAM='" + nam + "' and MAU='1' order by TT");
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
