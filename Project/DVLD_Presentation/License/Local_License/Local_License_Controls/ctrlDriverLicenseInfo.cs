using DVLD_Business;
using DVLD_Presentation.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentation.License.Local_License.Local_License_Controls
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        public clcLicenseBusiness LicenseInfo { get; set; }       
        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }

        public void LoadLicenseInfoByApplicationID(int ApplicationID)
        {
            LicenseInfo = clcLicenseBusiness.FindByApplicationID(ApplicationID);

            if(LicenseInfo != null )
            {
                lblClass.Text = LicenseInfo.LicenseClassesInfo.LicenseName;
                lblFullName.Text = LicenseInfo.ApplicationInfo.ApplicantPersonInfo.FirstName + " "
                     + LicenseInfo.ApplicationInfo.ApplicantPersonInfo.SecondName + " "
                     + LicenseInfo.ApplicationInfo.ApplicantPersonInfo.ThirdName + " "
                     + LicenseInfo.ApplicationInfo.ApplicantPersonInfo.LastName;

                lblLicenseID.Text = LicenseInfo.LicenseID.ToString();
                lblNationalNo.Text = LicenseInfo.ApplicationInfo.ApplicantPersonInfo.NationalNo;
                
                switch(LicenseInfo.ApplicationInfo.ApplicantPersonInfo.Gendor)
                {
                    case 0:
                        {
                            lblGendor.Text = "Male";
                            pbGendor.Image = Resources.Male_512;
                            pbPersonImage.Image = Resources.Male_512;
                            break;
                        }
                    case 1:
                        {
                            pbGendor.Image = Resources.Female_512;
                            pbPersonImage.Image = Resources.Female_512; 
                            lblGendor.Text = "Female";
                            break;
                        }
                }

                if(LicenseInfo.ApplicationInfo.ApplicantPersonInfo.imagePath != "")
                {
                    pbPersonImage.ImageLocation = LicenseInfo.ApplicationInfo.ApplicantPersonInfo.imagePath;
                }

                lblIssueDate.Text = LicenseInfo.IssueDate.ToShortDateString();
                lblIssueReason.Text = LicenseInfo.IssueReasonText;

                if(LicenseInfo.Notes != "")
                {
                    lblNotes.Text = LicenseInfo.Notes.ToString();
                }
                else
                {
                    lblNotes.Text = "No Notes";
                }

                lblIsActive.Text = (LicenseInfo.IsActive) ? "Yes" : "No";
                lblDateOfBirth.Text = LicenseInfo.ApplicationInfo.ApplicantPersonInfo.DateOfBirth.ToShortDateString();
                lblDriverID.Text = LicenseInfo.DriverID.ToString();
                lblExpirationDate.Text = LicenseInfo.ExpirationDate.ToShortDateString();
            }
        }
    }
}
