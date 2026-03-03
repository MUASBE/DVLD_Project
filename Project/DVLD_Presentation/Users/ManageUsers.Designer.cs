namespace DVLD_Presentation
{
    partial class ManageUsers
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageUsers));
            this.DGVUsers = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showPersonInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editPersonDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletePersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewPersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findPersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.cbUserChoice = new System.Windows.Forms.ComboBox();
            this.txtUserChoice = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblNumebrOfUser = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbIsActiveChoice = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVUsers)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVUsers
            // 
            this.DGVUsers.AllowUserToAddRows = false;
            this.DGVUsers.AllowUserToDeleteRows = false;
            this.DGVUsers.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DGVUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVUsers.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGVUsers.DefaultCellStyle = dataGridViewCellStyle5;
            this.DGVUsers.Location = new System.Drawing.Point(12, 235);
            this.DGVUsers.Name = "DGVUsers";
            this.DGVUsers.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVUsers.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.DGVUsers.Size = new System.Drawing.Size(932, 387);
            this.DGVUsers.TabIndex = 5;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showPersonInformationToolStripMenuItem,
            this.editPersonDetailsToolStripMenuItem,
            this.deletePersonToolStripMenuItem,
            this.addNewPersonToolStripMenuItem,
            this.findPersonToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(277, 184);
            // 
            // showPersonInformationToolStripMenuItem
            // 
            this.showPersonInformationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showPersonInformationToolStripMenuItem.Image")));
            this.showPersonInformationToolStripMenuItem.Name = "showPersonInformationToolStripMenuItem";
            this.showPersonInformationToolStripMenuItem.Size = new System.Drawing.Size(276, 36);
            this.showPersonInformationToolStripMenuItem.Text = "Show User Details";
            this.showPersonInformationToolStripMenuItem.Click += new System.EventHandler(this.showPersonInformationToolStripMenuItem_Click);
            // 
            // editPersonDetailsToolStripMenuItem
            // 
            this.editPersonDetailsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("editPersonDetailsToolStripMenuItem.Image")));
            this.editPersonDetailsToolStripMenuItem.Name = "editPersonDetailsToolStripMenuItem";
            this.editPersonDetailsToolStripMenuItem.Size = new System.Drawing.Size(276, 36);
            this.editPersonDetailsToolStripMenuItem.Text = "Edit User Details";
            this.editPersonDetailsToolStripMenuItem.Click += new System.EventHandler(this.editPersonDetailsToolStripMenuItem_Click);
            // 
            // deletePersonToolStripMenuItem
            // 
            this.deletePersonToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deletePersonToolStripMenuItem.Image")));
            this.deletePersonToolStripMenuItem.Name = "deletePersonToolStripMenuItem";
            this.deletePersonToolStripMenuItem.Size = new System.Drawing.Size(276, 36);
            this.deletePersonToolStripMenuItem.Text = "Delete User";
            this.deletePersonToolStripMenuItem.Click += new System.EventHandler(this.deletePersonToolStripMenuItem_Click);
            // 
            // addNewPersonToolStripMenuItem
            // 
            this.addNewPersonToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addNewPersonToolStripMenuItem.Image")));
            this.addNewPersonToolStripMenuItem.Name = "addNewPersonToolStripMenuItem";
            this.addNewPersonToolStripMenuItem.Size = new System.Drawing.Size(276, 36);
            this.addNewPersonToolStripMenuItem.Text = "Add New User";
            this.addNewPersonToolStripMenuItem.Click += new System.EventHandler(this.addNewPersonToolStripMenuItem_Click);
            // 
            // findPersonToolStripMenuItem
            // 
            this.findPersonToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("findPersonToolStripMenuItem.Image")));
            this.findPersonToolStripMenuItem.Name = "findPersonToolStripMenuItem";
            this.findPersonToolStripMenuItem.Size = new System.Drawing.Size(276, 36);
            this.findPersonToolStripMenuItem.Text = "Change Password";
            this.findPersonToolStripMenuItem.Click += new System.EventHandler(this.findPersonToolStripMenuItem_Click);
            // 
            // btnAddUser
            // 
            this.btnAddUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddUser.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAddUser.Image = ((System.Drawing.Image)(resources.GetObject("btnAddUser.Image")));
            this.btnAddUser.Location = new System.Drawing.Point(877, 173);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(67, 56);
            this.btnAddUser.TabIndex = 15;
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // cbUserChoice
            // 
            this.cbUserChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUserChoice.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUserChoice.FormattingEnabled = true;
            this.cbUserChoice.Items.AddRange(new object[] {
            "None",
            "User ID",
            "Person ID",
            "Full Name",
            "User Name",
            "Is Active"});
            this.cbUserChoice.Location = new System.Drawing.Point(110, 191);
            this.cbUserChoice.Name = "cbUserChoice";
            this.cbUserChoice.Size = new System.Drawing.Size(114, 24);
            this.cbUserChoice.TabIndex = 14;
            this.cbUserChoice.SelectedIndexChanged += new System.EventHandler(this.cbUserChoice_SelectedIndexChanged);
            // 
            // txtUserChoice
            // 
            this.txtUserChoice.Location = new System.Drawing.Point(237, 192);
            this.txtUserChoice.Multiline = true;
            this.txtUserChoice.Name = "txtUserChoice";
            this.txtUserChoice.Size = new System.Drawing.Size(166, 21);
            this.txtUserChoice.TabIndex = 13;
            this.txtUserChoice.Visible = false;
            this.txtUserChoice.TextChanged += new System.EventHandler(this.txtUserChoice_TextChanged);
            this.txtUserChoice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserChoice_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(382, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 30);
            this.label1.TabIndex = 11;
            this.label1.Text = "Manage Users";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(361, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(210, 159);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 25);
            this.label3.TabIndex = 16;
            this.label3.Text = "Filter By: ";
            // 
            // lblNumebrOfUser
            // 
            this.lblNumebrOfUser.AutoSize = true;
            this.lblNumebrOfUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Italic);
            this.lblNumebrOfUser.Location = new System.Drawing.Point(115, 647);
            this.lblNumebrOfUser.Name = "lblNumebrOfUser";
            this.lblNumebrOfUser.Size = new System.Drawing.Size(50, 24);
            this.lblNumebrOfUser.TabIndex = 18;
            this.lblNumebrOfUser.Text = "????";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Italic);
            this.label2.Location = new System.Drawing.Point(19, 647);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 24);
            this.label2.TabIndex = 17;
            this.label2.Text = "#Records:";
            // 
            // cbIsActiveChoice
            // 
            this.cbIsActiveChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIsActiveChoice.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIsActiveChoice.FormattingEnabled = true;
            this.cbIsActiveChoice.Items.AddRange(new object[] {
            "All",
            "YES",
            "NO"});
            this.cbIsActiveChoice.Location = new System.Drawing.Point(237, 190);
            this.cbIsActiveChoice.Name = "cbIsActiveChoice";
            this.cbIsActiveChoice.Size = new System.Drawing.Size(114, 24);
            this.cbIsActiveChoice.TabIndex = 20;
            this.cbIsActiveChoice.Visible = false;
            this.cbIsActiveChoice.SelectedIndexChanged += new System.EventHandler(this.cbIsActiveChoice_SelectedIndexChanged);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Location = new System.Drawing.Point(830, 641);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(115, 51);
            this.btnClose.TabIndex = 21;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ManageUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(957, 694);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cbIsActiveChoice);
            this.Controls.Add(this.lblNumebrOfUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.cbUserChoice);
            this.Controls.Add(this.txtUserChoice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.DGVUsers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ManageUsers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Users";
            this.Load += new System.EventHandler(this.ManageUsers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVUsers)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVUsers;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.ComboBox cbUserChoice;
        private System.Windows.Forms.TextBox txtUserChoice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNumebrOfUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showPersonInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editPersonDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletePersonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewPersonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findPersonToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbIsActiveChoice;
        private System.Windows.Forms.Button btnClose;
    }
}