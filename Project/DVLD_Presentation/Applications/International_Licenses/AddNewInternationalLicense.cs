using DVLD_Business;
using DVLD_Presentation.License;
using DVLD_Presentation.License.International_license;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentation.Applications.International_Licenses
{
    public partial class AddNewInternationalLicense : Form
    {
        private int _NewInternationalLicenseID = -1;

        private void _SetDefaultView()
        {
            ctrlDriverLicenseInfoWithFilter1.EnableFilter = true;
            ctrlDriverLicenseInfoWithFilter1.EnableBtnFind = true;
            ctrlDriverLicenseInfoWithFilter1.FoucestxtLicense();


            lblApplicationDate.Text = DateTime.Now.ToLongDateString();
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToLongDateString();   
            lblCreatedByUser.Text = clcGlobal.GolbalUser.UserName;
            lblFees.Text = clcApplicationTypesBusiness.GetApplicationTypeFees((int)
                clcApplicationBusiness.enApplicationType.NewInternationalLicense).ToString();

            btnIssueLicense.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            llShowLicenseHistory.Enabled = ctrlDriverLicenseInfoWithFilter1.LicenseInfo != null;
        }
        public AddNewInternationalLicense()
        {
            InitializeComponent();
        }

        private void AddNewInternationalLicense_Load(object sender, EventArgs e)
        {
            _SetDefaultView();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseClick(int obj)
        {
            int SelectedLicenseID = obj;

            if (SelectedLicenseID <= 0)
            {
                return;
            }

            if(clcInternationalLicenseBusiness.isInternationalLicenseExist(SelectedLicenseID))
            {
                MessageBox.Show("Seleted license is already had an active international license", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                _SetDefaultView();
                return;
            }

            if(!ctrlDriverLicenseInfoWithFilter1.LicenseInfo.IsActive)
            {
                MessageBox.Show("Seleted license is not active", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                _SetDefaultView();
                return ;
            }

            if(ctrlDriverLicenseInfoWithFilter1.LicenseInfo.isLicenseExpirated())
            {
                MessageBox.Show("Seleted license is expiared", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                _SetDefaultView();
                return;
            }

            if(ctrlDriverLicenseInfoWithFilter1.LicenseInfo.LicenseClassesInfo.LicenseID != 3)
            {
                MessageBox.Show("Seleted license is not Ordinary License", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                _SetDefaultView();
                return ;
            }
            llShowLicenseHistory.Enabled = ctrlDriverLicenseInfoWithFilter1.LicenseInfo != null;
            lblLocalLicenseID.Text = ctrlDriverLicenseInfoWithFilter1.LicenseInfo.LicenseID.ToString();
            btnIssueLicense.Enabled = true;

        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue international license", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                == DialogResult.OK)
            {
                clcInternationalLicenseBusiness NewInternationalLicesneInfo =
                ctrlDriverLicenseInfoWithFilter1.LicenseInfo.IssueInternationalLicense(clcGlobal.GolbalUser.UserID);

                if (NewInternationalLicesneInfo != null)
                {
                    lblInternationalLicenseID.Text = NewInternationalLicesneInfo.InternationalLicenseID.ToString();
                    lblApplicationID.Text = NewInternationalLicesneInfo.ApplicationID.ToString();

                    MessageBox.Show("License was renewed successfully", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _NewInternationalLicenseID = NewInternationalLicesneInfo.InternationalLicenseID;

                    btnIssueLicense.Enabled = false;
                    ctrlDriverLicenseInfoWithFilter1.EnableFilter = false;
                    llShowLicenseInfo.Enabled = true;
                }
                else
                {
                    MessageBox.Show("License was not renewed, please try again leter", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicensesHistory frm = new LicensesHistory(ctrlDriverLicenseInfoWithFilter1.LicenseInfo.ApplicationInfo.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            showInternationalLicenseInfo frm = new showInternationalLicenseInfo(_NewInternationalLicenseID);
            frm.ShowDialog();
        }
    }
}
