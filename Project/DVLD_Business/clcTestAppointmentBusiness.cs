using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;
namespace DVLD_Business
{
    public class clcTestAppointmentBusiness
    {
        public int TestAppointmentID { get; set; }
        public int LDLApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public clcTestTypeBusiness._enTestType TestTypeID { get; set; }
        public clcTestTypeBusiness TestTypeInfo; 
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clcUsersBusiness CreatedByUserInfo;
        public bool isLocked { get; set; }
        public int retakeTestApplicationID { get; set; }
        public clcApplicationBusiness RetakeTestApplicationInfo;

        public enum _enMode { AddMode = 1, UpdateMode = 2 }
        private _enMode Mode = _enMode.AddMode;

        public int GetTestID
        {
            get
            {
                return _GetTestID();
            }
        }
        private bool AddTestAppointment()
        {
            this.TestAppointmentID =  clcTestAppointmentData.AddTestAppointment((int)this.TestTypeID, this.LDLApplicationID,
                this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.isLocked, this.retakeTestApplicationID);
            return TestAppointmentID > 0;
        }
        private bool UpdateTestAppointment()
        {
            return clcTestAppointmentData.UpdateTestAppointment(this.TestAppointmentID, (int)this.TestTypeID, this.LDLApplicationID,
                this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.isLocked, this.retakeTestApplicationID);
        }
        public clcTestAppointmentBusiness()
        {
            this.TestAppointmentID =0;
            this.LDLApplicationID =0;
            this.TestTypeID = clcTestTypeBusiness._enTestType.VisionTest;
            TestTypeInfo = new clcTestTypeBusiness();

            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            CreatedByUserInfo = new clcUsersBusiness();
            this.isLocked = false;
            this.retakeTestApplicationID = -1;
            RetakeTestApplicationInfo = new clcApplicationBusiness();

            Mode = _enMode.AddMode;
        }
        
        private clcTestAppointmentBusiness(
            int TestAppointmentID, int TestType, int LDLApplicationID,
            DateTime AppointmentDate, float PaidFees, int CreatedByUserID,
            bool isLocked, int retakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = (clcTestTypeBusiness._enTestType)TestType;
            TestTypeInfo = clcTestTypeBusiness.Find((int)TestTypeID);
            this.LDLApplicationID = LDLApplicationID;
            
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            CreatedByUserInfo = clcUsersBusiness.FindByUserID(CreatedByUserID);
            this.isLocked = isLocked;
            this.retakeTestApplicationID = retakeTestApplicationID;
            if (retakeTestApplicationID != -1)
            {
                RetakeTestApplicationInfo = clcApplicationBusiness.Find(retakeTestApplicationID);
            }
            else
            {
                RetakeTestApplicationInfo = null;
            }

            Mode = _enMode.UpdateMode;
        }

        public static clcTestAppointmentBusiness FindByTestAppointmentID(int TestAppointmentID)
        {
            int LDLApplicationID = 0, CreatedByUserID = 0, retakeTestApplicationID=0, TestTypeID = 0;
            DateTime AppointmentDate = DateTime.MinValue;
            bool isLocked = false;
            float PaidFees = 0;

            if(clcTestAppointmentData.GetTestAppointmentByTestAppointmentsID
                (TestAppointmentID, ref TestTypeID, ref LDLApplicationID, ref AppointmentDate, ref PaidFees,
                ref CreatedByUserID, ref isLocked, ref retakeTestApplicationID))
            {
                return new clcTestAppointmentBusiness(TestAppointmentID, TestTypeID, LDLApplicationID,
                    AppointmentDate, PaidFees, CreatedByUserID, isLocked, retakeTestApplicationID);
            }
            else
            {
                return null;
            }

        }

        public static clcTestAppointmentBusiness FindByLastTestAppointmentLDLApplicationID(int LDLApplicationID,
            clcTestTypeBusiness._enTestType TestTypeID)
        {
            int TestAppointmentID = 0, CreatedByUserID = 0, retakeTestApplicationID = 0;
            DateTime AppointmentDate = DateTime.MinValue;
            bool isLocked = false;
            float PaidFees = 0;

            if (clcTestAppointmentData.GetLastTestAppointmentByLDLApplicationID
                (LDLApplicationID, (int) TestTypeID, ref TestAppointmentID, ref AppointmentDate, ref PaidFees,
                ref CreatedByUserID, ref isLocked, ref retakeTestApplicationID))
            {
                return new clcTestAppointmentBusiness(TestAppointmentID, (int)TestTypeID, LDLApplicationID,
                    AppointmentDate, PaidFees, CreatedByUserID, isLocked, retakeTestApplicationID);
            }
            else
            {
                return null;
            }

        }
        public bool Save()
        {
            switch (Mode)
            {
                case _enMode.AddMode:
                    {
                        if (AddTestAppointment())
                        {
                            Mode = _enMode.UpdateMode;
                            return true;
                        }
                        return false;
                    }
                case _enMode.UpdateMode:
                    {
                        if (UpdateTestAppointment())
                        {
                            return true;
                        }
                        return false;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        public static DataTable GetAllTestAppointments()
        {
            return clcTestAppointmentData.GetAllTestAppointments();
        }

        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clcTestAppointmentData.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID, TestTypeID);
        }
        public DataTable GetApplicationTestAppointmentsPerTestType(clcTestTypeBusiness._enTestType TestTypeID)
        {
            return clcTestAppointmentData.GetApplicationTestAppointmentsPerTestType(this.LDLApplicationID, (int)TestTypeID);

        }
        private int _GetTestID()
        {
            return clcTestAppointmentData.GetTestID(this.TestAppointmentID);
        }
    }
}
