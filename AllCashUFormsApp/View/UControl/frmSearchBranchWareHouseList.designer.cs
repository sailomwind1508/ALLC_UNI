namespace AllCashUFormsApp.View.UControl
{
    partial class frmSearchBranchWareHouseList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearchBranchWareHouseList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblgridCount = new System.Windows.Forms.Label();
            this.lblCountListText = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.colSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colWHID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBranchID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label1.Location = new System.Drawing.Point(11, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "ค้นหาจากรหัส";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(98, 15);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(439, 23);
            this.txtSearch.TabIndex = 1;
            // 
            // lblgridCount
            // 
            this.lblgridCount.AutoSize = true;
            this.lblgridCount.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lblgridCount.Location = new System.Drawing.Point(103, 507);
            this.lblgridCount.Name = "lblgridCount";
            this.lblgridCount.Size = new System.Drawing.Size(15, 16);
            this.lblgridCount.TabIndex = 5;
            this.lblgridCount.Text = "0";
            // 
            // lblCountListText
            // 
            this.lblCountListText.AutoSize = true;
            this.lblCountListText.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lblCountListText.Location = new System.Drawing.Point(14, 507);
            this.lblCountListText.Name = "lblCountListText";
            this.lblCountListText.Size = new System.Drawing.Size(83, 16);
            this.lblCountListText.TabIndex = 4;
            this.lblCountListText.Text = "จำนวนรายการ";
            // 
            // btnAccept
            // 
            this.btnAccept.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnAccept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccept.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnAccept.Image = ((System.Drawing.Image)(resources.GetObject("btnAccept.Image")));
            this.btnAccept.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAccept.Location = new System.Drawing.Point(383, 502);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(74, 28);
            this.btnAccept.TabIndex = 6;
            this.btnAccept.Text = "ตกลง";
            this.btnAccept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(463, 502);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 28);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelect,
            this.colWHID,
            this.Column6,
            this.colBranchID});
            this.grdList.Location = new System.Drawing.Point(14, 46);
            this.grdList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdList.Name = "grdList";
            this.grdList.Size = new System.Drawing.Size(523, 448);
            this.grdList.TabIndex = 8;
            this.grdList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.grdList_RowPostPaint);
            // 
            // colSelect
            // 
            this.colSelect.FillWeight = 30F;
            this.colSelect.HeaderText = "";
            this.colSelect.Name = "colSelect";
            this.colSelect.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colSelect.Width = 30;
            // 
            // colWHID
            // 
            this.colWHID.DataPropertyName = "WHID";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colWHID.DefaultCellStyle = dataGridViewCellStyle1;
            this.colWHID.HeaderText = "รหัสคลัง";
            this.colWHID.MaxInputLength = 20;
            this.colWHID.Name = "colWHID";
            this.colWHID.ReadOnly = true;
            this.colWHID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colWHID.Width = 90;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column6.DataPropertyName = "WHName";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.Column6.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column6.HeaderText = "ชื่อคลัง";
            this.Column6.MaxInputLength = 20;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colBranchID
            // 
            this.colBranchID.DataPropertyName = "BranchID";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colBranchID.DefaultCellStyle = dataGridViewCellStyle3;
            this.colBranchID.HeaderText = "รหัสสาขา";
            this.colBranchID.MaxInputLength = 20;
            this.colBranchID.Name = "colBranchID";
            this.colBranchID.ReadOnly = true;
            this.colBranchID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // frmSearchBranchWareHouseList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(553, 538);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lblgridCount);
            this.Controls.Add(this.lblCountListText);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSearchBranchWareHouseList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ค้นหารายการ";
            this.Load += new System.EventHandler(this.frmSearchBrachWareHouseList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblgridCount;
        private System.Windows.Forms.Label lblCountListText;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView grdList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWHID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBranchID;
    }
}