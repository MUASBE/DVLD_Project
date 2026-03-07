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

namespace DVLD_Presentation.License.License_Controls
{
    public partial class ctrlDriverLicenses : UserControl
    {
        int _DriverID = 0;

        DataTable DtDriverLicenses;
        DataTable DtInternationalDriverLicenses;
        private void LoadLocalDriverLicenses()
        {
            DtDriverLicenses = clcLicenseBusiness.GetDriverLicense(_DriverID);
            dgvLocalLicensesHistory.DataSource = DtDriverLicenses;
            if(dgvLocalLicensesHistory.ColumnCount > 0 )
            {

                dgvLocalLicensesHistory.Columns[0].HeaderText = "License ID";
                dgvLocalLicensesHistory.Columns[0].Width = 100;

                dgvLocalLicensesHistory.Columns[1].HeaderText = "App ID.";
                dgvLocalLicensesHistory.Columns[1].Width = 100;

                dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
                dgvLocalLicensesHistory.Columns[2].Width = 150;

                dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicensesHistory.Columns[3].Width = 150;

                dgvLocalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicensesHistory.Columns[4].Width = 150;

                dgvLocalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvLocalLicensesHistory.Columns[5].Width = 100;

                showLicenseInfoToolStripMenuItem.Enabled = true;
            }
            else
            {
                showLicenseInfoToolStripMenuItem.Enabled = false;
            }

            lblLocalLicensesRecords.Text = DtDriverLicenses.DefaultView.Count.ToString();
        }
        private void LoadInternationalDriverLicenses()
        {
            //dgvInternationalLicensesHistory;
        }
        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        public void LoadDriverLicenses(int PersonID)
        {
            //method to load local driver licenses 
            // method to load international driver license 

            _DriverID = clcDriverBusiness.GetDriverIDByPersonID(PersonID);

            if(_DriverID <= 0)
            {
                MessageBox.Show("There is no driver linked with this person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadLocalDriverLicenses();
            LoadInternationalDriverLicenses();
        }
        public void Clear()
        {
            DtDriverLicenses.Clear();
            DtInternationalDriverLicenses.Clear();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLicenseInfo frm = new ShowLicenseInfo((int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
