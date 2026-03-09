using DVLD_Business;
using DVLD_Presentation.Applications.International_Licenses;
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

namespace DVLD_Presentation.License.International_Licenses
{
    public partial class InternationalLicensesList : Form
    {
        private DataTable dtInternationalLicenses;

        private void _LoadInternationalLicensesList()
        {
            dtInternationalLicenses = clcInternationalLicenseBusiness.GetInternationalLicensesList();
            dgvInternationalLicenses.DataSource = dtInternationalLicenses;
            lblInternationalLicensesRecords.Text = dgvInternationalLicenses.RowCount.ToString();
            cmsApplications.Enabled = dgvInternationalLicenses.RowCount > 0;

            cbFilterBy.SelectedIndex = cbFilterBy.FindString("None");
            cbIsReleased.Visible = false;
            txtFilterValue.Text = "";
            txtFilterValue.Visible = false;
            dtInternationalLicenses.DefaultView.RowFilter = "";
        }

        public InternationalLicensesList()
        {
            InitializeComponent();
        }

        private void InternationalLicensesList_Load(object sender, EventArgs e)
        {
            _LoadInternationalLicensesList();
            
            if(dgvInternationalLicenses.RowCount > 0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Inter.License";
                dgvInternationalLicenses.Columns[0].Width = 100;

                dgvInternationalLicenses.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenses.Columns[1].Width = 110;

                dgvInternationalLicenses.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenses.Columns[2].Width = 100;

                dgvInternationalLicenses.Columns[3].HeaderText = "L.D.License";
                dgvInternationalLicenses.Columns[3].Width = 100;

                dgvInternationalLicenses.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns[4].Width = 140;

                dgvInternationalLicenses.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns[5].Width = 140;

                dgvInternationalLicenses.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns[6].Width = 100;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilterBy.SelectedItem.ToString() == "None")
            {
                cbIsReleased.Visible = false;
                txtFilterValue.Text = "";
                txtFilterValue.Visible = false;
            }
            else if(cbFilterBy.SelectedItem.ToString() == "Is Active")
            {
                cbIsReleased.Visible = true;
                cbIsReleased.SelectedIndex = -1;
                txtFilterValue.Text = "";
                txtFilterValue.Visible = false;
            }
            else
            {
                cbIsReleased.Visible = false;
                txtFilterValue.Text = "";
                txtFilterValue.Visible = true;
                txtFilterValue.Focus();
            }

            dtInternationalLicenses.DefaultView.RowFilter = "";
            lblInternationalLicensesRecords.Text = dtInternationalLicenses.DefaultView.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {

            if(cbFilterBy.SelectedItem.ToString() == "None" || cbFilterBy.SelectedItem.ToString() == "IsActive")
            {
                return;
            }

            string FilterText = "";

            switch(cbFilterBy.SelectedItem.ToString())
            {
                case "International License ID":
                    {
                        FilterText = "InternationalLicenseID";
                        break;
                    }

                case "Application ID":
                    {
                        FilterText = "ApplicationID";
                        break;
                    }

                case "Driver ID":
                    {
                        FilterText = "DriverID";
                        break;
                    }

                case "Local License ID":
                    {
                        FilterText = "IssuedUsingLocalLicenseID";
                        break;
                    }

            }

            if (txtFilterValue.Text == "")
            {
                dtInternationalLicenses.DefaultView.RowFilter = "";
                lblInternationalLicensesRecords.Text = dtInternationalLicenses.DefaultView.Count.ToString();
                return;
            }


            dtInternationalLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterText, txtFilterValue.Text.Trim());

            lblInternationalLicensesRecords.Text = dtInternationalLicenses.DefaultView.Count.ToString();

        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIsReleased.SelectedItem == null)
            {
                dtInternationalLicenses.DefaultView.RowFilter = "";
                return;
            }

            switch(cbIsReleased.SelectedItem.ToString())
            {
                case "All":
                    {
                        dtInternationalLicenses.DefaultView.RowFilter = "";
                        break;
                    }

                case "Yes":
                    {
                        dtInternationalLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", "IsActive", "1");
                        break;
                    }

                case "No":
                    {
                        dtInternationalLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", "IsActive", "0");
                        break;
                    }


            }

            lblInternationalLicensesRecords.Text = dtInternationalLicenses.DefaultView.Count.ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LicensesHistory frm = new LicensesHistory((int)dgvInternationalLicenses.CurrentRow.Cells[3].Value);
            frm.ShowDialog();

            InternationalLicensesList_Load(null, null);
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clcDriverBusiness.GetPersonIDByDriverID((int)dgvInternationalLicenses.CurrentRow.Cells[2].Value);

            ShowPersonInfo frm = new ShowPersonInfo(PersonID);
            frm.ShowDialog();

            InternationalLicensesList_Load(null, null);
        }

        private void btnNewApplication_Click(object sender, EventArgs e)
        {
            AddNewInternationalLicense frm = new AddNewInternationalLicense();
            frm.ShowDialog();

            InternationalLicensesList_Load(null, null);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showInternationalLicenseInfo frm = new showInternationalLicenseInfo((int)dgvInternationalLicenses.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
