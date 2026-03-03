namespace DVLD_Presentation
{
    partial class CtrlShowUserInfo
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
            this.ctrlShowPersonDetails1 = new DVLD_Presentation.CtrlShowPersonDetails();
            this.GBLoginInfo = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.lblIsActive = new System.Windows.Forms.Label();
            this.GBLoginInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlShowPersonDetails1
            // 
            this.ctrlShowPersonDetails1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlShowPersonDetails1.Location = new System.Drawing.Point(0, 0);
            this.ctrlShowPersonDetails1.Name = "ctrlShowPersonDetails1";
            this.ctrlShowPersonDetails1.Size = new System.Drawing.Size(859, 401);
            this.ctrlShowPersonDetails1.TabIndex = 0;
            // 
            // GBLoginInfo
            // 
            this.GBLoginInfo.Controls.Add(this.lblIsActive);
            this.GBLoginInfo.Controls.Add(this.lblUserID);
            this.GBLoginInfo.Controls.Add(this.lblUserName);
            this.GBLoginInfo.Controls.Add(this.label4);
            this.GBLoginInfo.Controls.Add(this.label3);
            this.GBLoginInfo.Controls.Add(this.label1);
            this.GBLoginInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GBLoginInfo.Font = new System.Drawing.Font("Microsoft Uighur", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBLoginInfo.Location = new System.Drawing.Point(0, 430);
            this.GBLoginInfo.Name = "GBLoginInfo";
            this.GBLoginInfo.Size = new System.Drawing.Size(859, 132);
            this.GBLoginInfo.TabIndex = 1;
            this.GBLoginInfo.TabStop = false;
            this.GBLoginInfo.Text = "Login Information";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Uighur", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Name : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Uighur", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(649, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 32);
            this.label3.TabIndex = 2;
            this.label3.Text = "Is Active : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Uighur", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(332, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 32);
            this.label4.TabIndex = 3;
            this.label4.Text = "User ID : ";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Microsoft New Tai Lue", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.ForeColor = System.Drawing.Color.Red;
            this.lblUserName.Location = new System.Drawing.Point(134, 56);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(48, 27);
            this.lblUserName.TabIndex = 4;
            this.lblUserName.Text = "????";
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("Microsoft New Tai Lue", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserID.ForeColor = System.Drawing.Color.Red;
            this.lblUserID.Location = new System.Drawing.Point(418, 57);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(48, 27);
            this.lblUserID.TabIndex = 5;
            this.lblUserID.Text = "????";
            // 
            // lblIsActive
            // 
            this.lblIsActive.AutoSize = true;
            this.lblIsActive.Font = new System.Drawing.Font("Microsoft New Tai Lue", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIsActive.ForeColor = System.Drawing.Color.Red;
            this.lblIsActive.Location = new System.Drawing.Point(731, 56);
            this.lblIsActive.Name = "lblIsActive";
            this.lblIsActive.Size = new System.Drawing.Size(48, 27);
            this.lblIsActive.TabIndex = 6;
            this.lblIsActive.Text = "????";
            // 
            // CtrlShowUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GBLoginInfo);
            this.Controls.Add(this.ctrlShowPersonDetails1);
            this.Name = "CtrlShowUserInfo";
            this.Size = new System.Drawing.Size(859, 562);
            this.GBLoginInfo.ResumeLayout(false);
            this.GBLoginInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CtrlShowPersonDetails ctrlShowPersonDetails1;
        private System.Windows.Forms.GroupBox GBLoginInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblIsActive;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Label lblUserName;
    }
}
