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
namespace DVLD_Presentation
{
    public partial class LDLApplicationList : Form
    {
        DataTable DTLDLApplications;
        private clcLDLApplicationBusiness LDLApplicationInfo;
        private void _ReshreshLDLApplications()
        {
            DTLDLApplications = clcLDLApplicationBusiness.GetAllLDLApplications();
            DGVLDLApplication.DataSource = DTLDLApplications;
            lblNumebrOfLDLApplication.Text = DGVLDLApplication.Rows.Count.ToString();
            cbUserChoice.SelectedIndex = 0;
            txtUserChoice.Text = "";
            txtUserChoice.Visible = false;
            cbStatusChoice.Visible = false;
        }

        public LDLApplicationList()
        {
            InitializeComponent();
        }

        private void LDLApplicationList_Load(object sender, EventArgs e)
        {
            _ReshreshLDLApplications();

                if (DGVLDLApplication.Rows.Count > 0)
                {
                    DGVLDLApplication.Columns[0].HeaderText = "L.D.L AppID";
                    DGVLDLApplication.Columns[0].Width = 80;
    
                    DGVLDLApplication.Columns[1].HeaderText = "Driving Class";
                    DGVLDLApplication.Columns[1].Width = 215;
    
                    DGVLDLApplication.Columns[2].HeaderText = "National No";
                    DGVLDLApplication.Columns[2].Width = 100;
    
                    DGVLDLApplication.Columns[3].HeaderText = "Full Name";
                    DGVLDLApplication.Columns[3].Width = 350;
    
                    DGVLDLApplication.Columns[4].HeaderText = "Application Date";
                    DGVLDLApplication.Columns[4].Width = 120;

                    DGVLDLApplication.Columns[5].HeaderText = "Passed Test";
                    DGVLDLApplication.Columns[5].Width = 100;

                    DGVLDLApplication.Columns[6].HeaderText = "Status";
                    DGVLDLApplication.Columns[6].Width = 100;
                    
                    DTLDLApplications.DefaultView.RowFilter = "";

                }
                else
                {
                    cbStatusChoice.Visible = false;
                    txtUserChoice.Visible = false;
                    cbStatusChoice.Visible = false;
                }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbUserChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbUserChoice.SelectedIndex == 0)
            {
                txtUserChoice.Visible = false;
                cbStatusChoice.Visible = false;
                DTLDLApplications.DefaultView.RowFilter = "";
            }
            else if (cbUserChoice.SelectedItem.ToString() == "Status")
            {
                txtUserChoice.Visible = false;
                cbStatusChoice.Visible = true;
                cbStatusChoice.SelectedIndex = -1;
            }
            else
            {
                cbStatusChoice.Visible = false;
                txtUserChoice.Visible = true;
                txtUserChoice.Text = "";
                txtUserChoice.Focus();
            }
        }
        private void cbStatusChoice_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if(cbStatusChoice.SelectedItem == null)
            {
                DTLDLApplications.DefaultView.RowFilter = "";
                return;
            }

