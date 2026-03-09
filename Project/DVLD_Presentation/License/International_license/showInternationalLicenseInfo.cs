using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentation.License.International_license
{
    public partial class showInternationalLicenseInfo : Form
    {
        private int _LicenseID = 0;

        public showInternationalLicenseInfo(int licenseID)
        {
            InitializeComponent();
            _LicenseID = licenseID;
        }

        private void showInteernationalLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlShowInternationalLicenseInfo1.LoadInfo(_LicenseID);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
