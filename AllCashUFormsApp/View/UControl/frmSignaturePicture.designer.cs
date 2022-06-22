
namespace AllCashUFormsApp.View.UControl
{
    partial class frmSignaturePicture
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSignaturePicture));
            this.picSignature = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picSignature)).BeginInit();
            this.SuspendLayout();
            // 
            // picSignature
            // 
            this.picSignature.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picSignature.Location = new System.Drawing.Point(0, 0);
            this.picSignature.Name = "picSignature";
            this.picSignature.Size = new System.Drawing.Size(528, 368);
            this.picSignature.TabIndex = 273;
            this.picSignature.TabStop = false;
            // 
            // frmSignaturePicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 368);
            this.Controls.Add(this.picSignature);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmSignaturePicture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "รูปลายเซ็น";
            this.Load += new System.EventHandler(this.frmSignaturePicture_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picSignature)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picSignature;
    }
}