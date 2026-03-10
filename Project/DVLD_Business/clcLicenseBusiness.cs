using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;
namespace DVLD_Business
{
    public class clcLicenseBusiness
    {
        public enum enIssueReason { FirstTime = 1, Renew = 2, ReplacementForDamaged = 3,
            ReplacementForLost = 4};

        public enIssueReason IssueReason = enIssueReason.Renew;

        public enum enMode { AddMode =  1, EditMode = 2 };
        public enMode Mode = enMode.AddMode;

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public clcApplicationBusiness ApplicationInfo;

        public int DriverID {  get; set; }
        public clcDriverBusiness DriverInfo;

        public int LicenseClassID {  get; set; }
        public clcLicenseClassesBusiness LicenseClassesInfo;

        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public bool IsActive { get; set; }
        public bool IsLicenseDetain
        {
            get { return clcDetainLicensesBusiness.IsLicenseDetain(this.LicenseID);}
        }

        public float GetFineFeesForDetainedLicense
        {
            get { return clcDetainLicenseData.GetFineFeesForDetainedLicense(this.LicenseID); }
        }

        public int CreatedByUserID { get; set; }
        public clcUsersBusiness CreatedByUserInfo;

        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(this.IssueReason);
            }
        }

        public clcLicenseBusiness()
        {
            IssueReason = enIssueReason.Renew;
            Mode = enMode.AddMode;

            LicenseID = 0;
            
            ApplicationID = 0;
            ApplicationInfo = new clcApplicationBusiness();
            
            DriverID = 0;
            DriverInfo = new clcDriverBusiness();

            LicenseClassID = 0;
            LicenseClassesInfo = new clcLicenseClassesBusiness();

            IssueDate = DateTime.MinValue;
            ExpirationDate = DateTime.MinValue;
            Notes = string.Empty;
            PaidFees = 0;
            IsActive = false;
            
            CreatedByUserID = 0;
            CreatedByUserInfo = new clcUsersBusiness();
        }
        private clcLicenseBusiness(int LicenseID, int ApplicationID, int DriverID,
            int LicenseClassID, DateTime IssueDate, DateTime ExpirationDate, string Notes,
            float PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
        {
            this.IssueReason = (enIssueReason)IssueReason;
            Mode = enMode.EditMode;
     
            this.LicenseID = LicenseID;

            this.ApplicationID = ApplicationID;
            this.ApplicationInfo = clcApplicationBusiness.Find(this.ApplicationID);
         
            this.DriverID = DriverID;
            this.DriverInfo = clcDriverBusiness.FindByDriverID(DriverID);
    
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassesInfo = clcLicenseClassesBusiness.Find(LicenseClassID);
        
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clcUsersBusiness.FindByUserID(CreatedByUserID);
        }

        public static clcLicenseBusiness Find(int LicenseID)
        {
            int ApplicationID = 0, DriverID = 0, IssueReason = 0, CreatedByUserID = 0, LicenseClassID = 0;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0;
            bool IsActive = false;  

            if(clcLicenseData.GetLicensesByLicenseID(LicenseID, ref ApplicationID, ref DriverID,
            ref LicenseClassID, ref IssueDate, ref ExpirationDate, ref Notes,
            ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clcLicenseBusiness(LicenseID, ApplicationID, DriverID,
                     LicenseClassID, IssueDate, ExpirationDate, Notes,
                     PaidFees, IsActive, IssueReason, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }
        public static clcLicenseBusiness FindByApplicationID(int ApplicationID)
        {
            int LicenseID = 0, DriverID = 0, IssueReason = 0, CreatedByUserID = 0, LicenseClassID = 0;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = string.Empty;
            float PaidFees = 0;
            bool IsActive = false;

            if (clcLicenseData.GetLicensesByApplicationID(ApplicationID , ref LicenseID, ref DriverID,
            ref LicenseClassID, ref IssueDate, ref ExpirationDate, ref Notes,
            ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clcLicenseBusiness(LicenseID, ApplicationID, DriverID,
                     LicenseClassID, IssueDate, ExpirationDate, Notes,
                     PaidFees, IsActive, IssueReason, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }
        public bool AddNewLicense()
        {
            this.LicenseID = clcLicenseData.AddNewLicense(ApplicationID, DriverID, LicenseClassID, IssueDate,
                ExpirationDate, Notes, PaidFees, IsActive, (int)IssueReason, CreatedByUserID);

            return this.LicenseID > 0;
        }

        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return clcLicenseData.IsLicenseExistByPersonID(PersonID, LicenseClassID);
        }
        public static int GetLicenseByPersonID(int PersonID, int LicenseClassID)
        {
            return clcLicenseData.GetLicenseByPersonID(PersonID, LicenseClassID);
        }
        public static string GetIssueReasonText(enIssueReason IssueReason)
        {

            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.ReplacementForDamaged:
                    return "Replacement for Damaged";
                case enIssueReason.ReplacementForLost:
                    return "Replacement for Lost";
                default:
                    return "First Time";
            }
        }

        public bool DeactivateLicense()
        {
            return clcLicenseData.DeactivateLicense(this.LicenseID);
        }
        public bool isLicenseExpirated()
        {
            return this.ExpirationDate < DateTime.Now;
        }

        public clcLicenseBusiness RenewLicense(string notes, int CreatedUserID)
        {

            if(!(this.IsActive && this.isLicenseExpirated()))
                return null;

            clcApplicationBusiness _RenewApplicationInfo = new clcApplicationBusiness();
            _RenewApplicationInfo.ApplicationDate = DateTime.Now;
            _RenewApplicationInfo.LastStatusDate = DateTime.Now;
            _RenewApplicationInfo.GreatedUserID = CreatedUserID;
            _RenewApplicationInfo.GreatedUserInfo = clcUsersBusiness.FindByUserID(CreatedUserID);
            _RenewApplicationInfo.ApplicantPersonID = this.ApplicationInfo.ApplicantPersonID;
            _RenewApplicationInfo.ApplicantPersonInfo = clcPersonBusiness.Find(_RenewApplicationInfo.ApplicantPersonID);
            _RenewApplicationInfo.ApplicationStatus = clcApplicationBusiness._enStatus.New;

            _RenewApplicationInfo.ApplicationTypeID = (int)clcApplicationBusiness.enApplicationType.RenewDrivingLicense;
            _RenewApplicationInfo.ApplicationTypeInfo = clcApplicationTypesBusiness.Find(_RenewApplicationInfo.ApplicationTypeID);
            _RenewApplicationInfo.PaidFees = _RenewApplicationInfo.ApplicationTypeInfo.ApplicatinTypeFees;

            if(!_RenewApplicationInfo.Save())
                return null;

            clcLicenseBusiness NewLicesne = new clcLicenseBusiness();

            NewLicesne.ApplicationID = _RenewApplicationInfo.ApplicationID;
            NewLicesne.ApplicationInfo = clcApplicationBusiness.Find(NewLicesne.ApplicationID);

            NewLicesne.DriverID = this.DriverInfo.DriverID;
            NewLicesne.DriverInfo = clcDriverBusiness.FindByDriverID(NewLicesne.DriverID);

            NewLicesne.LicenseClassID = this.LicenseClassesInfo.LicenseID;
            NewLicesne.LicenseClassesInfo = clcLicenseClassesBusiness.Find(NewLicesne.LicenseClassID);

            NewLicesne.IssueDate = DateTime.Now;
            NewLicesne.ExpirationDate = DateTime.Now.AddYears(NewLicesne.LicenseClassesInfo.DefaultValidityLength);
            NewLicesne.PaidFees = NewLicesne.LicenseClassesInfo.ClassFees;

            NewLicesne.Notes = notes;
            NewLicesne.IsActive = true;
            NewLicesne.IssueReason = clcLicenseBusiness.enIssueReason.Renew;

            NewLicesne.CreatedByUserID = CreatedUserID;
            NewLicesne.CreatedByUserInfo = clcUsersBusiness.FindByUserID(NewLicesne.CreatedByUserID);

            if (!DeactivateLicense())
                return null;

            if(!NewLicesne.AddNewLicense())
                return null;

            _RenewApplicationInfo.CompleteApplication();

            return NewLicesne;
        }
        public clcLicenseBusiness ReplaceLicense(enIssueReason IssueReason, clcApplicationBusiness.enApplicationType ApplicationType,
            int CreatedUserID)
        {
            if (!this.IsActive || this.isLicenseExpirated())
                return null;

            clcApplicationBusiness ReplaceApplicationInfo = new clcApplicationBusiness();
            ReplaceApplicationInfo.ApplicationDate = DateTime.Now;
            ReplaceApplicationInfo.LastStatusDate = DateTime.Now;
            ReplaceApplicationInfo.GreatedUserID = CreatedUserID;
            ReplaceApplicationInfo.GreatedUserInfo = clcUsersBusiness.FindByUserID(CreatedUserID);
            ReplaceApplicationInfo.ApplicantPersonID = this.ApplicationInfo.ApplicantPersonID;
            ReplaceApplicationInfo.ApplicantPersonInfo = clcPersonBusiness.Find(ReplaceApplicationInfo.ApplicantPersonID);
            ReplaceApplicationInfo.ApplicationStatus = clcApplicationBusiness._enStatus.New;

            ReplaceApplicationInfo.ApplicationTypeID = (int)ApplicationType;
            ReplaceApplicationInfo.ApplicationTypeInfo = clcApplicationTypesBusiness.Find(ReplaceApplicationInfo.ApplicationTypeID);
            ReplaceApplicationInfo.PaidFees = ReplaceApplicationInfo.ApplicationTypeInfo.ApplicatinTypeFees;

            if (!ReplaceApplicationInfo.Save())
                return null;

            clcLicenseBusiness NewLicesne = new clcLicenseBusiness();

            NewLicesne.ApplicationID = ReplaceApplicationInfo.ApplicationID;
            NewLicesne.ApplicationInfo = clcApplicationBusiness.Find(NewLicesne.ApplicationID);

            NewLicesne.DriverID = this.DriverInfo.DriverID;
            NewLicesne.DriverInfo = clcDriverBusiness.FindByDriverID(NewLicesne.DriverID);

            NewLicesne.LicenseClassID = this.LicenseClassesInfo.LicenseID;
            NewLicesne.LicenseClassesInfo = clcLicenseClassesBusiness.Find(NewLicesne.LicenseClassID);

            NewLicesne.IssueDate = DateTime.Now;
            NewLicesne.ExpirationDate = this.ExpirationDate;
            NewLicesne.PaidFees = NewLicesne.LicenseClassesInfo.ClassFees;

            NewLicesne.Notes = this.Notes;
            NewLicesne.IsActive = true;
            NewLicesne.IssueReason = IssueReason;

            NewLicesne.CreatedByUserID = CreatedUserID;
            NewLicesne.CreatedByUserInfo = clcUsersBusiness.FindByUserID(NewLicesne.CreatedByUserID);

            if (!DeactivateLicense())
                return null;

            if (!NewLicesne.AddNewLicense())
                return null;

            ReplaceApplicationInfo.CompleteApplication();

            return NewLicesne;
        }
        
        public int DetainLicense(DateTime DetainDate, float FineFees, int CreatedByUserID)
        {
            if(IsLicenseDetain || !IsActive)
            {
                return 0;
            }

            clcDetainLicensesBusiness DetainLicenseInfo = new clcDetainLicensesBusiness();
            
            DetainLicenseInfo.LicenseID = this.LicenseID;
            DetainLicenseInfo.LicenseInfo = clcLicenseBusiness.Find(this.LicenseID);

            DetainLicenseInfo.DetainDate = DetainDate;
            DetainLicenseInfo.FineFees = FineFees;
            DetainLicenseInfo.IsReleased = false;

            DetainLicenseInfo.CreatedByUserID = CreatedByUserID;
            DetainLicenseInfo.CreatedUserInfo = clcUsersBusiness.FindByUserID(CreatedByUserID);

            if(!DetainLicenseInfo.DetainLicense())
                return 0;

            int DetainID = DetainLicenseInfo.DetainID;

            return DetainID;
        }
        public int ReleaseDetainedLicense(int DetainID, int ReleaseByUserID)
        {
            if(!(IsLicenseDetain && IsActive))
            {
                return 0;
            }

            clcDetainLicensesBusiness DetainLicenseInfo = clcDetainLicensesBusiness.findByDetainID(DetainID);
            if(DetainLicenseInfo == null)
                { return 0; }

            clcApplicationBusiness NewApplicationInfo = new clcApplicationBusiness();
            NewApplicationInfo.ApplicationDate = DateTime.Now;
            NewApplicationInfo.LastStatusDate = DateTime.Now;
            NewApplicationInfo.GreatedUserID = ReleaseByUserID;
            NewApplicationInfo.GreatedUserInfo = clcUsersBusiness.FindByUserID(ReleaseByUserID);
            NewApplicationInfo.ApplicantPersonID = this.ApplicationInfo.ApplicantPersonID;
            NewApplicationInfo.ApplicantPersonInfo = clcPersonBusiness.Find(NewApplicationInfo.ApplicantPersonID);
            NewApplicationInfo.ApplicationStatus = clcApplicationBusiness._enStatus.New;

            NewApplicationInfo.ApplicationTypeID = (int)clcApplicationBusiness.enApplicationType.ReleaseDetainedDrivingLicsense;
            NewApplicationInfo.ApplicationTypeInfo = clcApplicationTypesBusiness.Find(NewApplicationInfo.ApplicationTypeID);
            NewApplicationInfo.PaidFees = NewApplicationInfo.ApplicationTypeInfo.ApplicatinTypeFees;

            if (!NewApplicationInfo.Save())
                return 0;

            DetainLicenseInfo.ReleaseDate = DateTime.Now;
            DetainLicenseInfo.ReleasedByUserID = ReleaseByUserID;
            DetainLicenseInfo.ReleaseUserInfo = clcUsersBusiness.FindByUserID(ReleaseByUserID) ;
            DetainLicenseInfo.ReleaseApplicationID = NewApplicationInfo.ApplicationID;
            DetainLicenseInfo.ReleaseApplicationInfo = clcApplicationBusiness.Find(NewApplicationInfo.ApplicationID);

            if(!DetainLicenseInfo.ReleaseDetainedLicense())
                return 0;

            NewApplicationInfo.CompleteApplication();

            return NewApplicationInfo.ApplicationID;
        }
        public clcInternationalLicenseBusiness IssueInternationalLicense(int CreatedUserID)
        {

            if (!this.IsActive || this.isLicenseExpirated() ||
                clcInternationalLicenseBusiness.isInternationalLicenseExist(this.LicenseID) || this.LicenseClassesInfo.LicenseID != 3)
                return null;

            clcApplicationBusiness NewApplicationInfo = new clcApplicationBusiness();
            NewApplicationInfo.ApplicationDate = DateTime.Now;
            NewApplicationInfo.LastStatusDate = DateTime.Now;
            NewApplicationInfo.GreatedUserID = CreatedUserID;
            NewApplicationInfo.GreatedUserInfo = clcUsersBusiness.FindByUserID(CreatedUserID);
            NewApplicationInfo.ApplicantPersonID = this.ApplicationInfo.ApplicantPersonID;
            NewApplicationInfo.ApplicantPersonInfo = clcPersonBusiness.Find(NewApplicationInfo.ApplicantPersonID);
            NewApplicationInfo.ApplicationStatus = clcApplicationBusiness._enStatus.New;

            NewApplicationInfo.ApplicationTypeID = (int)clcApplicationBusiness.enApplicationType.NewInternationalLicense;
            NewApplicationInfo.ApplicationTypeInfo = clcApplicationTypesBusiness.Find(NewApplicationInfo.ApplicationTypeID);
            NewApplicationInfo.PaidFees = NewApplicationInfo.ApplicationTypeInfo.ApplicatinTypeFees;

            if (!NewApplicationInfo.Save())
                return null;

            clcInternationalLicenseBusiness newInternationalLicense = new clcInternationalLicenseBusiness();


            newInternationalLicense.ApplicationID = NewApplicationInfo.ApplicationID;
            newInternationalLicense.ApplicationInfo = clcApplicationBusiness.Find(newInternationalLicense.ApplicationID);

            newInternationalLicense.DriverID = this.DriverInfo.DriverID;
            newInternationalLicense.DriverInfo = clcDriverBusiness.FindByDriverID(newInternationalLicense.DriverID);

            newInternationalLicense.IssuedUsingLocalLicenseID = this.LicenseID;
            newInternationalLicense.LicenseInfo = clcLicenseBusiness.Find(newInternationalLicense.IssuedUsingLocalLicenseID);

            newInternationalLicense.IssueDate = DateTime.Now;
            newInternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);

            newInternationalLicense.IsActive = true;

            newInternationalLicense.CreatedByUserID = CreatedUserID;
            newInternationalLicense.CreatedByUserInfo = clcUsersBusiness.FindByUserID(newInternationalLicense.CreatedByUserID);

            if(!newInternationalLicense.IssueInternationalLicense())
                return null;

            NewApplicationInfo.CompleteApplication();

            return newInternationalLicense;
        }
        public static DataTable GetDriverLicense(int DriverID)
        {
            return clcLicenseData.GetDriverLicense(DriverID);
        }
    }
}
