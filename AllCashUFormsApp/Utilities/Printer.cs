using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AllCashUFormsApp
{
    public static class Printer
    {
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Name);

        public static PrinterSettings Printer_Settings = new System.Drawing.Printing.PrinterSettings();

        /// <summary>
        /// Get or Sets the session's Default Printer
        /// </summary>
        public static string Session_DefaultPrinter
        {
            get { return Printer_Settings.PrinterName; }
            set
            {
                SetDefaultPrinter(value);
                Printer_Settings.DefaultPageSettings.PrinterSettings.PrinterName = value;
                Printer_Settings.PrinterName = value;
            }
        }
    }
}
