using AllCashUFormsApp.Model;
using AllCashUFormsApp.View.UControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllCashUFormsApp.Controller;
using System.Globalization;

namespace AllCashUFormsApp.View.UControl
{
    public partial class frmSearchVE : Form
    {
        VE bu = new VE();
        string frmText = "";
        //DialogResult confirmResult = new DialogResult();

        public frmSearchVE()
        {
            InitializeComponent();
        }

        public void PreparePrintDocs(string printText)
        {
            frmText = printText;
            //confirmResult = _confirmResult;
        }

        private void BindVEData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ObjectFactory objectFactory = new ObjectFactory();
                var conditionString = new string[] { "19000101" };
                if (dtpDocDate.Enabled)
                {
                    var _docDate = dtpDocDate.Value.ToString("yyyyMMdd", new CultureInfo("en-US"));
                    conditionString = new string[] { _docDate };
                }

                DataRow[] filteredRows = null;
                var obj = objectFactory.Get(ObjectType.V, null);

                var dt = obj.GetDataTableByCondition(null);
                if (!string.IsNullOrEmpty(txtSearchTxt.Text))
                {
                    filteredRows = dt.AsEnumerable().Where(x => x.Field<string>("DocNo").Contains(txtSearchTxt.Text)).ToArray();

                    if (dtpDocDate.Enabled)
                        filteredRows = dt.AsEnumerable().Where(x => x.Field<string>("DocNo").Contains(txtSearchTxt.Text) && x.Field<DateTime>("DocDate").ToShortDateString() == dtpDocDate.Value.ToShortDateString()).ToArray();
                }
                else
                {
                    if (dtpDocDate.Enabled)
                        filteredRows = dt.AsEnumerable().Where(x => x.Field<DateTime>("DocDate").ToShortDateString() == dtpDocDate.Value.ToShortDateString()).ToArray();
                    else
                        filteredRows = null;
                }

                int rowCount = 0;
                if (filteredRows != null)
                {
                    var _dt = new DataTable();
                    _dt = dt.Clone();
                    _dt.Clear();

                    //DataRow[] rowList = filteredRows.ToArray();
                    //_dt.AddDataTableRow(ref rowList);
                    _dt.AddDataTableRow(ref filteredRows);

                    grdList.DataSource = _dt;
                    rowCount = _dt.Rows.Count;
                }
                else
                {
                    grdList.DataSource = dt;
                    rowCount = dt.Rows.Count;
                }

