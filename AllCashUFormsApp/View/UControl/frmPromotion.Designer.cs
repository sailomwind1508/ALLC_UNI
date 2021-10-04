namespace AllCashUFormsApp.View.UControl
{
    partial class frmPromotion
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPromotion));
            this.pnlBR = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCountList = new System.Windows.Forms.Label();
            this.lblCountListText = new System.Windows.Forms.Label();
            this.colChoose = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPromotionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPromotionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSKUGroupID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRewardName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDisCountAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSKUGroupRewardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSKUGroupRewardAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEffectiveDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExpireDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlBR.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBR
            // 
            this.pnlBR.BackColor = System.Drawing.SystemColors.Window;
            this.pnlBR.Controls.Add(this.panel2);
            this.pnlBR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBR.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBR.Location = new System.Drawing.Point(0, 0);
            this.pnlBR.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlBR.Name = "pnlBR";
            this.pnlBR.Size = new System.Drawing.Size(930, 422);
            this.pnlBR.TabIndex = 23;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Azure;
            this.panel2.Controls.Add(this.grdList);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(906, 401);
            this.panel2.TabIndex = 19;
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChoose,
            this.colNo,
            this.colPromotionID,
            this.colPromotionName,
            this.colSKUGroupID,
            this.colRewardName,
            this.colDisCountAmt,
            this.colSKUGroupRewardID,
            this.colSKUGroupRewardAmt,
            this.colEffectiveDate,
            this.colExpireDate});
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdList.Name = "grdList";
            this.grdList.RowHeadersVisible = false;
            this.grdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdList.Size = new System.Drawing.Size(906, 369);
            this.grdList.TabIndex = 2;
            this.grdList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdList_CellContentClick);
            this.grdList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdList_CellDoubleClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Azure;
            this.panel1.Controls.Add(this.lblCountList);
            this.panel1.Controls.Add(this.lblCountListText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 369);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(906, 32);
            this.panel1.TabIndex = 18;
            // 
            // lblCountList
            // 
            this.lblCountList.AutoSize = true;
            this.lblCountList.Location = new System.Drawing.Point(92, 7);
            this.lblCountList.Name = "lblCountList";
            this.lblCountList.Size = new System.Drawing.Size(42, 16);
            this.lblCountList.TabIndex = 1;
            this.lblCountList.Text = "label1";
            // 
            // lblCountListText
            // 
            this.lblCountListText.AutoSize = true;
            this.lblCountListText.Location = new System.Drawing.Point(3, 7);
            this.lblCountListText.Name = "lblCountListText";
            this.lblCountListText.Size = new System.Drawing.Size(83, 16);
            this.lblCountListText.TabIndex = 0;
            this.lblCountListText.Text = "จำนวนรายการ";
            // 
            // colChoose
            // 
            this.colChoose.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colChoose.DataPropertyName = "Choose";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle2.NullValue = false;
            this.colChoose.DefaultCellStyle = dataGridViewCellStyle2;
            this.colChoose.FillWeight = 30F;
            this.colChoose.HeaderText = "เลือก";
            this.colChoose.MinimumWidth = 30;
            this.colChoose.Name = "colChoose";
            this.colChoose.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // colNo
            // 
            this.colNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colNo.DataPropertyName = "No";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colNo.DefaultCellStyle = dataGridViewCellStyle3;
            this.colNo.FillWeight = 50F;
            this.colNo.HeaderText = "No.";
            this.colNo.MinimumWidth = 50;
            this.colNo.Name = "colNo";
            this.colNo.ReadOnly = true;
            this.colNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colNo.Visible = false;
            // 
            // colPromotionID
            // 
            this.colPromotionID.DataPropertyName = "PromotionID";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colPromotionID.DefaultCellStyle = dataGridViewCellStyle4;
            this.colPromotionID.HeaderText = "รหัสโปรโมชั่น";
            this.colPromotionID.MinimumWidth = 100;
            this.colPromotionID.Name = "colPromotionID";
            this.colPromotionID.ReadOnly = true;
            this.colPromotionID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colPromotionName
            // 
            this.colPromotionName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colPromotionName.DataPropertyName = "PromotionName";
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colPromotionName.DefaultCellStyle = dataGridViewCellStyle5;
            this.colPromotionName.FillWeight = 200F;
            this.colPromotionName.HeaderText = "โปรโมชั่น";
            this.colPromotionName.MinimumWidth = 200;
            this.colPromotionName.Name = "colPromotionName";
            this.colPromotionName.ReadOnly = true;
            this.colPromotionName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colSKUGroupID
            // 
            this.colSKUGroupID.DataPropertyName = "SKUGroupID";
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colSKUGroupID.DefaultCellStyle = dataGridViewCellStyle6;
            this.colSKUGroupID.HeaderText = "กลุ่มสินค้า";
            this.colSKUGroupID.MinimumWidth = 100;
            this.colSKUGroupID.Name = "colSKUGroupID";
            this.colSKUGroupID.ReadOnly = true;
            this.colSKUGroupID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colSKUGroupID.Visible = false;
            // 
            // colRewardName
            // 
            this.colRewardName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colRewardName.DataPropertyName = "RewardName";
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colRewardName.DefaultCellStyle = dataGridViewCellStyle7;
            this.colRewardName.FillWeight = 200F;
            this.colRewardName.HeaderText = "สิ่งที่ไดรับ";
            this.colRewardName.MinimumWidth = 200;
            this.colRewardName.Name = "colRewardName";
            this.colRewardName.ReadOnly = true;
            this.colRewardName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colDisCountAmt
            // 
            this.colDisCountAmt.DataPropertyName = "DisCountAmt";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle8.Format = "N2";
            this.colDisCountAmt.DefaultCellStyle = dataGridViewCellStyle8;
            this.colDisCountAmt.HeaderText = "ส่วนลด(บาท)";
            this.colDisCountAmt.MinimumWidth = 100;
            this.colDisCountAmt.Name = "colDisCountAmt";
            this.colDisCountAmt.ReadOnly = true;
            this.colDisCountAmt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colSKUGroupRewardID
            // 
            this.colSKUGroupRewardID.DataPropertyName = "SKUGroupRewardID";
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colSKUGroupRewardID.DefaultCellStyle = dataGridViewCellStyle9;
            this.colSKUGroupRewardID.HeaderText = "สินค้าที่แจก";
            this.colSKUGroupRewardID.MinimumWidth = 100;
            this.colSKUGroupRewardID.Name = "colSKUGroupRewardID";
            this.colSKUGroupRewardID.ReadOnly = true;
            this.colSKUGroupRewardID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colSKUGroupRewardID.Visible = false;
            // 
            // colSKUGroupRewardAmt
            // 
            this.colSKUGroupRewardAmt.DataPropertyName = "SKUGroupRewardAmt";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle10.NullValue = "N0";
            this.colSKUGroupRewardAmt.DefaultCellStyle = dataGridViewCellStyle10;
            this.colSKUGroupRewardAmt.HeaderText = "จำนวนสินค้าที่แจก";
            this.colSKUGroupRewardAmt.MinimumWidth = 100;
            this.colSKUGroupRewardAmt.Name = "colSKUGroupRewardAmt";
            this.colSKUGroupRewardAmt.ReadOnly = true;
            this.colSKUGroupRewardAmt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colSKUGroupRewardAmt.Visible = false;
            // 
            // colEffectiveDate
            // 
            this.colEffectiveDate.DataPropertyName = "EffectiveDate";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle11.Format = "dd/MM/yyyy";
            this.colEffectiveDate.DefaultCellStyle = dataGridViewCellStyle11;
            this.colEffectiveDate.FillWeight = 80F;
            this.colEffectiveDate.HeaderText = "วันที่เริ่มโปรโมชั่น";
            this.colEffectiveDate.MinimumWidth = 80;
            this.colEffectiveDate.Name = "colEffectiveDate";
            this.colEffectiveDate.ReadOnly = true;
            this.colEffectiveDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colExpireDate
            // 
            this.colExpireDate.DataPropertyName = "ExpireDate";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle12.Format = "dd/MM/yyyy";
            this.colExpireDate.DefaultCellStyle = dataGridViewCellStyle12;
            this.colExpireDate.FillWeight = 80F;
            this.colExpireDate.HeaderText = "วันที่สิ้นสุดโปรโมชั่น";
            this.colExpireDate.MinimumWidth = 80;
            this.colExpireDate.Name = "colExpireDate";
            this.colExpireDate.ReadOnly = true;
            this.colExpireDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // frmPromotion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 422);
            this.Controls.Add(this.pnlBR);
            this.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.Name = "frmPromotion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSearchSupp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPromotion_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPromotion_FormClosed);
            this.Load += new System.EventHandler(this.frmPromotion_Load);
            this.pnlBR.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBR;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCountList;
        private System.Windows.Forms.Label lblCountListText;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView grdList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChoose;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPromotionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPromotionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSKUGroupID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRewardName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDisCountAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSKUGroupRewardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSKUGroupRewardAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEffectiveDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExpireDate;
    }
}