
namespace AllCashUFormsApp.View.UControl
{
    partial class frmRecoveryPOPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRecoveryPOPopup));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dtpDocDate = new System.Windows.Forms.DateTimePicker();
            this.txtWHName = new System.Windows.Forms.TextBox();
            this.txtWHCode = new System.Windows.Forms.TextBox();
            this.btnSearchWHCode = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ddlRecoveryMode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpEdDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkAllBranch = new System.Windows.Forms.CheckBox();
            this.txtBranchCode = new System.Windows.Forms.TextBox();
            this.lblDepo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(246, 126);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 34);
            this.btnCancel.TabIndex = 34;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(144, 126);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(96, 34);
            this.btnSave.TabIndex = 33;
            this.btnSave.Text = "ปรับปรุง";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dtpDocDate
            // 
            this.dtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDocDate.Location = new System.Drawing.Point(97, 67);
            this.dtpDocDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpDocDate.Name = "dtpDocDate";
            this.dtpDocDate.Size = new System.Drawing.Size(144, 23);
            this.dtpDocDate.TabIndex = 31;
            // 
            // txtWHName
            // 
            this.txtWHName.Location = new System.Drawing.Point(243, 39);
            this.txtWHName.MaxLength = 200;
            this.txtWHName.Name = "txtWHName";
            this.txtWHName.ReadOnly = true;
            this.txtWHName.Size = new System.Drawing.Size(220, 23);
            this.txtWHName.TabIndex = 136;
            // 
            // txtWHCode
            // 
            this.txtWHCode.Location = new System.Drawing.Point(97, 39);
            this.txtWHCode.MaxLength = 5;
            this.txtWHCode.Name = "txtWHCode";
            this.txtWHCode.Size = new System.Drawing.Size(107, 23);
            this.txtWHCode.TabIndex = 134;
            this.txtWHCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWHCode_KeyDown);
            // 
            // btnSearchWHCode
            // 
            this.btnSearchWHCode.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSearchWHCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchWHCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchWHCode.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSearchWHCode.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchWHCode.Image")));
            this.btnSearchWHCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchWHCode.Location = new System.Drawing.Point(206, 39);
            this.btnSearchWHCode.Name = "btnSearchWHCode";
            this.btnSearchWHCode.Size = new System.Drawing.Size(35, 23);
            this.btnSearchWHCode.TabIndex = 135;
            this.btnSearchWHCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchWHCode.UseVisualStyleBackColor = false;
            this.btnSearchWHCode.Click += new System.EventHandler(this.btnSearchWHCode_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-63, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 16);
            this.label1.TabIndex = 133;
            this.label1.Text = "Van : ";
            // 
            // ddlRecoveryMode
            // 
            this.ddlRecoveryMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlRecoveryMode.FormattingEnabled = true;
            this.ddlRecoveryMode.Location = new System.Drawing.Point(97, 94);
            this.ddlRecoveryMode.Name = "ddlRecoveryMode";
            this.ddlRecoveryMode.Size = new System.Drawing.Size(144, 24);
            this.ddlRecoveryMode.TabIndex = 138;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label3.Location = new System.Drawing.Point(249, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 141;
            this.label3.Text = "วันที่แก้ไข :";
            // 
            // dtpEdDate
            // 
            this.dtpEdDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEdDate.Location = new System.Drawing.Point(320, 67);
            this.dtpEdDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpEdDate.Name = "dtpEdDate";
            this.dtpEdDate.Size = new System.Drawing.Size(143, 23);
            this.dtpEdDate.TabIndex = 140;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(286, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(177, 16);
            this.label5.TabIndex = 142;
            this.label5.Text = "(วันที่แก้ไขจะดึงจาก TL เท่านั้น)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label2.Location = new System.Drawing.Point(53, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 16);
            this.label2.TabIndex = 148;
            this.label2.Text = "แวน :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label4.Location = new System.Drawing.Point(31, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 149;
            this.label4.Text = "จากวันที่ :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label6.Location = new System.Drawing.Point(6, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 16);
            this.label6.TabIndex = 150;
            this.label6.Text = "ข้อมูลต้นทาง :";
            // 
            // chkAllBranch
            // 
            this.chkAllBranch.AutoSize = true;
            this.chkAllBranch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkAllBranch.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.chkAllBranch.Location = new System.Drawing.Point(370, 15);
            this.chkAllBranch.Name = "chkAllBranch";
            this.chkAllBranch.Size = new System.Drawing.Size(96, 20);
            this.chkAllBranch.TabIndex = 151;
            this.chkAllBranch.Text = "เลือกทุกศูนย์";
            this.chkAllBranch.UseVisualStyleBackColor = true;
            this.chkAllBranch.CheckedChanged += new System.EventHandler(this.chkAllBranch_CheckedChanged);
            // 
            // txtBranchCode
            // 
            this.txtBranchCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtBranchCode.Location = new System.Drawing.Point(97, 13);
            this.txtBranchCode.MaxLength = 5000;
            this.txtBranchCode.Name = "txtBranchCode";
            this.txtBranchCode.ReadOnly = true;
            this.txtBranchCode.Size = new System.Drawing.Size(267, 23);
            this.txtBranchCode.TabIndex = 153;
            this.txtBranchCode.Click += new System.EventHandler(this.txtBranchCode_Click);
            // 
            // lblDepo
            // 
            this.lblDepo.AutoSize = true;
            this.lblDepo.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lblDepo.Location = new System.Drawing.Point(49, 16);
            this.lblDepo.Name = "lblDepo";
            this.lblDepo.Size = new System.Drawing.Size(43, 16);
            this.lblDepo.TabIndex = 152;
            this.lblDepo.Text = "เดโป้ :";
            // 
            // frmRecoveryPOPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(478, 174);
            this.Controls.Add(this.txtBranchCode);
            this.Controls.Add(this.lblDepo);
            this.Controls.Add(this.chkAllBranch);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpEdDate);
            this.Controls.Add(this.ddlRecoveryMode);
            this.Controls.Add(this.txtWHName);
            this.Controls.Add(this.txtWHCode);
            this.Controls.Add(this.btnSearchWHCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dtpDocDate);
            this.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRecoveryPOPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Condition";
            this.Load += new System.EventHandler(this.frmRecoveryPOPopup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DateTimePicker dtpDocDate;
        private System.Windows.Forms.TextBox txtWHName;
        private System.Windows.Forms.TextBox txtWHCode;
        private System.Windows.Forms.Button btnSearchWHCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddlRecoveryMode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpEdDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkAllBranch;
        private System.Windows.Forms.TextBox txtBranchCode;
        private System.Windows.Forms.Label lblDepo;
    }
}