                lblCountList.Text = rowCount.ToNumberFormat();
                grdList.CreateCheckBoxHeaderColumn("colSelect");

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                ex.Message.ShowErrorMessage();
            }
        }

        private void Search()
        {
            BindVEData();
        }

        private void frmSearchVE_Load(object sender, EventArgs e)
        {
            this.Text = frmText;
            dtpDocDate.SetDateTimePickerFormat();
            grdList.AutoGenerateColumns = false;

            BindVEData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void txtSearchTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search();
            }
        }

        private void rdoN_CheckedChanged(object sender, EventArgs e)
        {
            dtpDocDate.Enabled = true;
        }

        private void rdoY_CheckedChanged(object sender, EventArgs e)
        {
            dtpDocDate.Enabled = false;
        }

        private void grdList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            grdList.SetRowPostPaint(sender, e, this.Font);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> selectList = new Dictionary<string, string>();
            for (int i = 0; i < grdList.Rows.Count; i++)
            {
                if (Convert.ToBoolean(grdList.Rows[i].Cells[0].Value) == true)
                {
                    selectList.Add(grdList.Rows[i].Cells[1].Value.ToString(), grdList.Rows[i].Cells[7].Value.ToString());
                }
            }
            //var joinString = string.Join(",", selectList);
            if (selectList.Count == 0)
            {
                string msg = "กรุณาเลือกเอกสารที่ต้องการพิมพ์!!!";
                msg.ShowWarningMessage();
                return;
            }
            if (selectList.Count > 50)
            {
                string msg = "ห้ามเลือกเอกสารพร้อมกันเกิน 50 ใบ!!!";
                msg.ShowWarningMessage();
                return;
            }

            string cfMsg = "ต้องการพิมพ์โดยที่ไม่ดูรายงานใช่หรือไม่?";
            string title = "ยืนยันการพิมพ์!!";
            var confirmResult = FlexibleMessageBox.Show(cfMsg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            Cursor.Current = Cursors.WaitCursor;

            try
            {
                foreach (var item in selectList)
                {
                    var docNoItem = item.Key;
                    var whid = item.Value;
                    var wh = bu.GetBranchWarehouse(whid);

                    if (wh != null && wh.DriverEmpID == "credit")
                    {
                        Dictionary<string, object> _params = new Dictionary<string, object>();
                        _params.Add("@DocNo", docNoItem);

                        PrintCredit(whid, docNoItem, confirmResult);
                    }
                    else
                    {
                        Dictionary<string, object> _params = new Dictionary<string, object>();
                        _params.Add("@DocNo", docNoItem);
                        if (frmText == "พิมพ์ทั้งหมด")
                        {
                            if (confirmResult == DialogResult.Yes) //No Preview
                            {
                                _params = new Dictionary<string, object>();
                                _params.Add("@DocNo", docNoItem);
                                _params.Add("@ReportType", "ต้นฉบับใบกำกับภาษี/ต้นฉบับใบเสร็จรับเงิน");
                                this.OpenReportingReportsNonPreViewPopup("ใบกำกับภาษีเต็มรูป/ใบเสร็จรับเงิน(ต้นฉบับ)", "From_V.rdlc", "Form_V", _params);

                                _params = new Dictionary<string, object>();
                                _params.Add("@DocNo", docNoItem);
                                _params.Add("@ReportType", "สำเนาใบกำกับภาษี/สำเนาใบเสร็จรับเงิน");
                                this.OpenReportingReportsNonPreViewPopup("ใบกำกับภาษีเต็มรูป/ใบเสร็จรับเงิน(สำเนา)", "From_V.rdlc", "Form_V", _params);
                            }
                            else
                            {
                                _params = new Dictionary<string, object>();
                                _params.Add("@DocNo", docNoItem);
                                _params.Add("@ReportType", "ต้นฉบับใบกำกับภาษี/ต้นฉบับใบเสร็จรับเงิน");
                                this.OpenReportingReportsPopup("ใบกำกับภาษีเต็มรูป/ใบเสร็จรับเงิน(ต้นฉบับ)", "From_V.rdlc", "Form_V", _params);

                                _params = new Dictionary<string, object>();
                                _params.Add("@DocNo", docNoItem);
                                _params.Add("@ReportType", "สำเนาใบกำกับภาษี/สำเนาใบเสร็จรับเงิน");
                                this.OpenReportingReportsPopup("ใบกำกับภาษีเต็มรูป/ใบเสร็จรับเงิน(สำเนา)", "From_V.rdlc", "Form_V", _params);

                            }
                        }
                        else if (frmText == "พิมพ์ต้นฉบับ")
                        {
                            _params = new Dictionary<string, object>();
                            _params.Add("@DocNo", docNoItem);

                            if (confirmResult == DialogResult.Yes)
                            {
                                _params.Add("@ReportType", "ต้นฉบับใบกำกับภาษี/ต้นฉบับใบเสร็จรับเงิน");
                                this.OpenReportingReportsNonPreViewPopup("ใบกำกับภาษีเต็มรูป/ใบเสร็จรับเงิน(ต้นฉบับ)", "From_V.rdlc", "Form_V", _params);
                            }
                            else
                            {
                                _params.Add("@ReportType", "ต้นฉบับใบกำกับภาษี/ต้นฉบับใบเสร็จรับเงิน");
                                this.OpenReportingReportsPopup("ใบกำกับภาษีเต็มรูป/ใบเสร็จรับเงิน(ต้นฉบับ)", "From_V.rdlc", "Form_V", _params);
                            }
                        }
                        else if (frmText == "พิมพ์สำเนา")
                        {
                            _params = new Dictionary<string, object>();
                            _params.Add("@DocNo", docNoItem);

                            if (confirmResult == DialogResult.Yes)
                            {
                                _params.Add("@ReportType", "สำเนาใบกำกับภาษี/สำเนาใบเสร็จรับเงิน");
                                this.OpenReportingReportsNonPreViewPopup("ใบกำกับภาษีเต็มรูป/ใบเสร็จรับเงิน(สำเนา)", "From_V.rdlc", "Form_V", _params);
                            }
                            else
                            {
                                _params.Add("@ReportType", "สำเนาใบกำกับภาษี/สำเนาใบเสร็จรับเงิน");
                                this.OpenReportingReportsPopup("ใบกำกับภาษีเต็มรูป/ใบเสร็จรับเงิน(สำเนา)", "From_V.rdlc", "Form_V", _params);
                            }
                        }
                    }
                }

                Cursor.Current = Cursors.Default;

                this.Close();
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                ex.WriteLog(this.GetType());
            }
        }

        private void PrintCredit(string whid, string docNo, DialogResult confirmResult)
        {
            Dictionary<string, object> _params = new Dictionary<string, object>();
            if (frmText == "พิมพ์ทั้งหมด")
            {
                if (confirmResult == DialogResult.Yes) //No Preview
                {
                    _params = new Dictionary<string, object>();
                    _params.Add("@DocNo", docNo);
                    _params.Add("@ReportType", "ต้นฉบับใบกำกับภาษี");
                    this.OpenReportingReportsNonPreViewPopup("ต้นฉบับใบกำกับภาษี", "From_V.rdlc", "Form_V", _params);

                    _params = new Dictionary<string, object>();
                    _params.Add("@DocNo", docNo);
                    _params.Add("@ReportType", "ใบเสร็จรับเงิน(ต้นฉบับ)");
                    this.OpenReportingReportsNonPreViewPopup("ใบเสร็จรับเงิน(ต้นฉบับ)", "From_V.rdlc", "Form_V", _params);

                    _params = new Dictionary<string, object>();
                    _params.Add("@DocNo", docNo);
                    _params.Add("@ReportType", "สำเนาใบกำกับภาษี");
                    this.OpenReportingReportsNonPreViewPopup("สำเนาใบกำกับภาษี", "From_V.rdlc", "Form_V", _params);

                    _params = new Dictionary<string, object>();
                    _params.Add("@DocNo", docNo);
                    _params.Add("@ReportType", "ใบเสร็จรับเงิน(สำเนา)");
                    this.OpenReportingReportsNonPreViewPopup("ใบเสร็จรับเงิน(สำเนา)", "From_V.rdlc", "Form_V", _params);

                }
                else
                {
                    _params = new Dictionary<string, object>();
                    _params.Add("@DocNo", docNo);
                    _params.Add("@ReportType", "ต้นฉบับใบกำกับภาษี");
                    this.OpenReportingReportsPopup("ต้นฉบับใบกำกับภาษี", "From_V.rdlc", "Form_V", _params);

                    _params = new Dictionary<string, object>();
                    _params.Add("@DocNo", docNo);
                    _params.Add("@ReportType", "ใบเสร็จรับเงิน(ต้นฉบับ)");
                    this.OpenReportingReportsPopup("ใบเสร็จรับเงิน(ต้นฉบับ)", "From_V.rdlc", "Form_V", _params);

                    _params = new Dictionary<string, object>();
                    _params.Add("@DocNo", docNo);
                    _params.Add("@ReportType", "สำเนาใบกำกับภาษี");
                    this.OpenReportingReportsPopup("สำเนาใบกำกับภาษี", "From_V.rdlc", "Form_V", _params);

                    _params = new Dictionary<string, object>();
                    _params.Add("@DocNo", docNo);
                    _params.Add("@ReportType", "ใบเสร็จรับเงิน(สำเนา)");
                    this.OpenReportingReportsPopup("ใบเสร็จรับเงิน(สำเนา)", "From_V.rdlc", "Form_V", _params);
                }
            }
            else if (frmText == "พิมพ์ต้นฉบับ")
            {
                _params = new Dictionary<string, object>();
                _params.Add("@DocNo", docNo);

                if (confirmResult == DialogResult.Yes)
                {
                    _params.Add("@ReportType", "ต้นฉบับใบกำกับภาษี");
                    this.OpenReportingReportsNonPreViewPopup("ต้นฉบับใบกำกับภาษี", "From_V.rdlc", "Form_V", _params);

                    _params = new Dictionary<string, object>();
                    _params.Add("@DocNo", docNo);
                    _params.Add("@ReportType", "ใบเสร็จรับเงิน(ต้นฉบับ)");
                    this.OpenReportingReportsNonPreViewPopup("ใบเสร็จรับเงิน(ต้นฉบับ)", "From_V.rdlc", "Form_V", _params);
                }
                else
                {
                    _params.Add("@ReportType", "ต้นฉบับใบกำกับภาษี");
                    this.OpenReportingReportsPopup("ต้นฉบับใบกำกับภาษี", "From_V.rdlc", "Form_V", _params);

                    _params = new Dictionary<string, object>();
                    _params.Add("@DocNo", docNo);
                    _params.Add("@ReportType", "ใบเสร็จรับเงิน(ต้นฉบับ)");
                    this.OpenReportingReportsPopup("ใบเสร็จรับเงิน(ต้นฉบับ)", "From_V.rdlc", "Form_V", _params);
                }
            }
            else if (frmText == "พิมพ์สำเนา")
            {
                _params = new Dictionary<string, object>();
                _params.Add("@DocNo", docNo);

                if (confirmResult == DialogResult.Yes)
                {
                    _params.Add("@ReportType", "สำเนาใบกำกับภาษี");
                    this.OpenReportingReportsNonPreViewPopup("สำเนาใบกำกับภาษี", "From_V.rdlc", "Form_V", _params);

                    _params = new Dictionary<string, object>();
                    _params.Add("@DocNo", docNo);
                    _params.Add("@ReportType", "ใบเสร็จรับเงิน(สำเนา)");
                    this.OpenReportingReportsNonPreViewPopup("ใบเสร็จรับเงิน(สำเนา)", "From_V.rdlc", "Form_V", _params);
                }
                else
                {
                    _params.Add("@ReportType", "สำเนาใบกำกับภาษี");
                    this.OpenReportingReportsPopup("สำเนาใบกำกับภาษี", "From_V.rdlc", "Form_V", _params);

                    _params = new Dictionary<string, object>();
                    _params.Add("@DocNo", docNo);
                    _params.Add("@ReportType", "ใบเสร็จรับเงิน(สำเนา)");
                    this.OpenReportingReportsPopup("ใบเสร็จรับเงิน(สำเนา)", "From_V.rdlc", "Form_V", _params);
                }
            }
        }

        private void grdList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (grdList.Columns[e.ColumnIndex].Name == "DocStatusImg")
            {
                var rowIdx = e.RowIndex;
                var docStatus = grdList.Rows[rowIdx].Cells["colDocStatus"].Value;

                if (grdList.Rows[e.RowIndex].Cells["colDocStatus"].Value != null && !string.IsNullOrEmpty(docStatus.ToString()))
                {
                    Bitmap closeImg = new Bitmap(Properties.Resources.power_off);
                    Bitmap cancelmg = new Bitmap(Properties.Resources.closeBtn);
                    Bitmap statusImg = docStatus.ToString() == "Closed" ? closeImg : cancelmg;
                    if (statusImg != null)
                    {
                        // Your code would go here - below is just the code I used to test 
                        e.Value = statusImg;
                    }
                }
            }
        }
    }
}
