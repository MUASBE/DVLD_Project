using DVLD_Business;
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
using static DVLD_Business.clcApplicationBusiness;
using static DVLD_Business.clcLicenseBusiness;

namespace DVLD_Presentation.Applications
{
    public partial class ReplaceLostOrDamagedLicense : Form
    {
        private int _NewRelpacemntLicenseID = 0;
        private enIssueReason IssueReason = enIssueReason.ReplacementForLost;
        private clcApplicationBusiness.enApplicationType ApplicationType = enApplicationType.ReplaceLostDrivingLicense;
        private void _SetDefaultView()
        {
            ctrlDriverLicenseInfoWithFilter1.EnableFilter = true; 
            llShowLicenseInfo.Enabled = false;
            btnIssueReplacement.Enabled = false;

             IssueReason = enIssueReason.ReplacementForLost;
             ApplicationType = enApplicationType.ReplaceLostDrivingLicense;

            rbLostLicense.Checked = true;
            lblTitle.Text = "Replacemnt for Lost License";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = clcApplicationTypesBusiness.GetApplicationTypeFees((int)
                ApplicationType).ToString();
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = clcGlobal.GolbalUser.UserName;

            lblOldLicenseID.Text = lblApplicationID.Text = lblReplaceLicenseID.Text = "????";

            ctrlDriverLicenseInfoWithFilter1.FoucestxtLicense();
        }
        public ReplaceLostOrDamagedLicense()
        {
            InitializeComponent();
        }

        private void ReplaceLostOrDamagedLicense_Load(object sender, EventArgs e)
        {
            _SetDefaultView();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseClick(int obj)
        {
            int LicenseID = obj;

            if(LicenseID <= 0)
            {
                return;
            }

            lblOldLicenseID.Text = ctrlDriverLicenseInfoWithFilter1.LicenseInfo.LicenseID.ToString();

            if (ctrlDriverLicenseInfoWithFilter1.LicenseInfo.isLicenseExpirated())
            {
                MessageBox.Show("Seleted license is expiared, you should renew it", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                _SetDefaultView();
                return;
            }
            if (!ctrlDriverLicenseInfoWithFilter1.LicenseInfo.IsActive)
            {
                MessageBox.Show("Seleted license is not active", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                _SetDefaultView();
                return;
            }
            btnIssueReplacement.Enabled = true;


        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to renew license", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                == DialogResult.OK)
            {
                clcLicenseBusiness ReplacementLicense = ctrlDriverLicenseInfoWithFilter1.LicenseInfo
                    .ReplaceLicense(IssueReason, ApplicationType, clcGlobal.GolbalUser.UserID);

                if (ReplacementLicense != null)
                {
                    lblReplaceLicenseID.Text = ReplacementLicense.LicenseID.ToString();
                    lblApplicationID.Text = ReplacementLicense.ApplicationID.ToString();

                    MessageBox.Show("License was replaced successfully", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _NewRelpacemntLicenseID = ReplacementLicense.LicenseID;

                    btnIssueReplacement.Enabled = false;
                    ctrlDriverLicenseInfoWithFilter1.EnableFilter = false;
                    llShowLicenseInfo.Enabled = true;
                    gbReplacementFor.Enabled = false;

                }
                else
                {
                    MessageBox.Show("License was not replace, please try again leter", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            IssueReason = enIssueReason.ReplacementForLost;
            ApplicationType = enApplicationType.ReplaceLostDrivingLicense;

            lblTitle.Text = "Replacemnt for Lost License";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = clcApplicationTypesBusiness.GetApplicationTypeFees((int)
                ApplicationType).ToString();
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            IssueReason = enIssueReason.ReplacementForDamaged;
            ApplicationType = enApplicationType.ReplaceDamagedDrivingLicense;
            lblTitle.Text = "Replacemnt for Damaged License";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = clcApplicationTypesBusiness.GetApplicationTypeFees((int)
                ApplicationType).ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowLicenseInfo frm = new ShowLicenseInfo(_NewRelpacemntLicenseID);
            frm.ShowDialog();
        }
    }
}
