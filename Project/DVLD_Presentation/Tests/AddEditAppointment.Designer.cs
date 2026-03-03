namespace DVLD_Presentation
{
    partial class AddEditAppointment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEditAppointment));
            this.ctrllScheduleTest1 = new DVLD_Presentation.CtrllScheduleTest();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrllScheduleTest1
            // 
            this.ctrllScheduleTest1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrllScheduleTest1.Location = new System.Drawing.Point(0, 0);
            this.ctrllScheduleTest1.Name = "ctrllScheduleTest1";
            this.ctrllScheduleTest1.Size = new System.Drawing.Size(577, 706);
            this.ctrllScheduleTest1.TabIndex = 0;
            this.ctrllScheduleTest1.TestTypeID = DVLD_Business.clcTestTypeBusiness._enTestType.VisionTest;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Location = new System.Drawing.Point(292, 712);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(115, 46);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // AddEditAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 760);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrllScheduleTest1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddEditAppointment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddEditAppointment";
            this.Load += new System.EventHandler(this.AddEditAppointment_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CtrllScheduleTest ctrllScheduleTest1;
        private System.Windows.Forms.Button btnClose;
    }
}