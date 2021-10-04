namespace AllCashUFormsApp.View.Page
{
    partial class frmShelfPopup
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
            this.pnlBR = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSaveShelfCode = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txtShelfID = new System.Windows.Forms.TextBox();
            this.pnlBR.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.pnlBR.Size = new System.Drawing.Size(506, 59);
            this.pnlBR.TabIndex = 24;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Azure;
            this.panel2.Controls.Add(this.btnSaveShelfCode);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.txtShelfID);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(482, 38);
            this.panel2.TabIndex = 19;
            // 
            // btnSaveShelfCode
            // 
            this.btnSaveShelfCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveShelfCode.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnSaveShelfCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveShelfCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveShelfCode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSaveShelfCode.Image = global::AllCashUFormsApp.Properties.Resources.saveBtn;
            this.btnSaveShelfCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveShelfCode.Location = new System.Drawing.Point(407, 5);
            this.btnSaveShelfCode.Name = "btnSaveShelfCode";
            this.btnSaveShelfCode.Size = new System.Drawing.Size(70, 26);
            this.btnSaveShelfCode.TabIndex = 2;
            this.btnSaveShelfCode.Text = "บันทึก";
            this.btnSaveShelfCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveShelfCode.UseVisualStyleBackColor = false;
            this.btnSaveShelfCode.Click += new System.EventHandler(this.btnSaveShelfCode_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 16);
            this.label13.TabIndex = 0;
            this.label13.Text = "รหัส Shelf";
            // 
            // txtShelfID
            // 
            this.txtShelfID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtShelfID.Location = new System.Drawing.Point(70, 7);
            this.txtShelfID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtShelfID.MaxLength = 50;
            this.txtShelfID.Name = "txtShelfID";
            this.txtShelfID.Size = new System.Drawing.Size(333, 23);
            this.txtShelfID.TabIndex = 1;
            // 
            // frmShelfPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 59);
            this.ControlBox = false;
            this.Controls.Add(this.pnlBR);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmShelfPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmShelfPopup";
            this.Load += new System.EventHandler(this.frmShelfPopup_Load);
            this.pnlBR.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlBR;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSaveShelfCode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtShelfID;
    }
}