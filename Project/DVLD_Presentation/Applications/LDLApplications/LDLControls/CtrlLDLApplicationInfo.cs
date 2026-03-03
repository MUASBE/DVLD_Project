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

namespace DVLD_Presentation
{
    public partial class CtrlLDLApplicationInfo : UserControl
    {

        public event Action<int> OnLDLApplicationClick;
        // Create a protected method to raise the event with a parameter
        protected virtual void LDLApplicationClick(int LDLApplicationID)
        {
            Action<int> handler = OnLDLApplicationClick;
            if (handler != null)
            {
                handler(LDLApplicationID); // Raise the event with the parameter
            }
        }

        private int _LDLApplicationID;
        private clcLDLApplicationBusiness _LDLApplication;
        public int LDLApplicationID
        {
                get
                {
                    return _LDLApplicationID;
                }
        }
        public clcLDLApplicationBusiness LDLApplication
        {
                get
                {
                    return _LDLApplication;
                }
        }
        private void _resetDefaultValue()
        {
            _LDLApplicationID = 0;
            _LDLApplication = null;
            LinklblShowLicenseInfo.Enabled = false;
        }
        public CtrlLDLApplicationInfo()
        {
            InitializeComponent();
        }
        public void LoadLDLApplicationInfo(int LDLApplicationID)
        {
            _LDLApplicationID = LDLApplicationID;
            _LDLApplication = clcLDLApplicationBusiness.Find(LDLApplicationID);
            if( _LDLApplication != null )
            {
                lblLDLAppliactionID.Text = _LDLApplication.LDLApplicationID.ToString();
                lblLicenseName.Text = _LDLApplication.LicenseInfo.LicenseName;
                lblPassedTest.Text = _LDLApplication.PassedTestCount().ToString();
                ctrlBaseApplicationInfo1.LoadApplicationInfo(_LDLApplication.ApplicationID);

                if (OnLDLApplicationClick != null)
                {
                    OnLDLApplicationClick(_LDLApplication.ApplicationID);
                }

                int LicenseID = clcLicenseBusiness.GetLicenseByPersonID(_LDLApplication.ApplicationInfo.ApplicantPersonID, 
                    _LDLApplication.LicenseInfo.LicenseID);
                if ( LicenseID > 0 )
                    LinklblShowLicenseInfo.Enabled = true;
                else
                    LinklblShowLicenseInfo.Enabled = false;

            }
            else
            {
                MessageBox.Show("Local Driving Appliaction was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _resetDefaultValue();
            }
        }

        private void LinklblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowLicenseInfo frm = new ShowLicenseInfo(_LDLApplication.ApplicationID);
            frm.ShowDialog();
        }
    }
}
