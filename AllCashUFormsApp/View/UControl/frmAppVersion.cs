using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.IO.Compression;
using System.Threading;
using System.Diagnostics;

namespace AllCashUFormsApp.View.UControl
{
    public partial class frmAppVersion : Form
    {
        static DataTable allFileDT = new DataTable();
        public frmAppVersion()
        {
            InitializeComponent();
        }

        private void frmAppVersion_Load(object sender, EventArgs e)
        {
            string version = allFileDT.Rows[0]["Version"].ToString();

            lblMessage.Text = "ตรวจพบ U-Force เวอร์ชั่น:" + version + " ต้องการอัพเดตใช่หรือไม่?";
        }

        public void PrepareAppVersion(DataTable dt)
        {
            allFileDT = dt;
        }

        private void OpenConsoleUpdate()
        {
            string localPath = ConfigurationManager.AppSettings["LocalPath"].ToString();
            Application.Exit();

            string version = allFileDT.Rows[0]["Version"].ToString();

            Process.Start(localPath + "UpdateU-ForceApp.exe");
            //Process.Start(localPath + "UpdateU-ForceApp.exe", version);
        }

        public void DownloadFile()
        {
            try
            {
                //Set the File Path.
                string downloadPath = ConfigurationManager.AppSettings["UpdatePath"].ToString();
                string localPath = ConfigurationManager.AppSettings["LocalPath"].ToString();

                foreach (DataRow row in allFileDT.Rows)
                {
                    string fileName = row["fileName"].ToString();
                    using (WebClient wc = new WebClient())
                    {
                        //wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                        wc.DownloadFile(
                            // Param1 = Link of file
                            new System.Uri(downloadPath + fileName),
                            // Param2 = Path to save
                            localPath + fileName
                        );
                        
                    }


                    //System.IO.Compression.ZipFile.CreateFromDirectory(startPath, zipPath);

                    //System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);

                    //Compression.UnZip(localPath + fileName, localPath);

                    //string startPath = localPath;
                    //string zipPath = localPath + fileName;
                    //string extractPath = zipPath;

                    //DirectoryInfo di = new DirectoryInfo(localPath);
                    //foreach (FileInfo fi in di.GetFiles("*.zip"))
                    //    Compression.Decompress(fi);


                    //string zipped_path = localPath + fileName;
                    //string unzipped_path = "U-Force";
                    //string arguments = "e " + zipped_path + " -o" + unzipped_path;

                    //System.Diagnostics.Process process = Compression.Launch_in_Shell(localPath + fileName, "U-Force", arguments);

                    //if (!(process.ExitCode == 0))
                    //    throw new Exception("Unable to decompress file: " + zipped_path);


                    //ZipFile.CreateFromDirectory(startPath, zipPath);
                    //System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);
                }

                FlexibleMessageBox.Show("อัพเดตโปรแกรมเสร็จเรียบร้อยแล้ว!", "ข้อความ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Thread.Sleep(1000);
                Application.Exit();
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OpenConsoleUpdate();
        }

        private void btnCencal_Click(object sender, EventArgs e)
        {
            MemoryManagement.FlushMemory();
            this.Close();
        }
    }
}
