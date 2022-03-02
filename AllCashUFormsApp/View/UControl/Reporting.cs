//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
//using CrystalDecisions.Windows.Forms;
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

    public partial class Reporting : Form
    {
        private string formText = "";
        private string formName = "";
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

        public Object ReportData { get; set; }

        CultureInfo newCulture = new CultureInfo("th-TH");


        public Reporting()
        {
            InitializeComponent();

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
            }
            catch (Exception ex)
            {
                ex.WriteLog(this.GetType());
            }
        }

        private void TestReport_Load(object sender, EventArgs e)
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
