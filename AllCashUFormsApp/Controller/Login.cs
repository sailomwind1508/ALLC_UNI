using AllCashUFormsApp.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp.Controller
{
    public class Login
    {
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
                MessageBox.Show(message, "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return ret;
        }

        public bool VerifyData(string username, string password)
        {
            bool ret = false;
            try
            {
                var users = (new tbl_Users()).SelectAll();
                if (users.Count > 0)
                {
                    ret = users.Any(x => x.Username.ToLower() == username.ToLower() && x.Password == password);
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
            }

            return ret;
        }

        public Dictionary<string, string> GetConfigData()
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            ret.Add("=== เลือก ===", "");
            try
            {
                var conStrList = ConfigurationManager.ConnectionStrings;
                List<string> depoList = new List<string>();

                foreach (var item in conStrList)
                {
                    string name = ((ConnectionStringSettings)item).Name;
                    if (name.Contains("UNI"))
                    {
                        string depoName = name.Substring(4, (name.Length - 4));
                        ret.Add(depoName, item.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
            }

            return ret;
        }


        public List<tbl_Users> GetAllData()
        {
            return new tbl_Users().SelectAll();
        }
    }
}
