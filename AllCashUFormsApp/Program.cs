using AllCashUFormsApp.View.Page;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace AllCashUFormsApp
{
    static class Program
    {
        //private static Thread thread;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());

            //Process[] p = Process.GetProcessesByName("U-Force Back-End");
            //if (p.Count() > 1)
            //{
            //    p.First().Kill();
            //}
            //else
            //{
            //    Application.EnableVisualStyles();
            //    Application.SetCompatibleTextRenderingDefault(false);
            //    Application.Run(new frmLogin());
            //}




            //if (Application.OpenForms.OfType<frmLogin>().Any())
            //{
            //    Process[] p = Process.GetProcessesByName("U-Force Back-End");
            //    foreach (var item in p)
            //    {
            //        item.Kill();
            //    }

            //    Application.Exit();
            //}
            //else
            //{
            //    Application.EnableVisualStyles();
            //    Application.SetCompatibleTextRenderingDefault(false);
            //    Application.Run(new frmLogin());
            //}

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            //thread = new Thread(BusyWorkThread);
            //thread.IsBackground = false;
            //thread.Start();

            //Application.Run(new frmLogin());

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            //// Show the login form
            //frmLogin loginForm = new frmLogin();
            //loginForm.ShowDialog();

            //// Show the main form
            //Application.Run(new frmLogin());
        }

        //public static void BusyWorkThread()
        //{
        //    while (true)
        //    {
        //        Thread.Sleep(1000);
        //    }
        //}
    }
}
