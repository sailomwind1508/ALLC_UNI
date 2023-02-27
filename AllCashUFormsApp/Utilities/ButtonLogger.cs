using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp
{
    public class ButtonLogger : IMessageFilter
    {
        private const int WM_KEYUP = 0x101;
        private const int WM_LBUTTONUP = 0x202;

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_LBUTTONUP || (m.Msg == WM_KEYUP && ((int)m.WParam == 32 || (int)m.WParam == 13)))
            {
                Control ctl = Control.FromHandle(m.HWnd);
                if (ctl is Button)
                {
                    LogButtonClick((Button)ctl);
                }
            }
            return false; // allow normal processing of all messages
        }

        private void LogButtonClick(Button btn)
        {
            WriteLog("Click: " + btn.Parent.Name.ToString() + "." + btn.Name.ToString() + " (\"" + btn.Text + "\")");
            string msg = "Click: " + btn.Parent.Name.ToString() + "." + btn.Name.ToString() + " (\"" + btn.Text + "\")";
            msg.WriteLog(null);
        }

        private void WriteLog(string message)
        {
            Console.WriteLine(message);
        }

        public void Log(string message)
        {
            var stackTrace = new StackTrace();
            var caller = stackTrace.GetFrame(1).GetMethod().Name;
            Console.WriteLine($"{DateTime.Now:dd/MM/yyyy hh:mm:ss} {caller} : {message}");

            string msg = $"{DateTime.Now:dd/MM/yyyy hh:mm:ss} {caller} : {message}";
            msg.WriteLog(null);
        }

    }
}
