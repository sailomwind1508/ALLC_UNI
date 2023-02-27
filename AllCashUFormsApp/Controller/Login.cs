using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp.Controller
{
    public class Login : BaseControl
    {
        public Login() : base("")
        {
            
        }

        public bool ValidateData(string username, string password, string depo)
        {
            bool ret = true;
            string message = string.Empty;

            if (string.IsNullOrEmpty(depo))
            {
                message = "กรุณาเลือกเดโป้เพื่อเข้าสู่ระบบ!";
                ret = false;
            }
            else if (string.IsNullOrEmpty(username))
            {
                message = "กรุณากรอก ชื่อผู้ใช้งาน!";
                ret = false;
            }
            else if (string.IsNullOrEmpty(password))
            {
                message = "กรุณากรอก รหัสผ่าน!";
                ret = false;
            }

            if (!string.IsNullOrEmpty(message))
            {
                FlexibleMessageBox.Show(message, "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return ret;
        }

        public bool VerifyData(string username, string password)
        {
            bool ret = false;
            try
            {
                Connection.GetConnectionStringsManual(); //last edit by sailom.k 18/10/2022

                //new tbl_Users().SelectAllWarmup();
                if (IsDNSConnected()) //last edit by sailom.k 18/10/2022
                {
                    var users = (new tbl_Users()).Select(x => x.FlagDel == false && x.Username.ToLower() == username.ToLower() && x.Password == password);
                    ret = users.Count > 0;
                }
                //if (users.Count > 0)
                //{
                //    ret = users.Any(x => x.FlagDel == false && x.Username.ToLower() == username.ToLower() && x.Password == password); //01-06-2021 by sailom
                //}
            }
            catch (Exception ex)
            {
                //ex.WriteLog(this.GetType());
                throw ex;
            }

            return ret;
        }

        private bool IsDNSConnected()
        {
            bool ret = true;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(Connection.ConnectionString);
            builder.ConnectTimeout = 5;
            using (var connection = new SqlConnection(builder.ToString()))
            {
                try
                {
                    connection.Open();
                    connection.Close();
                }
                catch (SqlException ex)
                {
                    ret = false;
                    throw ex;                    
                }

                return ret;
            }
        }

        public List<tbl_Users> GetAllData()
        {
            return new tbl_Users().SelectAll();
        }
    }
}
