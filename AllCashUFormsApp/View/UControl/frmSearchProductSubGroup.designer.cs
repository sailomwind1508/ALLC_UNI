﻿namespace AllCashUFormsApp.View.UControl
{
    partial class frmSearchProductSubGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearchProductSubGroup));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.colProductSubGroupCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductSubGroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductSubGroupID = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.pnlTopPage.TabIndex = 2;
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
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
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
            this.pnlBottomPage.Location = new System.Drawing.Point(0, 509);
            this.pnlBottomPage.Name = "pnlBottomPage";
            this.pnlBottomPage.Size = new System.Drawing.Size(553, 37);
            this.pnlBottomPage.TabIndex = 3;
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
            this.btnAccept.Location = new System.Drawing.Point(395, 4);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(74, 28);
            this.btnAccept.TabIndex = 18;
            this.btnAccept.Text = "ตกลง";
            this.btnAccept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
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
            this.colProductSubGroupCode,
            this.colProductSubGroupName,
            this.colProductSubGroupID});
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Location = new System.Drawing.Point(0, 45);
            this.grdList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdList.Name = "grdList";
            this.grdList.Size = new System.Drawing.Size(553, 464);
            this.grdList.TabIndex = 32;
            this.grdList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.grdList_RowPostPaint);
            // 
            // colSelect
            // 
            this.colSelect.DataPropertyName = "Select";
            this.colSelect.FillWeight = 30F;
            this.colSelect.HeaderText = "";
            this.colSelect.Name = "colSelect";
            this.colSelect.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colSelect.Width = 30;
            // 
            // colProductSubGroupCode
            // 
            this.colProductSubGroupCode.DataPropertyName = "ProductSubGroupCode";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colProductSubGroupCode.DefaultCellStyle = dataGridViewCellStyle1;
            this.colProductSubGroupCode.HeaderText = "รหัส";
            this.colProductSubGroupCode.MaxInputLength = 10;
            this.colProductSubGroupCode.Name = "colProductSubGroupCode";
            this.colProductSubGroupCode.ReadOnly = true;
            this.colProductSubGroupCode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colProductSubGroupCode.Width = 120;
            // 
            // colProductSubGroupName
            // 
            this.colProductSubGroupName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colProductSubGroupName.DataPropertyName = "ProductSubGroupName";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colProductSubGroupName.DefaultCellStyle = dataGridViewCellStyle2;
            this.colProductSubGroupName.HeaderText = "ชื่อ";
            this.colProductSubGroupName.MaxInputLength = 50;
            this.colProductSubGroupName.Name = "colProductSubGroupName";
            // 
            // colProductSubGroupID
            // 
            this.colProductSubGroupID.DataPropertyName = "ProductSubGroupID";
            this.colProductSubGroupID.HeaderText = "ProductSubGroupID";
            this.colProductSubGroupID.Name = "colProductSubGroupID";
            this.colProductSubGroupID.Visible = false;
            // 
            // frmSearchProductSubGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(553, 546);
            this.Controls.Add(this.grdList);
            this.Controls.Add(this.pnlBottomPage);
            this.Controls.Add(this.pnlTopPage);
            this.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.Name = "frmSearchProductSubGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ค้นหารายการ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSearchProductSubGroup_FormClosed);
            this.Load += new System.EventHandler(this.frmSearchProductSubGroup_Load);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductSubGroupCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductSubGroupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductSubGroupID;
    }
}