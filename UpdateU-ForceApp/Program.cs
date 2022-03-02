using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UpdateU_ForceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string version = "1";

            Console.WriteLine("Updating U-Force!!! Please wait...");

            //System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            //System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            //string version = fvi.FileVersion;

            try
            {
                var dt = CheckAppVersion(version);
                version = dt.Rows[0]["Version"].ToString();

                //Set the File Path.
                string downloadPath = ConfigurationManager.AppSettings["UpdatePath"].ToString();
                string localPath1 = ConfigurationManager.AppSettings["LocalPathD"].ToString();
                string localPath2 = ConfigurationManager.AppSettings["LocalPathE"].ToString();
                string localPath3 = ConfigurationManager.AppSettings["LocalPathC"].ToString();
                string _localPath = "";

                if (Directory.Exists(localPath1)) //D
                {
                    _localPath = localPath1;
                }
                else if (Directory.Exists(localPath2)) //E
                {
                    _localPath = localPath2;
                }
                else if (Directory.Exists(localPath3)) //C
                {
                    _localPath = localPath3;
                }


                string fileName = "";

                foreach (DataRow row in dt.Rows)
                {
                    fileName = row["fileName"].ToString();
                    using (WebClient wc = new WebClient())
                    {
                        wc.DownloadFile(
                            // Param1 = Link of file
                            new System.Uri(downloadPath + fileName),
                            // Param2 = Path to save
                            _localPath + fileName
                        );
                    }
                }

                Compression.UnZip(_localPath + fileName, _localPath);

                //DirectoryInfo di = new DirectoryInfo(localPath);
                //foreach (FileInfo fi in di.GetFiles("*.zip"))
                //    Compression.Decompress(fi);

                Console.WriteLine("Update U-Force Back End version " + version + " is Successful!!!");

                Thread.Sleep(5000); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Update U-Force Back End version " + version + " is Fail!!! Please check your internet connection => " + ex.Message);

                Thread.Sleep(5000);
            }
        }

        public static DataTable CheckAppVersion(string version)
        {
            DataTable dt = new DataTable("AppVersion");
            try
            {
                DataTable newTable = new DataTable();
                Dictionary<string, object> sqlParams = new Dictionary<string, object>();
                sqlParams.Add("@versionParam", version);

                string sql = "proc_check_app_version";
                dt = ExecuteCenterStoreToDataTable(sql, sqlParams);
            }
            catch (Exception ex)
            {
                dt = null;
                Console.WriteLine(ex.Message);
            }

            return dt;
        }

        public static DataTable ExecuteCenterStoreToDataTable(string cSql, Dictionary<string, object> sqlParmas)
        {
            var conStr = ConfigurationManager.AppSettings["CenterConnect"].ToString();
            var conn = new SqlConnection(conStr);
            return GetDataTable(cSql, conn, CommandType.StoredProcedure, sqlParmas);
        }

        public static DataTable GetDataTable(string cSql, SqlConnection oCnn, CommandType comType, Dictionary<string, object> sqlParmas)
        {
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
    }
}
