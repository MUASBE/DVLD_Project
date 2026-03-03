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

namespace DVLD_Presentation
{
    public partial class TakeTest : Form
    {
        public enum enMode { AddMode, ReadMode};
        private enMode _Mode = enMode.AddMode;

        private int _TestAppointmentID;
        private clcTestAppointmentBusiness _TestAppointmentInfo;
        clcTestTypeBusiness._enTestType _TestTypeID;
        private int _TestID;
        private clcTestBusiness _TestInfo;

        private void _LoadTestData()
        {
            _TestID = _TestAppointmentInfo.GetTestID;
            _TestInfo = clcTestBusiness.FindByTestID(_TestID);

            if(_TestInfo.TestResult)
                rbPass.Checked = true;
            else
                rbFail.Checked = true;

            _TestInfo.Notes = txtNotes.Text;

            btnSave.Enabled = false;
            rbFail.Enabled = false;
            rbPass.Enabled = false;
            txtNotes.Enabled = false;

        }
        
        public TakeTest(int TestAppointmentID, clcTestTypeBusiness._enTestType TestTypeID)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
            _TestTypeID = TestTypeID;
        }

        private void TakeTest_Load(object sender, EventArgs e)
        {
            ctrlScheduledTest1.TestTypeID = _TestTypeID;
            _TestAppointmentInfo = clcTestAppointmentBusiness.FindByTestAppointmentID(_TestAppointmentID);
            if (_TestAppointmentInfo == null)
            {
                MessageBox.Show("Appointment is not exist, take appointment first", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            _Mode = (_TestAppointmentInfo.GetTestID > 0)? enMode.ReadMode : enMode.AddMode;

            ctrlScheduledTest1.LoadTestAppointmentInfo(_TestAppointmentInfo);

            if (_Mode == enMode.AddMode)
                _TestInfo = new clcTestBusiness();
            else
                _LoadTestData();

        }

        private void rbFail_CheckedChanged(object sender, EventArgs e)
        {
            _TestInfo.TestResult = false;
        }

        private void rbPass_CheckedChanged(object sender, EventArgs e)
        {
            _TestInfo.TestResult = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!rbFail.Checked && !rbPass.Checked)
            {
                MessageBox.Show("You should select test result before saving it", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(MessageBox.Show("Are you sure you want to save test information", "confirm",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                _TestInfo.Notes = txtNotes.Text.Trim();
                _TestInfo.TestAppointmentID = _TestAppointmentInfo.TestAppointmentID;
                _TestInfo.CreatedByUserID = clcGlobal.GolbalUser.UserID;

                if(_TestInfo.TakeTest())
                {
                    MessageBox.Show("Test Information was saved successfully", "Saved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error, Test Information was not saved, please try again", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnColse_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
