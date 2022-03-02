using AllCashUFormsApp.Controller;
using AllCashUFormsApp.Dal;
using AllCashUFormsApp.Model;
using AllCashUFormsApp.View.Page;
using AllCashUFormsApp.View.UControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace AllCashUFormsApp
{
    public static class FormHelper
    {
        static OD odBU = new OD();

        static ObjectFactory objectFactory = new ObjectFactory();
        static ObjectType _objType;

        //private static TextBox _txtSupplierCode;
        //private static TextBox _txtSuppName;
        //private static TextBox _txtAddress;
        //private static TextBox _txtContact;
        //private static TextBox _txtTelephone;
        private static NumericUpDown creditDayCtrl;
        private static DateTimePicker docDateCtrl;
        private static DateTimePicker dueDateCtrl;
        private static List<Control> controlList = new List<Control>();
        private static List<Control> supControlList = new List<Control>();
        private static List<Control> whControlList = new List<Control>();
        private static List<Control> fbiControlList = new List<Control>();
        private static List<Control> empControlList = new List<Control>();
        private static List<Control> custControlList = new List<Control>();
        private static List<Control> prdControlList = new List<Control>();
        private static List<Control> supDistictControlList = new List<Control>();
        private static List<Control> whIDControlList = new List<Control>();
        public static List<tbl_InvMovement> invWhItems = new List<tbl_InvMovement>();
        public static List<string> PrintingReportName = new List<string>();
        public static bool ShowPrintingReportName = true;

        private static Form frm;

        public static void CreateAndSendMail(string reportName, string branchName, string date)
        {

            string subject = string.Join(" ", new string[] { reportName, " ของ ", branchName, " ประจำวันที่ ", date });
            string bodyHtmlString = @" <p style='margin-right:0cm;margin-left:0cm;font-size:16px;font-family:'Tahoma',sans-serif;margin:0cm;'>
            <span style='font-size:17px;font-family:'Cordia New',sans-serif;'>นี่คือข้อความอัตโนมัติจากระบบ&nbsp;</span>U-Force : &nbsp;<span id='isPasted' style='color: rgb(0, 0, 0); font-family: 'Cordia New', sans-serif; font-size: 17px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;'>ตรวจ</span>
            <span style='font-size:17px;font-family:'Cordia New',sans-serif;'>&nbsp;</span>" + reportName + @"<span style='font-size:17px;font-family:'Cordia New',sans-serif;'>&nbsp;ประจำ &nbsp; " + branchName + @" &nbsp;ของวันที่ &nbsp;" + date + @"</span>&nbsp;</p>
            <p style='margin-right:0cm;margin-left:0cm;font-size:16px;font-family:'Tahoma',sans-serif;margin:0cm;'><br></p>
            <p style='margin-right:0cm;margin-left:0cm;font-size:16px;font-family:'Tahoma',sans-serif;margin:0cm;'><br></p>
            <p style='margin-right:0cm;margin-left:0cm;font-size:16px;font-family:'Tahoma',sans-serif;margin:0cm;'><br></p>
            <p id='isPasted' style='margin:0cm;font-size:15px;font-family:'Calibri',sans-serif;'><strong><u><span style='font-size:13px;font-family:'Arial',sans-serif;'>Best Regard</span></u></strong></p>
            <p style='margin:0cm;font-size:15px;font-family:'Calibri',sans-serif;'><strong><span style='font-size:13px;font-family:'Arial',sans-serif;'>Sailom Kaewpeela</span></strong></p>
            <p style='margin:0cm;font-size:15px;font-family:'Calibri',sans-serif;'><strong><span style='font-size:13px;font-family:'Arial',sans-serif;'>Senior System Analyst(IT)</span></strong></p>
            <p style='margin:0cm;font-size:15px;font-family:'Calibri',sans-serif;'><span style='font-size:13px;font-family:'Angsana New',serif;color:#4472C4;'><img width='55' src='data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAoHBwkHBgoJCAkLCwoMDxkQDw4ODx4WFxIZJCAmJSMgIyIoLTkwKCo2KyIjMkQyNjs9QEBAJjBGS0U+Sjk/QD3/2wBDAQsLCw8NDx0QEB09KSMpPT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT3/wAARCAA3ADcDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwB+veINUm128AvrmNUmaNEikKgAHAGB9K0bXw74xu4BKLm5iDDIEt2yt+WePxqrpMSTfEgLIoZftsrYPqCxH6ivXK5KdPnu2z6bH4z6ooU6UFsnqjxjU38Q6NOIb+6v4mP3T9oYq30IODVL+2tU/wCgle/9/wBv8a9K+IsaP4VdmUFklQqfTnH8jXlFZVYuErJno5dVji6PtJQSd7bG5pY8R61KyafdX8u37zfaGCr9STV++0Pxfp9u08lxdyRqMt5N0zED6ZzXceCYI4fCVh5ahTIm9iO7E8mt6t40E43bPGxGbShWcYU42TtseTeEPEGpL4ggikvJp4ZgwZJXLDhSQRnp0oqOwiSH4hyRxqFRLmcKB2GGoootpNDzWnTlVjJRteKf5j9E/wCSlD/r8m/9mr1qvJdE/wCSlD/r8m/9mr1qqw+z9THOv4lP/Cv1OY+If/Ioz/8AXSP/ANCFeS1638Qv+RRuP+ukf/oQrySsMT8Z62Q/7s/V/kj2bwb/AMijpv8A1y/qa26w/Bn/ACKGnf8AXL+prcrsh8KPl8V/Hn6v8zyW1/5KRN/19T/yaii1/wCSkTf9fU/8mornpdfU9rMfip/4V+pWtr1NO8eG6kOI0vn3n0BYgn9a7jxDfMmsxwXr3aWCxrJDFaoxa7kz9zcOmOOOM5rzXWf+Q3qH/XzJ/wChGuz8I+OoobePT9YfZsG2K4PTHYN6fWopzV3Fs7MfhpSjCvCPM0rNdfVf16dzo/ttnr9tc6VqsQhnEQkuId+fKBPy/N03dD7Vz7fC6IT5/tRxb5zgxDdj65x+OK0G8JrcajaTQ3InspQ73spYFrg7w6jjtkY+gxUd5oU1/od9dTWsr3n2x54oGY/MgcDaVzg5Ve/rW0o83xK55dGt7F2oVeVPdWvZ7dfT5K1y/JfwW1tJoWiOIriCIRJK/wDq4nI+VST1Y8njNUfDcVzba0+Ev7eGOA/bku5C6+bkbWRj1yMkkcYq9rHhy2m1CHVFuxp5jj2TOFUEr2wTwrDpu64rmvFvjaO6tW0zSHZomG2W4PVh6L9e5ok+XWXQMPTddezoq/N8TfR9b9H3X/D3xtHuRe+ORcr92aeZx9CrEUVW8K/8jLZ/8D/9Aais6LvFnXm0VGrGK6RX5s2tb8B6xJrF1NZxRzwzSNIreYFIyc4IP1qj/wAID4h/584/+/6f40UVo8PBu5yQzzEwio2Wnk/8yzZ+EvFunnNkrwZ/553SgflnFXjpvj5hg3EuPaeMUUU1QS2bIlm9SbvKnFv0/wCCZ934Q8V37BrxHnI6eZdK2P1qD/hAfEP/AD5x/wDf9P8AGiip+rxfVmkc9xEVZRivk/8AM2PC/gjVLTWorrUESCGEMeJAxYkEY4+tFFFawpxgrI4cTjauJnzz9ND/2Q==' style='height: 0'572in; width: 0'572in;' alt='image'><span style='background:white;'>&nbsp;</span></span></p>
            <p style='margin:0cm;font-size:15px;font-family:'Calibri',sans-serif;'><span style='font-size:13px;font-family:'Arial',sans-serif;color:red;background:white;'>United Foods Public Co, Ltd</span></p>
            <p style='margin:0cm;font-size:15px;font-family:'Calibri',sans-serif;'><span style='font-size:13px;font-family:'Arial',sans-serif;color:black;background:white;'>95 Moo'7 Thakarm Rd' Samaedam&nbsp;</span></p>
            <p style='margin:0cm;font-size:15px;font-family:'Calibri',sans-serif;'><span style='font-size:13px;font-family:'Arial',sans-serif;color:black;background:white;'>Bangkhuntien&nbsp;</span><span style='font-size:13px;font-family:'Arial',sans-serif;'>&nbsp;<span style='color:black;background:white;'>Bangkok 10150 Thailand</span></span></p>
            <p style='margin:0cm;font-size:15px;font-family:'Calibri',sans-serif;'><span style='font-size:13px;font-family:'Arial',sans-serif;color:black;background:white;'>Tel: 061-8579990</span></p>
            <p style='margin:0cm;font-size:15px;font-family:'Calibri',sans-serif;'><span style='font-size:13px;font-family:'Arial',sans-serif;color:black;background:white;'>Fax: -</span></p> ";

            FormHelper.SendMail(subject, bodyHtmlString, "");
        }

        public static void CreateAndSendMailWithAttachFile(string reportName, string branchName, string date)
        {

            string subject = string.Join(" ", new string[] { reportName, " ของ ", branchName, " ประจำวันที่ ", date });
            string bodyHtmlString = @" <p style='margin-right:0cm;margin-left:0cm;font-size:16px;font-family:'Tahoma',sans-serif;margin:0cm;'>
            <span style='font-size:17px;font-family:'Cordia New',sans-serif;'>นี่คือข้อความอัตโนมัติจากระบบ&nbsp;</span>U-Force : &nbsp;<span id='isPasted' style='color: rgb(0, 0, 0); font-family: 'Cordia New', sans-serif; font-size: 17px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;'>ตรวจ</span>
            <span style='font-size:17px;font-family:'Cordia New',sans-serif;'>&nbsp;</span>" + reportName + @"<span style='font-size:17px;font-family:'Cordia New',sans-serif;'>&nbsp;ประจำ &nbsp; " + branchName + @" &nbsp;ของวันที่ &nbsp;" + date + @"</span>&nbsp; 
            <span style='font-size:17px;font-family:'Cordia New',sans-serif;'>โดยมีรายละเอียดตามไฟล์แนบ&nbsp;</span></p>
            <p style='margin-right:0cm;margin-left:0cm;font-size:16px;font-family:'Tahoma',sans-serif;margin:0cm;'><br></p>
            <p style='margin-right:0cm;margin-left:0cm;font-size:16px;font-family:'Tahoma',sans-serif;margin:0cm;'><br></p>
            <p style='margin-right:0cm;margin-left:0cm;font-size:16px;font-family:'Tahoma',sans-serif;margin:0cm;'><br></p>
            <p id='isPasted' style='margin:0cm;font-size:15px;font-family:'Calibri',sans-serif;'><strong><u><span style='font-size:13px;font-family:'Arial',sans-serif;'>Best Regard</span></u></strong></p>
            <p style='margin:0cm;font-size:15px;font-family:'Calibri',sans-serif;'><strong><span style='font-size:13px;font-family:'Arial',sans-serif;'>Sailom Kaewpeela</span></strong></p>
            <p style='margin:0cm;font-size:15px;font-family:'Calibri',sans-serif;'><strong><span style='font-size:13px;font-family:'Arial',sans-serif;'>Senior System Analyst(IT)</span></strong></p>
            <p style='margin:0cm;font-size:15px;font-family:'Calibri',sans-serif;'><span style='font-size:13px;font-family:'Angsana New',serif;color:#4472C4;'><img width='55' src='data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAoHBwkHBgoJCAkLCwoMDxkQDw4ODx4WFxIZJCAmJSMgIyIoLTkwKCo2KyIjMkQyNjs9QEBAJjBGS0U+Sjk/QD3/2wBDAQsLCw8NDx0QEB09KSMpPT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT3/wAARCAA3ADcDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwB+veINUm128AvrmNUmaNEikKgAHAGB9K0bXw74xu4BKLm5iDDIEt2yt+WePxqrpMSTfEgLIoZftsrYPqCxH6ivXK5KdPnu2z6bH4z6ooU6UFsnqjxjU38Q6NOIb+6v4mP3T9oYq30IODVL+2tU/wCgle/9/wBv8a9K+IsaP4VdmUFklQqfTnH8jXlFZVYuErJno5dVji6PtJQSd7bG5pY8R61KyafdX8u37zfaGCr9STV++0Pxfp9u08lxdyRqMt5N0zED6ZzXceCYI4fCVh5ahTIm9iO7E8mt6t40E43bPGxGbShWcYU42TtseTeEPEGpL4ggikvJp4ZgwZJXLDhSQRnp0oqOwiSH4hyRxqFRLmcKB2GGoootpNDzWnTlVjJRteKf5j9E/wCSlD/r8m/9mr1qvJdE/wCSlD/r8m/9mr1qqw+z9THOv4lP/Cv1OY+If/Ioz/8AXSP/ANCFeS1638Qv+RRuP+ukf/oQrySsMT8Z62Q/7s/V/kj2bwb/AMijpv8A1y/qa26w/Bn/ACKGnf8AXL+prcrsh8KPl8V/Hn6v8zyW1/5KRN/19T/yaii1/wCSkTf9fU/8mornpdfU9rMfip/4V+pWtr1NO8eG6kOI0vn3n0BYgn9a7jxDfMmsxwXr3aWCxrJDFaoxa7kz9zcOmOOOM5rzXWf+Q3qH/XzJ/wChGuz8I+OoobePT9YfZsG2K4PTHYN6fWopzV3Fs7MfhpSjCvCPM0rNdfVf16dzo/ttnr9tc6VqsQhnEQkuId+fKBPy/N03dD7Vz7fC6IT5/tRxb5zgxDdj65x+OK0G8JrcajaTQ3InspQ73spYFrg7w6jjtkY+gxUd5oU1/od9dTWsr3n2x54oGY/MgcDaVzg5Ve/rW0o83xK55dGt7F2oVeVPdWvZ7dfT5K1y/JfwW1tJoWiOIriCIRJK/wDq4nI+VST1Y8njNUfDcVzba0+Ev7eGOA/bku5C6+bkbWRj1yMkkcYq9rHhy2m1CHVFuxp5jj2TOFUEr2wTwrDpu64rmvFvjaO6tW0zSHZomG2W4PVh6L9e5ok+XWXQMPTddezoq/N8TfR9b9H3X/D3xtHuRe+ORcr92aeZx9CrEUVW8K/8jLZ/8D/9Aais6LvFnXm0VGrGK6RX5s2tb8B6xJrF1NZxRzwzSNIreYFIyc4IP1qj/wAID4h/584/+/6f40UVo8PBu5yQzzEwio2Wnk/8yzZ+EvFunnNkrwZ/553SgflnFXjpvj5hg3EuPaeMUUU1QS2bIlm9SbvKnFv0/wCCZ934Q8V37BrxHnI6eZdK2P1qD/hAfEP/AD5x/wDf9P8AGiip+rxfVmkc9xEVZRivk/8AM2PC/gjVLTWorrUESCGEMeJAxYkEY4+tFFFawpxgrI4cTjauJnzz9ND/2Q==' style='height: 0'572in; width: 0'572in;' alt='image'><span style='background:white;'>&nbsp;</span></span></p>
            <p style='margin:0cm;font-size:15px;font-family:'Calibri',sans-serif;'><span style='font-size:13px;font-family:'Arial',sans-serif;color:red;background:white;'>United Foods Public Co, Ltd</span></p>
            <p style='margin:0cm;font-size:15px;font-family:'Calibri',sans-serif;'><span style='font-size:13px;font-family:'Arial',sans-serif;color:black;background:white;'>95 Moo'7 Thakarm Rd' Samaedam&nbsp;</span></p>
            <p style='margin:0cm;font-size:15px;font-family:'Calibri',sans-serif;'><span style='font-size:13px;font-family:'Arial',sans-serif;color:black;background:white;'>Bangkhuntien&nbsp;</span><span style='font-size:13px;font-family:'Arial',sans-serif;'>&nbsp;<span style='color:black;background:white;'>Bangkok 10150 Thailand</span></span></p>
            <p style='margin:0cm;font-size:15px;font-family:'Calibri',sans-serif;'><span style='font-size:13px;font-family:'Arial',sans-serif;color:black;background:white;'>Tel: 061-8579990</span></p>
            <p style='margin:0cm;font-size:15px;font-family:'Calibri',sans-serif;'><span style='font-size:13px;font-family:'Arial',sans-serif;color:black;background:white;'>Fax: -</span></p> ";

            //string body = string.Join(" ", new string[] { "ข้อความอัตโนมัติจากระบบ U-Force : พบการบันทึก Bank Note ของ ", bu.tbl_Branchs[0].BranchName, " ประจำวันที่ ", cdate, " โดยมีรายละเอียดตามไฟล์แนบ " });
            //string excelPath = FormHelper.PrintingReportName;

            FormHelper.SendMail(subject, bodyHtmlString, FormHelper.PrintingReportName);
        }

        public static void SendMail(string subject, object body, List<string> attachment_path)
        {
            try
            {
                //string mail_from = ConfigurationManager.AppSettings["mail_from"].ToString();
                //string mail_from_pass = ConfigurationManager.AppSettings["mail_from_pass"].ToString();
                //string mail_to = ""; // ConfigurationManager.AppSettings["mail_to"].ToString();
                //string mail_port = ConfigurationManager.AppSettings["mail_port"].ToString();
                //string mail_host = ConfigurationManager.AppSettings["mail_host"].ToString();

                var conStr = ConfigurationManager.AppSettings["CenterConnect"].ToString();
                var conn = new SqlConnection(conStr);

                string sql = " SELECT * FROM tbl_CfgMailSetting ";
                DataTable dt = new DataTable("MailSetting");
                dt = My_DataTable_Extensions.GetDataTable(sql, conn, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var r = dt.Rows[0];
                    string mail_to = "";
                    string mail_from = r["MailFrom"].ToString();
                    string mail_from_pass = r["MailFromPass"].ToString();
                    string mail_port = r["MailPort"].ToString();
                    string mail_host = r["MailHost"].ToString();

                    sql = " SELECT MailAddress FROM tbl_CfgMailAddress WHERE [Active] = 1 ";
                    dt = new DataTable("MailAddess");
                    dt = My_DataTable_Extensions.GetDataTable(sql, conn, CommandType.Text);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (!string.IsNullOrEmpty(dr["MailAddress"].ToString()))
                            {
                                mail_to = dr["MailAddress"].ToString();

                                SmtpClient smtp = new SmtpClient();
                                smtp.Port = Convert.ToInt32(mail_port);
                                smtp.Host = mail_host;
                                smtp.EnableSsl = true;
                                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                smtp.UseDefaultCredentials = false;
                                smtp.Credentials = new NetworkCredential(mail_from, mail_from_pass);

                                MailMessage mail = new MailMessage();
                                mail.From = new MailAddress(mail_from);
                                mail.To.Add(mail_to);
                                mail.Subject = subject;
                                mail.Body = body.ToString();
                                mail.IsBodyHtml = true;

                                //if (!string.IsNullOrEmpty(attachment_path))
                                if (attachment_path.Count > 0)
                                {
                                    foreach (var item in attachment_path)
                                    {
                                        Attachment attachment = new Attachment(item, MediaTypeNames.Application.Octet);
                                        ContentDisposition disposition = attachment.ContentDisposition;
                                        disposition.CreationDate = File.GetCreationTime(item);
                                        disposition.ModificationDate = File.GetLastWriteTime(item);
                                        disposition.ReadDate = File.GetLastAccessTime(item);
                                        disposition.FileName = Path.GetFileName(item);
                                        disposition.Size = new FileInfo(item).Length;
                                        disposition.DispositionType = DispositionTypeNames.Attachment;
                                        mail.Attachments.Add(attachment);
                                    }

                                }

                                smtp.Send(mail);
                            }
                        }
                    }
                }

                var result = "ส่งเมลล์เรียบร้อยแล้ว!!";
                result.ShowInfoMessage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("err: " + ex.Message);
            }
        }

        public static void SendMail(string subject, object body, string attachment_path = "")
        {
            try
            {
                //string mail_from = ConfigurationManager.AppSettings["mail_from"].ToString();
                //string mail_from_pass = ConfigurationManager.AppSettings["mail_from_pass"].ToString();
                //string mail_to = ""; // ConfigurationManager.AppSettings["mail_to"].ToString();
                //string mail_port = ConfigurationManager.AppSettings["mail_port"].ToString();
                //string mail_host = ConfigurationManager.AppSettings["mail_host"].ToString();

                var conStr = ConfigurationManager.AppSettings["CenterConnect"].ToString();
                var conn = new SqlConnection(conStr);

                string sql = " SELECT * FROM tbl_CfgMailSetting ";
                DataTable dt = new DataTable("MailSetting");
                dt = My_DataTable_Extensions.GetDataTable(sql, conn, CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var r = dt.Rows[0];
                    string mail_to = "";
                    string mail_from = r["MailFrom"].ToString();
                    string mail_from_pass = r["MailFromPass"].ToString();
                    string mail_port = r["MailPort"].ToString();
                    string mail_host = r["MailHost"].ToString();

                    sql = " SELECT MailAddress FROM tbl_CfgMailAddress WHERE [Active] = 1 ";
                    dt = new DataTable("MailAddess");
                    dt = My_DataTable_Extensions.GetDataTable(sql, conn, CommandType.Text);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (!string.IsNullOrEmpty(dr["MailAddress"].ToString()))
                            {
                                mail_to = dr["MailAddress"].ToString();

                                SmtpClient smtp = new SmtpClient();
                                smtp.Port = Convert.ToInt32(mail_port);
                                smtp.Host = mail_host;
                                smtp.EnableSsl = true;
                                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                smtp.UseDefaultCredentials = false;
                                smtp.Credentials = new NetworkCredential(mail_from, mail_from_pass);

                                MailMessage mail = new MailMessage();
                                mail.From = new MailAddress(mail_from);
                                mail.To.Add(mail_to);
                                mail.Subject = subject;
                                mail.Body = body.ToString();
                                mail.IsBodyHtml = true;

                                if (!string.IsNullOrEmpty(attachment_path))
                                {
                                    Attachment attachment = new Attachment(attachment_path, MediaTypeNames.Application.Octet);
                                    ContentDisposition disposition = attachment.ContentDisposition;
                                    disposition.CreationDate = File.GetCreationTime(attachment_path);
                                    disposition.ModificationDate = File.GetLastWriteTime(attachment_path);
                                    disposition.ReadDate = File.GetLastAccessTime(attachment_path);
                                    disposition.FileName = Path.GetFileName(attachment_path);
                                    disposition.Size = new FileInfo(attachment_path).Length;
                                    disposition.DispositionType = DispositionTypeNames.Attachment;
                                    mail.Attachments.Add(attachment);
                                }

                                smtp.Send(mail);
                            }
                        }
                    }
                }

                var result = "ส่งเมลล์เรียบร้อยแล้ว!!";
                result.ShowInfoMessage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("err: " + ex.Message);
            }
        }

        private static void SendCompletedCallBack(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show(string.Format("{0} send canceled.", e.UserState), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (e.Error != null)
            {
                MessageBox.Show(string.Format("{0} {1}", e.UserState, e.Error), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("your message is sended", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public static void ClearListBoxItem(this ListBox ltbs)
        {
            if (ltbs.Items.Count > 0)
            {
                ltbs.Items.Clear();
            }
        }

        public static void CheckOverStok1000(DateTime datetime)
        {
            //Check Over Stock in main stock(1000)---------------------------
            var dtOverStck = odBU.GetOverStockData(datetime);
            if (dtOverStck != null && dtOverStck.Rows.Count > 0)
            {
                string msgError = "";
                foreach (DataRow row in dtOverStck.Rows)
                {
                    msgError += "แวน : " + row["WHID"].ToString() + " สินค้า : " + row["ProductID"].ToString() + ":" + row["ProductShortName"].ToString() + " ใน Stock เหลือ "
                        + Convert.ToDecimal(row["Diff"]).ToStringN2() + " (หน่วยเล็ก) \n"; // "กรุณาเลือกตลาดที่ต้องการย้ายลูกค้าไป\n";
                }

                if (msgError != "")
                {
                    FlexibleMessageBox.Show(msgError, "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
            //Check Over Stock in main stock(1000)---------------------------
        }

        public static bool CheckOverStok1000PreOrder(DateTime datetime)
        {
            //Check Over Stock in main stock(1000)---------------------------
            bool isOverStock = false;
            var dtOverStck = odBU.GetOverStockPreOrderData(datetime);
            if (dtOverStck != null && dtOverStck.Rows.Count > 0)
            {
                string msgError = "";
                foreach (DataRow row in dtOverStck.Rows)
                {
                    msgError += "แวน : " + row["WHID"].ToString() + " สินค้า : " + row["ProductID"].ToString() + ":" + row["ProductShortName"].ToString() + " ใน Stock เหลือ "
                        + Convert.ToDecimal(row["Diff"]).ToStringN2() + " (หน่วยเล็ก) \n"; // "กรุณาเลือกตลาดที่ต้องการย้ายลูกค้าไป\n";

                    isOverStock = true;
                }

                if (msgError != "")
                {
                    FlexibleMessageBox.Show(msgError, "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }

            return isOverStock;
            //Check Over Stock in main stock(1000)---------------------------
        }

        public static int GetMinUOM(this List<tbl_ProductUomSet> allUomSet, tbl_PODetail poDt)
        {
            //if (poDt.OrderUom != 2) //06042021 by sailom.k
            var minUomID = allUomSet.GetMinUOM(poDt.ProductID);

            return minUomID;
        }

        public static int GetMinUOM(this List<tbl_ProductUomSet> allUomSet, tbl_PRDetail prDt)
        {
            //if (poDt.OrderUom != 2) //06042021 by sailom.k
            var minUomID = allUomSet.GetMinUOM(prDt.ProductID);

            return minUomID;
        }

        public static int GetMinUOM(this List<tbl_ProductUomSet> allUomSet, tbl_PODetail_PRE poDt)
        {
            //if (poDt.OrderUom != 2) //06042021 by sailom.k
            var minUomID = allUomSet.GetMinUOM(poDt.ProductID);

            return minUomID;
        }

        public static int GetMinUOM(this List<tbl_ProductUomSet> allUomSet, string productID)
        {
            //if (poDt.OrderUom != 2) //06042021 by sailom.k
            var minUomID = 2;

            var minUom = allUomSet.Where(x => x.ProductID == productID).ToList();
            if (minUom.Count > 0)
            {
                var minItem = minUom.OrderBy(x => x.BaseQty).First();
                if (minItem != null)
                {
                    minUomID = minItem.UomSetID;
                }
            }

            return minUomID;
        }

        public static List<tbl_ProductUomSet> GetProductUOMSet(this BaseControl bu, List<tbl_ProductUomSet> obj, string productID)
        {
            //List<tbl_ProductUomSet> ret = new List<tbl_ProductUomSet>();
            //var prd = bu.GetProduct().FirstOrDefault(x => x.ProductID == productID);
            //if (prd != null)
            //{
            //    var saleUom = bu.GetUOMSet(x => x.ProductID == prd.ProductID && x.UomSetID == prd.PurchaseUomID);
            //    if (saleUom != null)
            //        ret = saleUom; //25012021
            //}

            List<tbl_ProductUomSet> ret = new List<tbl_ProductUomSet>();
            //Func<tbl_ProductUomSet, bool> condition = (x => x.BaseUomID == 2 && x.UomSetID == 1 && x.ProductID == productID);
            int maxBaseQty = obj.Where(x => x.ProductID == productID).Max(a => a.BaseQty); //; bu.GetUOMSet(x => x.ProductID == productID).Max(a => a.BaseQty);
            ret = obj.Where(x => x.ProductID == productID && x.BaseQty == maxBaseQty).ToList();// bu.GetUOMSet(x => x.ProductID == productID && x.BaseQty == maxBaseQty);

            return ret;
        }

        public static void SetColumnStyle(this DataGridViewColumn col, int width, DataGridViewAutoSizeColumnMode sizeMod, DataGridViewContentAlignment alignment, string format = "", int minWidth = 0, DataGridViewColumn colType = null)
        {
            try
            {
                col.Width = width;
                col.AutoSizeMode = sizeMod;
                col.DefaultCellStyle.Alignment = alignment;

                if (!string.IsNullOrEmpty(format))
                    col.DefaultCellStyle.Format = format;
                if (minWidth != 0)
                    col.MinimumWidth = 120;

                col.ReadOnly = true;
                col.DefaultCellStyle.Font = new Font("Tahoma", 9.5F, GraphicsUnit.Point);

                if (colType != null)
                {
                    col = colType;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void BindStockOnHand(this TextBox txt, List<tbl_ProductUomSet> allUOMSet, BaseControl bu, string whid, string productID, decimal orderQty, TextBox qtyText = null, Label uomType = null)
        {
            //var whid = txtBranchCode.Text + txtFromWHCode.Text;

            //string productID = productDT.Rows[0]["ProductID"].ToString();

            var invWhItem = bu.GetStockMovement(productID, whid); // bu.GetInvWarehouse(productID, whid); //15-05-2021 by sailom.k
            if (invWhItem != null && invWhItem.Count > 0)
            {
                txt.Text = invWhItem.Sum(x => x.TrnQty).ToStringN0();  //invWhItem[0].QtyOnHand.ToStringN0(); //15-05-2021 by sailom.k
            }
            else
                txt.Text = "0";

            if (qtyText != null)
            {
                qtyText.Text = orderQty.ToStringN0();
            }
            if (uomType != null)
            {
                var minUomID = 2;
                minUomID = allUOMSet.GetMinUOM(productID);

                Func<tbl_ProductUomSet, bool> tbl_ProductUomSetPre = (x => x.ProductID == productID && x.UomSetID == minUomID);
                var prodUomSetPack = allUOMSet.Where(tbl_ProductUomSetPre).ToList();
                if (prodUomSetPack != null && prodUomSetPack.Count > 0)
                    uomType.Text = prodUomSetPack[0].UomSetName;
                else
                {
                    Func<tbl_ProductUomSet, bool> tbl_ProductUomSetPreOriginal = (x => x.ProductID == productID);
                    var prodUomSetOri = allUOMSet.Where(tbl_ProductUomSetPreOriginal).ToList();
                    if (prodUomSetOri.Count > 0)
                        uomType.Text = prodUomSetOri[0].UomSetName;
                }
            }
        }

        public static void BindDropdown3DocStatus(this ComboBox ddl, BaseControl bu, string selDocStatus = "")
        {
            if (ddl.DataSource == null)
            {
                var allDocStatus = bu.GetDocStatus().Where(x => x.DocStatusCode == "3" || x.DocStatusCode == "4" || x.DocStatusCode == "5").ToList();
                ddl.BindDropdownList(allDocStatus, "DocStatusName", "DocStatusCode", 0);
            }

            if (!string.IsNullOrEmpty(selDocStatus))
            {
                Predicate<tbl_DocumentStatus> condition = delegate (tbl_DocumentStatus x) { return x.DocStatusCode == selDocStatus; };
                ddl.SelectedValueDropdownList(condition);
            }

        }

        public static void BindDropdownDocStatus(this ComboBox ddl, BaseControl bu, string selDocStatus = "")
        {
            if (ddl.DataSource == null)
            {
                var allDocStatus = bu.GetDocStatus().Where(x => x.DocStatusCode == "4" || x.DocStatusCode == "5").ToList();
                ddl.BindDropdownList(allDocStatus, "DocStatusName", "DocStatusCode", 0);
            }

            if (!string.IsNullOrEmpty(selDocStatus))
            {
                Predicate<tbl_DocumentStatus> condition = delegate (tbl_DocumentStatus x) { return x.DocStatusCode == selDocStatus; };
                ddl.SelectedValueDropdownList(condition);
            }

        }

        public static void SetTitleForm(this Form form)
        {
            string version = ConfigurationManager.AppSettings["Version"];
            form.Text = version;
        }

        public static void SetPerformClick(Button btnSearch, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        public static Form GetFormByName(this string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            foreach (Type type in myAssembly.GetTypes())
            {
                if (type.BaseType != null && type.BaseType.FullName == "System.Windows.Forms.Form")
                {
                    if (type.FullName.Split('.').Last().ToLower() == name.ToLower())
                    {
                        var form = Activator.CreateInstance(type) as Form;
                        return form;
                    }
                }
            }
            return null;
        }

        public static void ClearTextBox(this TextBox txt)
        {
            txt.Text = string.Empty;
            txt.BackColor = Color.White;
        }

        public static void ClearTextBox(this MaskedTextBox txt)
        {
            txt.Text = string.Empty;
            txt.BackColor = Color.White;
        }

        public static void DisableTextBox(this TextBox txt, bool mode)
        {
            txt.ReadOnly = mode;
            txt.BackColor = txt.ReadOnly ? ColorTranslator.FromHtml("#DCDCDC") : Color.White;
        }

        public static void DisableTextBox(this MaskedTextBox txt, bool mode)
        {
            txt.ReadOnly = mode;
            txt.BackColor = txt.ReadOnly ? ColorTranslator.FromHtml("#DCDCDC") : Color.White;
        }

        public static void DisableTextBox(this ListBox txt, bool mode)
        {
            txt.Enabled = !mode;
            txt.BackColor = txt.Enabled ? Color.White : ColorTranslator.FromHtml("#DCDCDC");
        }

        public static void DefaultNumber(this TextBox txt)
        {
            txt.Text = string.Empty;
            txt.BackColor = Color.White;
            txt.Text = string.Format("{0:#,0.00}", 0);
            txt.TextAlign = HorizontalAlignment.Right;
        }

        public static void SetErrMessageList(this List<string> errList, Control ctrl, Label lbl = null)
        {
            if (ctrl is TextBox)
            {
                var txt = ctrl as TextBox;

                if (txt != null && string.IsNullOrEmpty(txt.Text))
                {
                    if (lbl != null)
                    {
                        string t = lbl.Text;
                        errList.Add(string.Format("--> {0}", t));
                        txt.ErrorTextBox();
                    }
                    else
                    {
                        string t = txt.Name.Substring(3, txt.Name.Length - 3);
                        errList.Add(string.Format("--> {0}", t));
                        txt.ErrorTextBox();
                    }
                }
            }
            else if (ctrl is ComboBox)
            {
                var ddl = ctrl as ComboBox;
                if (ddl.Items != null && ddl.Items.Count > 0)
                {
                    if (ddl.SelectedValue != null && ddl.SelectedValue.ToString() == "-1")
                    {
                        if (lbl != null)
                        {
                            string t = lbl.Text;
                            errList.Add(string.Format("--> {0}", t));
                            ddl.ErrorTextBox();
                        }
                    }
                }
                if (ddl.SelectedIndex == -1) //
                {
                    if (lbl != null)
                    {
                        string t = lbl.Text;
                        errList.Add(string.Format("--> {0}", t));
                        ddl.ErrorTextBox();
                    }
                }

            }

            else if (ctrl is MaskedTextBox)//new
            {
                var mtb = ctrl as MaskedTextBox;
                if (mtb.MaskCompleted == false)
                {
                    if (lbl != null)
                    {
                        string t = lbl.Text;
                        errList.Add(string.Format("--> {0}", t));
                        mtb.ErrorMaskedTextBox();
                    }
                }
            }

            else if (ctrl is ListBox)//new
            {
                var lsb = ctrl as ListBox;
                if (lsb.Items.Count == 0)
                {
                    if (lbl != null)
                    {
                        string t = lbl.Text;
                        errList.Add(string.Format("--> {0}", t));
                        lsb.ErrorListBox();
                    }
                }
            }
        }

        public static void ErrorListBox(this ListBox lsb)//new
        {
            lsb.BackColor = Color.MistyRose;
            lsb.TextChanged += Lsb_TextChanged;//new
        }

        public static void ErrorMaskedTextBox(this MaskedTextBox mtb)//new
        {
            mtb.BackColor = Color.MistyRose;
            mtb.TextChanged += Mtb_TextChanged;//new
        }

        public static void ErrorTextBox(this TextBox txt)
        {
            txt.BackColor = Color.MistyRose;
            txt.TextChanged += Txt_TextChanged;
        }

        public static void ErrorTextBox(this ComboBox ddl)
        {
            ddl.BackColor = Color.MistyRose;
            ddl.SelectedIndexChanged += Ddl_SelectedIndexChanged;
        }
        private static void Lsb_TextChanged(object sender, EventArgs e)//new
        {
            ((ListBox)sender).BackColor = Color.White;
        }

        private static void Mtb_TextChanged(object sender, EventArgs e)//new
        {
            ((MaskedTextBox)sender).BackColor = Color.White;
        }

        private static void Txt_TextChanged(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.White;
        }

        private static void Ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((ComboBox)sender).BackColor = Color.White;
        }

        public static void SetImageButtonDesigner(this Button button, Bitmap image)
        {

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ButtonCtrl));
            button.Anchor = System.Windows.Forms.AnchorStyles.None;
            button.BackColor = System.Drawing.Color.Azure;
            button.Cursor = System.Windows.Forms.Cursors.Hand;
            button.FlatAppearance.BorderSize = 0;
            button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            button.ForeColor = System.Drawing.Color.Black;
            button.Image = new Bitmap(image);// ((System.Drawing.Image)(resources.GetObject("Image")));
            button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button.Location = new System.Drawing.Point(319, 3);
            button.Size = new System.Drawing.Size(75, 23);
            button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            button.UseVisualStyleBackColor = false;
        }

        public static void BindDropdownList<T>(this ComboBox ddl, List<T> obj, string displayMember, string valueMember, int? defaultSelectedIndex = null, Predicate<T> selectValue = null)
        {
            ddl.DataSource = obj;
            ddl.DisplayMember = displayMember;
            ddl.ValueMember = valueMember;

            if (defaultSelectedIndex != null)
            {
                ddl.SelectedIndex = defaultSelectedIndex.Value;
            }
            if (selectValue != null)
            {
                ddl.SelectedValueDropdownList(selectValue);
            }
        }

        public static void BindDropdownList<T>(this ComboBox ddl, Dictionary<T, T> obj, string displayMember, string valueMember, int? defaultSelectedIndex = null, Predicate<T> selectValue = null)
        {
            ddl.DataSource = obj.ToList();
            ddl.DisplayMember = displayMember;
            ddl.ValueMember = valueMember;

            if (defaultSelectedIndex != null)
            {
                ddl.SelectedIndex = defaultSelectedIndex.Value;
            }
            if (selectValue != null)
            {
                ddl.SelectedValueDropdownList(selectValue);
            }
        }

        public static DialogResult ShowWarningMessage(this string msg)
        {
            return FlexibleMessageBox.Show(msg, "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult ShowErrorMessage(this string msg)
        {
            return FlexibleMessageBox.Show(msg, "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult ShowInfoMessage(this string msg)
        {
            return FlexibleMessageBox.Show(msg, "ข้อความ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void SelectedValueDropdownList<T>(this ComboBox ddl, Predicate<T> selectValue)
        {
            //Predicate<Program> test = delegate (Program p) { return p.age > 3; };
            var ddlDataSource = ((List<T>)ddl.DataSource);
            ddl.SelectedItem = ddlDataSource.Find(selectValue);

        }

        public static void EnableButton(this AddButton btnAdd, EditButton btnEdit, RemoveButton btnRemove,
            SaveButton btnSave, CancelButton btnCancel, CopyButton btnCopy, PrintButton btnPrint, string conditionText = "")
        {
            btnAdd.Enabled = false;

            EnableSubButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, conditionText);
        }

        public static void EnableButton(this AddButton btnAdd, EditButton btnEdit, RemoveButton btnRemove,
            SaveButton btnSave, CancelButton btnCancel, CopyButton btnCopy, string conditionText = "")
        {
            btnAdd.Enabled = false;

            EnableSubButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, conditionText);
        }

        public static void EnableButton(this EditButton btnEdit, RemoveButton btnRemove, SaveButton btnSave, CancelButton btnCancel,
            AddButton btnAdd, CopyButton btnCopy, PrintButton btnPrint, string conditionText = "")
        {
            EnableSubButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, conditionText);

            btnSave.Enabled = true;

            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
        }

        public static void EnableButton(this EditButton btnEdit, RemoveButton btnRemove, SaveButton btnSave, CancelButton btnCancel,
            AddButton btnAdd, CopyButton btnCopy, string conditionText = "")
        {
            EnableSubButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, conditionText);

            btnSave.Enabled = true;

            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
        }

        public static void EnableButton(this SaveButton btnSave, EditButton btnEdit, RemoveButton btnRemove, CancelButton btnCancel,
            AddButton btnAdd, CopyButton btnCopy, PrintButton btnPrint, ExcelButton btnExcel, string conditionText = "")
        {
            btnAdd.Enabled = true;

            EnableSubButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, conditionText);

            btnEdit.Enabled = true;
            btnRemove.Enabled = false;
            btnCopy.Enabled = true;
            btnPrint.Enabled = true;
            btnExcel.Enabled = true; //last by sailom .k 12/01/2022

            btnSave.Enabled = false;
            btnCancel.Enabled = true;
        }

        public static void EnableButton(this SaveButton btnSave, EditButton btnEdit, RemoveButton btnRemove, CancelButton btnCancel,
            AddButton btnAdd, CopyButton btnCopy, ExcelButton btnExcel, string conditionText = "")
        {
            btnAdd.Enabled = true;

            EnableSubButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, conditionText);

            btnEdit.Enabled = true;
            btnRemove.Enabled = false;
            btnCopy.Enabled = true;
            btnExcel.Enabled = true;//last by sailom .k 12/01/2022

            btnSave.Enabled = false;
            btnCancel.Enabled = true;
        }

        public static void EnableButton(this CopyButton btnCopy, EditButton btnEdit, RemoveButton btnRemove, SaveButton btnSave, CancelButton btnCancel,
            AddButton btnAdd, PrintButton btnPrint, string conditionText = "")
        {
            EnableSubButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, btnPrint, conditionText);

            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnCopy.Enabled = false;
        }

        public static void EnableButton(this CopyButton btnCopy, EditButton btnEdit, RemoveButton btnRemove, SaveButton btnSave, CancelButton btnCancel,
            AddButton btnAdd, string conditionText = "")
        {
            EnableSubButton(btnEdit, btnRemove, btnSave, btnCancel, btnAdd, btnCopy, conditionText);

            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnCopy.Enabled = false;
        }

        public static void EnableButton(this CancelButton btnCancel, Button btnSearchDoc)
        {
            btnSearchDoc.Enabled = true;
        }

        public static void EnableButton(this Button btnSearchDoc, AddButton btnAdd, EditButton btnEdit, RemoveButton btnRemove,
            SaveButton btnSave, CancelButton btnCancel, CopyButton btnCopy, PrintButton btnPrint, ExcelButton btnExcel)
        {
            btnSearchDoc.Enabled = true;
            btnEdit.Enabled = true;
            btnCopy.Enabled = true;
            btnPrint.Enabled = true;
            btnExcel.Enabled = true;//last by sailom .k 12/01/2022
            btnCancel.Enabled = true;

            btnSave.Enabled = false;
            btnRemove.Enabled = false;
            btnAdd.Enabled = false;
        }

        public static void EnableButton(this Button btnSearchDoc, AddButton btnAdd, EditButton btnEdit, RemoveButton btnRemove,
            SaveButton btnSave, CancelButton btnCancel, CopyButton btnCopy, ExcelButton btnExcel)
        {
            btnSearchDoc.Enabled = true;
            btnEdit.Enabled = true;
            btnCopy.Enabled = true;
            btnExcel.Enabled = true;//last by sailom .k 12/01/2022
            btnCancel.Enabled = true;

            btnSave.Enabled = false;
            btnRemove.Enabled = false;
            btnAdd.Enabled = false;
        }

        private static void EnableSubButton(EditButton btnEdit, RemoveButton btnRemove,
        SaveButton btnSave, CancelButton btnCancel, AddButton btnAdd, CopyButton btnCopy, PrintButton btnPrint, string conditionText = "")
        {
            btnEdit.Enabled = !string.IsNullOrEmpty(conditionText);
            btnRemove.Enabled = false;
            btnSave.Enabled = !btnEdit.Enabled && !btnAdd.Enabled;
            btnCancel.Enabled = !btnAdd.Enabled;
            btnAdd.Enabled = !btnSave.Enabled && !btnEdit.Enabled;

            btnCopy.Enabled = false;
            btnPrint.Enabled = false;
        }

        private static void EnableSubButton(EditButton btnEdit, RemoveButton btnRemove,
        SaveButton btnSave, CancelButton btnCancel, AddButton btnAdd, CopyButton btnCopy, string conditionText = "")
        {
            btnEdit.Enabled = !string.IsNullOrEmpty(conditionText);
            btnRemove.Enabled = false;
            btnSave.Enabled = !btnEdit.Enabled && !btnAdd.Enabled;
            btnCancel.Enabled = !btnAdd.Enabled;
            btnAdd.Enabled = !btnSave.Enabled && !btnEdit.Enabled;

            btnCopy.Enabled = false;
        }

        public static void ShowProcessErr(this Form frm)
        {
            string msg = "เกิดข้อผิดพลาด ไม่สามารถบันทึกข้อมูลได้ กรุณาติดต่อ IT United Foods!";
            msg.ShowErrorMessage();
        }

        public static void ShowDocNoProcessErr(this Form frm)
        {
            string msg = "การสร้างเลขที่เอกสารผิดพลาด กรุณาติดต่อ IT United Foods!";
            msg.ShowErrorMessage();
        }

        public static void SetDefaultGridViewEvent(this Form frm, DataGridView grd, DataGridViewCellEventHandler ev1, DataGridViewCellEventHandler ev2, DataGridViewCellValidatingEventHandler ev3,
            DataGridViewCellEventHandler ev4, DataGridViewEditingControlShowingEventHandler ev5, DataGridViewRowPostPaintEventHandler ev6, DataGridViewRowCancelEventHandler ev7, KeyEventHandler ev8, DataGridViewCellEventHandler ev9)
        {
            grd.CellContentClick -= new System.Windows.Forms.DataGridViewCellEventHandler(ev1);
            grd.CellEndEdit -= new System.Windows.Forms.DataGridViewCellEventHandler(ev2);
            grd.CellValidating -= new System.Windows.Forms.DataGridViewCellValidatingEventHandler(ev3);
            grd.CellValueChanged -= new System.Windows.Forms.DataGridViewCellEventHandler(ev4);
            grd.EditingControlShowing -= new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(ev5);
            grd.RowPostPaint -= new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(ev6);
            grd.UserDeletingRow -= new System.Windows.Forms.DataGridViewRowCancelEventHandler(ev7);
            grd.KeyDown -= new System.Windows.Forms.KeyEventHandler(ev8);
            grd.CellClick -= new System.Windows.Forms.DataGridViewCellEventHandler(ev9);

            grd.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(ev1);
            grd.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(ev2);
            grd.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(ev3);
            grd.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(ev4);
            grd.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(ev5);
            grd.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(ev6);
            grd.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(ev7);
            grd.KeyDown += new System.Windows.Forms.KeyEventHandler(ev8);
            grd.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(ev9);
        }

        /// <summary>
        /// Pop up Supplier form creater
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="_controls"></param>
        /// <param name="popupName"></param>
        public static void OpenSupplierPopup(this Form frm, List<Control> _controls, string popupName)
        {

            frmSearchSupp _frm = new frmSearchSupp();

            List<DataGridColumn> colList = new List<DataGridColumn>();
            colList.Add(new DataGridColumn() { DataPropertyName = "SupplierCode", HeaderText = "รหัสผู้จำหน่าย", Name = "SupplierCode", Width = 120, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, AddNumberInFirstRow = true, AddSearchAddOn = false });
            colList.Add(new DataGridColumn() { DataPropertyName = "SuppName", HeaderText = "ชื่อผู้จำหน่าย", Name = "SuppName", Width = 160, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.Fill });
            colList.Add(new DataGridColumn() { DataPropertyName = "SupplierRefCode", HeaderText = "รหัสเดิม", Name = "SupplierRefCode", Width = 80, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "ApSupplierTypeName", HeaderText = "กลุ่มผู้จำหน่าย", Name = "ApSupplierTypeName", Width = 120, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });

            _frm.PreparePopupForm("Supplier", frm.Name, popupName, colList, null, _controls, null);
            _frm.ShowDialog();
        }

        /// <summary>
        /// Pop up Promotion Temp form creater
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="_controls"></param>
        /// <param name="popupName"></param>
        public static void OpenPromotionTempPopup(this Form frm, List<Control> _controls, string popupName)
        {

            frmPromotion _frm = new frmPromotion();

            List<DataGridColumn> colList = new List<DataGridColumn>();
            AddPromotionPopupCols(_frm, colList);

            _frm.PreparePopupForm("PromotionTemp", frm.Name, popupName, colList, null, _controls, null);
            _frm.ShowDialog();
        }

        /// <summary>
        /// Pop up Promotion form creater
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="_controls"></param>
        /// <param name="popupName"></param>
        public static void OpenPromotionPopup(this Form frm, List<Control> _controls, string popupName)
        {

            frmPromotion _frm = new frmPromotion();

            List<DataGridColumn> colList = new List<DataGridColumn>();
            //AddPromotionPopupCols(_frm, colList);
            _frm.PreparePopupForm("Promotion", frm.Name, popupName, colList, null, _controls, null);
            _frm.ShowDialog();
        }

        public static void AddPromotionPopupCols(frmPromotion _frm, List<DataGridColumn> colList)
        {
            //colList.Add(new DataGridColumn() { DataPropertyName = "DocNo", HeaderText = "เลขที่เอกสาร", Name = "DocNo", Width = 80, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "Choose", HeaderText = "เลือก", Name = "Choose", Width = 50, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleCenter, ColoumnType = new DataGridViewCheckBoxColumn() });
            colList.Add(new DataGridColumn() { DataPropertyName = "No", HeaderText = "ลำดับ", Name = "No", Width = 50, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleCenter });
            colList.Add(new DataGridColumn() { DataPropertyName = "PromotionID", HeaderText = "รหัสโปรโมชั่น", Name = "PromotionID", Width = 80, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "PromotionName", HeaderText = "ชื่อโปรโมชั่น", Name = "PromotionName", Width = 150, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.Fill });
            //colList.Add(new DataGridColumn() { DataPropertyName = "SKUGroupID", HeaderText = "กลุ่มสินค้า", Name = "SKUGroupID", Width = 80, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "RewardName", HeaderText = "ชื่อส่วนลด", Name = "RewardName", Width = 150, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "DisCountAmt", HeaderText = "ส่วนลด", Name = "DisCountAmt", Width = 100, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" });
            //colList.Add(new DataGridColumn() { DataPropertyName = "SKUGroupRewardID", HeaderText = "สินค้าที่แจก", Name = "SKUGroupRewardID", Width = 80, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "SKUGroupRewardAmt", HeaderText = "จำนวนสินค้าที่แจก", Name = "SKUGroupRewardAmt", Width = 60, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" });
            colList.Add(new DataGridColumn() { DataPropertyName = "EffectiveDate", HeaderText = "วันที่เริ่มโปรโมชั่น", Name = "EffectiveDate", Width = 100, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "dd/MM/yyyy" });
            colList.Add(new DataGridColumn() { DataPropertyName = "ExpireDate", HeaderText = "วันที่สิ้นสุดโปรโมชั่น", Name = "ExpireDate", Width = 100, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "dd/MM/yyyy" });
        }

        /// <summary>
        /// Pop up Customer form creater
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="_controls"></param>
        /// <param name="popupName"></param>
        public static void OpenCustomerPopup(this Form frm, List<Control> _controls, string popupName, Func<tbl_ArCustomer, bool> predicate = null)
        {
            frmSearchSupp _frm = new frmSearchSupp();

            List<DataGridColumn> colList = new List<DataGridColumn>();
            colList.Add(new DataGridColumn() { DataPropertyName = "CustomerID", HeaderText = "รหัสลูกค้า", Name = "CustomerID", Width = 120, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, AddNumberInFirstRow = true, AddSearchAddOn = false });
            colList.Add(new DataGridColumn() { DataPropertyName = "CustName", HeaderText = "ชื่อลูกค้า", Name = "CustName", Width = 120, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.Fill });
            colList.Add(new DataGridColumn() { DataPropertyName = "CustomerRefCode", HeaderText = "รหัสเดิม", Name = "CustomerRefCode", Width = 80, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "ShopTypeName", HeaderText = "ประเภทร้านค้า", Name = "ShopTypeName", Width = 120, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "SalAreaName", HeaderText = "ตลาด", Name = "SalAreaName", Width = 100, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "WHID", HeaderText = "คลัง Van", Name = "WHID", Width = 100, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "Seq", HeaderText = "ลำดับ", Name = "Seq", Width = 60, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" });
            colList.Add(new DataGridColumn() { ColoumnType = new DataGridViewCheckBoxColumn(), DataPropertyName = "FlagMember", HeaderText = "สมาชิก", Name = "FlagMember", Width = 80, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleCenter });
            colList.Add(new DataGridColumn() { DataPropertyName = "CreditDay", HeaderText = "เครดิต(วัน)", Name = "CreditDay", Width = 80, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" });

            if (predicate != null)
            {
                _frm.PreparePopupFormWithPredicate("Customer", frm.Name, popupName, colList, null, _controls, predicate);
                //_frm.PreparePopupForm("Customer", frm.Name, popupName, colList, null, null);
            }
            else
                _frm.PreparePopupForm("Customer", frm.Name, popupName, colList, null, _controls, null);

            _frm.ShowDialog();
        }

        public static void OpenCustomerPrePopup(this Form frm, List<Control> _controls, string popupName, Func<tbl_ArCustomer, bool> predicate = null)
        {
            frmSearchSupp _frm = new frmSearchSupp();

            List<DataGridColumn> colList = new List<DataGridColumn>();
            colList.Add(new DataGridColumn() { DataPropertyName = "CustomerID", HeaderText = "รหัสลูกค้า", Name = "CustomerID", Width = 120, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, AddNumberInFirstRow = true, AddSearchAddOn = false });
            colList.Add(new DataGridColumn() { DataPropertyName = "CustName", HeaderText = "ชื่อลูกค้า", Name = "CustName", Width = 120, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.Fill });
            colList.Add(new DataGridColumn() { DataPropertyName = "CustomerRefCode", HeaderText = "รหัสเดิม", Name = "CustomerRefCode", Width = 80, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "ShopTypeName", HeaderText = "ประเภทร้านค้า", Name = "ShopTypeName", Width = 120, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "SalAreaName", HeaderText = "ตลาด", Name = "SalAreaName", Width = 100, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "WHID", HeaderText = "คลัง Van", Name = "WHID", Width = 100, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "Seq", HeaderText = "ลำดับ", Name = "Seq", Width = 60, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" });
            colList.Add(new DataGridColumn() { ColoumnType = new DataGridViewCheckBoxColumn(), DataPropertyName = "FlagMember", HeaderText = "สมาชิก", Name = "FlagMember", Width = 80, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleCenter });
            colList.Add(new DataGridColumn() { DataPropertyName = "CreditDay", HeaderText = "เครดิต(วัน)", Name = "CreditDay", Width = 80, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" });

            if (predicate != null)
            {
                _frm.PreparePopupFormWithPredicate("CustomerPre", frm.Name, popupName, colList, null, _controls, predicate);
            }
            else
                _frm.PreparePopupForm("CustomerPre", frm.Name, popupName, colList, null, _controls, null);

            _frm.ShowDialog();
        }

        /// <summary>
        /// Pop up Employee form creater
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="_controls"></param>
        /// <param name="popupName"></param>
        /// <param name="predicate"></param>
        public static void OpenEmployeePopup(this Form frm, List<Control> _controls, string popupName, Func<tbl_Employee, bool> predicate = null)
        {

            frmSearchSupp _frm = new frmSearchSupp();

            List<DataGridColumn> colList = new List<DataGridColumn>();
            colList.AddEmployeeColumn();

            if (predicate != null)
                _frm.PreparePopupFormWithPredicate("Employee", frm.Name, popupName, colList, null, _controls, predicate);
            else
                _frm.PreparePopupForm("Employee", frm.Name, popupName, colList, null, _controls, null);

            _frm.ShowDialog();
        }

        /// <summary>
        /// Pop up Employee Name form creater
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="_controls"></param>
        /// <param name="popupName"></param>
        /// <param name="predicate"></param>
        public static void OpenEmployeeNamePopup(this Form frm, List<Control> _controls, string popupName, Func<tbl_Employee, bool> predicate)
        {

            frmSearchSupp _frm = new frmSearchSupp();

            List<DataGridColumn> colList = new List<DataGridColumn>();
            colList.AddEmployeeColumn();

            _frm.PreparePopupFormWithPredicate("EmployeeName", frm.Name, popupName, colList, null, _controls, predicate);
            _frm.ShowDialog();
        }

        /// <summary>
        /// Pop up Employee Column form creater
        /// </summary>
        /// <param name="colList"></param>
        private static void AddEmployeeColumn(this List<DataGridColumn> colList)
        {
            colList.Add(new DataGridColumn() { DataPropertyName = "EmpCode", HeaderText = "รหัสพนักงาน", Name = "EmpCode", Width = 120, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, AddNumberInFirstRow = true, AddSearchAddOn = false });
            colList.Add(new DataGridColumn() { DataPropertyName = "EmpName", HeaderText = "ชื่อพนักงาน", Name = "EmpName", Width = 200, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.Fill });
        }

        /// <summary>
        /// Pop up Branch Warehouse form patern 1
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="_controls"></param>
        /// <param name="popupName"></param>
        public static void OpenBranchWarehousePopup(this Form frm, List<Control> _controls, string popupName)
        {

            frmSearchSupp _frm = new frmSearchSupp();

            List<DataGridColumn> colList = new List<DataGridColumn>();
            colList.AddBranchWarehousColumn();

            _frm.PreparePopupForm("BranchWarehouse", frm.Name, popupName, colList, null, _controls);
            _frm.ShowDialog();
        }

        public static void OpenBranchWarehouseIDPopup(this Form frm, List<Control> _controls, string popupName)
        {

            frmSearchSupp _frm = new frmSearchSupp();

            List<DataGridColumn> colList = new List<DataGridColumn>();
            colList.AddBranchWarehouseColumnID();

            _frm.PreparePopupForm("BranchWarehouseID", frm.Name, popupName, colList, null, _controls);
            _frm.ShowDialog();
        }
        /// <summary>
        /// Pop up Branch Warehouse form patern 2
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="_controls"></param>
        /// <param name="popupName"></param>
        /// <param name="consitionString"></param>

        public static void OpenBranchWarehousePopup(this Form frm, List<Control> _controls, string popupName, string[] consitionString)
        {

            frmSearchSupp _frm = new frmSearchSupp();

            List<DataGridColumn> colList = new List<DataGridColumn>();
            colList.AddBranchWarehousColumn();

            _frm.PreparePopupForm("BranchWarehouse", frm.Name, popupName, colList, null, _controls, consitionString);
            _frm.ShowDialog();
        }

        public static void OpenBranchWarehouseIDPopup(this Form frm, List<Control> _controls, string popupName, string[] consitionString)
        {

            frmSearchSupp _frm = new frmSearchSupp();

            List<DataGridColumn> colList = new List<DataGridColumn>();
            colList.AddBranchWarehouseColumnID();

            _frm.PreparePopupForm("BranchWarehouseID", frm.Name, popupName, colList, null, _controls, consitionString);
            _frm.ShowDialog();
        }

        /// <summary>
        /// Pop up Branch Warehouse form patern 3
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="_controls"></param>
        /// <param name="popupName"></param>
        /// <param name="predicate"></param>
        public static void OpenBranchWarehousePopup(this Form frm, List<Control> _controls, string popupName, Func<tbl_BranchWarehouse, bool> predicate)
        {

            frmSearchSupp _frm = new frmSearchSupp();

            List<DataGridColumn> colList = new List<DataGridColumn>();
            colList.AddBranchWarehousColumn();

            _frm.PreparePopupFormWithPredicate("BranchWarehouse", frm.Name, popupName, colList, null, _controls, predicate);
            _frm.ShowDialog();
        }

        public static void OpenBranchWarehouseIDPopup(this Form frm, List<Control> _controls, string popupName, Func<tbl_BranchWarehouse, bool> predicate)
        {

            frmSearchSupp _frm = new frmSearchSupp();

            List<DataGridColumn> colList = new List<DataGridColumn>();
            colList.AddBranchWarehouseColumnID();

            _frm.PreparePopupFormWithPredicate("BranchWarehouseID", frm.Name, popupName, colList, null, _controls, predicate);
            _frm.ShowDialog();
        }

        public static void OpenCrystalReportsPopup(this Form frm, string popUPText, string reportName, string storeName, Dictionary<string, object> _params, bool autoGenExcel = false)
        {

            frmCrystalReportEx _frm = new frmCrystalReportEx();

            _frm.PrepareReportPopup(popUPText, reportName, storeName, _params, autoGenExcel);
            _frm.ShowDialog();
        }

        public static void OpenTestCrystalReportsPopup(this Form frm, string popUPText, string reportName, string storeName, Dictionary<string, object> _params, bool autoGenExcel = false)
        {

            Reporting _frm = new Reporting();
            _frm.PrepareReportPopup(popUPText, reportName, storeName, _params, autoGenExcel);
            _frm.ShowDialog();
        }

        public static void OpenExcelReportsPopup(this Form frm, string popUPText, string reportName, string storeName, Dictionary<string, object> _params, bool autoGenExcel = false)
        {

            //for support excel report
            frmWait wait = new frmWait();
            wait.Show();

            frmCrystalReport _frm = new frmCrystalReport();

            _frm.PrepareExcelReportPopup(popUPText, reportName, storeName, _params, autoGenExcel);
            //_frm.ShowDialog();

            wait.Dispose();
            wait.Close();
        }

        public static void OpenExcelReportsPopup(this Form frm, string popUPText, string reportName, DataTable dt, bool autoGenExcel = false)
        {

            //for support excel report
            frmWait wait = new frmWait();
            wait.Show();

            frmCrystalReport _frm = new frmCrystalReport();

            _frm.PrepareExcelReportWithDTPopup(popUPText, reportName, dt, autoGenExcel);
            //_frm.ShowDialog();

            wait.Dispose();
            wait.Close();
        }

        public static void OpenManualExcelReportsPopup(this Form frm, string popUPText, string reportName, string storeName, Dictionary<string, object> _params, bool autoGenExcel = false)
        {

            //for support excel report
            frmWait wait = new frmWait();
            wait.Show();

            frmCrystalReport _frm = new frmCrystalReport();

            _frm.PrepareManualExcelReportPopup(popUPText, reportName, storeName, _params, autoGenExcel);
            //_frm.ShowDialog();

            wait.Dispose();
            wait.Close();
        }

        public static void OpenManualExcelCenterReportsPopup(this Form frm, string popUPText, string reportName, string storeName, Dictionary<string, object> _params, bool autoGenExcel = false)
        {

            //for support excel report
            frmWait wait = new frmWait();
            wait.Show();

            frmCrystalReport _frm = new frmCrystalReport();

            _frm.PrepareManualExcelCenterReportPopup(popUPText, reportName, storeName, _params, autoGenExcel);
            //_frm.ShowDialog();

            wait.Dispose();
            wait.Close();
        }

        /// <summary>
        /// Pop up Branch Warehous Column form creater
        /// </summary>
        /// <param name="colList"></param>
        private static void AddBranchWarehousColumn(this List<DataGridColumn> colList)
        {
            colList.Add(new DataGridColumn() { DataPropertyName = "WHCode", HeaderText = "รหัสคลังสินค้า", Name = "WHCode", Width = 120, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, AddNumberInFirstRow = true, AddSearchAddOn = false });
            colList.Add(new DataGridColumn() { DataPropertyName = "WHName", HeaderText = "ชื่อคลังสินค้า", Name = "WHName", Width = 200, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.Fill });
        }
        /// <summary>
        /// Pop up Branch Warehous Column form creater
        /// </summary>
        /// <param name="colList"></param>
        /// 

        private static void AddBranchWarehouseColumnID(this List<DataGridColumn> colList)
        {
            colList.Add(new DataGridColumn() { DataPropertyName = "WHID", HeaderText = "รหัสคลังสินค้า", Name = "WHID", Width = 120, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, AddNumberInFirstRow = true, AddSearchAddOn = false });
            colList.Add(new DataGridColumn() { DataPropertyName = "WHName", HeaderText = "ชื่อคลังสินค้า", Name = "WHName", Width = 200, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.Fill });
        }

        private static void AddBranchWarehouseColumn(this List<DataGridColumn> colList)
        {
            colList.Add(new DataGridColumn() { DataPropertyName = "WHCode", HeaderText = "รหัสคลังสินค้า", Name = "WHCode", Width = 120, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, AddNumberInFirstRow = true, AddSearchAddOn = false });
            colList.Add(new DataGridColumn() { DataPropertyName = "WHName", HeaderText = "ชื่อคลังสินค้า", Name = "WHName", Width = 200, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.Fill });
        }

        private static void AddDistrictColumn(this List<DataGridColumn> colList)
        {
            colList.Add(new DataGridColumn() { DataPropertyName = "DistrictCode", HeaderText = "รหัสตำบล", Name = "DistrictCode", Width = 120, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, AddNumberInFirstRow = true, AddSearchAddOn = false });
            colList.Add(new DataGridColumn() { DataPropertyName = "DistrictName", HeaderText = "ชื่อตำบล", Name = "DistrictName", Width = 30, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.Fill });
            colList.Add(new DataGridColumn() { DataPropertyName = "DistrictID", HeaderText = "รหัสตำบล", Name = "DistrictID", Visibility = false });

        }

        public static void OpenDistrictPopup(this Form frm, List<Control> _controls, string popupName, Func<tbl_MstDistrict, bool> predicate)
        {

            frmSearchSupp _frm = new frmSearchSupp();

            List<DataGridColumn> colList = new List<DataGridColumn>();

            colList.AddDistrictColumn();

            _frm.PreparePopupFormWithPredicate("SubDistict", frm.Name, popupName, colList, null, _controls, predicate);

            _frm.ShowDialog();
        }
        /// <summary>
        /// Pop up From Branch ID form creater
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="_controls"></param>
        /// <param name="popupName"></param>

        public static void OpenFromBranchIDPopup(this Form frm, List<Control> _controls, string popupName)
        {

            frmSearchSupp _frm = new frmSearchSupp();

            List<DataGridColumn> colList = new List<DataGridColumn>();
            colList.Add(new DataGridColumn() { DataPropertyName = "BranchCode", HeaderText = "รหัส", Name = "BranchCode", Width = 120, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, AddNumberInFirstRow = true, AddSearchAddOn = false });
            colList.Add(new DataGridColumn() { DataPropertyName = "BranchName", HeaderText = "ชื่อ", Name = "BranchName", Width = 200, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.Fill });

            _frm.PreparePopupForm("FromBranchID", frm.Name, popupName, colList, null, _controls);
            _frm.ShowDialog();
        }

        /// <summary>
        /// Pop up Sale Area District form creater
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="_controls"></param>
        /// <param name="popupName"></param>
        /// <param name="predicate"></param>
        public static void OpenSaleAreaDistrictPopup(this Form frm, List<Control> _controls, string popupName, Func<tbl_SalAreaDistrict, bool> predicate = null)
        {

            frmSearchSupp _frm = new frmSearchSupp();

            List<DataGridColumn> colList = new List<DataGridColumn>();
            colList.Add(new DataGridColumn() { DataPropertyName = "ProvinceName", HeaderText = "จังหวัด", Name = "ProvinceName", Width = 180, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, AddNumberInFirstRow = true, AddSearchAddOn = false });
            colList.Add(new DataGridColumn() { DataPropertyName = "AreaName", HeaderText = "อำเภอ", Name = "AreaName", Width = 180, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "DistrictCode", HeaderText = "รหัสตำบล", Name = "DistrictCode", Width = 120, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "DistrictName", HeaderText = "ชื่อตำบล", Name = "DistrictName", Width = 200, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.Fill });

            if (predicate != null)
                _frm.PreparePopupFormWithPredicate("SaleAreaDistrict", frm.Name, popupName, colList, null, _controls, predicate);
            else
                _frm.PreparePopupForm("SaleAreaDistrict", frm.Name, popupName, colList, null, _controls, null);


            _frm.ShowDialog();
        }

        /// <summary>
        /// Pop up document form creater
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="popupName"></param>
        /// <param name="docTypeCode"></param>
        public static void OpenDocPopup(this Form frm, string popupName, string docTypeCode)
        {

            frmSearchSupp _frm = new frmSearchSupp();

            List<DataGridColumn> colList = new List<DataGridColumn>();
            //bool addSearchAd = false;
            //if (docTypeCode.Trim() == "RL")
            //    addSearchAd = true;

            colList.Add(new DataGridColumn() { DataPropertyName = "DocNo", HeaderText = "เลขที่เอกสาร", Name = "DocNo", Width = 120, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleCenter, AddNumberInFirstRow = true, AddSearchAddOn = true });
            colList.Add(new DataGridColumn() { DataPropertyName = "DocStatusImg", HeaderText = "", Name = "DocStatusImg", Width = 30, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleCenter, ColoumnType = new DataGridViewImageColumn() });
            colList.Add(new DataGridColumn() { DataPropertyName = "DocStatus", HeaderText = "สถานะ", Name = "DocStatus", Width = 80, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleCenter });

            if (docTypeCode.Trim() == "RL")
                colList.Add(new DataGridColumn() { DataPropertyName = "WHID", HeaderText = "แวน", Name = "WHID", Width = 80, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleCenter });


            colList.Add(new DataGridColumn() { DataPropertyName = "DocRef", HeaderText = "DocRef", Name = "DocRef", Width = 100, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleCenter });
            colList.Add(new DataGridColumn() { DataPropertyName = "DocDate", HeaderText = "วันที่เอกสาร", Name = "DocDate", Width = 100, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "dd/MM/yyyy" });
            colList.Add(new DataGridColumn() { DataPropertyName = "SuppName", HeaderText = "ผู้จำหน่าย", Name = "SuppName", Width = 160, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "CreditDay", HeaderText = "เครดิต(วัน)", Name = "CreditDay", Width = 80, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" });
            colList.Add(new DataGridColumn() { DataPropertyName = "DueDate", HeaderText = "ครบกำหนด", Name = "DueDate", Width = 100, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "dd/MM/yyyy" });
            colList.Add(new DataGridColumn() { DataPropertyName = "TotalDue", HeaderText = "จำนวนรวมทั้งสิ้น", Name = "TotalDue", Width = 130, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" });
            colList.Add(new DataGridColumn() { DataPropertyName = "CrUser", HeaderText = "ผู้จัดทำ", Name = "CrUser", Width = 120, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "Remark", HeaderText = "หมายเหตุ", Name = "Remark", Width = 120, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });

            _frm.PreparePopupForm(docTypeCode, frm.Name, popupName, colList);
            _frm.ShowDialog();
        }

        /// <summary>
        /// Pop up IV form creater
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="popupName"></param>
        /// <param name="docTypeCode"></param>
        public static void OpenIVDocPopup(this Form frm, string popupName, string docTypeCode, bool isShowAll = false)
        {

            frmSearchSupp _frm = new frmSearchSupp();

            List<DataGridColumn> colList = new List<DataGridColumn>();
            colList.Add(new DataGridColumn() { DataPropertyName = "DocNo", HeaderText = "เลขที่เอกสาร", Name = "DocNo", Width = 120, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleCenter, AddNumberInFirstRow = true, AddSearchAddOn = true });
            colList.Add(new DataGridColumn() { DataPropertyName = "DocStatusImg", HeaderText = "", Name = "DocStatusImg", Width = 30, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleCenter, ColoumnType = new DataGridViewImageColumn() });
            colList.Add(new DataGridColumn() { DataPropertyName = "DocStatus", HeaderText = "สถานะ", Name = "DocStatus", Width = 80, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleCenter });
            colList.Add(new DataGridColumn() { DataPropertyName = "DocDate", HeaderText = "วันที่เอกสาร", Name = "DocDate", Width = 100, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleCenter, Format = "dd/MM/yyyy" });


            colList.Add(new DataGridColumn() { DataPropertyName = "CustomerID", HeaderText = "รหัสลูกค้า", Name = "CustomerID", Width = 120, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "CustomerName", HeaderText = "ชื่อลูกค้า", Name = "CustomerName", Width = 160, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });

            colList.Add(new DataGridColumn() { DataPropertyName = "WHID", HeaderText = "รหัสคลัง", Name = "WHID", Width = 160, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "SaleEmpID", HeaderText = "รหัสพนักงาน", Name = "SaleEmpID", Width = 160, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "TotalDue", HeaderText = "ยอดรวม", Name = "TotalDue", Width = 130, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" });


            colList.Add(new DataGridColumn() { DataPropertyName = "CrUser", HeaderText = "ผู้จัดทำ", Name = "CrUser", Width = 120, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "Remark", HeaderText = "หมายเหตุ", Name = "Remark", Width = 120, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });

            string[] _conditionString = null;
            if (docTypeCode == "IV")
                _conditionString = isShowAll ? new string[] { "19000101" } : null; //edit by sailom .k 14/12/2021
            else
                _conditionString = null;

            _frm.PreparePopupForm(docTypeCode, frm.Name, popupName, colList, null, null, _conditionString);

            _frm.Width = 800;
            _frm.Height = 600;
            _frm.ShowDialog();
        }

        public static void OpenCellProductPopup(this Form frm, string popupName, string docTypeCode, int rowIndex)
        {

            frmSearchSupp _frm = new frmSearchSupp();

            List<DataGridColumn> colList = new List<DataGridColumn>();
            colList.Add(new DataGridColumn() { DataPropertyName = "ProductID", HeaderText = "รหัส", Name = "ProductCode", Width = 80, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, AddNumberInFirstRow = true, AddSearchAddOn = false });
            colList.Add(new DataGridColumn() { DataPropertyName = "ProductRefCode", HeaderText = "รหัส SAP", Name = "ProductCod", Width = 90, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "ProductName", HeaderText = "ชื่อ", Name = "ProductCod", Width = 160, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.Fill });
            colList.Add(new DataGridColumn() { DataPropertyName = "ProductGroupName", HeaderText = "กลุ่มสินค้า", Name = "ProductCod", Width = 100, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "ProductSubGroupName", HeaderText = "กลุ่มย่อยสินค้า", Name = "ProductCod", Width = 110, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });

            _frm.PreparePopupForm(docTypeCode, frm.Name, popupName, colList, rowIndex);
            _frm.ShowDialog();
        }

        public static void OpenProductPopup(this Form frm, List<Control> _controls, string popupName)
        {

            frmSearchSupp _frm = new frmSearchSupp();

            List<DataGridColumn> colList = new List<DataGridColumn>();
            colList.Add(new DataGridColumn() { DataPropertyName = "ProductID", HeaderText = "รหัส", Name = "ProductCode", Width = 80, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet, AddNumberInFirstRow = true, AddSearchAddOn = false });
            colList.Add(new DataGridColumn() { DataPropertyName = "ProductRefCode", HeaderText = "รหัส SAP", Name = "ProductCod", Width = 90, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "ProductName", HeaderText = "ชื่อ", Name = "ProductCod", Width = 160, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.Fill });
            colList.Add(new DataGridColumn() { DataPropertyName = "ProductGroupName", HeaderText = "กลุ่มสินค้า", Name = "ProductCod", Width = 100, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });
            colList.Add(new DataGridColumn() { DataPropertyName = "ProductSubGroupName", HeaderText = "กลุ่มย่อยสินค้า", Name = "ProductCod", Width = 110, AutoSizeColumnMode = DataGridViewAutoSizeColumnMode.NotSet });

            _frm.PreparePopupForm("TRProduct", frm.Name, popupName, colList, null, _controls);
            _frm.ShowDialog();
        }

        private static void PrepareBindData(string type, List<Control> _controls)
        {
            if (_controls[0].Name.Contains("txtDistrictCode"))
            {
                _objType = ObjectType.SubDistict;
            }
            if (_controls[0].Name.Contains("WHID"))
            {
                _objType = ObjectType.BranchWarehouseID;
            }
            if (_controls[0].Name.Contains("SupplierCode"))
            {
                _objType = ObjectType.Supplier;
            }
            else if (_controls[0].Name.Contains("WHCode"))
            {
                _objType = ObjectType.BranchWarehouse;
            }
            else if (_controls[0].Name.Contains("BranchCode"))
            {
                _objType = ObjectType.FromBranchID;
            }
            else if (_controls[0].Name.Contains("BranchID"))//
            {
                _objType = ObjectType.FromBranchID;
            }
            else if (_controls[0].Name.Contains("EmpCode"))
            {
                _objType = ObjectType.Employee;
            }
            else if (_controls[0].Name.Contains("CustomerID"))
            {
                _objType = ObjectType.Customer;
            }
            else if (_controls[0].Name.Contains("ProductCode"))
            {
                _objType = ObjectType.TRProduct;
            }
            else if (_controls[0].Name.Contains("EmpID"))
            {
                _objType = ObjectType.Employee;
            }

            switch (type)
            {
                case "BranchWarehouseID": { _objType = ObjectType.BranchWarehouseID; whIDControlList = _controls; } break;
                case "SubDistict": { _objType = ObjectType.SubDistict; supDistictControlList = _controls; } break;
                case "Supplier": { _objType = ObjectType.Supplier; supControlList = _controls; } break;
                case "ODProduct": { _objType = ObjectType.ODProduct; } break;
                case "REProduct": { _objType = ObjectType.REProduct; } break;
                case "OD": { _objType = ObjectType.OD; } break;
                case "REOD": { _objType = ObjectType.REOD; } break;
                case "RJRB": { _objType = ObjectType.RJRB; } break;
                case "BranchWarehouse": { _objType = ObjectType.BranchWarehouse; whControlList = _controls; } break;
                case "FromBranchID": { _objType = ObjectType.FromBranchID; fbiControlList = _controls; } break;
                case "Employee": { _objType = ObjectType.Employee; empControlList = _controls; } break;
                case "EmployeeName": { _objType = ObjectType.EmployeeName; empControlList = _controls; } break;
                case "Customer": { _objType = ObjectType.Customer; custControlList = _controls; } break;
                case "TRProduct": { _objType = ObjectType.TRProduct; prdControlList = _controls; } break;
                case "CCProduct": { _objType = ObjectType.CCProduct; prdControlList = _controls; } break;
                default:
                    break;
            }

            if (_controls[0].Name.Contains("txtDistrictCode"))
            {
                controlList = supDistictControlList;
            }
            else if (_controls[0].Name.Contains("SupplierCode"))
            {
                controlList = supControlList;
            }
            else if (_controls[0].Name.Contains("WHCode"))
            {
                controlList = whControlList;
            }
            else if (_controls[0].Name.Contains("BranchCode"))
            {
                controlList = fbiControlList;
            }
            else if (_controls[0].Name.Contains("EmpCode"))
            {
                controlList = empControlList;
            }
            else if (_controls[0].Name.Contains("CustomerID"))
            {
                controlList = custControlList;
            }
            else if (_controls[0].Name.Contains("ProductCode"))
            {
                controlList = prdControlList;
            }
            else if (_controls[0].Name.Contains("EmpID"))
            {
                controlList = empControlList;
            }
        }

        public static void SetSearchControl(this TextBox txt, string type, List<Control> _controls)
        {
            PrepareBindData(type, _controls);

            Control keydownControl = _controls.FirstOrDefault(x => x.Name == txt.Name);
            if (keydownControl != null)
            {
                //keydownControl.KeyDown -= KeydownControl_KeyDown;
                //keydownControl.KeyDown += KeydownControl_KeyDown;
            }
        }

        private static void KeydownControl_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (sender is TextBox)
            //    {
            //        TextBox txt = (TextBox)sender;
            //        if (!string.IsNullOrEmpty(txt.Text))
            //        {
            //            BindData(txt.Text);
            //        }
            //    }
            //}
        }

        private static void BindData(string code)
        {
            switch (_objType)
            {
                case ObjectType.BranchWarehouseID:
                    var bwhID = objectFactory.Get(_objType, null) as BranchWarehouse;
                    var bwhDataID = bwhID.GetAllData().FirstOrDefault(x => x.WHID.ToString().ToLower() == code.ToLower());
                    SetControlValue(bwhDataID, code);

                    break;
                case ObjectType.Supplier:
                    var sup = objectFactory.Get(_objType, null) as Supplier;
                    var supData = sup.GetAllData().FirstOrDefault(x => x.SupplierCode.ToLower() == code.ToLower());
                    SetControlValue(supData, code);

                    break;
                case ObjectType.SubDistict:
                    var supD = objectFactory.Get(_objType, null) as SubDistict;
                    var supDData = supD.GetAllData().FirstOrDefault(x => x.DistrictCode.ToLower() == code.ToLower());
                    SetControlValue(supDData, code);

                    break;
                case ObjectType.BranchWarehouse:
                    var bwh = objectFactory.Get(_objType, null) as BranchWarehouse;
                    var bwhData = bwh.GetAllData().FirstOrDefault(x => x.WHCode.ToLower() == code.ToLower());
                    SetControlValue(bwhData, code);

                    break;
                case ObjectType.FromBranchID:
                    var fbi = objectFactory.Get(_objType, null) as Branch;
                    var fbiData = fbi.GetAllData().FirstOrDefault(x => x.BranchCode.ToLower() == code.ToLower());
                    SetControlValue(fbiData, code);

                    break;
                case ObjectType.Employee:
                    var emp = objectFactory.Get(_objType, null) as Employee;
                    var empData = emp.GetAllData().FirstOrDefault(x => x.EmpCode.ToLower() == code.ToLower());
                    SetControlValue(empData, code);

                    break;
                case ObjectType.EmployeeName:
                    var empName = objectFactory.Get(_objType, null) as Employee;
                    var empNameData = empName.GetAllData().FirstOrDefault(x => x.EmpCode.ToLower() == code.ToLower());
                    SetControlValue(empNameData, code);

                    break;
                case ObjectType.Customer:
                    {
                        var cust = objectFactory.Get(_objType, null) as Customer;
                        var custData = cust.GetAllData(x => x.CustomerID.ToLower() == code.ToLower());
                        if (custData.Count > 0)
                            SetControlValue(custData[0], code);
                        else
                        {
                            SetControlValue(custData, code);
                            if (!string.IsNullOrEmpty(code))
                            {
                                ShowWarningMessage("ไม่พบข้อมูลร้านค้านี้ในระบบ!!!");
                                return;
                            }
                        }
                    }
                    break;
                case ObjectType.TRProduct:
                    var prd = objectFactory.Get(_objType, null) as Product;
                    var prdData = prd.GetAllData().FirstOrDefault(x => x.ProductCode.ToLower() == code.ToLower());
                    SetControlValue(prdData, code);

                    break;
                case ObjectType.CCProduct:
                    var ccprd = objectFactory.Get(_objType, null) as Product;
                    var ccprdData = ccprd.GetAllData().FirstOrDefault(x => x.ProductCode.ToLower() == code.ToLower());
                    SetControlValue(ccprdData, code);

                    break;
                default:
                    break;
            }

            CalcDueDate();
        }

        public static void BindData(this Form frm, string type, List<Control> _controls, string code)
        {
            if (type == "PromotionTemp") //pop up shelf
            {
                var bu = new PromotionTemp();
                var allPro = bu.GetAllData();
                if (allPro != null && allPro.Count > 0)
                {
                    var item = allPro.FirstOrDefault(x => x.PromotionID == code);
                    if (item != null)
                    {
                        var skuList = bu.GetHQSKUGroup(x => x.SKUGroupID == item.SKUGroupRewardID);
                        if (skuList != null && skuList.Count > 0)
                        {
                            var prd = bu.GetProduct(x => skuList.Select(a => a.SKU_ID).Contains(x.ProductID)).ToList();
                            if (prd != null && prd.Count > 0)
                            {
                                if (prd.Any(x => x.Flavour == "ชั้นวาง" || x.ProductName.Contains("ชั้นวาง")))
                                {
                                    frmShelfPopup _frm = new frmShelfPopup();
                                    _frm.SetPromotionTemp(item);
                                    _frm.Text = "รหัส Shelf";
                                    _frm.ShowDialog();
                                }
                            }
                        }
                    }
                }
                return;
            }
            else
            {
                PrepareBindData(type, _controls);

                BindData(code);
            }
        }

        private static void SetEventControl(this Control obj)
        {
            if (obj is DateTimePicker)
            {
                var ctrl = obj as DateTimePicker;

                ctrl.ValueChanged += Obj_ValueChanged;
            }
            if (obj is NumericUpDown)
            {
                var ctrl = obj as NumericUpDown;

                ctrl.ValueChanged += Obj_ValueChanged;
            }
        }

        public static void SetObjectFromObject<T>(this tbl_HQ_Promotion_Hit obj, T targetObj)
        {
            foreach (PropertyInfo updateDataItem in targetObj.GetType().GetProperties())
            {
                foreach (PropertyInfo item in obj.GetType().GetProperties())
                {
                    if (updateDataItem.Name == item.Name)
                    {
                        var value = item.GetValue(item.PropertyType, null);

                        Type t = Nullable.GetUnderlyingType(updateDataItem.PropertyType) ?? updateDataItem.PropertyType;
                        object safeValue = (value == null) ? null : Convert.ChangeType(value, t);

                        updateDataItem.SetValue(item, safeValue, null);
                    }
                }
            }
        }

        public static void SetObjectFromControl<T>(this ControlCollection ctrl, T data)
        {
            var _controlList = new List<Control>();

            foreach (Control item in ctrl)
            {
                if (item is TextBox)
                {
                    _controlList.Add(item);
                }
                else if (item is ComboBox)
                {
                    _controlList.Add(item);
                }

                if (item is GroupBox)
                {
                    foreach (Control gItem in item.Controls)
                    {
                        if (gItem is TextBox)
                        {
                            _controlList.Add(gItem);
                        }
                    }
                }
            }

            foreach (Control item in _controlList)
            {
                if (data != null)
                {
                    foreach (PropertyInfo dataItem in data.GetType().GetProperties())
                    {
                        //var ctrlName = item.Name.Substring(3, item.Name.Length - 3);
                        if (item.Name.Contains(dataItem.Name))
                        {
                            object value = null;

                            if (item is TextBox)
                            {
                                var obj = item as TextBox;
                                value = obj.Text;
                            }
                            else if (item is DateTimePicker)
                            {
                                var obj = item as DateTimePicker;
                                value = obj.Value;
                            }
                            else if (item is NumericUpDown)
                            {
                                var obj = item as NumericUpDown;
                                value = obj.Value;
                            }
                            else if (item is ComboBox)
                            {
                                var obj = item as ComboBox;
                                value = obj.SelectedValue;
                            }

                            try
                            {
                                dataItem.SetValue(data, Convert.ChangeType(value, dataItem.PropertyType), null);
                            }
                            catch
                            {
                                //var _type = dataItem.PropertyType.FullName.GetPropertyType();
                                Type _type = Nullable.GetUnderlyingType(dataItem.PropertyType) ?? dataItem.PropertyType;

                                object safeValue = null;
                                if (_type.FullName.Contains(typeof(string).ToString()))
                                {
                                    safeValue = string.IsNullOrEmpty(value.ToString()) ? null : Convert.ChangeType(value, _type);
                                }
                                else
                                {
                                    if (value != null)
                                        safeValue = string.IsNullOrEmpty(value.ToString()) ? Convert.ChangeType(0, _type) : Convert.ChangeType(value, _type);
                                }

                                dataItem.SetValue(data, safeValue, null);
                            }

                        }
                    }
                }
            }
        }

        //private static Type GetPropertyType(this string typeName)
        //{
        //    Type ret = null;
        //    if (typeName.Contains(typeof(Int16).ToString()))
        //    {
        //        ret = typeof(Int16);
        //    }
        //    else if (typeName.Contains(typeof(Int32).ToString()))
        //    {
        //        ret = typeof(Int32);
        //    }
        //    else if (typeName.Contains(typeof(DateTime).ToString()))
        //    {
        //        ret = typeof(DateTime);
        //    }
        //    else if (typeName.Contains(typeof(Boolean).ToString()))
        //    {
        //        ret = typeof(Boolean);
        //    }

        //    return ret;
        //}

        public static void SetTextBoxControlValue<T>(this ControlCollection ctrl, T data)
        {
            controlList = new List<Control>();

            foreach (Control item in ctrl)
            {
                if (item is TextBox)
                {
                    controlList.Add(item);
                }

                if (item is GroupBox)
                {
                    foreach (Control gItem in item.Controls)
                    {
                        if (gItem is TextBox)
                        {
                            controlList.Add(gItem);
                        }
                    }
                }
            }

            SetControlValue(data, "");
        }

        public static bool CheckExistsData<T>(this T obj, string code)
        {
            bool ret = false;
            if (obj is tbl_Branch)
            {
                Func<tbl_Branch, bool> func = (x => x.BranchCode == code);
                ret = new tbl_Branch().Select(func).Count > 0;
            }
            if (obj is tbl_Employee)
            {
                Func<tbl_Employee, bool> func = (x => x.EmpID == code);
                ret = new tbl_Employee().Select(func).Count > 0;
            }
            if (obj is tbl_Users)
            {
                Func<tbl_Users, bool> func = (x => x.Username == code);
                ret = new tbl_Users().Select(func).Count > 0;
            }
            if (obj is tbl_BranchWarehouse)
            {
                Func<tbl_BranchWarehouse, bool> func = (x => x.WHCode == code);
                ret = new tbl_BranchWarehouse().Select(func).Count > 0;
            }
            if (obj is tbl_SalAreaDistrict)
            {
                Func<tbl_SalAreaDistrict, bool> func = (x => x.SalAreaID == code);
                ret = new tbl_SalAreaDistrict().Select(func).Count > 0;
            }
            if (obj is tbl_SalArea)
            {
                Func<tbl_SalArea, bool> func = (x => x.SalAreaID == code);
                ret = new tbl_SalArea().Select(func).Count > 0;
            }
            if (obj is tbl_ArCustomer)
            {
                //Func<tbl_ArCustomer, bool> func = (x => x.CustomerID == code);
                ret = new tbl_ArCustomer().SelectSingle(code).Count > 0;
            }
            if (obj is tbl_PriceGroup)
            {
                Func<tbl_PriceGroup, bool> func = (x => x.PriceGroupID.ToString() == code);
                ret = new tbl_PriceGroup().Select(func).Count > 0;
            }
            if (obj is tbl_ShopType)
            {
                Func<tbl_ShopType, bool> func = (x => x.ShopTypeID.ToString() == code);
                ret = new tbl_ShopType().Select(func).Count > 0;
            }
            if (obj is tbl_ArCustomerShelf)
            {
                Func<tbl_ArCustomerShelf, bool> func = (x => x.ShelfID == code);
                ret = new tbl_ArCustomerShelf().Select(func).Count > 0;
            }
            if (obj is tbl_PayMaster)
            {
                Func<tbl_PayMaster, bool> func = (x => x.DocNo == code);
                ret = new tbl_PayMaster().Select(func).Count > 0;
            }
            return ret;
        }

        private static void SetControlValue<T>(T data, string code)
        {
            foreach (Control item in controlList)
            {
                object safeValue = null;
                string empName = "";

                if (data != null)
                {
                    foreach (PropertyInfo dataItem in data.GetType().GetProperties())
                    {
                        if ((item.Name == "txtSaleEmpID" && dataItem.Name == "EmpID") ||
                            (item.Name == "txtDriverEmpID" && dataItem.Name == "EmpID") ||
                            (item.Name == "txtHelperEmpID" && dataItem.Name == "EmpID") ||
                            (item.Name == "txtBranchCode_" && dataItem.Name == "BranchCode"))
                        {
                            var value = dataItem.GetValue(data, null);

                            Type t = Nullable.GetUnderlyingType(dataItem.PropertyType) ?? dataItem.PropertyType;
                            safeValue = (value == null) ? null : Convert.ChangeType(value, t);
                        }
                        else
                        {
                            //var ctrlName = item.Name.Substring(3, item.Name.Length - 3);
                            //if (ctrlName == dataItem.Name) 
                            if (item.Name.Contains(dataItem.Name))
                            {
                                var value = dataItem.GetValue(data, null);

                                Type t = Nullable.GetUnderlyingType(dataItem.PropertyType) ?? dataItem.PropertyType;
                                safeValue = (value == null) ? null : Convert.ChangeType(value, t);
                            }

                            //for empployee popup
                            if ((_objType == ObjectType.Employee || _objType == ObjectType.EmployeeName) && (dataItem.Name.Contains("TitleName") || dataItem.Name.Contains("FirstName")))
                            {
                                var value = dataItem.GetValue(data, null);
                                empName += value + " ";
                            }

                        }
                    }
                }

                if (item is TextBox)
                {
                    var obj = item as TextBox;
                    obj.Text = safeValue != null ? safeValue.ToString() : "";

                    //for empployee popup
                    if (_objType == ObjectType.Employee)
                    {
                        if (item.Name.Contains("EmpName") && !string.IsNullOrEmpty(empName))
                        {
                            obj.Text = empName;
                        }
                    }
                    if (_objType == ObjectType.EmployeeName)
                    {
                        if (item.Name.Contains("EmpCode") && !string.IsNullOrEmpty(empName))
                        {
                            obj.Text = empName;
                        }
                    }
                    if (_objType == ObjectType.BranchWarehouse)
                    {
                        if (item.Name.Contains("txtBranchName_"))
                        {
                            obj.Text = safeValue != null ? safeValue.ToString() : "";
                        }
                    }
                }
                else if (item is DateTimePicker)
                {
                    var obj = item as DateTimePicker;
                    obj.Value = safeValue != null ? Convert.ToDateTime(safeValue) : DateTime.Now.ToDateTimeFormat();

                    if (obj.Name.Contains("DocDate"))
                    {
                        docDateCtrl = obj;
                        obj.SetEventControl();
                    }

                    if (obj.Name.Contains("DueDate"))
                        dueDateCtrl = obj;
                }
                else if (item is NumericUpDown)
                {
                    var obj = item as NumericUpDown;
                    obj.Value = safeValue != null ? Convert.ToInt32(safeValue) : 0;

                    if (obj.Name.Contains("CreditDay"))
                    {
                        creditDayCtrl = obj;
                        obj.SetEventControl();
                    }
                }
            }
        }

        private static void Obj_ValueChanged(object sender, EventArgs e)
        {
            CalcDueDate();
        }

        private static void CalcDueDate()
        {
            if (dueDateCtrl != null && docDateCtrl != null && creditDayCtrl != null)
            {
                dueDateCtrl.Value = docDateCtrl.Value.AddDays(Convert.ToInt32(creditDayCtrl.Value));
            }
        }

        public static void CalcDueDate(this DateTimePicker docDateCtrl, DateTimePicker dueDateCtrl, NumericUpDown creditDayCtrl)
        {
            if (dueDateCtrl != null && docDateCtrl != null && creditDayCtrl != null)
            {
                dueDateCtrl.Value = docDateCtrl.Value.AddDays(Convert.ToInt32(creditDayCtrl.Value));
                dueDateCtrl.SetDateTimePickerFormat();
            }
        }

        public static void CalcDueDate(this NumericUpDown creditDayCtrl, DateTimePicker docDateCtrl, DateTimePicker dueDateCtrl)
        {
            if (dueDateCtrl != null && docDateCtrl != null && creditDayCtrl != null)
            {
                dueDateCtrl.Value = docDateCtrl.Value.AddDays(Convert.ToInt32(creditDayCtrl.Value));
            }
        }

        public static void OpenControl(this Form frm, bool mode, string[] tagName, int[] editCols)
        {
            var textboxs = GetAll(frm, typeof(TextBox));
            foreach (TextBox item in textboxs)
            {
                if (item.Name.Contains("txt"))
                {
                    item.DisableTextBox(!mode);
                }
                else if (item.Name.Contains("txd"))
                {
                    item.DisableTextBox(!mode);
                    item.BackColor = Color.Turquoise;
                }
                else if (item.Name.Contains("txn"))
                {
                    item.DisableTextBox(true);
                }

                if (tagName.Contains(item.Name))
                {
                    item.DisableTextBox(true);
                }
            }


            var dtps = GetAll(frm, typeof(DateTimePicker));
            foreach (DateTimePicker item in dtps)
            {
                item.Enabled = mode;
            }

            var nuds = GetAll(frm, typeof(NumericUpDown));
            foreach (NumericUpDown item in nuds)
            {
                item.Enabled = mode;
            }

            var btns = GetAll(frm, typeof(Button));
            foreach (Button item in btns)
            {
                item.Enabled = mode;
            }

            var rdos = GetAll(frm, typeof(RadioButton));
            foreach (RadioButton item in rdos)
            {
                item.Enabled = mode;
            }

            var ddls = GetAll(frm, typeof(ComboBox));
            foreach (ComboBox item in ddls)
            {
                item.Enabled = false;
            }

            var grds = GetAll(frm, typeof(DataGridView));
            foreach (DataGridView item in grds)
            {
                bool isEdit = true;
                if (item.Tag != null && item.Tag.ToString() == "ReadOnly")
                    isEdit = false;

                if (isEdit)
                {
                    if (item.RowCount > 0)
                    {
                        for (int i = 0; i < item.RowCount; i++)
                        {
                            for (int j = 0; j < item.ColumnCount; j++)
                            {
                                if (editCols.Contains(item.Rows[i].Cells[j].ColumnIndex))
                                {
                                    Color c = mode ? Color.White : ColorTranslator.FromHtml("#E3E3E3");
                                    item.Rows[i].Cells[j].Style.BackColor = c;
                                    item.Rows[i].Cells[j].ReadOnly = !mode;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void OpenControl(this Control ctrls, bool mode, string[] tagName)
        {
            var textboxs = GetAll(ctrls, typeof(TextBox));
            foreach (TextBox item in textboxs)
            {
                if (item.Name.Contains("txt"))
                {
                    item.DisableTextBox(!mode);
                }
                else if (item.Name.Contains("txd"))
                {
                    item.DisableTextBox(!mode);
                    item.BackColor = Color.Turquoise;
                }
                else if (item.Name.Contains("txn"))
                {
                    item.DisableTextBox(true);
                }

                if (tagName.Contains(item.Name))
                {
                    item.DisableTextBox(true);
                }
            }

            var dtps = GetAll(ctrls, typeof(DateTimePicker));
            foreach (DateTimePicker item in dtps)
            {
                item.Enabled = mode;
            }

            var nuds = GetAll(ctrls, typeof(NumericUpDown));
            foreach (NumericUpDown item in nuds)
            {
                item.Enabled = mode;
            }

            var btns = GetAll(ctrls, typeof(Button));
            foreach (Button item in btns)
            {
                item.Enabled = mode;
            }

            var rdos = GetAll(ctrls, typeof(RadioButton));
            foreach (RadioButton item in rdos)
            {
                item.Enabled = mode;
            }

            var ddls = GetAll(ctrls, typeof(ComboBox));
            foreach (ComboBox item in ddls)
            {
                item.Enabled = mode;
            }

            var mtbs = GetAll(ctrls, typeof(MaskedTextBox));
            foreach (MaskedTextBox item in mtbs)
            {
                item.Enabled = mode;
                if (item.Enabled)
                {
                    item.BackColor = Color.White;
                }
            }

            var chkboxs = GetAll(ctrls, typeof(CheckBox));
            foreach (CheckBox item in chkboxs)
            {
                item.Enabled = mode;
            }

            //var grds = GetAll(ctrls, typeof(DataGridView));
            //foreach (DataGridView item in grds)
            //{
            //    if (item.RowCount > 0)
            //    {
            //        for (int i = 0; i < item.RowCount; i++)
            //        {
            //            for (int j = 0; j < item.ColumnCount; j++)
            //            {
            //                if (editCols.Contains(item.Rows[i].Cells[j].ColumnIndex))
            //                {
            //                    Color c = mode ? Color.White : ColorTranslator.FromHtml("#E3E3E3");
            //                    item.Rows[i].Cells[j].Style.BackColor = c;
            //                    item.Rows[i].Cells[j].ReadOnly = !mode;
            //                }
            //            }
            //        }
            //    }
            //}
        }
        //public static void OpenControl(this Control ctrls, bool mode, string[] tagName)
        //{
        //    var textboxs = GetAll(ctrls, typeof(TextBox));
        //    foreach (TextBox item in textboxs)
        //    {
        //        if (item.Name.Contains("txt"))
        //        {
        //            item.DisableTextBox(!mode);
        //        }
        //        else if (item.Name.Contains("txd"))
        //        {
        //            item.DisableTextBox(!mode);
        //            item.BackColor = Color.Turquoise;
        //        }
        //        else if (item.Name.Contains("txn"))
        //        {
        //            item.DisableTextBox(true);
        //        }

        //        if (tagName.Contains(item.Name))
        //        {
        //            item.DisableTextBox(true);
        //        }
        //    }

        //    var dtps = GetAll(ctrls, typeof(DateTimePicker));
        //    foreach (DateTimePicker item in dtps)
        //    {
        //        item.Enabled = mode;
        //    }

        //    var nuds = GetAll(ctrls, typeof(NumericUpDown));
        //    foreach (NumericUpDown item in nuds)
        //    {
        //        item.Enabled = mode;
        //    }

        //    var btns = GetAll(ctrls, typeof(Button));
        //    foreach (Button item in btns)
        //    {
        //        item.Enabled = mode;
        //    }

        //    var rdos = GetAll(ctrls, typeof(RadioButton));
        //    foreach (RadioButton item in rdos)
        //    {
        //        item.Enabled = mode;
        //    }

        //    var ddls = GetAll(ctrls, typeof(ComboBox));
        //    foreach (ComboBox item in ddls)
        //    {
        //        item.Enabled = mode;
        //    }

        //    var mtbs = GetAll(ctrls, typeof(MaskedTextBox));
        //    foreach (MaskedTextBox item in mtbs)
        //    {
        //        item.Enabled = mode;
        //    }

        //    //var grds = GetAll(ctrls, typeof(DataGridView));
        //    //foreach (DataGridView item in grds)
        //    //{
        //    //    if (item.RowCount > 0)
        //    //    {
        //    //        for (int i = 0; i < item.RowCount; i++)
        //    //        {
        //    //            for (int j = 0; j < item.ColumnCount; j++)
        //    //            {
        //    //                if (editCols.Contains(item.Rows[i].Cells[j].ColumnIndex))
        //    //                {
        //    //                    Color c = mode ? Color.White : ColorTranslator.FromHtml("#E3E3E3");
        //    //                    item.Rows[i].Cells[j].Style.BackColor = c;
        //    //                    item.Rows[i].Cells[j].ReadOnly = !mode;
        //    //                }
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //}

        public static void ClearControl(this Form frm, BaseControl bu, string docTypeCode, int digit)
        {
            var textboxs = GetAll(frm, typeof(TextBox));
            foreach (TextBox item in textboxs)
            {
                if (item.Name.Contains("txt"))
                {
                    item.ClearTextBox();
                }
                else if (item.Name.Contains("txd"))
                {
                    item.ClearTextBox();
                    item.BackColor = Color.Turquoise;
                }
                else if (item.Name.Contains("txn"))
                {
                    item.DefaultNumber();
                }
            }

            var mTextBoxs = GetAll(frm, typeof(MaskedTextBox));
            foreach (MaskedTextBox item in mTextBoxs)
            {
                item.ClearTextBox();
                string digitStr = "";
                string digitStr2 = "";
                if (docTypeCode == "IM" || docTypeCode == "V")
                {
                    var documentType = bu.GetDocumentType().FirstOrDefault(x => x.DocTypeCode.Trim() == docTypeCode.Trim());
                    if (documentType != null)
                    {
                        docTypeCode = documentType.DocTypeCode;
                        int runDigit = 0;

                        runDigit = (documentType.DocFormat.Length - documentType.RunLength.Value);// - 1; //edit by sailom .k 10/12/2021

                        for (int i = 0; i < runDigit; i++)
                        {
                            digitStr += "0";
                        }

                        int len = documentType.RunLength.Value;
                        if (docTypeCode == "V")
                        {
                            len = len + 3;
                        }

                        for (int i = 0; i < len; i++)
                        {
                            digitStr2 += "0";
                        }
                    }
                }

                string realDocTypeCode = "";
                realDocTypeCode = docTypeCode;

                if (docTypeCode == "IM")
                {
                    item.Mask = digitStr.Substring(0, 11) + "M" + digitStr2;
                }
                else if (docTypeCode == "V")
                {
                    //item.Mask = "V" + digitStr2;
                    //item.Mask = "##" + digitStr2; //edit by sailom .k 10/12/2021
                }
                else
                {
                    item.Mask = realDocTypeCode + digitStr;
                }

                item.BackColor = Color.Turquoise;
            }

            var dtps = GetAll(frm, typeof(DateTimePicker));
            foreach (DateTimePicker item in dtps)
            {
                item.Value = DateTime.Now;
            }

            var nuds = GetAll(frm, typeof(NumericUpDown));
            foreach (NumericUpDown item in nuds)
            {
                item.Value = 0;
            }

            var grds = GetAll(frm, typeof(DataGridView));
            foreach (DataGridView item in grds)
            {
                item.DataSource = null;
                item.Rows.Clear();
                item.Refresh();
            }

        }

        public static void ClearControl(this Form frm, string docTypeCode, int digit)
        {
            var textboxs = GetAll(frm, typeof(TextBox));
            foreach (TextBox item in textboxs)
            {
                if (item.Name.Contains("txt"))
                {
                    item.ClearTextBox();
                }
                else if (item.Name.Contains("txd"))
                {
                    item.ClearTextBox();
                    item.BackColor = Color.Turquoise;
                }
                else if (item.Name.Contains("txn"))
                {
                    item.DefaultNumber();
                }
            }

            var mTextBoxs = GetAll(frm, typeof(MaskedTextBox));
            foreach (MaskedTextBox item in mTextBoxs)
            {
                item.ClearTextBox();
                string digitStr = "";

                if (docTypeCode == "RL") //for RL from teblet pattern
                    digit += 2;

                for (int i = 0; i < digit; i++)
                {
                    digitStr += "0";
                }

                string realDocTypeCode = "";
                if (docTypeCode.Contains("L") || docTypeCode == "IV")
                {
                    char[] temp = new char[docTypeCode.Length];
                    temp = docTypeCode.ToCharArray();
                    List<string> _char = new List<string>();
                    foreach (char chrItem in temp)
                    {
                        if (chrItem == 'L')
                        {
                            realDocTypeCode += "\\" + chrItem;
                        }
                        else
                        {
                            realDocTypeCode += chrItem;
                        }
                    }
                }
                else
                {
                    realDocTypeCode = docTypeCode;
                }

                if (docTypeCode != "IV")
                {
                    if (docTypeCode == "RL")
                    {
                        string _digitStr = "";
                        var tmp = digitStr.ToCharArray();
                        for (int i = 0; i < tmp.Count(); i++)
                        {
                            //if (i == 3)
                            //{
                            //    tmp[i] = 'V';
                            //}
                            _digitStr += "C";
                        }

                        item.Mask = realDocTypeCode + _digitStr;
                    }
                    else
                        item.Mask = realDocTypeCode + digitStr;
                }
                else
                    item.Mask = digitStr;

                item.BackColor = Color.Turquoise;
            }

            var dtps = GetAll(frm, typeof(DateTimePicker));
            foreach (DateTimePicker item in dtps)
            {
                item.Value = DateTime.Now;
            }

            var nuds = GetAll(frm, typeof(NumericUpDown));
            foreach (NumericUpDown item in nuds)
            {
                item.Value = 0;
            }

            var grds = GetAll(frm, typeof(DataGridView));
            foreach (DataGridView item in grds)
            {
                item.DataSource = null;
                item.Rows.Clear();
                item.Refresh();
            }

            //Control[] tbxs = frm.Controls.Find("txt", true);
            //if (tbxs != null && tbxs.Length > 0)
            //{
            //    tbxs[0].Text = "Found!";
            //}
        }

        public static void ClearControl(this Control ctrls)
        {
            var textboxs = GetAll(ctrls, typeof(TextBox));
            foreach (TextBox item in textboxs)
            {
                if (item.Name.Contains("txt"))
                {
                    item.ClearTextBox();
                }
                else if (item.Name.Contains("txd"))
                {
                    item.ClearTextBox();
                    item.BackColor = Color.Turquoise;
                }
                else if (item.Name.Contains("txn"))
                {
                    item.DefaultNumber();
                }
            }

            var dtps = GetAll(ctrls, typeof(DateTimePicker));
            foreach (DateTimePicker item in dtps)
            {
                item.Value = DateTime.Now;
            }

            var nuds = GetAll(ctrls, typeof(NumericUpDown));
            foreach (NumericUpDown item in nuds)
            {
                item.Value = 0;
            }

            var mtbs = GetAll(ctrls, typeof(MaskedTextBox));
            foreach (MaskedTextBox item in mtbs)
            {
                item.ClearTextBox();
            }

            var ltbs = GetAll(ctrls, typeof(ListBox));//
            foreach (ListBox item in ltbs)
            {
                item.Items.Clear();
            }

            var pics = GetAll(ctrls, typeof(PictureBox));//
            foreach (PictureBox item in pics)
            {
                item.Image = null;
            }

            //var _btn = GetAll(ctrls, typeof(Button));
            //foreach (Button item in _btn)
            //{
                
            //}

            //var grds = GetAll(ctrls, typeof(DataGridView));
            //foreach (DataGridView item in grds)
            //{
            //    item.DataSource = null;
            //    item.Rows.Clear();
            //    item.Refresh();
            //}
        }
        //public static void ClearControl(this Control ctrls)
        //{
        //    var textboxs = GetAll(ctrls, typeof(TextBox));
        //    foreach (TextBox item in textboxs)
        //    {
        //        if (item.Name.Contains("txt"))
        //        {
        //            item.ClearTextBox();
        //        }
        //        else if (item.Name.Contains("txd"))
        //        {
        //            item.ClearTextBox();
        //            item.BackColor = Color.Turquoise;
        //        }
        //        else if (item.Name.Contains("txn"))
        //        {
        //            item.DefaultNumber();
        //        }
        //    }

        //    var dtps = GetAll(ctrls, typeof(DateTimePicker));
        //    foreach (DateTimePicker item in dtps)
        //    {
        //        item.Value = DateTime.Now;
        //    }

        //    var nuds = GetAll(ctrls, typeof(NumericUpDown));
        //    foreach (NumericUpDown item in nuds)
        //    {
        //        item.Value = 0;
        //    }


        //    var mtbs = GetAll(ctrls, typeof(MaskedTextBox));
        //    foreach (MaskedTextBox item in mtbs)
        //    {
        //        item.ClearTextBox();
        //    }

        //    //var grds = GetAll(ctrls, typeof(DataGridView));
        //    //foreach (DataGridView item in grds)
        //    //{
        //    //    item.DataSource = null;
        //    //    item.Rows.Clear();
        //    //    item.Refresh();
        //    //}
        //}

        public static IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type)).Concat(controls).Where(c => c.GetType() == type);
        }

        public static void EnableButton(this Form frm, EditButton btnEdit, RemoveButton btnRemove, SaveButton btnSave, CancelButton btnCancel,
            AddButton btnAdd, CopyButton btnCopy, PrintButton btnPrint, string conditionText)
        {
            btnEdit.Enabled = !string.IsNullOrEmpty(conditionText);
            btnRemove.Enabled = !string.IsNullOrEmpty(conditionText);
            btnSave.Enabled = !btnEdit.Enabled && !btnAdd.Enabled;
            btnCancel.Enabled = !btnAdd.Enabled;
            btnAdd.Enabled = !btnSave.Enabled && !btnEdit.Enabled;

            btnCopy.Enabled = false;
            btnPrint.Enabled = false;
        }

        public static void EnableButton(this Form frm, EditButton btnEdit, RemoveButton btnRemove, SaveButton btnSave, CancelButton btnCancel,
            AddButton btnAdd, CopyButton btnCopy, PrintButton btnPrint, PrintButton btnPrintCrys, string conditionText)
        {
            btnEdit.Enabled = !string.IsNullOrEmpty(conditionText);
            btnRemove.Enabled = !string.IsNullOrEmpty(conditionText);
            btnSave.Enabled = !btnEdit.Enabled && !btnAdd.Enabled;
            btnCancel.Enabled = !btnAdd.Enabled;
            btnAdd.Enabled = !btnSave.Enabled && !btnEdit.Enabled;

            btnCopy.Enabled = false;
            btnPrint.Enabled = false;
            btnPrintCrys.Enabled = btnPrint.Enabled;
        }

        public static void EnableButton(this Form frm, EditButton btnEdit, RemoveButton btnRemove, SaveButton btnSave, CancelButton btnCancel,
            AddButton btnAdd, CopyButton btnCopy, string conditionText)
        {
            btnEdit.Enabled = !string.IsNullOrEmpty(conditionText);
            btnRemove.Enabled = !string.IsNullOrEmpty(conditionText);
            btnSave.Enabled = !btnEdit.Enabled && !btnAdd.Enabled;
            btnCancel.Enabled = !btnAdd.Enabled;
            btnAdd.Enabled = !btnSave.Enabled && !btnEdit.Enabled;

            btnCopy.Enabled = false;
        }

        //public static List<tbl_DiscountType> Gettbl_DiscountType(this Form frm)
        //{
        //    List<tbl_DiscountType> ret = new List<tbl_DiscountType>();
        //    ret.Add(new tbl_DiscountType { DiscountTypeCode = "N", DiscountTypeName = "ไม่มี" });
        //    ret.Add(new tbl_DiscountType { DiscountTypeCode = "A", DiscountTypeName = "บาท" });
        //    ret.Add(new tbl_DiscountType { DiscountTypeCode = "P", DiscountTypeName = "เปอร์เซ็นต์" });
        //    ret.Add(new tbl_DiscountType { DiscountTypeCode = "Q", DiscountTypeName = "จำนวน" });

        //    return ret;
        //}

        public static void PrepareDocRunning(this BaseControl bu, string docTypeCode, bool isReverse = false)
        {
            bu.tbl_DocRunning.Clear();

            var docRunList = bu.tbl_DocRunning;

            CultureInfo cultures = System.Globalization.CultureInfo.GetCultureInfo("th-TH");

            string year = "";
            string month = "";
            DateTime cDate = DateTime.Now; // Convert.ToDateTime(DateTime.Now, cultures);

            year = cDate.ToString("yy", cultures);
            month = cDate.Month.ToString();

            Func<tbl_DocRunning, bool> tbl_DocRunningPre = (x => x.DocTypeCode.Replace(" ", string.Empty).Trim() == docTypeCode.Trim() && x.YearDoc.Replace(" ", string.Empty) == year && x.MonthDoc.Replace(" ", string.Empty) == month);
            var docRunningList = bu.GetDocRunning(tbl_DocRunningPre);

            //Func<tbl_POMaster, bool> tbl_POMasterPre = (x => x.DocTypeCode == docTypeCode);
            //var poList = bu.GetPOMaster(tbl_POMasterPre);
            var docList = new DocTypeCode().GenDocNo(docTypeCode);
            if (docList != null)
            {
                tbl_DocRunning docRunning = new tbl_DocRunning();
                if (docRunningList != null && docRunningList.Count > 0)
                {
                    docRunning = docRunningList[0];
                }
                else
                {
                    docRunning.DocTypeCode = docTypeCode;
                    docRunning.YearDoc = year;
                    docRunning.MonthDoc = month;
                    docRunning.WHCode = "";
                    docRunning.FlagSend = false;
                }

                docRunning.DayDoc = "0"; // CountDocDate(poList).ToString();

                string maxDocNo = docList;// bu.GenDocNo(docTypeCode); //GetMaxODDoc(bu, poList);
                string runDoc = "";

                var tbl_DocumentType = (new tbl_DocumentType()).SelectAll().FirstOrDefault(x => x.DocTypeCode.Trim() == docTypeCode.Trim());
                if (tbl_DocumentType != null)
                {
                    int runLength = tbl_DocumentType.RunLength.Value;
                    if (runLength != 0)
                        runDoc = maxDocNo.Substring(maxDocNo.Length - runLength, runLength);
                }

                if (!string.IsNullOrEmpty(runDoc))
                {
                    int runningNo = isReverse ? Convert.ToInt32(runDoc) - 1 : Convert.ToInt32(runDoc) + 1;
                    docRunning.RunDoc = runningNo;

                    docRunning.ModifiledDate = cDate.ToDateTimeFormat();

                    docRunList.Add(docRunning);
                }
            }
        }

        private static string GetMaxODDoc(BaseControl bu, List<tbl_POMaster> poList)
        {
            if (poList != null && poList.Count > 0)
            {
                return poList.Max(x => x.DocNo);
            }
            else
                return bu.GenDocNo("OD");
        }

        public static bool ConfirmMessageBox(this string msg, string title)
        {
            var confirmResult = FlexibleMessageBox.Show(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            return (confirmResult == DialogResult.Yes);
        }

        public static bool ValiadteDataGridView(this DataGridView grdList, List<tbl_Product> allProduct, int _prdCodeCell, int _uomCell, int _qtyCell, int _priceCell, BaseControl bu, string whid, bool isValidateOverRecieve = false, bool isRJ = false)
        {
            bool ret = true;

            var allprdUOMSets = bu.GetUOMSet();

            List<bool> checkEmptyCell = new List<bool>();
            List<bool> checkPrdCode = new List<bool>();
            List<bool> checkQty = new List<bool>();
            List<bool> checkPrice = new List<bool>();

            var validateRLList = new List<ValidateRL>();

            if (grdList.RowCount > 0)
            {
                //edit by sailom .k 13/12/2021----------------------------------------------------------
                //if (invWhItems.Count == 0 || isRJ)
                {
                    List<string> prdList = new List<string>();
                    for (int i = 0; i < grdList.RowCount; i++)
                    {
                        var prdCodeCell1 = grdList.Rows[i].Cells[_prdCodeCell];
                        var qtyCell1 = grdList.Rows[i].Cells[_qtyCell];
                        if (prdCodeCell1.IsNotNullOrEmptyCell() && qtyCell1.IsNotNullOrEmptyCell()) //check over recieve
                        {
                            var productID1 = prdCodeCell1.EditedFormattedValue.ToString();
                            prdList.Add(productID1);
                        }
                    }

                    if (prdList.Count > 0)
                    {
                        invWhItems = bu.GetTotalStockMovement(prdList, whid); //  edit by sailom 13/12/2021
                    }
                }
                //edit by sailom .k 13/12/2021----------------------------------------------------------

                for (int i = 0; i < grdList.RowCount; i++)
                {
                    var prdCodeCell = grdList.Rows[i].Cells[_prdCodeCell];
                    var qtyCell = grdList.Rows[i].Cells[_qtyCell];
                    var uomCell = grdList.Rows[i].Cells[_uomCell];

                    if (grdList.RowCount == 1 && !prdCodeCell.IsNotNullOrEmptyCell()) //check empty cell
                    {
                        checkEmptyCell.Add(false);
                    }

                    if (prdCodeCell.IsNotNullOrEmptyCell() && allProduct.Count(x => x.ProductCode == prdCodeCell.EditedFormattedValue.ToString()) == 0) //check product code
                    {
                        checkPrdCode.Add(false);
                    }

                    if (prdCodeCell.IsNotNullOrEmptyCell() && qtyCell.IsNotNullOrEmptyCell() && Convert.ToDecimal(qtyCell.EditedFormattedValue.ToString()) <= 0) //check qty
                    {
                        checkQty.Add(false);
                    }

                    if (!isValidateOverRecieve) //check unit price
                    {
                        var priceCell = grdList.Rows[i].Cells[_priceCell];
                        if (prdCodeCell.IsNotNullOrEmptyCell() && priceCell.IsNotNullOrEmptyCell() && Convert.ToDecimal(priceCell.EditedFormattedValue.ToString()) <= 0)
                        {
                            checkPrice.Add(false);
                        }
                    }

                    if (isValidateOverRecieve)
                    {
                        //var cell3 = grdList.Rows[i].Cells[3];
                        //var cell4 = grdList.Rows[i].Cells[4];

                        if (prdCodeCell.IsNotNullOrEmptyCell() && qtyCell.IsNotNullOrEmptyCell()) //check over recieve
                        {
                            decimal qtyValue = 0;
                            if (decimal.TryParse(qtyCell.EditedFormattedValue.ToString(), out qtyValue))
                            {
                                if (qtyValue > 0)
                                {
                                    var productID = prdCodeCell.EditedFormattedValue.ToString();

                                    var productUomName = uomCell.EditedFormattedValue.ToString();
                                    Func<tbl_ProductUom, bool> tbl_ProductUomPre = (x => x.ProductUomName == productUomName);
                                    var prdUOMs = bu.GetUOM(tbl_ProductUomPre);
                                    if (prdUOMs != null && prdUOMs.Count > 0)
                                    {
                                        decimal unitQty = 0;

                                        var prdUOMSets = bu.GetProductUOMSet(allprdUOMSets, productID);
                                        if (prdUOMSets != null && prdUOMSets.Count > 0 && prdUOMs != null && prdUOMs.Count > 0)
                                        {
                                            if (prdUOMs[0].ProductUomID == prdUOMSets[0].UomSetID) // prdUOMs[0].ProductUomID != 2)
                                                unitQty = (qtyValue * prdUOMSets[0].BaseQty);
                                            else
                                                unitQty = qtyValue;
                                        }
                                        else
                                            unitQty = qtyValue;

                                        //var invWhItem = bu.GetStockMovement(productID, whid); //  bu.GetInvWarehouse(productID, whid);

                                        //edit by sailom .k 13/12/2021----------------------------------------------------------
                                        decimal whQty = 0;
                                        if (invWhItems.Count > 0)
                                        {
                                            var invWhItem = invWhItems.FirstOrDefault(x => x.ProductID == productID);
                                            if (invWhItem != null)
                                                whQty = invWhItem.TrnQty; //edit by sailom .k 13/12/2021//.Sum(x => x.TrnQty);  //invWhItem[0].QtyOnHand;
                                        }
                                        //edit by sailom .k 13/12/2021----------------------------------------------------------

                                        if (unitQty > whQty)
                                        {
                                            validateRLList.Add(new ValidateRL
                                            {
                                                ProductID = productID,
                                                StockQty = whQty,
                                                InputQty = unitQty
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
                checkEmptyCell.Add(false);

            if (checkEmptyCell.Any(x => !x))
            {
                var message = "กรุณากรอกข้อมูลสินค้าให้ครบถ้วน !!!";
                message.ShowWarningMessage();
                ret = false;
            }
            if (ret && checkPrdCode.Any(x => !x))
            {
                var message = "รหัสสินค้าไม่ถูกต้อง กรุณาตรวจสอบอีกครั้ง !!!";
                message.ShowWarningMessage();
                ret = false;
            }
            if (ret && checkQty.Any(x => !x))
            {
                var message = "จำนวนสินค้าไม่ถูกต้อง กรุณาตรวจสอบอีกครั้ง !!!";
                message.ShowWarningMessage();
                ret = false;
            }

            if (!isValidateOverRecieve)
            {
                if (ret && checkPrice.Any(x => !x))
                {
                    var message = "ตรวจพบหน่วยละของสินค้าบางตัวเท่ากับ 0 !!!";
                    message.ShowWarningMessage();
                }
            }

            if (isValidateOverRecieve)
            {
                if (ret && validateRLList.Count > 0)
                {
                    var message = "";
                    var tempMsg = "";

                    if (isRJ)
                        tempMsg += string.Format("จำนวนทำลายต้องไม่เกินจำนวน Stock!!!\n");

                    tempMsg += string.Format("ไม่สามารถทำรายการได้ เนื่องจากสินค้าใน stock คลัง {0} มีไม่เพียงพอ \n\n", whid);
                    foreach (var item in validateRLList)
                    {
                        tempMsg += string.Format("--> {0} คงเหลือ {1} BPK จาก {2} BRK \n", item.ProductID, item.StockQty.ToStringN2(), item.InputQty.ToStringN2());
                    }
                    message = tempMsg;

                    message.ShowWarningMessage();
                    ret = false;
                }
            }

            return ret;
        }

        public static void SetErrMessage(this List<string> errList, Dictionary<Control, Label> ctrls)
        {
            foreach (KeyValuePair<Control, Label> item in ctrls)
            {
                errList.SetErrMessageList(item.Key, item.Value);
            }
        }

        public static decimal CalDiscountType(this BaseControl bu, string discountType, string _discountAmt, decimal orderQty, decimal unitPrice)
        {
            string lineDiscountType = "";
            var allLineDiscountType = bu.GetDiscountType();
            var ldt = allLineDiscountType.FirstOrDefault(x => x.DiscountTypeName == discountType);

            decimal discount = 0;

            if (ldt != null)
            {
                lineDiscountType = ldt.DiscountTypeCode;
                var cell8Value = Convert.ToDecimal(_discountAmt);

                switch (lineDiscountType)
                {
                    case "N": { discount = 0; } break;
                    case "A": { discount = cell8Value; } break;
                    case "P":
                        {
                            decimal total = (orderQty * unitPrice).ToDecimalN2();
                            discount = cell8Value;
                            decimal discountAmt = (total * discount) / 100;

                            discount = discountAmt;
                        }
                        break;
                    case "Q": { discount = cell8Value * orderQty; } break;
                    case "F": { discount = (cell8Value * unitPrice).ToDecimalN2(); } break;
                    default:
                        break;
                }
            }
            else
                discount = 0;

            return discount;
        }

        public static List<DataTable> ReadExcelToDataTable(this List<DataTable> dtList, string fileName, List<string> SheetName)
        {
            List<DataTable> DataList = new List<DataTable>();
            try
            {
                DataSet ds = new DataSet();

                OleDbDataAdapter cmd;

                string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + fileName + ";";
                connStr += "Extended Properties=Excel 12.0;";//Debug x86 

                OleDbConnection MyConnection = new OleDbConnection(connStr);

                foreach (var name in SheetName)
                {
                    ds = new DataSet();
                    string cSql = "SELECT * FROM " + name;
                    cmd = new OleDbDataAdapter(cSql, MyConnection);
                    cmd.Fill(ds, name);
                    DataList.Add(ds.Tables[0]);
                }

            }
            catch (Exception ex)
            {
                DataList = null;
            }

            return DataList;
        }

        public static List<DataTable> ReadExxel(this List<DataTable> dtList, string fileName, List<string> SheetName)
        {
            List<DataTable> DataList = new List<DataTable>();
            bool headers = true;

            try
            {
                var _xl = new Microsoft.Office.Interop.Excel.Application();
                var wb = _xl.Workbooks.Open(fileName);
                var sheets = wb.Sheets;
                DataSet dataSet = null;
                if (sheets != null && sheets.Count != 0)
                {
                    dataSet = new DataSet();
                    foreach (var item in sheets)
                    {
                        var sheet = (Microsoft.Office.Interop.Excel.Worksheet)item;
                        DataTable dt = null;
                        if (sheet != null)
                        {
                            dt = new DataTable();
                            var ColumnCount = ((Microsoft.Office.Interop.Excel.Range)sheet.UsedRange.Rows[1, Type.Missing]).Columns.Count;
                            var rowCount = ((Microsoft.Office.Interop.Excel.Range)sheet.UsedRange.Columns[1, Type.Missing]).Rows.Count;

                            for (int j = 0; j < ColumnCount; j++)
                            {
                                var cell = (Microsoft.Office.Interop.Excel.Range)sheet.Cells[1, j + 1];
                                var column = new DataColumn(headers ? cell.Value : string.Empty);
                                dt.Columns.Add(column);
                            }

                            for (int i = 0; i < rowCount; i++)
                            {
                                var r = dt.NewRow();
                                for (int j = 0; j < ColumnCount; j++)
                                {
                                    var cell = (Microsoft.Office.Interop.Excel.Range)sheet.Cells[i + 1 + (headers ? 1 : 0), j + 1];
                                    r[j] = cell.Value;
                                }
                                dt.Rows.Add(r);
                            }

                        }
                        DataList.Add(dt);
                    }
                }
                _xl.Quit();
            }
            catch (Exception ex)
            {
                DataList = null;
            }

            return DataList;
        }

        public static string[] GetRange(string range, Microsoft.Office.Interop.Excel.Worksheet excelWorksheet)
        {
            Microsoft.Office.Interop.Excel.Range workingRangeCells =
              excelWorksheet.get_Range(range, Type.Missing);
            //workingRangeCells.Select();

            System.Array array = (System.Array)workingRangeCells.Cells.Value2;
            string[] arrayS = (string[])array;

            return arrayS;
        }

        //private static int CountDocDate(List<tbl_POMaster> poList)
        //{
        //    if (poList != null && poList.Count > 0)
        //    {
        //        var docDateList = poList.Select(x => x.DocDate).ToList();

        //        docDateList.Add(dtpDocDate.Value.ToDateTimeFormat());

        //        int count = docDateList.Distinct().Count();

        //        return count;
        //    }
        //    else
        //        return 0;
        //}


        //private static void txtSupplierCode_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        TextBox txt = (TextBox)sender;
        //        if (!string.IsNullOrEmpty(txt.Text))
        //        {
        //            frm.BindSupplierData(txt.Text);
        //        }
        //    }
        //}

        //public static void BindSupplierData(this Form frm, string supplierCode)
        //{
        //    var supplier = odBU.GetSupplier(supplierCode);
        //    if (supplier != null)
        //    {
        //        _txtSupplierCode.Text = supplier.SupplierCode;
        //        _txtSuppName.Text = supplier.SuppName;
        //        _txtAddress.Text = supplier.BillTo;
        //        _txtContact.Text = supplier.Contact;
        //        _txtTelephone.Text = supplier.Telephone;
        //        creditDayCtrl.Value = supplier.CreditDay;
        //        _dtpDocDate.Value = DateTime.Now;

        //        UpdateDueDate();
        //    }
        //}

        //public static void ChangedDateTimePicker(this DateTimePicker dtpDocDate, NumericUpDown nudCreditDay, DateTimePicker dtpDueDate)
        //{
        //    creditDayCtrl = nudCreditDay;
        //    dueDateCtrl = dtpDueDate;
        //    //dtpDocDate.ValueChanged += dtpDocDate_ValueChanged;
        //}

        //private static void dtpDocDate_ValueChanged(object sender, EventArgs e)
        //{
        //    DateTimePicker dtp = (DateTimePicker)sender;
        //    _dtpDocDate = dtp;
        //    UpdateDueDate();
        //}

        //public static void ChangedDateTimePicker(this NumericUpDown nudCreditDay, DateTimePicker dtpDocDate, DateTimePicker dtpDueDate)
        //{
        //    dueDateCtrl = dtpDueDate;
        //    docDateCtrl = dtpDocDate;
        //    //creditDayCtrl.ValueChanged += nudCreditDay_ValueChanged;
        //}

        //private static void nudCreditDay_ValueChanged(object sender, EventArgs e)
        //{
        //    NumericUpDown nud = (NumericUpDown)sender;
        //    creditDayCtrl = nud;
        //    UpdateDueDate();
        //}

        //public static void UpdateDueDate()
        //{
        //    cueDateCtrl.Value = _dtpDocDate.Value.AddDays(Convert.ToInt32(creditDayCtrl.Value));
        //}


        //public static void SetSupplierCodeChange(this Form _frm, TextBox txtSupplierCode, TextBox txtSuppName, TextBox txtContact, 
        //    TextBox txtTelephone, NumericUpDown nudCreditDay, DateTimePicker dtpDocDate, DateTimePicker dtpDueDate)
        //{
        //    frm = _frm;

        //    _txtSupplierCode = txtSupplierCode;
        //    _txtSuppName = txtSuppName;
        //    _txtContact = txtContact;
        //    _txtTelephone = txtTelephone;
        //    _nudCreditDay = nudCreditDay;
        //    _dtpDocDate = dtpDocDate;
        //    _dtpDueDate = dtpDueDate;

        //    txtSupplierCode.KeyDown += txtSupplierCode_KeyDown;
        //}

        //public static void SetSupplierCodeChange(this Form _frm, TextBox txtSupplierCode, TextBox txtSuppName, TextBox txtAddress, TextBox txtContact,
        //    TextBox txtTelephone, NumericUpDown nudCreditDay, DateTimePicker dtpDocDate, DateTimePicker dtpDueDate)
        //{
        //    frm = _frm;

        //    _txtSupplierCode = txtSupplierCode;
        //    _txtSuppName = txtSuppName;
        //    _txtAddress = txtAddress;
        //    _txtContact = txtContact;
        //    _txtTelephone = txtTelephone;
        //    _nudCreditDay = nudCreditDay;
        //    _dtpDocDate = dtpDocDate;
        //    _dtpDueDate = dtpDueDate;

        //    txtSupplierCode.KeyDown += txtSupplierCode_KeyDown;
        //}


    }


}
