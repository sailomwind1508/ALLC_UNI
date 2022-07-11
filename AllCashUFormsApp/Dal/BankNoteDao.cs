using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp.Dal
{
    public static class BankNoteDao
    {

        public static DataTable GetBankNoteTable(this V_BankNote V_BankNote,DateTime DateDoc)
        {
            DataTable dt = new DataTable();
            try
            {
                string date = DateDoc.ToString("yyyyMMdd", new CultureInfo("en-US"));
                //string sql = "select * from V_BankNote ";
                //sql += " where cast(DocDate as DATE) ='" + d + "'";
                //sql += " order by WHID";
                //dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);

                string sql = "proc_V_BankNote";
                Dictionary<string, object> sqlParmas = new Dictionary<string, object>();
                sqlParmas.Add("@DocDate", date);
                dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, sqlParmas);

                return dt;
            }
            catch (Exception ex)
            {
                ex.WriteLog(V_BankNote.GetType());
                return null;
            }
        }

    }
}
