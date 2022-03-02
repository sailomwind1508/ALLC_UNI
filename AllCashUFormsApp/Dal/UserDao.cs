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
    public static class UserDao
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="tbl_Users"></param>
        /// <returns></returns>
        public static List<tbl_Users> Select(this tbl_Users tbl_Users, Func<tbl_Users, bool> predicate)
        {
            List<tbl_Users> list = new List<tbl_Users>();
            try
            {
                list = tbl_Users.SelectAll().Where(predicate).ToList();

                //using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                //{
                //    list = db.tbl_Users.Where(predicate).ToList();
                //}
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Users.GetType());
            }

            return list;
        }

        /// <summary>
        /// select all data
        /// </summary>
        /// <param name="tbl_Users"></param>
        /// <returns></returns>
        public static List<tbl_Users> SelectAll(this tbl_Users tbl_Users)
        {
            List<tbl_Users> list = new List<tbl_Users>();
            try
            {

                //string sql = "";
                //sql += " SELECT * FROM [dbo].[tbl_Users] Order By UserID";

                //List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Users), sql);
                //list = dynamicListReturned.Cast<tbl_Users>().ToList();

                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    list = db.tbl_Users.OrderBy(x => x.UserID).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Users.GetType());
            }

            return list;
        }

        /// <summary>
        /// add new data
        /// </summary>
        /// <param name="tbl_Users"></param>
        /// <returns></returns>
        public static int Insert(this tbl_Users tbl_Users)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.tbl_Users.Attach(tbl_Users);
                    db.tbl_Users.Add(tbl_Users);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Users.GetType());
            }

            return ret;
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="tbl_Users"></param>
        /// <returns></returns>
        public static int Update(this tbl_Users tbl_Users)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    var updateData = db.tbl_Users.FirstOrDefault(x => x.EmpID == tbl_Users.EmpID);
                    if (updateData != null)
                    {
                        foreach (PropertyInfo updateDataItem in updateData.GetType().GetProperties())
                        {
                            foreach (PropertyInfo tbl_UsersItem in tbl_Users.GetType().GetProperties())
                            {
                                if (updateDataItem.Name == tbl_UsersItem.Name)
                                {
                                    var value = tbl_UsersItem.GetValue(tbl_Users, null);

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
                        ret = tbl_Users.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Users.GetType());
            }

            return ret;
        }

        /// <summary>
        /// remove data
        /// </summary>
        /// <param name="tbl_Users"></param>
        /// <returns></returns>
        public static int Delete(this tbl_Users tbl_Users)
        {
            int ret = 0;
            try
            {
                using (DB_ALL_CASH_UNIEntities db = new DB_ALL_CASH_UNIEntities(Helper.ConnectionString))
                {
                    db.Entry(tbl_Users).State = EntityState.Deleted;
                    db.tbl_Users.Remove(tbl_Users);
                    ret = db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Users.GetType());
            }

            return ret;
        }

        public static DataTable proc_User_Data(this tbl_Users tbl_Users, Dictionary<string, object> _params)
        {
            try
            {
                DataTable newTable = new DataTable();

                string sql = "proc_User_Data";

                newTable = My_DataTable_Extensions.ExecuteStoreToDataTable(sql, _params);

                return newTable;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<tbl_Users> SelectUserList(this tbl_Users tbl_Users, string allEmpID = "")
        {
            List<tbl_Users> list = new List<tbl_Users>();
            try
            {
                string sql = "SELECT * FROM [dbo].[tbl_Users]";

                if (!string.IsNullOrEmpty(allEmpID))
                {
                    sql += " WHERE EmpID IN (SELECT [value] FROM dbo.fn_split_string_to_column('" + allEmpID + "', ',')) ";
                }

                List<dynamic> dynamicListReturned = My_DataTable_Extensions.ExecuteSQLToList(typeof(tbl_Users), sql);
                list = dynamicListReturned.Cast<tbl_Users>().ToList();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_Users.GetType());
            }

            return list;
        }

        private static string SetQueryInsert(string db_server, string db_name)
        {
            string sql = "INSERT INTO [" + db_server + "]." + db_name + ".dbo.[tbl_Users]";
            sql += " ([Username] ,[Password] ,[FirstName]  ,[LastName] ,[Email]";
            sql += " ,[EmpID]  ,[RoleID] ,[CrDate] ,[CrUser] ,[FlagDel] ,[FlagSend] )";

            sql += " VALUES (";
            sql += " @Username ,@Password ,@FirstName ,@LastName ,@Email";
            sql += " ,@EmpID  ,@RoleID ,@CrDate ,@CrUser ,@FlagDel ,@FlagSend";
            sql += ")";

            return sql;
        }

        public static int InsertUser(this tbl_Users tbl_User, string db_name, string db_server)
        {
            int ret = 0;

            SqlConnection con = new SqlConnection(Connection.ConnectionString);
            SqlCommand cmd = new SqlCommand();

            try
            {
                string sql = "";
                sql = SetQueryInsert(db_server, db_name);

                cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.Parameters.AddWithValue("@Username", tbl_User.Username);
                cmd.Parameters.AddWithValue("@Password", tbl_User.Password);
                cmd.Parameters.AddWithValue("@FirstName", tbl_User.FirstName);
                cmd.Parameters.AddWithValue("@LastName", tbl_User.LastName);
                cmd.Parameters.AddWithValue("@Email", tbl_User.Email);

                cmd.Parameters.AddWithValue("@EmpID", tbl_User.EmpID);
                cmd.Parameters.AddWithValue("@RoleID", tbl_User.RoleID);


                cmd.Parameters.AddWithValue("@CrDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@CrUser", Helper.tbl_Users.Username);

                cmd.Parameters.AddWithValue("@FlagDel", false);
                cmd.Parameters.AddWithValue("@FlagSend", false);

                ret = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                ex.WriteLog(tbl_User.GetType());
            }

            return ret;
        }
    }
}
