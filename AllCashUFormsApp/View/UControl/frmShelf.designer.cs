namespace AllCashUFormsApp.View.UControl.A_UC
{
    partial class frmShelf
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShelf));
            this.label1 = new System.Windows.Forms.Label();
            this.txtShelfNo = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSaveShelf = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "เลขที่ Shelf :";
            // 
            // txtShelfNo
            // 
            this.txtShelfNo.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtShelfNo.Location = new System.Drawing.Point(94, 6);
            this.txtShelfNo.MaxLength = 20;
            this.txtShelfNo.Name = "txtShelfNo";
            this.txtShelfNo.Size = new System.Drawing.Size(216, 23);
            this.txtShelfNo.TabIndex = 2;
            this.txtShelfNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtShelfNo_KeyPress);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(316, 38);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 28);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSaveShelf
            // 
            this.btnSaveShelf.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSaveShelf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveShelf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveShelf.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.btnSaveShelf.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveShelf.Image")));
            this.btnSaveShelf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveShelf.Location = new System.Drawing.Point(316, 7);
            this.btnSaveShelf.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSaveShelf.Name = "btnSaveShelf";
            this.btnSaveShelf.Size = new System.Drawing.Size(74, 28);
            this.btnSaveShelf.TabIndex = 16;
            this.btnSaveShelf.Text = "บันทึก";
            this.btnSaveShelf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveShelf.UseVisualStyleBackColor = false;
            this.btnSaveShelf.Click += new System.EventHandler(this.btnSaveShelf_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.label2.Location = new System.Drawing.Point(8, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 17;
            this.label2.Text = "หมายเหตุ :";
            // 
            // txtRemark
            // 
            this.txtRemark.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtRemark.Location = new System.Drawing.Point(11, 54);
            this.txtRemark.MaxLength = 500;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(299, 23);
            this.txtRemark.TabIndex = 18;
            // 
            // frmShelf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(399, 83);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveShelf);
            this.Controls.Add(this.txtShelfNo);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmShelf";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add";
            this.Load += new System.EventHandler(this.frmShelf_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtShelfNo;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSaveShelf;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRemark;
    }
}