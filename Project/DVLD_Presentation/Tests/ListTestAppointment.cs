using DVLD_Business;
using DVLD_Presentation.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD_Business.clcTestTypeBusiness;

namespace DVLD_Presentation
{
    public partial class ListTestAppointment : Form
    {
        private int _LDLApplicationID;
        private clcLDLApplicationBusiness _LDLApplicationInfo;
        private clcTestTypeBusiness._enTestType _TestTypeID;

        private DataTable DtTestAppointment;

        private void _LoadLDLApplicationInfo()
        {
            ctrlLDLApplicationInfo1.LoadLDLApplicationInfo(_LDLApplicationID);

            if(ctrlLDLApplicationInfo1.LDLApplication == null )
            {
                MessageBox.Show("Failed to find Local Driving Application Information, Please try again",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
                _LDLApplicationInfo = clcLDLApplicationBusiness.Find(_LDLApplicationID);
            }    

        }

        private void ShowTestTypeHeader()
        {
            switch(_TestTypeID)
            {
                case clcTestTypeBusiness._enTestType.VisionTest:
                    {
                        this.Text = "Vision Test Appointments";
                        lblTitle.Text = "Vision Test Appointments";
                        pbTestTypeImage.Image = Resources.Vision_Test_32;
                        break;
                    }

                case clcTestTypeBusiness._enTestType.WrittenTest:
                    {
                        this.Text = "Written Test Appointments";
                        lblTitle.Text = "Written Test Appointments";
                        pbTestTypeImage.Image = Resources.Written_Test_512;
                        break;
                    }

                case clcTestTypeBusiness._enTestType.StreetTest:
                    {
                        this.Text = "Street Test Appointments";
                        lblTitle.Text = "Street Test Appointments";
                        pbTestTypeImage.Image = Resources.driving_test_512;
                        break;
                    }

                default:
                    {
                        this.Close ();
                        break;
                    }
            }
        }

        private void _LoadTestAppointmentList()
        {

            DtTestAppointment = clcTestAppointmentBusiness.GetApplicationTestAppointmentsPerTestType(_LDLApplicationID, (int)_TestTypeID);
            dgvLicenseTestAppointments.DataSource = DtTestAppointment;
            lblRecordsCount.Text = dgvLicenseTestAppointments.RowCount.ToString();
            if (dgvLicenseTestAppointments.RowCount > 0 )
            {
                dgvLicenseTestAppointments.Columns[0].HeaderText = "AppointmentID";
                dgvLicenseTestAppointments.Columns[0].Width = 150;

                dgvLicenseTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvLicenseTestAppointments.Columns[1].Width = 400;

                dgvLicenseTestAppointments.Columns[2].HeaderText = "PaidFees";
                dgvLicenseTestAppointments.Columns[2].Width = 150;

                dgvLicenseTestAppointments.Columns[3].HeaderText = "IsLocked";
                dgvLicenseTestAppointments.Columns[3].Width = 200;
            }
        }
        public ListTestAppointment(int LDLApplicationID, clcTestTypeBusiness._enTestType TestTypeID)
        {
            InitializeComponent();

            _LDLApplicationID = LDLApplicationID;
            _TestTypeID = TestTypeID;
        }

        private void ListTestAppointment_Load(object sender, EventArgs e)
        {
            _LoadLDLApplicationInfo();
            ShowTestTypeHeader();
            _LoadTestAppointmentList();
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {

            if(_LDLApplicationInfo.IsThereAnActiveAppointment((int)_TestTypeID))
            {
                MessageBox.Show("Person already have an active appointment, you cann't take another one",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clcTestBusiness LastTest = _LDLApplicationInfo.GetLastTestPerTestType(_TestTypeID);

            if (LastTest == null)
            {
                AddEditAppointment frm1 = new AddEditAppointment(_LDLApplicationID, _TestTypeID);
                frm1.ShowDialog();
                ListTestAppointment_Load(null, null);
                return;
            }

            //if person already passed the test s/he cannot retak it.
            if (LastTest.TestResult == true)
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AddEditAppointment frmAddEditAppointment = new
                AddEditAppointment(_LDLApplicationID, _TestTypeID);
            frmAddEditAppointment.ShowDialog();

            ListTestAppointment_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditAppointment frmAddEditAppointment = new
                AddEditAppointment(_LDLApplicationID, _TestTypeID,
                (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value);
            frmAddEditAppointment.ShowDialog();

            ListTestAppointment_Load(null, null);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TakeTest frm = new TakeTest((int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value, _TestTypeID);
            frm.ShowDialog();
            ListTestAppointment_Load(null, null);
        }
    }
}
