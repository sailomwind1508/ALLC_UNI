
namespace AllCashUFormsApp.View.Page
{
    partial class frmVerifyDataToSAP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVerifyDataToSAP));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.FormPic = new System.Windows.Forms.PictureBox();
            this.FormHeader = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblRowCount = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnRemoveVE = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSendToSAP = new System.Windows.Forms.Button();
            this.btnCheckUOM = new System.Windows.Forms.Button();
            this.btnCheckStock = new System.Windows.Forms.Button();
            this.btnCheckVE = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnCheckPO = new System.Windows.Forms.Button();
            this.pnlCen = new System.Windows.Forms.Panel();
            this.grdResult = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnVerQtyAmt = new System.Windows.Forms.Button();
            this.btnClose = new AllCashUFormsApp.View.UControl.CloseButton(this.components);
            this.btnAdd = new AllCashUFormsApp.View.UControl.AddButton(this.components);
            this.btnExcel = new AllCashUFormsApp.View.UControl.ExcelButton(this.components);
            this.btnEdit = new AllCashUFormsApp.View.UControl.EditButton(this.components);
            this.btnPrint = new AllCashUFormsApp.View.UControl.PrintButton(this.components);
            this.btnRemove = new AllCashUFormsApp.View.UControl.RemoveButton(this.components);
            this.btnCancel = new AllCashUFormsApp.View.UControl.CancelButton(this.components);
            this.btnCopy = new AllCashUFormsApp.View.UControl.CopyButton(this.components);
            this.btnSave = new AllCashUFormsApp.View.UControl.SaveButton(this.components);
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormPic)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlCen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Azure;
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 28);
            this.panel1.TabIndex = 28;
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
            this.FormHeader.Size = new System.Drawing.Size(48, 17);
            this.FormHeader.TabIndex = 0;
            this.FormHeader.Text = "label8";
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
            this.panel2.Location = new System.Drawing.Point(0, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1008, 515);
            this.panel2.TabIndex = 29;
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
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Azure;
            this.pnlTop.Controls.Add(this.btnVerQtyAmt);
            this.pnlTop.Controls.Add(this.groupBox2);
            this.pnlTop.Controls.Add(this.btnRemoveVE);
            this.pnlTop.Controls.Add(this.groupBox1);
            this.pnlTop.Controls.Add(this.btnSendToSAP);
            this.pnlTop.Controls.Add(this.btnCheckUOM);
            this.pnlTop.Controls.Add(this.btnCheckStock);
            this.pnlTop.Controls.Add(this.btnCheckVE);
            this.pnlTop.Controls.Add(this.progressBar1);
            this.pnlTop.Controls.Add(this.btnCheckPO);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(992, 185);
            this.pnlTop.TabIndex = 117;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSearch);
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Location = new System.Drawing.Point(422, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(246, 56);
            this.groupBox2.TabIndex = 137;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ค้นหาเลขใบกำกับภาษี";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(6, 22);
            this.txtSearch.MaxLength = 50;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(175, 23);
            this.txtSearch.TabIndex = 135;
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
            // btnRemoveVE
            // 
            this.btnRemoveVE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnRemoveVE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRemoveVE.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemoveVE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveVE.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnRemoveVE.ForeColor = System.Drawing.Color.Black;
            this.btnRemoveVE.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveVE.Image")));
            this.btnRemoveVE.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemoveVE.Location = new System.Drawing.Point(674, 13);
            this.btnRemoveVE.Name = "btnRemoveVE";
            this.btnRemoveVE.Size = new System.Drawing.Size(161, 42);
            this.btnRemoveVE.TabIndex = 134;
            this.btnRemoveVE.Text = "ลบใบกำกับภาษีเต็มรูป (U-Force)";
            this.btnRemoveVE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRemoveVE.UseVisualStyleBackColor = false;
            this.btnRemoveVE.Click += new System.EventHandler(this.btnRemoveVE_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpDateFrom);
            this.groupBox1.Controls.Add(this.dtpDateTo);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(411, 56);
            this.groupBox1.TabIndex = 133;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "วันที่เช็ค Stock";
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpDateFrom.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.dtpDateFrom.Location = new System.Drawing.Point(75, 22);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(127, 23);
            this.dtpDateFrom.TabIndex = 10;
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.CustomFormat = "dd/MM/yyyy";
            this.dtpDateTo.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.dtpDateTo.Location = new System.Drawing.Point(277, 22);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(127, 23);
            this.dtpDateTo.TabIndex = 124;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label8.Location = new System.Drawing.Point(6, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 16);
            this.label8.TabIndex = 104;
            this.label8.Text = "วันที่เริ่มต้น: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label1.Location = new System.Drawing.Point(208, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 125;
            this.label1.Text = "วันที่สิ้นสุด: ";
            // 
            // btnSendToSAP
            // 
            this.btnSendToSAP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSendToSAP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSendToSAP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSendToSAP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendToSAP.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnSendToSAP.ForeColor = System.Drawing.Color.Black;
            this.btnSendToSAP.Image = ((System.Drawing.Image)(resources.GetObject("btnSendToSAP.Image")));
            this.btnSendToSAP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendToSAP.Location = new System.Drawing.Point(541, 61);
            this.btnSendToSAP.Name = "btnSendToSAP";
            this.btnSendToSAP.Size = new System.Drawing.Size(214, 42);
            this.btnSendToSAP.TabIndex = 123;
            this.btnSendToSAP.Text = "4. ส่งข้อมูลเข้า DB CENTER (U-Force)";
            this.btnSendToSAP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSendToSAP.UseVisualStyleBackColor = false;
            this.btnSendToSAP.Click += new System.EventHandler(this.btnSendToSAP_Click);
            // 
            // btnCheckUOM
            // 
            this.btnCheckUOM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCheckUOM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCheckUOM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckUOM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckUOM.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnCheckUOM.ForeColor = System.Drawing.Color.Black;
            this.btnCheckUOM.Image = ((System.Drawing.Image)(resources.GetObject("btnCheckUOM.Image")));
            this.btnCheckUOM.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCheckUOM.Location = new System.Drawing.Point(760, 61);
            this.btnCheckUOM.Name = "btnCheckUOM";
            this.btnCheckUOM.Size = new System.Drawing.Size(211, 42);
            this.btnCheckUOM.TabIndex = 122;
            this.btnCheckUOM.Text = "5. ตรวจสอบ UOM && PRD Line (U-Force)";
            this.btnCheckUOM.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCheckUOM.UseVisualStyleBackColor = false;
            this.btnCheckUOM.Click += new System.EventHandler(this.btnCheckUOM_Click);
            // 
            // btnCheckStock
            // 
            this.btnCheckStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCheckStock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCheckStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckStock.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnCheckStock.ForeColor = System.Drawing.Color.Black;
            this.btnCheckStock.Image = ((System.Drawing.Image)(resources.GetObject("btnCheckStock.Image")));
            this.btnCheckStock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCheckStock.Location = new System.Drawing.Point(391, 61);
            this.btnCheckStock.Name = "btnCheckStock";
            this.btnCheckStock.Size = new System.Drawing.Size(146, 42);
            this.btnCheckStock.TabIndex = 121;
            this.btnCheckStock.Text = "3. ตรวจสอบ stock (U-Force)";
            this.btnCheckStock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCheckStock.UseVisualStyleBackColor = false;
            this.btnCheckStock.Click += new System.EventHandler(this.btnCheckStock_Click);
            // 
            // btnCheckVE
            // 
            this.btnCheckVE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnCheckVE.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckVE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckVE.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnCheckVE.ForeColor = System.Drawing.Color.Black;
            this.btnCheckVE.Image = ((System.Drawing.Image)(resources.GetObject("btnCheckVE.Image")));
            this.btnCheckVE.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCheckVE.Location = new System.Drawing.Point(167, 61);
            this.btnCheckVE.Name = "btnCheckVE";
            this.btnCheckVE.Size = new System.Drawing.Size(220, 42);
            this.btnCheckVE.TabIndex = 119;
            this.btnCheckVE.Text = "2. ตรวจสอบใบกำกับภาษีเต็มรูป (U-Force)";
            this.btnCheckVE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCheckVE.UseVisualStyleBackColor = false;
            this.btnCheckVE.Click += new System.EventHandler(this.btnCheckVE_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(5, 150);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(984, 31);
            this.progressBar1.TabIndex = 105;
            // 
            // btnCheckPO
            // 
            this.btnCheckPO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCheckPO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCheckPO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckPO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckPO.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnCheckPO.ForeColor = System.Drawing.Color.Black;
            this.btnCheckPO.Image = ((System.Drawing.Image)(resources.GetObject("btnCheckPO.Image")));
            this.btnCheckPO.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCheckPO.Location = new System.Drawing.Point(5, 61);
            this.btnCheckPO.Name = "btnCheckPO";
            this.btnCheckPO.Size = new System.Drawing.Size(157, 42);
            this.btnCheckPO.TabIndex = 118;
            this.btnCheckPO.Text = "1. ตรวจสอบบิลขาย (U-Force)";
            this.btnCheckPO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCheckPO.UseVisualStyleBackColor = false;
            this.btnCheckPO.Click += new System.EventHandler(this.btnCheckPO_Click);
            // 
            // pnlCen
            // 
            this.pnlCen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCen.BackColor = System.Drawing.Color.Azure;
            this.pnlCen.Controls.Add(this.grdResult);
            this.pnlCen.Location = new System.Drawing.Point(0, 187);
            this.pnlCen.Name = "pnlCen";
            this.pnlCen.Size = new System.Drawing.Size(992, 250);
            this.pnlCen.TabIndex = 115;
            // 
            // grdResult
            // 
            this.grdResult.AllowUserToAddRows = false;
            this.grdResult.AllowUserToDeleteRows = false;
            this.grdResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdResult.Location = new System.Drawing.Point(0, 0);
            this.grdResult.Name = "grdResult";
            this.grdResult.ReadOnly = true;
            this.grdResult.Size = new System.Drawing.Size(992, 250);
            this.grdResult.TabIndex = 0;
            this.grdResult.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdResult_CellDoubleClick);
            this.grdResult.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.grdResult_RowPostPaint);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnVerQtyAmt
            // 
            this.btnVerQtyAmt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnVerQtyAmt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnVerQtyAmt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerQtyAmt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerQtyAmt.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnVerQtyAmt.ForeColor = System.Drawing.Color.Black;
            this.btnVerQtyAmt.Image = ((System.Drawing.Image)(resources.GetObject("btnVerQtyAmt.Image")));
            this.btnVerQtyAmt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVerQtyAmt.Location = new System.Drawing.Point(5, 106);
            this.btnVerQtyAmt.Name = "btnVerQtyAmt";
            this.btnVerQtyAmt.Size = new System.Drawing.Size(157, 42);
            this.btnVerQtyAmt.TabIndex = 138;
            this.btnVerQtyAmt.Text = "3.1 ตรวจสอบ Qty & Amount";
            this.btnVerQtyAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnVerQtyAmt.UseVisualStyleBackColor = false;
            this.btnVerQtyAmt.Click += new System.EventHandler(this.btnVerQtyAmt_Click);
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
            this.btnExcel.Text = "Import Excel";
            this.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcel.UseVisualStyleBackColor = false;
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
            // frmVerifyDataToSAP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 541);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "frmVerifyDataToSAP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmVerifyDataToSAP";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmVerifyDataToSAP_Load);
            this.panel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormPic)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlCen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private UControl.CloseButton btnClose;
        private UControl.AddButton btnAdd;
        private UControl.ExcelButton btnExcel;
        private UControl.EditButton btnEdit;
        private UControl.PrintButton btnPrint;
        private UControl.RemoveButton btnRemove;
        private UControl.CancelButton btnCancel;
        private UControl.CopyButton btnCopy;
        private UControl.SaveButton btnSave;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox FormPic;
        private System.Windows.Forms.Label FormHeader;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnCheckVE;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnCheckPO;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnlCen;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnSendToSAP;
        private System.Windows.Forms.Button btnCheckUOM;
        private System.Windows.Forms.Button btnCheckStock;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView grdResult;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRemoveVE;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblRowCount;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Button btnVerQtyAmt;
    }
}