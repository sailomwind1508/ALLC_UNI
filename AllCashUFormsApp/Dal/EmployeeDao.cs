using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp
{
    public static class EmployeeDao
    {
        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_Employee"></param>
        /// <returns></returns>
        public static tbl_Employee SelectSingle(this tbl_Employee tbl_Employee, string empID)
        {
            List<tbl_Employee> list = new List<tbl_Employee>();
            tbl_Employee ret = null;
            try
            {
                DataTable dt = new DataTable();
                string sql = @" SELECT * FROM [dbo].[tbl_Employee] WHERE EmpID = '" + empID + "' ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Employee), sql);
                list = dynamicListReturned.Cast<tbl_Employee>().ToList();

                if (list.Count > 0)
                    ret = list.First();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Employee.GetType());
            }

            return ret;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_Employee"></param>
        /// <returns></returns>
        public static List<tbl_Employee> Select(this tbl_Employee tbl_Employee, Func<tbl_Employee, bool> predicate)
        {
            List<tbl_Employee> list = new List<tbl_Employee>();
            try
            {
                list = tbl_Employee.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_Employee.Where(predicate).OrderBy(x => x.EmpCode).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Employee.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_Employee"></param>
        /// <returns></returns>
        public static List<tbl_Employee> SelectAll(this tbl_Employee tbl_Employee)
        {
            List<tbl_Employee> list = new List<tbl_Employee>();
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                sql += " SELECT * ";
                sql += "  FROM [dbo].[tbl_Employee] ";

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Employee), sql);
                list = dynamicListReturned.Cast<tbl_Employee>().ToList();

                //SqlDataAdapter da = new SqlDataAdapter(sql, Connection.ConnectionString);
                //da.Fill(dt);

                //list = ConvertHelper.ConvertDataTable<tbl_Employee>(dt);

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_Employee.OrderBy(x => x.EmpCode).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Employee.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_Employee"></param>
        /// <returns></returns>
        public static int Insert(this tbl_Employee tbl_Employee)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_Employee.Attach(tbl_Employee);
                    db.tbl_Employee.Add(tbl_Employee);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Employee.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_Employee"></param>
        /// <returns></returns>
        public static int Update(this tbl_Employee tbl_Employee)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_Employee.FirstOrDefault(x => x.EmpID == tbl_Employee.EmpID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_EmployeeItem in tbl_Employee.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_EmployeeItem.Name)
                                {
                                    var value = tbl_EmployeeItem.GetValue(tbl_Employee, null);

                                    Type t = Nullable.GetUnderlyingType(updateDataItem.PropertyType) ?? updateDataItem.PropertyType;
                                    object safeValue = (value == null) ? null : Convert.ChangeType(value, t);

                                    updateDataItem.SetValue(updateData, safeValue, null);
                                }
                            }
                        }

                        db.Entry(updateData).State = System.Data.Entity.EntityState.Modified;
                        ret = db.SaveChanges();
                    }
                    else
                    {
                        ret = tbl_Employee.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Employee.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_Employee"></param>
        /// <returns></returns>
        public static int Delete(this tbl_Employee tbl_Employee)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_Employee).State = EntityState.Deleted;
                    db.tbl_Employee.Remove(tbl_Employee);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Employee.GetType());
            }

            return ret;
        }

        public static DataTable GetSaleEmployee(this tbl_Employee tbl_Employee, int PositionID)//
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "SELECT * FROM SaleEmployeeView ";
                sql += " WHERE PositionID = " + PositionID + "";
                //sql += " AND WHID IS NULL";

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable GetAllSaleEmployee(this tbl_Employee tbl_Employee)//
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "SELECT * FROM AllSaleEmployeeView";

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable GetEmployeePopup(this tbl_Employee tbl_Employee)//
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "SELECT EmpID, CONCAT(TitleName, ' ', FirstName) AS FullName FROM tbl_Employee ";
                sql += " WHERE PositionID = 4 AND DepartmentID = 3";

                dt = My_DataTable_Extensions.ExecuteSQLToDataTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable proc_Employee_Data(this tbl_Employee tbl_Employee, Dictionary<string, object> _params)
        {
            try
            {
                DataTable newTable = new DataTable();

                string sql = "proc_Employee_Data";

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);

                return newTable;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int InsertEmployee(this tbl_Employee tbl_Employee, string db_name, string db_server)
        {
            int ret = 0;

            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(Connection.ConnectionString);
            SqlCommand cmd = new SqlCommand();

            try
            {
                string sql = "";
                sql = SetQueryInsert(db_server, db_name);

                cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.Parameters.AddWithValue("@EmpID", tbl_Employee.EmpID);
                cmd.Parameters.AddWithValue("@EmpCode", tbl_Employee.EmpCode);
                cmd.Parameters.AddWithValue("@TitleName", tbl_Employee.TitleName);
                cmd.Parameters.AddWithValue("@FirstName", tbl_Employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", tbl_Employee.LastName);

                cmd.Parameters.AddWithValue("@NickName", tbl_Employee.NickName);
                cmd.Parameters.AddWithValue("@DepartmentID", tbl_Employee.DepartmentID);
                cmd.Parameters.AddWithValue("@PositionID", tbl_Employee.PositionID);
                cmd.Parameters.AddWithValue("@MgrID", tbl_Employee.MgrID);
                cmd.Parameters.AddWithValue("@RoleID", tbl_Employee.RoleID);

                if (tbl_Employee.Mobile == null)
                {
                    cmd.Parameters.AddWithValue("@Mobile", "");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Mobile", tbl_Employee.Mobile);
                }

                cmd.Parameters.AddWithValue("@CrDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@CrUser", Helper.tbl_Users.Username);

                cmd.Parameters.AddWithValue("@FlagDel", false);
                cmd.Parameters.AddWithValue("@FlagSend", false);
                cmd.Parameters.AddWithValue("@IDCard", tbl_Employee.IDCard);
                cmd.Parameters.AddWithValue("@EmpIDCard", tbl_Employee.EmpIDCard);

                ret = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Employee.GetType());
            }

            return ret;
        }

        private static string SetQueryInsert(string db_server, string db_name)
        {
            string sql = "INSERT INTO [" + db_server + "]." + db_name + ".dbo.[tbl_Employee]";
            sql += " ([EmpID] ,[EmpCode] ,[TitleName] ,[FirstName] ,[LastName] ";
            sql += " ,[NickName]  ,[DepartmentID] ,[PositionID] ,[MgrID] ,[RoleID] ,[Mobile]";
            sql += " ,[CrDate]  ,[CrUser] ,[FlagDel] ,[FlagSend]";
            sql += " ,[IDCard]  ,[EmpIDCard] )";

            sql += " VALUES (";
            sql += " @EmpID ,@EmpCode ,@TitleName ,@FirstName ,@LastName";
            sql += " ,@NickName ,@DepartmentID ,@PositionID ,@MgrID ,@RoleID ,@Mobile";
            sql += " ,@CrDate ,@CrUser ,@FlagDel ,@FlagSend";
            sql += " ,@IDCard ,@EmpIDCard";
            sql += ")";
            return sql;
        }

        public static List<tbl_Employee> SelectEmpList(this tbl_Employee tbl_Employee, string allEmpID)
        {
            List<tbl_Employee> list = new List<tbl_Employee>();
            try
            {
                string sql = "SELECT * FROM [dbo].[tbl_Employee]";

                if (!string.IsNullOrEmpty(allEmpID))
                {
                    sql += " WHERE EmpID IN (SELECT [value] FROM dbo.fn_split_string_to_column('" + allEmpID + "', ',')) ";
                }

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Employee), sql);
                list = dynamicListReturned.Cast<tbl_Employee>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Employee.GetType());
            }

            return list;
        }

        public static DataTable proc_GetEmployee_Data(this tbl_Employee tbl_Employee, Dictionary<string, object> _params ,bool flagAllColumns = false)
        {
            try
            {
                DataTable dt = new DataTable();

                string sql = "proc_GetEmployee_Data";
                dt = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);

                if (flagAllColumns == false)
                {
                    DataTable newTable = new DataTable("DepoTable");
                    newTable.Columns.Add("รหัสพนักงาน", typeof(string));
                    newTable.Columns.Add("ชื่อ-นามสกุล", typeof(string));
                    newTable.Columns.Add("แผนก", typeof(string));
                    newTable.Columns.Add("ตำแหน่ง", typeof(string));
                    newTable.Columns.Add("ชื่อหัวหน้า", typeof(string));
                    newTable.Columns.Add("เบอร์โทร", typeof(string));
                    newTable.Columns.Add("วันที่เพิ่ม", typeof(string));
                    newTable.Columns.Add("เพิ่มโดย", typeof(string));
                    newTable.Columns.Add("วันที่แก้ไข", typeof(string));
                    newTable.Columns.Add("แก้ไขโดย", typeof(string));
                    newTable.Columns.Add("ยกเลิก", typeof(bool));

                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            newTable.Rows.Add(dt.Rows[i]["EmpCode"], dt.Rows[i]["FullName"], dt.Rows[i]["DepartmentName"], dt.Rows[i]["PositionName"], "",
                                dt.Rows[i]["Mobile"], dt.Rows[i]["CrDate"], dt.Rows[i]["CrUser"],
                                dt.Rows[i]["EdDate"], dt.Rows[i]["EdUser"], dt.Rows[i]["FlagDel"]);
                        }
                    }

                    return newTable;
                }
                else
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
