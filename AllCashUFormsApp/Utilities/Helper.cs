using AllCashUFormsApp.Model;
using AllCashUFormsApp.View.UControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AllCashUFormsApp
{
    public static class Helper
    {
        public static tbl_Users tbl_Users { get; set; }
        public static string ConnectionString { get; set; }
        public static string Original_ConnectionString { get; set; }
        public static string BranchName { get; set; }
        public static string Original_BranchName { get; set; }
        public static string user_name { get; set; }

        public static void WriteLog(this Exception ex, Type type)
        {
            ErrorLogsDao.Insert(new tbl_error_logs { user_code = Helper.user_name, form_name = (type != null ? type.Name : ""), function_name = Helper.GetCurrentMethod(), err_desc = ex.Message });
        }

        public static void WriteLog(this string msg, Type type)
        {
            ErrorLogsDao.Insert(new tbl_error_logs { user_code = Helper.user_name, form_name = (type != null ? type.Name : ""), function_name = Helper.GetCurrentMethod(), err_desc = msg }); //edit by sailom.k 11/11/2021
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }

        public static Dictionary<string, string> PrepareDataParameter(Object data)
        {
            Dictionary<string, string> _data = new Dictionary<string, string>();
            foreach (var prop in data.GetType().GetProperties())
            {
                if (prop.GetValue(data, null) != null)
                {
                    _data.Add(prop.Name, prop.GetValue(data, null).ToString());
                }
            }

            return _data;
        }

        public static string Replacement(string input, string replacement)
        {
            string[] words = input.Split(' ');
            string result = string.Empty;

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Contains('/'))
                {
                    int barIndex = Reverse(words[i]).LastIndexOf('/') + 1;
                    int removeLenght = words[i].Substring(barIndex).Length;
                    words[i] = Reverse(words[i]);
                    words[i] = words[i].Remove(barIndex, removeLenght);
                    words[i] = words[i].Insert(barIndex, Reverse(replacement));
                    string ToReverse = words[i];
                    words[i] = Reverse(ToReverse);

                    break;
                }
            }

            for (int i = 0; i < words.Length; i++)
            {
                result += words[i] + " ";
            }

            result = result.Remove(result.Length - 1);

            return result;
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

    }
}
