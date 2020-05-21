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
    /// Interaction logic for WpfPstk.xaml
    /// </summary>
    public partial class WpfPstk : Window
    {
        public WpfPstk()
        {
            InitializeComponent();
        }
        ToolBll str = new ToolBll();
        ServerInfor srv = new ServerInfor();
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string tungay = dtpTuNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
            string denngay = dtpDenNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
            ClsOracle clsora = new ClsOracle();
            DataTable dt = new DataTable();
            clsora.ClsConnect();
            string tk = str.Left(cboTk.SelectedValue.ToString().Trim(),10);
            //MessageBox.Show(tk);

            string sql = "select to_char(a.ngaygd,'dd/MM/yyyy') as NGGD,c.AC_DESC as TENTK,a.*,b.* from hsbt a, DMPOS b,DMTKGL c where a.ngaybc >= " + "to_date(" + "'" + tungay + "'" + "," + "'dd/mm/yyyy" + "')" + " and a.ngaybc <= " + "to_date(" + "'" + denngay + "'" + "," + "'dd/mm/yyyy" + "') and a.MAPGD= " + "'" + str.Left(cboPos.SelectedValue.ToString().Trim(), 6) + "'" + " and a.TK= " + "'" + tk + "'" + " and a.MAPGD=b.PO_MA and a.TK=c.BANK_AC order by a.NGAYGD "; 
            //MessageBox.Show(sql);
            dt = clsora.LoadDataText(sql);
            //dataGrid1.ItemsSource = dt.DefaultView;
            if (dt.Rows.Count > 0)
            {
                rpt_Pstk rpt = new rpt_Pstk();
                RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
            }
            else
            {
                MessageBox.Show("Không có bản ghi nào ", "Thông báo");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtpTuNgay.SelectedDate = DateTime.Parse("01/01/" + DateTime.Now.ToString("yyyy"));
            dtpDenNgay.SelectedDate = DateTime.Now.Date;
            ClsServer cls = new ClsServer();
            cls.ClsConnect();
            /*
            DataTable dtgl = new DataTable();
            dtgl = cls.LoadDataText("select BANK_AC as TKGL,TK_CAP5 as TKSBV ,AC_DESC as TENTK from dmtkgl where left(BANK_AC,2) in ('92','94','97','98','93','99') order by BANK_AC");
            for (int i = 0; i<dtgl.Rows.Count; i++)
            {
                cboTk.Items.Add(dtgl.Rows[i][0] + "|" + dtgl.Rows[i][1] + "|" + dtgl.Rows[i][2]);
            }
            */
            DataTable dtglc3 = new DataTable();
            dtglc3 = cls.LoadDataText("select BANK_AC as TKGL,TK_CAP5 as TKSBV ,AC_DESC as TENTK from dmtkgl where len(BANK_AC)=4 order by BANK_AC");
            for (int i = 0; i < dtglc3.Rows.Count; i++)
            {
                cboTkC3.Items.Add(dtglc3.Rows[i][0] + "|" + dtglc3.Rows[i][1] + "|" + dtglc3.Rows[i][2]);
            }

            DataTable dtpos = new DataTable();
            cls.ClsConnect();
            string sql = "select PO_MA,PO_TEN from DMPOS where PO_MACN=" + "'" + BienBll.MainPos + "'" + " order by PO_MA";
            dtpos = cls.LoadDataText(sql);
            for (int i = 0; i < dtpos.Rows.Count; i++)
            {
                cboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
            }
            cboPos.SelectedIndex = 7;
            // cls.DongKetNoi();
        }

        private void cboTkC3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cboTk.Items.Clear();
            ClsServer cls = new ClsServer();
            cls.ClsConnect();
            DataTable dtgl = new DataTable();
            dtgl = cls.LoadDataText("select BANK_AC as TKGL,TK_CAP5 as TKSBV ,AC_DESC as TENTK from dmtkgl where left(BANK_AC,4) ='" + str.Left(cboTkC3.SelectedValue.ToString().Trim(), 4) + "' and len(BANK_AC)=10 order by BANK_AC");
            for (int i = 0; i < dtgl.Rows.Count; i++)
            {
                cboTk.Items.Add(dtgl.Rows[i][0] + "|" + dtgl.Rows[i][1] + "|" + dtgl.Rows[i][2]);
            }
            cls.DongKetNoi();
        }
    }
}
