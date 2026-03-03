using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clcTestBusiness
    {
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        public clcTestBusiness()
        {
            this.TestID = 0;
            this.TestResult = false;
            
            this.CreatedByUserID = 0;
            
            this.Notes = string.Empty;

            this.TestAppointmentID = 0;
        }
        private clcTestBusiness(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            this.TestID = TestID;
            this.TestResult = TestResult;

            this.CreatedByUserID = CreatedByUserID;
            this.Notes = Notes;

            this.TestAppointmentID = TestAppointmentID;
        }
        public static clcTestBusiness FindLastTestPerPersonAndLicenseClass
            (int PersonID, int LicenseClassID, clcTestTypeBusiness._enTestType TestTypeID)
        {
            int TestID = -1;
            int TestAppointmentID = -1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;

            if (clcTestData.GetLastTestByPersonAndTestTypeAndLicenseClass
                (PersonID, LicenseClassID, (int)TestTypeID, ref TestID,
            ref TestAppointmentID, ref TestResult,
            ref Notes, ref CreatedByUserID))

                return new clcTestBusiness(TestID,
                        TestAppointmentID, TestResult,
                        Notes, CreatedByUserID);
            else
                return null;

        }

        public static clcTestBusiness FindByTestID(int TestID)
        {
            int TestAppointmentID = 0, CreatedByUserID = 0;
            bool TestResult = false;
            string Notes = "";

            if(clcTestData.GetTestByTestID(TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))
            {
                return new clcTestBusiness(TestID, TestAppointmentID, TestResult,Notes, CreatedByUserID);
            }
            else
            {
                return null;
            }

        }

        public static clcTestBusiness FindByTestAppointmentID(int TestAppointmentID)
        {
            int TestID = 0, CreatedByUserID = 0;
            bool TestResult = false;
            string Notes = "";

            if (clcTestData.GetTestByTestAppoinmentID(TestAppointmentID, ref TestID, ref TestResult, ref Notes, ref CreatedByUserID))
            {
                return new clcTestBusiness(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            }
            else
            {
                return null;
            }

        }

        public bool TakeTest()
        {
            this.TestID =  clcTestData.AddTest(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
            return this.TestID > 0;
        }
    }
}
