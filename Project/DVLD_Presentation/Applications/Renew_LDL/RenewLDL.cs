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

namespace DVLD_Presentation.Applications.Renew_LDL
{
    public partial class RenewLDL : Form
    {
        public enum enMode { AddMode =1, EditMode =2 }
        public enMode Mode = enMode.AddMode;

        private int _NewLicenseID;


        private void _SetDefaultView()
        {
            ctrlDriverLicenseInfoWithFilter1.EnableFilter = true;
            ctrlDriverLicenseInfoWithFilter1.EnableBtnFind = true;
            ctrlDriverLicenseInfoWithFilter1.FoucestxtLicense();

            
            lblApplicationDate.Text = DateTime.Now.ToLongDateString();
            lblIssueDate.Text = lblApplicationDate.Text;
            lblCreatedByUser.Text = clcGlobal.GolbalUser.UserName;
            lblApplicationFees.Text = clcApplicationTypesBusiness.GetApplicationTypeFees(
                (int)clcApplicationBusiness.enApplicationType.RenewDrivingLicense).ToString();
            lblTotalFees.Text = lblApplicationFees.Text;

            btnRenewLicense.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            txtNotes.Enabled = false;
            llShowLicenseHistory.Enabled = ctrlDriverLicenseInfoWithFilter1.LicenseInfo != null;
        }

        public RenewLDL()
        {
            InitializeComponent();
            Mode = enMode.AddMode;
        }


        private void RenewLDL_Load(object sender, EventArgs e)
        {
            _SetDefaultView();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseClick(int obj)
        {
            int _LicenseID = obj;
            if(ctrlDriverLicenseInfoWithFilter1.LicenseInfo == null)
            {
                _SetDefaultView();
                return;
            }

            if (!ctrlDriverLicenseInfoWithFilter1.LicenseInfo.IsActive)
            {
                MessageBox.Show("Seleted license is not active", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                _SetDefaultView();
                return;

            }
            
            if(!ctrlDriverLicenseInfoWithFilter1.LicenseInfo.isLicenseExpirated())
            {
                MessageBox.Show("Seleted license is not yet expiared", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                _SetDefaultView();
            }
            else
            {
                btnRenewLicense.Enabled = true;
                txtNotes.Enabled = true;
            }

            llShowLicenseHistory.Enabled = ctrlDriverLicenseInfoWithFilter1.LicenseInfo != null;

            lblLicenseFees.Text = ctrlDriverLicenseInfoWithFilter1.LicenseInfo.PaidFees.ToString();
            txtNotes.Text = ctrlDriverLicenseInfoWithFilter1.LicenseInfo.Notes;
            lblOldLicenseID.Text = ctrlDriverLicenseInfoWithFilter1.LicenseInfo.LicenseID.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(ctrlDriverLicenseInfoWithFilter1.LicenseInfo.LicenseClassesInfo.DefaultValidityLength).ToString();

            lblTotalFees.Text = (Convert.ToSingle(lblTotalFees.Text) +
                ctrlDriverLicenseInfoWithFilter1.LicenseInfo.PaidFees).ToString();
        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            if (ctrlDriverLicenseInfoWithFilter1.LicenseInfo == null)
            {
                _SetDefaultView();
                return;
            }

            if(MessageBox.Show("Are you sure you want to renew license", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                == DialogResult.OK)
            {

                clcLicenseBusiness _newLicenseInfo = ctrlDriverLicenseInfoWithFilter1.LicenseInfo.RenewLicense(txtNotes.Text, 
                    clcGlobal.GolbalUser.UserID);
                
                


                if(_newLicenseInfo != null)
                {
                    lblRenewedLicenseID.Text = _newLicenseInfo.LicenseID.ToString();
                    lblApplicationID.Text = _newLicenseInfo.ApplicationID.ToString();

                    MessageBox.Show("License was renewed successfully", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _NewLicenseID = _newLicenseInfo.LicenseID;

                    txtNotes.Enabled = false;
                    btnRenewLicense.Enabled = false;
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

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowLicenseInfo frm = new ShowLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicensesHistory frm = new LicensesHistory(ctrlDriverLicenseInfoWithFilter1.LicenseInfo.ApplicationInfo.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}
