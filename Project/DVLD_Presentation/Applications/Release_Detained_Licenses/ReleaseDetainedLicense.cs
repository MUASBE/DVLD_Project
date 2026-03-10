using DVLD_Business;
using DVLD_Presentation.License;
using DVLD_Presentation.License.Local_License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentation.Applications.Release_Detained_Licenses
{
    public partial class ReleaseDetainedLicense : Form
    {
        private int _DetainID = 0;

        private void _SetDefaultView()
        {
            lblDetainID.Text = "????";
            lblDetainDate.Text = DateTime.Now.ToLongDateString();
            lblFineFees.Text = "????";
            lblLicenseID.Text = (ctrlDriverLicenseInfoWithFilter1.LicenseInfo != null) ?
                ctrlDriverLicenseInfoWithFilter1.LicenseInfo.LicenseID.ToString() : "????";
            lblCreatedByUser.Text = clcGlobal.GolbalUser.UserName;
            lblApplicationID.Text = "????";
            lblApplicationFees.Text = clcApplicationTypesBusiness.GetApplicationTypeFees((int)
                clcApplicationBusiness.enApplicationType.ReleaseDetainedDrivingLicsense).ToString();
            lblTotalFees.Text = lblApplicationFees.Text;
            
            llShowLicenseHistory.Enabled = (ctrlDriverLicenseInfoWithFilter1.LicenseInfo != null);
            llShowLicenseInfo.Enabled = false;
            btnIssueRelease.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.TabIndex = 0;
            ctrlDriverLicenseInfoWithFilter1.FoucestxtLicense();
        }

        public ReleaseDetainedLicense()
        {
            InitializeComponent();
        }

        private void ReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
            _SetDefaultView();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseClick(int obj)
        {
            int SelectedLicenseID = obj;

            if (SelectedLicenseID <= 0)
            {
                _SetDefaultView();
                return;
            }

            _DetainID = clcDetainLicensesBusiness.GetDetainIDForLicense(ctrlDriverLicenseInfoWithFilter1.LicenseInfo.LicenseID);
            if (_DetainID <= 0)
            {
                MessageBox.Show($"License is not detained, please select another one", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _SetDefaultView();
                return;
            }

            lblLicenseID.Text = ctrlDriverLicenseInfoWithFilter1.LicenseInfo.LicenseID.ToString();
            lblDetainID.Text = _DetainID.ToString();
            lblFineFees.Text = ctrlDriverLicenseInfoWithFilter1.LicenseInfo.GetFineFeesForDetainedLicense.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblFineFees.Text) + 
                Convert.ToSingle(lblApplicationFees.Text)).ToString();
            llShowLicenseHistory.Enabled = true;
            llShowLicenseInfo.Enabled = true;
            btnIssueRelease.Enabled = true;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnIssueRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Release license", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                == DialogResult.OK)
            {
                int ApplicationID = ctrlDriverLicenseInfoWithFilter1.LicenseInfo.ReleaseDetainedLicense(_DetainID, clcGlobal.GolbalUser.UserID);

                if(ApplicationID > 0)
                {
                    lblApplicationID.Text = ApplicationID.ToString();
                    MessageBox.Show("License was Released successfully", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                    ctrlDriverLicenseInfoWithFilter1.LoadLicenseDataAgain();

                    btnIssueRelease.Enabled = false;
                    llShowLicenseHistory.Enabled = true;
                    llShowLicenseInfo.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Error on Releasing license, please try again leter", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ctrlDriverLicenseInfoWithFilter1.LicenseInfo != null)
            {
                LicensesHistory frm = new LicensesHistory(ctrlDriverLicenseInfoWithFilter1.LicenseInfo.DriverInfo.PersonID);

                frm.ShowDialog();
            }
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ctrlDriverLicenseInfoWithFilter1.LicenseInfo != null)
            {
                ShowLicenseInfo frm = new ShowLicenseInfo(ctrlDriverLicenseInfoWithFilter1.LicenseInfo.LicenseID);

                frm.ShowDialog();
            }
        }
    }
}
