
namespace AllCashUFormsApp.View.UControl
{
    partial class frmExcel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExcel));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pnlBackground = new System.Windows.Forms.Panel();
            this.pnlData = new System.Windows.Forms.Panel();
            this.pnlShowData = new System.Windows.Forms.Panel();
            this.tabExcel = new System.Windows.Forms.TabControl();
            this.tabHQPromotion = new System.Windows.Forms.TabPage();
            this.grdHQPromotion = new System.Windows.Forms.DataGridView();
            this.tabHQPromotionMaster = new System.Windows.Forms.TabPage();
            this.grdHQPromotionMaster = new System.Windows.Forms.DataGridView();
            this.tabHQReward = new System.Windows.Forms.TabPage();
            this.grdHQReward = new System.Windows.Forms.DataGridView();
            this.tabHQSKUGroup = new System.Windows.Forms.TabPage();
            this.grdHQSKUGroup = new System.Windows.Forms.DataGridView();
            this.tabHQSKUGroupExc = new System.Windows.Forms.TabPage();
            this.grdHQSKUGroupExc = new System.Windows.Forms.DataGridView();
            this.pnlConfirm = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnUpLoad = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtPathExcel = new System.Windows.Forms.TextBox();
            this.pnlBackground.SuspendLayout();
            this.pnlData.SuspendLayout();
            this.pnlShowData.SuspendLayout();
            this.tabExcel.SuspendLayout();
            this.tabHQPromotion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdHQPromotion)).BeginInit();
            this.tabHQPromotionMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdHQPromotionMaster)).BeginInit();
            this.tabHQReward.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdHQReward)).BeginInit();
            this.tabHQSKUGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdHQSKUGroup)).BeginInit();
            this.tabHQSKUGroupExc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdHQSKUGroupExc)).BeginInit();
            this.pnlConfirm.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pnlBackground
            // 
            this.pnlBackground.BackColor = System.Drawing.Color.Azure;
            this.pnlBackground.Controls.Add(this.pnlData);
            this.pnlBackground.Controls.Add(this.pnlSearch);
            this.pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackground.Location = new System.Drawing.Point(0, 0);
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.Size = new System.Drawing.Size(753, 444);
            this.pnlBackground.TabIndex = 0;
            // 
            // pnlData
            // 
            this.pnlData.Controls.Add(this.pnlShowData);
            this.pnlData.Controls.Add(this.pnlConfirm);
            this.pnlData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlData.Location = new System.Drawing.Point(0, 59);
            this.pnlData.Name = "pnlData";
            this.pnlData.Size = new System.Drawing.Size(753, 385);
            this.pnlData.TabIndex = 13;
            // 
            // pnlShowData
            // 
            this.pnlShowData.Controls.Add(this.tabExcel);
            this.pnlShowData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlShowData.Location = new System.Drawing.Point(0, 0);
            this.pnlShowData.Name = "pnlShowData";
            this.pnlShowData.Size = new System.Drawing.Size(753, 337);
            this.pnlShowData.TabIndex = 2;
            // 
            // tabExcel
            // 
            this.tabExcel.Controls.Add(this.tabHQPromotion);
            this.tabExcel.Controls.Add(this.tabHQPromotionMaster);
            this.tabExcel.Controls.Add(this.tabHQReward);
            this.tabExcel.Controls.Add(this.tabHQSKUGroup);
            this.tabExcel.Controls.Add(this.tabHQSKUGroupExc);
            this.tabExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabExcel.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.tabExcel.Location = new System.Drawing.Point(0, 0);
            this.tabExcel.Name = "tabExcel";
            this.tabExcel.SelectedIndex = 0;
            this.tabExcel.Size = new System.Drawing.Size(753, 337);
            this.tabExcel.TabIndex = 4;
            // 
            // tabHQPromotion
            // 
            this.tabHQPromotion.Controls.Add(this.grdHQPromotion);
            this.tabHQPromotion.Location = new System.Drawing.Point(4, 25);
            this.tabHQPromotion.Name = "tabHQPromotion";
            this.tabHQPromotion.Padding = new System.Windows.Forms.Padding(3);
            this.tabHQPromotion.Size = new System.Drawing.Size(745, 308);
            this.tabHQPromotion.TabIndex = 0;
            this.tabHQPromotion.Text = "HQ-Promotion";
            this.tabHQPromotion.UseVisualStyleBackColor = true;
            // 
            // grdHQPromotion
            // 
            this.grdHQPromotion.AllowUserToAddRows = false;
            this.grdHQPromotion.AllowUserToDeleteRows = false;
            this.grdHQPromotion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdHQPromotion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdHQPromotion.Location = new System.Drawing.Point(3, 3);
            this.grdHQPromotion.Name = "grdHQPromotion";
            this.grdHQPromotion.ReadOnly = true;
            this.grdHQPromotion.Size = new System.Drawing.Size(739, 302);
            this.grdHQPromotion.TabIndex = 0;
            this.grdHQPromotion.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.grdHQPromotion_RowPostPaint);
            // 
            // tabHQPromotionMaster
            // 
            this.tabHQPromotionMaster.Controls.Add(this.grdHQPromotionMaster);
            this.tabHQPromotionMaster.Location = new System.Drawing.Point(4, 25);
            this.tabHQPromotionMaster.Name = "tabHQPromotionMaster";
            this.tabHQPromotionMaster.Padding = new System.Windows.Forms.Padding(3);
            this.tabHQPromotionMaster.Size = new System.Drawing.Size(745, 308);
            this.tabHQPromotionMaster.TabIndex = 1;
            this.tabHQPromotionMaster.Text = "HQ-Promotion-Master";
            this.tabHQPromotionMaster.UseVisualStyleBackColor = true;
            // 
            // grdHQPromotionMaster
            // 
            this.grdHQPromotionMaster.AllowUserToAddRows = false;
            this.grdHQPromotionMaster.AllowUserToDeleteRows = false;
            this.grdHQPromotionMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdHQPromotionMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdHQPromotionMaster.Location = new System.Drawing.Point(3, 3);
            this.grdHQPromotionMaster.Name = "grdHQPromotionMaster";
            this.grdHQPromotionMaster.ReadOnly = true;
            this.grdHQPromotionMaster.Size = new System.Drawing.Size(739, 302);
            this.grdHQPromotionMaster.TabIndex = 1;
            this.grdHQPromotionMaster.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.grdHQPromotionMaster_RowPostPaint);
            // 
            // tabHQReward
            // 
            this.tabHQReward.Controls.Add(this.grdHQReward);
            this.tabHQReward.Location = new System.Drawing.Point(4, 25);
            this.tabHQReward.Name = "tabHQReward";
            this.tabHQReward.Padding = new System.Windows.Forms.Padding(3);
            this.tabHQReward.Size = new System.Drawing.Size(745, 308);
            this.tabHQReward.TabIndex = 2;
            this.tabHQReward.Text = "HQ-Reward";
            this.tabHQReward.UseVisualStyleBackColor = true;
            // 
            // grdHQReward
            // 
            this.grdHQReward.AllowUserToAddRows = false;
            this.grdHQReward.AllowUserToDeleteRows = false;
            this.grdHQReward.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdHQReward.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdHQReward.Location = new System.Drawing.Point(3, 3);
            this.grdHQReward.Name = "grdHQReward";
            this.grdHQReward.ReadOnly = true;
            this.grdHQReward.Size = new System.Drawing.Size(739, 302);
            this.grdHQReward.TabIndex = 2;
            this.grdHQReward.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.grdHQReward_RowPostPaint);
            // 
            // tabHQSKUGroup
            // 
            this.tabHQSKUGroup.Controls.Add(this.grdHQSKUGroup);
            this.tabHQSKUGroup.Location = new System.Drawing.Point(4, 25);
            this.tabHQSKUGroup.Name = "tabHQSKUGroup";
            this.tabHQSKUGroup.Size = new System.Drawing.Size(745, 308);
            this.tabHQSKUGroup.TabIndex = 3;
            this.tabHQSKUGroup.Text = "HQ-SKUGroup";
            this.tabHQSKUGroup.UseVisualStyleBackColor = true;
            // 
            // grdHQSKUGroup
            // 
            this.grdHQSKUGroup.AllowUserToAddRows = false;
            this.grdHQSKUGroup.AllowUserToDeleteRows = false;
            this.grdHQSKUGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdHQSKUGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdHQSKUGroup.Location = new System.Drawing.Point(0, 0);
            this.grdHQSKUGroup.Name = "grdHQSKUGroup";
            this.grdHQSKUGroup.ReadOnly = true;
            this.grdHQSKUGroup.Size = new System.Drawing.Size(745, 308);
            this.grdHQSKUGroup.TabIndex = 3;
            this.grdHQSKUGroup.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.grdHQSKUGroup_RowPostPaint);
            // 
            // tabHQSKUGroupExc
            // 
            this.tabHQSKUGroupExc.Controls.Add(this.grdHQSKUGroupExc);
            this.tabHQSKUGroupExc.Location = new System.Drawing.Point(4, 25);
            this.tabHQSKUGroupExc.Name = "tabHQSKUGroupExc";
            this.tabHQSKUGroupExc.Size = new System.Drawing.Size(745, 308);
            this.tabHQSKUGroupExc.TabIndex = 4;
            this.tabHQSKUGroupExc.Text = "HQ-SKUGroupExc";
            this.tabHQSKUGroupExc.UseVisualStyleBackColor = true;
            // 
            // grdHQSKUGroupExc
            // 
            this.grdHQSKUGroupExc.AllowUserToAddRows = false;
            this.grdHQSKUGroupExc.AllowUserToDeleteRows = false;
            this.grdHQSKUGroupExc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdHQSKUGroupExc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdHQSKUGroupExc.Location = new System.Drawing.Point(0, 0);
            this.grdHQSKUGroupExc.Name = "grdHQSKUGroupExc";
            this.grdHQSKUGroupExc.ReadOnly = true;
            this.grdHQSKUGroupExc.Size = new System.Drawing.Size(745, 308);
            this.grdHQSKUGroupExc.TabIndex = 4;
            this.grdHQSKUGroupExc.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.grdHQSKUGroupExc_RowPostPaint);
            // 
            // pnlConfirm
            // 
            this.pnlConfirm.Controls.Add(this.btnCancel);
            this.pnlConfirm.Controls.Add(this.btnConfirm);
            this.pnlConfirm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlConfirm.Location = new System.Drawing.Point(0, 337);
            this.pnlConfirm.Name = "pnlConfirm";
            this.pnlConfirm.Size = new System.Drawing.Size(753, 48);
            this.pnlConfirm.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(361, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 35);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnConfirm.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnConfirm.Image = ((System.Drawing.Image)(resources.GetObject("btnConfirm.Image")));
            this.btnConfirm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfirm.Location = new System.Drawing.Point(276, 5);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(81, 35);
            this.btnConfirm.TabIndex = 5;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.btnUpLoad);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Controls.Add(this.btnBrowse);
            this.pnlSearch.Controls.Add(this.txtPathExcel);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(753, 59);
            this.pnlSearch.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(147, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(457, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "SKUGroup และ SKUGroupEXC กำหนด Columns ใน Excel เป็น Type Text เท่านั้น !!";
            // 
            // btnUpLoad
            // 
            this.btnUpLoad.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnUpLoad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpLoad.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnUpLoad.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnUpLoad.Image = ((System.Drawing.Image)(resources.GetObject("btnUpLoad.Image")));
            this.btnUpLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpLoad.Location = new System.Drawing.Point(500, 6);
            this.btnUpLoad.Name = "btnUpLoad";
            this.btnUpLoad.Size = new System.Drawing.Size(81, 26);
            this.btnUpLoad.TabIndex = 3;
            this.btnUpLoad.Text = "UpLoad";
            this.btnUpLoad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpLoad.UseVisualStyleBackColor = false;
            this.btnUpLoad.Click += new System.EventHandler(this.btnUpLoad_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label1.Location = new System.Drawing.Point(76, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "PathExcel :";
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnBrowse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowse.Image")));
            this.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrowse.Location = new System.Drawing.Point(413, 6);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(81, 26);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtPathExcel
            // 
            this.txtPathExcel.Location = new System.Drawing.Point(153, 8);
            this.txtPathExcel.MaxLength = 100;
            this.txtPathExcel.Name = "txtPathExcel";
            this.txtPathExcel.Size = new System.Drawing.Size(241, 23);
            this.txtPathExcel.TabIndex = 1;
            // 
            // frmExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 444);
            this.Controls.Add(this.pnlBackground);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ส่งข้อมูล";
            this.pnlBackground.ResumeLayout(false);
            this.pnlData.ResumeLayout(false);
            this.pnlShowData.ResumeLayout(false);
            this.tabExcel.ResumeLayout(false);
            this.tabHQPromotion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdHQPromotion)).EndInit();
            this.tabHQPromotionMaster.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdHQPromotionMaster)).EndInit();
            this.tabHQReward.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdHQReward)).EndInit();
            this.tabHQSKUGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdHQSKUGroup)).EndInit();
            this.tabHQSKUGroupExc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdHQSKUGroupExc)).EndInit();
            this.pnlConfirm.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel pnlBackground;
        private System.Windows.Forms.TextBox txtPathExcel;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnUpLoad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Panel pnlData;
        private System.Windows.Forms.Panel pnlConfirm;
        private System.Windows.Forms.Panel pnlShowData;
        private System.Windows.Forms.TabControl tabExcel;
        private System.Windows.Forms.TabPage tabHQPromotion;
        private System.Windows.Forms.TabPage tabHQPromotionMaster;
        private System.Windows.Forms.DataGridView grdHQPromotion;
        private System.Windows.Forms.DataGridView grdHQPromotionMaster;
        private System.Windows.Forms.TabPage tabHQReward;
        private System.Windows.Forms.TabPage tabHQSKUGroup;
        private System.Windows.Forms.TabPage tabHQSKUGroupExc;
        private System.Windows.Forms.DataGridView grdHQReward;
        private System.Windows.Forms.DataGridView grdHQSKUGroup;
        private System.Windows.Forms.DataGridView grdHQSKUGroupExc;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label label2;
    }
}