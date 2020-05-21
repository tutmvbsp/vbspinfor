using System;
//using Oracle.DataAccess.Client;
using System.Data.OracleClient;
using System.Data;
using System.Configuration;
using System.Windows.Forms;

namespace DAL
{
   public class ClsOracle
    {
        OracleConnection _connect = new OracleConnection(); // C#
        
        public bool KiemTraKetNoi()
       {
           bool ok = true;
           //string ketnoi = @"Data Source=(DESCRIPTION=" + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.31.0.55)(PORT=1521)))" + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=IMSREPORT)));" + "User Id=intellect;Password=intellect";
           string ketnoi = ConfigurationManager.ConnectionStrings["strora"].ConnectionString;
           _connect.ConnectionString = ketnoi;//ConfigurationManager.ConnectionStrings["strcon"].ConnectionString;
           if (_connect.State == ConnectionState.Closed)

               try
               {
                   _connect.Open();
               }
               catch //(Exception e)
               {
                   ok = false;
               }
               //finally
               //{
               //    _connect.Close();
               //    _connect.Dispose();
               //}
           return ok;
       }
       //
        //----------------------------
       public void ClsConnect()
        {
            if (_connect.State == ConnectionState.Closed)
            {
                // string connectstring = "Data Source=IMSREPORT;User Id=intellect;Password=intellect;";
                //const string connectstring = @"Data Source=(DESCRIPTION=" + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.31.0.55)(PORT=1521)))" + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=IMSREPORT)));" + "User Id=intellect;Password=intellect";
                //_connect.ConnectionString = connectstring; //ConfigurationManager.ConnectionStrings["strcon"].ConnectionString;                
                string ketnoi = ConfigurationManager.ConnectionStrings["strora"].ConnectionString;
                _connect.ConnectionString = ketnoi; //ConfigurationManager.ConnectionStrings["strcon"].ConnectionString;
                if (_connect.State == ConnectionState.Closed)

                    try
                    {
                        _connect.Open();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lổi Open Oracle"+ex.Message);
                    }
                    //finally
                    //{
                    //    _connect.Close();
                    //    _connect.Dispose();
                    //}
            }

        }


        // --------------------------
        public void DongKetNoi()
        {
            if (_connect.State == ConnectionState.Open)
            {
                _connect.Close();
               // _connect.Dispose();
            }
        }
        // Load Procedure khong co  tham so
        public DataTable LoadDataProc(string sql)
        {
            OracleCommand command = new OracleCommand(sql, _connect);
            command.CommandType = CommandType.StoredProcedure;
            OracleDataAdapter adapter = new OracleDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
        //Load text khong co tham so
        public DataTable LoadDataText(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                OracleCommand command = new OracleCommand(sql, _connect);
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 3600;
                OracleDataAdapter adapter = new OracleDataAdapter(command);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
            return dt;
        }
        //---
        //Load text co tham so
        public DataTable LoadDataTextPara(string sql, string[] name, object[] value, int nparameter)
        {
            OracleCommand command = new OracleCommand(sql, _connect);
            command.CommandType = CommandType.Text;
            //command.CommandTimeout = 240;
            for (int i = 0; i < nparameter; i++)
            {
                // command.Parameters.AddWithValue(name[i], value[i]);
                command.Parameters.Add(name[i], value[i]);
            }
            OracleDataAdapter adapter = new OracleDataAdapter(command);
            //DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            //adapter.Fill(ds, tablename);
            //dt = ds.Tables[tablename];
            adapter.Fill(dt);
            return dt;
        }
        //Load Procedure co tham so
        //public DataTable LoadDataProcPara(string sql, string[] name, object[] value, int nparameter)
        //{
        //    OracleCommand command = new OracleCommand(sql, _connect);
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.CommandTimeout = 150;

        //    for (int i = 0; i < nparameter; i++)
        //    {
        //        command.Parameters.Add(name[i], value[i]);
        //    }
        //    OracleDataAdapter adapter = new OracleDataAdapter(command);
        //    DataTable dt = new DataTable();
        //    adapter.Fill(dt);
        //    return dt;
        //}
        public DataTable LoadDataProcPara(string orl, string[] name, object[] value, int nparameter)
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["strora"].ConnectionString;
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            OracleCommand command = new OracleCommand(orl, conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("csrData", OracleType.Cursor).Direction = ParameterDirection.Output;
            command.CommandTimeout = 150;
            for (int i = 0; i < nparameter; i++)
            {
                command.Parameters.AddWithValue(name[i], value[i]);
            }
            OracleDataAdapter adapter = new OracleDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
            // return dt;

            //SqlCommand command = new SqlCommand(sql, conn);
            //command.CommandType = CommandType.StoredProcedure;
            //command.CommandTimeout = 150;
            //for (int i = 0; i < nparameter; i++)
            //{
            //    command.Parameters.AddWithValue(name[i], value[i]);
            //}
            //SqlDataAdapter adapter = new SqlDataAdapter(command);
            ////DataSet ds = new DataSet();
            //DataTable dt = new DataTable();
            ////adapter.Fill(ds, tablename);
            ////dt = ds.Tables[tablename];
            //adapter.Fill(dt);
            //return dt;
        }

        public int UpdateData(string sql)
        {
            OracleCommand command = new OracleCommand(sql, _connect);
            command.CommandType = CommandType.StoredProcedure;
            return command.ExecuteNonQuery();
        }
        public int UpdateData(string sql, string[] name, object[] value, int nparameter)
        {
            OracleCommand command = new OracleCommand(sql, _connect);
            command.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < nparameter; i++)
                {
                    command.Parameters.Add(name[i], value[i]);
                }
                return command.ExecuteNonQuery();
            }
        } 
    
}
