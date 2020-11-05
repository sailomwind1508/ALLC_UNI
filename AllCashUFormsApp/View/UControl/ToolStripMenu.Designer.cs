namespace AllCashUFormsApp.View.UserControl
{
    partial class ToolStripMenu
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.all_menuStrip = new System.Windows.Forms.MenuStrip();
            this.SuspendLayout();
            // 
            // all_menuStrip
            // 
            this.all_menuStrip.Location = new System.Drawing.Point(0, 0);
            this.all_menuStrip.Name = "all_menuStrip";
            this.all_menuStrip.Size = new System.Drawing.Size(150, 24);
            this.all_menuStrip.TabIndex = 0;
            this.all_menuStrip.Text = "menuStrip1";
            // 
            // ToolStripMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.all_menuStrip);
            this.Name = "ToolStripMenu";
            this.Load += new System.EventHandler(this.ToolStripMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip all_menuStrip;
    }
}
