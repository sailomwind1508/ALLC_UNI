namespace AllCashUFormsApp.View.UControl
{
    partial class frmSupplierType
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSupplierType));
            this.panel7 = new System.Windows.Forms.Panel();
            this.SplitContainerForDesign = new System.Windows.Forms.SplitContainer();
            this.pnlGridView = new System.Windows.Forms.Panel();
            this.grdApSupplierType = new System.Windows.Forms.DataGridView();
            this.colAPSupplierTypeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colApSupplierTypeCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colApSupplierTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCrDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCrUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEdDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEdUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lbl_Qty = new System.Windows.Forms.Label();
            this.lbl_List = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.rdoC = new System.Windows.Forms.RadioButton();
            this.rdoN = new System.Windows.Forms.RadioButton();
            this.label26 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.btnNormal = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlEdit = new System.Windows.Forms.Panel();
            this.txtApSupplierTypeName = new System.Windows.Forms.TextBox();
            this.txtApSupplierTypeCode = new System.Windows.Forms.TextBox();
            this.lbl_Name = new System.Windows.Forms.Label();
            this.lbl_Code = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnClose = new AllCashUFormsApp.View.UControl.CloseButton(this.components);
            this.btnAdd = new AllCashUFormsApp.View.UControl.AddButton(this.components);
            this.btnEdit = new AllCashUFormsApp.View.UControl.EditButton(this.components);
            this.btnPrint = new AllCashUFormsApp.View.UControl.PrintButton(this.components);
            this.btnRemove = new AllCashUFormsApp.View.UControl.RemoveButton(this.components);
            this.btnCancel = new AllCashUFormsApp.View.UControl.CancelButton(this.components);
            this.btnCopy = new AllCashUFormsApp.View.UControl.CopyButton(this.components);
            this.btnSave = new AllCashUFormsApp.View.UControl.SaveButton(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerForDesign)).BeginInit();
            this.SplitContainerForDesign.Panel1.SuspendLayout();
            this.SplitContainerForDesign.Panel2.SuspendLayout();
            this.SplitContainerForDesign.SuspendLayout();
            this.pnlGridView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdApSupplierType)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.panel8.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            this.pnlEdit.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Azure;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Controls.Add(this.SplitContainerForDesign);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 28);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(807, 422);
            this.panel7.TabIndex = 148;
            // 
            // SplitContainerForDesign
            // 
            this.SplitContainerForDesign.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SplitContainerForDesign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainerForDesign.Location = new System.Drawing.Point(0, 0);
            this.SplitContainerForDesign.Name = "SplitContainerForDesign";
            // 
            // SplitContainerForDesign.Panel1
            // 
            this.SplitContainerForDesign.Panel1.Controls.Add(this.pnlGridView);
            this.SplitContainerForDesign.Panel1.Controls.Add(this.pnlBottom);
            this.SplitContainerForDesign.Panel1.Controls.Add(this.pnlSearch);
            // 
            // SplitContainerForDesign.Panel2
            // 
            this.SplitContainerForDesign.Panel2.BackColor = System.Drawing.Color.SkyBlue;
            this.SplitContainerForDesign.Panel2.Controls.Add(this.pnlStatus);
            this.SplitContainerForDesign.Panel2.Controls.Add(this.pnlEdit);
            this.SplitContainerForDesign.Size = new System.Drawing.Size(803, 418);
            this.SplitContainerForDesign.SplitterDistance = 465;
            this.SplitContainerForDesign.TabIndex = 0;
            // 
            // pnlGridView
            // 
            this.pnlGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGridView.Controls.Add(this.grdApSupplierType);
            this.pnlGridView.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.pnlGridView.Location = new System.Drawing.Point(0, 74);
            this.pnlGridView.Name = "pnlGridView";
            this.pnlGridView.Size = new System.Drawing.Size(461, 317);
            this.pnlGridView.TabIndex = 2;
            // 
            // grdApSupplierType
            // 
            this.grdApSupplierType.AllowUserToAddRows = false;
            this.grdApSupplierType.AllowUserToDeleteRows = false;
            this.grdApSupplierType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdApSupplierType.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAPSupplierTypeID,
            this.colApSupplierTypeCode,
            this.colApSupplierTypeName,
            this.colCrDate,
            this.colCrUser,
            this.colEdDate,
            this.colEdUser});
            this.grdApSupplierType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdApSupplierType.Location = new System.Drawing.Point(0, 0);
            this.grdApSupplierType.Name = "grdApSupplierType";
            this.grdApSupplierType.ReadOnly = true;
            this.grdApSupplierType.Size = new System.Drawing.Size(461, 317);
            this.grdApSupplierType.TabIndex = 0;
            this.grdApSupplierType.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdApSupplierType_CellClick);
            this.grdApSupplierType.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdApSupplierType_CellDoubleClick);
            this.grdApSupplierType.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.grdApSupplierType_RowPostPaint);
            this.grdApSupplierType.SelectionChanged += new System.EventHandler(this.grdApSupplierType_SelectionChanged);
            // 
            // colAPSupplierTypeID
            // 
            this.colAPSupplierTypeID.DataPropertyName = "APSupplierTypeID";
            this.colAPSupplierTypeID.HeaderText = "APSupplierTypeID";
            this.colAPSupplierTypeID.Name = "colAPSupplierTypeID";
            this.colAPSupplierTypeID.ReadOnly = true;
            this.colAPSupplierTypeID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colAPSupplierTypeID.Visible = false;
            // 
            // colApSupplierTypeCode
            // 
            this.colApSupplierTypeCode.DataPropertyName = "ApSupplierTypeCode";
            this.colApSupplierTypeCode.HeaderText = "รหัสกลุ่มสินค้า";
            this.colApSupplierTypeCode.MaxInputLength = 2;
            this.colApSupplierTypeCode.Name = "colApSupplierTypeCode";
            this.colApSupplierTypeCode.ReadOnly = true;
            // 
            // colApSupplierTypeName
            // 
            this.colApSupplierTypeName.DataPropertyName = "ApSupplierTypeName";
            this.colApSupplierTypeName.HeaderText = "ชื่อกลุ่มสินค้า";
            this.colApSupplierTypeName.Name = "colApSupplierTypeName";
            this.colApSupplierTypeName.ReadOnly = true;
            this.colApSupplierTypeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colCrDate
            // 
            this.colCrDate.DataPropertyName = "CrDate";
            this.colCrDate.HeaderText = "วันที่เพิ่ม";
            this.colCrDate.Name = "colCrDate";
            this.colCrDate.ReadOnly = true;
            // 
            // colCrUser
            // 
            this.colCrUser.DataPropertyName = "CrUser";
            this.colCrUser.HeaderText = "เพิ่มโดย";
            this.colCrUser.MaxInputLength = 50;
            this.colCrUser.Name = "colCrUser";
            this.colCrUser.ReadOnly = true;
            this.colCrUser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colCrUser.Width = 80;
            // 
            // colEdDate
            // 
            this.colEdDate.DataPropertyName = "EdDate";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle3.Format = "dd/MM/yyyy";
            dataGridViewCellStyle3.NullValue = null;
            this.colEdDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.colEdDate.HeaderText = "วันที่แก้ไข";
            this.colEdDate.Name = "colEdDate";
            this.colEdDate.ReadOnly = true;
            this.colEdDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colEdUser
            // 
            this.colEdUser.DataPropertyName = "EdUser";
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.colEdUser.DefaultCellStyle = dataGridViewCellStyle4;
            this.colEdUser.HeaderText = "แก้ไขโดย";
            this.colEdUser.MaxInputLength = 50;
            this.colEdUser.Name = "colEdUser";
            this.colEdUser.ReadOnly = true;
            this.colEdUser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colEdUser.Width = 80;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.SkyBlue;
            this.pnlBottom.Controls.Add(this.lbl_Qty);
            this.pnlBottom.Controls.Add(this.lbl_List);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 391);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(461, 23);
            this.pnlBottom.TabIndex = 1;
            // 
            // lbl_Qty
            // 
            this.lbl_Qty.AutoSize = true;
            this.lbl_Qty.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lbl_Qty.Location = new System.Drawing.Point(92, 2);
            this.lbl_Qty.Name = "lbl_Qty";
            this.lbl_Qty.Size = new System.Drawing.Size(12, 16);
            this.lbl_Qty.TabIndex = 210;
            this.lbl_Qty.Text = ".";
            // 
            // lbl_List
            // 
            this.lbl_List.AutoSize = true;
            this.lbl_List.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lbl_List.Location = new System.Drawing.Point(3, 2);
            this.lbl_List.Name = "lbl_List";
            this.lbl_List.Size = new System.Drawing.Size(83, 16);
            this.lbl_List.TabIndex = 209;
            this.lbl_List.Text = "จำนวนรายการ";
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.SkyBlue;
            this.pnlSearch.Controls.Add(this.panel8);
            this.pnlSearch.Controls.Add(this.label26);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.label13);
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(461, 74);
            this.pnlSearch.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Azure;
            this.panel8.Controls.Add(this.rdoC);
            this.panel8.Controls.Add(this.rdoN);
            this.panel8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel8.Location = new System.Drawing.Point(65, 42);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(152, 25);
            this.panel8.TabIndex = 208;
            // 
            // rdoC
            // 
            this.rdoC.AutoSize = true;
            this.rdoC.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.rdoC.Location = new System.Drawing.Point(85, 3);
            this.rdoC.Name = "rdoC";
            this.rdoC.Size = new System.Drawing.Size(62, 20);
            this.rdoC.TabIndex = 18;
            this.rdoC.Text = "ยกเลิก";
            this.rdoC.UseVisualStyleBackColor = true;
            this.rdoC.CheckedChanged += new System.EventHandler(this.rdoC_CheckedChanged);
            // 
            // rdoN
            // 
            this.rdoN.AutoSize = true;
            this.rdoN.BackColor = System.Drawing.Color.Azure;
            this.rdoN.Checked = true;
            this.rdoN.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.rdoN.Location = new System.Drawing.Point(6, 3);
            this.rdoN.Name = "rdoN";
            this.rdoN.Size = new System.Drawing.Size(50, 20);
            this.rdoN.TabIndex = 17;
            this.rdoN.TabStop = true;
            this.rdoN.Text = "ปกติ";
            this.rdoN.UseVisualStyleBackColor = false;
            this.rdoN.CheckedChanged += new System.EventHandler(this.rdoN_CheckedChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label26.Location = new System.Drawing.Point(6, 47);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(57, 16);
            this.label26.TabIndex = 207;
            this.label26.Text = "สถานะ : ";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSearch.Image = global::AllCashUFormsApp.Properties.Resources.search;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(388, 14);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(70, 26);
            this.btnSearch.TabIndex = 206;
            this.btnSearch.Text = "ค้นหา";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label13.Location = new System.Drawing.Point(8, 19);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(51, 16);
            this.label13.TabIndex = 204;
            this.label13.Text = "ค้นหา  :";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtSearch.Location = new System.Drawing.Point(65, 16);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSearch.MaxLength = 50;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(317, 23);
            this.txtSearch.TabIndex = 205;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // pnlStatus
            // 
            this.pnlStatus.BackColor = System.Drawing.Color.Azure;
            this.pnlStatus.Controls.Add(this.btnNormal);
            this.pnlStatus.Controls.Add(this.lblStatus);
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStatus.Location = new System.Drawing.Point(0, 59);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(330, 28);
            this.pnlStatus.TabIndex = 13;
            // 
            // btnNormal
            // 
            this.btnNormal.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnNormal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNormal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNormal.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnNormal.Location = new System.Drawing.Point(71, 2);
            this.btnNormal.Name = "btnNormal";
            this.btnNormal.Size = new System.Drawing.Size(59, 23);
            this.btnNormal.TabIndex = 12;
            this.btnNormal.Text = "ปกติ";
            this.btnNormal.UseVisualStyleBackColor = false;
            this.btnNormal.Click += new System.EventHandler(this.btnNormal_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lblStatus.Location = new System.Drawing.Point(11, 4);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(53, 16);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "สถานะ :";
            // 
            // pnlEdit
            // 
            this.pnlEdit.BackColor = System.Drawing.Color.Azure;
            this.pnlEdit.Controls.Add(this.txtApSupplierTypeName);
            this.pnlEdit.Controls.Add(this.txtApSupplierTypeCode);
            this.pnlEdit.Controls.Add(this.lbl_Name);
            this.pnlEdit.Controls.Add(this.lbl_Code);
            this.pnlEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEdit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.pnlEdit.Location = new System.Drawing.Point(0, 0);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(330, 59);
            this.pnlEdit.TabIndex = 2;
            // 
            // txtApSupplierTypeName
            // 
            this.txtApSupplierTypeName.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtApSupplierTypeName.Location = new System.Drawing.Point(71, 33);
            this.txtApSupplierTypeName.MaxLength = 50;
            this.txtApSupplierTypeName.Name = "txtApSupplierTypeName";
            this.txtApSupplierTypeName.Size = new System.Drawing.Size(190, 23);
            this.txtApSupplierTypeName.TabIndex = 7;
            // 
            // txtApSupplierTypeCode
            // 
            this.txtApSupplierTypeCode.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtApSupplierTypeCode.Location = new System.Drawing.Point(71, 8);
            this.txtApSupplierTypeCode.MaxLength = 2;
            this.txtApSupplierTypeCode.Name = "txtApSupplierTypeCode";
            this.txtApSupplierTypeCode.Size = new System.Drawing.Size(190, 23);
            this.txtApSupplierTypeCode.TabIndex = 8;
            // 
            // lbl_Name
            // 
            this.lbl_Name.AutoSize = true;
            this.lbl_Name.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lbl_Name.Location = new System.Drawing.Point(26, 36);
            this.lbl_Name.Name = "lbl_Name";
            this.lbl_Name.Size = new System.Drawing.Size(37, 16);
            this.lbl_Name.TabIndex = 5;
            this.lbl_Name.Text = "ชื่อ  :";
            // 
            // lbl_Code
            // 
            this.lbl_Code.AutoSize = true;
            this.lbl_Code.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lbl_Code.Location = new System.Drawing.Point(25, 9);
            this.lbl_Code.Name = "lbl_Code";
            this.lbl_Code.Size = new System.Drawing.Size(39, 16);
            this.lbl_Code.TabIndex = 6;
            this.lbl_Code.Text = "รหัส :";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnClose);
            this.panel5.Controls.Add(this.btnAdd);
            this.panel5.Controls.Add(this.btnEdit);
            this.panel5.Controls.Add(this.btnPrint);
            this.panel5.Controls.Add(this.btnRemove);
            this.panel5.Controls.Add(this.btnCancel);
            this.panel5.Controls.Add(this.btnCopy);
            this.panel5.Controls.Add(this.btnSave);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(678, 28);
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
            this.btnClose.Location = new System.Drawing.Point(485, 3);
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
            this.btnAdd.Location = new System.Drawing.Point(7, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(56, 23);
            this.btnAdd.TabIndex = 23;
            this.btnAdd.Text = "เพิ่ม";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Azure;
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(807, 28);
            this.panel1.TabIndex = 147;
            // 
            // frmSupplierType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 450);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSupplierType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "กลุ่มสินค้า";
            this.Load += new System.EventHandler(this.frmSearchSupplierType_Load);
            this.panel7.ResumeLayout(false);
            this.SplitContainerForDesign.Panel1.ResumeLayout(false);
            this.SplitContainerForDesign.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerForDesign)).EndInit();
            this.SplitContainerForDesign.ResumeLayout(false);
            this.pnlGridView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdApSupplierType)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.pnlStatus.ResumeLayout(false);
            this.pnlStatus.PerformLayout();
            this.pnlEdit.ResumeLayout(false);
            this.pnlEdit.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel7;
        private CloseButton btnClose;
        private AddButton btnAdd;
        private EditButton btnEdit;
        private PrintButton btnPrint;
        private RemoveButton btnRemove;
        private CancelButton btnCancel;
        private CopyButton btnCopy;
        private SaveButton btnSave;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer SplitContainerForDesign;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.RadioButton rdoC;
        private System.Windows.Forms.RadioButton rdoN;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel pnlGridView;
        private System.Windows.Forms.DataGridView grdApSupplierType;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lbl_Qty;
        private System.Windows.Forms.Label lbl_List;
        private System.Windows.Forms.Panel pnlEdit;
        private System.Windows.Forms.TextBox txtApSupplierTypeName;
        private System.Windows.Forms.TextBox txtApSupplierTypeCode;
        private System.Windows.Forms.Label lbl_Name;
        private System.Windows.Forms.Label lbl_Code;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Button btnNormal;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAPSupplierTypeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colApSupplierTypeCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colApSupplierTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCrDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCrUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEdDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEdUser;
    }
}