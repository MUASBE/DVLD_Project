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
        private int _LicenseID = 0;
        private clcLicenseBusiness _LicenseInfo;
        public clcLicenseBusiness LicenseInfo
        {
            get { return _LicenseInfo; }
        }
        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }
        private void _ResetDefaultView()
        {
            lblClass.Text = "????";
            lblFullName.Text = "????";

            lblLicenseID.Text = "????";
            lblNationalNo.Text = "????";
            lblGendor.Text = "????";
            pbGendor.Image = Resources.Male_512;
            pbPersonImage.Image = Resources.Male_512;

            lblIssueDate.Text = "????";
            lblIssueReason.Text = "????";

            lblNotes.Text = "????";

            lblIsActive.Text = "????";
            lblIsDetained.Text = "????";
            lblDateOfBirth.Text = "????";
            lblDriverID.Text = "????";
            lblExpirationDate.Text = "????";
        }
        public void LoadLicenseInfoByApplicationID(int ApplicationID)
        {
           _LicenseInfo = clcLicenseBusiness.FindByApplicationID(ApplicationID);

            if (_LicenseInfo == null)
            {
                MessageBox.Show("Could not find License ID = " + _LicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                _ResetDefaultView();
                return;
            }

            lblClass.Text = _LicenseInfo.LicenseClassesInfo.LicenseName;
                lblFullName.Text = _LicenseInfo.ApplicationInfo.ApplicantPersonInfo.FirstName + " "
                     + _LicenseInfo.ApplicationInfo.ApplicantPersonInfo.SecondName + " "
                     + _LicenseInfo.ApplicationInfo.ApplicantPersonInfo.ThirdName + " "
                     + _LicenseInfo.ApplicationInfo.ApplicantPersonInfo.LastName;

            lblLicenseID.Text = _LicenseInfo.LicenseID.ToString();
            lblNationalNo.Text = _LicenseInfo.ApplicationInfo.ApplicantPersonInfo.NationalNo;
            
            switch(_LicenseInfo.ApplicationInfo.ApplicantPersonInfo.Gendor)
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

            if(_LicenseInfo.ApplicationInfo.ApplicantPersonInfo.imagePath != "")
            {
                pbPersonImage.ImageLocation = _LicenseInfo.ApplicationInfo.ApplicantPersonInfo.imagePath;
            }

            lblIssueDate.Text = _LicenseInfo.IssueDate.ToShortDateString();
            lblIssueReason.Text = _LicenseInfo.IssueReasonText;

            if(_LicenseInfo.Notes != "")
            {
                lblNotes.Text = _LicenseInfo.Notes.ToString();
            }
            else
            {
                lblNotes.Text = "No Notes";
            }

            lblIsActive.Text = (_LicenseInfo.IsActive) ? "Yes" : "No";
            lblDateOfBirth.Text = _LicenseInfo.ApplicationInfo.ApplicantPersonInfo.DateOfBirth.ToShortDateString();
            lblDriverID.Text = _LicenseInfo.DriverID.ToString();
            lblExpirationDate.Text = LicenseInfo.ExpirationDate.ToShortDateString();
            
        }

        public void LoadInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _LicenseInfo = clcLicenseBusiness.Find(_LicenseID);
            if (_LicenseInfo == null)
            {
                MessageBox.Show("Could not find License ID = " + _LicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                _ResetDefaultView();
                return;
            }

            lblLicenseID.Text = _LicenseInfo.LicenseID.ToString();
            lblIsActive.Text = _LicenseInfo.IsActive ? "Yes" : "No";
            //lblIsDetained.Text = _LicenseInfo.IsDetained ? "Yes" : "No";
            lblClass.Text = _LicenseInfo.LicenseClassesInfo.LicenseName;
            lblFullName.Text = _LicenseInfo.ApplicationInfo.ApplicantPersonInfo.FirstName + " "
                     + _LicenseInfo.ApplicationInfo.ApplicantPersonInfo.SecondName + " "
                     + _LicenseInfo.ApplicationInfo.ApplicantPersonInfo.ThirdName + " "
                     + _LicenseInfo.ApplicationInfo.ApplicantPersonInfo.LastName; 

            lblNationalNo.Text = _LicenseInfo.DriverInfo.PersonInfo.NationalNo;
            switch (_LicenseInfo.ApplicationInfo.ApplicantPersonInfo.Gendor)
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

            if (_LicenseInfo.ApplicationInfo.ApplicantPersonInfo.imagePath != "")
            {
                pbPersonImage.ImageLocation = _LicenseInfo.ApplicationInfo.ApplicantPersonInfo.imagePath;
            }

            lblDateOfBirth.Text = _LicenseInfo.DriverInfo.PersonInfo.DateOfBirth.ToLongDateString();

            lblDriverID.Text = _LicenseInfo.DriverID.ToString();
            lblIssueDate.Text = _LicenseInfo.IssueDate.ToLongDateString();
            lblExpirationDate.Text = _LicenseInfo.ExpirationDate.ToLongDateString();
            lblIssueReason.Text = _LicenseInfo.IssueReasonText;
            lblNotes.Text = _LicenseInfo.Notes == "" ? "No Notes" : _LicenseInfo.Notes;

            lblIsDetained.Text = (_LicenseInfo.IsLicenseDetain) ? "Yes" : "No";


        }
    }
}
