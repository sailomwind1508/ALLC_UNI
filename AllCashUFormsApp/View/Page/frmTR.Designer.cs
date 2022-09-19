namespace AllCashUFormsApp.View.Page
{
    partial class frmTR
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTR));
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlTR = new System.Windows.Forms.Panel();
            this.txdDocNo = new System.Windows.Forms.MaskedTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ddlDocStatus = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpDocDate = new System.Windows.Forms.DateTimePicker();
            this.btnSearchDoc = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlTL = new System.Windows.Forms.Panel();
            this.txtToWHName = new System.Windows.Forms.TextBox();
            this.txtToWHCode = new System.Windows.Forms.TextBox();
            this.btnSeatchToWHCode = new System.Windows.Forms.Button();
            this.lblToWHCode = new System.Windows.Forms.Label();
            this.txtFromWHName = new System.Windows.Forms.TextBox();
            this.txtFromWHCode = new System.Windows.Forms.TextBox();
            this.btnSearchFromWHCode = new System.Windows.Forms.Button();
            this.lblFromWHCode = new System.Windows.Forms.Label();
            this.txtCrUser = new System.Windows.Forms.TextBox();
            this.lblCrUser = new System.Windows.Forms.Label();
            this.txtBranchName = new System.Windows.Forms.TextBox();
            this.txtBranchCode = new System.Windows.Forms.TextBox();
            this.btnSearchBranchCode = new System.Windows.Forms.Button();
            this.lblBranchCode = new System.Windows.Forms.Label();
            this.lblUOM = new System.Windows.Forms.Label();
            this.lblOrderQty = new System.Windows.Forms.Label();
            this.txtOrderQty = new System.Windows.Forms.TextBox();
            this.txtToStock = new System.Windows.Forms.TextBox();
            this.txtFromStock = new System.Windows.Forms.TextBox();
            this.lblStock = new System.Windows.Forms.Label();
            this.txtToProductName = new System.Windows.Forms.TextBox();
            this.txtToProductCode = new System.Windows.Forms.TextBox();
            this.btnSearchToProductCode = new System.Windows.Forms.Button();
            this.lblToProductCode = new System.Windows.Forms.Label();
            this.txtFromProductName = new System.Windows.Forms.TextBox();
            this.txtFromProductCode = new System.Windows.Forms.TextBox();
            this.btnSearchFromProductCode = new System.Windows.Forms.Button();
            this.lblFromProductCode = new System.Windows.Forms.Label();
            this.pnlBL = new System.Windows.Forms.Panel();
            this.grdDetails = new System.Windows.Forms.GroupBox();
            this.ddlToUOM = new System.Windows.Forms.ComboBox();
            this.ddlFromUOM = new System.Windows.Forms.ComboBox();
            this.txtBaseQty = new System.Windows.Forms.TextBox();
            this.chkFixBaseQty = new System.Windows.Forms.CheckBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.pnlBot = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.FormPic = new System.Windows.Forms.PictureBox();
            this.FormHeader = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlTop.SuspendLayout();
            this.pnlTR.SuspendLayout();
            this.pnlTL.SuspendLayout();
            this.pnlBL.SuspendLayout();
            this.grdDetails.SuspendLayout();
            this.pnlBot.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormPic)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Azure;
            this.pnlTop.Controls.Add(this.pnlTR);
            this.pnlTop.Controls.Add(this.pnlTL);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(992, 112);
            this.pnlTop.TabIndex = 117;
            // 
            // pnlTR
            // 
            this.pnlTR.BackColor = System.Drawing.Color.Azure;
            this.pnlTR.Controls.Add(this.txdDocNo);
            this.pnlTR.Controls.Add(this.ddlDocStatus);
            this.pnlTR.Controls.Add(this.dtpDocDate);
            this.pnlTR.Controls.Add(this.btnSearchDoc);
            this.pnlTR.Controls.Add(this.label11);
            this.pnlTR.Controls.Add(this.label8);
            this.pnlTR.Controls.Add(this.label7);
            this.pnlTR.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlTR.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTR.Location = new System.Drawing.Point(567, 0);
            this.pnlTR.Name = "pnlTR";
            this.pnlTR.Size = new System.Drawing.Size(425, 112);
            this.pnlTR.TabIndex = 21;
            // 
            // txdDocNo
            // 
            this.txdDocNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txdDocNo.BackColor = System.Drawing.Color.Turquoise;
            this.txdDocNo.Location = new System.Drawing.Point(222, 5);
            this.txdDocNo.Name = "txdDocNo";
            this.txdDocNo.Size = new System.Drawing.Size(163, 23);
            this.txdDocNo.TabIndex = 8;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(126, 60);
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
            this.ddlDocStatus.Location = new System.Drawing.Point(222, 57);
            this.ddlDocStatus.Name = "ddlDocStatus";
            this.ddlDocStatus.Size = new System.Drawing.Size(163, 24);
            this.ddlDocStatus.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(157, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 16);
            this.label8.TabIndex = 104;
            this.label8.Text = "วันที่โอน : ";
            // 
            // dtpDocDate
            // 
            this.dtpDocDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpDocDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDocDate.Location = new System.Drawing.Point(222, 31);
            this.dtpDocDate.Name = "dtpDocDate";
            this.dtpDocDate.Size = new System.Drawing.Size(163, 23);
            this.dtpDocDate.TabIndex = 9;
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
            this.btnSearchDoc.Location = new System.Drawing.Point(387, 5);
            this.btnSearchDoc.Name = "btnSearchDoc";
            this.btnSearchDoc.Size = new System.Drawing.Size(35, 23);
            this.btnSearchDoc.TabIndex = 22;
            this.btnSearchDoc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchDoc.UseVisualStyleBackColor = false;
            this.btnSearchDoc.Click += new System.EventHandler(this.btnSearchDoc_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(133, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 16);
            this.label7.TabIndex = 101;
            this.label7.Text = "เลขที่เอกสาร : ";
            // 
            // pnlTL
            // 
            this.pnlTL.BackColor = System.Drawing.Color.Azure;
            this.pnlTL.Controls.Add(this.txtToWHName);
            this.pnlTL.Controls.Add(this.txtToWHCode);
            this.pnlTL.Controls.Add(this.btnSeatchToWHCode);
            this.pnlTL.Controls.Add(this.lblToWHCode);
            this.pnlTL.Controls.Add(this.txtFromWHName);
            this.pnlTL.Controls.Add(this.txtFromWHCode);
            this.pnlTL.Controls.Add(this.btnSearchFromWHCode);
            this.pnlTL.Controls.Add(this.lblFromWHCode);
            this.pnlTL.Controls.Add(this.txtCrUser);
            this.pnlTL.Controls.Add(this.lblCrUser);
            this.pnlTL.Controls.Add(this.txtBranchName);
            this.pnlTL.Controls.Add(this.txtBranchCode);
            this.pnlTL.Controls.Add(this.btnSearchBranchCode);
            this.pnlTL.Controls.Add(this.lblBranchCode);
            this.pnlTL.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTL.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTL.Location = new System.Drawing.Point(0, 0);
            this.pnlTL.Name = "pnlTL";
            this.pnlTL.Size = new System.Drawing.Size(561, 112);
            this.pnlTL.TabIndex = 20;
            // 
            // txtToWHName
            // 
            this.txtToWHName.Location = new System.Drawing.Point(265, 57);
            this.txtToWHName.MaxLength = 200;
            this.txtToWHName.Name = "txtToWHName";
            this.txtToWHName.ReadOnly = true;
            this.txtToWHName.Size = new System.Drawing.Size(285, 23);
            this.txtToWHName.TabIndex = 6;
            // 
            // txtToWHCode
            // 
            this.txtToWHCode.Location = new System.Drawing.Point(103, 57);
            this.txtToWHCode.MaxLength = 15;
            this.txtToWHCode.Name = "txtToWHCode";
            this.txtToWHCode.ReadOnly = true;
            this.txtToWHCode.Size = new System.Drawing.Size(123, 23);
            this.txtToWHCode.TabIndex = 5;
            // 
            // btnSeatchToWHCode
            // 
            this.btnSeatchToWHCode.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSeatchToWHCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSeatchToWHCode.Enabled = false;
            this.btnSeatchToWHCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeatchToWHCode.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSeatchToWHCode.Image = ((System.Drawing.Image)(resources.GetObject("btnSeatchToWHCode.Image")));
            this.btnSeatchToWHCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSeatchToWHCode.Location = new System.Drawing.Point(228, 57);
            this.btnSeatchToWHCode.Name = "btnSeatchToWHCode";
            this.btnSeatchToWHCode.Size = new System.Drawing.Size(35, 23);
            this.btnSeatchToWHCode.TabIndex = 21;
            this.btnSeatchToWHCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSeatchToWHCode.UseVisualStyleBackColor = false;
            this.btnSeatchToWHCode.Click += new System.EventHandler(this.btnSeatchToWHCode_Click);
            // 
            // lblToWHCode
            // 
            this.lblToWHCode.AutoSize = true;
            this.lblToWHCode.Location = new System.Drawing.Point(9, 60);
            this.lblToWHCode.Name = "lblToWHCode";
            this.lblToWHCode.Size = new System.Drawing.Size(94, 16);
            this.lblToWHCode.TabIndex = 131;
            this.lblToWHCode.Text = "คลังปลายทาง : ";
            // 
            // txtFromWHName
            // 
            this.txtFromWHName.Location = new System.Drawing.Point(265, 31);
            this.txtFromWHName.MaxLength = 200;
            this.txtFromWHName.Name = "txtFromWHName";
            this.txtFromWHName.ReadOnly = true;
            this.txtFromWHName.Size = new System.Drawing.Size(285, 23);
            this.txtFromWHName.TabIndex = 4;
            // 
            // txtFromWHCode
            // 
            this.txtFromWHCode.Location = new System.Drawing.Point(103, 31);
            this.txtFromWHCode.MaxLength = 15;
            this.txtFromWHCode.Name = "txtFromWHCode";
            this.txtFromWHCode.ReadOnly = true;
            this.txtFromWHCode.Size = new System.Drawing.Size(123, 23);
            this.txtFromWHCode.TabIndex = 3;
            // 
            // btnSearchFromWHCode
            // 
            this.btnSearchFromWHCode.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSearchFromWHCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchFromWHCode.Enabled = false;
            this.btnSearchFromWHCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchFromWHCode.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSearchFromWHCode.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchFromWHCode.Image")));
            this.btnSearchFromWHCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchFromWHCode.Location = new System.Drawing.Point(228, 31);
            this.btnSearchFromWHCode.Name = "btnSearchFromWHCode";
            this.btnSearchFromWHCode.Size = new System.Drawing.Size(35, 23);
            this.btnSearchFromWHCode.TabIndex = 20;
            this.btnSearchFromWHCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchFromWHCode.UseVisualStyleBackColor = false;
            this.btnSearchFromWHCode.Click += new System.EventHandler(this.btnSearchFromWHCode_Click);
            // 
            // lblFromWHCode
            // 
            this.lblFromWHCode.AutoSize = true;
            this.lblFromWHCode.Location = new System.Drawing.Point(23, 34);
            this.lblFromWHCode.Name = "lblFromWHCode";
            this.lblFromWHCode.Size = new System.Drawing.Size(80, 16);
            this.lblFromWHCode.TabIndex = 130;
            this.lblFromWHCode.Text = "คลังต้นทาง : ";
            // 
            // txtCrUser
            // 
            this.txtCrUser.BackColor = System.Drawing.SystemColors.Control;
            this.txtCrUser.Location = new System.Drawing.Point(103, 83);
            this.txtCrUser.MaxLength = 50;
            this.txtCrUser.Name = "txtCrUser";
            this.txtCrUser.ReadOnly = true;
            this.txtCrUser.Size = new System.Drawing.Size(447, 23);
            this.txtCrUser.TabIndex = 7;
            // 
            // lblCrUser
            // 
            this.lblCrUser.AutoSize = true;
            this.lblCrUser.Location = new System.Drawing.Point(46, 86);
            this.lblCrUser.Name = "lblCrUser";
            this.lblCrUser.Size = new System.Drawing.Size(57, 16);
            this.lblCrUser.TabIndex = 129;
            this.lblCrUser.Text = "ผู้จำทำ : ";
            // 
            // txtBranchName
            // 
            this.txtBranchName.Location = new System.Drawing.Point(265, 5);
            this.txtBranchName.MaxLength = 200;
            this.txtBranchName.Name = "txtBranchName";
            this.txtBranchName.ReadOnly = true;
            this.txtBranchName.Size = new System.Drawing.Size(285, 23);
            this.txtBranchName.TabIndex = 2;
            // 
            // txtBranchCode
            // 
            this.txtBranchCode.Location = new System.Drawing.Point(103, 5);
            this.txtBranchCode.MaxLength = 5;
            this.txtBranchCode.Name = "txtBranchCode";
            this.txtBranchCode.ReadOnly = true;
            this.txtBranchCode.Size = new System.Drawing.Size(123, 23);
            this.txtBranchCode.TabIndex = 1;
            // 
            // btnSearchBranchCode
            // 
            this.btnSearchBranchCode.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSearchBranchCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchBranchCode.Enabled = false;
            this.btnSearchBranchCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchBranchCode.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSearchBranchCode.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchBranchCode.Image")));
            this.btnSearchBranchCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchBranchCode.Location = new System.Drawing.Point(228, 5);
            this.btnSearchBranchCode.Name = "btnSearchBranchCode";
            this.btnSearchBranchCode.Size = new System.Drawing.Size(35, 23);
            this.btnSearchBranchCode.TabIndex = 19;
            this.btnSearchBranchCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchBranchCode.UseVisualStyleBackColor = false;
            this.btnSearchBranchCode.Click += new System.EventHandler(this.btnSearchBranchCode_Click);
            // 
            // lblBranchCode
            // 
            this.lblBranchCode.AutoSize = true;
            this.lblBranchCode.Location = new System.Drawing.Point(23, 8);
            this.lblBranchCode.Name = "lblBranchCode";
            this.lblBranchCode.Size = new System.Drawing.Size(80, 16);
            this.lblBranchCode.TabIndex = 128;
            this.lblBranchCode.Text = "เดโป้/สาขา : ";
            // 
            // lblUOM
            // 
            this.lblUOM.AutoSize = true;
            this.lblUOM.Location = new System.Drawing.Point(945, 114);
            this.lblUOM.Name = "lblUOM";
            this.lblUOM.Size = new System.Drawing.Size(33, 16);
            this.lblUOM.TabIndex = 144;
            this.lblUOM.Text = "แพ็ค";
            this.lblUOM.Visible = false;
            // 
            // lblOrderQty
            // 
            this.lblOrderQty.AutoSize = true;
            this.lblOrderQty.Location = new System.Drawing.Point(760, 17);
            this.lblOrderQty.Name = "lblOrderQty";
            this.lblOrderQty.Size = new System.Drawing.Size(53, 16);
            this.lblOrderQty.TabIndex = 143;
            this.lblOrderQty.Text = "จน. โอน";
            // 
            // txtOrderQty
            // 
            this.txtOrderQty.BackColor = System.Drawing.SystemColors.Window;
            this.txtOrderQty.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderQty.Location = new System.Drawing.Point(745, 35);
            this.txtOrderQty.MaxLength = 5;
            this.txtOrderQty.Name = "txtOrderQty";
            this.txtOrderQty.Size = new System.Drawing.Size(85, 23);
            this.txtOrderQty.TabIndex = 14;
            this.txtOrderQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtToStock
            // 
            this.txtToStock.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToStock.Location = new System.Drawing.Point(654, 62);
            this.txtToStock.MaxLength = 5;
            this.txtToStock.Name = "txtToStock";
            this.txtToStock.ReadOnly = true;
            this.txtToStock.Size = new System.Drawing.Size(85, 23);
            this.txtToStock.TabIndex = 17;
            this.txtToStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtFromStock
            // 
            this.txtFromStock.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromStock.Location = new System.Drawing.Point(654, 35);
            this.txtFromStock.MaxLength = 5;
            this.txtFromStock.Name = "txtFromStock";
            this.txtFromStock.ReadOnly = true;
            this.txtFromStock.Size = new System.Drawing.Size(85, 23);
            this.txtFromStock.TabIndex = 13;
            this.txtFromStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(666, 17);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(62, 16);
            this.lblStock.TabIndex = 139;
            this.lblStock.Text = "จน. Stock";
            // 
            // txtToProductName
            // 
            this.txtToProductName.Location = new System.Drawing.Point(260, 62);
            this.txtToProductName.MaxLength = 200;
            this.txtToProductName.Name = "txtToProductName";
            this.txtToProductName.ReadOnly = true;
            this.txtToProductName.Size = new System.Drawing.Size(385, 23);
            this.txtToProductName.TabIndex = 16;
            // 
            // txtToProductCode
            // 
            this.txtToProductCode.Location = new System.Drawing.Point(98, 62);
            this.txtToProductCode.MaxLength = 20;
            this.txtToProductCode.Name = "txtToProductCode";
            this.txtToProductCode.Size = new System.Drawing.Size(123, 23);
            this.txtToProductCode.TabIndex = 15;
            // 
            // btnSearchToProductCode
            // 
            this.btnSearchToProductCode.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSearchToProductCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchToProductCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchToProductCode.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSearchToProductCode.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchToProductCode.Image")));
            this.btnSearchToProductCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchToProductCode.Location = new System.Drawing.Point(223, 62);
            this.btnSearchToProductCode.Name = "btnSearchToProductCode";
            this.btnSearchToProductCode.Size = new System.Drawing.Size(35, 23);
            this.btnSearchToProductCode.TabIndex = 24;
            this.btnSearchToProductCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchToProductCode.UseVisualStyleBackColor = false;
            this.btnSearchToProductCode.Click += new System.EventHandler(this.btnSearchToProductCode_Click);
            // 
            // lblToProductCode
            // 
            this.lblToProductCode.AutoSize = true;
            this.lblToProductCode.Location = new System.Drawing.Point(10, 65);
            this.lblToProductCode.Name = "lblToProductCode";
            this.lblToProductCode.Size = new System.Drawing.Size(88, 16);
            this.lblToProductCode.TabIndex = 138;
            this.lblToProductCode.Text = "ไปรหัสสินค้า : ";
            // 
            // txtFromProductName
            // 
            this.txtFromProductName.Location = new System.Drawing.Point(260, 35);
            this.txtFromProductName.MaxLength = 200;
            this.txtFromProductName.Name = "txtFromProductName";
            this.txtFromProductName.ReadOnly = true;
            this.txtFromProductName.Size = new System.Drawing.Size(385, 23);
            this.txtFromProductName.TabIndex = 12;
            // 
            // txtFromProductCode
            // 
            this.txtFromProductCode.Location = new System.Drawing.Point(98, 35);
            this.txtFromProductCode.MaxLength = 20;
            this.txtFromProductCode.Name = "txtFromProductCode";
            this.txtFromProductCode.Size = new System.Drawing.Size(123, 23);
            this.txtFromProductCode.TabIndex = 11;
            // 
            // btnSearchFromProductCode
            // 
            this.btnSearchFromProductCode.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSearchFromProductCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchFromProductCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchFromProductCode.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSearchFromProductCode.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchFromProductCode.Image")));
            this.btnSearchFromProductCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchFromProductCode.Location = new System.Drawing.Point(223, 35);
            this.btnSearchFromProductCode.Name = "btnSearchFromProductCode";
            this.btnSearchFromProductCode.Size = new System.Drawing.Size(35, 23);
            this.btnSearchFromProductCode.TabIndex = 23;
            this.btnSearchFromProductCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchFromProductCode.UseVisualStyleBackColor = false;
            this.btnSearchFromProductCode.Click += new System.EventHandler(this.btnSearchFromProductCode_Click);
            // 
            // lblFromProductCode
            // 
            this.lblFromProductCode.AutoSize = true;
            this.lblFromProductCode.Location = new System.Drawing.Point(3, 38);
            this.lblFromProductCode.Name = "lblFromProductCode";
            this.lblFromProductCode.Size = new System.Drawing.Size(95, 16);
            this.lblFromProductCode.TabIndex = 137;
            this.lblFromProductCode.Text = "โอนรหัสสินค้า : ";
            // 
            // pnlBL
            // 
            this.pnlBL.BackColor = System.Drawing.Color.Azure;
            this.pnlBL.Controls.Add(this.grdDetails);
            this.pnlBL.Controls.Add(this.txtRemark);
            this.pnlBL.Controls.Add(this.lblRemark);
            this.pnlBL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBL.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBL.Location = new System.Drawing.Point(0, 0);
            this.pnlBL.Name = "pnlBL";
            this.pnlBL.Size = new System.Drawing.Size(992, 347);
            this.pnlBL.TabIndex = 116;
            // 
            // grdDetails
            // 
            this.grdDetails.Controls.Add(this.ddlToUOM);
            this.grdDetails.Controls.Add(this.ddlFromUOM);
            this.grdDetails.Controls.Add(this.txtBaseQty);
            this.grdDetails.Controls.Add(this.chkFixBaseQty);
            this.grdDetails.Controls.Add(this.txtFromProductCode);
            this.grdDetails.Controls.Add(this.lblUOM);
            this.grdDetails.Controls.Add(this.btnSearchToProductCode);
            this.grdDetails.Controls.Add(this.txtToProductCode);
            this.grdDetails.Controls.Add(this.lblOrderQty);
            this.grdDetails.Controls.Add(this.lblToProductCode);
            this.grdDetails.Controls.Add(this.txtToProductName);
            this.grdDetails.Controls.Add(this.txtOrderQty);
            this.grdDetails.Controls.Add(this.txtFromProductName);
            this.grdDetails.Controls.Add(this.lblStock);
            this.grdDetails.Controls.Add(this.txtToStock);
            this.grdDetails.Controls.Add(this.btnSearchFromProductCode);
            this.grdDetails.Controls.Add(this.lblFromProductCode);
            this.grdDetails.Controls.Add(this.txtFromStock);
            this.grdDetails.Location = new System.Drawing.Point(5, 10);
            this.grdDetails.Name = "grdDetails";
            this.grdDetails.Size = new System.Drawing.Size(984, 133);
            this.grdDetails.TabIndex = 145;
            this.grdDetails.TabStop = false;
            // 
            // ddlToUOM
            // 
            this.ddlToUOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ddlToUOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlToUOM.FormattingEnabled = true;
            this.ddlToUOM.Location = new System.Drawing.Point(745, 62);
            this.ddlToUOM.Name = "ddlToUOM";
            this.ddlToUOM.Size = new System.Drawing.Size(81, 24);
            this.ddlToUOM.TabIndex = 149;
            // 
            // ddlFromUOM
            // 
            this.ddlFromUOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ddlFromUOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlFromUOM.FormattingEnabled = true;
            this.ddlFromUOM.Location = new System.Drawing.Point(836, 34);
            this.ddlFromUOM.Name = "ddlFromUOM";
            this.ddlFromUOM.Size = new System.Drawing.Size(81, 24);
            this.ddlFromUOM.TabIndex = 147;
            this.ddlFromUOM.SelectionChangeCommitted += new System.EventHandler(this.ddlFromUOM_SelectionChangeCommitted);
            // 
            // txtBaseQty
            // 
            this.txtBaseQty.BackColor = System.Drawing.SystemColors.Window;
            this.txtBaseQty.Enabled = false;
            this.txtBaseQty.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBaseQty.Location = new System.Drawing.Point(745, 90);
            this.txtBaseQty.MaxLength = 5;
            this.txtBaseQty.Name = "txtBaseQty";
            this.txtBaseQty.Size = new System.Drawing.Size(85, 23);
            this.txtBaseQty.TabIndex = 146;
            this.txtBaseQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBaseQty.Visible = false;
            // 
            // chkFixBaseQty
            // 
            this.chkFixBaseQty.AutoSize = true;
            this.chkFixBaseQty.Location = new System.Drawing.Point(633, 92);
            this.chkFixBaseQty.Name = "chkFixBaseQty";
            this.chkFixBaseQty.Size = new System.Drawing.Size(106, 20);
            this.chkFixBaseQty.TabIndex = 145;
            this.chkFixBaseQty.Text = "หน่วยคูณ(เล็ก)";
            this.chkFixBaseQty.UseVisualStyleBackColor = true;
            this.chkFixBaseQty.Visible = false;
            this.chkFixBaseQty.CheckedChanged += new System.EventHandler(this.chkFixBaseQty_CheckedChanged);
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(103, 149);
            this.txtRemark.MaxLength = 255;
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(732, 99);
            this.txtRemark.TabIndex = 18;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(32, 149);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(71, 16);
            this.lblRemark.TabIndex = 110;
            this.lblRemark.Text = "หมายเหตุ : ";
            // 
            // pnlBot
            // 
            this.pnlBot.Controls.Add(this.pnlBL);
            this.pnlBot.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBot.Location = new System.Drawing.Point(0, 112);
            this.pnlBot.Name = "pnlBot";
            this.pnlBot.Size = new System.Drawing.Size(992, 347);
            this.pnlBot.TabIndex = 118;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.Azure;
            this.panel3.Controls.Add(this.pnlBot);
            this.panel3.Controls.Add(this.pnlTop);
            this.panel3.Location = new System.Drawing.Point(7, 41);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(992, 462);
            this.panel3.TabIndex = 17;
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
            this.panel1.TabIndex = 26;
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
            this.panel5.TabIndex = 26;
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
            this.panel2.TabIndex = 27;
            // 
            // frmTR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 541);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTR";
            this.Text = "frmTR";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTR_FormClosed);
            this.Load += new System.EventHandler(this.frmTR_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTR.ResumeLayout(false);
            this.pnlTR.PerformLayout();
            this.pnlTL.ResumeLayout(false);
            this.pnlTL.PerformLayout();
            this.pnlBL.ResumeLayout(false);
            this.pnlBL.PerformLayout();
            this.grdDetails.ResumeLayout(false);
            this.grdDetails.PerformLayout();
            this.pnlBot.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FormPic)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlTR;
        private System.Windows.Forms.MaskedTextBox txdDocNo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox ddlDocStatus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpDocDate;
        private System.Windows.Forms.Button btnSearchDoc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pnlTL;
        private System.Windows.Forms.Panel pnlBL;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.Panel pnlBot;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox FormPic;
        private System.Windows.Forms.Label FormHeader;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtToWHName;
        private System.Windows.Forms.TextBox txtToWHCode;
        private System.Windows.Forms.Button btnSeatchToWHCode;
        private System.Windows.Forms.Label lblToWHCode;
        private System.Windows.Forms.TextBox txtFromWHName;
        private System.Windows.Forms.TextBox txtFromWHCode;
        private System.Windows.Forms.Button btnSearchFromWHCode;
        private System.Windows.Forms.Label lblFromWHCode;
        private System.Windows.Forms.TextBox txtCrUser;
        private System.Windows.Forms.Label lblCrUser;
        private System.Windows.Forms.TextBox txtBranchName;
        private System.Windows.Forms.TextBox txtBranchCode;
        private System.Windows.Forms.Button btnSearchBranchCode;
        private System.Windows.Forms.Label lblBranchCode;
        private System.Windows.Forms.Label lblUOM;
        private System.Windows.Forms.Label lblOrderQty;
        private System.Windows.Forms.TextBox txtOrderQty;
        private System.Windows.Forms.TextBox txtToStock;
        private System.Windows.Forms.TextBox txtFromStock;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.TextBox txtToProductName;
        private System.Windows.Forms.TextBox txtToProductCode;
        private System.Windows.Forms.Button btnSearchToProductCode;
        private System.Windows.Forms.Label lblToProductCode;
        private System.Windows.Forms.TextBox txtFromProductName;
        private System.Windows.Forms.TextBox txtFromProductCode;
        private System.Windows.Forms.Button btnSearchFromProductCode;
        private System.Windows.Forms.Label lblFromProductCode;
        private System.Windows.Forms.GroupBox grdDetails;
        private System.Windows.Forms.TextBox txtBaseQty;
        private System.Windows.Forms.CheckBox chkFixBaseQty;
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
        private System.Windows.Forms.ComboBox ddlToUOM;
        private System.Windows.Forms.ComboBox ddlFromUOM;
    }
}