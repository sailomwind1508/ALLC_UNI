namespace AllCashUFormsApp.View.Page
{
    partial class frmProductMovement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProductMovement));
            this.FormPic = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.FormHeader = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlTR = new System.Windows.Forms.Panel();
            this.grbPrdMMType = new System.Windows.Forms.GroupBox();
            this.rdoDetails = new System.Windows.Forms.RadioButton();
            this.rdoSummary = new System.Windows.Forms.RadioButton();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlTL = new System.Windows.Forms.Panel();
            this.txtProID = new System.Windows.Forms.TextBox();
            this.chkAllMM = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtToWHCode = new System.Windows.Forms.TextBox();
            this.btnToWHCode = new System.Windows.Forms.Button();
            this.ddlProductSubGroup = new System.Windows.Forms.ComboBox();
            this.ddlProductGroup = new System.Windows.Forms.ComboBox();
            this.txtFromWHCode = new System.Windows.Forms.TextBox();
            this.btnFromWHCode = new System.Windows.Forms.Button();
            this.txtBranchName = new System.Windows.Forms.TextBox();
            this.txtBranchCode = new System.Windows.Forms.TextBox();
            this.btnSearchBranchCode = new System.Windows.Forms.Button();
            this.lblEmpCode = new System.Windows.Forms.Label();
            this.lblWHCode = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblBranchCode = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ccbProductCode = new CheckComboBoxTest.CheckedComboBox();
            this.btnClose = new AllCashUFormsApp.View.UControl.CloseButton(this.components);
            this.btnAdd = new AllCashUFormsApp.View.UControl.AddButton(this.components);
            this.btnExcel = new AllCashUFormsApp.View.UControl.ExcelButton(this.components);
            this.btnEdit = new AllCashUFormsApp.View.UControl.EditButton(this.components);
            this.btnPrint = new AllCashUFormsApp.View.UControl.PrintButton(this.components);
            this.btnRemove = new AllCashUFormsApp.View.UControl.RemoveButton(this.components);
            this.btnCancel = new AllCashUFormsApp.View.UControl.CancelButton(this.components);
            this.btnCopy = new AllCashUFormsApp.View.UControl.CopyButton(this.components);
            this.btnSave = new AllCashUFormsApp.View.UControl.SaveButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.FormPic)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.pnlTR.SuspendLayout();
            this.grbPrdMMType.SuspendLayout();
            this.pnlTL.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // FormPic
            // 
            this.FormPic.Location = new System.Drawing.Point(12, 3);
            this.FormPic.Name = "FormPic";
            this.FormPic.Size = new System.Drawing.Size(30, 30);
            this.FormPic.TabIndex = 1;
            this.FormPic.TabStop = false;
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
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.Azure;
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.pnlTop);
            this.panel3.Location = new System.Drawing.Point(7, 41);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(992, 462);
            this.panel3.TabIndex = 17;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.grdList);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.panel6.Location = new System.Drawing.Point(0, 106);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(992, 356);
            this.panel6.TabIndex = 119;
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToResizeColumns = false;
            this.grdList.AllowUserToResizeRows = false;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.MultiSelect = false;
            this.grdList.Name = "grdList";
            this.grdList.Size = new System.Drawing.Size(992, 356);
            this.grdList.TabIndex = 15;
            this.grdList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.grdList_RowPostPaint);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Azure;
            this.pnlTop.Controls.Add(this.pnlTR);
            this.pnlTop.Controls.Add(this.pnlTL);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(992, 106);
            this.pnlTop.TabIndex = 118;
            // 
            // pnlTR
            // 
            this.pnlTR.BackColor = System.Drawing.Color.Azure;
            this.pnlTR.Controls.Add(this.grbPrdMMType);
            this.pnlTR.Controls.Add(this.dtpToDate);
            this.pnlTR.Controls.Add(this.dtpFromDate);
            this.pnlTR.Controls.Add(this.label3);
            this.pnlTR.Controls.Add(this.label8);
            this.pnlTR.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlTR.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTR.Location = new System.Drawing.Point(728, 0);
            this.pnlTR.Name = "pnlTR";
            this.pnlTR.Size = new System.Drawing.Size(264, 106);
            this.pnlTR.TabIndex = 21;
            // 
            // grbPrdMMType
            // 
            this.grbPrdMMType.Controls.Add(this.rdoDetails);
            this.grbPrdMMType.Controls.Add(this.rdoSummary);
            this.grbPrdMMType.Location = new System.Drawing.Point(24, 51);
            this.grbPrdMMType.Name = "grbPrdMMType";
            this.grbPrdMMType.Size = new System.Drawing.Size(224, 45);
            this.grbPrdMMType.TabIndex = 107;
            this.grbPrdMMType.TabStop = false;
            this.grbPrdMMType.Text = "รูปแบบรายงาน";
            // 
            // rdoDetails
            // 
            this.rdoDetails.AutoSize = true;
            this.rdoDetails.Location = new System.Drawing.Point(112, 18);
            this.rdoDetails.Name = "rdoDetails";
            this.rdoDetails.Size = new System.Drawing.Size(112, 20);
            this.rdoDetails.TabIndex = 18;
            this.rdoDetails.TabStop = true;
            this.rdoDetails.Text = "แบบรายละเอียด";
            this.rdoDetails.UseVisualStyleBackColor = true;
            this.rdoDetails.CheckedChanged += new System.EventHandler(this.rdoDetails_CheckedChanged);
            // 
            // rdoSummary
            // 
            this.rdoSummary.AutoSize = true;
            this.rdoSummary.Location = new System.Drawing.Point(14, 18);
            this.rdoSummary.Name = "rdoSummary";
            this.rdoSummary.Size = new System.Drawing.Size(92, 20);
            this.rdoSummary.TabIndex = 17;
            this.rdoSummary.TabStop = true;
            this.rdoSummary.Text = "แบบสรุปรวม";
            this.rdoSummary.UseVisualStyleBackColor = true;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.Location = new System.Drawing.Point(85, 28);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(163, 23);
            this.dtpToDate.TabIndex = 105;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Location = new System.Drawing.Point(85, 4);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(163, 23);
            this.dtpFromDate.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 106;
            this.label3.Text = "ถึงวันที่ : ";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(42, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 16);
            this.label8.TabIndex = 104;
            this.label8.Text = "วันที่ : ";
            // 
            // pnlTL
            // 
            this.pnlTL.BackColor = System.Drawing.Color.Azure;
            this.pnlTL.Controls.Add(this.txtProID);
            this.pnlTL.Controls.Add(this.chkAllMM);
            this.pnlTL.Controls.Add(this.ccbProductCode);
            this.pnlTL.Controls.Add(this.btnClear);
            this.pnlTL.Controls.Add(this.btnSearch);
            this.pnlTL.Controls.Add(this.txtToWHCode);
            this.pnlTL.Controls.Add(this.btnToWHCode);
            this.pnlTL.Controls.Add(this.ddlProductSubGroup);
            this.pnlTL.Controls.Add(this.ddlProductGroup);
            this.pnlTL.Controls.Add(this.txtFromWHCode);
            this.pnlTL.Controls.Add(this.btnFromWHCode);
            this.pnlTL.Controls.Add(this.txtBranchName);
            this.pnlTL.Controls.Add(this.txtBranchCode);
            this.pnlTL.Controls.Add(this.btnSearchBranchCode);
            this.pnlTL.Controls.Add(this.lblEmpCode);
            this.pnlTL.Controls.Add(this.lblWHCode);
            this.pnlTL.Controls.Add(this.label6);
            this.pnlTL.Controls.Add(this.lblBranchCode);
            this.pnlTL.Controls.Add(this.label2);
            this.pnlTL.Controls.Add(this.label1);
            this.pnlTL.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTL.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTL.Location = new System.Drawing.Point(0, 0);
            this.pnlTL.Name = "pnlTL";
            this.pnlTL.Size = new System.Drawing.Size(722, 106);
            this.pnlTL.TabIndex = 20;
            // 
            // txtProID
            // 
            this.txtProID.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtProID.Location = new System.Drawing.Point(89, 79);
            this.txtProID.MaxLength = 5000;
            this.txtProID.Name = "txtProID";
            this.txtProID.ReadOnly = true;
            this.txtProID.Size = new System.Drawing.Size(468, 23);
            this.txtProID.TabIndex = 128;
            this.txtProID.Click += new System.EventHandler(this.txtProID_Click);
            // 
            // chkAllMM
            // 
            this.chkAllMM.AutoSize = true;
            this.chkAllMM.BackColor = System.Drawing.Color.PaleTurquoise;
            this.chkAllMM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkAllMM.Location = new System.Drawing.Point(563, 27);
            this.chkAllMM.Name = "chkAllMM";
            this.chkAllMM.Size = new System.Drawing.Size(112, 20);
            this.chkAllMM.TabIndex = 127;
            this.chkAllMM.Text = "ดึงข้อมูลทุกคลัง";
            this.chkAllMM.UseVisualStyleBackColor = false;
            this.chkAllMM.CheckedChanged += new System.EventHandler(this.chkAllMM_CheckedChanged);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.Location = new System.Drawing.Point(631, 78);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(67, 26);
            this.btnClear.TabIndex = 126;
            this.btnClear.Text = "ล้าง";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(559, 78);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(67, 26);
            this.btnSearch.TabIndex = 125;
            this.btnSearch.Text = "ค้นหา";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtToWHCode
            // 
            this.txtToWHCode.Location = new System.Drawing.Point(364, 28);
            this.txtToWHCode.MaxLength = 15;
            this.txtToWHCode.Name = "txtToWHCode";
            this.txtToWHCode.Size = new System.Drawing.Size(156, 23);
            this.txtToWHCode.TabIndex = 121;
            // 
            // btnToWHCode
            // 
            this.btnToWHCode.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnToWHCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToWHCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToWHCode.ForeColor = System.Drawing.SystemColors.Control;
            this.btnToWHCode.Image = ((System.Drawing.Image)(resources.GetObject("btnToWHCode.Image")));
            this.btnToWHCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnToWHCode.Location = new System.Drawing.Point(522, 28);
            this.btnToWHCode.Name = "btnToWHCode";
            this.btnToWHCode.Size = new System.Drawing.Size(35, 23);
            this.btnToWHCode.TabIndex = 122;
            this.btnToWHCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnToWHCode.UseVisualStyleBackColor = false;
            this.btnToWHCode.Click += new System.EventHandler(this.btnSeatchToWHCode_Click);
            // 
            // ddlProductSubGroup
            // 
            this.ddlProductSubGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ddlProductSubGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlProductSubGroup.FormattingEnabled = true;
            this.ddlProductSubGroup.Location = new System.Drawing.Point(410, 52);
            this.ddlProductSubGroup.Name = "ddlProductSubGroup";
            this.ddlProductSubGroup.Size = new System.Drawing.Size(215, 24);
            this.ddlProductSubGroup.TabIndex = 120;
            // 
            // ddlProductGroup
            // 
            this.ddlProductGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ddlProductGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlProductGroup.FormattingEnabled = true;
            this.ddlProductGroup.Location = new System.Drawing.Point(89, 52);
            this.ddlProductGroup.Name = "ddlProductGroup";
            this.ddlProductGroup.Size = new System.Drawing.Size(219, 24);
            this.ddlProductGroup.TabIndex = 119;
            this.ddlProductGroup.SelectedIndexChanged += new System.EventHandler(this.ddlProductGroup_SelectedIndexChanged);
            // 
            // txtFromWHCode
            // 
            this.txtFromWHCode.Location = new System.Drawing.Point(89, 28);
            this.txtFromWHCode.MaxLength = 15;
            this.txtFromWHCode.Name = "txtFromWHCode";
            this.txtFromWHCode.Size = new System.Drawing.Size(123, 23);
            this.txtFromWHCode.TabIndex = 110;
            // 
            // btnFromWHCode
            // 
            this.btnFromWHCode.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnFromWHCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFromWHCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFromWHCode.ForeColor = System.Drawing.SystemColors.Control;
            this.btnFromWHCode.Image = ((System.Drawing.Image)(resources.GetObject("btnFromWHCode.Image")));
            this.btnFromWHCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFromWHCode.Location = new System.Drawing.Point(214, 28);
            this.btnFromWHCode.Name = "btnFromWHCode";
            this.btnFromWHCode.Size = new System.Drawing.Size(35, 23);
            this.btnFromWHCode.TabIndex = 111;
            this.btnFromWHCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFromWHCode.UseVisualStyleBackColor = false;
            this.btnFromWHCode.Click += new System.EventHandler(this.btnSearchFromWHCode_Click);
            // 
            // txtBranchName
            // 
            this.txtBranchName.Location = new System.Drawing.Point(252, 4);
            this.txtBranchName.MaxLength = 200;
            this.txtBranchName.Name = "txtBranchName";
            this.txtBranchName.ReadOnly = true;
            this.txtBranchName.Size = new System.Drawing.Size(305, 23);
            this.txtBranchName.TabIndex = 2;
            // 
            // txtBranchCode
            // 
            this.txtBranchCode.Location = new System.Drawing.Point(89, 4);
            this.txtBranchCode.MaxLength = 5;
            this.txtBranchCode.Name = "txtBranchCode";
            this.txtBranchCode.Size = new System.Drawing.Size(123, 23);
            this.txtBranchCode.TabIndex = 0;
            // 
            // btnSearchBranchCode
            // 
            this.btnSearchBranchCode.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSearchBranchCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchBranchCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchBranchCode.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSearchBranchCode.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchBranchCode.Image")));
            this.btnSearchBranchCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchBranchCode.Location = new System.Drawing.Point(214, 4);
            this.btnSearchBranchCode.Name = "btnSearchBranchCode";
            this.btnSearchBranchCode.Size = new System.Drawing.Size(35, 23);
            this.btnSearchBranchCode.TabIndex = 1;
            this.btnSearchBranchCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchBranchCode.UseVisualStyleBackColor = false;
            this.btnSearchBranchCode.Click += new System.EventHandler(this.btnSearchBranchCode_Click);
            // 
            // lblEmpCode
            // 
            this.lblEmpCode.AutoSize = true;
            this.lblEmpCode.Location = new System.Drawing.Point(14, 55);
            this.lblEmpCode.Name = "lblEmpCode";
            this.lblEmpCode.Size = new System.Drawing.Size(75, 16);
            this.lblEmpCode.TabIndex = 117;
            this.lblEmpCode.Text = "กลุ่มสินค้า : ";
            // 
            // lblWHCode
            // 
            this.lblWHCode.AutoSize = true;
            this.lblWHCode.Location = new System.Drawing.Point(16, 31);
            this.lblWHCode.Name = "lblWHCode";
            this.lblWHCode.Size = new System.Drawing.Size(73, 16);
            this.lblWHCode.TabIndex = 113;
            this.lblWHCode.Text = "คลังสินค้า : ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 16);
            this.label6.TabIndex = 109;
            this.label6.Text = "รหัสสินค้า : ";
            // 
            // lblBranchCode
            // 
            this.lblBranchCode.AutoSize = true;
            this.lblBranchCode.Location = new System.Drawing.Point(42, 7);
            this.lblBranchCode.Name = "lblBranchCode";
            this.lblBranchCode.Size = new System.Drawing.Size(47, 16);
            this.lblBranchCode.TabIndex = 99;
            this.lblBranchCode.Text = "เดโป้ : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(331, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 16);
            this.label2.TabIndex = 123;
            this.label2.Text = "ถึง : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(313, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 16);
            this.label1.TabIndex = 118;
            this.label1.Text = "กลุ่มย่อยสินค้า : ";
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
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ccbProductCode
            // 
            this.ccbProductCode.CheckOnClick = true;
            this.ccbProductCode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ccbProductCode.DropDownHeight = 1;
            this.ccbProductCode.FormattingEnabled = true;
            this.ccbProductCode.IntegralHeight = false;
            this.ccbProductCode.Location = new System.Drawing.Point(559, 2);
            this.ccbProductCode.Name = "ccbProductCode";
            this.ccbProductCode.Size = new System.Drawing.Size(135, 24);
            this.ccbProductCode.TabIndex = 108;
            this.ccbProductCode.ValueSeparator = ", ";
            this.ccbProductCode.Visible = false;
            this.ccbProductCode.DropDownClosed += new System.EventHandler(this.ccb_DropDownClosed);
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
            // frmProductMovement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 541);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmProductMovement";
            this.Text = "frmProductMovement";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmProductMovement_FormClosed);
            this.Load += new System.EventHandler(this.frmProductMovement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FormPic)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTR.ResumeLayout(false);
            this.pnlTR.PerformLayout();
            this.grbPrdMMType.ResumeLayout(false);
            this.grbPrdMMType.PerformLayout();
            this.pnlTL.ResumeLayout(false);
            this.pnlTL.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox FormPic;
        private UControl.CloseButton btnClose;
        private UControl.AddButton btnAdd;
        private UControl.ExcelButton btnExcel;
        private UControl.EditButton btnEdit;
        private UControl.PrintButton btnPrint;
        private UControl.RemoveButton btnRemove;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label FormHeader;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private UControl.CancelButton btnCancel;
        private UControl.CopyButton btnCopy;
        private UControl.SaveButton btnSave;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlTR;
        private System.Windows.Forms.GroupBox grbPrdMMType;
        private System.Windows.Forms.RadioButton rdoDetails;
        private System.Windows.Forms.RadioButton rdoSummary;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Panel pnlTL;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtToWHCode;
        private System.Windows.Forms.Button btnToWHCode;
        private System.Windows.Forms.ComboBox ddlProductSubGroup;
        private System.Windows.Forms.ComboBox ddlProductGroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblEmpCode;
        private System.Windows.Forms.TextBox txtFromWHCode;
        private System.Windows.Forms.Button btnFromWHCode;
        private System.Windows.Forms.Label lblWHCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBranchName;
        private System.Windows.Forms.TextBox txtBranchCode;
        private System.Windows.Forms.Button btnSearchBranchCode;
        private System.Windows.Forms.Label lblBranchCode;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridView grdList;
        private CheckComboBoxTest.CheckedComboBox ccbProductCode;
        private System.Windows.Forms.CheckBox chkAllMM;
        private System.Windows.Forms.TextBox txtProID;
    }
}