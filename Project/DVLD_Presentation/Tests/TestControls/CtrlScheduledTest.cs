using DVLD_Business;
using DVLD_Presentation.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentation
{
    public partial class CtrlScheduledTest : UserControl
    {
        private clcLDLApplicationBusiness _LDLApplicationInfo;
        private clcTestTypeBusiness._enTestType _TestTypeID;
        public clcTestTypeBusiness._enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;
                
                switch(_TestTypeID)
                {
                    case clcTestTypeBusiness._enTestType.VisionTest:
                        {
                            pbTestTypeImage.Image = Resources.Vision_Test_32;
                            break;
                        }

                    case clcTestTypeBusiness._enTestType.WrittenTest:
                        {
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }

                    case clcTestTypeBusiness._enTestType.StreetTest:
                        {
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;
                        }
                }
            }
        }
        public CtrlScheduledTest()
        {
            InitializeComponent();
        }

        public void LoadTestAppointmentInfo(clcTestAppointmentBusiness TestAppointmentInfo)
        {
            _LDLApplicationInfo = clcLDLApplicationBusiness.Find(TestAppointmentInfo.LDLApplicationID);
            if (_LDLApplicationInfo != null )
            {
                lblLocalDrivingLicenseAppID.Text = _LDLApplicationInfo.LDLApplicationID.ToString();
                lblFullName.Text = _LDLApplicationInfo.ApplicationInfo.ApplicantPersonInfo.FirstName + " "
                    + _LDLApplicationInfo.ApplicationInfo.ApplicantPersonInfo.SecondName + " "
                    + _LDLApplicationInfo.ApplicationInfo.ApplicantPersonInfo.ThirdName + " "
                    + _LDLApplicationInfo.ApplicationInfo.ApplicantPersonInfo.LastName;
                lblDrivingClass.Text = _LDLApplicationInfo.LicenseInfo.LicenseName;

                lblTrial.Text = _LDLApplicationInfo.GetTestTrialCount((int)TestAppointmentInfo.TestTypeID).ToString();
                lblDate.Text = TestAppointmentInfo.AppointmentDate.ToString();
                lblFees.Text = TestAppointmentInfo.PaidFees.ToString();
                lblTestID.Text = (TestAppointmentInfo.GetTestID > 0) ? TestAppointmentInfo.GetTestID.ToString() : "Not Take yet";

            }
             
            
        }

    }
}
