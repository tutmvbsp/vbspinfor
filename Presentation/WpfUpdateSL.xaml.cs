using System;
using System.Collections;
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
using System.ComponentModel;
using System.IO;
using System.Data;
using DAL;
using BLL;


namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfImportText.xaml
    /// </summary>
    public partial class WpfUpdateSL : Window
    {
        public WpfUpdateSL()
        {
            InitializeComponent();
            backgroundWorker = (BackgroundWorker)FindResource("backgroundWorker");
        }
        private BackgroundWorker backgroundWorker;
        private FileStream _fw;
        private ToolBll bll = new ToolBll();
        private DataTable dt = new DataTable();
        private DataTable dtku = new DataTable();
        private DataTable dttk = new DataTable();
        private ClsServer cls = new ClsServer();
        //string Thumuc = "C:\\TEXT";
        private string strup = "";
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            try
            {
                DataTable dt_chk = new DataTable();
                dt_chk =
                    cls.LoadDataText("select * from U_HSTD where NGAYBT= '" +dtpNgayBt.SelectedDate.Value.ToString("dd/MM/yyyy") + "'");
                if (dt_chk.Rows.Count > 0)
                {
                    MessageBox.Show("Kiểm tra lại số liệu, đã thực hiện trước đó ngày : " + dtpNgayBt.SelectedDate.Value.ToString("dd/MM/yyyy"));
                }
                else 
                {
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@Ngaybt";
                    giatri[0] = dtpNgayBt.SelectedDate.Value.ToString("dd/MM/yyyy");
                    bien[1] = "@Ngayku";
                    giatri[1] = dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy");
                    //cls.UpdateDataProcPara("usp_UpdateData", bien, giatri, thamso);
                    cls.UpdateLdbf("usp_UpdateData", bien, giatri, thamso);
                    string sqlcd = "insert into U_HSTD (NGAYBT)  values ('" +dtpNgayBt.SelectedDate.Value.ToString("dd/MM/yyyy") + "')";
                    cls.UpdateDataText(sqlcd);
                    MessageBox.Show("OK","Mess",MessageBoxButton.OK,MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();
        }

        private void WriteText(String fileName)
        {
            System.Text.Encoding encode = System.Text.Encoding.BigEndianUnicode;
            _fw = new System.IO.FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            StreamWriter sw = new StreamWriter(_fw, encode);
            foreach (DataRow row in dt.Rows)
            {
                //foreach (DataColumn col in dt.Columns)
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i + 1 < dt.Columns.Count)
                    {
                        //sw.Write(row[col].ToString() + "#");
                        sw.Write(row[i].ToString() + "#");
                    }
                    else
                    {
                        sw.Write(row[i].ToString());
                    }
                }
                sw.WriteLine();
            }
            sw.Close();
        }

        private void InsertText(String PathDir)
        {
            try
            {

                int thamso = 1;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@PathDir";
                giatri[0] = PathDir;
                if (File.Exists(giatri[0].ToString().Trim()))
                {
                    cls.UpdateDataProcPara("usp_InsertTextUpHstd", bien, giatri, thamso);
                    File.Delete(giatri[0].ToString().Trim());
                    MessageBox.Show("Insert OK");
                }
                else
                {
                    MessageBox.Show(" Chưa có file : " + giatri[1].ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                DataTable dtchk = new DataTable();
                string sql = "select * from U_HSTD where NGAYKU=NGAYBT";
                cls.ClsConnect();
                dtchk = cls.LoadDataText(sql);
                if (dtchk.Rows.Count > 0)
                {
                    MessageBox.Show("Kiểm tra lại, số liệu có thể đã được cập nhật");
                }
                else
                {
                    int thamso = 2;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@NgayKu";
                    giatri[0] = dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy");
                    bien[1] = "@NgayBt";
                    giatri[1] = dtpNgayBt.SelectedDate.Value.ToString("dd/MM/yyyy");
                    cls.UpdateDataProcPara("usp_UpdateHsku", bien, giatri, thamso);
                    string sqlup = "insert into U_HSTD (NGAYKU)  values ('" + giatri[1] + "')";
                    cls.UpdateDataText(sqlup);
                    MessageBox.Show("Update OK");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            DataTable  dtver =  new DataTable();
            dtver = cls.LoadDataText("select MAX(CONVERT(date,NGAYKU,105)) as NGKUMAX,MAX(CONVERT(date,NGAYBT,105)) as NGBTMAX from U_HSTD");
            dtpNgayKu.SelectedDate = Convert.ToDateTime(dtver.Rows[0]["NGKUMAX"]);//DateTime.Now.AddDays(-3);
            dtpNgayBt.SelectedDate = DateTime.Now.AddDays(-1); //Convert.ToDateTime(dtver.Rows[0]["NGBTMAX"]); //
            cls.DongKetNoi();
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                dtku = cls.LoadDataText("select * from UPDATEHSTD where MA='KU' and left(ngay,10)='" + dtpNgayBt.SelectedDate.Value.ToString("dd/MM/yyyy")+"'");
                dgvKu.ItemsSource = dtku.DefaultView;
                dttk = cls.LoadDataText("select * from UPDATEHSTD where MA='TK' and left(ngay,10)='" + dtpNgayBt.SelectedDate.Value.ToString("dd/MM/yyyy") + "'");
                dgvTk.ItemsSource = dttk.DefaultView;
                lblPerCent.Content = dtku.Rows.Count.ToString();
                    #region update hsku
                    if (dtku.Rows.Count>0)
                    {
                        foreach (DataRow dr in dtku.Rows)
                        {
                            // MessageBox.Show(dr[1] + "  " + dr[2] + "  " + dr[4] + "  " + dr[5]);
                            lblLoi.Content = dtku.Rows;
                            string ma = dr[0].ToString().Trim();
                            string soku = dr[1].ToString().Trim();
                            string mnv = dr[2].ToString().Trim();
                            int sotien = Convert.ToInt32(dr[4]);
                            #region
                            if (mnv == "11") //Giai ngan
                            {
                                strup = "update a set a.KU_GNGAN=a.KU_GNGAN+" + sotien + "" +
                                        ",a.KU_A_GNGAN=a.KU_A_GNGAN+" + sotien + "" +
                                        ",a.KU_M_GNGAN=a.KU_M_GNGAN+" + sotien + "" +
                                        ",a.KU_DNOTHAN=a.KU_DNOTHAN+" + sotien + "" +
                                        " from HSKU a where a.KU_SOKU='" + soku +
                                        "' and LEFT(a.KU_NGAYBC,10)='" +
                                        dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy") + "'";
                                //MessageBox.Show(soku);
                                //MessageBox.Show(mnv);
                                cls.UpdateDataText(strup);
                                //MessageBox.Show(strup);
                            }
                            #endregion
                            #region
                            if (mnv == "12") //Thu no trong han
                            {
                                strup = "update a set a.KU_TNTH=a.KU_TNTH+" + sotien + "" +
                                        ",a.KU_A_TNTHAN=a.KU_A_TNTHAN+" + sotien + "" +
                                        ",a.KU_M_TNTHAN=a.KU_M_TNTHAN+" + sotien + "" +
                                        ",a.KU_DNOTHAN=a.KU_DNOTHAN-" + sotien + "" +
                                        " from HSKU a where a.KU_SOKU='" + soku +
                                        "' and LEFT(a.KU_NGAYBC,10)='" +
                                        dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy") + "'";
                                cls.UpdateDataText(strup);
                            }
                            #endregion
                            #region
                            if (mnv == "13") //Thu no qua han
                            {
                                strup = "update a set a.KU_TNQH=a.KU_TNQH+" + sotien + "" +
                                        ",a.KU_A_TNQHAN=a.KU_A_TNQHAN+" + sotien + "" +
                                        ",a.KU_M_TNQHAN=a.KU_M_TNQHAN+" + sotien + "" +
                                        ",a.KU_DNOQHAN=a.KU_DNOQHAN-" + sotien + "" +
                                        " from HSKU a where a.KU_SOKU='" + soku +
                                        "' and LEFT(a.KU_NGAYBC,10)='" +
                                        dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy") + "'";
                                cls.UpdateDataText(strup);
                            }
                            #endregion
                            #region
                            if (mnv == "14") //Chuyen qua han
                            {
                                strup = "update a set a.KU_CHUYENQH=a.KU_CHUYENQH+" + sotien + "" +
                                        ",a.KU_A_CHUYENQH=a.KU_A_CHUYENQH+" + sotien + "" +
                                        ",a.KU_M_CHUYENQH=a.KU_M_CHUYENQH+" + sotien + "" +
                                        " from HSKU a where a.KU_SOKU='" + soku +
                                        "' and LEFT(a.KU_NGAYBC,10)='" +
                                        dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy") + "'";
                                cls.UpdateDataText(strup);
                            }
                            #endregion
                            #region
                            if (mnv == "15") //Gia han no
                            {
                                strup = "update a set a.KU_GHANNO=a.KU_GHANNO+" + sotien + "" +
                                        ",a.KU_A_GHANNO=a.KU_A_GHANNO+" + sotien + "" +
                                        ",a.KU_M_GHANNO=a.KU_M_GHANNO+" + sotien + "" +
                                        " from HSKU a where a.KU_SOKU='" + soku +
                                        "' and LEFT(a.KU_NGAYBC,10)='" +
                                        dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy") + "'";
                                cls.UpdateDataText(strup);
                            }
                            #endregion
                            #region
                            if (mnv == "16") //Thu no khoanh
                            {
                                strup = "update a set a.KU_TNKH=a.KU_TNKH+" + sotien + "" +
                                        ",a.KU_A_TNKHOANH=a.KU_A_TNKHOANH+" + sotien + "" +
                                        ",a.KU_M_TNKHOANH=a.KU_M_TNKHOANH+" + sotien + "" +
                                        ",a.KU_DNOKHOANH=a.KU_DNOKHOANH-" + sotien + "" +
                                        " from HSKU a where a.KU_SOKU='" + soku +
                                        "' and LEFT(a.KU_NGAYBC,10)='" +
                                        dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy") + "'";
                                cls.UpdateDataText(strup);
                            }
                            #endregion
                            cls.UpdateDataText("update hsku set ku_ngaybc = '" +
                                               dtpNgayBt.SelectedDate.Value.ToString("dd/MM/yyyy") + "' where left(KU_NGAYBC,10)='" + dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy")+"'");
                            string sqlcd = "insert into U_HSTD (NGAYKU)  values ('" + dtpNgayBt.SelectedDate.Value.ToString("dd/MM/yyyy") + "')";
                            cls.UpdateDataText(sqlcd);

                        }
                       
                    }
                    else
                    {
                        MessageBox.Show("Không có KU nào");
                    }

                    #endregion  //update KU
                    #region

                    if (dttk.Rows.Count > 0)
                    {
                        #region foreach
                        foreach (DataRow dr in dttk.Rows)
                        {
                            string ma = dr[0].ToString().Trim();
                            string soku = dr[1].ToString().Trim();
                            string mnv = dr[2].ToString().Trim();
                            int sotien = Convert.ToInt32(dr[4]);

                            #region mnv
                            if (mnv == "17") //Gui TK
                            {
                                strup = "update a set " +
                                        "a.CS_A_GUITK=a.CS_A_GUITK+" + sotien + "" +
                                        ",a.CS_M_GUITK=a.CS_M_GUITK+" + sotien + "" +
                                        ",a.CS_SODU_TK=a.CS_SODU_TK+" + sotien + "" +
                                        " from CASA a where a.CS_SO_TK='" + soku +
                                        "' and LEFT(a.CS_NGAYBC,10)='" +
                                        dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy") + "'";
                                cls.UpdateDataText(strup);

                            }
                            else
                            {
                                strup = "update a set " +
                                        "a.CS_A_RUTTK=a.CS_A_RUTTK+" + sotien + "" +
                                        ",a.CS_M_RUTTK=a.CS_M_RUTTK+" + sotien + "" +
                                        ",a.CS_SODU_TK=a.CS_SODU_TK-" + sotien + "" +
                                        " from CASA a where a.CS_SO_TK='" + soku +
                                        "' and LEFT(a.CS_NGAYBC,10)='" +
                                        dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy") + "'";
                                cls.UpdateDataText(strup);

                            }

                            #endregion
                        }
                        cls.UpdateDataText("update CASA set CS_NGAYBC = '" +
                   dtpNgayBt.SelectedDate.Value.ToString("dd/MM/yyyy") + "' where left(CS_NGAYBC,10)='" + dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy") + "'");

                        #endregion
                    }
                    else
                    {
                        MessageBox.Show("Không có TK CASA nào");
                    }
                    #endregion

                    MessageBox.Show("Update HSKU OK");
                cls.DongKetNoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnProg_Click(object sender, RoutedEventArgs e)
        {
            backgroundWorker.WorkerReportsProgress = true;
            if (!backgroundWorker.IsBusy)
                backgroundWorker.RunWorkerAsync();

        }
        private void DoSlowProcess()
        {

            int iterations = 0;
            //-------------------------------------------------
            this.Dispatcher.Invoke((Action)(() =>
            {
                iterations = dtku.Rows.Count;
            }));

            //-----------------------------
            for (int i = 0; i <= iterations - 1; i++)
            {
                int percentComplete = (int)((float)i / (float)(iterations - 1) * 100);
                string updateMessage = string.Format("Iteration {0} of {1}", i, iterations - 1);
                backgroundWorker.ReportProgress(percentComplete, updateMessage);
                Dispatcher.Invoke(new System.Action(() => lblPerCent.Content = percentComplete.ToString() + "%"));
                Update(i);

            }

        }
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                lblLoi.Content = e.Error.Message;
                System.Windows.Forms.MessageBox.Show(e.Error.StackTrace);
            }
            else if (e.Cancelled)
            {
                lblLoi.Content = "Cancelled";
            }
            else
            {
                Dispatcher.Invoke(new System.Action(() => lblLoi.Content = "Đã hoàn thành "));
                //bt.IsEnabled = true;
                //  progressBar1.Value=0;

            }
        }
        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)

        {
            progressBar1.Value = e.ProgressPercentage;
            lblPerCent.Content = (string)e.UserState;
        }
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var bgw = sender as BackgroundWorker;
            DoSlowProcess();
        }       
        private void Update(int SoReCord)
        {
            try
            {
                cls.ClsConnect();
                dtku = cls.LoadDataText("select * from UPDATEHSTD where MA='KU' and left(ngay,10)='" + dtpNgayBt.SelectedDate.Value.ToString("dd/MM/yyyy") + "'");
                dgvKu.ItemsSource = dtku.DefaultView;
                dttk = cls.LoadDataText("select * from UPDATEHSTD where MA='TK' and left(ngay,10)='" + dtpNgayBt.SelectedDate.Value.ToString("dd/MM/yyyy") + "'");
                dgvTk.ItemsSource = dttk.DefaultView;
                
                
                    #region update hsku

                    SoReCord = dtku.Rows.Count;
                    if (SoReCord > 0)
                    {
                        foreach (DataRow dr in dtku.Rows)
                        {
                            // MessageBox.Show(dr[1] + "  " + dr[2] + "  " + dr[4] + "  " + dr[5]);
                            string ma = dr[0].ToString().Trim();
                            string soku = dr[1].ToString().Trim();
                            string mnv = dr[2].ToString().Trim();
                            int sotien = Convert.ToInt32(dr[4]);
                            #region
                            if (mnv == "11") //Giai ngan
                            {
                                strup = "update a set a.KU_GNGAN=a.KU_GNGAN+" + sotien + "" +
                                        ",a.KU_A_GNGAN=a.KU_A_GNGAN+" + sotien + "" +
                                        ",a.KU_M_GNGAN=a.KU_M_GNGAN+" + sotien + "" +
                                        ",a.KU_DNOTHAN=a.KU_DNOTHAN+" + sotien + "" +
                                        " from HSKU a where a.KU_SOKU='" + soku +
                                        "' and LEFT(a.KU_NGAYBC,10)='" +
                                        dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy") + "'";
                                //MessageBox.Show(soku);
                                //MessageBox.Show(mnv);
                                cls.UpdateDataText(strup);
                                //MessageBox.Show(strup);
                            }
                            #endregion
                            #region
                            if (mnv == "12") //Thu no trong han
                            {
                                strup = "update a set a.KU_TNTH=a.KU_TNTH+" + sotien + "" +
                                        ",a.KU_A_TNTHAN=a.KU_A_TNTHAN+" + sotien + "" +
                                        ",a.KU_M_TNTHAN=a.KU_M_TNTHAN+" + sotien + "" +
                                        ",a.KU_DNOTHAN=a.KU_DNOTHAN-" + sotien + "" +
                                        " from HSKU a where a.KU_SOKU='" + soku +
                                        "' and LEFT(a.KU_NGAYBC,10)='" +
                                        dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy") + "'";
                                cls.UpdateDataText(strup);
                            }
                            #endregion
                            #region
                            if (mnv == "13") //Thu no qua han
                            {
                                strup = "update a set a.KU_TNQH=a.KU_TNQH+" + sotien + "" +
                                        ",a.KU_A_TNQHAN=a.KU_A_TNQHAN+" + sotien + "" +
                                        ",a.KU_M_TNQHAN=a.KU_M_TNQHAN+" + sotien + "" +
                                        ",a.KU_DNOQHAN=a.KU_DNOQHAN-" + sotien + "" +
                                        " from HSKU a where a.KU_SOKU='" + soku +
                                        "' and LEFT(a.KU_NGAYBC,10)='" +
                                        dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy") + "'";
                                cls.UpdateDataText(strup);
                            }
                            #endregion
                            #region
                            if (mnv == "14") //Chuyen qua han
                            {
                                strup = "update a set a.KU_CHUYENQH=a.KU_CHUYENQH+" + sotien + "" +
                                        ",a.KU_A_CHUYENQH=a.KU_A_CHUYENQH+" + sotien + "" +
                                        ",a.KU_M_CHUYENQH=a.KU_M_CHUYENQH+" + sotien + "" +
                                        " from HSKU a where a.KU_SOKU='" + soku +
                                        "' and LEFT(a.KU_NGAYBC,10)='" +
                                        dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy") + "'";
                                cls.UpdateDataText(strup);
                            }
                            #endregion
                            #region
                            if (mnv == "15") //Gia han no
                            {
                                strup = "update a set a.KU_GHANNO=a.KU_GHANNO+" + sotien + "" +
                                        ",a.KU_A_GHANNO=a.KU_A_GHANNO+" + sotien + "" +
                                        ",a.KU_M_GHANNO=a.KU_M_GHANNO+" + sotien + "" +
                                        " from HSKU a where a.KU_SOKU='" + soku +
                                        "' and LEFT(a.KU_NGAYBC,10)='" +
                                        dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy") + "'";
                                cls.UpdateDataText(strup);
                            }
                            #endregion
                            #region
                            if (mnv == "16") //Thu no khoanh
                            {
                                strup = "update a set a.KU_TNKH=a.KU_TNKH+" + sotien + "" +
                                        ",a.KU_A_TNKHOANH=a.KU_A_TNKHOANH+" + sotien + "" +
                                        ",a.KU_M_TNKHOANH=a.KU_M_TNKHOANH+" + sotien + "" +
                                        ",a.KU_DNOKHOANH=a.KU_DNOKHOANH-" + sotien + "" +
                                        " from HSKU a where a.KU_SOKU='" + soku +
                                        "' and LEFT(a.KU_NGAYBC,10)='" +
                                        dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy") + "'";
                                cls.UpdateDataText(strup);
                            }
                            #endregion
                            cls.UpdateDataText("update hsku set ku_ngaybc = '" +
                                               dtpNgayBt.SelectedDate.Value.ToString("dd/MM/yyyy") + "' where left(KU_NGAYBC,10)='" + dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy") + "'");
                        }
                        MessageBox.Show("Update HSKU OK");
                    }
                    else
                    {
                        MessageBox.Show("Không có KU nào");
                    }

                    #endregion  //update KU
                
                    #region
                    SoReCord = dttk.Rows.Count;
                    if (SoReCord > 0)
                    {
                        #region foreach
                        foreach (DataRow dr in dttk.Rows)
                        {
                            string ma = dr[0].ToString().Trim();
                            string soku = dr[1].ToString().Trim();
                            string mnv = dr[2].ToString().Trim();
                            int sotien = Convert.ToInt32(dr[4]);

                            #region mnv
                            if (mnv == "17") //Gui TK
                            {
                                strup = "update a set " +
                                        "a.CS_A_GUITK=a.CS_A_GUITK+" + sotien + "" +
                                        ",a.CS_M_GUITK=a.CS_M_GUITK+" + sotien + "" +
                                        ",a.CS_SODU_TK=a.CS_SODU_TK+" + sotien + "" +
                                        " from CASA a where a.CS_SO_TK='" + soku +
                                        "' and LEFT(a.CS_NGAYBC,10)='" +
                                        dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy") + "'";
                                cls.UpdateDataText(strup);

                            }
                            else
                            {
                                strup = "update a set " +
                                        "a.CS_A_RUTTK=a.CS_A_RUTTK+" + sotien + "" +
                                        ",a.CS_M_RUTTK=a.CS_M_RUTTK+" + sotien + "" +
                                        ",a.CS_SODU_TK=a.CS_SODU_TK-" + sotien + "" +
                                        " from CASA a where a.CS_SO_TK='" + soku +
                                        "' and LEFT(a.CS_NGAYBC,10)='" +
                                        dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy") + "'";
                                cls.UpdateDataText(strup);

                            }

                            #endregion
                        }
                        cls.UpdateDataText("update CASA set CS_NGAYBC = '" +
                   dtpNgayBt.SelectedDate.Value.ToString("dd/MM/yyyy") + "' where left(CS_NGAYBC,10)='" + dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy") + "'");

                        #endregion
                    }
                    else
                    {
                        MessageBox.Show("Không có TK CASA nào");
                    }
                    #endregion
               

                cls.DongKetNoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (dtpNgayBt.SelectedDate.Value.ToString("dd/MM/yyyy") ==
                dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy"))
            {
                MessageBox.Show("Ngày bút toán không được nhỏ hơn hoặc bằng ngày khế ước", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);                    
            } else
            {
                #region

                cls.ClsConnect();
                try
                {
                    DataTable dt_chk = new DataTable();
                    dt_chk =
                        cls.LoadDataText("select * from U_HSTD where NGAYBT= '" +
                                         dtpNgayBt.SelectedDate.Value.ToString("dd/MM/yyyy") + "'");
                    if (dt_chk.Rows.Count > 0)
                    {
                        MessageBox.Show("Kiểm tra lại số liệu, đã thực hiện trước đó ngày : " +
                                        dtpNgayBt.SelectedDate.Value.ToString("dd/MM/yyyy"));
                    }
                    else
                    {
                        int thamso = 2;
                        string[] bien = new string[thamso];
                        object[] giatri = new object[thamso];
                        bien[0] = "@Ngaygd";
                        giatri[0] = dtpNgayBt.SelectedDate.Value.ToString("dd/MM/yyyy");
                        bien[1] = "@Ngayku";
                        giatri[1] = dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy");
                        //cls.UpdateDataProcPara("usp_UpdateData", bien, giatri, thamso);
                        cls.UpdateLdbf("usp_UpdateData", bien, giatri, thamso);
                        string sqlcd = "insert into U_HSTD (NGAYBT)  values ('" +
                                       dtpNgayBt.SelectedDate.Value.ToString("dd/MM/yyyy") + "')";
                        cls.UpdateDataText(sqlcd);
                        MessageBox.Show("OK", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                cls.DongKetNoi();

                #endregion
            }
        }
    }
}