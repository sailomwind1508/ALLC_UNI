using System.Windows.Forms;

namespace AllCashUFormsApp.View.Page
{
    partial class FrmAddMenu
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddMenu));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnClose = new AllCashUFormsApp.View.UControl.CloseButton(this.components);
            this.btnAdd = new AllCashUFormsApp.View.UControl.AddButton(this.components);
            this.btnExcel = new AllCashUFormsApp.View.UControl.ExcelButton(this.components);
            this.btnEdit = new AllCashUFormsApp.View.UControl.EditButton(this.components);
            this.btnPrint = new AllCashUFormsApp.View.UControl.PrintButton(this.components);
            this.btnRemove = new AllCashUFormsApp.View.UControl.RemoveButton(this.components);
            this.btnCancel = new AllCashUFormsApp.View.UControl.CancelButton(this.components);
            this.btnCopy = new AllCashUFormsApp.View.UControl.CopyButton(this.components);
            this.btnSave = new AllCashUFormsApp.View.UControl.SaveButton(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.FormPic = new System.Windows.Forms.PictureBox();
            this.FormHeader = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.MenuPicShow = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSeq = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.MenuPic = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMenuText = new System.Windows.Forms.TextBox();
            this.txtParent = new System.Windows.Forms.TextBox();
            this.txtMenuName = new System.Windows.Forms.TextBox();
            this.txtMenuID = new System.Windows.Forms.TextBox();
            this.txtPicture = new System.Windows.Forms.TextBox();
            this.txtFormName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.listViewMenu = new System.Windows.Forms.ListView();
            this.MenuID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MenuName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MenuText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MenuParent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FormName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Seq = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewPicture = new System.Windows.Forms.ListView();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormPic)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MenuPicShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MenuPic)).BeginInit();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Azure;
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 28);
            this.panel1.TabIndex = 18;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnClose);
            this.panel5.Controls.Add(this.btnAdd);
            this.panel5.Controls.Add(this.btnExcel);
            this.panel5.Controls.Add(this.btnEdit);
            this.panel5.Controls.Add(this.btnPrint);
            this.panel5.Controls.Add(this.btnRemove);
            this.panel5.Controls.Add(this.btnCancel);
            this.panel5.Controls.Add(this.btnCopy);
            this.panel5.Controls.Add(this.btnSave);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(782, 28);
            this.panel5.TabIndex = 18;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.BackColor = System.Drawing.Color.Azure;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(600, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(57, 23);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "ออก";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAdd.BackColor = System.Drawing.Color.Azure;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(7, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(56, 23);
            this.btnAdd.TabIndex = 23;
            this.btnAdd.Text = "เพิ่ม";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExcel.BackColor = System.Drawing.Color.Azure;
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.Enabled = false;
            this.btnExcel.FlatAppearance.BorderSize = 0;
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.ForeColor = System.Drawing.Color.Black;
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcel.Location = new System.Drawing.Point(485, 3);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(109, 23);
            this.btnExcel.TabIndex = 17;
            this.btnExcel.Text = "Import Excel";
            this.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcel.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEdit.BackColor = System.Drawing.Color.Azure;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.Color.Black;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(69, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(63, 23);
            this.btnEdit.TabIndex = 25;
            this.btnEdit.Text = "แก้ไข";
            this.btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPrint.BackColor = System.Drawing.Color.Azure;
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.Enabled = false;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.Black;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(418, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(61, 23);
            this.btnPrint.TabIndex = 17;
            this.btnPrint.Text = "พิมพ์";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = false;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRemove.BackColor = System.Drawing.Color.Azure;
            this.btnRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemove.Enabled = false;
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.ForeColor = System.Drawing.Color.Black;
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemove.Location = new System.Drawing.Point(138, 3);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(49, 23);
            this.btnRemove.TabIndex = 25;
            this.btnRemove.Text = "ลบ";
            this.btnRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.BackColor = System.Drawing.Color.Azure;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Enabled = false;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(344, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCopy.BackColor = System.Drawing.Color.Azure;
            this.btnCopy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCopy.Enabled = false;
            this.btnCopy.FlatAppearance.BorderSize = 0;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopy.ForeColor = System.Drawing.Color.Black;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCopy.Location = new System.Drawing.Point(193, 3);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 25;
            this.btnCopy.Text = "คัดลอก";
            this.btnCopy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCopy.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave.BackColor = System.Drawing.Color.Azure;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(274, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(64, 23);
            this.btnSave.TabIndex = 24;
            this.btnSave.Text = "บันทึก";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(0, 31);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1008, 512);
            this.panel2.TabIndex = 21;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.SystemColors.Control;
            this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
            this.panel4.Controls.Add(this.FormPic);
            this.panel4.Controls.Add(this.FormHeader);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1008, 35);
            this.panel4.TabIndex = 18;
            // 
            // FormPic
            // 
            this.FormPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FormPic.Location = new System.Drawing.Point(12, 3);
            this.FormPic.Name = "FormPic";
            this.FormPic.Size = new System.Drawing.Size(30, 30);
            this.FormPic.TabIndex = 1;
            this.FormPic.TabStop = false;
            // 
            // FormHeader
            // 
            this.FormHeader.AutoSize = true;
            this.FormHeader.BackColor = System.Drawing.Color.Azure;
            this.FormHeader.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormHeader.Location = new System.Drawing.Point(48, 8);
            this.FormHeader.Name = "FormHeader";
            this.FormHeader.Size = new System.Drawing.Size(48, 17);
            this.FormHeader.TabIndex = 0;
            this.FormHeader.Text = "label8";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Location = new System.Drawing.Point(8, 41);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(992, 462);
            this.panel3.TabIndex = 17;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Azure;
            this.panel7.Controls.Add(this.MenuPicShow);
            this.panel7.Controls.Add(this.label8);
            this.panel7.Controls.Add(this.txtSeq);
            this.panel7.Controls.Add(this.label7);
            this.panel7.Controls.Add(this.label3);
            this.panel7.Controls.Add(this.label6);
            this.panel7.Controls.Add(this.label5);
            this.panel7.Controls.Add(this.MenuPic);
            this.panel7.Controls.Add(this.label1);
            this.panel7.Controls.Add(this.txtMenuText);
            this.panel7.Controls.Add(this.txtParent);
            this.panel7.Controls.Add(this.txtMenuName);
            this.panel7.Controls.Add(this.txtMenuID);
            this.panel7.Controls.Add(this.txtPicture);
            this.panel7.Controls.Add(this.txtFormName);
            this.panel7.Controls.Add(this.label4);
            this.panel7.Controls.Add(this.txtUserID);
            this.panel7.Controls.Add(this.label2);
            this.panel7.Controls.Add(this.btnBrowse);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel7.Location = new System.Drawing.Point(665, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(327, 462);
            this.panel7.TabIndex = 21;
            // 
            // MenuPicShow
            // 
            this.MenuPicShow.Location = new System.Drawing.Point(67, 285);
            this.MenuPicShow.Name = "MenuPicShow";
            this.MenuPicShow.Size = new System.Drawing.Size(243, 172);
            this.MenuPicShow.TabIndex = 19;
            this.MenuPicShow.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(54, 196);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 16);
            this.label8.TabIndex = 18;
            this.label8.Text = "Seq : ";
            // 
            // txtSeq
            // 
            this.txtSeq.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtSeq.Location = new System.Drawing.Point(101, 195);
            this.txtSeq.MaxLength = 5;
            this.txtSeq.Name = "txtSeq";
            this.txtSeq.Size = new System.Drawing.Size(134, 23);
            this.txtSeq.TabIndex = 6;
            this.txtSeq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSeq_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(29, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 16);
            this.label7.TabIndex = 16;
            this.label7.Text = "Menu ID : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Menu Text : ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 262);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "Image : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Menu Parent : ";
            // 
            // MenuPic
            // 
            this.MenuPic.BackColor = System.Drawing.Color.Transparent;
            this.MenuPic.Location = new System.Drawing.Point(241, 191);
            this.MenuPic.Name = "MenuPic";
            this.MenuPic.Size = new System.Drawing.Size(69, 30);
            this.MenuPic.TabIndex = 14;
            this.MenuPic.TabStop = false;
            this.MenuPic.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "User ID : ";
            // 
            // txtMenuText
            // 
            this.txtMenuText.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtMenuText.Location = new System.Drawing.Point(101, 98);
            this.txtMenuText.MaxLength = 50;
            this.txtMenuText.Name = "txtMenuText";
            this.txtMenuText.Size = new System.Drawing.Size(209, 23);
            this.txtMenuText.TabIndex = 3;
            // 
            // txtParent
            // 
            this.txtParent.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtParent.Location = new System.Drawing.Point(101, 165);
            this.txtParent.MaxLength = 5;
            this.txtParent.Name = "txtParent";
            this.txtParent.Size = new System.Drawing.Size(209, 23);
            this.txtParent.TabIndex = 5;
            this.txtParent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtParent_KeyPress);
            // 
            // txtMenuName
            // 
            this.txtMenuName.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtMenuName.Location = new System.Drawing.Point(101, 66);
            this.txtMenuName.MaxLength = 50;
            this.txtMenuName.Name = "txtMenuName";
            this.txtMenuName.Size = new System.Drawing.Size(209, 23);
            this.txtMenuName.TabIndex = 2;
            // 
            // txtMenuID
            // 
            this.txtMenuID.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtMenuID.Location = new System.Drawing.Point(101, 5);
            this.txtMenuID.Name = "txtMenuID";
            this.txtMenuID.ReadOnly = true;
            this.txtMenuID.Size = new System.Drawing.Size(209, 23);
            this.txtMenuID.TabIndex = 0;
            // 
            // txtPicture
            // 
            this.txtPicture.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtPicture.Location = new System.Drawing.Point(67, 259);
            this.txtPicture.Name = "txtPicture";
            this.txtPicture.ReadOnly = true;
            this.txtPicture.Size = new System.Drawing.Size(243, 23);
            this.txtPicture.TabIndex = 8;
            // 
            // txtFormName
            // 
            this.txtFormName.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtFormName.Location = new System.Drawing.Point(101, 130);
            this.txtFormName.MaxLength = 50;
            this.txtFormName.Name = "txtFormName";
            this.txtFormName.Size = new System.Drawing.Size(209, 23);
            this.txtFormName.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Form Name : ";
            // 
            // txtUserID
            // 
            this.txtUserID.BackColor = System.Drawing.SystemColors.Window;
            this.txtUserID.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.txtUserID.Location = new System.Drawing.Point(101, 36);
            this.txtUserID.MaxLength = 5;
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(134, 23);
            this.txtUserID.TabIndex = 1;
            this.txtUserID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserID_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Menu Name : ";
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowse.Image")));
            this.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrowse.Location = new System.Drawing.Point(154, 227);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(81, 26);
            this.btnBrowse.TabIndex = 7;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Azure;
            this.panel6.Controls.Add(this.listViewMenu);
            this.panel6.Controls.Add(this.listViewPicture);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(665, 462);
            this.panel6.TabIndex = 20;
            // 
            // listViewMenu
            // 
            this.listViewMenu.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.MenuID,
            this.MenuName,
            this.MenuText,
            this.MenuParent,
            this.FormName,
            this.Seq});
            this.listViewMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.listViewMenu.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewMenu.FullRowSelect = true;
            this.listViewMenu.GridLines = true;
            this.listViewMenu.HideSelection = false;
            this.listViewMenu.Location = new System.Drawing.Point(170, 0);
            this.listViewMenu.Name = "listViewMenu";
            this.listViewMenu.Size = new System.Drawing.Size(489, 462);
            this.listViewMenu.TabIndex = 18;
            this.listViewMenu.UseCompatibleStateImageBehavior = false;
            this.listViewMenu.View = System.Windows.Forms.View.Details;
            this.listViewMenu.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // MenuID
            // 
            this.MenuID.Text = "Menu ID";
            this.MenuID.Width = 55;
            // 
            // MenuName
            // 
            this.MenuName.Text = "Menu Name";
            this.MenuName.Width = 100;
            // 
            // MenuText
            // 
            this.MenuText.Text = "Menu Text";
            this.MenuText.Width = 100;
            // 
            // MenuParent
            // 
            this.MenuParent.Text = "Menu Parent";
            this.MenuParent.Width = 75;
            // 
            // FormName
            // 
            this.FormName.Text = "FormName";
            this.FormName.Width = 100;
            // 
            // Seq
            // 
            this.Seq.Text = "Seq";
            this.Seq.Width = 50;
            // 
            // listViewPicture
            // 
            this.listViewPicture.Dock = System.Windows.Forms.DockStyle.Left;
            this.listViewPicture.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewPicture.HideSelection = false;
            this.listViewPicture.Location = new System.Drawing.Point(0, 0);
            this.listViewPicture.Name = "listViewPicture";
            this.listViewPicture.Size = new System.Drawing.Size(170, 462);
            this.listViewPicture.TabIndex = 19;
            this.listViewPicture.UseCompatibleStateImageBehavior = false;
            this.listViewPicture.View = System.Windows.Forms.View.Details;
            this.listViewPicture.SelectedIndexChanged += new System.EventHandler(this.listViewPicture_SelectedIndexChanged);
            // 
            // FrmAddMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1008, 541);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmAddMenu";
            this.Text = "frmAddMenu";
            this.Load += new System.EventHandler(this.frmAddMenu_Load);
            this.Resize += new System.EventHandler(this.FrmAddMenu_Resize);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormPic)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MenuPicShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MenuPic)).EndInit();
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.TextBox txtMenuName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMenuText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFormName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtParent;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtPicture;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox MenuPic;
        private System.Windows.Forms.TextBox txtMenuID;
        private System.Windows.Forms.Label label7;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Label FormHeader;
        private ColumnHeader MenuID;
        private ColumnHeader MenuName;
        private ColumnHeader MenuText;
        private ListView listViewMenu;
        private ListView listViewPicture;
        private ColumnHeader MenuParent;
        private Panel panel7;
        private Panel panel6;
        private ColumnHeader FormName;
        private PictureBox FormPic;
        private Label label8;
        private TextBox txtSeq;
        private ColumnHeader Seq;
        private PictureBox MenuPicShow;
        private Panel panel5;
        private UControl.CloseButton btnClose;
        private UControl.AddButton btnAdd;
        private UControl.ExcelButton btnExcel;
        private UControl.EditButton btnEdit;
        private UControl.PrintButton btnPrint;
        private UControl.RemoveButton btnRemove;
        private UControl.CancelButton btnCancel;
        private UControl.CopyButton btnCopy;
        private UControl.SaveButton btnSave;
    }
}