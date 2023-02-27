namespace AllCashUFormsApp.View.UControl
{
    partial class frmSearchShopType
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearchShopType));
            this.gridShopType = new System.Windows.Forms.DataGridView();
            this.colShopTypeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShopTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCrDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCrUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEdDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEdUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFlagDel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pnlEdit = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNormal = new System.Windows.Forms.Button();
            this.chkNormal = new System.Windows.Forms.CheckBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtShopTypeName = new System.Windows.Forms.TextBox();
            this.txtShopTypeID = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.rdoC = new System.Windows.Forms.RadioButton();
            this.rdoN = new System.Windows.Forms.RadioButton();
            this.label26 = new System.Windows.Forms.Label();
            this.btnSearchShopType = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
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
            this.panel7 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.gridShopType)).BeginInit();
            this.pnlEdit.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridShopType
            // 
            this.gridShopType.AllowUserToAddRows = false;
            this.gridShopType.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridShopType.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridShopType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridShopType.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colShopTypeID,
            this.colShopTypeName,
            this.colCrDate,
            this.colCrUser,
            this.colEdDate,
            this.colEdUser,
            this.colFlagDel});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridShopType.DefaultCellStyle = dataGridViewCellStyle8;
            this.gridShopType.Location = new System.Drawing.Point(3, 64);
            this.gridShopType.Name = "gridShopType";
            this.gridShopType.ReadOnly = true;
            this.gridShopType.Size = new System.Drawing.Size(380, 330);
            this.gridShopType.TabIndex = 204;
            this.gridShopType.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridShopType_CellClick);
            this.gridShopType.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridShopType_CellDoubleClick);
            this.gridShopType.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.gridShopType_RowPostPaint);
            // 
            // colShopTypeID
            // 
            this.colShopTypeID.DataPropertyName = "ShopTypeID";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colShopTypeID.DefaultCellStyle = dataGridViewCellStyle2;
            this.colShopTypeID.HeaderText = "รหัสประเภทร้านค้า";
            this.colShopTypeID.Name = "colShopTypeID";
            this.colShopTypeID.ReadOnly = true;
            this.colShopTypeID.Width = 120;
            // 
            // colShopTypeName
            // 
            this.colShopTypeName.DataPropertyName = "ShopTypeName";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colShopTypeName.DefaultCellStyle = dataGridViewCellStyle3;
            this.colShopTypeName.HeaderText = "ชื่อประเภทร้านค้า";
            this.colShopTypeName.Name = "colShopTypeName";
            this.colShopTypeName.ReadOnly = true;
            this.colShopTypeName.Width = 120;
            // 
            // colCrDate
            // 
            this.colCrDate.DataPropertyName = "CrDate";
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle4.Format = "d";
            this.colCrDate.DefaultCellStyle = dataGridViewCellStyle4;
            this.colCrDate.HeaderText = "วันที่เพิ่ม";
            this.colCrDate.Name = "colCrDate";
            this.colCrDate.ReadOnly = true;
            this.colCrDate.Width = 80;
            // 
            // colCrUser
            // 
            this.colCrUser.DataPropertyName = "CrUser";
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colCrUser.DefaultCellStyle = dataGridViewCellStyle5;
            this.colCrUser.HeaderText = "เพิ่มโดย";
            this.colCrUser.Name = "colCrUser";
            this.colCrUser.ReadOnly = true;
            this.colCrUser.Width = 70;
            // 
            // colEdDate
            // 
            this.colEdDate.DataPropertyName = "EdDate";
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle6.Format = "d";
            this.colEdDate.DefaultCellStyle = dataGridViewCellStyle6;
            this.colEdDate.HeaderText = "วันที่แก้ไข";
            this.colEdDate.Name = "colEdDate";
            this.colEdDate.ReadOnly = true;
            // 
            // colEdUser
            // 
            this.colEdUser.DataPropertyName = "EdUser";
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colEdUser.DefaultCellStyle = dataGridViewCellStyle7;
            this.colEdUser.HeaderText = "แก้ไขโดย";
            this.colEdUser.Name = "colEdUser";
            this.colEdUser.ReadOnly = true;
            // 
            // colFlagDel
            // 
            this.colFlagDel.DataPropertyName = "FlagDel";
            this.colFlagDel.HeaderText = "ยกเลิก";
            this.colFlagDel.Name = "colFlagDel";
            this.colFlagDel.ReadOnly = true;
            this.colFlagDel.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colFlagDel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colFlagDel.Width = 70;
            // 
            // pnlEdit
            // 
            this.pnlEdit.BackColor = System.Drawing.Color.SkyBlue;
            this.pnlEdit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlEdit.Controls.Add(this.groupBox1);
            this.pnlEdit.Location = new System.Drawing.Point(394, 27);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(277, 421);
            this.pnlEdit.TabIndex = 146;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightCyan;
            this.groupBox1.Controls.Add(this.btnNormal);
            this.groupBox1.Controls.Add(this.chkNormal);
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Controls.Add(this.txtShopTypeName);
            this.groupBox1.Controls.Add(this.txtShopTypeID);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.lblID);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 111);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnNormal
            // 
            this.btnNormal.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnNormal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNormal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNormal.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnNormal.Location = new System.Drawing.Point(185, 71);
            this.btnNormal.Name = "btnNormal";
            this.btnNormal.Size = new System.Drawing.Size(75, 23);
            this.btnNormal.TabIndex = 9;
            this.btnNormal.Text = "ปกติ";
            this.btnNormal.UseVisualStyleBackColor = false;
            this.btnNormal.Visible = false;
            this.btnNormal.Click += new System.EventHandler(this.btnNormal_Click);
            // 
            // chkNormal
            // 
            this.chkNormal.AutoSize = true;
            this.chkNormal.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.chkNormal.Location = new System.Drawing.Point(70, 73);
            this.chkNormal.Name = "chkNormal";
            this.chkNormal.Size = new System.Drawing.Size(51, 20);
            this.chkNormal.TabIndex = 8;
            this.chkNormal.Text = "ปกติ";
            this.chkNormal.UseVisualStyleBackColor = true;
            this.chkNormal.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lblStatus.Location = new System.Drawing.Point(6, 74);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(53, 16);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "สถานะ :";
            this.lblStatus.Visible = false;
            // 
            // txtShopTypeName
            // 
            this.txtShopTypeName.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtShopTypeName.Location = new System.Drawing.Point(70, 44);
            this.txtShopTypeName.Name = "txtShopTypeName";
            this.txtShopTypeName.Size = new System.Drawing.Size(190, 23);
            this.txtShopTypeName.TabIndex = 4;
            // 
            // txtShopTypeID
            // 
            this.txtShopTypeID.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtShopTypeID.Location = new System.Drawing.Point(70, 18);
            this.txtShopTypeID.Name = "txtShopTypeID";
            this.txtShopTypeID.Size = new System.Drawing.Size(190, 23);
            this.txtShopTypeID.TabIndex = 4;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lblName.Location = new System.Drawing.Point(22, 48);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(37, 16);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "ชื่อ  :";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lblID.Location = new System.Drawing.Point(20, 19);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(39, 16);
            this.lblID.TabIndex = 3;
            this.lblID.Text = "รหัส :";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Azure;
            this.panel8.Controls.Add(this.rdoC);
            this.panel8.Controls.Add(this.rdoN);
            this.panel8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel8.Location = new System.Drawing.Point(67, 35);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(185, 25);
            this.panel8.TabIndex = 203;
            // 
            // rdoC
            // 
            this.rdoC.AutoSize = true;
            this.rdoC.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.rdoC.Location = new System.Drawing.Point(110, 2);
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
            this.rdoN.Location = new System.Drawing.Point(10, 3);
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
            this.label26.Location = new System.Drawing.Point(8, 40);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(57, 16);
            this.label26.TabIndex = 202;
            this.label26.Text = "สถานะ : ";
            // 
            // btnSearchShopType
            // 
            this.btnSearchShopType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchShopType.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSearchShopType.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchShopType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchShopType.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnSearchShopType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSearchShopType.Image = global::AllCashUFormsApp.Properties.Resources.search;
            this.btnSearchShopType.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchShopType.Location = new System.Drawing.Point(293, 7);
            this.btnSearchShopType.Name = "btnSearchShopType";
            this.btnSearchShopType.Size = new System.Drawing.Size(70, 26);
            this.btnSearchShopType.TabIndex = 5;
            this.btnSearchShopType.Text = "ค้นหา";
            this.btnSearchShopType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchShopType.UseVisualStyleBackColor = false;
            this.btnSearchShopType.Click += new System.EventHandler(this.btnSearchShopType_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtSearch.Location = new System.Drawing.Point(67, 9);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSearch.MaxLength = 50;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(220, 23);
            this.txtSearch.TabIndex = 4;
            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.Color.SkyBlue;
            this.pnlGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlGrid.Controls.Add(this.gridShopType);
            this.pnlGrid.Controls.Add(this.panel8);
            this.pnlGrid.Controls.Add(this.label26);
            this.pnlGrid.Controls.Add(this.btnSearchShopType);
            this.pnlGrid.Controls.Add(this.label13);
            this.pnlGrid.Controls.Add(this.txtSearch);
            this.pnlGrid.Location = new System.Drawing.Point(-2, 27);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(390, 421);
            this.pnlGrid.TabIndex = 145;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label13.Location = new System.Drawing.Point(10, 12);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(51, 16);
            this.label13.TabIndex = 3;
            this.label13.Text = "ค้นหา  :";
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
            this.btnClose.Location = new System.Drawing.Point(485, 5);
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
            this.btnAdd.Location = new System.Drawing.Point(7, 5);
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
            this.panel1.Size = new System.Drawing.Size(678, 28);
            this.panel1.TabIndex = 145;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Azure;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Controls.Add(this.pnlEdit);
            this.panel7.Controls.Add(this.pnlGrid);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(678, 450);
            this.panel7.TabIndex = 146;
            // 
            // frmSearchShopType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel7);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSearchShopType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ประเภทร้านค้า";
            this.Load += new System.EventHandler(this.frmSearchShopType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridShopType)).EndInit();
            this.pnlEdit.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            this.pnlGrid.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridShopType;
        private System.Windows.Forms.Panel pnlEdit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtShopTypeName;
        private System.Windows.Forms.TextBox txtShopTypeID;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.RadioButton rdoC;
        private System.Windows.Forms.RadioButton rdoN;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button btnSearchShopType;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel5;
        private CloseButton btnClose;
        private AddButton btnAdd;
        private EditButton btnEdit;
        private PrintButton btnPrint;
        private RemoveButton btnRemove;
        private CancelButton btnCancel;
        private CopyButton btnCopy;
        private SaveButton btnSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnNormal;
        private System.Windows.Forms.CheckBox chkNormal;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShopTypeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShopTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCrDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCrUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEdDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEdUser;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colFlagDel;
    }
}