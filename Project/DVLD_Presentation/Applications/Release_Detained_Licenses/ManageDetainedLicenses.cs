using DVLD_Business;
using DVLD_Presentation.License;
using DVLD_Presentation.License.Detain_license;
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

namespace DVLD_Presentation.Applications.Release_Detained_Licenses
{
    public partial class ManageDetainedLicenses : Form
    {
        private int _PersonID = 0;
        private DataTable _DtDetainedLicensesList;

        private void _LoadDetainedLicensesList()
        {
            _DtDetainedLicensesList = clcDetainLicensesBusiness.GetAllDetainedLicenses();
            dgvDetainedLicenses.DataSource = _DtDetainedLicensesList;

            cbFilterBy.SelectedIndex = cbFilterBy.FindString("None");
            txtFilterValue.Text = "";
            txtFilterValue.Visible = false;
            cbIsReleased.Visible = false;
            _DtDetainedLicensesList.DefaultView.RowFilter = "";
            lblTotalRecords.Text = _DtDetainedLicensesList.DefaultView.Count.ToString();

        }

        public ManageDetainedLicenses()
        {
            InitializeComponent();
        }

        private void ManageDetainedLicenses_Load(object sender, EventArgs e)
        {
            _LoadDetainedLicensesList();

            if(dgvDetainedLicenses.RowCount > 0 )
            {
                dgvDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicenses.Columns[0].Width = 90;

                dgvDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicenses.Columns[1].Width = 90;

                dgvDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicenses.Columns[2].Width = 160;

                dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns[3].Width = 110;

                dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns[4].Width = 110;

                dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns[5].Width = 160;

                dgvDetainedLicenses.Columns[6].HeaderText = "N.No.";
                dgvDetainedLicenses.Columns[6].Width = 90;

                dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns[7].Width = 340;

                dgvDetainedLicenses.Columns[8].HeaderText = "Rlease App.ID";
                dgvDetainedLicenses.Columns[8].Width = 150;

                cmsApplications.Enabled = true;
            }
            else
            {
                cbFilterBy.SelectedIndex = -1;
                txtFilterValue.Text = "";
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = false;
            }

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            _DtDetainedLicensesList.DefaultView.RowFilter = "";
            
            if(cbFilterBy.SelectedItem.ToString() == "None")
            {
                
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = false;
            }

            else if (cbFilterBy.SelectedItem.ToString() == "Is Released")
            {
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.SelectedIndex = -1;
            }

            else
            {
                txtFilterValue.Visible = true;
                cbIsReleased.Visible = false;
                txtFilterValue.Focus();
            }

            
            lblTotalRecords.Text = _DtDetainedLicensesList.DefaultView.Count.ToString();

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {

            if (cbFilterBy.SelectedItem.ToString() == "None" || cbFilterBy.SelectedItem.ToString() == "Is Released")
            {
                return;
            }

            string txtFilter = "";
            
            switch(cbFilterBy.SelectedItem.ToString())
            {
                case "Detain ID":
                    {
                        txtFilter = "DetainID";
                        break;
                    }

                case "National No":
                    {
                        txtFilter = "NationalNo";
                        break;
                    }

                case "Full Name":
                    {
                        txtFilter = "FullName";
                        break;
                    }

                case "Release Application ID":
                    {
                        txtFilter = "ReleaseApplicationID";
                        break;
                    }
            }

            if (txtFilter == "DetainID"  || txtFilter == "ReleaseApplicationID")
            {
                if (txtFilterValue.Text == "")
                {
                    _DtDetainedLicensesList.DefaultView.RowFilter = "";
                    lblTotalRecords.Text = _DtDetainedLicensesList.DefaultView.Count.ToString();
                    return;
                }


                _DtDetainedLicensesList.DefaultView.RowFilter = string.Format("[{0}] = {1}", txtFilter, txtFilterValue.Text.Trim());
            }
            else
            {
                _DtDetainedLicensesList.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", txtFilter, txtFilterValue.Text.Trim());
            }

            lblTotalRecords.Text = _DtDetainedLicensesList.DefaultView.Count.ToString();

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() == "Detain ID" || cbFilterBy.SelectedItem.ToString() == "Release Application ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIsReleased.SelectedItem == null)
            {
                _DtDetainedLicensesList.DefaultView.RowFilter = "";
                lblTotalRecords.Text = _DtDetainedLicensesList.DefaultView.Count.ToString();
                return;
            }

            switch (cbIsReleased.SelectedItem.ToString())
            {
                case "All":
                    {
                        _DtDetainedLicensesList.DefaultView.RowFilter = "";
                        break;
                    }
                case "Yes":
                    {
                        _DtDetainedLicensesList.DefaultView.RowFilter = string.Format("[{0}] = {1}", "IsReleased", "1");
                        break;
                    }
                case "No":
                    {
                        _DtDetainedLicensesList.DefaultView.RowFilter = string.Format("[{0}] = {1}", "IsReleased", "0");
                        break;
                    }
            }

            lblTotalRecords.Text = _DtDetainedLicensesList.DefaultView.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            DetainLicense frm = new DetainLicense();
            frm.ShowDialog();

            ManageDetainedLicenses_Load(null, null);
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicense frm = new ReleaseDetainedLicense();
            frm.ShowDialog();

            ManageDetainedLicenses_Load(null, null);
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            if (clcDetainLicensesBusiness.IsLicenseDetain((int)dgvDetainedLicenses.CurrentRow.Cells[1].Value))
            {
                releaseDetainedLicenseToolStripMenuItem.Enabled = true;
            }
            else
            {
                releaseDetainedLicenseToolStripMenuItem.Enabled = false;
            }
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicense frm = new ReleaseDetainedLicense((int)dgvDetainedLicenses.CurrentRow.Cells[1].Value);
            frm.ShowDialog();

            ManageDetainedLicenses_Load(null, null);
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _PersonID = clcLicenseBusiness.Find((int)dgvDetainedLicenses.CurrentRow.Cells[1].Value).DriverInfo.PersonID;

            ShowPersonInfo frm = new ShowPersonInfo(_PersonID);
            frm.ShowDialog();

            ManageDetainedLicenses_Load(null, null);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLicenseInfo frm = new ShowLicenseInfo((int)dgvDetainedLicenses.CurrentRow.Cells[1].Value);
            frm.ShowDialog();

            ManageDetainedLicenses_Load(null, null);
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _PersonID = clcLicenseBusiness.Find((int)dgvDetainedLicenses.CurrentRow.Cells[1].Value).DriverInfo.PersonID;

            LicensesHistory frm = new LicensesHistory(_PersonID);
            frm.ShowDialog();

            ManageDetainedLicenses_Load(null, null);

        }
    }
}
