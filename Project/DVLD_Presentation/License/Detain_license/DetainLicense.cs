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

namespace DVLD_Presentation.License.Detain_license
{
    public partial class DetainLicense : Form
    {
        private int _DetainID = 0;

        private void _SetDefaultView()
        {
            lblDetainID.Text = "????";
            lblDetainDate.Text = DateTime.Now.ToLongDateString();
            txtFineFees.Text = "";
            txtFineFees.Enabled = false;
            lblLicenseID.Text = (ctrlDriverLicenseInfoWithFilter1.LicenseInfo != null) ?
                ctrlDriverLicenseInfoWithFilter1.LicenseInfo.LicenseID.ToString() : "????";
            lblCreatedByUser.Text = clcGlobal.GolbalUser.UserName;

            llShowLicenseHistory.Enabled = (ctrlDriverLicenseInfoWithFilter1.LicenseInfo != null);

            ctrlDriverLicenseInfoWithFilter1.TabIndex = 0;
            ctrlDriverLicenseInfoWithFilter1.FoucestxtLicense();
        }

        public DetainLicense()
        {
            InitializeComponent();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseClick(int obj)
        {
            int SelectedLicenseID = obj;

            if(SelectedLicenseID <= 0)
            {
                return;
            }

            int DetainID = clcDetainLicensesBusiness.GetDetainIDForLicense(ctrlDriverLicenseInfoWithFilter1.LicenseInfo.LicenseID);
            if (DetainID > 0)
            {
                MessageBox.Show($"License has already detained with detain id {DetainID}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _SetDefaultView();
                return;
            }

            if (!ctrlDriverLicenseInfoWithFilter1.LicenseInfo.IsActive)
            {
                MessageBox.Show("Seleted license is not active", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                _SetDefaultView();
                return;
            }

            lblLicenseID.Text = ctrlDriverLicenseInfoWithFilter1.LicenseInfo.LicenseID.ToString();
            llShowLicenseHistory.Enabled = (ctrlDriverLicenseInfoWithFilter1.LicenseInfo != null);
            btnIssueDetain.Enabled = true;
            txtFineFees.Enabled = true;

        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                errorProvider1.SetError(txtFineFees, "Enter fine fees to complete process");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);
                e.Cancel = false;
            }
        }

        private void btnIssueDetain_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())
            {
                return;
            }

            if (MessageBox.Show("Are you sure you want to Detain license", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                == DialogResult.OK)
            {
                _DetainID = ctrlDriverLicenseInfoWithFilter1.LicenseInfo.DetainLicense(DateTime.Now,
                    Convert.ToSingle(txtFineFees.Text.Trim()), clcGlobal.GolbalUser.UserID);

                if(_DetainID > 0)
                {
                    lblDetainID.Text = _DetainID.ToString();
                    
                    MessageBox.Show("License was Detained successfully", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ctrlDriverLicenseInfoWithFilter1.LoadLicenseDataAgain();

                    btnIssueDetain.Enabled = false;
                    txtFineFees.Enabled = false;
                    llShowLicenseHistory.Enabled = true;
                    llShowLicenseInfo.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Error on detaining license, please try again leter", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                

            }

        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void DetainLicense_Load(object sender, EventArgs e)
        {
            _SetDefaultView();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
