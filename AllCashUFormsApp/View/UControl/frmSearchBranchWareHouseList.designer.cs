﻿namespace AllCashUFormsApp.View.UControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTopPage = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlBottomPage = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.lblgridCount = new System.Windows.Forms.Label();
            this.lblCountListText = new System.Windows.Forms.Label();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.colSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colWHID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWHName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSaleType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBranchID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTopPage.SuspendLayout();
            this.pnlBottomPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
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
            this.pnlTopPage.TabIndex = 1;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            // pnlBottomPage
            // 
            this.pnlBottomPage.BackColor = System.Drawing.Color.Azure;
            this.pnlBottomPage.Controls.Add(this.btnCancel);
            this.pnlBottomPage.Controls.Add(this.btnAccept);
            this.pnlBottomPage.Controls.Add(this.lblgridCount);
            this.pnlBottomPage.Controls.Add(this.lblCountListText);
            this.pnlBottomPage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottomPage.Location = new System.Drawing.Point(0, 501);
            this.pnlBottomPage.Name = "pnlBottomPage";
            this.pnlBottomPage.Size = new System.Drawing.Size(553, 37);
            this.pnlBottomPage.TabIndex = 2;
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
            this.btnCancel.Location = new System.Drawing.Point(475, 4);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 28);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = false;
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
            this.btnAccept.Location = new System.Drawing.Point(395, 4);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(74, 28);
            this.btnAccept.TabIndex = 18;
            this.btnAccept.Text = "ตกลง";
            this.btnAccept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAccept.UseVisualStyleBackColor = false;
            // 
            // lblgridCount
            // 
            this.lblgridCount.AutoSize = true;
            this.lblgridCount.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lblgridCount.Location = new System.Drawing.Point(104, 9);
            this.lblgridCount.Name = "lblgridCount";
            this.lblgridCount.Size = new System.Drawing.Size(15, 16);
            this.lblgridCount.TabIndex = 16;
            this.lblgridCount.Text = "0";
            // 
            // lblCountListText
            // 
            this.lblCountListText.AutoSize = true;
            this.lblCountListText.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lblCountListText.Location = new System.Drawing.Point(15, 9);
            this.lblCountListText.Name = "lblCountListText";
            this.lblCountListText.Size = new System.Drawing.Size(83, 16);
            this.lblCountListText.TabIndex = 15;
            this.lblCountListText.Text = "จำนวนรายการ";
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelect,
            this.colWHID,
            this.colWHName,
            this.colSaleType,
            this.colBranchID});
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Location = new System.Drawing.Point(0, 45);
            this.grdList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdList.Name = "grdList";
            this.grdList.Size = new System.Drawing.Size(553, 456);
            this.grdList.TabIndex = 16;
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
            // colWHName
            // 
            this.colWHName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colWHName.DataPropertyName = "WHName";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colWHName.DefaultCellStyle = dataGridViewCellStyle2;
            this.colWHName.HeaderText = "ชื่อคลัง";
            this.colWHName.MaxInputLength = 100;
            this.colWHName.Name = "colWHName";
            this.colWHName.ReadOnly = true;
            // 
            // colSaleType
            // 
            this.colSaleType.DataPropertyName = "SaleType";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.colSaleType.DefaultCellStyle = dataGridViewCellStyle3;
            this.colSaleType.HeaderText = "ประเภทแวน";
            this.colSaleType.MaxInputLength = 100;
            this.colSaleType.Name = "colSaleType";
            this.colSaleType.ReadOnly = true;
            // 
            // colBranchID
            // 
            this.colBranchID.DataPropertyName = "BranchID";
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colBranchID.DefaultCellStyle = dataGridViewCellStyle4;
            this.colBranchID.HeaderText = "รหัสสาขา";
            this.colBranchID.MaxInputLength = 20;
            this.colBranchID.Name = "colBranchID";
            this.colBranchID.ReadOnly = true;
            this.colBranchID.Visible = false;
            // 
            // frmSearchBranchWareHouseList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(553, 538);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.pnlBottomPage);
            this.Controls.Add(this.pnlTopPage);
            this.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmSearchBranchWareHouseList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ค้นหารายการ";
            this.pnlTopPage.ResumeLayout(false);
            this.pnlTopPage.PerformLayout();
            this.pnlBottomPage.ResumeLayout(false);
            this.pnlBottomPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTopPage;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlBottomPage;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Label lblgridCount;
        private System.Windows.Forms.Label lblCountListText;
        private System.Windows.Forms.DataGridView grdList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWHID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWHName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSaleType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBranchID;
    }
}