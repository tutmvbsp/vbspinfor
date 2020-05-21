using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
namespace DAL
{
    public class ClsExcel
    {
        //SqlConnection connect = new SqlConnection();
        OleDbConnection connect = new OleDbConnection();
        private string str = "strExc";
        //
        public void ClsConnect()
        {
            if (connect.State == ConnectionState.Closed)
            {
                string ketnoi = ConfigurationManager.ConnectionStrings[str].ConnectionString;
                connect.ConnectionString = ketnoi; //ConfigurationManager.ConnectionStrings["strcon"].ConnectionString;
                if (connect.State == ConnectionState.Closed)

                    try
                    {
                        connect.Open();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                //finally
                //{
                //    connect.Close();
                //    connect.Dispose();
                //}
            }

        }



        // --------------------------
        public void DongKetNoi()
        {
            if (connect.State == ConnectionState.Open)
            {
                connect.Close();
                connect.Dispose();
            }
        }
        //Load text khong co tham so
        public DataTable LoadDataText(string sql)
        {
            OleDbCommand command = new OleDbCommand(sql, connect);
            command.CommandType = CommandType.Text;
            OleDbDataAdapter  adapter = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

    }
}
