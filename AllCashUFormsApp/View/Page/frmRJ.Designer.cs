namespace AllCashUFormsApp.View.Page
{
    partial class frmRJ
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRJ));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.FormPic = new System.Windows.Forms.PictureBox();
            this.FormHeader = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlTR = new System.Windows.Forms.Panel();
            this.btnRB = new System.Windows.Forms.Button();
            this.txtRBDoc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txdDocNo = new System.Windows.Forms.MaskedTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ddlDocStatus = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpDocDate = new System.Windows.Forms.DateTimePicker();
            this.btnSearchDoc = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlTL = new System.Windows.Forms.Panel();
            this.txtCrUser = new System.Windows.Forms.TextBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtWHName = new System.Windows.Forms.TextBox();
            this.txtWHCode = new System.Windows.Forms.TextBox();
            this.btnSearchWHCode = new System.Windows.Forms.Button();
            this.lblWHCode = new System.Windows.Forms.Label();
            this.lblCrUser = new System.Windows.Forms.Label();
            this.pnlCen = new System.Windows.Forms.Panel();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.colProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSearchProduct = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnit = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colOrderQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCause = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colUomSetID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnPrintCrys = new AllCashUFormsApp.View.UControl.PrintButton(this.components);
            this.btnClose = new AllCashUFormsApp.View.UControl.CloseButton(this.components);
            this.btnAdd = new AllCashUFormsApp.View.UControl.AddButton(this.components);
            this.btnExcel = new AllCashUFormsApp.View.UControl.ExcelButton(this.components);
            this.btnEdit = new AllCashUFormsApp.View.UControl.EditButton(this.components);
            this.btnPrint = new AllCashUFormsApp.View.UControl.PrintButton(this.components);
            this.btnRemove = new AllCashUFormsApp.View.UControl.RemoveButton(this.components);
            this.btnCancel = new AllCashUFormsApp.View.UControl.CancelButton(this.components);
            this.btnCopy = new AllCashUFormsApp.View.UControl.CopyButton(this.components);
            this.btnSave = new AllCashUFormsApp.View.UControl.SaveButton(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormPic)).BeginInit();
            this.panel3.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlTR.SuspendLayout();
            this.pnlTL.SuspendLayout();
            this.pnlCen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
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
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(0, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1008, 512);
            this.panel2.TabIndex = 29;
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
            this.FormHeader.Location = new System.Drawing.Point(48, 8);
            this.FormHeader.Name = "FormHeader";
            this.FormHeader.Size = new System.Drawing.Size(48, 17);
            this.FormHeader.TabIndex = 0;
            this.FormHeader.Text = "label8";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.Azure;
            this.panel3.Controls.Add(this.pnlTop);
            this.panel3.Controls.Add(this.pnlCen);
            this.panel3.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.panel3.Location = new System.Drawing.Point(7, 41);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(992, 462);
            this.panel3.TabIndex = 17;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Azure;
            this.pnlTop.Controls.Add(this.pnlTR);
            this.pnlTop.Controls.Add(this.pnlTL);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(992, 119);
            this.pnlTop.TabIndex = 117;
            // 
            // pnlTR
            // 
            this.pnlTR.BackColor = System.Drawing.Color.Azure;
            this.pnlTR.Controls.Add(this.btnRB);
            this.pnlTR.Controls.Add(this.txtRBDoc);
            this.pnlTR.Controls.Add(this.label4);
            this.pnlTR.Controls.Add(this.txdDocNo);
            this.pnlTR.Controls.Add(this.label11);
            this.pnlTR.Controls.Add(this.ddlDocStatus);
            this.pnlTR.Controls.Add(this.label8);
            this.pnlTR.Controls.Add(this.dtpDocDate);
            this.pnlTR.Controls.Add(this.btnSearchDoc);
            this.pnlTR.Controls.Add(this.label7);
            this.pnlTR.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlTR.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTR.Location = new System.Drawing.Point(567, 0);
            this.pnlTR.Name = "pnlTR";
            this.pnlTR.Size = new System.Drawing.Size(425, 119);
            this.pnlTR.TabIndex = 21;
            // 
            // btnRB
            // 
            this.btnRB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRB.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnRB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRB.ForeColor = System.Drawing.SystemColors.Control;
            this.btnRB.Image = ((System.Drawing.Image)(resources.GetObject("btnRB.Image")));
            this.btnRB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRB.Location = new System.Drawing.Point(387, 28);
            this.btnRB.Name = "btnRB";
            this.btnRB.Size = new System.Drawing.Size(35, 23);
            this.btnRB.TabIndex = 115;
            this.btnRB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRB.UseVisualStyleBackColor = false;
            this.btnRB.Click += new System.EventHandler(this.btnRB_Click);
            // 
            // txtRBDoc
            // 
            this.txtRBDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRBDoc.BackColor = System.Drawing.SystemColors.Window;
            this.txtRBDoc.Location = new System.Drawing.Point(222, 29);
            this.txtRBDoc.MaxLength = 50;
            this.txtRBDoc.Name = "txtRBDoc";
            this.txtRBDoc.Size = new System.Drawing.Size(163, 23);
            this.txtRBDoc.TabIndex = 114;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(125, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 16);
            this.label4.TabIndex = 116;
            this.label4.Text = "ใบโอนอ้างอิง : ";
            // 
            // txdDocNo
            // 
            this.txdDocNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txdDocNo.BackColor = System.Drawing.Color.Turquoise;
            this.txdDocNo.Location = new System.Drawing.Point(222, 2);
            this.txdDocNo.Name = "txdDocNo";
            this.txdDocNo.Size = new System.Drawing.Size(163, 23);
            this.txdDocNo.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(119, 88);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(97, 16);
            this.label11.TabIndex = 110;
            this.label11.Text = "สถานะเอกสาร : ";
            // 
            // ddlDocStatus
            // 
            this.ddlDocStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ddlDocStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlDocStatus.Enabled = false;
            this.ddlDocStatus.FormattingEnabled = true;
            this.ddlDocStatus.Location = new System.Drawing.Point(222, 85);
            this.ddlDocStatus.Name = "ddlDocStatus";
            this.ddlDocStatus.Size = new System.Drawing.Size(163, 24);
            this.ddlDocStatus.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(132, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 16);
            this.label8.TabIndex = 104;
            this.label8.Text = "วันที่เอกสาร : ";
            // 
            // dtpDocDate
            // 
            this.dtpDocDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpDocDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDocDate.Location = new System.Drawing.Point(222, 56);
            this.dtpDocDate.Name = "dtpDocDate";
            this.dtpDocDate.Size = new System.Drawing.Size(163, 23);
            this.dtpDocDate.TabIndex = 15;
            // 
            // btnSearchDoc
            // 
            this.btnSearchDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchDoc.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSearchDoc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchDoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchDoc.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSearchDoc.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchDoc.Image")));
            this.btnSearchDoc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchDoc.Location = new System.Drawing.Point(387, 2);
            this.btnSearchDoc.Name = "btnSearchDoc";
            this.btnSearchDoc.Size = new System.Drawing.Size(35, 23);
            this.btnSearchDoc.TabIndex = 14;
            this.btnSearchDoc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchDoc.UseVisualStyleBackColor = false;
            this.btnSearchDoc.Click += new System.EventHandler(this.btnSearchDoc_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(126, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 16);
            this.label7.TabIndex = 101;
            this.label7.Text = "เลขที่เอกสาร : ";
            // 
            // pnlTL
            // 
            this.pnlTL.BackColor = System.Drawing.Color.Azure;
            this.pnlTL.Controls.Add(this.txtCrUser);
            this.pnlTL.Controls.Add(this.txtRemark);
            this.pnlTL.Controls.Add(this.label5);
            this.pnlTL.Controls.Add(this.txtWHName);
            this.pnlTL.Controls.Add(this.txtWHCode);
            this.pnlTL.Controls.Add(this.btnSearchWHCode);
            this.pnlTL.Controls.Add(this.lblWHCode);
            this.pnlTL.Controls.Add(this.lblCrUser);
            this.pnlTL.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTL.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTL.Location = new System.Drawing.Point(0, 0);
            this.pnlTL.Name = "pnlTL";
            this.pnlTL.Size = new System.Drawing.Size(561, 119);
            this.pnlTL.TabIndex = 20;
            // 
            // txtCrUser
            // 
            this.txtCrUser.Location = new System.Drawing.Point(102, 57);
            this.txtCrUser.MaxLength = 200;
            this.txtCrUser.Name = "txtCrUser";
            this.txtCrUser.ReadOnly = true;
            this.txtCrUser.Size = new System.Drawing.Size(450, 23);
            this.txtCrUser.TabIndex = 116;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(102, 30);
            this.txtRemark.MaxLength = 255;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(450, 23);
            this.txtRemark.TabIndex = 114;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 16);
            this.label5.TabIndex = 115;
            this.label5.Text = "หมายเหตุ : ";
            // 
            // txtWHName
            // 
            this.txtWHName.Location = new System.Drawing.Point(267, 3);
            this.txtWHName.MaxLength = 200;
            this.txtWHName.Name = "txtWHName";
            this.txtWHName.ReadOnly = true;
            this.txtWHName.Size = new System.Drawing.Size(285, 23);
            this.txtWHName.TabIndex = 5;
            // 
            // txtWHCode
            // 
            this.txtWHCode.Location = new System.Drawing.Point(102, 3);
            this.txtWHCode.MaxLength = 15;
            this.txtWHCode.Name = "txtWHCode";
            this.txtWHCode.Size = new System.Drawing.Size(123, 23);
            this.txtWHCode.TabIndex = 3;
            // 
            // btnSearchWHCode
            // 
            this.btnSearchWHCode.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSearchWHCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchWHCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchWHCode.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSearchWHCode.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchWHCode.Image")));
            this.btnSearchWHCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchWHCode.Location = new System.Drawing.Point(227, 3);
            this.btnSearchWHCode.Name = "btnSearchWHCode";
            this.btnSearchWHCode.Size = new System.Drawing.Size(35, 23);
            this.btnSearchWHCode.TabIndex = 4;
            this.btnSearchWHCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchWHCode.UseVisualStyleBackColor = false;
            this.btnSearchWHCode.Click += new System.EventHandler(this.btnSearchFromWHCode_Click);
            // 
            // lblWHCode
            // 
            this.lblWHCode.AutoSize = true;
            this.lblWHCode.Location = new System.Drawing.Point(7, 5);
            this.lblWHCode.Name = "lblWHCode";
            this.lblWHCode.Size = new System.Drawing.Size(97, 16);
            this.lblWHCode.TabIndex = 113;
            this.lblWHCode.Text = "คลังซุ้มทำลาย : ";
            // 
            // lblCrUser
            // 
            this.lblCrUser.AutoSize = true;
            this.lblCrUser.Location = new System.Drawing.Point(47, 60);
            this.lblCrUser.Name = "lblCrUser";
            this.lblCrUser.Size = new System.Drawing.Size(57, 16);
            this.lblCrUser.TabIndex = 109;
            this.lblCrUser.Text = "ผู้จำทำ : ";
            // 
            // pnlCen
            // 
            this.pnlCen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCen.BackColor = System.Drawing.Color.Azure;
            this.pnlCen.Controls.Add(this.grdList);
            this.pnlCen.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.pnlCen.Location = new System.Drawing.Point(0, 125);
            this.pnlCen.Name = "pnlCen";
            this.pnlCen.Size = new System.Drawing.Size(992, 321);
            this.pnlCen.TabIndex = 115;
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToResizeColumns = false;
            this.grdList.AllowUserToResizeRows = false;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProductCode,
            this.colSearchProduct,
            this.colProductName,
            this.colUnit,
            this.colOrderQty,
            this.colCause,
            this.colUomSetID});
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.MultiSelect = false;
            this.grdList.Name = "grdList";
            this.grdList.Size = new System.Drawing.Size(992, 321);
            this.grdList.TabIndex = 17;
            // 
            // colProductCode
            // 
            this.colProductCode.DataPropertyName = "ProductID";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colProductCode.DefaultCellStyle = dataGridViewCellStyle1;
            this.colProductCode.HeaderText = "รหัสสินค้า";
            this.colProductCode.MaxInputLength = 15;
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colSearchProduct
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colSearchProduct.DefaultCellStyle = dataGridViewCellStyle2;
            this.colSearchProduct.HeaderText = "";
            this.colSearchProduct.Name = "colSearchProduct";
            this.colSearchProduct.Text = "...";
            this.colSearchProduct.UseColumnTextForButtonValue = true;
            this.colSearchProduct.Width = 30;
            // 
            // colProductName
            // 
            this.colProductName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colProductName.DataPropertyName = "ProductName";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colProductName.DefaultCellStyle = dataGridViewCellStyle3;
            this.colProductName.HeaderText = "รายการ";
            this.colProductName.Name = "colProductName";
            this.colProductName.ReadOnly = true;
            this.colProductName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colUnit
            // 
            this.colUnit.DataPropertyName = "UomSetName";
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colUnit.DefaultCellStyle = dataGridViewCellStyle4;
            this.colUnit.HeaderText = "หน่วย";
            this.colUnit.Name = "colUnit";
            this.colUnit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colUnit.Width = 70;
            // 
            // colOrderQty
            // 
            this.colOrderQty.DataPropertyName = "OrderQty";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle5.Format = "N2";
            this.colOrderQty.DefaultCellStyle = dataGridViewCellStyle5;
            this.colOrderQty.HeaderText = "จำนวนทำลาย";
            this.colOrderQty.MaxInputLength = 6;
            this.colOrderQty.Name = "colOrderQty";
            this.colOrderQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colOrderQty.Width = 150;
            // 
            // colCause
            // 
            this.colCause.DataPropertyName = "Cause";
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colCause.DefaultCellStyle = dataGridViewCellStyle6;
            this.colCause.HeaderText = "สาเหตุ";
            this.colCause.Name = "colCause";
            this.colCause.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCause.Width = 150;
            // 
            // colUomSetID
            // 
            this.colUomSetID.DataPropertyName = "UomSetID";
            this.colUomSetID.HeaderText = "UomSetID";
            this.colUomSetID.Name = "colUomSetID";
            this.colUomSetID.ReadOnly = true;
            this.colUomSetID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colUomSetID.Visible = false;
            this.colUomSetID.Width = 80;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnPrintCrys);
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
            this.panel5.TabIndex = 24;
            // 
            // btnPrintCrys
            // 
            this.btnPrintCrys.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPrintCrys.BackColor = System.Drawing.Color.Azure;
            this.btnPrintCrys.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintCrys.FlatAppearance.BorderSize = 0;
            this.btnPrintCrys.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintCrys.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintCrys.ForeColor = System.Drawing.Color.Black;
            this.btnPrintCrys.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintCrys.Image")));
            this.btnPrintCrys.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintCrys.Location = new System.Drawing.Point(484, 3);
            this.btnPrintCrys.Name = "btnPrintCrys";
            this.btnPrintCrys.Size = new System.Drawing.Size(61, 23);
            this.btnPrintCrys.TabIndex = 26;
            this.btnPrintCrys.Text = "พิมพ์";
            this.btnPrintCrys.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintCrys.UseVisualStyleBackColor = false;
            this.btnPrintCrys.Click += new System.EventHandler(this.btnPrintCrys_Click);
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
            this.btnClose.Location = new System.Drawing.Point(666, 3);
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
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 9F);
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
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
            this.btnExcel.Location = new System.Drawing.Point(551, 3);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(109, 23);
            this.btnExcel.TabIndex = 17;
            this.btnExcel.Text = "Import Excel";
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
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
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
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
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
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
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
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
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
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmRJ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 541);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRJ";
            this.Text = "frmRJ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmRJ_FormClosed);
            this.Load += new System.EventHandler(this.frmRJ_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormPic)).EndInit();
            this.panel3.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTR.ResumeLayout(false);
            this.pnlTR.PerformLayout();
            this.pnlTL.ResumeLayout(false);
            this.pnlTL.PerformLayout();
            this.pnlCen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox txdDocNo;
        private System.Windows.Forms.ComboBox ddlDocStatus;
        private System.Windows.Forms.Panel pnlTL;
        private System.Windows.Forms.TextBox txtWHName;
        private System.Windows.Forms.TextBox txtWHCode;
        private System.Windows.Forms.Button btnSearchWHCode;
        private System.Windows.Forms.Label lblWHCode;
        private System.Windows.Forms.Label lblCrUser;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel pnlCen;
        private System.Windows.Forms.DataGridView grdList;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox FormPic;
        private System.Windows.Forms.Label FormHeader;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlTR;
        private System.Windows.Forms.DateTimePicker dtpDocDate;
        private System.Windows.Forms.Button btnSearchDoc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnRB;
        private System.Windows.Forms.TextBox txtRBDoc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCrUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductCode;
        private System.Windows.Forms.DataGridViewButtonColumn colSearchProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrderQty;
        private System.Windows.Forms.DataGridViewComboBoxColumn colCause;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUomSetID;
        private System.Windows.Forms.Panel panel5;
        private UControl.PrintButton btnPrintCrys;
        private UControl.CloseButton btnClose;
        private UControl.AddButton btnAdd;
        private UControl.ExcelButton btnExcel;
        private UControl.EditButton btnEdit;
        private UControl.PrintButton btnPrint;
        private UControl.RemoveButton btnRemove;
        private UControl.CancelButton btnCancel;
        private UControl.CopyButton btnCopy;
        private UControl.SaveButton btnSave;
    }
}