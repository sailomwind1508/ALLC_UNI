using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AllCashUFormsApp
{
    public static class My_DataTable_Extensions
    {

        public static List<dynamic> ExecuteSQLToList(Type type, string cSql)
        {
            MemoryManagement.FlushMemory();

            var conn = new SqlConnection(Connection.ConnectionString);
            var dt = GetDataTable(cSql, conn);

            List<dynamic> dynamicListReturned = ConvertHelper.GetListFromDT(type, dt);

            return dynamicListReturned;
        }

        public static DataTable ExecuteSQLToDataTable(string cSql)
        {
            MemoryManagement.FlushMemory();
            try
            {
                var conn = new SqlConnection(Connection.ConnectionString);
                return GetDataTable(cSql, conn);
            }
            catch (Exception ex)
            {

                throw ex;
            }         
        }

        public static DataTable ExecuteStoreToDataTable(string cSql)
        {
            MemoryManagement.FlushMemory();

            var conn = new SqlConnection(Connection.ConnectionString);
            return GetDataTable(cSql, conn, CommandType.StoredProcedure);
        }

        public static List<dynamic> ExecuteStoreToList(Type type, string cSql, Dictionary<string, object> sqlParmas)
        {
            MemoryManagement.FlushMemory();

            var conn = new SqlConnection(Connection.ConnectionString);
            DataTable dt = GetDataTable(cSql, conn, CommandType.StoredProcedure, sqlParmas);

            List<dynamic> dynamicListReturned = ConvertHelper.GetListFromDT(type, dt);

            return dynamicListReturned;
        }

        public static DataTable ExecuteCenterStoreToDataTable(string cSql, Dictionary<string, object> sqlParmas)
        {
            MemoryManagement.FlushMemory();

            var conStr = ConfigurationManager.AppSettings["CenterConnect"].ToString();
            var conn = new SqlConnection(conStr);
            return GetDataTable(cSql, conn, CommandType.StoredProcedure, sqlParmas);
        }

        public static DataTable ExecuteStoreToDataTable(string cSql, Dictionary<string, object> sqlParmas)
        {
            MemoryManagement.FlushMemory();

            var conn = new SqlConnection(Connection.ConnectionString);
            return GetDataTable(cSql, conn, CommandType.StoredProcedure, sqlParmas);
        }

        public static void ExecuteSQL(CommandType comType, string cSql)
        {
            MemoryManagement.FlushMemory();

            string constr = Connection.ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(cSql, con);
                cmd.ExecuteNonQuery();
                cmd.CommandType = comType;
                cmd.CommandTimeout = 0;
                con.Close();
            }
        }

        public static int ExecuteSQLScalar(string cSql, CommandType comType)
        {
            MemoryManagement.FlushMemory();

            int ret = 1;
            var oCnn = new SqlConnection(Connection.ConnectionString);

            oCnn.Open();
            using (var command = new SqlCommand(cSql, oCnn, null))
            {
                var source = new TaskCompletionSource<DataTable>();
                var resultTable = new DataTable(command.CommandText);
                SqlDataReader dataReader = null;
                try
                {
                    command.CommandType = comType;
                    command.CommandTimeout = 0;
                     
                    Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    ret = 0;
                    oCnn.Close();
                    source.SetException(ex);
                }
                finally
                {
                    oCnn.Close();
                    dataReader?.Close();
                }

                return ret;
            }
        }

        public static int ExecuteSQLScalar(string cSql, CommandType comType, Dictionary<string, object> sqlParmas)
        {
            MemoryManagement.FlushMemory();

            int ret = 1;
            var oCnn = new SqlConnection(Connection.ConnectionString);

            oCnn.Open();
            using (var command = new SqlCommand(cSql, oCnn, null))
            {
                var source = new TaskCompletionSource<DataTable>();
                var resultTable = new DataTable(command.CommandText);
                SqlDataReader dataReader = null;
                try
                {
                    command.CommandType = comType;
                    command.CommandTimeout = 0;
                    foreach (var p in sqlParmas)
                    {
                        command.Parameters.Add(new SqlParameter(p.Key, p.Value));
                        //command.Parameters.Add(p);
                    }

                    // CommandBehavior.SingleRow - This is the secret to execute the datareader to return only one row  
                    Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    ret = 0;
                    oCnn.Close();
                    source.SetException(ex);
                }
                finally
                {
                    oCnn.Close();
                    dataReader?.Close();
                }

                return ret;
            }
        }

        /// <summary>  
        /// Read Sql  
        /// </summary>  
        /// <param name="command"></param>  
        /// <param name="oCnn"></param>  
        /// <returns></returns>  
        public static DataTable GetDataTable(string cSql, SqlConnection oCnn)
        {
            //MemoryManagement.FlushMemory();

            //DataTable dt = new DataTable();
            ////using (var con = new SqlConnection { ConnectionString = "ConnectionString" })
            //{
            //    using (var command = new SqlCommand { Connection = oCnn })
            //    {
            //        oCnn.Open();
            //        command.CommandText = cSql;
            //        //command.Parameters.AddWithValue("@param", "Param");
            //        //load the into DataTable
            //        dt.Load(command.ExecuteReader(), LoadOption.Upsert);
            //        oCnn.Close();
            //    }// this will dispose command

            //}// this will dispose and close connection

            //return GetDataTableFast(cSql, oCnn); //last edit by sailom.k 19/04/2022
            try
            {
                oCnn.Open();
                using (var command = new SqlCommand(cSql, oCnn, null))
                {
                    var source = new TaskCompletionSource<DataTable>();
                    var resultTable = new DataTable(command.CommandText);
                    SqlDataReader dataReader = null;
                    try
                    {
                        // CommandBehavior.SingleRow - This is the secret to execute the datareader to return only one row  
                        command.CommandTimeout = 0;
                        dataReader = command.ExecuteReader(CommandBehavior.Default);
                        resultTable.Load(dataReader);
                        source.SetResult(resultTable);
                    }
                    catch (Exception ex)
                    {
                        oCnn.Close();
                        source.SetException(ex);
                        throw ex;
                    }
                    finally
                    {
                        oCnn.Close();
                        dataReader?.Close();
                    }

                    return resultTable;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static DataTable GetDataTableFast(string cSql, SqlConnection oCnn)
        {
            oCnn.Open();
            DateTime start = DateTime.Now;
            IDataReader rdr = new SqlCommand(cSql, oCnn).ExecuteReader();
            DataTable resultTable = GetDataTableFromDataReader(rdr);
            TimeSpan ts = DateTime.Now.Subtract(start);
            System.Diagnostics.Trace.Write("Time Elapsed in Custom : " + ts.TotalMilliseconds);
            oCnn.Close();
            return resultTable;
        }

        public static DataTable GetDataTableFromDataReader(IDataReader dataReader)
        {
            DataTable schemaTable = dataReader.GetSchemaTable();
            DataTable resultTable = new DataTable();

            foreach (DataRow dataRow in schemaTable.Rows)
            {
                DataColumn dataColumn = new DataColumn();
                dataColumn.ColumnName = dataRow["ColumnName"].ToString();
                dataColumn.DataType = Type.GetType(dataRow["DataType"].ToString());
                dataColumn.ReadOnly = (bool)dataRow["IsReadOnly"];
                dataColumn.AutoIncrement = (bool)dataRow["IsAutoIncrement"];
                dataColumn.Unique = (bool)dataRow["IsUnique"];

                resultTable.Columns.Add(dataColumn);
            }

            while (dataReader.Read())
            {
                DataRow dataRow = resultTable.NewRow();
                for (int i = 0; i < resultTable.Columns.Count - 1; i++)
                {
                    dataRow[i] = dataReader[i];
                }
                resultTable.Rows.Add(dataRow);
            }

            return resultTable;
        }

        public static DataTable GetDataTable(string cSql, SqlConnection oCnn, CommandType comType)
        {
            MemoryManagement.FlushMemory();

            oCnn.Open();
            using (var command = new SqlCommand(cSql, oCnn, null))
            {
                var source = new TaskCompletionSource<DataTable>();
                var resultTable = new DataTable(command.CommandText);
                SqlDataReader dataReader = null;
                try
                {
                    command.CommandType = comType;
                    command.CommandTimeout = 0;
                    // CommandBehavior.SingleRow - This is the secret to execute the datareader to return only one row  
                    dataReader = command.ExecuteReader(CommandBehavior.Default);
                    resultTable.Load(dataReader);
                    source.SetResult(resultTable);
                }
                catch (Exception ex)
                {
                    oCnn.Close();
                    source.SetException(ex);
                }
                finally
                {
                    oCnn.Close();
                    dataReader?.Close();
                }

                return resultTable;
            }
        }

        public static DataTable GetDataTable(string cSql, SqlConnection oCnn, CommandType comType, Dictionary<string, object> sqlParmas)
        {
            MemoryManagement.FlushMemory();

            oCnn.Open();
            using (var command = new SqlCommand(cSql, oCnn, null))
            {
                var source = new TaskCompletionSource<DataTable>();
                var resultTable = new DataTable(command.CommandText);
                SqlDataReader dataReader = null;
                try
                {
                    command.CommandType = comType;
                    command.CommandTimeout = 0;
                    foreach (var p in sqlParmas)
                    {
                        command.Parameters.Add(new SqlParameter(p.Key, p.Value));
                        //command.Parameters.Add(p);
                    }

                    // CommandBehavior.SingleRow - This is the secret to execute the datareader to return only one row  
                    dataReader = command.ExecuteReader(CommandBehavior.Default);
                    resultTable.Load(dataReader);
                    source.SetResult(resultTable);

                }
                catch (Exception ex)
                {
                    oCnn.Close();
                    source.SetException(ex);
                }
                finally
                {
                    oCnn.Close();
                    dataReader?.Close();
                }

                return resultTable;
            }
        }

        // Export DataTable into an excel file with field names in the header line
        // - Save excel file without ever making it visible if filepath is given
        // - Don't save excel file, just make it visible if no filepath is given
        public static void ExportToExcel(this DataTable tbl, string excelFilePath = null, string sheetName = null)
        {
            try
            {
                if (tbl == null || tbl.Columns.Count == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                // load excel, and create a new workbook
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Workbooks.Add();

                // single worksheet
                Microsoft.Office.Interop.Excel._Worksheet workSheet = excelApp.ActiveSheet;
                //workSheet.Name = sheetName;

                // column headings
                for (var i = 0; i < tbl.Columns.Count; i++)
                {
                    workSheet.Cells[1, i + 1] = tbl.Columns[i].ColumnName;
                }

                // rows
                for (var i = 0; i < tbl.Rows.Count; i++)
                {
                    // to do: format datetime values before printing
                    for (var j = 0; j < tbl.Columns.Count; j++)
                    {
                        workSheet.Cells[i + 2, j + 1] = tbl.Rows[i][j];
                    }
                }

                // check file path
                if (!string.IsNullOrEmpty(excelFilePath))
                {
                    try
                    {
                        workSheet.SaveAs(excelFilePath);
                        excelApp.Quit();
                        excelApp.Visible = true;
                        //MessageBox.Show("Excel file saved!");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                                            + ex.Message);
                    }
                }

                else
                { // no file path is given
                    excelApp.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ExportToExcel: \n" + ex.Message);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowThreadProcessId(HandleRef handle, out int processId);

        private static void KillProcess(int pid, string processName)
        {
            // to kill current process of excel
            System.Diagnostics.Process[] AllProcesses = System.Diagnostics.Process.GetProcessesByName(processName);
            foreach (System.Diagnostics.Process process in AllProcesses)
            {
                if (process.Id == pid)
                {
                    process.Kill();
                }
            }
            AllProcesses = null;
        }

        private static void NAR(object o)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(o);
            }
            catch { }
            finally
            {
                o = null;
            }
        }


        public static void ExportToExcelR2(List<DataTable> _reportDTList, string ExcelFilePath = null, string excelReportName = null)
        {
            try
            {
                ClosedXML.Excel.XLWorkbook wb = new ClosedXML.Excel.XLWorkbook();

                using (ClosedXML.Excel.XLWorkbook _wb = new ClosedXML.Excel.XLWorkbook())
                {
                    foreach (DataTable _dt in _reportDTList)
                    {
                        if (_dt.Rows.Count > 2)
                        {
                            if (string.IsNullOrEmpty(_dt.TableName))
                                _dt.TableName = "Sheet1";

                            _wb.Worksheets.Add(_dt, _dt.TableName);
                        }
                    }

                    wb = _wb;
                }

                wb.SaveAs(ExcelFilePath);
                System.Diagnostics.Process.Start(ExcelFilePath, excelReportName);

                //string msg = "Save File Complete";
                //msg.ShowInfoMessage();
            }
            catch (Exception ex)
            {
                ex.Message.ShowWarningMessage();
            }
        }

        public static void ExportToExcelR3(List<DataTable> _reportDTList, string ExcelFilePath = null, string excelReportName = null)
        {
            try
            {
                ClosedXML.Excel.XLWorkbook wb = new ClosedXML.Excel.XLWorkbook();

                using (ClosedXML.Excel.XLWorkbook _wb = new ClosedXML.Excel.XLWorkbook())
                {
                    foreach (DataTable _dt in _reportDTList)
                    {
                        if (_dt.Rows.Count > 0)
                        {
                            if (string.IsNullOrEmpty(_dt.TableName))
                                _dt.TableName = "Sheet1";

                            _wb.Worksheets.Add(_dt, _dt.TableName);
                        }
                    }

                    wb = _wb;
                }

                wb.SaveAs(ExcelFilePath);
                System.Diagnostics.Process.Start(ExcelFilePath, excelReportName);

                //string msg = "Save File Complete";
                //msg.ShowInfoMessage();
            }
            catch (Exception ex)
            {
                ex.Message.ShowWarningMessage();
            }
        }
    }
}
