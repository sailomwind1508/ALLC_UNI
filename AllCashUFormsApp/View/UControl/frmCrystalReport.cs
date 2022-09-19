
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using DataTable = System.Data.DataTable;

namespace AllCashUFormsApp.View.UControl
{
    public partial class frmCrystalReport : Form
    {
        private string formText = "";
        //private string formName = "";
        private string StoreName = "";
        private string ReportName = "";
        bool autoGenEx = false;
        Dictionary<string, object> Params = new Dictionary<string, object>();
        private string ReportPath = ConfigurationManager.AppSettings["ReportPath"];
        private string XSLTPath = ConfigurationManager.AppSettings["XSLTPath"];
        //ReportDocument rprt = new ReportDocument();

        List<string> colsNameList = new List<string>();

        private string empname = "";
        private string van = "";
        private string actualexvat_ex = "";
        private string actualexvat = "";
        private string actualperday = "";
        private string percentage = "";
        private string visited = "";
        private string visit_per_day = "";
        private string visit_perc = "";
        private string bought = "";
        private string boughtperday = "";
        private string sku = "";
        private string invoice = "";
        private string sku_inv = "";

        private string com = "";
        private string perc_com = "";
        private string actualinvat = "";

        private string headerReportName = "";
        private string headerDate = "";

        private string headerRemark = "";
        public string excelName { get; set; }



        CultureInfo newCulture = new CultureInfo("th-TH");

        public frmCrystalReport()
        {
            InitializeComponent();
        }

        private void frmCrystalReport_Load(object sender, EventArgs e)
        {

        }

        public void PrepareReportPopup(string popUPText, string reportName, string storeName, Dictionary<string, object> _params, bool _autoGenExcel = false)
        {
            try
            {
                //formName = frmName;
                formText = popUPText;
                ReportName = reportName;
                StoreName = storeName;
                Params = _params;
                autoGenEx = _autoGenExcel;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
            }
        }

        public void PrepareExcelReportPopup(string popUPText, string reportName, string storeName, Dictionary<string, object> _params, bool _autoGenExcel = false)
        {
            try
            {
                //formName = frmName;
                formText = popUPText;
                ReportName = reportName;
                StoreName = storeName;
                Params = _params;
                autoGenEx = _autoGenExcel;

                //PrepareCrytalToExcelReport();
                CreateExcelFromXSLT(popUPText);
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
            }
        }

        public void PrepareManualExcelReportPopup(string popUPText, string reportName, string storeName, Dictionary<string, object> _params, bool _autoGenExcel = false)
        {
            try
            {
                //formName = frmName;
                formText = popUPText;
                ReportName = reportName;
                StoreName = storeName;
                Params = _params;
                autoGenEx = _autoGenExcel;

                if (formText == "รายงานสัดส่วน")
                {
                    empname = "ชื่อพนักงานขาย";
                    van = "Van";
                    actualexvat_ex = "Actual (Exc Vat)";
                    actualexvat = "Actual (Inc Vat)";
                    actualperday = "Act./ Day";
                    percentage = "% Count./Van.";
                    visited = "Visited";
                    visit_per_day = "Visit/Day";
                    visit_perc = "% Visited";
                    bought = "Bought";
                    boughtperday = "Bought/Day";
                    sku = "SKU";
                    invoice = "Invoice";
                    sku_inv = "SKU./Invoice";

                    headerReportName = "รายงานสัดส่วน";
                    headerDate = "วันที่ : ";

                    colsNameList = new List<string> { empname, van, actualexvat_ex, actualexvat, actualperday, percentage, visited, visit_per_day, visit_perc, bought, boughtperday, sku, invoice, sku_inv };

                    ExportRatioToExcel();
                }
                else if (formText == "รายงานสัดส่วน(KPI)")
                {
                    empname = "ชื่อพนักงานขาย";
                    van = "Van";
                    actualexvat = "Actual(Exc Vat)";
                    actualinvat = "Actual(Inc Vat)";
                    bought = "Bought(นับซ้ำ)";
                    sku = "SKU";
                    visited = "Visited";
                    com = "Com";
                    perc_com = "% Com";
                    invoice = "Invoice";
                    sku_inv = "SKU./Invoice";

                    headerReportName = "รายงานสัดส่วน(KPI)";
                    headerDate = "วันที่ : ";

                    colsNameList = new List<string> { empname, van, actualexvat, actualinvat, bought, sku, visited, com, perc_com, invoice, sku_inv };

                    ExportRatioKPIToExcel();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
            }
        }

        public void PrepareManualExcelCenterReportPopup(string popUPText, string reportName, string storeName, Dictionary<string, object> _params, bool _autoGenExcel = false)
        {
            try
            {
                //formName = frmName;
                formText = popUPText;
                ReportName = reportName;
                StoreName = storeName;
                Params = _params;
                autoGenEx = _autoGenExcel;

                if (formText == "รายงานสัดส่วน")
                {
                    empname = "ชื่อศูนย์";
                    van = "แวน";
                    actualexvat_ex = "Actual (Exc Vat)";
                    actualexvat = "Actual (Inc Vat)";
                    actualperday = "Act./ Day";
                    percentage = "% Count./Van.";
                    visited = "Visited";
                    visit_per_day = "Visit/Day";
                    visit_perc = "% Visited";
                    bought = "Bought";
                    boughtperday = "Bought/Day";
                    sku = "SKU";
                    invoice = "Invoice";
                    sku_inv = "SKU./Invoice";

                    headerReportName = "รายงานสัดส่วน";
                    headerDate = "วันที่ : ";

                    colsNameList = new List<string> { empname, van, actualexvat_ex, actualexvat, actualperday, percentage, visited, visit_per_day, visit_perc, bought, boughtperday, sku, invoice, sku_inv };

                    ExportRatioCenterToExcel();
                }
                else if (formText == "รายงานสัดส่วน(KPI)")
                {
                    empname = "ชื่อศูนย์";
                    van = "แวน";
                    actualexvat = "Actual(Exc Vat)";
                    actualinvat = "Actual(Inc Vat)";
                    bought = "Bought(นับซ้ำ)";
                    sku = "SKU";
                    visited = "Visited";
                    com = "Com";
                    perc_com = "% Com";
                    invoice = "Invoice";
                    sku_inv = "SKU./Invoice";

                    headerReportName = "รายงานสัดส่วน(KPI)";
                    headerDate = "วันที่ : ";

                    colsNameList = new List<string> { empname, van, actualexvat, actualinvat, bought, sku, visited, com, perc_com, invoice, sku_inv };

                    ExportRatioKPICenterToExcel();
                }
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
            }
        }


        public void PrepareExcelReportWithDTPopup(string popUPText, string reportName, DataTable dt, bool _autoGenExcel = false)
        {
            try
            {
                //formName = frmName;
                formText = popUPText;
                ReportName = reportName;
                //StoreName = storeName;
                //Params = _params;
                autoGenEx = _autoGenExcel;

                //PrepareCrytalToExcelReport();
                CreateExcelFromXSLT(popUPText, dt);
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
            }
        }

        private void frmCrystalReport_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private XPathDocument GetDocument(DataTable dt)
        {
            using (StringWriter sw = new StringWriter())
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                ds.WriteXml(sw);
                using (StringReader sr = new StringReader(sw.ToString()))
                {
                    return new XPathDocument(sr);
                }
            }
        }

