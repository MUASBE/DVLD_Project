namespace DVLD_Presentation
{
    partial class ManagePeopleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagePeopleForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNumebrOfPeople = new System.Windows.Forms.Label();
            this.DGVPeople = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showPersonInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editPersonDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletePersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewPersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findPersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUserChoice = new System.Windows.Forms.TextBox();
            this.cbUserChoice = new System.Windows.Forms.ComboBox();
            this.btnAddPerson = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVPeople)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(556, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 159);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(580, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "Manage People ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Italic);
            this.label2.Location = new System.Drawing.Point(4, 734);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "#Records:";
            // 
            // lblNumebrOfPeople
            // 
            this.lblNumebrOfPeople.AutoSize = true;
            this.lblNumebrOfPeople.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Italic);
            this.lblNumebrOfPeople.Location = new System.Drawing.Point(100, 734);
            this.lblNumebrOfPeople.Name = "lblNumebrOfPeople";
            this.lblNumebrOfPeople.Size = new System.Drawing.Size(50, 24);
            this.lblNumebrOfPeople.TabIndex = 3;
            this.lblNumebrOfPeople.Text = "????";
            // 
            // DGVPeople
            // 
            this.DGVPeople.AllowUserToAddRows = false;
            this.DGVPeople.AllowUserToDeleteRows = false;
            this.DGVPeople.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DGVPeople.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVPeople.ContextMenuStrip = this.contextMenuStrip1;
            this.DGVPeople.Location = new System.Drawing.Point(9, 376);
            this.DGVPeople.Name = "DGVPeople";
            this.DGVPeople.ReadOnly = true;
            this.DGVPeople.Size = new System.Drawing.Size(1420, 345);
            this.DGVPeople.TabIndex = 4;
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
            this.contextMenuStrip1.Size = new System.Drawing.Size(300, 184);
            // 
            // showPersonInformationToolStripMenuItem
            // 
            this.showPersonInformationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showPersonInformationToolStripMenuItem.Image")));
            this.showPersonInformationToolStripMenuItem.Name = "showPersonInformationToolStripMenuItem";
            this.showPersonInformationToolStripMenuItem.Size = new System.Drawing.Size(299, 36);
            this.showPersonInformationToolStripMenuItem.Text = "Show Person Details";
            this.showPersonInformationToolStripMenuItem.Click += new System.EventHandler(this.showPersonInformationToolStripMenuItem_Click);
            // 
            // editPersonDetailsToolStripMenuItem
            // 
            this.editPersonDetailsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("editPersonDetailsToolStripMenuItem.Image")));
            this.editPersonDetailsToolStripMenuItem.Name = "editPersonDetailsToolStripMenuItem";
            this.editPersonDetailsToolStripMenuItem.Size = new System.Drawing.Size(299, 36);
            this.editPersonDetailsToolStripMenuItem.Text = "Edit Person Details";
            this.editPersonDetailsToolStripMenuItem.Click += new System.EventHandler(this.editPersonDetailsToolStripMenuItem_Click);
            // 
            // deletePersonToolStripMenuItem
            // 
            this.deletePersonToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deletePersonToolStripMenuItem.Image")));
            this.deletePersonToolStripMenuItem.Name = "deletePersonToolStripMenuItem";
            this.deletePersonToolStripMenuItem.Size = new System.Drawing.Size(299, 36);
            this.deletePersonToolStripMenuItem.Text = "Delete Person";
            this.deletePersonToolStripMenuItem.Click += new System.EventHandler(this.deletePersonToolStripMenuItem_Click);
            // 
            // addNewPersonToolStripMenuItem
            // 
            this.addNewPersonToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addNewPersonToolStripMenuItem.Image")));
            this.addNewPersonToolStripMenuItem.Name = "addNewPersonToolStripMenuItem";
            this.addNewPersonToolStripMenuItem.Size = new System.Drawing.Size(299, 36);
            this.addNewPersonToolStripMenuItem.Text = "Add NewPerson";
            this.addNewPersonToolStripMenuItem.Click += new System.EventHandler(this.addNewPersonToolStripMenuItem_Click);
            // 
            // findPersonToolStripMenuItem
            // 
            this.findPersonToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("findPersonToolStripMenuItem.Image")));
            this.findPersonToolStripMenuItem.Name = "findPersonToolStripMenuItem";
            this.findPersonToolStripMenuItem.Size = new System.Drawing.Size(299, 36);
            this.findPersonToolStripMenuItem.Text = "Find Person";
            this.findPersonToolStripMenuItem.Click += new System.EventHandler(this.findPersonToolStripMenuItem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 332);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Filter By: ";
            // 
            // txtUserChoice
            // 
            this.txtUserChoice.Location = new System.Drawing.Point(351, 332);
            this.txtUserChoice.Multiline = true;
            this.txtUserChoice.Name = "txtUserChoice";
            this.txtUserChoice.Size = new System.Drawing.Size(166, 21);
            this.txtUserChoice.TabIndex = 7;
            this.txtUserChoice.TextChanged += new System.EventHandler(this.txtUserChoice_TextChanged);
            this.txtUserChoice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserChoice_KeyPress);
            // 
            // cbUserChoice
            // 
            this.cbUserChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUserChoice.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUserChoice.FormattingEnabled = true;
            this.cbUserChoice.Items.AddRange(new object[] {
            "None",
            "Person ID",
            "National No",
            "First Name",
            "Second Name",
            "Third Name",
            "Last Name",
            "Gendor",
            "Phone",
            "Email",
            "Country Name"});
            this.cbUserChoice.Location = new System.Drawing.Point(110, 331);
            this.cbUserChoice.Name = "cbUserChoice";
            this.cbUserChoice.Size = new System.Drawing.Size(228, 24);
            this.cbUserChoice.TabIndex = 8;
            this.cbUserChoice.SelectedIndexChanged += new System.EventHandler(this.cbUserChoice_SelectedIndexChanged);
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPerson.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAddPerson.Image = ((System.Drawing.Image)(resources.GetObject("btnAddPerson.Image")));
            this.btnAddPerson.Location = new System.Drawing.Point(1349, 314);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(73, 49);
            this.btnAddPerson.TabIndex = 9;
            this.btnAddPerson.UseVisualStyleBackColor = true;
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // ManagePeopleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1434, 761);
            this.Controls.Add(this.btnAddPerson);
            this.Controls.Add(this.cbUserChoice);
            this.Controls.Add(this.txtUserChoice);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DGVPeople);
            this.Controls.Add(this.lblNumebrOfPeople);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "ManagePeopleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage People";
            this.Load += new System.EventHandler(this.ManagePeopleForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVPeople)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNumebrOfPeople;
        private System.Windows.Forms.DataGridView DGVPeople;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUserChoice;
        private System.Windows.Forms.ComboBox cbUserChoice;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showPersonInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editPersonDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletePersonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewPersonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findPersonToolStripMenuItem;
    }
}