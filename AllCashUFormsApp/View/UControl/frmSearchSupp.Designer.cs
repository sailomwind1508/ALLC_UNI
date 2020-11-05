namespace AllCashUFormsApp.View.UControl
{
    partial class frmSearchSupp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearchSupp));
            this.pnlBR = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlAdcSearch = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.rdoN = new System.Windows.Forms.RadioButton();
            this.rdoY = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpDocDate = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.ddlDocStatus = new System.Windows.Forms.ComboBox();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnSearchSupp = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSSuppCode = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCountList = new System.Windows.Forms.Label();
            this.lblCountListText = new System.Windows.Forms.Label();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.pnlSearchAddOn = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lnkSearchAddOn = new System.Windows.Forms.LinkLabel();
            this.txtCustName = new System.Windows.Forms.TextBox();
            this.txtCustomerCode = new System.Windows.Forms.TextBox();
            this.btnSearchCust = new System.Windows.Forms.Button();
            this.txtWHName = new System.Windows.Forms.TextBox();
            this.txtWHCode = new System.Windows.Forms.TextBox();
            this.btnSearchWHCode = new System.Windows.Forms.Button();
            this.pnlBR.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlAdcSearch.SuspendLayout();
            this.panel6.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.pnlSearchAddOn.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBR
            // 
            this.pnlBR.BackColor = System.Drawing.SystemColors.Window;
            this.pnlBR.Controls.Add(this.panel2);
            this.pnlBR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBR.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBR.Location = new System.Drawing.Point(0, 0);
            this.pnlBR.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlBR.Name = "pnlBR";
            this.pnlBR.Size = new System.Drawing.Size(644, 422);
            this.pnlBR.TabIndex = 23;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Azure;
            this.panel2.Controls.Add(this.grdList);
            this.panel2.Controls.Add(this.pnlSearchAddOn);
            this.panel2.Controls.Add(this.pnlAdcSearch);
            this.panel2.Controls.Add(this.pnlTop);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(620, 401);
            this.panel2.TabIndex = 19;
            // 
            // pnlAdcSearch
            // 
            this.pnlAdcSearch.BackColor = System.Drawing.Color.Azure;
            this.pnlAdcSearch.Controls.Add(this.panel6);
            this.pnlAdcSearch.Controls.Add(this.label8);
            this.pnlAdcSearch.Controls.Add(this.dtpDocDate);
            this.pnlAdcSearch.Controls.Add(this.lnkSearchAddOn);
            this.pnlAdcSearch.Controls.Add(this.label11);
            this.pnlAdcSearch.Controls.Add(this.ddlDocStatus);
            this.pnlAdcSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAdcSearch.Location = new System.Drawing.Point(0, 31);
            this.pnlAdcSearch.Name = "pnlAdcSearch";
            this.pnlAdcSearch.Size = new System.Drawing.Size(620, 42);
            this.pnlAdcSearch.TabIndex = 19;
            this.pnlAdcSearch.Visible = false;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Azure;
            this.panel6.Controls.Add(this.rdoN);
            this.panel6.Controls.Add(this.rdoY);
            this.panel6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel6.Location = new System.Drawing.Point(435, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(182, 25);
            this.panel6.TabIndex = 115;
            // 
            // rdoN
            // 
            this.rdoN.AutoSize = true;
            this.rdoN.Location = new System.Drawing.Point(108, 2);
            this.rdoN.Name = "rdoN";
            this.rdoN.Size = new System.Drawing.Size(68, 20);
            this.rdoN.TabIndex = 16;
            this.rdoN.Text = "เลือกวัน";
            this.rdoN.UseVisualStyleBackColor = true;
            this.rdoN.CheckedChanged += new System.EventHandler(this.rdoN_CheckedChanged);
            // 
            // rdoY
            // 
            this.rdoY.AutoSize = true;
            this.rdoY.Checked = true;
            this.rdoY.Location = new System.Drawing.Point(9, 2);
            this.rdoY.Name = "rdoY";
            this.rdoY.Size = new System.Drawing.Size(93, 20);
            this.rdoY.TabIndex = 15;
            this.rdoY.TabStop = true;
            this.rdoY.Text = "เลือกทั้งหมด";
            this.rdoY.UseVisualStyleBackColor = true;
            this.rdoY.CheckedChanged += new System.EventHandler(this.rdoY_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(234, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 16);
            this.label8.TabIndex = 114;
            this.label8.Text = "วันที่สั่งซื้อ : ";
            // 
            // dtpDocDate
            // 
            this.dtpDocDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDocDate.Enabled = false;
            this.dtpDocDate.Location = new System.Drawing.Point(315, 4);
            this.dtpDocDate.Name = "dtpDocDate";
            this.dtpDocDate.Size = new System.Drawing.Size(118, 23);
            this.dtpDocDate.TabIndex = 113;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 7);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(97, 16);
            this.label11.TabIndex = 112;
            this.label11.Text = "สถานะเอกสาร : ";
            // 
            // ddlDocStatus
            // 
            this.ddlDocStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlDocStatus.FormattingEnabled = true;
            this.ddlDocStatus.Location = new System.Drawing.Point(106, 4);
            this.ddlDocStatus.Name = "ddlDocStatus";
            this.ddlDocStatus.Size = new System.Drawing.Size(124, 24);
            this.ddlDocStatus.TabIndex = 111;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Azure;
            this.pnlTop.Controls.Add(this.btnSearchSupp);
            this.pnlTop.Controls.Add(this.label13);
            this.pnlTop.Controls.Add(this.txtSSuppCode);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(620, 31);
            this.pnlTop.TabIndex = 16;
            // 
            // btnSearchSupp
            // 
            this.btnSearchSupp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchSupp.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSearchSupp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchSupp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchSupp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSearchSupp.Image = global::AllCashUFormsApp.Properties.Resources.search;
            this.btnSearchSupp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchSupp.Location = new System.Drawing.Point(547, 1);
            this.btnSearchSupp.Name = "btnSearchSupp";
            this.btnSearchSupp.Size = new System.Drawing.Size(70, 26);
            this.btnSearchSupp.TabIndex = 2;
            this.btnSearchSupp.Text = "ค้นหา";
            this.btnSearchSupp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchSupp.UseVisualStyleBackColor = false;
            this.btnSearchSupp.Click += new System.EventHandler(this.btnSearchSupp_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(26, 7);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 16);
            this.label13.TabIndex = 0;
            this.label13.Text = "ค้นหา";
            // 
            // txtSSuppCode
            // 
            this.txtSSuppCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSSuppCode.Location = new System.Drawing.Point(70, 4);
            this.txtSSuppCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSSuppCode.MaxLength = 50;
            this.txtSSuppCode.Name = "txtSSuppCode";
            this.txtSSuppCode.Size = new System.Drawing.Size(471, 23);
            this.txtSSuppCode.TabIndex = 1;
            this.txtSSuppCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSSuppCode_KeyDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Azure;
            this.panel1.Controls.Add(this.lblCountList);
            this.panel1.Controls.Add(this.lblCountListText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 369);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(620, 32);
            this.panel1.TabIndex = 18;
            // 
            // lblCountList
            // 
            this.lblCountList.AutoSize = true;
            this.lblCountList.Location = new System.Drawing.Point(92, 7);
            this.lblCountList.Name = "lblCountList";
            this.lblCountList.Size = new System.Drawing.Size(42, 16);
            this.lblCountList.TabIndex = 1;
            this.lblCountList.Text = "label1";
            // 
            // lblCountListText
            // 
            this.lblCountListText.AutoSize = true;
            this.lblCountListText.Location = new System.Drawing.Point(3, 7);
            this.lblCountListText.Name = "lblCountListText";
            this.lblCountListText.Size = new System.Drawing.Size(83, 16);
            this.lblCountListText.TabIndex = 0;
            this.lblCountListText.Text = "จำนวนรายการ";
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Location = new System.Drawing.Point(0, 133);
            this.grdList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdList.Name = "grdList";
            this.grdList.ReadOnly = true;
            this.grdList.RowHeadersVisible = false;
            this.grdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdList.Size = new System.Drawing.Size(620, 236);
            this.grdList.TabIndex = 2;
            this.grdList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdList_CellDoubleClick);
            // 
            // pnlSearchAddOn
            // 
            this.pnlSearchAddOn.BackColor = System.Drawing.Color.Azure;
            this.pnlSearchAddOn.Controls.Add(this.txtWHName);
            this.pnlSearchAddOn.Controls.Add(this.txtWHCode);
            this.pnlSearchAddOn.Controls.Add(this.btnSearchWHCode);
            this.pnlSearchAddOn.Controls.Add(this.txtCustName);
            this.pnlSearchAddOn.Controls.Add(this.label1);
            this.pnlSearchAddOn.Controls.Add(this.txtCustomerCode);
            this.pnlSearchAddOn.Controls.Add(this.btnSearchCust);
            this.pnlSearchAddOn.Controls.Add(this.label2);
            this.pnlSearchAddOn.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchAddOn.Location = new System.Drawing.Point(0, 73);
            this.pnlSearchAddOn.Name = "pnlSearchAddOn";
            this.pnlSearchAddOn.Size = new System.Drawing.Size(620, 60);
            this.pnlSearchAddOn.TabIndex = 20;
            this.pnlSearchAddOn.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 16);
            this.label2.TabIndex = 112;
            this.label2.Text = "Van : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 16);
            this.label1.TabIndex = 113;
            this.label1.Text = "ลูกค้า : ";
            // 
            // lnkSearchAddOn
            // 
            this.lnkSearchAddOn.AutoSize = true;
            this.lnkSearchAddOn.Location = new System.Drawing.Point(3, 23);
            this.lnkSearchAddOn.Name = "lnkSearchAddOn";
            this.lnkSearchAddOn.Size = new System.Drawing.Size(79, 16);
            this.lnkSearchAddOn.TabIndex = 114;
            this.lnkSearchAddOn.TabStop = true;
            this.lnkSearchAddOn.Text = "ค้นหาเพิ่มเติม";
            this.lnkSearchAddOn.Visible = false;
            this.lnkSearchAddOn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSearchAddOn_LinkClicked);
            // 
            // txtCustName
            // 
            this.txtCustName.Location = new System.Drawing.Point(267, 32);
            this.txtCustName.MaxLength = 200;
            this.txtCustName.Name = "txtCustName";
            this.txtCustName.ReadOnly = true;
            this.txtCustName.Size = new System.Drawing.Size(344, 23);
            this.txtCustName.TabIndex = 123;
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.Location = new System.Drawing.Point(70, 32);
            this.txtCustomerCode.MaxLength = 5;
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.Size = new System.Drawing.Size(153, 23);
            this.txtCustomerCode.TabIndex = 121;
            // 
            // btnSearchCust
            // 
            this.btnSearchCust.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSearchCust.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchCust.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchCust.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSearchCust.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchCust.Image")));
            this.btnSearchCust.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchCust.Location = new System.Drawing.Point(226, 32);
            this.btnSearchCust.Name = "btnSearchCust";
            this.btnSearchCust.Size = new System.Drawing.Size(35, 23);
            this.btnSearchCust.TabIndex = 122;
            this.btnSearchCust.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchCust.UseVisualStyleBackColor = false;
            this.btnSearchCust.Click += new System.EventHandler(this.btnSearchCust_Click);
            // 
            // txtWHName
            // 
            this.txtWHName.Location = new System.Drawing.Point(267, 5);
            this.txtWHName.MaxLength = 200;
            this.txtWHName.Name = "txtWHName";
            this.txtWHName.ReadOnly = true;
            this.txtWHName.Size = new System.Drawing.Size(344, 23);
            this.txtWHName.TabIndex = 132;
            // 
            // txtWHCode
            // 
            this.txtWHCode.Location = new System.Drawing.Point(70, 5);
            this.txtWHCode.MaxLength = 5;
            this.txtWHCode.Name = "txtWHCode";
            this.txtWHCode.Size = new System.Drawing.Size(153, 23);
            this.txtWHCode.TabIndex = 130;
            // 
            // btnSearchWHCode
            // 
            this.btnSearchWHCode.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSearchWHCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchWHCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchWHCode.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSearchWHCode.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchWHCode.Image")));
            this.btnSearchWHCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchWHCode.Location = new System.Drawing.Point(225, 5);
            this.btnSearchWHCode.Name = "btnSearchWHCode";
            this.btnSearchWHCode.Size = new System.Drawing.Size(35, 23);
            this.btnSearchWHCode.TabIndex = 131;
            this.btnSearchWHCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchWHCode.UseVisualStyleBackColor = false;
            this.btnSearchWHCode.Click += new System.EventHandler(this.btnSearchWHCode_Click);
            // 
            // frmSearchSupp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 422);
            this.Controls.Add(this.pnlBR);
            this.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.Name = "frmSearchSupp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSearchSupp";
            this.Load += new System.EventHandler(this.frmSearchSupp_Load);
            this.pnlBR.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlAdcSearch.ResumeLayout(false);
            this.pnlAdcSearch.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.pnlSearchAddOn.ResumeLayout(false);
            this.pnlSearchAddOn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBR;
        private System.Windows.Forms.TextBox txtSSuppCode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCountList;
        private System.Windows.Forms.Label lblCountListText;
        private System.Windows.Forms.Button btnSearchSupp;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView grdList;
        private System.Windows.Forms.Panel pnlAdcSearch;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox ddlDocStatus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpDocDate;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.RadioButton rdoN;
        private System.Windows.Forms.RadioButton rdoY;
        private System.Windows.Forms.Panel pnlSearchAddOn;
        private System.Windows.Forms.LinkLabel lnkSearchAddOn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCustName;
        private System.Windows.Forms.TextBox txtCustomerCode;
        private System.Windows.Forms.Button btnSearchCust;
        private System.Windows.Forms.TextBox txtWHName;
        private System.Windows.Forms.TextBox txtWHCode;
        private System.Windows.Forms.Button btnSearchWHCode;
    }
}