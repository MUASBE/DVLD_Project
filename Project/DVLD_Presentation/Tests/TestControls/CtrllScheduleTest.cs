using DVLD_Business;
using DVLD_Presentation.Properties;
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
    public partial class CtrllScheduleTest : UserControl
    {
        
        public enum enMode {AddMode = 0, EditMode = 1};
        private enMode _Mode = enMode.AddMode;

        private int _LDLApplicationID = -1;
        private clcLDLApplicationBusiness LDLApplicationInfo;

        private clcTestTypeBusiness._enTestType _TestTypeID = clcTestTypeBusiness._enTestType.VisionTest;
        private clcTestTypeBusiness TestTypeInfo;

        private int _TestAppointmentID = -1;
        private clcTestAppointmentBusiness TestAppointmentInfo;

        private int _RetakeTestApplicationID = -1;
        private clcApplicationBusiness _RetakeTestApplicationInfo;
        public clcTestTypeBusiness._enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {
                    case clcTestTypeBusiness._enTestType.VisionTest:
                        {
                            gbTestType.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.Vision_Test_32;
                             break;
                        }
                        
                    case clcTestTypeBusiness._enTestType.WrittenTest:
                        {
                            gbTestType.Text = "Written Test";
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }
                    case clcTestTypeBusiness._enTestType.StreetTest:
                        {
                            gbTestType.Text = "Street Test";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;
                        }
                }

            }
        }

        public enum enCreationMode { ScheduleTestFirstTime = 0, RetakeTest = 1};
        private enCreationMode _CreationMode = enCreationMode.ScheduleTestFirstTime;

        private bool _LoadDataInControl()
        {

            TestAppointmentInfo = clcTestAppointmentBusiness.FindByTestAppointmentID(_TestAppointmentID);
            if (TestAppointmentInfo == null)
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID.ToString(),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

            if(DateTime.Compare(DateTime.Now, TestAppointmentInfo.AppointmentDate) < 0)
                dtpTestDate.MinDate = DateTime.Now;
            else
                dtpTestDate.MinDate = TestAppointmentInfo.AppointmentDate;

            if(TestAppointmentInfo.retakeTestApplicationID != -1)
            {
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeAppFees.Text = TestAppointmentInfo.RetakeTestApplicationInfo.PaidFees.ToString();
                lblRetakeTestAppID.Text = TestAppointmentInfo.retakeTestApplicationID.ToString();
            }
            else
            {
                gbRetakeTestInfo.Enabled = false ;
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }

                return true;
        }
        private bool HandleActiveAppointment()
        {
            if(_Mode == enMode.AddMode && LDLApplicationInfo.IsThereAnActiveAppointment((int)_TestTypeID))
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "Person already have an active appointment";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                gbRetakeTestInfo.Enabled = false;
                return false;
            }
            else
                lblUserMessage.Visible = false;
            return true;
        }
        private bool HandleAppointmentLocked()
        {
            if(TestAppointmentInfo.isLocked)
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "Person already sat for this test, you can not take a new appointment";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                gbRetakeTestInfo.Enabled = false;
                return false;
            }

            else
                lblUserMessage.Visible = false;

            return true;
        }
        private bool HandlePreviousTestAppointment()
        {
            switch (TestTypeID)
            {
                case clcTestTypeBusiness._enTestType.VisionTest:
                    //in this case no required prvious test to pass.
                    lblUserMessage.Visible = false;

                    return true;

                case clcTestTypeBusiness._enTestType.WrittenTest:
                    //Written Test, you cannot sechdule it before person passes the vision test.
                    //we check if pass visiontest 1.
                    if (!LDLApplicationInfo.DoesPassTestType((int)clcTestTypeBusiness._enTestType.VisionTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Vision Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }


                    return true;

                case clcTestTypeBusiness._enTestType.StreetTest:

                    //Street Test, you cannot sechdule it before person passes the written test.
                    //we check if pass Written 2.
                    if (!LDLApplicationInfo.DoesPassTestType((int)clcTestTypeBusiness._enTestType.WrittenTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Written Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }


                    return true;

            }
            return true;
        }

        public CtrllScheduleTest()
        {
            InitializeComponent();
        }


        public void LoadTestAppointments_View(int LDLApplicationID,
            clcTestTypeBusiness._enTestType TestTypeID, int TestAppointmentID = -1)
        {

            _Mode = (TestAppointmentID == -1) ? enMode.AddMode : enMode.EditMode;

            _LDLApplicationID = LDLApplicationID;
            _TestAppointmentID = TestAppointmentID;
            _TestTypeID = TestTypeID;

            TestTypeInfo = clcTestTypeBusiness.Find((int)_TestTypeID);
            LDLApplicationInfo = clcLDLApplicationBusiness.Find(_LDLApplicationID);

            if( LDLApplicationInfo == null )
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LDLApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            if (LDLApplicationInfo.DoesAttendTestType((int)TestTypeID))
                _CreationMode = enCreationMode.RetakeTest;
            else
                _CreationMode = enCreationMode.ScheduleTestFirstTime;

            if(_CreationMode == enCreationMode.RetakeTest && _Mode == enMode.AddMode)
            {
                _RetakeTestApplicationInfo = new clcApplicationBusiness();
                _RetakeTestApplicationInfo.ApplicantPersonID = LDLApplicationInfo.ApplicationInfo.ApplicantPersonID;
                _RetakeTestApplicationInfo.ApplicantPersonInfo = clcPersonBusiness.Find(_RetakeTestApplicationInfo.ApplicantPersonID);
                _RetakeTestApplicationInfo.ApplicationDate = DateTime.Now;
                _RetakeTestApplicationInfo.ApplicationTypeID = (int)clcApplicationBusiness.enApplicationType.RetakeTest;
                _RetakeTestApplicationInfo.ApplicationTypeInfo = clcApplicationTypesBusiness.Find(_RetakeTestApplicationInfo.ApplicationTypeID);
                _RetakeTestApplicationInfo.GreatedUserInfo = clcGlobal.GolbalUser;
                _RetakeTestApplicationInfo.GreatedUserID = clcGlobal.GolbalUser.UserID;
                _RetakeTestApplicationInfo.PaidFees = _RetakeTestApplicationInfo.ApplicationTypeInfo.ApplicatinTypeFees;
                _RetakeTestApplicationInfo.LastStatusDate = DateTime.Now;
                _RetakeTestApplicationInfo.ApplicationStatus = clcApplicationBusiness._enStatus.Completed;

                if (!_RetakeTestApplicationInfo.Save())
                    return;

                _RetakeTestApplicationID = _RetakeTestApplicationInfo.ApplicationID;

                lblRetakeAppFees.Text = _RetakeTestApplicationInfo.PaidFees.ToString();

                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = "0";
            }
            else
            {
                gbRetakeTestInfo.Enabled = false;
                lblTitle.Text = "Schedule Test";
                lblRetakeTestAppID.Text = "N/A";
            }
            
            lblLocalDrivingLicenseAppID.Text = _LDLApplicationID.ToString();
            lblDrivingClass.Text = LDLApplicationInfo.LicenseInfo.LicenseName;
            lblFullName.Text = LDLApplicationInfo.ApplicationInfo.ApplicantPersonInfo.FirstName + " "
                + LDLApplicationInfo.ApplicationInfo.ApplicantPersonInfo.SecondName + " "
                + LDLApplicationInfo.ApplicationInfo.ApplicantPersonInfo.ThirdName + " "
                + LDLApplicationInfo.ApplicationInfo.ApplicantPersonInfo.LastName;

            lblTrial.Text = LDLApplicationInfo.GetTestTrialCount((int)_TestTypeID).ToString();

            lblFees.Text = TestTypeInfo.TestTypeFees.ToString();

            if(_RetakeTestApplicationID != -1)
            {
                lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeAppFees.Text)).ToString();
                lblRetakeTestAppID.Text = _RetakeTestApplicationID.ToString();
            }
            else
            {
                lblTotalFees.Text = lblFees.Text;
            }

            if (_Mode == enMode.AddMode)
            {
                dtpTestDate.MinDate = DateTime.Now;
                TestAppointmentInfo = new clcTestAppointmentBusiness();

            }
            else
            {
                if (!_LoadDataInControl())
                    return;
            }

            if (!HandleActiveAppointment())
                return;

            if(!HandleAppointmentLocked())
                return;

            if (!HandlePreviousTestAppointment())
                return;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TestAppointmentInfo.LDLApplicationID = _LDLApplicationID;
            TestAppointmentInfo.TestTypeID = _TestTypeID;
            TestAppointmentInfo.TestTypeInfo = TestTypeInfo;
            TestAppointmentInfo.AppointmentDate = dtpTestDate.Value;
            TestAppointmentInfo.CreatedByUserID = clcGlobal.GolbalUser.UserID;
            TestAppointmentInfo.CreatedByUserInfo = clcGlobal.GolbalUser;
            TestAppointmentInfo.isLocked = false;
            TestAppointmentInfo.PaidFees = Convert.ToSingle(lblTotalFees.Text);
            TestAppointmentInfo.retakeTestApplicationID = _RetakeTestApplicationID;

            if (TestAppointmentInfo.Save())
            {
                _Mode = enMode.EditMode;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
