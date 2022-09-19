//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
//using CrystalDecisions.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
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

    public partial class Reporting : Form, IDisposable
    {
        private string formText = "";
        //private string formName = "";
        private string StoreName = "";
        private string ReportName = "";
        bool autoGenEx = false;
        bool nonPreView = false;
        private int m_currentPageIndex;
        private IList<Stream> m_streams;
        //private int m_EndPage;

        Dictionary<string, object> Params = new Dictionary<string, object>();
        private string ReportPath = ConfigurationManager.AppSettings["ReportPath"];
        private string XSLTPath = ConfigurationManager.AppSettings["XSLTPath"];
        //ReportDocument rprt = new ReportDocument();

        List<string> colsNameList = new List<string>();

        //private string empname = "";
        //private string van = "";
        //private string actualexvat_ex = "";
        //private string actualexvat = "";
        //private string actualperday = "";
        //private string percentage = "";
        //private string visited = "";
        //private string visit_per_day = "";
        //private string visit_perc = "";
        //private string bought = "";
        //private string boughtperday = "";
        //private string sku = "";
        //private string invoice = "";
        //private string sku_inv = "";

        //private string com = "";
        //private string perc_com = "";
        //private string actualinvat = "";

        //private string headerReportName = "";
        //private string headerDate = "";

        //private string headerRemark = "";
        public string excelName { get; set; }

        public Object ReportData { get; set; }

        CultureInfo newCulture = new CultureInfo("th-TH");
        bool isManyReport = false;

        public Reporting()
        {
            InitializeComponent();

        }

        public void LoadForm()
        {
            this.Reporting_Load(null, null);
        }

        public void PrepareReportNonPreViewPopup(string popUPText, string reportName, string storeName, Dictionary<string, object> _params, bool _autoGenExcel = false)
        {
            try
            {
                //formName = frmName;
                formText = popUPText;
                ReportName = reportName;
                StoreName = storeName;
                Params = _params;
                autoGenEx = _autoGenExcel;
                this.Text = popUPText;
                isManyReport = false;
                nonPreView = true;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
            }
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
                this.Text = popUPText;
                isManyReport = false;
                nonPreView = false;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
            }
        }

        public void PrepareMargeReportPopup(string popUPText, string reportName, string storeName, Dictionary<string, object> _params)
        {
            try
            {
                //formName = frmName;
                formText = popUPText;
                ReportName = reportName;
                StoreName = storeName;
                Params = _params;
                //autoGenEx = _autoGenExcel;
                this.Text = popUPText;
                isManyReport = false;
                nonPreView = false;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
            }
        }

        public void PrepareManyReportPopup(string popUPText, string reportName, string storeName, Dictionary<string, object> _params, bool _autoGenExcel = false)
        {
            try
            {
                //formName = frmName;
                formText = popUPText;
                ReportName = reportName;
                StoreName = storeName;
                Params = _params;
                autoGenEx = _autoGenExcel;
                this.Text = popUPText;
                isManyReport = true;
                nonPreView = false;
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
            }
        }

        private void Reporting_Load(object sender, EventArgs e)
        {
            if (isManyReport)
                ExportManyReport();
            else
            {
                if (nonPreView)
                    ExportReportNonPreView();
                else
                    ExportReport();
            }
        }

        private void ExportReport()
        {
            try
            {
                string _reportPath = ReportPath + ReportName;

                DataTable table = new DataTable();

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

                table = ds.Tables[0];
                table.TableName = StoreName;
                ReportData = table;

                var rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", this.ReportData);

                this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.ZoomPercent = 150;

                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(rds);

                reportViewer1.LocalReport.ReportPath = _reportPath;

                this.reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
            }
            this.reportViewer1.RefreshReport();
            //this.reportViewer2.RefreshReport();
            //this.reportViewer1.RefreshReport();
        }

        private void ExportManyReport()
        {
            try
            {
                string _reportPath = ReportPath + ReportName;

                DataTable table = new DataTable();

                DataSet ds = new DataSet();
                List<DataTable> listDT = new List<DataTable>();
                List<Microsoft.Reporting.WinForms.ReportDataSource> sourceList = new List<Microsoft.Reporting.WinForms.ReportDataSource>();
                var rds = new Microsoft.Reporting.WinForms.ReportDataSource();

                if (Params.Values.Any(x => x is List<string>))
                {
                    using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
                    {
                        int index = 1;
                        //foreach (var item in Params)
                        {
                            foreach (var item2 in (List<string>)Params.First().Value)
                            {
                                SqlCommand cmd = new SqlCommand(StoreName, con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandTimeout = 0;
                                cmd.Parameters.Add(new SqlParameter(Params.First().Key.ToString(), item2.ToString()));

                                var docType = (string)(Params.Last().Value);
                                cmd.Parameters.Add(new SqlParameter(Params.Last().Key.ToString(), docType));

                                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                                sda.Fill(ds, StoreName);
                                table = ds.Tables[0];
                                table.TableName = StoreName;
                                ReportData = table;

                                string dsName = "DataSet" + index.ToString();
                                Microsoft.Reporting.WinForms.ReportDataSource _rds = new Microsoft.Reporting.WinForms.ReportDataSource(dsName, this.ReportData);
                                sourceList.Add(_rds);

                                index++;
                            }
                        }
                    }

                    this.reportViewer1.ProcessingMode = ProcessingMode.Local;
                    this.reportViewer1.Reset();
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = _reportPath;
                    this.reportViewer1.LocalReport.ReportPath = _reportPath;
                    this.reportViewer1.LocalReport.DataSources.Clear();
                    //this.reportViewer1.LocalReport.DataSources.Clear();
                    this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                    this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                    this.reportViewer1.ZoomPercent = 150;

                    foreach (var rdsItem in sourceList)
                    {
                        rds = new Microsoft.Reporting.WinForms.ReportDataSource(rdsItem.Name, rdsItem.Value);


                        this.reportViewer1.LocalReport.DataSources.Add(rds);
                    }

                    this.reportViewer1.Refresh();
                }
                else
                {
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

                    table = ds.Tables[0];
                    table.TableName = StoreName;
                    ReportData = table;

                    rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", this.ReportData);

                    this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                    this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                    this.reportViewer1.ZoomPercent = 150;

                    this.reportViewer1.LocalReport.DataSources.Clear();
                    this.reportViewer1.LocalReport.DataSources.Add(rds);
                }

                //reportViewer1.LocalReport.ReportPath = _reportPath;

                //this.reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
            }
            this.reportViewer1.RefreshReport();
            //this.reportViewer2.RefreshReport();
            //this.reportViewer1.RefreshReport();
        }

        private void ExportReportNonPreView()
        {
            try
            {
                string _reportPath = ReportPath + ReportName;

                DataTable table = new DataTable();

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

                table = ds.Tables[0];
                table.TableName = StoreName;
                ReportData = table;

                LocalReport report = new LocalReport();
                report.ReportPath = _reportPath;
                report.DataSources.Add(new ReportDataSource("DataSet1", ReportData));

                if (formText == "ใบคุมส่งสินค้าตามคลังรถ")
                    PrintRDLCReport.PrintToPrinter(report, true);
                else
                    PrintRDLCReport.PrintToPrinter(report);
                //Export(report);

                //Print();
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
            }
        }

        // Routine to provide to the report renderer, in order to
        //    save an image for each page of the report.
        private Stream CreateStream(string name,
          string fileNameExtension, Encoding encoding,
          string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        // Export the given report as an EMF (Enhanced Metafile) file.
        private void Export(LocalReport report)
        {

            string deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>8.5in</PageWidth>
                <PageHeight>11in</PageHeight>
                <MarginTop>0.25in</MarginTop>
                <MarginLeft>0.25in</MarginLeft>
                <MarginRight>0.25in</MarginRight>
                <MarginBottom>0.25in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream,
               out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        public void PrintPage(object sender, PrintPageEventArgs e)
        {
            Stream pageToPrint = m_streams[m_currentPageIndex];
            pageToPrint.Position = 0;

            // Load each page into a Metafile to draw it.
            using (Metafile pageMetaFile = new Metafile(pageToPrint))
            {
                System.Drawing.Rectangle adjustedRect = new System.Drawing.Rectangle(
                        e.PageBounds.Left - (int)e.PageSettings.HardMarginX,
                        e.PageBounds.Top - (int)e.PageSettings.HardMarginY,
                        e.PageBounds.Width,
                        e.PageBounds.Height);

                // Draw a white background for the report
                e.Graphics.FillRectangle(Brushes.White, adjustedRect);

                // Draw the report content
                e.Graphics.DrawImage(pageMetaFile, adjustedRect);

                // Prepare for next page.  Make sure we haven't hit the end.
                m_currentPageIndex++;
                e.HasMorePages = m_currentPageIndex < m_streams.Count;
            }
        }

        public void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }

        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }

        //private DataTable LoadSalesData()
        //{
        //    // Create a new DataSet and read sales data file 
        //    //    data.xml into the first DataTable.
        //    DataSet dataSet = new DataSet();
        //    dataSet.ReadXml(@"..\..\data.xml");
        //    return dataSet.Tables[0];
        //}

        //private void PrintOld()
        //{
        //    if (m_streams == null || m_streams.Count == 0)
        //        throw new Exception("Error: no stream to print.");
        //    PrintDocument printDoc = new PrintDocument();
        //    if (!printDoc.PrinterSettings.IsValid)
        //    {
        //        throw new Exception("Error: cannot find the default printer.");
        //    }
        //    else
        //    {
        //        printDoc.PrintPage += new PrintPageEventHandler(PrintPage);

        //        m_currentPageIndex = 0;

        //        var page = printDoc.PrinterSettings.FromPage - 1;
        //        var m_EndPage = printDoc.PrinterSettings.ToPage - 1;
        //        var m_CurrentPageIndex = page;

        //        //m_currentPageIndex = 0;
        //        printDoc.Print();
        //    }
        //}

        //// Handler for PrintPageEvents
        //private void PrintPageOld(object sender, PrintPageEventArgs ev)
        //{
        //    Metafile pageImage = new
        //       Metafile(m_streams[m_currentPageIndex]);

        //    // Adjust rectangular area with printer margins.
        //    System.Drawing.Rectangle adjustedRect = new System.Drawing.Rectangle(
        //        ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
        //        ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
        //        ev.PageBounds.Width,
        //        ev.PageBounds.Height);

        //    // Draw a white background for the report
        //    ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

        //    // Draw the report content
        //    ev.Graphics.DrawImage(pageImage, adjustedRect);

        //    // Prepare for the next page. Make sure we haven't hit the end.
        //    m_currentPageIndex++;
        //    ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        //}

        //public void PrintPage2(object sender, PrintPageEventArgs e)
        //{
        //    Stream pageToPrint = m_streams[m_currentPageIndex];
        //    pageToPrint.Position = 0;

        //    // Load each page into a Metafile to draw it.
        //    using (Metafile pageMetaFile = new Metafile(pageToPrint))
        //    {
        //        System.Drawing.Rectangle adjustedRect = new System.Drawing.Rectangle(
        //                e.PageBounds.Left - (int)e.PageSettings.HardMarginX,
        //                e.PageBounds.Top - (int)e.PageSettings.HardMarginY,
        //                e.PageBounds.Width,
        //                e.PageBounds.Height);

        //        // Draw a white background for the report
        //        e.Graphics.FillRectangle(Brushes.White, adjustedRect);

        //        // Draw the report content
        //        e.Graphics.DrawImage(pageMetaFile, adjustedRect);

        //        // Prepare for next page.  Make sure we haven't hit the end.

        //        m_currentPageIndex++;

        //        e.HasMorePages = m_currentPageIndex < m_streams.Count;

        //        if (m_currentPageIndex > m_EndPage) e.HasMorePages = false;
        //    }
        //}




        //private void TestReport_Load(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string _reportPath = ReportPath + ReportName;

        //        DataTable table = new DataTable();

        //        DataSet ds = new DataSet();

        //        using (SqlConnection con = new SqlConnection(Connection.ConnectionString))
        //        {
        //            SqlCommand cmd = new SqlCommand(StoreName, con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandTimeout = 0;
        //            foreach (var item in Params)
        //            {
        //                cmd.Parameters.Add(new SqlParameter(item.Key, item.Value));
        //            }

        //            SqlDataAdapter sda = new SqlDataAdapter(cmd);

        //            sda.Fill(ds, StoreName);
        //        }

        //        table = ds.Tables[0];
        //        table.TableName = StoreName;
        //        ReportData = table;

        //        var rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", this.ReportData);

        //        this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
        //        this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
        //        this.reportViewer1.ZoomPercent = 150;

        //        this.reportViewer1.LocalReport.DataSources.Clear();
        //        this.reportViewer1.LocalReport.DataSources.Add(rds);

        //        reportViewer1.LocalReport.ReportPath = _reportPath;

        //        this.reportViewer1.RefreshReport();

        //    }
        //    catch (Exception ex)
        //    {
        //        ex.WriteLog(this.GetType());
        //    }
        //    this.reportViewer1.RefreshReport();
        //    //this.reportViewer2.RefreshReport();
        //    //this.reportViewer1.RefreshReport();
        //}

        //private ParameterField SetCrystalParam(string paramName, string paramValue)
        //{
        //    ParameterField pfItemYr = new ParameterField();

        //    try
        //    {
        //        pfItemYr.ParameterFieldName = paramName;

        //        ParameterDiscreteValue dcItemYr = new ParameterDiscreteValue();

        //        dcItemYr.Value = paramValue;

        //        pfItemYr.CurrentValues.Add(dcItemYr);

        //        return pfItemYr;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.WriteLog(this.GetType());

        //        //string msg = ex.Message;
        //        //msg.ShowErrorMessage();

        //        return new ParameterField();
        //    }
        //}

    }
}
