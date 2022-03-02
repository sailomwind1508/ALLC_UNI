namespace AllCashUFormsApp.View.Page
{
    partial class frmOD
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOD));
            this.panel1 = new System.Windows.Forms.Panel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.FormPic = new System.Windows.Forms.PictureBox();
            this.FormHeader = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlBot = new System.Windows.Forms.Panel();
            this.pnlBL = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.rdoAccCode = new System.Windows.Forms.RadioButton();
            this.rdoProdCode = new System.Windows.Forms.RadioButton();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlBR = new System.Windows.Forms.Panel();
            this.txnTotalDue = new System.Windows.Forms.TextBox();
            this.txnAmount = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txnExcVat = new System.Windows.Forms.TextBox();
            this.txnVatAmt = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txnIncVat = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlTR = new System.Windows.Forms.Panel();
            this.txdDocNo = new System.Windows.Forms.MaskedTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ddlDocStatus = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpDueDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.nudCreditDay = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpDocDate = new System.Windows.Forms.DateTimePicker();
            this.btnSearchDoc = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlTL = new System.Windows.Forms.Panel();
            this.txtCrUser = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTelephone = new System.Windows.Forms.TextBox();
            this.lblTelephone = new System.Windows.Forms.Label();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.lblContact = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtSuppName = new System.Windows.Forms.TextBox();
            this.txtSupplierCode = new System.Windows.Forms.TextBox();
            this.btnSearchSupp = new System.Windows.Forms.Button();
            this.lblSupplierCode = new System.Windows.Forms.Label();
            this.pnlCen = new System.Windows.Forms.Panel();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.colProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSearchProduct = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.pnlBot.SuspendLayout();
            this.pnlBL.SuspendLayout();
            this.panel6.SuspendLayout();
            this.pnlBR.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlTR.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCreditDay)).BeginInit();
            this.pnlTL.SuspendLayout();
            this.pnlCen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Azure;
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 28);
            this.panel1.TabIndex = 22;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Azure;
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(0, 30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1008, 512);
            this.panel2.TabIndex = 23;
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
            this.panel3.Controls.Add(this.pnlBot);
            this.panel3.Controls.Add(this.pnlTop);
            this.panel3.Controls.Add(this.pnlCen);
            this.panel3.Location = new System.Drawing.Point(7, 41);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(992, 462);
            this.panel3.TabIndex = 17;
            // 
            // pnlBot
            // 
            this.pnlBot.BackColor = System.Drawing.Color.Azure;
            this.pnlBot.Controls.Add(this.pnlBL);
            this.pnlBot.Controls.Add(this.pnlBR);
            this.pnlBot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBot.Location = new System.Drawing.Point(0, 323);
            this.pnlBot.Name = "pnlBot";
            this.pnlBot.Size = new System.Drawing.Size(992, 139);
            this.pnlBot.TabIndex = 118;
            // 
            // pnlBL
            // 
            this.pnlBL.BackColor = System.Drawing.Color.Azure;
            this.pnlBL.Controls.Add(this.panel6);
            this.pnlBL.Controls.Add(this.txtComment);
            this.pnlBL.Controls.Add(this.label12);
            this.pnlBL.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlBL.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBL.Location = new System.Drawing.Point(0, 0);
            this.pnlBL.Name = "pnlBL";
            this.pnlBL.Size = new System.Drawing.Size(561, 139);
            this.pnlBL.TabIndex = 116;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.LightCyan;
            this.panel6.Controls.Add(this.rdoAccCode);
            this.panel6.Controls.Add(this.rdoProdCode);
            this.panel6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel6.Location = new System.Drawing.Point(125, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(182, 25);
            this.panel6.TabIndex = 114;
            // 
            // rdoAccCode
            // 
            this.rdoAccCode.AutoSize = true;
            this.rdoAccCode.Location = new System.Drawing.Point(93, 2);
            this.rdoAccCode.Name = "rdoAccCode";
            this.rdoAccCode.Size = new System.Drawing.Size(75, 20);
            this.rdoAccCode.TabIndex = 16;
            this.rdoAccCode.TabStop = true;
            this.rdoAccCode.Text = "รหัสบัญชี";
            this.rdoAccCode.UseVisualStyleBackColor = true;
            // 
            // rdoProdCode
            // 
            this.rdoProdCode.AutoSize = true;
            this.rdoProdCode.BackColor = System.Drawing.Color.LightCyan;
            this.rdoProdCode.Location = new System.Drawing.Point(9, 2);
            this.rdoProdCode.Name = "rdoProdCode";
            this.rdoProdCode.Size = new System.Drawing.Size(78, 20);
            this.rdoProdCode.TabIndex = 15;
            this.rdoProdCode.TabStop = true;
            this.rdoProdCode.Text = "รหัสสินค้า";
            this.rdoProdCode.UseVisualStyleBackColor = false;
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(6, 34);
            this.txtComment.MaxLength = 255;
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComment.Size = new System.Drawing.Size(399, 96);
            this.txtComment.TabIndex = 17;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 7);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(112, 16);
            this.label12.TabIndex = 110;
            this.label12.Text = "หมายเหตุเพิ่มเติม : ";
            // 
            // pnlBR
            // 
            this.pnlBR.BackColor = System.Drawing.Color.Azure;
            this.pnlBR.Controls.Add(this.txnTotalDue);
            this.pnlBR.Controls.Add(this.txnAmount);
            this.pnlBR.Controls.Add(this.label13);
            this.pnlBR.Controls.Add(this.label17);
            this.pnlBR.Controls.Add(this.label14);
            this.pnlBR.Controls.Add(this.txnExcVat);
            this.pnlBR.Controls.Add(this.txnVatAmt);
            this.pnlBR.Controls.Add(this.label15);
            this.pnlBR.Controls.Add(this.txnIncVat);
            this.pnlBR.Controls.Add(this.label16);
            this.pnlBR.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlBR.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBR.Location = new System.Drawing.Point(567, 0);
            this.pnlBR.Name = "pnlBR";
            this.pnlBR.Size = new System.Drawing.Size(425, 139);
            this.pnlBR.TabIndex = 22;
            // 
            // txnTotalDue
            // 
            this.txnTotalDue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txnTotalDue.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txnTotalDue.Location = new System.Drawing.Point(222, 110);
            this.txnTotalDue.MaxLength = 5;
            this.txnTotalDue.Name = "txnTotalDue";
            this.txnTotalDue.ReadOnly = true;
            this.txnTotalDue.Size = new System.Drawing.Size(163, 23);
            this.txnTotalDue.TabIndex = 22;
            // 
            // txnAmount
            // 
            this.txnAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txnAmount.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txnAmount.Location = new System.Drawing.Point(222, 5);
            this.txnAmount.MaxLength = 5;
            this.txnAmount.Name = "txnAmount";
            this.txnAmount.ReadOnly = true;
            this.txnAmount.Size = new System.Drawing.Size(163, 23);
            this.txnAmount.TabIndex = 18;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(137, 8);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 16);
            this.label13.TabIndex = 112;
            this.label13.Text = "รวมเป็นเงิน : ";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(91, 113);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(125, 16);
            this.label17.TabIndex = 120;
            this.label17.Text = "จำนวนเงินรวมทั้งสิ้น : ";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(77, 34);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(139, 16);
            this.label14.TabIndex = 114;
            this.label14.Text = "มูลค่าสินค้ายกเว้นภาษี : ";
            // 
            // txnExcVat
            // 
            this.txnExcVat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txnExcVat.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txnExcVat.Location = new System.Drawing.Point(222, 31);
            this.txnExcVat.MaxLength = 5;
            this.txnExcVat.Name = "txnExcVat";
            this.txnExcVat.ReadOnly = true;
            this.txnExcVat.Size = new System.Drawing.Size(163, 23);
            this.txnExcVat.TabIndex = 19;
            // 
            // txnVatAmt
            // 
            this.txnVatAmt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txnVatAmt.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txnVatAmt.Location = new System.Drawing.Point(222, 84);
            this.txnVatAmt.MaxLength = 5;
            this.txnVatAmt.Name = "txnVatAmt";
            this.txnVatAmt.ReadOnly = true;
            this.txnVatAmt.Size = new System.Drawing.Size(163, 23);
            this.txnVatAmt.TabIndex = 21;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(111, 61);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(105, 16);
            this.label15.TabIndex = 116;
            this.label15.Text = "มูลค่าสินค้าปกติ : ";
            // 
            // txnIncVat
            // 
            this.txnIncVat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txnIncVat.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txnIncVat.Location = new System.Drawing.Point(222, 58);
            this.txnIncVat.MaxLength = 5;
            this.txnIncVat.Name = "txnIncVat";
            this.txnIncVat.ReadOnly = true;
            this.txnIncVat.Size = new System.Drawing.Size(163, 23);
            this.txnIncVat.TabIndex = 20;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(120, 87);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(96, 16);
            this.label16.TabIndex = 118;
            this.label16.Text = "ภาษีมูลค่าเพิ่ม : ";
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Azure;
            this.pnlTop.Controls.Add(this.pnlTR);
            this.pnlTop.Controls.Add(this.pnlTL);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(992, 136);
            this.pnlTop.TabIndex = 117;
            // 
            // pnlTR
            // 
            this.pnlTR.BackColor = System.Drawing.Color.Azure;
            this.pnlTR.Controls.Add(this.txdDocNo);
            this.pnlTR.Controls.Add(this.label11);
            this.pnlTR.Controls.Add(this.ddlDocStatus);
            this.pnlTR.Controls.Add(this.label10);
            this.pnlTR.Controls.Add(this.dtpDueDate);
            this.pnlTR.Controls.Add(this.label9);
            this.pnlTR.Controls.Add(this.nudCreditDay);
            this.pnlTR.Controls.Add(this.label8);
            this.pnlTR.Controls.Add(this.dtpDocDate);
            this.pnlTR.Controls.Add(this.btnSearchDoc);
            this.pnlTR.Controls.Add(this.label7);
            this.pnlTR.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlTR.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTR.Location = new System.Drawing.Point(567, 0);
            this.pnlTR.Name = "pnlTR";
            this.pnlTR.Size = new System.Drawing.Size(425, 136);
            this.pnlTR.TabIndex = 21;
            // 
            // txdDocNo
            // 
            this.txdDocNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txdDocNo.BackColor = System.Drawing.Color.Turquoise;
            this.txdDocNo.Location = new System.Drawing.Point(222, 2);
            this.txdDocNo.Name = "txdDocNo";
            this.txdDocNo.Size = new System.Drawing.Size(163, 23);
            this.txdDocNo.TabIndex = 8;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(119, 109);
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
            this.ddlDocStatus.Location = new System.Drawing.Point(222, 106);
            this.ddlDocStatus.Name = "ddlDocStatus";
            this.ddlDocStatus.Size = new System.Drawing.Size(163, 24);
            this.ddlDocStatus.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(135, 85);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 16);
            this.label10.TabIndex = 108;
            this.label10.Text = "ครบกำหนด : ";
            // 
            // dtpDueDate
            // 
            this.dtpDueDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpDueDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDueDate.Location = new System.Drawing.Point(222, 80);
            this.dtpDueDate.Name = "dtpDueDate";
            this.dtpDueDate.Size = new System.Drawing.Size(163, 23);
            this.dtpDueDate.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(133, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 16);
            this.label9.TabIndex = 106;
            this.label9.Text = "เครดิต (วัน) : ";
            // 
            // nudCreditDay
            // 
            this.nudCreditDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudCreditDay.Location = new System.Drawing.Point(222, 54);
            this.nudCreditDay.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudCreditDay.Name = "nudCreditDay";
            this.nudCreditDay.Size = new System.Drawing.Size(78, 23);
            this.nudCreditDay.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(141, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 16);
            this.label8.TabIndex = 104;
            this.label8.Text = "วันที่สั่งซื้อ : ";
            // 
            // dtpDocDate
            // 
            this.dtpDocDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpDocDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDocDate.Location = new System.Drawing.Point(222, 28);
            this.dtpDocDate.Name = "dtpDocDate";
            this.dtpDocDate.Size = new System.Drawing.Size(163, 23);
            this.dtpDocDate.TabIndex = 10;
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
            this.btnSearchDoc.TabIndex = 9;
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
            this.pnlTL.Controls.Add(this.label6);
            this.pnlTL.Controls.Add(this.txtRemark);
            this.pnlTL.Controls.Add(this.label5);
            this.pnlTL.Controls.Add(this.txtTelephone);
            this.pnlTL.Controls.Add(this.lblTelephone);
            this.pnlTL.Controls.Add(this.txtContact);
            this.pnlTL.Controls.Add(this.lblContact);
            this.pnlTL.Controls.Add(this.txtAddress);
            this.pnlTL.Controls.Add(this.lblAddress);
            this.pnlTL.Controls.Add(this.txtSuppName);
            this.pnlTL.Controls.Add(this.txtSupplierCode);
            this.pnlTL.Controls.Add(this.btnSearchSupp);
            this.pnlTL.Controls.Add(this.lblSupplierCode);
            this.pnlTL.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTL.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTL.Location = new System.Drawing.Point(0, 0);
            this.pnlTL.Name = "pnlTL";
            this.pnlTL.Size = new System.Drawing.Size(561, 136);
            this.pnlTL.TabIndex = 20;
            // 
            // txtCrUser
            // 
            this.txtCrUser.Location = new System.Drawing.Point(142, 108);
            this.txtCrUser.MaxLength = 50;
            this.txtCrUser.Name = "txtCrUser";
            this.txtCrUser.Size = new System.Drawing.Size(410, 23);
            this.txtCrUser.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(77, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 16);
            this.label6.TabIndex = 109;
            this.label6.Text = "ผู้จำทำ : ";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(142, 82);
            this.txtRemark.MaxLength = 255;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(410, 23);
            this.txtRemark.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(63, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 16);
            this.label5.TabIndex = 107;
            this.label5.Text = "หมายเหตุ : ";
            // 
            // txtTelephone
            // 
            this.txtTelephone.Location = new System.Drawing.Point(389, 56);
            this.txtTelephone.MaxLength = 15;
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(163, 23);
            this.txtTelephone.TabIndex = 5;
            // 
            // lblTelephone
            // 
            this.lblTelephone.AutoSize = true;
            this.lblTelephone.Location = new System.Drawing.Point(315, 59);
            this.lblTelephone.Name = "lblTelephone";
            this.lblTelephone.Size = new System.Drawing.Size(68, 16);
            this.lblTelephone.TabIndex = 105;
            this.lblTelephone.Text = "เบอร์โทร : ";
            // 
            // txtContact
            // 
            this.txtContact.Location = new System.Drawing.Point(142, 56);
            this.txtContact.MaxLength = 10;
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(167, 23);
            this.txtContact.TabIndex = 4;
            // 
            // lblContact
            // 
            this.lblContact.AutoSize = true;
            this.lblContact.Location = new System.Drawing.Point(46, 59);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(88, 16);
            this.lblContact.TabIndex = 103;
            this.lblContact.Text = "พนักงานขาย : ";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(142, 30);
            this.txtAddress.MaxLength = 50;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ReadOnly = true;
            this.txtAddress.Size = new System.Drawing.Size(410, 23);
            this.txtAddress.TabIndex = 3;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(38, 33);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(96, 16);
            this.lblAddress.TabIndex = 101;
            this.lblAddress.Text = "สถานที่ส่งของ : ";
            // 
            // txtSuppName
            // 
            this.txtSuppName.Location = new System.Drawing.Point(308, 4);
            this.txtSuppName.MaxLength = 200;
            this.txtSuppName.Name = "txtSuppName";
            this.txtSuppName.ReadOnly = true;
            this.txtSuppName.Size = new System.Drawing.Size(244, 23);
            this.txtSuppName.TabIndex = 2;
            // 
            // txtSupplierCode
            // 
            this.txtSupplierCode.Location = new System.Drawing.Point(142, 4);
            this.txtSupplierCode.MaxLength = 5;
            this.txtSupplierCode.Name = "txtSupplierCode";
            this.txtSupplierCode.Size = new System.Drawing.Size(123, 23);
            this.txtSupplierCode.TabIndex = 0;
            // 
            // btnSearchSupp
            // 
            this.btnSearchSupp.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSearchSupp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchSupp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchSupp.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSearchSupp.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchSupp.Image")));
            this.btnSearchSupp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchSupp.Location = new System.Drawing.Point(267, 4);
            this.btnSearchSupp.Name = "btnSearchSupp";
            this.btnSearchSupp.Size = new System.Drawing.Size(35, 23);
            this.btnSearchSupp.TabIndex = 1;
            this.btnSearchSupp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchSupp.UseVisualStyleBackColor = false;
            this.btnSearchSupp.Click += new System.EventHandler(this.btnSearchSupp_Click);
            // 
            // lblSupplierCode
            // 
            this.lblSupplierCode.AutoSize = true;
            this.lblSupplierCode.Location = new System.Drawing.Point(62, 7);
            this.lblSupplierCode.Name = "lblSupplierCode";
            this.lblSupplierCode.Size = new System.Drawing.Size(72, 16);
            this.lblSupplierCode.TabIndex = 99;
            this.lblSupplierCode.Text = "ผู้จำหน่าย : ";
            // 
            // pnlCen
            // 
            this.pnlCen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCen.BackColor = System.Drawing.Color.Azure;
            this.pnlCen.Controls.Add(this.grdList);
            this.pnlCen.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.pnlCen.Location = new System.Drawing.Point(0, 137);
            this.pnlCen.Name = "pnlCen";
            this.pnlCen.Size = new System.Drawing.Size(992, 180);
            this.pnlCen.TabIndex = 115;
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToResizeColumns = false;
            this.grdList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.grdList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProductCode,
            this.colSearchProduct,
            this.colProductName,
            this.colUnit,
            this.colVAT,
            this.colAmount,
            this.colUnitPrice,
            this.colTotal,
            this.colUomSetID});
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Name = "grdList";
            this.grdList.Size = new System.Drawing.Size(992, 180);
            this.grdList.TabIndex = 14;
            // 
            // colProductCode
            // 
            this.colProductCode.DataPropertyName = "ProductID";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colProductCode.DefaultCellStyle = dataGridViewCellStyle2;
            this.colProductCode.HeaderText = "รหัสสินค้า";
            this.colProductCode.MaxInputLength = 15;
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colSearchProduct
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colSearchProduct.DefaultCellStyle = dataGridViewCellStyle3;
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
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colProductName.DefaultCellStyle = dataGridViewCellStyle4;
            this.colProductName.HeaderText = "รายการ";
            this.colProductName.Name = "colProductName";
            this.colProductName.ReadOnly = true;
            this.colProductName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colUnit
            // 
            this.colUnit.DataPropertyName = "UomSetName";
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colUnit.DefaultCellStyle = dataGridViewCellStyle5;
            this.colUnit.HeaderText = "หน่วย";
            this.colUnit.MaxInputLength = 15;
            this.colUnit.Name = "colUnit";
            this.colUnit.ReadOnly = true;
            this.colUnit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colUnit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colUnit.Width = 70;
            // 
            // colVAT
            // 
            this.colVAT.DataPropertyName = "VatType";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colVAT.DefaultCellStyle = dataGridViewCellStyle6;
            this.colVAT.HeaderText = "ภาษี";
            this.colVAT.Name = "colVAT";
            this.colVAT.ReadOnly = true;
            this.colVAT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colVAT.Width = 60;
            // 
            // colAmount
            // 
            this.colAmount.DataPropertyName = "OrderQty";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle7.Format = "N2";
            dataGridViewCellStyle7.NullValue = null;
            this.colAmount.DefaultCellStyle = dataGridViewCellStyle7;
            this.colAmount.HeaderText = "จำนวน";
            this.colAmount.MaxInputLength = 6;
            this.colAmount.Name = "colAmount";
            this.colAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colAmount.Width = 80;
            // 
            // colUnitPrice
            // 
            this.colUnitPrice.DataPropertyName = "UnitPrice";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle8.Format = "N2";
            this.colUnitPrice.DefaultCellStyle = dataGridViewCellStyle8;
            this.colUnitPrice.HeaderText = "หน่วยละ";
            this.colUnitPrice.MaxInputLength = 10;
            this.colUnitPrice.Name = "colUnitPrice";
            this.colUnitPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colUnitPrice.Width = 120;
            // 
            // colTotal
            // 
            this.colTotal.DataPropertyName = "LineTotal";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle9.Format = "N2";
            this.colTotal.DefaultCellStyle = dataGridViewCellStyle9;
            this.colTotal.HeaderText = "จำนวนเงิน";
            this.colTotal.Name = "colTotal";
            this.colTotal.ReadOnly = true;
            this.colTotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTotal.Width = 120;
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
            this.panel5.TabIndex = 19;
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
            // frmOD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 541);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmOD";
            this.Text = "frmOD";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmOD_FormClosed);
            this.Load += new System.EventHandler(this.frmOD_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormPic)).EndInit();
            this.panel3.ResumeLayout(false);
            this.pnlBot.ResumeLayout(false);
            this.pnlBL.ResumeLayout(false);
            this.pnlBL.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.pnlBR.ResumeLayout(false);
            this.pnlBR.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTR.ResumeLayout(false);
            this.pnlTR.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCreditDay)).EndInit();
            this.pnlTL.ResumeLayout(false);
            this.pnlTL.PerformLayout();
            this.pnlCen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox FormPic;
        private System.Windows.Forms.Panel pnlTL;
        private System.Windows.Forms.Panel pnlTR;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label FormHeader;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnSearchSupp;
        private System.Windows.Forms.Label lblSupplierCode;
        private System.Windows.Forms.TextBox txtSuppName;
        private System.Windows.Forms.TextBox txtSupplierCode;
        private System.Windows.Forms.TextBox txtCrUser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTelephone;
        private System.Windows.Forms.Label lblTelephone;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.Label lblContact;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Button btnSearchDoc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox ddlDocStatus;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpDueDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudCreditDay;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpDocDate;
        private System.Windows.Forms.Panel pnlBL;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton rdoProdCode;
        private System.Windows.Forms.RadioButton rdoAccCode;
        private System.Windows.Forms.Panel pnlCen;
        private System.Windows.Forms.DataGridView grdList;
        private System.Windows.Forms.Panel pnlBR;
        private System.Windows.Forms.TextBox txnTotalDue;
        private System.Windows.Forms.TextBox txnAmount;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txnExcVat;
        private System.Windows.Forms.TextBox txnVatAmt;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txnIncVat;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlBot;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.MaskedTextBox txdDocNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductCode;
        private System.Windows.Forms.DataGridViewButtonColumn colSearchProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVAT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
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