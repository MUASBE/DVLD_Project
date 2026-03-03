namespace DVLD_Presentation
{
    partial class CtrlShowPersonDetailsWithFilter
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlShowPersonDetailsWithFilter));
            this.cbUserChoice = new System.Windows.Forms.ComboBox();
            this.txtUserChoice = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.GbFilter = new System.Windows.Forms.GroupBox();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.btnFindPerson = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.ctrlShowPersonDetails1 = new DVLD_Presentation.CtrlShowPersonDetails();
            this.GbFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbUserChoice
            // 
            this.cbUserChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUserChoice.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUserChoice.FormattingEnabled = true;
            this.cbUserChoice.Items.AddRange(new object[] {
            "Person ID",
            "National No"});
            this.cbUserChoice.Location = new System.Drawing.Point(95, 43);
            this.cbUserChoice.Name = "cbUserChoice";
            this.cbUserChoice.Size = new System.Drawing.Size(228, 24);
            this.cbUserChoice.TabIndex = 11;
            this.cbUserChoice.SelectedIndexChanged += new System.EventHandler(this.cbUserChoice_SelectedIndexChanged);
            // 
            // txtUserChoice
            // 
            this.txtUserChoice.Location = new System.Drawing.Point(338, 42);
            this.txtUserChoice.Multiline = true;
            this.txtUserChoice.Name = "txtUserChoice";
            this.txtUserChoice.Size = new System.Drawing.Size(166, 25);
            this.txtUserChoice.TabIndex = 10;
            this.txtUserChoice.TextChanged += new System.EventHandler(this.txtUserChoice_TextChanged_1);
            this.txtUserChoice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserChoice_KeyPress);
            this.txtUserChoice.Validating += new System.ComponentModel.CancelEventHandler(this.txtUserChoice_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Find By:";
            // 
            // GbFilter
            // 
            this.GbFilter.Controls.Add(this.btnAddPerson);
            this.GbFilter.Controls.Add(this.label3);
            this.GbFilter.Controls.Add(this.btnFindPerson);
            this.GbFilter.Controls.Add(this.cbUserChoice);
            this.GbFilter.Controls.Add(this.txtUserChoice);
            this.GbFilter.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GbFilter.Location = new System.Drawing.Point(10, 17);
            this.GbFilter.Name = "GbFilter";
            this.GbFilter.Size = new System.Drawing.Size(636, 78);
            this.GbFilter.TabIndex = 12;
            this.GbFilter.TabStop = false;
            this.GbFilter.Text = "Filter";
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPerson.Image = ((System.Drawing.Image)(resources.GetObject("btnAddPerson.Image")));
            this.btnAddPerson.Location = new System.Drawing.Point(586, 36);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(39, 37);
            this.btnAddPerson.TabIndex = 14;
            this.btnAddPerson.UseVisualStyleBackColor = true;
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // btnFindPerson
            // 
            this.btnFindPerson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindPerson.Image = ((System.Drawing.Image)(resources.GetObject("btnFindPerson.Image")));
            this.btnFindPerson.Location = new System.Drawing.Point(529, 36);
            this.btnFindPerson.Name = "btnFindPerson";
            this.btnFindPerson.Size = new System.Drawing.Size(43, 36);
            this.btnFindPerson.TabIndex = 13;
            this.btnFindPerson.UseVisualStyleBackColor = true;
            this.btnFindPerson.Click += new System.EventHandler(this.btnFindPerson_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ctrlShowPersonDetails1
            // 
            this.ctrlShowPersonDetails1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctrlShowPersonDetails1.Location = new System.Drawing.Point(0, 105);
            this.ctrlShowPersonDetails1.Name = "ctrlShowPersonDetails1";
            this.ctrlShowPersonDetails1.Size = new System.Drawing.Size(846, 433);
            this.ctrlShowPersonDetails1.TabIndex = 0;
            // 
            // CtrlShowPersonDetailsWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GbFilter);
            this.Controls.Add(this.ctrlShowPersonDetails1);
            this.Name = "CtrlShowPersonDetailsWithFilter";
            this.Size = new System.Drawing.Size(846, 538);
            this.Load += new System.EventHandler(this.CtrlShowPersonDetailsWithFilter_Load);
            this.GbFilter.ResumeLayout(false);
            this.GbFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CtrlShowPersonDetails ctrlShowPersonDetails1;
        private System.Windows.Forms.ComboBox cbUserChoice;
        private System.Windows.Forms.TextBox txtUserChoice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox GbFilter;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.Button btnFindPerson;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
