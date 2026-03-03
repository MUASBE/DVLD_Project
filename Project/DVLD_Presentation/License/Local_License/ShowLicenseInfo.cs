using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentation.License.Local_License
{
    public partial class ShowLicenseInfo : Form
    {
        private int _ApplicationID;
        public ShowLicenseInfo(int ApplicationID)
        {
            InitializeComponent();
            _ApplicationID = ApplicationID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfo1.LoadLicenseInfoByApplicationID(_ApplicationID);
            if(ctrlDriverLicenseInfo1.LicenseInfo == null)
            {
                MessageBox.Show("License was not found, please try again leter", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}