        private void CreateExcelFromXSLT(string excelName, DataTable dt)
        {
            try
            {
                string _reportPath = XSLTPath + ReportName;

                XPathDocument input = GetDocument(dt);
                string dir = @"C:\AllCashExcels";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                string cDate = DateTime.Now.ToString("yyMMddhhmmss");
                if (formText != "ใบรับสินค้า")
                {
                    excelName = string.Join("", excelName, '_', cDate);
                }
                else
                    excelName = "Sheet1";

                string _excelName = dir + @"\" + excelName + ".xls";
                FormHelper.PrintingReportName.Add(_excelName);

                using (FileStream output = new FileStream(_excelName, FileMode.Create))
                {
                    XslCompiledTransform xslt = new XslCompiledTransform();
                    xslt.Load(_reportPath);
                    xslt.Transform(input, null, output);
                }

                if (FormHelper.ShowPrintingReportName)
                    System.Diagnostics.Process.Start(_excelName);
            }
            catch (Exception ex)
            {

                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private XPathDocument GetDocument(DataSet ds)
        {
            using (StringWriter sw = new StringWriter())
            {
                ds.WriteXml(sw);
                using (StringReader sr = new StringReader(sw.ToString()))
                {
                    return new XPathDocument(sr);
                }
            }
        }

        private void CreateExcelFromXSLT(string excelName)
        {
            try
            {
                string _reportPath = XSLTPath + ReportName;

                DataSet ds = new DataSet();

                using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(StoreName, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    foreach (var item in Params)
                    {
                        cmd.Parameters.Add(new SqlParameter(item.Key, item.Value));
                    }

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);

                    sda.Fill(ds, StoreName);
                }

                XPathDocument input = GetDocument(ds);
                string dir = @"C:\AllCashExcels";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                string cDate = DateTime.Now.ToString("yyMMddhhmmss");
                if (formText != "ใบรับสินค้า")
                {
                    excelName = string.Join("", excelName, '_', cDate);
                }
                else
                    excelName = "Sheet1";

                string _excelName = dir + @"\" + excelName + ".xls";
                FormHelper.PrintingReportName.Add(_excelName);

                using (FileStream output = new FileStream(_excelName, FileMode.Create))
                {
                    XslCompiledTransform xslt = new XslCompiledTransform();
                    xslt.Load(_reportPath);
                    xslt.Transform(input, null, output);
                }

                if (FormHelper.ShowPrintingReportName)
                    System.Diagnostics.Process.Start(_excelName);
            }
            catch (Exception ex)
            {

                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void PrintToExcel(string excelName)
        {
            try
            {
                ////string excelName = @"C:\AllCashExcels\test-report.xls";
                //string dir = @"C:\AllCashExcels";
                //if (!Directory.Exists(dir))
                //{
                //    Directory.CreateDirectory(dir);
                //}

                //string _excelName = dir + @"\" + excelName + ".xls";

                //ReportDocument cryRpt = rprt;

                //ParameterFields paramFields = new ParameterFields();
                //foreach (var item in Params)
                //{
                //    cryRpt.SetParameterValue(item.Key, item.Value.ToString());
                //}

                //ExportOptions CrExportOptions;

                //DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                //ExcelFormatOptions CrFormatTypeOptions = new ExcelFormatOptions();
                //CrDiskFileDestinationOptions.DiskFileName = _excelName;
                //CrExportOptions = cryRpt.ExportOptions;
                //CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                //CrExportOptions.ExportFormatType = ExportFormatType.Excel;
                //CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                //CrExportOptions.FormatOptions = CrFormatTypeOptions;
                //cryRpt.Export();

                ////open excel
                //System.Diagnostics.Process.Start(_excelName);

                ////Microsoft.Office.Interop.Excel.Application xlapp;
                ////Microsoft.Office.Interop.Excel.Workbook xlworkbook;
                ////xlapp = new Microsoft.Office.Interop.Excel.Application();

                ////xlworkbook = xlapp.Workbooks.Open(_excelName, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

                ////xlapp.Visible = true;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
        }

        private void PrepareCrytalToExcelReport()
        {
            try
            {
                //this.Text = formText;

                //string _reportPath = ReportPath + ReportName;

                //rprt.Load(_reportPath);

                //TableLogOnInfos crTableLogOnInfos = new TableLogOnInfos();

                //TableLogOnInfo crTableLogOnInfo = new TableLogOnInfo();
                //ConnectionInfo tConnInfo = new ConnectionInfo();

                //var conStr = Helper.ConnectionString;

                //string ServerName = conStr.Split('=')[4].Split(';')[0];
                //string DatabaseName = conStr.Split('=')[5].Split(';')[0];
                //string UserName = conStr.Split('=')[6].Split(';')[0];
                //string Password = conStr.Split('=')[7].Split(';')[0];

                //tConnInfo.ServerName = ServerName;
                //tConnInfo.DatabaseName = DatabaseName;
                //tConnInfo.UserID = UserName;
                //tConnInfo.Password = Password;
                //foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in rprt.Database.Tables)
                //{
                //    crTableLogOnInfo = crTable.LogOnInfo;
                //    crTableLogOnInfo.ConnectionInfo = tConnInfo;
                //    crTable.ApplyLogOnInfo(crTableLogOnInfo);
                //    crTableLogOnInfos.Add(crTableLogOnInfo);
                //}
                //crystalReportViewer1.LogOnInfo = crTableLogOnInfos;


                //DataSet ds = new DataSet();

                //using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
                //{
                //    SqlCommand cmd = new SqlCommand(StoreName, con);
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.CommandTimeout = 0;
                //    foreach (var item in Params)
                //    {
                //        cmd.Parameters.Add(new SqlParameter(item.Key, item.Value));
                //    }

                //    SqlDataAdapter sda = new SqlDataAdapter(cmd);

                //    sda.Fill(ds, StoreName);
                //}

                //ParameterFields paramFields = new ParameterFields();
                //foreach (var item in Params)
                //{
                //    paramFields.Add(SetCrystalParam(item.Key, item.Value.ToString()));
                //}

                ////string stashPrinterName = Printer.Session_DefaultPrinter;


                ////rprt.PrintOptions.PrinterName = stashPrinterName;
                ////rprt.PrintToPrinter(1, true, 1, 1);

                ////crystalReportViewer1.ParameterFieldInfo = paramFields;

                //rprt.SetDataSource(ds);

                ////crystalReportViewer1.ReportSource = rprt;

                ////crystalReportViewer1.RefreshReport();

                //PrintToExcel(ReportName.Split('.')[0].ToString());

            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                //string msg = ex.Message;
                //msg.ShowErrorMessage();
            }
        }

        #region รายงานสัดส่วน

        public bool ExportRatioCenterToExcel()
        {
            bool result = false;

            string _reportPath = XSLTPath + ReportName;

            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(StoreName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                foreach (var item in Params)
                {
                    cmd.Parameters.Add(new SqlParameter(item.Key, item.Value));
                }

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                sda.Fill(ds, StoreName);
            }

            XPathDocument input = GetDocument(ds);
            string dir = @"C:\AllCashExcels";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string cDate = DateTime.Now.ToString("yyMMddhhmmss");
            headerReportName = string.Join("", headerReportName, '_', cDate);
            string _excelName = dir + @"\" + headerReportName + ".xls";

            //Cursor.Current = Cursors.WaitCursor;
            //SaveFileDialog saveFileDialog1 = null;
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.ScreenUpdating = false;
            excel.Visible = false;
            excel.DisplayAlerts = false;
            excel.Interactive = false;

            Microsoft.Office.Interop.Excel.Workbook worKbooK = excel.Workbooks.Add(Type.Missing);

            int pid = -1;
            HandleRef hwnd = new HandleRef(excel, (IntPtr)excel.Hwnd);
            GetWindowThreadProcessId(hwnd, out pid);

            List<DataTable> dtList = new List<DataTable>();

            try
            {
                DataTable dt = ds.Tables[0];
                var enumList = dt.AsEnumerable().ToList();
                var dayGroup = enumList.GroupBy(x => x.Field<int>("day")).Select(y => new { data = y.First() }).ToList();
                //decimal countVan = Convert.ToDecimal(enumList.Select(x => x.Field<string>("whid")).Distinct().Count());

                DataTable dtClone = new DataTable();
                foreach (var item in colsNameList)
                {
                    dtClone.Columns.Add(item);
                }

                var _dayGroup = dayGroup.OrderByDescending(x => x.data.Field<int>("day")).ToList();
                int dof = _dayGroup.Count;
                foreach (var day in _dayGroup)
                {
                    var vanInDay = enumList.Where(a => a.Field<int>("day") == day.data.Field<int>("day")).ToList();
                    decimal countVan = Convert.ToDecimal(vanInDay.Select(x => x.Field<string>("BranchID")).Distinct().Count());

                    dtClone.Clear();

                    var tempDataRow = new List<DataRow>();
                    tempDataRow = dt.AsEnumerable().Where(x => x.Field<int>("day") == day.data.Field<int>("day")).ToList();

                    string sheetName = dof.ToString();// day.data.Field<int>("day").ToString(); //change request by manager UBN last edit by sailom 19/10/2021
                    if (day.data.Field<int>("day") == 99)
                    {
                        sheetName = "สัดส่วน";
                    }

                    dof--;

                    for (int i = 0; i < tempDataRow.Count; i++)
                    {
                        var r = tempDataRow[i];
                        dtClone.Rows.Add(r["branchname"].ToString(), r["BranchID"].ToString(), r["actualexvat"].ToString(), r["actualincvat"].ToString(), r["actualperday"].ToString(), r["percentage"].ToString()
                        , r["visit_cust"].ToString(), r["visit_cust"].ToString(), r["perc_visit"].ToString(), r["custbought"].ToString(), r["boughtperday"].ToString(), r["sku"].ToString()
                        , r["invoice"].ToString(), r["sku/inv"].ToString());
                    }
                    //Add total row
                    var total_actualexvat = tempDataRow.Sum(x => x.Field<decimal>("actualexvat"));
                    var total_actualincvat = tempDataRow.Sum(x => x.Field<decimal>("actualincvat"));
                    var total_actualperday = tempDataRow.Sum(x => x.Field<decimal>("actualperday"));
                    var total_percentage = tempDataRow.Sum(x => x.Field<decimal>("percentage"));
                    var total_visit_cust = tempDataRow.Sum(x => x.Field<decimal>("visit_cust"));
                    var total_visit_cust2 = tempDataRow.Sum(x => x.Field<decimal>("visit_cust"));

                    var total_custbought = tempDataRow.Sum(x => x.Field<decimal>("custbought"));
                    var total_boughtperday = tempDataRow.Sum(x => x.Field<decimal>("boughtperday"));
                    var total_sku = tempDataRow.Sum(x => x.Field<decimal>("sku"));
                    var total_invoice = tempDataRow.Sum(x => x.Field<decimal>("invoice"));
                    var total_sku_inv = tempDataRow.Sum(x => x.Field<decimal>("sku/inv"));
                    var total_perc_visit = total_invoice / (total_visit_cust == 0 ? 1 : total_visit_cust); //tempDataRow.Sum(x => x.Field<decimal>("perc_visit"));

                    dtClone.Rows.Add("TOTAL:", "-", total_actualexvat, total_actualincvat, total_actualperday, total_percentage, total_visit_cust, total_visit_cust2,
                        total_perc_visit, total_custbought, total_boughtperday, total_sku, total_invoice, total_sku_inv);

                    //Add ACG row
                    var avg_actualexvat = total_actualexvat / (countVan == 0 ? 1 : countVan);
                    var avg_actualincvat = total_actualincvat / (countVan == 0 ? 1 : countVan);
                    var avg_actualperday = total_actualperday / (countVan == 0 ? 1 : countVan);
                    var avg_percentage = total_percentage / (countVan == 0 ? 1 : countVan);
                    var avg_visit_cust = total_visit_cust / (countVan == 0 ? 1 : countVan);
                    var avg_visit_cust2 = total_visit_cust2 / (countVan == 0 ? 1 : countVan);

                    var avg_custbought = total_custbought / (countVan == 0 ? 1 : countVan);
                    var avg_boughtperday = total_boughtperday / (countVan == 0 ? 1 : countVan);
                    var avg_sku = total_sku / (countVan == 0 ? 1 : countVan);
                    var avg_invoice = total_invoice / (countVan == 0 ? 1 : countVan);
                    var avg_sku_inv = total_sku_inv / (countVan == 0 ? 1 : countVan);
                    var avg_perc_visit = avg_invoice / (avg_visit_cust == 0 ? 1 : avg_visit_cust);

                    dtClone.Rows.Add("AVG/VAN:", "-", avg_actualexvat, avg_actualincvat, avg_actualperday, avg_percentage, avg_visit_cust, avg_visit_cust2,
                        avg_perc_visit, avg_custbought, avg_boughtperday, avg_sku, avg_invoice, avg_sku_inv);

                    if (dtClone != null && dtClone.Rows.Count > 0)
                    {
                        DataRow row1 = dt.Rows[0];
                        //string fDate = tempDataRow.First()["HDate"].ToString();// _dayGroup.First().data.Field<string>("HDate");
                        //string lDate = tempDataRow.Last()["HDate"].ToString();// _dayGroup.Last().data.Field<string>("HDate");
                        //string headerDate = tempDataRow[0]["day"].ToString() == "99" ? fDate + "-" + lDate : tempDataRow[0]["HDate"].ToString();

                        string[] header = new string[] { sheetName, row1["CompanyName"].ToString(), "CENTER"
                        , row1["month"].ToString(), row1["year"].ToString(), tempDataRow[0]["HDate"].ToString()};

                        CreateRatioWorkSheet(worKbooK, dtClone, header);
                    }
                }

                worKbooK.SaveAs(_excelName);
                result = true;
            }
            catch (Exception ex)
            {
                //ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
            finally
            {
                worKbooK.Close(false, Type.Missing, Type.Missing);
                NAR(worKbooK);
                excel.Quit();
                NAR(excel);

                //Finally
                KillProcess(pid, "EXCEL");

                System.Diagnostics.Process.Start(_excelName);
            }

            return result;
        }

        public bool ExportRatioToExcel()
        {
            bool result = false;

            string _reportPath = XSLTPath + ReportName;

            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(StoreName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                foreach (var item in Params)
                {
                    cmd.Parameters.Add(new SqlParameter(item.Key, item.Value));
                }

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                sda.Fill(ds, StoreName);
            }

            XPathDocument input = GetDocument(ds);
            string dir = @"C:\AllCashExcels";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string cDate = DateTime.Now.ToString("yyMMddhhmmss");
            headerReportName = string.Join("", headerReportName, '_', cDate);
            string _excelName = dir + @"\" + headerReportName + ".xls";

            //Cursor.Current = Cursors.WaitCursor;
            //SaveFileDialog saveFileDialog1 = null;
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.ScreenUpdating = false;
            excel.Visible = false;
            excel.DisplayAlerts = false;
            excel.Interactive = false;

            Microsoft.Office.Interop.Excel.Workbook worKbooK = excel.Workbooks.Add(Type.Missing);

            int pid = -1;
            HandleRef hwnd = new HandleRef(excel, (IntPtr)excel.Hwnd);
            GetWindowThreadProcessId(hwnd, out pid);

            List<DataTable> dtList = new List<DataTable>();

            try
            {
                DataTable dt = ds.Tables[0];
                var enumList = dt.AsEnumerable().ToList();
                var dayGroup = enumList.GroupBy(x => x.Field<int>("day")).Select(y => new { data = y.First() }).ToList();
                //decimal countVan = Convert.ToDecimal(enumList.Select(x => x.Field<string>("whid")).Distinct().Count());

                DataTable dtClone = new DataTable();
                foreach (var item in colsNameList)
                {
                    dtClone.Columns.Add(item);
                }

                var _dayGroup = dayGroup.OrderByDescending(x => x.data.Field<int>("day")).ToList();
                int dof = _dayGroup.Count;
                foreach (var day in _dayGroup)
                {
                    var vanInDay = enumList.Where(a => a.Field<int>("day") == day.data.Field<int>("day")).ToList();
                    decimal countVan = Convert.ToDecimal(vanInDay.Select(x => x.Field<string>("whid")).Distinct().Count());

                    dtClone.Clear();

                    var tempDataRow = new List<DataRow>();
                    tempDataRow = dt.AsEnumerable().Where(x => x.Field<int>("day") == day.data.Field<int>("day")).ToList();
                    tempDataRow = tempDataRow.OrderBy(x => x.Field<string>("whid")).ToList(); //last edit by sailom .k 05/07/2022

                    string sheetName = dof.ToString();// day.data.Field<int>("day").ToString(); //change request by manager UBN last edit by sailom 19/10/2021
                    if (day.data.Field<int>("day") == 99)
                    {
                        sheetName = "สัดส่วน";
                    }

                    dof--;

                    for (int i = 0; i < tempDataRow.Count; i++)
                    {
                        var r = tempDataRow[i];
                        dtClone.Rows.Add(r["empname"].ToString(), r["van"].ToString(), r["actualexvat"].ToString(), r["actualincvat"].ToString(), r["actualperday"].ToString(), r["percentage"].ToString()
                        , r["visit_cust"].ToString(), r["visit_cust"].ToString(), r["perc_visit"].ToString(), r["custbought"].ToString(), r["boughtperday"].ToString(), r["sku"].ToString()
                        , r["invoice"].ToString(), r["sku/inv"].ToString());
                    }
                    //Add total row
                    var total_actualexvat = tempDataRow.Sum(x => x.Field<decimal>("actualexvat"));
                    var total_actualincvat = tempDataRow.Sum(x => x.Field<decimal>("actualincvat"));
                    var total_actualperday = tempDataRow.Sum(x => x.Field<decimal>("actualperday"));
                    var total_percentage = tempDataRow.Sum(x => x.Field<decimal>("percentage"));
                    var total_visit_cust = tempDataRow.Sum(x => x.Field<decimal>("visit_cust"));
                    var total_visit_cust2 = tempDataRow.Sum(x => x.Field<decimal>("visit_cust"));

                    var total_custbought = tempDataRow.Sum(x => x.Field<decimal>("custbought"));
                    var total_boughtperday = tempDataRow.Sum(x => x.Field<decimal>("boughtperday"));
                    var total_sku = tempDataRow.Sum(x => x.Field<decimal>("sku"));
                    var total_invoice = tempDataRow.Sum(x => x.Field<decimal>("invoice"));
                    var total_sku_inv = tempDataRow.Sum(x => x.Field<decimal>("sku/inv"));
                    var total_perc_visit = total_invoice / (total_visit_cust == 0 ? 1 : total_visit_cust); //tempDataRow.Sum(x => x.Field<decimal>("perc_visit"));

                    dtClone.Rows.Add("TOTAL:", "-", total_actualexvat, total_actualincvat, total_actualperday, total_percentage, total_visit_cust, total_visit_cust2,
                        total_perc_visit, total_custbought, total_boughtperday, total_sku, total_invoice, total_sku_inv);

                    //Add ACG row
                    var avg_actualexvat = total_actualexvat / (countVan == 0 ? 1 : countVan);
                    var avg_actualincvat = total_actualincvat / (countVan == 0 ? 1 : countVan);
                    var avg_actualperday = total_actualperday / (countVan == 0 ? 1 : countVan);
                    var avg_percentage = total_percentage / (countVan == 0 ? 1 : countVan);
                    var avg_visit_cust = total_visit_cust / (countVan == 0 ? 1 : countVan);
                    var avg_visit_cust2 = total_visit_cust2 / (countVan == 0 ? 1 : countVan);

                    var avg_custbought = total_custbought / (countVan == 0 ? 1 : countVan);
                    var avg_boughtperday = total_boughtperday / (countVan == 0 ? 1 : countVan);
                    var avg_sku = total_sku / (countVan == 0 ? 1 : countVan);
                    var avg_invoice = total_invoice / (countVan == 0 ? 1 : countVan);
                    var avg_sku_inv = total_sku_inv / (countVan == 0 ? 1 : countVan);
                    var avg_perc_visit = avg_invoice / (avg_visit_cust == 0 ? 1 : avg_visit_cust);

                    dtClone.Rows.Add("AVG/VAN:", "-", avg_actualexvat, avg_actualincvat, avg_actualperday, avg_percentage, avg_visit_cust, avg_visit_cust2,
                        avg_perc_visit, avg_custbought, avg_boughtperday, avg_sku, avg_invoice, avg_sku_inv);

                    if (dtClone != null && dtClone.Rows.Count > 0)
                    {
                        DataRow row1 = dt.Rows[0];
                        //string fDate = tempDataRow.First()["HDate"].ToString();// _dayGroup.First().data.Field<string>("HDate");
                        //string lDate = tempDataRow.Last()["HDate"].ToString();// _dayGroup.Last().data.Field<string>("HDate");
                        //string headerDate = tempDataRow[0]["day"].ToString() == "99" ? fDate + "-" + lDate : tempDataRow[0]["HDate"].ToString();

                        string[] header = new string[] { sheetName, row1["CompanyName"].ToString(), row1["branchname"].ToString()
                        , row1["month"].ToString(), row1["year"].ToString(), tempDataRow[0]["HDate"].ToString()};

                        CreateRatioWorkSheet(worKbooK, dtClone, header);
                    }
                }

                worKbooK.SaveAs(_excelName);
                result = true;
            }
            catch (Exception ex)
            {
                //ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
            finally
            {
                worKbooK.Close(false, Type.Missing, Type.Missing);
                NAR(worKbooK);
                excel.Quit();
                NAR(excel);

                //Finally
                KillProcess(pid, "EXCEL");

                System.Diagnostics.Process.Start(_excelName);
            }

            return result;
        }

        private void CreateRatioWorkSheet(Microsoft.Office.Interop.Excel.Workbook worKbooK, System.Data.DataTable dt, string[] headers)
        {
            Microsoft.Office.Interop.Excel.Worksheet worKsheeT;
            worKsheeT = (Microsoft.Office.Interop.Excel.Worksheet)worKbooK.ActiveSheet;

            try
            {
                var xlSheets = worKbooK.Sheets as Microsoft.Office.Interop.Excel.Sheets;
                var xlNewSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);
                xlNewSheet.Name = headers[0];
                worKsheeT = xlNewSheet;

                if (dt != null)
                {
                    worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[1, dt.Columns.Count]].Merge();
                    worKsheeT.Range[worKsheeT.Cells[2, 1], worKsheeT.Cells[2, dt.Columns.Count]].Merge();

                    worKsheeT.Cells[1, 1] = headers[1];
                    worKsheeT.Cells[2, 1] = headerReportName.Split('_')[0].ToString();
                    worKsheeT.Cells[3, 1] = "BB. : ";
                    worKsheeT.Cells[3, 2] = headers[2];
                    worKsheeT.Cells[3, 3] = "รอบที่ : ";
                    worKsheeT.Cells[3, 4] = headers[3] + "(" + headers[4] + ")";
                    worKsheeT.Cells[4, 1] = "วันที่ : ";
                    worKsheeT.Cells[4, 2] = headers[5].ToString();

                    worKsheeT.Cells.Font.Size = 11;
                    worKsheeT.Cells.Font.Color = System.Drawing.Color.Black;
                    worKsheeT.Cells.Font.Bold = true;

                    worKsheeT.Columns["A"].NumberFormat = "@";
                    worKsheeT.Columns["B"].NumberFormat = "mm/dd/yyyyy";
                    worKsheeT.Columns["C"].NumberFormat = "#,##0.00";
                    worKsheeT.Columns["D"].NumberFormat = "#,##0.00";
                    worKsheeT.Columns["E"].NumberFormat = "#,##0.00";
                    worKsheeT.Columns["F"].NumberFormat = "#,##0.00%";
                    worKsheeT.Columns["G"].NumberFormat = "#,##0.00";
                    worKsheeT.Columns["H"].NumberFormat = "#,##0.00";
                    worKsheeT.Columns["I"].NumberFormat = "#,##0.00%";
                    worKsheeT.Columns["J"].NumberFormat = "#,##0.00";
                    worKsheeT.Columns["K"].NumberFormat = "#,##0.00";
                    worKsheeT.Columns["L"].NumberFormat = "#,##0.00";
                    worKsheeT.Columns["M"].NumberFormat = "#,##0.00";
                    worKsheeT.Columns["N"].NumberFormat = "#,##0.00";

                    int rowCount = dt.Rows.Count;
                    int columnCount = dt.Columns.Count;

                    // Count of data tables provided.
                    int iterator = rowCount;

                    int rows = dt.Rows.Count;
                    int columns = dt.Columns.Count;
                    int beginRows = 6;

                    // Add the +1 to allow room for column headers.
                    var data = new object[rows + 1, columns];

                    for (int column = 0; column < colsNameList.Count; column++)
                    {
                        data[0, column] = colsNameList[column];
                    }

                    // Insert the provided records.
                    for (int row = 0; row < rows; row++)
                    {
                        for (var column = 0; column < columns; column++)
                        {
                            data[row + 1, column] = dt.Rows[row][column].ToString();
                        }
                    }

                    // Write this data to the excel worksheet.
                    Microsoft.Office.Interop.Excel.Range beginWrite = (Microsoft.Office.Interop.Excel.Range)worKsheeT.Cells[beginRows, 1];
                    Microsoft.Office.Interop.Excel.Range endWrite = (Microsoft.Office.Interop.Excel.Range)worKsheeT.Cells[rows + beginRows, columns];

                    Microsoft.Office.Interop.Excel.Range sheetData = worKsheeT.Range[beginWrite, endWrite];
                    sheetData.Value2 = data;

                    sheetData.Font.Size = 10;
                    sheetData.Font.Bold = false;

                    Microsoft.Office.Interop.Excel.Range beginWriteTT = (Microsoft.Office.Interop.Excel.Range)worKsheeT.Cells[(rows + beginRows) - 1, 1];
                    Microsoft.Office.Interop.Excel.Range endWriteTT = (Microsoft.Office.Interop.Excel.Range)worKsheeT.Cells[(rows + beginRows) - 1, columns];

                    Microsoft.Office.Interop.Excel.Range beginWriteAVG = (Microsoft.Office.Interop.Excel.Range)worKsheeT.Cells[(rows + beginRows), 1];
                    Microsoft.Office.Interop.Excel.Range endWriteAVG = (Microsoft.Office.Interop.Excel.Range)worKsheeT.Cells[(rows + beginRows), columns];

                    Microsoft.Office.Interop.Excel.Range total = worKsheeT.Range[beginWriteTT, endWriteTT];
                    Microsoft.Office.Interop.Excel.Range avg = worKsheeT.Range[beginWriteAVG, endWriteAVG];

                    total.Font.Bold = true;
                    avg.Font.Bold = true;

                    Microsoft.Office.Interop.Excel.Borders border = sheetData.Borders;
                    border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    border.Weight = 2d;

                    // Additional row, column and table formatting.
                    worKsheeT.Select();
                    sheetData.Worksheet.ListObjects.Add(XlListObjectSourceType.xlSrcRange,
                                                       sheetData,
                                                       System.Type.Missing,
                                                       XlYesNoGuess.xlYes,
                                                       System.Type.Missing).Name = headers[0];
                    sheetData.Select();


                    //SetHeaderText(worKsheeT, dt);
                    SetHeaderCellStyle(worKsheeT, dt);

                    Color c = new Color();
                    c = ColorTranslator.FromHtml("#C4D79B");
                    List<int> greenCols = new List<int>() { 6, 9 };
                    foreach (var item in greenCols)
                    {
                        Object perc = worKsheeT.Range[worKsheeT.Cells[beginRows, item], worKsheeT.Cells[rows + beginRows, item]];
                        Microsoft.Office.Interop.Excel.Range cellPerc = perc as Microsoft.Office.Interop.Excel.Range;
                        cellPerc.Interior.Color = ColorTranslator.ToOle(c);
                    }
                }
                else
                {
                    string msg = "ไม่พบข้อมูล Quarterly นี้ กรุณาลองตรวจสอบใหม่อีกครั้ง";
                    msg.ShowWarningMessage();
                }

            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
            finally
            {


            }

        }

        #endregion

        #region รายงานสัดส่วน(KPI)

        public bool ExportRatioKPICenterToExcel()
        {
            bool result = false;

            string _reportPath = XSLTPath + ReportName;

            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(StoreName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                foreach (var item in Params)
                {
                    cmd.Parameters.Add(new SqlParameter(item.Key, item.Value));
                }

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                sda.Fill(ds, StoreName);
            }

            XPathDocument input = GetDocument(ds);
            string dir = @"C:\AllCashExcels";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string cDate = DateTime.Now.ToString("yyMMddhhmmss");
            headerReportName = string.Join("", headerReportName, '_', cDate);
            string _excelName = dir + @"\" + headerReportName + ".xls";

            //Cursor.Current = Cursors.WaitCursor;
            //SaveFileDialog saveFileDialog1 = null;
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.ScreenUpdating = false;
            excel.Visible = false;
            excel.DisplayAlerts = false;
            excel.Interactive = false;

            Microsoft.Office.Interop.Excel.Workbook worKbooK = excel.Workbooks.Add(Type.Missing);

            int pid = -1;
            HandleRef hwnd = new HandleRef(excel, (IntPtr)excel.Hwnd);
            GetWindowThreadProcessId(hwnd, out pid);

            List<DataTable> dtList = new List<DataTable>();

            try
            {
                DataTable dt = ds.Tables[0];
                var enumList = dt.AsEnumerable().ToList();
                var dayGroup = enumList.GroupBy(x => x.Field<int>("day")).Select(y => new { data = y.First() }).ToList();

                DataTable dtClone = new DataTable();
                foreach (var item in colsNameList)
                {
                    dtClone.Columns.Add(item);
                }

                var _dayGroup = dayGroup.OrderByDescending(x => x.data.Field<int>("day")).ToList();
                int dof = _dayGroup.Count;
                foreach (var day in _dayGroup)
                {
                    var vanInDay = enumList.Where(a => a.Field<int>("day") == day.data.Field<int>("day")).ToList();
                    decimal countVan = Convert.ToDecimal(vanInDay.Select(x => x.Field<string>("BranchID")).Distinct().Count());

                    dtClone.Clear();

                    var tempDataRow = new List<DataRow>();
                    tempDataRow = dt.AsEnumerable().Where(x => x.Field<int>("day") == day.data.Field<int>("day")).ToList();
                    tempDataRow = tempDataRow.OrderBy(x => x.Field<string>("BranchID")).ToList();

                    string sheetName = dof.ToString();// day.data.Field<int>("day").ToString(); //change request by manager UBN last edit by sailom 19/10/2021
                    if (day.data.Field<int>("day") == 99)
                    {
                        sheetName = "สัดส่วน";
                    }

                    dof--;

                    for (int i = 0; i < tempDataRow.Count; i++)
                    {
                        var r = tempDataRow[i];

                        dtClone.Rows.Add(r["branchname"].ToString(), r["BranchID"].ToString(), r["actualexvat"].ToString(), r["actualincvat"].ToString(), r["CustBought"].ToString(), r["sku"].ToString()
                        , r["visited"].ToString(), r["com"].ToString(), r["perc_com"].ToString(), r["invoice"].ToString(), r["sku_inv"].ToString());
                    }

                    //Add total row
                    var total_actualexvat = tempDataRow.Sum(x => x.Field<decimal>("actualexvat"));
                    var total_actualincvat = tempDataRow.Sum(x => x.Field<decimal>("actualincvat"));
                    var total_bought = tempDataRow.Sum(x => x.Field<decimal>("CustBought"));
                    var total_sku = tempDataRow.Sum(x => x.Field<decimal>("sku"));
                    var total_visited = tempDataRow.Sum(x => x.Field<decimal>("visited"));
                    var total_com = tempDataRow.Sum(x => x.Field<decimal>("com"));
                    var total_perc_com = total_com / (total_visited == 0 ? 1 : total_visited);
                    var total_invoice = tempDataRow.Sum(x => x.Field<decimal>("invoice"));
                    var total_sku_inv = tempDataRow.Sum(x => x.Field<decimal>("sku_inv"));

                    dtClone.Rows.Add("TOTAL:", "-", total_actualexvat, total_actualincvat, total_bought, total_sku, total_visited, total_com,
                        total_perc_com, total_invoice, total_sku_inv);

                    //Add ACG row
                    var avg_actualexvat = total_actualexvat / (countVan == 0 ? 1 : countVan);
                    var avg_actualincvat = total_actualincvat / (countVan == 0 ? 1 : countVan);
                    var avg_bought = total_bought / (countVan == 0 ? 1 : countVan);
                    var avg_sku = total_sku / (countVan == 0 ? 1 : countVan);
                    var avg_visited = total_visited / (countVan == 0 ? 1 : countVan);
                    var avg_com = total_com / (countVan == 0 ? 1 : countVan);
                    var avg_perc_com = avg_com / (avg_visited == 0 ? 1 : avg_visited);
                    var avg_invoice = total_invoice / (countVan == 0 ? 1 : countVan);
                    var avg_sku_inv = total_sku_inv / (countVan == 0 ? 1 : countVan);

                    dtClone.Rows.Add("AVG/VAN:", "-", avg_actualexvat, avg_actualincvat, avg_bought, avg_sku, avg_visited, avg_com,
                        avg_perc_com, avg_invoice, avg_sku_inv);

                    if (dtClone != null && dtClone.Rows.Count > 0)
                    {
                        DataRow row1 = dt.Rows[0];
                        //string fDate = tempDataRow.First()["HDate"].ToString();
                        //string lDate = tempDataRow.Last()["HDate"].ToString();
                        //string headerDate = tempDataRow[0]["day"].ToString() == "99" ? fDate + "-" + lDate : tempDataRow[0]["HDate"].ToString();

                        string[] header = new string[] { sheetName, row1["CompanyName"].ToString(), "CENTER"
                        , row1["month"].ToString(), row1["year"].ToString(), tempDataRow[0]["HDate"].ToString()};

                        CreateRatioKPIWorkSheet(worKbooK, dtClone, header);
                    }
                }

                worKbooK.SaveAs(_excelName);
                result = true;
            }
            catch (Exception ex)
            {
                //ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
            finally
            {
                worKbooK.Close(false, Type.Missing, Type.Missing);
                NAR(worKbooK);
                excel.Quit();
                NAR(excel);

                //Finally
                KillProcess(pid, "EXCEL");

                System.Diagnostics.Process.Start(_excelName);
            }

            return result;
        }

        public bool ExportRatioKPIToExcel()
        {
            bool result = false;

            string _reportPath = XSLTPath + ReportName;

            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(StoreName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                foreach (var item in Params)
                {
                    cmd.Parameters.Add(new SqlParameter(item.Key, item.Value));
                }

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                sda.Fill(ds, StoreName);
            }

            XPathDocument input = GetDocument(ds);
            string dir = @"C:\AllCashExcels";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string cDate = DateTime.Now.ToString("yyMMddhhmmss");
            headerReportName = string.Join("", headerReportName, '_', cDate);
            string _excelName = dir + @"\" + headerReportName + ".xls";

            //Cursor.Current = Cursors.WaitCursor;
            //SaveFileDialog saveFileDialog1 = null;
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.ScreenUpdating = false;
            excel.Visible = false;
            excel.DisplayAlerts = false;
            excel.Interactive = false;

            Microsoft.Office.Interop.Excel.Workbook worKbooK = excel.Workbooks.Add(Type.Missing);

            int pid = -1;
            HandleRef hwnd = new HandleRef(excel, (IntPtr)excel.Hwnd);
            GetWindowThreadProcessId(hwnd, out pid);

            List<DataTable> dtList = new List<DataTable>();

            try
            {
                DataTable dt = ds.Tables[0];
                var enumList = dt.AsEnumerable().ToList();
                var dayGroup = enumList.GroupBy(x => x.Field<int>("day")).Select(y => new { data = y.First() }).ToList();

                DataTable dtClone = new DataTable();
                foreach (var item in colsNameList)
                {
                    dtClone.Columns.Add(item);
                }

                var _dayGroup = dayGroup.OrderByDescending(x => x.data.Field<int>("day")).ToList();
                int dof = _dayGroup.Count;
                foreach (var day in _dayGroup)
                {
                    var vanInDay = enumList.Where(a => a.Field<int>("day") == day.data.Field<int>("day")).ToList();
                    decimal countVan = Convert.ToDecimal(vanInDay.Select(x => x.Field<string>("whid")).Distinct().Count());

                    dtClone.Clear();

                    var tempDataRow = new List<DataRow>();
                    tempDataRow = dt.AsEnumerable().Where(x => x.Field<int>("day") == day.data.Field<int>("day")).ToList();
                    tempDataRow = tempDataRow.OrderBy(x => x.Field<string>("whid")).ToList();

                    string sheetName = dof.ToString();// day.data.Field<int>("day").ToString(); //change request by manager UBN last edit by sailom 19/10/2021
                    if (day.data.Field<int>("day") == 99)
                    {
                        sheetName = "สัดส่วน";
                    }

                    dof--;

                    for (int i = 0; i < tempDataRow.Count; i++)
                    {
                        var r = tempDataRow[i];

                        dtClone.Rows.Add(r["empname"].ToString(), r["whid"].ToString(), r["actualexvat"].ToString(), r["actualincvat"].ToString(), r["CustBought"].ToString(), r["sku"].ToString()
                        , r["visited"].ToString(), r["com"].ToString(), r["perc_com"].ToString(), r["invoice"].ToString(), r["sku_inv"].ToString());
                    }

                    //Add total row
                    var total_actualexvat = tempDataRow.Sum(x => x.Field<decimal>("actualexvat"));
                    var total_actualincvat = tempDataRow.Sum(x => x.Field<decimal>("actualincvat"));
                    var total_bought = tempDataRow.Sum(x => x.Field<decimal>("CustBought"));
                    var total_sku = tempDataRow.Sum(x => x.Field<decimal>("sku"));
                    var total_visited = tempDataRow.Sum(x => x.Field<decimal>("visited"));
                    var total_com = tempDataRow.Sum(x => x.Field<decimal>("com"));
                    var total_perc_com = total_com / (total_visited == 0 ? 1 : total_visited);
                    var total_invoice = tempDataRow.Sum(x => x.Field<decimal>("invoice"));
                    var total_sku_inv = tempDataRow.Sum(x => x.Field<decimal>("sku_inv"));

                    dtClone.Rows.Add("TOTAL:", "-", total_actualexvat, total_actualincvat, total_bought, total_sku, total_visited, total_com,
                        total_perc_com, total_invoice, total_sku_inv);

                    //Add ACG row
                    var avg_actualexvat = total_actualexvat / (countVan == 0 ? 1 : countVan);
                    var avg_actualincvat = total_actualincvat / (countVan == 0 ? 1 : countVan);
                    var avg_bought = total_bought / (countVan == 0 ? 1 : countVan);
                    var avg_sku = total_sku / (countVan == 0 ? 1 : countVan);
                    var avg_visited = total_visited / (countVan == 0 ? 1 : countVan);
                    var avg_com = total_com / (countVan == 0 ? 1 : countVan);
                    var avg_perc_com = avg_com / (avg_visited == 0 ? 1 : avg_visited);
                    var avg_invoice = total_invoice / (countVan == 0 ? 1 : countVan);
                    var avg_sku_inv = total_sku_inv / (countVan == 0 ? 1 : countVan);

                    dtClone.Rows.Add("AVG/VAN:", "-", avg_actualexvat, avg_actualincvat, avg_bought, avg_sku, avg_visited, avg_com,
                        avg_perc_com, avg_invoice, avg_sku_inv);

                    if (dtClone != null && dtClone.Rows.Count > 0)
                    {
                        DataRow row1 = dt.Rows[0];
                        //string fDate = tempDataRow.First()["HDate"].ToString();
                        //string lDate = tempDataRow.Last()["HDate"].ToString();
                        //string headerDate = tempDataRow[0]["day"].ToString() == "99" ? fDate + "-" + lDate : tempDataRow[0]["HDate"].ToString();

                        string[] header = new string[] { sheetName, row1["CompanyName"].ToString(), row1["branchname"].ToString()
                        , row1["month"].ToString(), row1["year"].ToString(), tempDataRow[0]["HDate"].ToString()};

                        CreateRatioKPIWorkSheet(worKbooK, dtClone, header);
                    }
                }

                worKbooK.SaveAs(_excelName);
                result = true;
            }
            catch (Exception ex)
            {
                //ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
            finally
            {
                worKbooK.Close(false, Type.Missing, Type.Missing);
                NAR(worKbooK);
                excel.Quit();
                NAR(excel);

                //Finally
                KillProcess(pid, "EXCEL");

                System.Diagnostics.Process.Start(_excelName);
            }

            return result;
        }

        private void CreateRatioKPIWorkSheet(Microsoft.Office.Interop.Excel.Workbook worKbooK, System.Data.DataTable dt, string[] headers)
        {
            Microsoft.Office.Interop.Excel.Worksheet worKsheeT;
            worKsheeT = (Microsoft.Office.Interop.Excel.Worksheet)worKbooK.ActiveSheet;

            try
            {
                var xlSheets = worKbooK.Sheets as Microsoft.Office.Interop.Excel.Sheets;
                var xlNewSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);
                xlNewSheet.Name = headers[0];
                worKsheeT = xlNewSheet;

                if (dt != null)
                {
                    worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[1, dt.Columns.Count]].Merge();
                    worKsheeT.Range[worKsheeT.Cells[2, 1], worKsheeT.Cells[2, dt.Columns.Count]].Merge();

                    worKsheeT.Cells[1, 1] = headers[1];
                    worKsheeT.Cells[2, 1] = headerReportName.Split('_')[0].ToString();
                    worKsheeT.Cells[3, 1] = "BB. : ";
                    worKsheeT.Cells[3, 2] = headers[2];
                    worKsheeT.Cells[3, 3] = "รอบที่ : ";
                    worKsheeT.Cells[3, 4] = headers[3] + "(" + headers[4] + ")";
                    worKsheeT.Cells[4, 1] = "วันที่ : ";
                    worKsheeT.Cells[4, 2] = headers[5].ToString();

                    worKsheeT.Cells.Font.Size = 11;
                    worKsheeT.Cells.Font.Color = System.Drawing.Color.Black;
                    worKsheeT.Cells.Font.Bold = true;

                    worKsheeT.Columns["A"].NumberFormat = "@";
                    worKsheeT.Columns["B"].NumberFormat = "mm/dd/yyyyy";
                    worKsheeT.Columns["C"].NumberFormat = "#,##0.00";
                    worKsheeT.Columns["D"].NumberFormat = "#,##0.00";
                    worKsheeT.Columns["E"].NumberFormat = "#,##0.00";
                    worKsheeT.Columns["F"].NumberFormat = "#,##0.00";
                    worKsheeT.Columns["G"].NumberFormat = "#,##0.00";
                    worKsheeT.Columns["H"].NumberFormat = "#,##0.00";
                    worKsheeT.Columns["I"].NumberFormat = "#,##0.00%";
                    worKsheeT.Columns["J"].NumberFormat = "#,##0.00";
                    worKsheeT.Columns["K"].NumberFormat = "#,##0.00";

                    int rowCount = dt.Rows.Count;
                    int columnCount = dt.Columns.Count;

                    // Count of data tables provided.
                    int iterator = rowCount;

                    int rows = dt.Rows.Count;
                    int columns = dt.Columns.Count;
                    int beginRows = 6;

                    // Add the +1 to allow room for column headers.
                    var data = new object[rows + 1, columns];

                    for (int column = 0; column < colsNameList.Count; column++)
                    {
                        data[0, column] = colsNameList[column];
                    }

                    // Insert the provided records.
                    for (int row = 0; row < rows; row++)
                    {
                        for (var column = 0; column < columns; column++)
                        {
                            data[row + 1, column] = dt.Rows[row][column].ToString();
                        }
                    }

                    // Write this data to the excel worksheet.
                    Microsoft.Office.Interop.Excel.Range beginWrite = (Microsoft.Office.Interop.Excel.Range)worKsheeT.Cells[beginRows, 1];
                    Microsoft.Office.Interop.Excel.Range endWrite = (Microsoft.Office.Interop.Excel.Range)worKsheeT.Cells[rows + beginRows, columns];

                    Microsoft.Office.Interop.Excel.Range sheetData = worKsheeT.Range[beginWrite, endWrite];
                    sheetData.Value2 = data;

                    sheetData.Font.Size = 10;
                    sheetData.Font.Bold = false;

                    Microsoft.Office.Interop.Excel.Range beginWriteTT = (Microsoft.Office.Interop.Excel.Range)worKsheeT.Cells[(rows + beginRows) - 1, 1];
                    Microsoft.Office.Interop.Excel.Range endWriteTT = (Microsoft.Office.Interop.Excel.Range)worKsheeT.Cells[(rows + beginRows) - 1, columns];

                    Microsoft.Office.Interop.Excel.Range beginWriteAVG = (Microsoft.Office.Interop.Excel.Range)worKsheeT.Cells[(rows + beginRows), 1];
                    Microsoft.Office.Interop.Excel.Range endWriteAVG = (Microsoft.Office.Interop.Excel.Range)worKsheeT.Cells[(rows + beginRows), columns];

                    Microsoft.Office.Interop.Excel.Range total = worKsheeT.Range[beginWriteTT, endWriteTT];
                    Microsoft.Office.Interop.Excel.Range avg = worKsheeT.Range[beginWriteAVG, endWriteAVG];

                    total.Font.Bold = true;
                    avg.Font.Bold = true;

                    Microsoft.Office.Interop.Excel.Borders border = sheetData.Borders;
                    border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    border.Weight = 2d;

                    // Additional row, column and table formatting.
                    worKsheeT.Select();
                    sheetData.Worksheet.ListObjects.Add(XlListObjectSourceType.xlSrcRange,
                                                       sheetData,
                                                       System.Type.Missing,
                                                       XlYesNoGuess.xlYes,
                                                       System.Type.Missing).Name = headers[0];
                    sheetData.Select();


                    //SetHeaderText(worKsheeT, dt);
                    SetHeaderCellStyle(worKsheeT, dt);

                    Color c = new Color();
                    c = ColorTranslator.FromHtml("#C4D79B");
                    List<int> greenCols = new List<int>() { 9 };
                    foreach (var item in greenCols)
                    {
                        Object perc = worKsheeT.Range[worKsheeT.Cells[beginRows, item], worKsheeT.Cells[rows + beginRows, item]];
                        Microsoft.Office.Interop.Excel.Range cellPerc = perc as Microsoft.Office.Interop.Excel.Range;
                        cellPerc.Interior.Color = ColorTranslator.ToOle(c);
                    }
                }
                else
                {
                    string msg = "ไม่พบข้อมูล Quarterly นี้ กรุณาลองตรวจสอบใหม่อีกครั้ง";
                    msg.ShowWarningMessage();
                }

            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());

                string msg = ex.Message;
                msg.ShowErrorMessage();
            }
            finally
            {


            }

        }

        #endregion

        private void SetHeaderText(Microsoft.Office.Interop.Excel.Worksheet worKsheeT, System.Data.DataTable dataTable)
        {
            //for (int i = 0; i < colsNameList.Count; i++)
            //{
            //    worKsheeT.Cells[2, (i + 1)] = colsNameList[i];
            //}
        }

        private void SetHeaderCellStyle(Microsoft.Office.Interop.Excel.Worksheet worKsheeT, System.Data.DataTable dataTable)
        {
            Color c = new Color();
            Color cblue = Color.Blue;
            c = ColorTranslator.FromHtml("#92CDDC");

            List<int> blueRows = new List<int>() { 1, 2 };
            foreach (var item in blueRows)
            {
                Object headerLine = worKsheeT.Range[worKsheeT.Cells[item, 1], worKsheeT.Cells[item, dataTable.Columns.Count]];
                Microsoft.Office.Interop.Excel.Range cellRang = headerLine as Microsoft.Office.Interop.Excel.Range;
                cellRang.Font.Color = ColorTranslator.ToOle(cblue);
                cellRang.Font.Bold = true;
            }

            Object headerLine3_1 = worKsheeT.Range[worKsheeT.Cells[3, 1], worKsheeT.Cells[3, 1]];
            Object headerLine3_12 = worKsheeT.Range[worKsheeT.Cells[3, 3], worKsheeT.Cells[3, 3]];
            Object headerLine4_1 = worKsheeT.Range[worKsheeT.Cells[4, 1], worKsheeT.Cells[4, 1]];
            Microsoft.Office.Interop.Excel.Range cellRang3_1 = headerLine3_1 as Microsoft.Office.Interop.Excel.Range;
            Microsoft.Office.Interop.Excel.Range cellRang3_12 = headerLine3_12 as Microsoft.Office.Interop.Excel.Range;
            Microsoft.Office.Interop.Excel.Range cellRang4_1 = headerLine4_1 as Microsoft.Office.Interop.Excel.Range;
            cellRang3_1.Borders.Weight = 2d;
            cellRang3_12.Borders.Weight = 2d;
            cellRang4_1.Borders.Weight = 2d;
            cellRang3_1.Interior.Color = ColorTranslator.ToOle(c);
            cellRang3_12.Interior.Color = ColorTranslator.ToOle(c);
            cellRang4_1.Interior.Color = ColorTranslator.ToOle(c);

            Object headerLine3_2 = worKsheeT.Range[worKsheeT.Cells[3, 2], worKsheeT.Cells[3, 2]];
            Object headerLine3_22 = worKsheeT.Range[worKsheeT.Cells[3, 4], worKsheeT.Cells[3, 4]];
            Object headerLine4_2 = worKsheeT.Range[worKsheeT.Cells[4, 2], worKsheeT.Cells[4, 2]];
            Microsoft.Office.Interop.Excel.Range cellRang3_2 = headerLine3_2 as Microsoft.Office.Interop.Excel.Range;
            Microsoft.Office.Interop.Excel.Range cellRang3_22 = headerLine3_22 as Microsoft.Office.Interop.Excel.Range;
            Microsoft.Office.Interop.Excel.Range cellRang4_2 = headerLine4_2 as Microsoft.Office.Interop.Excel.Range;
            cellRang3_2.Borders.Weight = 2d;
            cellRang3_22.Borders.Weight = 2d;
            cellRang4_2.Borders.Weight = 2d;
            cellRang3_2.Font.Bold = false;
            cellRang3_22.Font.Bold = false;
            cellRang4_2.Font.Bold = false;


            Object headerLine6 = worKsheeT.Range[worKsheeT.Cells[6, 1], worKsheeT.Cells[6, dataTable.Columns.Count]];
            Microsoft.Office.Interop.Excel.Range cellRang6 = headerLine6 as Microsoft.Office.Interop.Excel.Range;
            cellRang6.Interior.Color = ColorTranslator.ToOle(c);
            cellRang6.Font.Bold = true;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowThreadProcessId(HandleRef handle, out int processId);

        private void KillProcess(int pid, string processName)
        {
            // to kill current process of excel
            System.Diagnostics.Process[] AllProcesses = System.Diagnostics.Process.GetProcessesByName(processName);
            foreach (System.Diagnostics.Process process in AllProcesses)
            {
                if (process.Id == pid)
                {
                    process.Kill();
                }
            }
            AllProcesses = null;
        }

        private void NAR(object o)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(o);
            }
            catch { }
            finally
            {
                o = null;
            }
        }
    }
}
