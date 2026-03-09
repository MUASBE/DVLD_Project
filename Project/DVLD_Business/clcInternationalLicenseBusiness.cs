using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DVLD_Business
{
    public class clcInternationalLicenseBusiness
    {
        public int InternationalLicenseID {  get; set; }
        public int ApplicationID { get; set; }
        public clcApplicationBusiness ApplicationInfo;
        public int DriverID { get; set; }
        public clcDriverBusiness DriverInfo;
        public int IssuedUsingLocalLicenseID { get; set; }
        public clcLicenseBusiness LicenseInfo;
        public int CreatedByUserID { get; set; }
        public clcUsersBusiness CreatedByUserInfo;
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }

        public clcInternationalLicenseBusiness()
        {
            InternationalLicenseID = 0;

            ApplicationID = 0;
            ApplicationInfo = new clcApplicationBusiness();

            DriverID = 0;
            DriverInfo = new clcDriverBusiness();

            IssuedUsingLocalLicenseID = 0;
            LicenseInfo = new clcLicenseBusiness();

            CreatedByUserID = 0;
            CreatedByUserInfo = new clcUsersBusiness();

            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;

            IsActive = false;

        }

        private clcInternationalLicenseBusiness(int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
           int CreatedByUserID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive)
        {
            this.InternationalLicenseID = InternationalLicenseID;

            this.ApplicationID = ApplicationID;
            this.ApplicationInfo = clcApplicationBusiness.Find(ApplicationID);

            this.DriverID = DriverID;
            this.DriverInfo = clcDriverBusiness.FindByDriverID(DriverID);

            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.LicenseInfo = clcLicenseBusiness.Find(IssuedUsingLocalLicenseID);

            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clcUsersBusiness.FindByUserID(CreatedByUserID);

            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;

            this.IsActive = IsActive;
        }

        public static clcInternationalLicenseBusiness FindByInternationalLicenseID(int InternationalLicenseID)
        {
            int ApplicationID = 0, DriverID = 0, IssuedUsingLocalLicenseID = 0, CreatedByUserID = 0;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            bool IsActive = false;

            if(clcInternationalLicenseData.GetInternationalLicenseByInternationalLicenseID(InternationalLicenseID, ref ApplicationID, ref DriverID,
                ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                return new clcInternationalLicenseBusiness(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID,
                    CreatedByUserID, IssueDate, ExpirationDate, IsActive);
            }
            else
            {
                return null;
            }
        }
        public static clcInternationalLicenseBusiness FindByIssuedUsingLocalLicenseID(int IssuedUsingLocalLicenseID)
        {
            int ApplicationID = 0, DriverID = 0, InternationalLicenseID = 0, CreatedByUserID = 0;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            bool IsActive = false;

            if (clcInternationalLicenseData.GetInternationalLicenseByIssuedUsingLocalLicenseID(IssuedUsingLocalLicenseID, 
                ref ApplicationID, ref DriverID, ref InternationalLicenseID, ref IssueDate, ref ExpirationDate,
                ref IsActive, ref CreatedByUserID))
            {
                return new clcInternationalLicenseBusiness(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID,
                    CreatedByUserID, IssueDate, ExpirationDate, IsActive);
            }
            else
            {
                return null;
            }
        }
        public bool IssueInternationalLicense()
        {
            this.InternationalLicenseID = clcInternationalLicenseData.AddInternationalLicense(ApplicationID, DriverID, IssuedUsingLocalLicenseID,
                IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            return this.InternationalLicenseID > 0;
        }

        public static DataTable GetInternationalLicensesByDriverID(int DriverID)
        {
            return clcInternationalLicenseData.GetInternationalLicensesByDriverID(DriverID);
        }
        public static int GetActiverInternationalLicenseID(int IssuedUsingLocalLicenseID)
        {
            return clcInternationalLicenseData.GetActiverInternationalLicenseID(IssuedUsingLocalLicenseID);
        }

        public static DataTable GetInternationalLicensesList()
        {
            return clcInternationalLicenseData.GetInternationalLicensesList();
        }
        public static bool isInternationalLicenseExist(int IssuedUsingLocalLicenseID)
        {
            return GetActiverInternationalLicenseID(IssuedUsingLocalLicenseID) > 0;
        }
    }
}
