using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Business.clcLDLApplicationBusiness;
namespace DVLD_Business
{
    public class clcLDLApplicationBusiness
    {
        public int LDLApplicationID { get; set; }
        public int ApplicationID { get; set; }
        public clcApplicationBusiness ApplicationInfo;
        
        public int LicenseID { get; set; }
        public clcLicenseClassesBusiness LicenseInfo;
        public enum enMode { AddMode = 1, UpdateMode = 2 }
        private enMode Mode = enMode.AddMode;
        private bool AddLDLApplication()
        {
            this.LDLApplicationID = clcLDLApplicationData.AddLDLApplication(this.ApplicationInfo.ApplicationID, this.LicenseInfo.LicenseID);
            return this.LDLApplicationID > 0;
        }
        private bool UpdateLDLApplication()
        {
            return clcLDLApplicationData.UpdateLDLApplication(this.ApplicationInfo.ApplicationID, this.LicenseInfo.LicenseID);
        }
        public clcLDLApplicationBusiness()
        {
            LDLApplicationID = 0;
            ApplicationID = 0;
            ApplicationInfo = new clcApplicationBusiness();
            LicenseID = 0;
            LicenseInfo = new clcLicenseClassesBusiness();
            Mode = enMode.AddMode;
        }
        private clcLDLApplicationBusiness(int lDLApplicationID, int ApplicationID, int licenseID)
        {
            this.LDLApplicationID = lDLApplicationID;
            this.ApplicationID = ApplicationID;
            ApplicationInfo = clcApplicationBusiness.Find(this.ApplicationID);
            this.LicenseID = licenseID;
            LicenseInfo = clcLicenseClassesBusiness.Find(this.LicenseID);
            Mode = enMode.UpdateMode;
        }
        public static clcLDLApplicationBusiness Find(int LDLApplicationID)
        {
            int ApplicationID = 0;
            int licenseID = 0;
            if(clcLDLApplicationData.GetLDLApplicationByID(LDLApplicationID, ref ApplicationID, ref licenseID))
            {
                return new clcLDLApplicationBusiness(LDLApplicationID, ApplicationID, licenseID);
            }
            else
            {
                return null;
            } 
            
        }
        public static DataTable GetAllLDLApplications()
        {
            return clcLDLApplicationData.GetAllLDLApplications();
        }
        public bool Save()
        {

            if(!ApplicationInfo.Save())
                return false;

            switch (Mode)
            {
                case enMode.AddMode:
                    {
                        if (AddLDLApplication())
                        {
                            Mode = enMode.UpdateMode;
                            return true;
                        }
                        return false;
                    }
                case enMode.UpdateMode:
                    {
                        if (UpdateLDLApplication())
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

        static public bool Delete(int LDLApplicationID)
        {
            int ApplicationID = clcLDLApplicationData.GetApplicationIDForActiveLDLApplication(LDLApplicationID);
            if (clcLDLApplicationData.DeleteLDLApplication(LDLApplicationID))
            {
                return clcApplicationData.DeleteApplication(ApplicationID);
            }
            return false;
        }

        public DataTable GetAllAppointmentForLDLApplicationAndTestType(clcTestTypeBusiness._enTestType TestTypeID)
        {
            return clcLDLApplicationData.GetAllAppointmentForLDLApplicationWithTestType(this.LDLApplicationID, (int)TestTypeID);
        }

        public int GetTestTrialCount( int TestTypeID)
        {
            return clcLDLApplicationData.GetTestTrialCount(this.LDLApplicationID, TestTypeID);
        }
        public static int GetTestTrialCount(int LDLApplicationID, int TestTypeID)
        {
            return clcLDLApplicationData.GetTestTrialCount(LDLApplicationID, TestTypeID);
        }

        public bool IsThereAnActiveAppointment(int TestTypeID)
        {
            return clcLDLApplicationData.IsThereAnActiveAppointment(this.LDLApplicationID, TestTypeID);
        }
        public static  bool IsThereAnActiveAppointment(int LDLApplicationID, int TestTypeID)
        {
            return clcLDLApplicationData.IsThereAnActiveAppointment(LDLApplicationID, TestTypeID);
        }

        public bool DoesPassTestType(int TestTypeID)
        {
            return clcLDLApplicationData.DoesPassTestType(this.LDLApplicationID, TestTypeID);
        }

        public static bool DoesPassTestType(int LDLApplicationID, int TestTypeID)
        {
            return clcLDLApplicationData.DoesPassTestType(LDLApplicationID, TestTypeID);
        }

        public bool DoesAttendTestType(int TestTypeID)
        {
            return clcLDLApplicationData.DoesAttendTestType(this.LDLApplicationID, TestTypeID);
        }

        public static bool DoesAttendTestType(int LDLApplicationID, int TestTypeID)
        {
            return clcLDLApplicationData.DoesAttendTestType(LDLApplicationID, TestTypeID);
        }

        public int PassedTestCount()
        {
            return clcLDLApplicationData.PassedTestCount(this.LDLApplicationID);
        }

        public bool isPassedAllTest()
        {
            return clcLDLApplicationData.PassedTestCount(this.LDLApplicationID) == 3;
        }

        public clcTestBusiness GetLastTestPerTestType(clcTestTypeBusiness._enTestType TestTypeID)
        {
            return clcTestBusiness.FindLastTestPerPersonAndLicenseClass(this.ApplicationInfo.ApplicantPersonID, this.LicenseID, TestTypeID);
        }
    }
}
