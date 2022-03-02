using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AllCashUFormsApp.Controller
{
    public class CancelPOItem : BaseControl
    {
        private SqlCommand m_rCommand;

        public SqlCommand Command
        {
            get { return m_rCommand; }
            set { m_rCommand = value; }
        }

        public CancelPOItem() : base("")
        {
           
        }

        public DataTable GetCustomerShelf(string customerID)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = " SELECT ShelfID, REPLACE([ShelfID], ' ', '') AS 'ShelfIDName' FROM [dbo].[tbl_ArCustomerShelf] WHERE CustomerID = '" + customerID + "' ";
                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                dt = null;
            }

            return dt;
        }

        public bool CallCancelPOItem(string poDocNo, string shelfID, string productID, BackgroundWorker self)
        {
            bool ret = false;
            try
            {
                using (var cn = new SqlConnection(Connection.ConnectionString))
                {
                    cn.FireInfoMessageEventOnUserErrors = true;
                    cn.Open();
                    cn.InfoMessage += (o, args) => self.ReportProgress(0, args.Message);

                    using (Command = cn.CreateCommand())
                    {
                        Command.CommandText = "proc_manual_remove_PO_Item";
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.CommandTimeout = 0;
                        Command.Parameters.Add(new SqlParameter("@PODocNo", poDocNo));
                        Command.Parameters.Add(new SqlParameter("@ShelfID", shelfID));
                        Command.Parameters.Add(new SqlParameter("@ProductID", productID));
                        Command.ExecuteNonQuery();

                        ret = true;
                    }
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                ret = false;
            }

            return ret;
        }

        public bool CallStopCancelPOItem()
        {
            bool ret = false;
            try
            {
                Command.Cancel();
                ret = true;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
                ret = false;
            }

            return ret;
        }

    }
}
