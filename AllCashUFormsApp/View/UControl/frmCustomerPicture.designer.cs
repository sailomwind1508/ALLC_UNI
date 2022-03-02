
namespace AllCashUFormsApp.View.UControl
{
    partial class frmCustomerPicture
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
            this.picCustomerImg = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picCustomerImg)).BeginInit();
            this.SuspendLayout();
            // 
            // picCustomerImg
            // 
            this.picCustomerImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picCustomerImg.Location = new System.Drawing.Point(0, 0);
            this.picCustomerImg.Name = "picCustomerImg";
            this.picCustomerImg.Size = new System.Drawing.Size(528, 368);
            this.picCustomerImg.TabIndex = 273;
            this.picCustomerImg.TabStop = false;
            // 
            // frmCustomerPicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 368);
            this.Controls.Add(this.picCustomerImg);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmCustomerPicture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "รูปภาพร้านค้า";
            this.Load += new System.EventHandler(this.frmCustomerPicture_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCustomerImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picCustomerImg;
    }
}