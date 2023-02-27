using AllCashUFormsApp.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AllCashUFormsApp.View.UControl
{
    public partial class frmDocDate : Form
    {
        SQLPrompt bu = new SQLPrompt();

        public frmDocDate()
        {
            InitializeComponent();
        }

        private void frmDocDate_Load(object sender, EventArgs e)
        {
            dtpDateFrom.SetDateTimePickerFormat();
            dtpDateTo.SetDateTimePickerFormat();

            dtpDateFrom.Value = DateTime.Now;
            dtpDateTo.Value = DateTime.Now;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            var dt = bu.RepairVE(dtpDateFrom.Value, dtpDateTo.Value);
            if (dt != null && dt.Rows.Count > 0)
            {
                string msg = "";
                msg += "ใบกำกับภาษีเต็มรูปเลขที่ \n";

                foreach (DataRow row in dt.Rows)
                {
                    msg += "\nเลขที่ : " + row["IVDocNo"].ToString() + ", วันที่ : " + row["IVDocDate"].ToString() + ", แวน : " + row["IVWHID"].ToString();
                }

                msg += "\n\n ปรับปรุงข้อมูลเรียบร้อยแล้ว!!!";

                Cursor.Current = Cursors.Default;
                msg.ShowInfoMessage();
               

                this.Close();
            }
            else
            {
                Cursor.Current = Cursors.Default;
                string message = "ไม่พบเอกสารที่มีปัญหา!!!";
                message.ShowWarningMessage();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
