
namespace AllCashUFormsApp.View.UControl
{
    partial class frmSearchProvince
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearchProvince));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelRowCount = new System.Windows.Forms.Label();
            this.lblQtyList = new System.Windows.Forms.Label();
            this.grdProvince = new System.Windows.Forms.DataGridView();
            this.colProvinceCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProvinceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProvinceID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProvince)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(668, 43);
            this.panel1.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(94, 10);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtSearch.MaxLength = 300;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(567, 23);
            this.txtSearch.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label1.Location = new System.Drawing.Point(17, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "ค้นหารหัส";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labelRowCount);
            this.panel2.Controls.Add(this.lblQtyList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 336);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(668, 34);
            this.panel2.TabIndex = 1;
            // 
            // labelRowCount
            // 
            this.labelRowCount.AutoSize = true;
            this.labelRowCount.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.labelRowCount.Location = new System.Drawing.Point(118, 6);
            this.labelRowCount.Name = "labelRowCount";
            this.labelRowCount.Size = new System.Drawing.Size(15, 16);
            this.labelRowCount.TabIndex = 14;
            this.labelRowCount.Text = "0";
            // 
            // lblQtyList
            // 
            this.lblQtyList.AutoSize = true;
            this.lblQtyList.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lblQtyList.Location = new System.Drawing.Point(14, 6);
            this.lblQtyList.Name = "lblQtyList";
            this.lblQtyList.Size = new System.Drawing.Size(83, 16);
            this.lblQtyList.TabIndex = 13;
            this.lblQtyList.Text = "จำนวนรายการ";
            // 
            // grdProvince
            // 
            this.grdProvince.AllowUserToAddRows = false;
            this.grdProvince.AllowUserToDeleteRows = false;
            this.grdProvince.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdProvince.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProvinceCode,
            this.colProvinceName,
            this.colProvinceID});
            this.grdProvince.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProvince.Location = new System.Drawing.Point(0, 43);
            this.grdProvince.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdProvince.Name = "grdProvince";
            this.grdProvince.ReadOnly = true;
            this.grdProvince.Size = new System.Drawing.Size(668, 293);
            this.grdProvince.TabIndex = 2;
            // 
            // colProvinceCode
            // 
            this.colProvinceCode.DataPropertyName = "ProvinceCode";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.colProvinceCode.DefaultCellStyle = dataGridViewCellStyle1;
            this.colProvinceCode.HeaderText = "รหัสจังหวัด";
            this.colProvinceCode.MaxInputLength = 6;
            this.colProvinceCode.Name = "colProvinceCode";
            this.colProvinceCode.ReadOnly = true;
            this.colProvinceCode.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colProvinceCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colProvinceName
            // 
            this.colProvinceName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colProvinceName.DataPropertyName = "ProvinceName";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.colProvinceName.DefaultCellStyle = dataGridViewCellStyle2;
            this.colProvinceName.HeaderText = "ชื่อจังหวัด";
            this.colProvinceName.MaxInputLength = 50;
            this.colProvinceName.Name = "colProvinceName";
            this.colProvinceName.ReadOnly = true;
            this.colProvinceName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colProvinceName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colProvinceID
            // 
            this.colProvinceID.DataPropertyName = "ProvinceID";
            this.colProvinceID.HeaderText = "รหัส";
            this.colProvinceID.Name = "colProvinceID";
            this.colProvinceID.ReadOnly = true;
            this.colProvinceID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colProvinceID.Visible = false;
            // 
            // frmSearchProvince
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(668, 370);
            this.Controls.Add(this.grdProvince);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.Name = "frmSearchProvince";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "เลือกจังหวัด";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProvince)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelRowCount;
        private System.Windows.Forms.Label lblQtyList;
        private System.Windows.Forms.DataGridView grdProvince;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProvinceCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProvinceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProvinceID;
    }
}