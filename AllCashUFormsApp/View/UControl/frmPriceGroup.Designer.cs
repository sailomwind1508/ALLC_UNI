namespace AllCashUFormsApp.View.UControl
{
    partial class frmPriceGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPriceGroup));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel3 = new System.Windows.Forms.Panel();
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
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.pnlGridView = new System.Windows.Forms.Panel();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.colPriceGroupID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPriceGroupCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPriceGroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.txtPriceGroupName = new System.Windows.Forms.TextBox();
            this.txtPriceGroupCode = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.pnlGridView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.panel8.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            this.pnlEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Azure;
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(807, 35);
            this.panel3.TabIndex = 33;
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
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(807, 33);
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
            this.btnClose.Location = new System.Drawing.Point(483, 4);
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
            this.btnAdd.Location = new System.Drawing.Point(5, 5);
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
            this.btnEdit.Location = new System.Drawing.Point(67, 5);
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
            this.btnPrint.Location = new System.Drawing.Point(416, 5);
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
            this.btnRemove.Location = new System.Drawing.Point(136, 5);
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
            this.btnCancel.Location = new System.Drawing.Point(342, 5);
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
            this.btnCopy.Location = new System.Drawing.Point(191, 5);
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
            this.btnSave.Location = new System.Drawing.Point(272, 5);
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
            this.panel1.Controls.Add(this.splitMain);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(807, 415);
            this.panel1.TabIndex = 34;
            // 
            // splitMain
            // 
            this.splitMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.pnlGridView);
            this.splitMain.Panel1.Controls.Add(this.pnlBottom);
            this.splitMain.Panel1.Controls.Add(this.pnlSearch);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.BackColor = System.Drawing.Color.SkyBlue;
            this.splitMain.Panel2.Controls.Add(this.pnlStatus);
            this.splitMain.Panel2.Controls.Add(this.pnlEdit);
            this.splitMain.Size = new System.Drawing.Size(807, 415);
            this.splitMain.SplitterDistance = 470;
            this.splitMain.TabIndex = 1;
            // 
            // pnlGridView
            // 
            this.pnlGridView.Controls.Add(this.grdList);
            this.pnlGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridView.Location = new System.Drawing.Point(0, 74);
            this.pnlGridView.Name = "pnlGridView";
            this.pnlGridView.Size = new System.Drawing.Size(466, 314);
            this.pnlGridView.TabIndex = 0;
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPriceGroupID,
            this.colPriceGroupCode,
            this.colPriceGroupName,
            this.colCrDate,
            this.colCrUser,
            this.colEdDate,
            this.colEdUser});
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Name = "grdList";
            this.grdList.ReadOnly = true;
            this.grdList.Size = new System.Drawing.Size(466, 314);
            this.grdList.TabIndex = 205;
            this.grdList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdList_CellClick);
            this.grdList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdList_CellDoubleClick);
            this.grdList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.grdList_RowPostPaint);
            this.grdList.SelectionChanged += new System.EventHandler(this.grdList_SelectionChanged);
            // 
            // colPriceGroupID
            // 
            this.colPriceGroupID.DataPropertyName = "PriceGroupID";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colPriceGroupID.DefaultCellStyle = dataGridViewCellStyle1;
            this.colPriceGroupID.HeaderText = "PriceGroupID";
            this.colPriceGroupID.Name = "colPriceGroupID";
            this.colPriceGroupID.ReadOnly = true;
            this.colPriceGroupID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colPriceGroupID.Visible = false;
            // 
            // colPriceGroupCode
            // 
            this.colPriceGroupCode.DataPropertyName = "PriceGroupCode";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colPriceGroupCode.DefaultCellStyle = dataGridViewCellStyle2;
            this.colPriceGroupCode.HeaderText = "รหัสกลุ่มราคา";
            this.colPriceGroupCode.MaxInputLength = 2;
            this.colPriceGroupCode.Name = "colPriceGroupCode";
            this.colPriceGroupCode.ReadOnly = true;
            this.colPriceGroupCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colPriceGroupName
            // 
            this.colPriceGroupName.DataPropertyName = "PriceGroupName";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colPriceGroupName.DefaultCellStyle = dataGridViewCellStyle3;
            this.colPriceGroupName.HeaderText = "ชื่อกลุ่มราคา";
            this.colPriceGroupName.MaxInputLength = 50;
            this.colPriceGroupName.Name = "colPriceGroupName";
            this.colPriceGroupName.ReadOnly = true;
            this.colPriceGroupName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colPriceGroupName.Width = 120;
            // 
            // colCrDate
            // 
            this.colCrDate.DataPropertyName = "CrDate";
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle4.Format = "dd/MM/yyyy";
            this.colCrDate.DefaultCellStyle = dataGridViewCellStyle4;
            this.colCrDate.HeaderText = "วันที่เพิ่ม";
            this.colCrDate.Name = "colCrDate";
            this.colCrDate.ReadOnly = true;
            this.colCrDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colCrDate.Width = 80;
            // 
            // colCrUser
            // 
            this.colCrUser.DataPropertyName = "CrUser";
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colCrUser.DefaultCellStyle = dataGridViewCellStyle5;
            this.colCrUser.HeaderText = "เพิ่มโดย";
            this.colCrUser.MaxInputLength = 50;
            this.colCrUser.Name = "colCrUser";
            this.colCrUser.ReadOnly = true;
            this.colCrUser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colCrUser.Width = 70;
            // 
            // colEdDate
            // 
            this.colEdDate.DataPropertyName = "EdDate";
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle6.Format = "dd/MM/yyyy";
            this.colEdDate.DefaultCellStyle = dataGridViewCellStyle6;
            this.colEdDate.HeaderText = "วันที่แก้ไข";
            this.colEdDate.Name = "colEdDate";
            this.colEdDate.ReadOnly = true;
            this.colEdDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colEdDate.Width = 90;
            // 
            // colEdUser
            // 
            this.colEdUser.DataPropertyName = "EdUser";
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colEdUser.DefaultCellStyle = dataGridViewCellStyle7;
            this.colEdUser.HeaderText = "แก้ไขโดย";
            this.colEdUser.MaxInputLength = 50;
            this.colEdUser.Name = "colEdUser";
            this.colEdUser.ReadOnly = true;
            this.colEdUser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colEdUser.Width = 70;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.SkyBlue;
            this.pnlBottom.Controls.Add(this.lbl_Qty);
            this.pnlBottom.Controls.Add(this.lbl_List);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 388);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(466, 23);
            this.pnlBottom.TabIndex = 2;
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
            this.pnlSearch.Size = new System.Drawing.Size(466, 74);
            this.pnlSearch.TabIndex = 1;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Azure;
            this.panel8.Controls.Add(this.rdoC);
            this.panel8.Controls.Add(this.rdoN);
            this.panel8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel8.Location = new System.Drawing.Point(65, 42);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(130, 25);
            this.panel8.TabIndex = 208;
            // 
            // rdoC
            // 
            this.rdoC.AutoSize = true;
            this.rdoC.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.rdoC.Location = new System.Drawing.Point(63, 3);
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
            this.btnSearch.Location = new System.Drawing.Point(393, 16);
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
            this.txtSearch.Size = new System.Drawing.Size(322, 23);
            this.txtSearch.TabIndex = 205;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // pnlStatus
            // 
            this.pnlStatus.BackColor = System.Drawing.Color.Azure;
            this.pnlStatus.Controls.Add(this.btnNormal);
            this.pnlStatus.Controls.Add(this.lblStatus);
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStatus.Location = new System.Drawing.Point(0, 57);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(329, 28);
            this.pnlStatus.TabIndex = 15;
            // 
            // btnNormal
            // 
            this.btnNormal.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnNormal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNormal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNormal.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnNormal.Location = new System.Drawing.Point(93, 2);
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
            this.lblStatus.Location = new System.Drawing.Point(29, 5);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(57, 16);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "สถานะ  :";
            // 
            // pnlEdit
            // 
            this.pnlEdit.BackColor = System.Drawing.Color.Azure;
            this.pnlEdit.Controls.Add(this.txtPriceGroupName);
            this.pnlEdit.Controls.Add(this.txtPriceGroupCode);
            this.pnlEdit.Controls.Add(this.lblName);
            this.pnlEdit.Controls.Add(this.lblID);
            this.pnlEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEdit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.pnlEdit.Location = new System.Drawing.Point(0, 0);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(329, 57);
            this.pnlEdit.TabIndex = 14;
            // 
            // txtPriceGroupName
            // 
            this.txtPriceGroupName.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtPriceGroupName.Location = new System.Drawing.Point(93, 31);
            this.txtPriceGroupName.MaxLength = 50;
            this.txtPriceGroupName.Name = "txtPriceGroupName";
            this.txtPriceGroupName.Size = new System.Drawing.Size(176, 23);
            this.txtPriceGroupName.TabIndex = 7;
            // 
            // txtPriceGroupCode
            // 
            this.txtPriceGroupCode.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtPriceGroupCode.Location = new System.Drawing.Point(93, 5);
            this.txtPriceGroupCode.MaxLength = 2;
            this.txtPriceGroupCode.Name = "txtPriceGroupCode";
            this.txtPriceGroupCode.Size = new System.Drawing.Size(176, 23);
            this.txtPriceGroupCode.TabIndex = 8;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lblName.Location = new System.Drawing.Point(50, 33);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(37, 16);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "ชื่อ  :";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lblID.Location = new System.Drawing.Point(48, 8);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(39, 16);
            this.lblID.TabIndex = 6;
            this.lblID.Text = "รหัส :";
            // 
            // frmPriceGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPriceGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "กลุ่มราคา";
            this.Load += new System.EventHandler(this.frmPriceGroup_Load);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.pnlGridView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
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
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.Panel pnlGridView;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lbl_Qty;
        private System.Windows.Forms.Label lbl_List;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.RadioButton rdoC;
        private System.Windows.Forms.RadioButton rdoN;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Button btnNormal;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlEdit;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox txtPriceGroupName;
        private System.Windows.Forms.TextBox txtPriceGroupCode;
        private System.Windows.Forms.DataGridView grdList;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPriceGroupID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPriceGroupCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPriceGroupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCrDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCrUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEdDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEdUser;
    }
}