            switch (cbStatusChoice.SelectedItem.ToString())
            {
                case "New":
                    {
                        DTLDLApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", "Status", "New");
                        break;
                    }
                case "Cancelled":
                    {
                        DTLDLApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", "Status", "Cancelled");
                        break;
                    }
                case "Completed":
                    {
                        DTLDLApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", "Status", "Completed");
                        break;
                    }
                default:
                    {
                        DTLDLApplications.DefaultView.RowFilter = "";
                        break;
                    }
            }
        }

        private void txtUserChoice_TextChanged(object sender, EventArgs e)
        {
            string FilterText = "";

            switch(cbUserChoice.Text)
            {
                case "L.D.L AppID":
                    {
                        FilterText = "LocalDrivingLicenseApplicationID";
                        break;
                    }
                case "National No":
                    {
                        FilterText = "NationalNo";
                        break;
                    }
                case "Full Name":
                    {
                        FilterText = "FullName";
                        break;
                    }
                default:
                    {
                        DTLDLApplications.DefaultView.RowFilter = "";
                        break;
                    }
            }

            if(FilterText == "LocalDrivingLicenseApplicationID")
            {
                if (txtUserChoice.Text == "")
                {
                    DTLDLApplications.DefaultView.RowFilter = "";
                    lblNumebrOfLDLApplication.Text = DTLDLApplications.DefaultView.Count.ToString();
                    return;
                }


                DTLDLApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterText, txtUserChoice.Text.Trim());
            }
            else
            {
                DTLDLApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterText, txtUserChoice.Text);
            }

            lblNumebrOfLDLApplication.Text = DTLDLApplications.DefaultView.Count.ToString();
        }

        private void txtUserChoice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbUserChoice.Text == "L.D.L AppID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            AddEditLDLApplication frmAddEditLDLApplication = new AddEditLDLApplication();
            frmAddEditLDLApplication.ShowDialog();
            _ReshreshLDLApplications();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditLDLApplication frmAddEditLDLApplication = new AddEditLDLApplication((int)DGVLDLApplication.CurrentRow.Cells[0].Value);
            frmAddEditLDLApplication.ShowDialog();
            _ReshreshLDLApplications();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLDLApplicationInfo frmShowLDLApplicationInfo = new ShowLDLApplicationInfo((int)DGVLDLApplication.CurrentRow.Cells[0].Value);
            frmShowLDLApplicationInfo.ShowDialog();
            _ReshreshLDLApplications();
        }

        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete the selected application?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clcLDLApplicationBusiness.Delete((int)DGVLDLApplication.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("The application has been deleted successfully.", "Delete Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _ReshreshLDLApplications();
                }
                else
                {
                    MessageBox.Show("Unable to delete the application. Please try again.", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CancelApplicaitonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LDLApplicationInfo = clcLDLApplicationBusiness.Find((int)DGVLDLApplication.CurrentRow.Cells[0].Value);
            if (LDLApplicationInfo != null)
            {
                if (LDLApplicationInfo.ApplicationInfo.CancelApplication())
                {
                    MessageBox.Show("The application has been cancelled successfully.", "Cancel Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _ReshreshLDLApplications();
                }
                else
                {
                    MessageBox.Show("Unable to cancel the application. Please try again.", "Cancel Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            LDLApplicationInfo = null;
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListTestAppointment frmListTestAppointment = new
                ListTestAppointment((int)DGVLDLApplication.CurrentRow.Cells[0].Value,clcTestTypeBusiness._enTestType.VisionTest);
            frmListTestAppointment.ShowDialog();

            _ReshreshLDLApplications();
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListTestAppointment frmListTestAppointment = new
                ListTestAppointment((int)DGVLDLApplication.CurrentRow.Cells[0].Value, clcTestTypeBusiness._enTestType.WrittenTest);

            frmListTestAppointment.ShowDialog();

            _ReshreshLDLApplications();
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListTestAppointment frmListTestAppointment = new
                ListTestAppointment((int)DGVLDLApplication.CurrentRow.Cells[0].Value, clcTestTypeBusiness._enTestType.StreetTest);

            frmListTestAppointment.ShowDialog();

            _ReshreshLDLApplications();
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            LDLApplicationInfo = clcLDLApplicationBusiness.Find((int)DGVLDLApplication.CurrentRow.Cells[0].Value);

            int NumberOfPassedTest = LDLApplicationInfo.PassedTestCount();
            ScheduleTestsMenue.Enabled = true;
            switch (NumberOfPassedTest)
            {
                case 0:
                    {
                        scheduleVisionTestToolStripMenuItem.Enabled = true;
                        scheduleStreetTestToolStripMenuItem.Enabled = false;
                        scheduleWrittenTestToolStripMenuItem.Enabled = false;
                        break;
                    }

                case 1:
                    {
                        scheduleVisionTestToolStripMenuItem.Enabled = false;
                        scheduleStreetTestToolStripMenuItem.Enabled = false;
                        scheduleWrittenTestToolStripMenuItem.Enabled = true;
                        break;
                    }

                case 2:
                    {
                        scheduleVisionTestToolStripMenuItem.Enabled = false;
                        scheduleStreetTestToolStripMenuItem.Enabled = true;
                        scheduleWrittenTestToolStripMenuItem.Enabled = false;
                        break;
                    }

                case 3:
                    {
                        ScheduleTestsMenue.Enabled = false;
                        issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;
                        break;
                    }
            }

            switch(LDLApplicationInfo.ApplicationInfo.ApplicationStatus)
            {
                case clcApplicationBusiness._enStatus.New:
                    {
                        editToolStripMenuItem.Enabled = true;
                        DeleteApplicationToolStripMenuItem.Enabled = true;
                        CancelApplicaitonToolStripMenuItem.Enabled = true;
                        showLicenseToolStripMenuItem.Enabled= false;
                        issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (NumberOfPassedTest == 3);
                        break;
                    }

                case clcApplicationBusiness._enStatus.Cancelled:
                    {
                        ScheduleTestsMenue.Enabled = false;
                        editToolStripMenuItem.Enabled = false ;
                        DeleteApplicationToolStripMenuItem.Enabled = false;
                        CancelApplicaitonToolStripMenuItem.Enabled = false;
                        ScheduleTestsMenue.Enabled = false;
                        issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                        showLicenseToolStripMenuItem.Enabled = false;
                        break;
                    }

                case clcApplicationBusiness._enStatus.Completed:
                    {
                        ScheduleTestsMenue.Enabled = false;
                        editToolStripMenuItem.Enabled = false;
                        DeleteApplicationToolStripMenuItem.Enabled = false;
                        CancelApplicaitonToolStripMenuItem.Enabled = false;
                        issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                        showLicenseToolStripMenuItem.Enabled = true;
                        break;
                    }
            }

        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IssueLicenseFirstTime frm = new IssueLicenseFirstTime((int)DGVLDLApplication.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _ReshreshLDLApplications();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LDLApplicationInfo = clcLDLApplicationBusiness.Find((int)DGVLDLApplication.CurrentRow.Cells[0].Value);

            int LicenseID = clcLicenseBusiness.GetLicenseByPersonID(LDLApplicationInfo.ApplicationInfo.ApplicantPersonID,
                LDLApplicationInfo.LicenseInfo.LicenseID);

            if(LicenseID != 0)
            {
                ShowLicenseInfo frm = new ShowLicenseInfo(LicenseID);
                frm.ShowDialog();
                _ReshreshLDLApplications();
                LDLApplicationInfo = null;
            }
            else
            {
                MessageBox.Show("No License Found!", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LDLApplicationInfo = clcLDLApplicationBusiness.Find((int)DGVLDLApplication.CurrentRow.Cells[0].Value);
            LicensesHistory frm;

            if (LDLApplicationInfo != null)
            {
                 frm = new LicensesHistory(LDLApplicationInfo.ApplicationInfo.ApplicantPersonID);
                frm.ShowDialog();
            }
            else
            {
                frm = new LicensesHistory();
                frm.ShowDialog();
            }
                LDLApplicationInfo = null;
        }
    }
}
