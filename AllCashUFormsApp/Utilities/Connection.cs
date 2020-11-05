using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace AllCashUFormsApp
{
    public static class Connection
    {
        public static string ConnectionString { get; set; }

        public static string GetConnectionStrings()
        {
            string ServerName = ConfigurationManager.AppSettings["ServerName"];
            string DatabaseName = ConfigurationManager.AppSettings["DatabaseName"];
            string UserName = ConfigurationManager.AppSettings["UserName"];
            string Password = ConfigurationManager.AppSettings["Password"];
            string Timeout = ConfigurationManager.AppSettings["Timeout"];
            string LoginName = ConfigurationManager.AppSettings["LoginName"];

            string _connectionString =
           "Data Source=" + ServerName + ";" +
           "Initial Catalog=" + DatabaseName + ";" +
           "User id=" + UserName + ";" +
           "Password=" + Password + ";" +
           "Connection Timeout=" + Timeout + ";";

            ConnectionString = _connectionString;

            return ConnectionString;
        }


        public static void GetConnectionStringsManual()
        {
            var conStr = Helper.ConnectionString;

            string ServerName = conStr.Split('=')[4].Split(';')[0];
            string DatabaseName = conStr.Split('=')[5].Split(';')[0];
            string UserName = conStr.Split('=')[6].Split(';')[0];
            string Password = conStr.Split('=')[7].Split(';')[0];

            string _connectionString =
           "Data Source=" + ServerName + ";" +
           "Initial Catalog=" + DatabaseName + ";" +
           "User id=" + UserName + ";" +
           "Password=" + Password + ";" +
           "Connection Timeout=0;";

            ConnectionString = _connectionString;
        }
    }
}
