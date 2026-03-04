using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentation.License.Local_License
{
    public partial class IssueLicenseFirstTime : Form
    {
        private int _LDLApplicationID = -1;
        private clcLDLApplicationBusiness _LDLApplicationInfo;
        
        private clcLicenseBusiness LicenseInfo;
        public IssueLicenseFirstTime(int LDLApplicationID)
        {
            InitializeComponent();
            _LDLApplicationID= LDLApplicationID;  
        }

        private void IssueLicenseFirstTime_Load(object sender, EventArgs e)
        {

            txtNotes.Focus();
            _LDLApplicationInfo = clcLDLApplicationBusiness.Find(_LDLApplicationID);

            if (_LDLApplicationInfo == null)
            {

                MessageBox.Show("No Applicaiton with ID=" + _LDLApplicationID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }


            if (!_LDLApplicationInfo.isPassedAllTest())
            {

                MessageBox.Show("Person Should Pass All Tests First.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int LicenseID = clcLicenseBusiness.GetLicenseByPersonID(_LDLApplicationInfo.ApplicationInfo.ApplicantPersonID,
                _LDLApplicationInfo.LicenseInfo.LicenseID);
            if (LicenseID > 0)
            {

                MessageBox.Show("Person already has License before with License ID=" + LicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }

            ctrlLDLApplicationInfo1.LoadLDLApplicationInfo( _LDLApplicationID );
            if(ctrlLDLApplicationInfo1.LDLApplication == null )
            {
                this.Close();
            }
            
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)// under development
        {
            LicenseInfo = new clcLicenseBusiness();

            LicenseInfo.ApplicationID = ctrlLDLApplicationInfo1.LDLApplication.ApplicationID;
            LicenseInfo.ApplicationInfo = ctrlLDLApplicationInfo1.LDLApplication.ApplicationInfo;

            LicenseInfo.LicenseClassesInfo = ctrlLDLApplicationInfo1.LDLApplication.LicenseInfo;
            LicenseInfo.LicenseClassID = ctrlLDLApplicationInfo1.LDLApplication.LicenseID;

            LicenseInfo.IssueDate = DateTime.Now;
            LicenseInfo.ExpirationDate = LicenseInfo.IssueDate.AddYears(ctrlLDLApplicationInfo1.LDLApplication.LicenseInfo.DefaultValidityLength);
            
            LicenseInfo.Notes = txtNotes.Text.Trim();
            LicenseInfo.PaidFees = ctrlLDLApplicationInfo1.LDLApplication.LicenseInfo.ClassFees;
            LicenseInfo.IsActive = true;

            LicenseInfo.CreatedByUserID = clcGlobal.GolbalUser.UserID;
            LicenseInfo.CreatedByUserInfo = clcGlobal.GolbalUser;

            LicenseInfo.IssueReason = clcLicenseBusiness.enIssueReason.FirstTime;

            int DriverID = clcDriverBusiness.GetDriverIDByPersonID(ctrlLDLApplicationInfo1.LDLApplication.ApplicationInfo.ApplicantPersonID);
            if(DriverID > 0)
            {
                LicenseInfo.DriverID = DriverID;
                LicenseInfo.DriverInfo = clcDriverBusiness.FindByDriverID(DriverID);
            }
            else
            {
                LicenseInfo.DriverInfo = new clcDriverBusiness();
                LicenseInfo.DriverInfo.PersonID = ctrlLDLApplicationInfo1.LDLApplication.ApplicationInfo.ApplicantPersonID;
                LicenseInfo.DriverInfo.PersonInfo = clcPersonBusiness.Find(LicenseInfo.DriverInfo.PersonID);
                LicenseInfo.DriverInfo.CreatedByUserID = clcGlobal.GolbalUser.UserID;
                LicenseInfo.DriverInfo.CreatedByUserInfo = clcGlobal.GolbalUser;
                LicenseInfo.DriverInfo.CreatedDate = DateTime.Now;

                if (!LicenseInfo.DriverInfo.AddNewDriver())
                    return;

                LicenseInfo.DriverID = LicenseInfo.DriverInfo.DriverID;

            }

            if(LicenseInfo.AddNewLicense())
            {
                MessageBox.Show($"License issued successfully with License ID  {LicenseInfo.LicenseID}", "Successed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show($"Failing in issue License, please try again", "Fail",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
