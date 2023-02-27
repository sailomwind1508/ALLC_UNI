
namespace AllCashUFormsApp.View.Page
{
    partial class frmSQLPrompt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSQLPrompt));
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.txtSqlCmd = new System.Windows.Forms.TextBox();
            this.lblSQLCmd = new System.Windows.Forms.Label();
            this.btnRecoveryPO = new System.Windows.Forms.Button();
            this.pnlCen = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblRowCount = new System.Windows.Forms.Label();
            this.tabSQLResult = new System.Windows.Forms.TabControl();
            this.tabResult = new System.Windows.Forms.TabPage();
            this.grdSQLResult = new System.Windows.Forms.DataGridView();
            this.tabMessages = new System.Windows.Forms.TabPage();
            this.txtSQLMessage = new System.Windows.Forms.TextBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnSendToCenter = new System.Windows.Forms.Button();
            this.btnUnlockEndDAy = new System.Windows.Forms.Button();
            this.btnRepairVE = new System.Windows.Forms.Button();
            this.txtBranchCode = new System.Windows.Forms.TextBox();
            this.btnCheckDNS = new System.Windows.Forms.Button();
            this.chkAllBranch = new System.Windows.Forms.CheckBox();
            this.lblDepo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.FormPic = new System.Windows.Forms.PictureBox();
            this.FormHeader = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlCen.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tabSQLResult.SuspendLayout();
            this.tabResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSQLResult)).BeginInit();
            this.tabMessages.SuspendLayout();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormPic)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Azure;
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1358, 28);
            this.panel1.TabIndex = 30;
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
            // txtSqlCmd
            // 
            this.txtSqlCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSqlCmd.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtSqlCmd.Location = new System.Drawing.Point(3, 46);
            this.txtSqlCmd.MaxLength = 2147483647;
            this.txtSqlCmd.Multiline = true;
            this.txtSqlCmd.Name = "txtSqlCmd";
            this.txtSqlCmd.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSqlCmd.Size = new System.Drawing.Size(1336, 209);
            this.txtSqlCmd.TabIndex = 123;
            // 
            // lblSQLCmd
            // 
            this.lblSQLCmd.AutoSize = true;
            this.lblSQLCmd.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSQLCmd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSQLCmd.Location = new System.Drawing.Point(37, 15);
            this.lblSQLCmd.Name = "lblSQLCmd";
            this.lblSQLCmd.Size = new System.Drawing.Size(67, 16);
            this.lblSQLCmd.TabIndex = 121;
            this.lblSQLCmd.Text = "คำสั่ง SQL";
            // 
            // btnRecoveryPO
            // 
            this.btnRecoveryPO.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnRecoveryPO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRecoveryPO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecoveryPO.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnRecoveryPO.ForeColor = System.Drawing.Color.Black;
            this.btnRecoveryPO.Image = ((System.Drawing.Image)(resources.GetObject("btnRecoveryPO.Image")));
            this.btnRecoveryPO.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecoveryPO.Location = new System.Drawing.Point(810, 2);
            this.btnRecoveryPO.Name = "btnRecoveryPO";
            this.btnRecoveryPO.Size = new System.Drawing.Size(125, 40);
            this.btnRecoveryPO.TabIndex = 119;
            this.btnRecoveryPO.Text = "Recovery PO";
            this.btnRecoveryPO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRecoveryPO.UseVisualStyleBackColor = false;
            this.btnRecoveryPO.Click += new System.EventHandler(this.btnRecoveryPO_Click);
            // 
            // pnlCen
            // 
            this.pnlCen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCen.BackColor = System.Drawing.Color.Azure;
            this.pnlCen.Controls.Add(this.panel7);
            this.pnlCen.Controls.Add(this.tabSQLResult);
            this.pnlCen.Location = new System.Drawing.Point(0, 261);
            this.pnlCen.Name = "pnlCen";
            this.pnlCen.Size = new System.Drawing.Size(1342, 449);
            this.pnlCen.TabIndex = 115;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Khaki;
            this.panel7.Controls.Add(this.pictureBox2);
            this.panel7.Controls.Add(this.lblRowCount);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel7.Location = new System.Drawing.Point(0, 423);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1342, 26);
            this.panel7.TabIndex = 168;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 21);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // lblRowCount
            // 
            this.lblRowCount.AutoSize = true;
            this.lblRowCount.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lblRowCount.Location = new System.Drawing.Point(36, 5);
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.Size = new System.Drawing.Size(12, 16);
            this.lblRowCount.TabIndex = 5;
            this.lblRowCount.Text = ".";
            // 
            // tabSQLResult
            // 
            this.tabSQLResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabSQLResult.Controls.Add(this.tabResult);
            this.tabSQLResult.Controls.Add(this.tabMessages);
            this.tabSQLResult.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.tabSQLResult.Location = new System.Drawing.Point(3, 7);
            this.tabSQLResult.Name = "tabSQLResult";
            this.tabSQLResult.SelectedIndex = 0;
            this.tabSQLResult.Size = new System.Drawing.Size(1336, 418);
            this.tabSQLResult.TabIndex = 19;
            // 
            // tabResult
            // 
            this.tabResult.Controls.Add(this.grdSQLResult);
            this.tabResult.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.tabResult.Location = new System.Drawing.Point(4, 25);
            this.tabResult.Name = "tabResult";
            this.tabResult.Padding = new System.Windows.Forms.Padding(3);
            this.tabResult.Size = new System.Drawing.Size(1328, 389);
            this.tabResult.TabIndex = 0;
            this.tabResult.Text = "Results";
            this.tabResult.UseVisualStyleBackColor = true;
            // 
            // grdSQLResult
            // 
            this.grdSQLResult.AllowUserToAddRows = false;
            this.grdSQLResult.AllowUserToDeleteRows = false;
            this.grdSQLResult.AllowUserToResizeRows = false;
            this.grdSQLResult.BackgroundColor = System.Drawing.SystemColors.Window;
            this.grdSQLResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSQLResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSQLResult.Location = new System.Drawing.Point(3, 3);
            this.grdSQLResult.Name = "grdSQLResult";
            this.grdSQLResult.ReadOnly = true;
            this.grdSQLResult.Size = new System.Drawing.Size(1322, 383);
            this.grdSQLResult.TabIndex = 18;
            this.grdSQLResult.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.grdSQLResult_RowPostPaint);
            // 
            // tabMessages
            // 
            this.tabMessages.Controls.Add(this.txtSQLMessage);
            this.tabMessages.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.tabMessages.Location = new System.Drawing.Point(4, 25);
            this.tabMessages.Name = "tabMessages";
            this.tabMessages.Padding = new System.Windows.Forms.Padding(3);
            this.tabMessages.Size = new System.Drawing.Size(1328, 389);
            this.tabMessages.TabIndex = 1;
            this.tabMessages.Text = "Messages";
            this.tabMessages.UseVisualStyleBackColor = true;
            // 
            // txtSQLMessage
            // 
            this.txtSQLMessage.BackColor = System.Drawing.SystemColors.Window;
            this.txtSQLMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQLMessage.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtSQLMessage.Location = new System.Drawing.Point(3, 3);
            this.txtSQLMessage.MaxLength = 255;
            this.txtSQLMessage.Multiline = true;
            this.txtSQLMessage.Name = "txtSQLMessage";
            this.txtSQLMessage.ReadOnly = true;
            this.txtSQLMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSQLMessage.Size = new System.Drawing.Size(1322, 383);
            this.txtSQLMessage.TabIndex = 17;
            // 
            // btnExecute
            // 
            this.btnExecute.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnExecute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExecute.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExecute.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnExecute.ForeColor = System.Drawing.Color.Black;
            this.btnExecute.Image = ((System.Drawing.Image)(resources.GetObject("btnExecute.Image")));
            this.btnExecute.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExecute.Location = new System.Drawing.Point(546, 2);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(97, 40);
            this.btnExecute.TabIndex = 118;
            this.btnExecute.Text = "Run SQL";
            this.btnExecute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExecute.UseVisualStyleBackColor = false;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Azure;
            this.pnlTop.Controls.Add(this.btnSendToCenter);
            this.pnlTop.Controls.Add(this.btnUnlockEndDAy);
            this.pnlTop.Controls.Add(this.btnRepairVE);
            this.pnlTop.Controls.Add(this.txtBranchCode);
            this.pnlTop.Controls.Add(this.lblSQLCmd);
            this.pnlTop.Controls.Add(this.btnRecoveryPO);
            this.pnlTop.Controls.Add(this.btnCheckDNS);
            this.pnlTop.Controls.Add(this.btnExecute);
            this.pnlTop.Controls.Add(this.chkAllBranch);
            this.pnlTop.Controls.Add(this.lblDepo);
            this.pnlTop.Controls.Add(this.pictureBox1);
            this.pnlTop.Controls.Add(this.txtSqlCmd);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1342, 259);
            this.pnlTop.TabIndex = 117;
            // 
            // btnSendToCenter
            // 
            this.btnSendToCenter.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSendToCenter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSendToCenter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendToCenter.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnSendToCenter.ForeColor = System.Drawing.Color.Black;
            this.btnSendToCenter.Image = ((System.Drawing.Image)(resources.GetObject("btnSendToCenter.Image")));
            this.btnSendToCenter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendToCenter.Location = new System.Drawing.Point(1069, 2);
            this.btnSendToCenter.Name = "btnSendToCenter";
            this.btnSendToCenter.Size = new System.Drawing.Size(137, 40);
            this.btnSendToCenter.TabIndex = 130;
            this.btnSendToCenter.Text = "Send To Center";
            this.btnSendToCenter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSendToCenter.UseVisualStyleBackColor = false;
            this.btnSendToCenter.Click += new System.EventHandler(this.btnSendToCenter_Click);
            // 
            // btnUnlockEndDAy
            // 
            this.btnUnlockEndDAy.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnUnlockEndDAy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUnlockEndDAy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnlockEndDAy.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnUnlockEndDAy.ForeColor = System.Drawing.Color.Black;
            this.btnUnlockEndDAy.Image = ((System.Drawing.Image)(resources.GetObject("btnUnlockEndDAy.Image")));
            this.btnUnlockEndDAy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUnlockEndDAy.Location = new System.Drawing.Point(936, 2);
            this.btnUnlockEndDAy.Name = "btnUnlockEndDAy";
            this.btnUnlockEndDAy.Size = new System.Drawing.Size(131, 40);
            this.btnUnlockEndDAy.TabIndex = 129;
            this.btnUnlockEndDAy.Text = "Unlock EndDay";
            this.btnUnlockEndDAy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUnlockEndDAy.UseVisualStyleBackColor = false;
            this.btnUnlockEndDAy.Click += new System.EventHandler(this.btnUnlockEndDAy_Click);
            // 
            // btnRepairVE
            // 
            this.btnRepairVE.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnRepairVE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRepairVE.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepairVE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepairVE.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnRepairVE.ForeColor = System.Drawing.Color.Black;
            this.btnRepairVE.Image = ((System.Drawing.Image)(resources.GetObject("btnRepairVE.Image")));
            this.btnRepairVE.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepairVE.Location = new System.Drawing.Point(645, 2);
            this.btnRepairVE.Name = "btnRepairVE";
            this.btnRepairVE.Size = new System.Drawing.Size(163, 40);
            this.btnRepairVE.TabIndex = 128;
            this.btnRepairVE.Text = "ปรับปรุง ใบกำกับภาษี";
            this.btnRepairVE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRepairVE.UseVisualStyleBackColor = false;
            this.btnRepairVE.Click += new System.EventHandler(this.btnRepairVE_Click);
            // 
            // txtBranchCode
            // 
            this.txtBranchCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtBranchCode.Location = new System.Drawing.Point(155, 12);
            this.txtBranchCode.MaxLength = 5000;
            this.txtBranchCode.Name = "txtBranchCode";
            this.txtBranchCode.ReadOnly = true;
            this.txtBranchCode.Size = new System.Drawing.Size(290, 23);
            this.txtBranchCode.TabIndex = 127;
            this.txtBranchCode.Click += new System.EventHandler(this.txtBranchCode_Click);
            // 
            // btnCheckDNS
            // 
            this.btnCheckDNS.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnCheckDNS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCheckDNS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckDNS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckDNS.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnCheckDNS.ForeColor = System.Drawing.Color.Black;
            this.btnCheckDNS.Image = ((System.Drawing.Image)(resources.GetObject("btnCheckDNS.Image")));
            this.btnCheckDNS.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCheckDNS.Location = new System.Drawing.Point(1208, 2);
            this.btnCheckDNS.Name = "btnCheckDNS";
            this.btnCheckDNS.Size = new System.Drawing.Size(130, 40);
            this.btnCheckDNS.TabIndex = 126;
            this.btnCheckDNS.Text = "ตรวจสอบ DNS";
            this.btnCheckDNS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCheckDNS.UseVisualStyleBackColor = false;
            this.btnCheckDNS.Click += new System.EventHandler(this.btnCheckDNS_Click);
            // 
            // chkAllBranch
            // 
            this.chkAllBranch.AutoSize = true;
            this.chkAllBranch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkAllBranch.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.chkAllBranch.Location = new System.Drawing.Point(451, 14);
            this.chkAllBranch.Name = "chkAllBranch";
            this.chkAllBranch.Size = new System.Drawing.Size(96, 20);
            this.chkAllBranch.TabIndex = 125;
            this.chkAllBranch.Text = "เลือกทุกศูนย์";
            this.chkAllBranch.UseVisualStyleBackColor = true;
            this.chkAllBranch.CheckedChanged += new System.EventHandler(this.chkAllBranch_CheckedChanged);
            // 
            // lblDepo
            // 
            this.lblDepo.AutoSize = true;
            this.lblDepo.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblDepo.Location = new System.Drawing.Point(104, 15);
            this.lblDepo.Name = "lblDepo";
            this.lblDepo.Size = new System.Drawing.Size(45, 16);
            this.lblDepo.TabIndex = 121;
            this.lblDepo.Text = "เดโป้ :";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(3, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(41, 32);
            this.pictureBox1.TabIndex = 124;
            this.pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.Azure;
            this.panel3.Controls.Add(this.pnlTop);
            this.panel3.Controls.Add(this.pnlCen);
            this.panel3.Location = new System.Drawing.Point(7, 41);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1342, 710);
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
            this.FormHeader.Location = new System.Drawing.Point(48, 11);
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
            this.panel4.Size = new System.Drawing.Size(1358, 35);
            this.panel4.TabIndex = 18;
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
            this.panel2.Size = new System.Drawing.Size(1358, 760);
            this.panel2.TabIndex = 31;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icons8-table-96.png");
            this.imageList1.Images.SetKeyName(1, "icons8-topic-100.png");
            // 
            // frmSQLPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1358, 786);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "frmSQLPrompt";
            this.Text = "frmSQLPrompt";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSQLPrompt_Load);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.pnlCen.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tabSQLResult.ResumeLayout(false);
            this.tabResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSQLResult)).EndInit();
            this.tabMessages.ResumeLayout(false);
            this.tabMessages.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FormPic)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private UControl.CloseButton btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private UControl.AddButton btnAdd;
        private UControl.ExcelButton btnExcel;
        private UControl.EditButton btnEdit;
        private UControl.PrintButton btnPrint;
        private UControl.RemoveButton btnRemove;
        private UControl.CancelButton btnCancel;
        private UControl.CopyButton btnCopy;
        private UControl.SaveButton btnSave;
        private System.Windows.Forms.TextBox txtSqlCmd;
        private System.Windows.Forms.Label lblSQLCmd;
        private System.Windows.Forms.Button btnRecoveryPO;
        private System.Windows.Forms.Panel pnlCen;
        private System.Windows.Forms.TextBox txtSQLMessage;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox FormPic;
        private System.Windows.Forms.Label FormHeader;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView grdSQLResult;
        private System.Windows.Forms.TabControl tabSQLResult;
        private System.Windows.Forms.TabPage tabResult;
        private System.Windows.Forms.TabPage tabMessages;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lblRowCount;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblDepo;
        private System.Windows.Forms.CheckBox chkAllBranch;
        private System.Windows.Forms.Button btnCheckDNS;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox txtBranchCode;
        private System.Windows.Forms.Button btnRepairVE;
        private System.Windows.Forms.Button btnUnlockEndDAy;
        private System.Windows.Forms.Button btnSendToCenter;
    }
}