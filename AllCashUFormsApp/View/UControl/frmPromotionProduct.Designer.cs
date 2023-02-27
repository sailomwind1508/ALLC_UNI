
namespace AllCashUFormsApp.View.UControl
{
    partial class frmPromotionProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPromotionProduct));
            this.pnlBR = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCountList = new System.Windows.Forms.Label();
            this.lblCountListText = new System.Windows.Forms.Label();
            this.colChoose = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPromotionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.pnlBR.Size = new System.Drawing.Size(665, 359);
            this.pnlBR.TabIndex = 24;
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
            this.panel2.Size = new System.Drawing.Size(641, 338);
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
            this.colProductName,
            this.colProductQty,
            this.colProductID,
            this.colPromotionID});
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdList.Name = "grdList";
            this.grdList.RowHeadersVisible = false;
            this.grdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdList.Size = new System.Drawing.Size(641, 306);
            this.grdList.TabIndex = 2;
            this.grdList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdList_CellContentClick);
            this.grdList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdList_CellDoubleClick);
            this.grdList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdList_CellEndEdit);
            this.grdList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grdList_EditingControlShowing);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Azure;
            this.panel1.Controls.Add(this.lblCountList);
            this.panel1.Controls.Add(this.lblCountListText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 306);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(641, 32);
            this.panel1.TabIndex = 18;
            // 
            // lblCountList
            // 
            this.lblCountList.AutoSize = true;
            this.lblCountList.Location = new System.Drawing.Point(96, 7);
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
            this.lblCountListText.Size = new System.Drawing.Size(102, 16);
            this.lblCountListText.TabIndex = 0;
            this.lblCountListText.Text = "จำนวนของแถม : ";
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
            // colProductName
            // 
            this.colProductName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colProductName.DataPropertyName = "ProductName";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.colProductName.DefaultCellStyle = dataGridViewCellStyle3;
            this.colProductName.HeaderText = "สินค้า";
            this.colProductName.MinimumWidth = 100;
            this.colProductName.Name = "colProductName";
            this.colProductName.ReadOnly = true;
            // 
            // colProductQty
            // 
            this.colProductQty.DataPropertyName = "ProductQty";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9.5F);
            dataGridViewCellStyle4.Format = "N0";
            this.colProductQty.DefaultCellStyle = dataGridViewCellStyle4;
            this.colProductQty.HeaderText = "จำนวน";
            this.colProductQty.Name = "colProductQty";
            this.colProductQty.ReadOnly = true;
            // 
            // colProductID
            // 
            this.colProductID.DataPropertyName = "ProductID";
            this.colProductID.HeaderText = "colProductID";
            this.colProductID.Name = "colProductID";
            this.colProductID.ReadOnly = true;
            this.colProductID.Visible = false;
            // 
            // colPromotionID
            // 
            this.colPromotionID.DataPropertyName = "PromotionID";
            this.colPromotionID.HeaderText = "colPromotionID";
            this.colPromotionID.Name = "colPromotionID";
            this.colPromotionID.ReadOnly = true;
            this.colPromotionID.Visible = false;
            // 
            // frmPromotionProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 359);
            this.Controls.Add(this.pnlBR);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "frmPromotionProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPromotionProduct";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPromotionProduct_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPromotionProduct_FormClosed);
            this.Load += new System.EventHandler(this.frmPromotionProduct_Load);
            this.pnlBR.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBR;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView grdList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCountList;
        private System.Windows.Forms.Label lblCountListText;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChoose;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPromotionID;
    }
}