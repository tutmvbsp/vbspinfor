using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
namespace DAL
{
    public class ServerInfor
    {
        string ketnoi = ConfigurationManager.ConnectionStrings["strsrv"].ConnectionString;        
        public string DbNameSerVer()
        {
            var builder = new SqlConnectionStringBuilder(ketnoi);
            return builder.InitialCatalog;
        }

        public string DbSourceSerVer()
        {
            var builder = new SqlConnectionStringBuilder(ketnoi);
            return builder.DataSource;
        }
        public string DbUserSerVer()
        {
            var builder = new SqlConnectionStringBuilder(ketnoi);
            return builder.UserID;
        }
        public string DbPassSerVer()
        {
            var builder = new SqlConnectionStringBuilder(ketnoi);
            return builder.Password;
        }
    }
}
