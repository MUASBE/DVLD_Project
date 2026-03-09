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

namespace DVLD_Presentation.License.International_license.Control
{
    public partial class ctrlShowInternationalLicenseInfo : UserControl
    {
        private int _LicenseID = 0;
        private clcInternationalLicenseBusiness _LicenseInfo;
        public clcInternationalLicenseBusiness LicenseInfo
        {
            get { return _LicenseInfo; }
        }
        private void _ResetDefaultView()
        {
            lblFullName.Text = "????";

            lblLicenseID.Text = "????";
            lblNationalNo.Text = "????";
            lblGendor.Text = "????";
            pbGendor.Image = Resources.Male_512;
            pbPersonImage.Image = Resources.Male_512;

            lblIssueDate.Text = "????";
            lblExpirationDate.Text = "????";

            lblInternationalLicenseID.Text = "????";

            lblIsActive.Text = "????";
            lblDateOfBirth.Text = "????";
            lblDriverID.Text = "????";
            lblAppplicationID.Text = "????";
        }
        public ctrlShowInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        public void LoadInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _LicenseInfo = clcInternationalLicenseBusiness.FindByInternationalLicenseID(_LicenseID);
            if (_LicenseInfo == null)
            {
                MessageBox.Show("Could not find License ID = " + _LicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                _ResetDefaultView();
                return;
            }

            lblFullName.Text = _LicenseInfo.ApplicationInfo.ApplicantPersonInfo.FirstName + " "
                     + _LicenseInfo.ApplicationInfo.ApplicantPersonInfo.SecondName + " "
                     + _LicenseInfo.ApplicationInfo.ApplicantPersonInfo.ThirdName + " "
                     + _LicenseInfo.ApplicationInfo.ApplicantPersonInfo.LastName;

            lblInternationalLicenseID.Text = _LicenseInfo.InternationalLicenseID.ToString();
            lblLicenseID.Text = _LicenseInfo.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text = _LicenseInfo.ApplicationInfo.ApplicantPersonInfo.NationalNo;
            lblGendor.Text = (_LicenseInfo.ApplicationInfo.ApplicantPersonInfo.Gendor == 0) ?  "Male" : "Female";
            pbGendor.Image = (_LicenseInfo.ApplicationInfo.ApplicantPersonInfo.Gendor == 0) ? Resources.Male_512 : Resources.Female_512;
            pbPersonImage.Image = (_LicenseInfo.ApplicationInfo.ApplicantPersonInfo.Gendor == 0) ? Resources.Male_512 : Resources.Female_512;
            lblIssueDate.Text = _LicenseInfo.IssueDate.ToString();
            lblIsActive.Text = (_LicenseInfo.IsActive) ? "Yes" : "No";
            lblDateOfBirth.Text = _LicenseInfo.ApplicationInfo.ApplicantPersonInfo.DateOfBirth.ToString();
            lblDriverID.Text = _LicenseInfo.DriverID.ToString();
            lblExpirationDate.Text = _LicenseInfo.ExpirationDate.ToString();
            lblAppplicationID.Text = _LicenseInfo.ApplicationID.ToString();

            if(_LicenseInfo.ApplicationInfo.ApplicantPersonInfo.imagePath != "")
            {
                pbPersonImage.ImageLocation = _LicenseInfo.ApplicationInfo.ApplicantPersonInfo.imagePath;
            }

        }
    }
}
