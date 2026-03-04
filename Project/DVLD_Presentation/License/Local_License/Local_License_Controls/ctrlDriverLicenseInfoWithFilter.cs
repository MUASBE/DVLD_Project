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

namespace DVLD_Presentation.License.Local_License.Local_License_Controls
{
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {
        private int _LicenseID = 0;
        public event Action<int> OnLicenseClick;
        // Create a protected method to raise the event with a parameter
        protected virtual void LicenseClick(int LicenseID)
        {
            Action<int> handler = OnLicenseClick;
            if (handler != null)
            {
                handler(LicenseID); // Raise the event with the parameter
            }
        }
        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
            txtLicenseID.Focus();
        }
        
        public clcLicenseBusiness LicenseInfo
        {
            get
            {
                return ctrlDriverLicenseInfo1.LicenseInfo;
            }
        }
        public bool EnableBtnFind
        {
            get
            {
                return btnFind.Enabled;
            }
            set
            {
                btnFind.Enabled = value;
            }
        }
        public bool EnableFilter
        {
            get
            {
                return gbFilters.Enabled;
            }
            set
            {
                gbFilters.Enabled = value;
            }
        }
        public void FoucestxtLicense()
        {
            txtLicenseID.Focus();
        }
        private void _FindNow()
        {
            if (int.TryParse(txtLicenseID.Text.Trim(), out _LicenseID))
            {
                ctrlDriverLicenseInfo1.LoadInfo(_LicenseID);

                if (OnLicenseClick != null && EnableFilter)
                {
                    OnLicenseClick(_LicenseID);
                }

            }
        }
        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if(gbFilters.Enabled)
            {
                if(txtLicenseID.Text == "")
                {
                    errorProvider1.SetError(txtLicenseID, "Filed is require");
                    e.Cancel = true;
                    txtLicenseID.Focus();
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txtLicenseID, null);
                }
            }
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }
            
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                return;
            }

            _FindNow();
        }

        private void txtLicenseID_TextChanged(object sender, EventArgs e)
        {
            if (txtLicenseID.Text.Contains("\n"))
            {
                txtLicenseID.Text = txtLicenseID.Text.Substring(0, txtLicenseID.Text.Length - 2);
            }
        }
    }
}
