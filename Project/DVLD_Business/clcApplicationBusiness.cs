using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;
namespace DVLD_Business
{
    public class clcApplicationBusiness
    {
        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public clcPersonBusiness ApplicantPersonInfo;
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public clcApplicationTypesBusiness ApplicationTypeInfo;
        public _enStatus ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        public enum _enStatus { New = 1, Cancelled = 2, Completed = 3 };
        private enum _enMode { AddMode = 1, UpdateMode = 2 }
        private _enMode Mode = _enMode.AddMode;

        public int GreatedUserID { get; set; }
        public clcUsersBusiness GreatedUserInfo { get; set; }
        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };

        private bool AddApplication()
        {
            this.ApplicationID = clcApplicationData.AddApplication(this.ApplicantPersonInfo.PersonID,
                this.ApplicationDate, this.ApplicationTypeInfo.ApplicationTypeID,
                (int)ApplicationStatus, this.LastStatusDate, this.PaidFees, this.GreatedUserInfo.UserID);
            return this.ApplicationID > 0;
        }
        private bool UpdateApplication()
        {
            return clcApplicationData.UpdateApplication(this.ApplicationID, this.ApplicantPersonInfo.PersonID,
                this.ApplicationDate, this.ApplicationTypeInfo.ApplicationTypeID,
                (int)ApplicationStatus, this.LastStatusDate, this.PaidFees, this.GreatedUserInfo.UserID);
        }
        public clcApplicationBusiness()
        {
            this.ApplicationID = 0;
            this.ApplicantPersonID = 0;
            this.ApplicantPersonInfo = new clcPersonBusiness();
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = 0;
            this.ApplicationTypeInfo = new clcApplicationTypesBusiness();
            this.ApplicationStatus = _enStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.GreatedUserID = 0;
            this.GreatedUserInfo = new clcUsersBusiness();
            this.Mode = _enMode.AddMode;
        }

        private clcApplicationBusiness(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate,
            int ApplicationTypeID, int ApplicationStatus, DateTime LastStatusDate, float PaidFees, int GreatedUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicantPersonInfo = clcPersonBusiness.Find(ApplicantPersonID);
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeInfo = clcApplicationTypesBusiness.Find(ApplicationTypeID);
            this.ApplicationStatus = (_enStatus)ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.GreatedUserID = GreatedUserID;
            this.GreatedUserInfo = clcUsersBusiness.FindByUserID(GreatedUserID);
            this.Mode = _enMode.UpdateMode;
        }

        public static clcApplicationBusiness Find(int ApplicationID)
        {
            int ApplicantPersonID = 0;
            DateTime ApplicationDate = DateTime.Now;
            int ApplicationTypeID = 0;
            int ApplicationStatus = 0;
            int GreatedUserID = 0;
            DateTime LastStatusDate = DateTime.Now;
            float PaidFees = 0;
            if (clcApplicationData.GetApplicationByApplicationID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate,
                ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref GreatedUserID))
            {
                return new clcApplicationBusiness(ApplicationID, ApplicantPersonID, ApplicationDate,
                    ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, GreatedUserID);
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
                        if (AddApplication())
                        {
                            Mode = _enMode.UpdateMode;
                            return true;
                        }
                        return false;
                    }
                case _enMode.UpdateMode:
                    {
                        if (UpdateApplication())
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
        public bool CancelApplication()
        {
            return clcApplicationData.UpdateStatus(this.ApplicationID, 2);
        }
        public bool CompleteApplication()
        {
            return clcApplicationData.UpdateStatus(this.ApplicationID, 3);
        }
        public static bool IsApplicantHasSameActiveApplication(int ApplicantPersonID, int ApplicationTypeID)
        {
            return clcApplicationData.IsApplicantHasSameActiveApplication(ApplicantPersonID, ApplicationTypeID);
        }
        public bool IsApplicantHasSameActiveApplication(int ApplicationTypeID)
        {
            return clcApplicationData.IsApplicantHasSameActiveApplication(this.ApplicantPersonID, ApplicationTypeID);
        }
        public static bool DeleteApplication(int ApplicationID)
        {
            return clcApplicationData.DeleteApplication(ApplicationID);
        }
        public static bool IsApplicationExist(int ApplicationID)
        {
            return clcApplicationData.IsApplicationExist(ApplicationID);
        }

        public static int GetActiveApplicationIDForApplicant(int ApplicantPersonID, int ApplicationTypeID , int LicenseID)
        {
            return clcApplicationData.GetActiveApplicationIDForLicenseClass(ApplicantPersonID, ApplicationTypeID, LicenseID);
        }
    }
}
