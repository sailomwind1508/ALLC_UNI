
namespace AllCashUFormsApp.View.UControl
{
    partial class frmALLShopType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmALLShopType));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.pnlBottomPage = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.lbl_RowCount = new System.Windows.Forms.Label();
            this.lblQtyList = new System.Windows.Forms.Label();
            this.pnlTopPage = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.colSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colShopTypeCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShopTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShopTypeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.pnlBottomPage.SuspendLayout();
            this.pnlTopPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCenter
            // 
            this.pnlCenter.BackColor = System.Drawing.Color.Azure;
            this.pnlCenter.Controls.Add(this.grdList);
            this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenter.Location = new System.Drawing.Point(0, 45);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Size = new System.Drawing.Size(553, 459);
            this.pnlCenter.TabIndex = 5;
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelect,
            this.colShopTypeCode,
            this.colShopTypeName,
            this.colShopTypeID});
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdList.Name = "grdList";
            this.grdList.Size = new System.Drawing.Size(553, 459);
            this.grdList.TabIndex = 2;
            this.grdList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.grdList_RowPostPaint);
            // 
            // pnlBottomPage
            // 
            this.pnlBottomPage.BackColor = System.Drawing.Color.Azure;
            this.pnlBottomPage.Controls.Add(this.btnCancel);
            this.pnlBottomPage.Controls.Add(this.btnAccept);
            this.pnlBottomPage.Controls.Add(this.lbl_RowCount);
            this.pnlBottomPage.Controls.Add(this.lblQtyList);
            this.pnlBottomPage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottomPage.Location = new System.Drawing.Point(0, 504);
            this.pnlBottomPage.Name = "pnlBottomPage";
            this.pnlBottomPage.Size = new System.Drawing.Size(553, 37);
            this.pnlBottomPage.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(475, 3);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 28);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnAccept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccept.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnAccept.Image = ((System.Drawing.Image)(resources.GetObject("btnAccept.Image")));
            this.btnAccept.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAccept.Location = new System.Drawing.Point(395, 3);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(74, 28);
            this.btnAccept.TabIndex = 16;
            this.btnAccept.Text = "ตกลง";
            this.btnAccept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // lbl_RowCount
            // 
            this.lbl_RowCount.AutoSize = true;
            this.lbl_RowCount.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lbl_RowCount.Location = new System.Drawing.Point(91, 6);
            this.lbl_RowCount.Name = "lbl_RowCount";
            this.lbl_RowCount.Size = new System.Drawing.Size(15, 16);
            this.lbl_RowCount.TabIndex = 14;
            this.lbl_RowCount.Text = "0";
            // 
            // lblQtyList
            // 
            this.lblQtyList.AutoSize = true;
            this.lblQtyList.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lblQtyList.Location = new System.Drawing.Point(5, 6);
            this.lblQtyList.Name = "lblQtyList";
            this.lblQtyList.Size = new System.Drawing.Size(83, 16);
            this.lblQtyList.TabIndex = 13;
            this.lblQtyList.Text = "จำนวนรายการ";
            // 
            // pnlTopPage
            // 
            this.pnlTopPage.BackColor = System.Drawing.Color.Azure;
            this.pnlTopPage.Controls.Add(this.txtSearch);
            this.pnlTopPage.Controls.Add(this.label1);
            this.pnlTopPage.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopPage.Location = new System.Drawing.Point(0, 0);
            this.pnlTopPage.Name = "pnlTopPage";
            this.pnlTopPage.Size = new System.Drawing.Size(553, 45);
            this.pnlTopPage.TabIndex = 3;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(83, 11);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSearch.MaxLength = 50;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(456, 23);
            this.txtSearch.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label1.Location = new System.Drawing.Point(15, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "ค้นหารหัส";
            // 
            // colSelect
            // 
            this.colSelect.DataPropertyName = "colSelect";
            this.colSelect.FillWeight = 30F;
            this.colSelect.HeaderText = "";
            this.colSelect.Name = "colSelect";
            this.colSelect.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colSelect.Width = 30;
            // 
            // colShopTypeCode
            // 
            this.colShopTypeCode.DataPropertyName = "ShopTypeCode";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colShopTypeCode.DefaultCellStyle = dataGridViewCellStyle1;
            this.colShopTypeCode.HeaderText = "รหัสประเภทร้านค้า";
            this.colShopTypeCode.MaxInputLength = 2;
            this.colShopTypeCode.Name = "colShopTypeCode";
            this.colShopTypeCode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colShopTypeCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colShopTypeCode.Width = 120;
            // 
            // colShopTypeName
            // 
            this.colShopTypeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colShopTypeName.DataPropertyName = "ShopTypeName";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colShopTypeName.DefaultCellStyle = dataGridViewCellStyle2;
            this.colShopTypeName.HeaderText = "ชื่อประเภทร้านค้า";
            this.colShopTypeName.MaxInputLength = 50;
            this.colShopTypeName.Name = "colShopTypeName";
            this.colShopTypeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colShopTypeID
            // 
            this.colShopTypeID.DataPropertyName = "ShopTypeID";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.colShopTypeID.DefaultCellStyle = dataGridViewCellStyle3;
            this.colShopTypeID.HeaderText = "ShopTypeID";
            this.colShopTypeID.MaxInputLength = 20;
            this.colShopTypeID.Name = "colShopTypeID";
            this.colShopTypeID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colShopTypeID.Visible = false;
            // 
            // frmALLShopType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 541);
            this.Controls.Add(this.pnlCenter);
            this.Controls.Add(this.pnlBottomPage);
            this.Controls.Add(this.pnlTopPage);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmALLShopType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ค้นหาประเภทร้านค้า";
            this.Load += new System.EventHandler(this.frmALLShopType_Load);
            this.pnlCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.pnlBottomPage.ResumeLayout(false);
            this.pnlBottomPage.PerformLayout();
            this.pnlTopPage.ResumeLayout(false);
            this.pnlTopPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.DataGridView grdList;
        private System.Windows.Forms.Panel pnlBottomPage;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Label lbl_RowCount;
        private System.Windows.Forms.Label lblQtyList;
        private System.Windows.Forms.Panel pnlTopPage;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShopTypeCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShopTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShopTypeID;
    }
}