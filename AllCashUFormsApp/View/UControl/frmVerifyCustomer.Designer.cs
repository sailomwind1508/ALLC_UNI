
namespace AllCashUFormsApp.View.UControl
{
    partial class frmVerifyCustomer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVerifyCustomer));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.lblgridCount = new System.Windows.Forms.Label();
            this.lblCountListText = new System.Windows.Forms.Label();
            this.pnlBot = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.colSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colTotalDue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWHID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustShortName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBillTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDocNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDocTypeCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlBot.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(849, 4);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 28);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnAccept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccept.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnAccept.Image = ((System.Drawing.Image)(resources.GetObject("btnAccept.Image")));
            this.btnAccept.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAccept.Location = new System.Drawing.Point(769, 4);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(74, 28);
            this.btnAccept.TabIndex = 10;
            this.btnAccept.Text = "ตกลง";
            this.btnAccept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // lblgridCount
            // 
            this.lblgridCount.AutoSize = true;
            this.lblgridCount.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lblgridCount.Location = new System.Drawing.Point(96, 9);
            this.lblgridCount.Name = "lblgridCount";
            this.lblgridCount.Size = new System.Drawing.Size(15, 16);
            this.lblgridCount.TabIndex = 8;
            this.lblgridCount.Text = "0";
            // 
            // lblCountListText
            // 
            this.lblCountListText.AutoSize = true;
            this.lblCountListText.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lblCountListText.Location = new System.Drawing.Point(7, 9);
            this.lblCountListText.Name = "lblCountListText";
            this.lblCountListText.Size = new System.Drawing.Size(83, 16);
            this.lblCountListText.TabIndex = 7;
            this.lblCountListText.Text = "จำนวนรายการ";
            // 
            // pnlBot
            // 
            this.pnlBot.BackColor = System.Drawing.Color.Azure;
            this.pnlBot.Controls.Add(this.btnAccept);
            this.pnlBot.Controls.Add(this.btnCancel);
            this.pnlBot.Controls.Add(this.lblCountListText);
            this.pnlBot.Controls.Add(this.lblgridCount);
            this.pnlBot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBot.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.pnlBot.Location = new System.Drawing.Point(0, 352);
            this.pnlBot.Name = "pnlBot";
            this.pnlBot.Size = new System.Drawing.Size(933, 36);
            this.pnlBot.TabIndex = 119;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Azure;
            this.panel1.Controls.Add(this.grdList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(933, 352);
            this.panel1.TabIndex = 120;
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.AllowUserToOrderColumns = true;
            this.grdList.AllowUserToResizeRows = false;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelect,
            this.colTotalDue,
            this.colWHID,
            this.colCustomerID,
            this.colCustName,
            this.colCustShortName,
            this.colBillTo,
            this.colDocNo,
            this.colDocTypeCode});
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdList.Name = "grdList";
            this.grdList.Size = new System.Drawing.Size(933, 352);
            this.grdList.TabIndex = 5;
            this.grdList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdList_CellDoubleClick);
            this.grdList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.grdList_RowPostPaint);
            // 
            // colSelect
            // 
            this.colSelect.FillWeight = 30F;
            this.colSelect.HeaderText = "";
            this.colSelect.Name = "colSelect";
            this.colSelect.Width = 30;
            // 
            // colTotalDue
            // 
            this.colTotalDue.DataPropertyName = "TotalDue";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.colTotalDue.DefaultCellStyle = dataGridViewCellStyle1;
            this.colTotalDue.HeaderText = "ยอดขาย";
            this.colTotalDue.Name = "colTotalDue";
            this.colTotalDue.ReadOnly = true;
            this.colTotalDue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colWHID
            // 
            this.colWHID.DataPropertyName = "WHID";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.colWHID.DefaultCellStyle = dataGridViewCellStyle2;
            this.colWHID.HeaderText = "VAN";
            this.colWHID.Name = "colWHID";
            this.colWHID.ReadOnly = true;
            this.colWHID.Width = 70;
            // 
            // colCustomerID
            // 
            this.colCustomerID.DataPropertyName = "CustomerID";
            this.colCustomerID.HeaderText = "รหัสร้านค้า";
            this.colCustomerID.Name = "colCustomerID";
            this.colCustomerID.ReadOnly = true;
            // 
            // colCustName
            // 
            this.colCustName.DataPropertyName = "CustName";
            this.colCustName.HeaderText = "ชื่อร้านค้า";
            this.colCustName.Name = "colCustName";
            this.colCustName.ReadOnly = true;
            this.colCustName.Width = 160;
            // 
            // colCustShortName
            // 
            this.colCustShortName.DataPropertyName = "CustShortName";
            this.colCustShortName.HeaderText = "ชื่อย่อ";
            this.colCustShortName.Name = "colCustShortName";
            this.colCustShortName.ReadOnly = true;
            this.colCustShortName.Width = 120;
            // 
            // colBillTo
            // 
            this.colBillTo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colBillTo.DataPropertyName = "BillTo";
            this.colBillTo.HeaderText = "ที่อยู่";
            this.colBillTo.Name = "colBillTo";
            this.colBillTo.ReadOnly = true;
            // 
            // colDocNo
            // 
            this.colDocNo.DataPropertyName = "DocNo";
            this.colDocNo.HeaderText = "DocNo";
            this.colDocNo.Name = "colDocNo";
            this.colDocNo.ReadOnly = true;
            this.colDocNo.Visible = false;
            // 
            // colDocTypeCode
            // 
            this.colDocTypeCode.DataPropertyName = "DocTypeCode";
            this.colDocTypeCode.HeaderText = "DocTypeCode";
            this.colDocTypeCode.Name = "colDocTypeCode";
            this.colDocTypeCode.ReadOnly = true;
            this.colDocTypeCode.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Azure;
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.pnlBot);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(933, 388);
            this.panel2.TabIndex = 121;
            // 
            // frmVerifyCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 388);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.Name = "frmVerifyCustomer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "เลือกร้านที่จะออกใบกำกับภาษี";
            this.Load += new System.EventHandler(this.frmVerifyCustomer_Load);
            this.pnlBot.ResumeLayout(false);
            this.pnlBot.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Label lblgridCount;
        private System.Windows.Forms.Label lblCountListText;
        private System.Windows.Forms.Panel pnlBot;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView grdList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalDue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWHID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustShortName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBillTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDocNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDocTypeCode;
        private System.Windows.Forms.Panel panel2;
    }
}