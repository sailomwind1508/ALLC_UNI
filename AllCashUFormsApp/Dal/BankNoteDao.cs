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
                string d = DateDoc.ToString("yyyyMMdd", new CultureInfo("en-US"));
                string sql = " ";
                sql += "select * from V_BankNote where cast(DocDate as DATE) ='" + d + "'";
                sql += " order by WHID";

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

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
