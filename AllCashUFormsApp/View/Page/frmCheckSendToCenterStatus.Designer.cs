
namespace AllCashUFormsApp.View.Page
{
    partial class frmCheckSendToCenterStatus
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckSendToCenterStatus));
            this.grdResult = new System.Windows.Forms.DataGridView();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblRowCount = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCheckStatus = new System.Windows.Forms.Button();
            this.dtpDocDate = new System.Windows.Forms.DateTimePicker();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnClose = new AllCashUFormsApp.View.UControl.CloseButton(this.components);
            this.btnAdd = new AllCashUFormsApp.View.UControl.AddButton(this.components);
            this.btnExcel = new AllCashUFormsApp.View.UControl.ExcelButton(this.components);
            this.btnEdit = new AllCashUFormsApp.View.UControl.EditButton(this.components);
            this.btnPrint = new AllCashUFormsApp.View.UControl.PrintButton(this.components);
            this.btnRemove = new AllCashUFormsApp.View.UControl.RemoveButton(this.components);
            this.btnCancel = new AllCashUFormsApp.View.UControl.CancelButton(this.components);
            this.btnCopy = new AllCashUFormsApp.View.UControl.CopyButton(this.components);
            this.btnSave = new AllCashUFormsApp.View.UControl.SaveButton(this.components);
            this.pnlCen = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.FormPic = new System.Windows.Forms.PictureBox();
            this.FormHeader = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dtpDocDateTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).BeginInit();
            this.panel6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlCen.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormPic)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdResult
            // 
            this.grdResult.AllowUserToAddRows = false;
            this.grdResult.AllowUserToDeleteRows = false;
            this.grdResult.AllowUserToResizeRows = false;
            this.grdResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdResult.Location = new System.Drawing.Point(0, 0);
            this.grdResult.Name = "grdResult";
            this.grdResult.ReadOnly = true;
            this.grdResult.Size = new System.Drawing.Size(992, 372);
            this.grdResult.TabIndex = 0;
            this.grdResult.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.grdResult_RowPostPaint);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Azure;
            this.panel6.Controls.Add(this.lblRowCount);
            this.panel6.Controls.Add(this.label82);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 434);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(992, 31);
            this.panel6.TabIndex = 116;
            // 
            // lblRowCount
            // 
            this.lblRowCount.AutoSize = true;
            this.lblRowCount.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lblRowCount.Location = new System.Drawing.Point(92, 6);
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.Size = new System.Drawing.Size(12, 16);
            this.lblRowCount.TabIndex = 5;
            this.lblRowCount.Text = ".";
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label82.Location = new System.Drawing.Point(3, 6);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(83, 16);
            this.label82.TabIndex = 4;
            this.label82.Text = "จำนวนรายการ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCheckStatus);
            this.groupBox2.Controls.Add(this.dtpDocDateTo);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dtpDocDate);
            this.groupBox2.Controls.Add(this.txtSearch);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Location = new System.Drawing.Point(6, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(953, 56);
            this.groupBox2.TabIndex = 137;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ค้นหาเลขใบกำกับภาษี";
            // 
            // btnCheckStatus
            // 
            this.btnCheckStatus.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnCheckStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCheckStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckStatus.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnCheckStatus.ForeColor = System.Drawing.Color.Black;
            this.btnCheckStatus.Image = ((System.Drawing.Image)(resources.GetObject("btnCheckStatus.Image")));
            this.btnCheckStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCheckStatus.Location = new System.Drawing.Point(706, 10);
            this.btnCheckStatus.Name = "btnCheckStatus";
            this.btnCheckStatus.Size = new System.Drawing.Size(175, 40);
            this.btnCheckStatus.TabIndex = 138;
            this.btnCheckStatus.Text = "ตรวจสอบสถานะการส่ง";
            this.btnCheckStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCheckStatus.UseVisualStyleBackColor = false;
            this.btnCheckStatus.Click += new System.EventHandler(this.btnCheckStatus_Click);
            // 
            // dtpDocDate
            // 
            this.dtpDocDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDocDate.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.dtpDocDate.Location = new System.Drawing.Point(339, 20);
            this.dtpDocDate.Name = "dtpDocDate";
            this.dtpDocDate.Size = new System.Drawing.Size(127, 23);
            this.dtpDocDate.TabIndex = 10;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(6, 22);
            this.txtSearch.MaxLength = 50;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(175, 23);
            this.txtSearch.TabIndex = 135;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label8.Location = new System.Drawing.Point(224, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 16);
            this.label8.TabIndex = 104;
            this.label8.Text = "วันที่เอกสาร From : ";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(183, 22);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(35, 23);
            this.btnSearch.TabIndex = 136;
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Azure;
            this.pnlTop.Controls.Add(this.groupBox2);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(992, 66);
            this.pnlTop.TabIndex = 117;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnClose);
            this.panel5.Controls.Add(this.btnAdd);
            this.panel5.Controls.Add(this.btnExcel);
            this.panel5.Controls.Add(this.btnEdit);
            this.panel5.Controls.Add(this.btnPrint);
            this.panel5.Controls.Add(this.btnRemove);
            this.panel5.Controls.Add(this.btnCancel);
            this.panel5.Controls.Add(this.btnCopy);
            this.panel5.Controls.Add(this.btnSave);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(782, 28);
            this.panel5.TabIndex = 17;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.BackColor = System.Drawing.Color.Azure;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Image = global::AllCashUFormsApp.Properties.Resources.power_off;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(600, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(57, 23);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "ออก";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAdd.BackColor = System.Drawing.Color.Azure;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(7, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(56, 23);
            this.btnAdd.TabIndex = 23;
            this.btnAdd.Text = "เพิ่ม";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExcel.BackColor = System.Drawing.Color.Azure;
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.Enabled = false;
            this.btnExcel.FlatAppearance.BorderSize = 0;
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.ForeColor = System.Drawing.Color.Black;
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcel.Location = new System.Drawing.Point(485, 3);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(109, 23);
            this.btnExcel.TabIndex = 17;
            this.btnExcel.Text = "Export Excel";
            this.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEdit.BackColor = System.Drawing.Color.Azure;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.Color.Black;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(69, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(63, 23);
            this.btnEdit.TabIndex = 25;
            this.btnEdit.Text = "แก้ไข";
            this.btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPrint.BackColor = System.Drawing.Color.Azure;
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.Enabled = false;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.Black;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(418, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(61, 23);
            this.btnPrint.TabIndex = 17;
            this.btnPrint.Text = "พิมพ์";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = false;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRemove.BackColor = System.Drawing.Color.Azure;
            this.btnRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemove.Enabled = false;
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.ForeColor = System.Drawing.Color.Black;
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemove.Location = new System.Drawing.Point(138, 3);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(49, 23);
            this.btnRemove.TabIndex = 25;
            this.btnRemove.Text = "ลบ";
            this.btnRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRemove.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.BackColor = System.Drawing.Color.Azure;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Enabled = false;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(344, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCopy.BackColor = System.Drawing.Color.Azure;
            this.btnCopy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCopy.Enabled = false;
            this.btnCopy.FlatAppearance.BorderSize = 0;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopy.ForeColor = System.Drawing.Color.Black;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCopy.Location = new System.Drawing.Point(193, 3);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 25;
            this.btnCopy.Text = "คัดลอก";
            this.btnCopy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCopy.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave.BackColor = System.Drawing.Color.Azure;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(274, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(64, 23);
            this.btnSave.TabIndex = 24;
            this.btnSave.Text = "บันทึก";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // pnlCen
            // 
            this.pnlCen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCen.BackColor = System.Drawing.Color.Azure;
            this.pnlCen.Controls.Add(this.grdResult);
            this.pnlCen.Location = new System.Drawing.Point(0, 65);
            this.pnlCen.Name = "pnlCen";
            this.pnlCen.Size = new System.Drawing.Size(992, 372);
            this.pnlCen.TabIndex = 115;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Azure;
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 28);
            this.panel1.TabIndex = 30;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.SystemColors.Control;
            this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
            this.panel4.Controls.Add(this.FormPic);
            this.panel4.Controls.Add(this.FormHeader);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1008, 35);
            this.panel4.TabIndex = 18;
            // 
            // FormPic
            // 
            this.FormPic.Location = new System.Drawing.Point(12, 3);
            this.FormPic.Name = "FormPic";
            this.FormPic.Size = new System.Drawing.Size(30, 30);
            this.FormPic.TabIndex = 1;
            this.FormPic.TabStop = false;
            // 
            // FormHeader
            // 
            this.FormHeader.AutoSize = true;
            this.FormHeader.BackColor = System.Drawing.Color.Azure;
            this.FormHeader.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormHeader.Location = new System.Drawing.Point(48, 10);
            this.FormHeader.Name = "FormHeader";
            this.FormHeader.Size = new System.Drawing.Size(94, 17);
            this.FormHeader.TabIndex = 0;
            this.FormHeader.Text = "FormHeader";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(0, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1008, 515);
            this.panel2.TabIndex = 31;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.Azure;
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.pnlTop);
            this.panel3.Controls.Add(this.pnlCen);
            this.panel3.Location = new System.Drawing.Point(7, 41);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(992, 465);
            this.panel3.TabIndex = 17;
            // 
            // dtpDocDateTo
            // 
            this.dtpDocDateTo.CustomFormat = "dd/MM/yyyy";
            this.dtpDocDateTo.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.dtpDocDateTo.Location = new System.Drawing.Point(573, 20);
            this.dtpDocDateTo.Name = "dtpDocDateTo";
            this.dtpDocDateTo.Size = new System.Drawing.Size(127, 23);
            this.dtpDocDateTo.TabIndex = 137;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label1.Location = new System.Drawing.Point(472, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 16);
            this.label1.TabIndex = 138;
            this.label1.Text = "วันที่เอกสาร To : ";
            // 
            // frmCheckSendToCenterStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 541);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "frmCheckSendToCenterStatus";
            this.Text = "frmCheckSendToCenterStatus";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmCheckSendToCenterStatus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.pnlCen.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormPic)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdResult;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblRowCount;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtpDocDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel panel5;
        private UControl.CloseButton btnClose;
        private UControl.AddButton btnAdd;
        private UControl.ExcelButton btnExcel;
        private UControl.EditButton btnEdit;
        private UControl.PrintButton btnPrint;
        private UControl.RemoveButton btnRemove;
        private UControl.CancelButton btnCancel;
        private UControl.CopyButton btnCopy;
        private UControl.SaveButton btnSave;
        private System.Windows.Forms.Panel pnlCen;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox FormPic;
        private System.Windows.Forms.Label FormHeader;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCheckStatus;
        private System.Windows.Forms.DateTimePicker dtpDocDateTo;
        private System.Windows.Forms.Label label1;
    }
}