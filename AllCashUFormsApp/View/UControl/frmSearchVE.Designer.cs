
namespace AllCashUFormsApp.View.UControl
{
    partial class frmSearchVE
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearchVE));
            this.lblCountListText = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSearchTxt = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpDocDate = new System.Windows.Forms.DateTimePicker();
            this.rdoN = new System.Windows.Forms.RadioButton();
            this.rdoY = new System.Windows.Forms.RadioButton();
            this.panel6 = new System.Windows.Forms.Panel();
            this.pnlAdcSearch = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.colSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colDocNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocStatusImg = new System.Windows.Forms.DataGridViewImageColumn();
            this.colDocStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDocDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWHID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSaleEmpID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalDue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCrUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.lblCountList = new System.Windows.Forms.Label();
            this.pnlBR = new System.Windows.Forms.Panel();
            this.panel6.SuspendLayout();
            this.pnlAdcSearch.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlBR.SuspendLayout();
            this.SuspendLayout();
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
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(26, 7);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 16);
            this.label13.TabIndex = 0;
            this.label13.Text = "ค้นหา :";
            // 
            // txtSearchTxt
            // 
            this.txtSearchTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchTxt.Location = new System.Drawing.Point(78, 4);
            this.txtSearchTxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSearchTxt.MaxLength = 50;
            this.txtSearchTxt.Name = "txtSearchTxt";
            this.txtSearchTxt.Size = new System.Drawing.Size(618, 23);
            this.txtSearchTxt.TabIndex = 1;
            this.txtSearchTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchTxt_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 16);
            this.label8.TabIndex = 114;
            this.label8.Text = "วันที่สั่งซื้อ : ";
            // 
            // dtpDocDate
            // 
            this.dtpDocDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDocDate.Enabled = false;
            this.dtpDocDate.Location = new System.Drawing.Point(78, 1);
            this.dtpDocDate.Name = "dtpDocDate";
            this.dtpDocDate.Size = new System.Drawing.Size(118, 23);
            this.dtpDocDate.TabIndex = 113;
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
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Azure;
            this.panel6.Controls.Add(this.rdoN);
            this.panel6.Controls.Add(this.rdoY);
            this.panel6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel6.Location = new System.Drawing.Point(200, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(182, 25);
            this.panel6.TabIndex = 115;
            // 
            // pnlAdcSearch
            // 
            this.pnlAdcSearch.BackColor = System.Drawing.Color.Azure;
            this.pnlAdcSearch.Controls.Add(this.panel6);
            this.pnlAdcSearch.Controls.Add(this.label8);
            this.pnlAdcSearch.Controls.Add(this.dtpDocDate);
            this.pnlAdcSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAdcSearch.Location = new System.Drawing.Point(0, 31);
            this.pnlAdcSearch.Name = "pnlAdcSearch";
            this.pnlAdcSearch.Size = new System.Drawing.Size(775, 28);
            this.pnlAdcSearch.TabIndex = 19;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Azure;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.pnlAdcSearch);
            this.panel2.Controls.Add(this.pnlTop);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(775, 440);
            this.panel2.TabIndex = 19;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.Azure;
            this.panel3.Controls.Add(this.grdList);
            this.panel3.Location = new System.Drawing.Point(0, 59);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(775, 346);
            this.panel3.TabIndex = 20;
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.AllowUserToResizeRows = false;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelect,
            this.colDocNo,
            this.DocStatusImg,
            this.colDocStatus,
            this.colDocDate,
            this.colCustomerID,
            this.colCustomerName,
            this.colWHID,
            this.colSaleEmpID,
            this.colTotalDue,
            this.colCrUser,
            this.colRemark});
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdList.Name = "grdList";
            this.grdList.Size = new System.Drawing.Size(775, 346);
            this.grdList.TabIndex = 4;
            this.grdList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grdList_CellFormatting);
            this.grdList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.grdList_RowPostPaint);
            // 
            // colSelect
            // 
            this.colSelect.FillWeight = 30F;
            this.colSelect.HeaderText = "";
            this.colSelect.Name = "colSelect";
            this.colSelect.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colSelect.Width = 30;
            // 
            // colDocNo
            // 
            this.colDocNo.DataPropertyName = "DocNo";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colDocNo.DefaultCellStyle = dataGridViewCellStyle1;
            this.colDocNo.HeaderText = "เลขที่เอกสาร";
            this.colDocNo.MaxInputLength = 255;
            this.colDocNo.Name = "colDocNo";
            this.colDocNo.ReadOnly = true;
            this.colDocNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colDocNo.Width = 120;
            // 
            // DocStatusImg
            // 
            this.DocStatusImg.DataPropertyName = "DocStatusImg";
            this.DocStatusImg.HeaderText = "";
            this.DocStatusImg.Name = "DocStatusImg";
            this.DocStatusImg.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DocStatusImg.Width = 30;
            // 
            // colDocStatus
            // 
            this.colDocStatus.DataPropertyName = "DocStatus";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colDocStatus.DefaultCellStyle = dataGridViewCellStyle2;
            this.colDocStatus.HeaderText = "สถานะ";
            this.colDocStatus.MaxInputLength = 500;
            this.colDocStatus.Name = "colDocStatus";
            this.colDocStatus.ReadOnly = true;
            this.colDocStatus.Width = 50;
            // 
            // colDocDate
            // 
            this.colDocDate.DataPropertyName = "DocDate";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "dd/MM/yyyy";
            this.colDocDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.colDocDate.HeaderText = "วันที่เอกสาร";
            this.colDocDate.Name = "colDocDate";
            // 
            // colCustomerID
            // 
            this.colCustomerID.DataPropertyName = "CustomerID";
            this.colCustomerID.HeaderText = "รหัสลูกค้า";
            this.colCustomerID.Name = "colCustomerID";
            this.colCustomerID.Width = 120;
            // 
            // colCustomerName
            // 
            this.colCustomerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCustomerName.DataPropertyName = "CustomerName";
            this.colCustomerName.HeaderText = "ชื่อลูกค้า";
            this.colCustomerName.MinimumWidth = 160;
            this.colCustomerName.Name = "colCustomerName";
            // 
            // colWHID
            // 
            this.colWHID.DataPropertyName = "WHID";
            this.colWHID.HeaderText = "รหัสคลัง";
            this.colWHID.Name = "colWHID";
            this.colWHID.Width = 120;
            // 
            // colSaleEmpID
            // 
            this.colSaleEmpID.DataPropertyName = "SaleEmpID";
            this.colSaleEmpID.HeaderText = "รหัสพนักงาน";
            this.colSaleEmpID.Name = "colSaleEmpID";
            this.colSaleEmpID.Width = 120;
            // 
            // colTotalDue
            // 
            this.colTotalDue.DataPropertyName = "TotalDue";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            this.colTotalDue.DefaultCellStyle = dataGridViewCellStyle4;
            this.colTotalDue.HeaderText = "ยอดรวม";
            this.colTotalDue.Name = "colTotalDue";
            this.colTotalDue.Width = 130;
            // 
            // colCrUser
            // 
            this.colCrUser.DataPropertyName = "CrUser";
            this.colCrUser.HeaderText = "ผู้จัดทำ";
            this.colCrUser.Name = "colCrUser";
            this.colCrUser.Width = 120;
            // 
            // colRemark
            // 
            this.colRemark.DataPropertyName = "Remark";
            this.colRemark.HeaderText = "หมายเหตุ";
            this.colRemark.Name = "colRemark";
            this.colRemark.Width = 120;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Azure;
            this.pnlTop.Controls.Add(this.btnSearch);
            this.pnlTop.Controls.Add(this.label13);
            this.pnlTop.Controls.Add(this.txtSearchTxt);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(775, 31);
            this.pnlTop.TabIndex = 16;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSearch.Image = global::AllCashUFormsApp.Properties.Resources.search;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(702, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(70, 26);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "ค้นหา";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Azure;
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnAccept);
            this.panel1.Controls.Add(this.lblCountList);
            this.panel1.Controls.Add(this.lblCountListText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 406);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(775, 34);
            this.panel1.TabIndex = 18;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(698, 3);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 28);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnAccept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccept.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnAccept.Image = ((System.Drawing.Image)(resources.GetObject("btnAccept.Image")));
            this.btnAccept.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAccept.Location = new System.Drawing.Point(618, 3);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(74, 28);
            this.btnAccept.TabIndex = 20;
            this.btnAccept.Text = "พิมพ์";
            this.btnAccept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
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
            // pnlBR
            // 
            this.pnlBR.BackColor = System.Drawing.SystemColors.Window;
            this.pnlBR.Controls.Add(this.panel2);
            this.pnlBR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBR.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBR.Location = new System.Drawing.Point(0, 0);
            this.pnlBR.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlBR.Name = "pnlBR";
            this.pnlBR.Size = new System.Drawing.Size(799, 461);
            this.pnlBR.TabIndex = 24;
            // 
            // frmSearchVE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 461);
            this.Controls.Add(this.pnlBR);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "frmSearchVE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ใบกำกับภาษีเต็มรูป";
            this.Load += new System.EventHandler(this.frmSearchVE_Load);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.pnlAdcSearch.ResumeLayout(false);
            this.pnlAdcSearch.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlBR.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCountListText;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtSearchTxt;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpDocDate;
        private System.Windows.Forms.RadioButton rdoN;
        private System.Windows.Forms.RadioButton rdoY;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel pnlAdcSearch;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCountList;
        private System.Windows.Forms.Panel pnlBR;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView grdList;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDocNo;
        private System.Windows.Forms.DataGridViewImageColumn DocStatusImg;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDocStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDocDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWHID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSaleEmpID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalDue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCrUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRemark;
    }
}