using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DVLD_Business
{
    public class clcDetainLicensesBusiness
    {
        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public clcLicenseBusiness LicenseInfo;
        public DateTime DetainDate { get; set; }
        public float FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clcUsersBusiness CreatedUserInfo;
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public clcUsersBusiness ReleaseUserInfo;

        public int ReleaseApplicationID { get; set; }
        public clcApplicationBusiness ReleaseApplicationInfo;

        public clcDetainLicensesBusiness()
        {
            this.DetainID = 0;

            this.LicenseID = 0;
            this.LicenseInfo = new clcLicenseBusiness();

            this.DetainDate = DateTime.MinValue;
            this.FineFees = 0;

            this.CreatedByUserID = 0;
            this.CreatedUserInfo = new clcUsersBusiness();

            this.IsReleased = false;
            this.ReleaseDate = DateTime.MinValue;

            this.ReleasedByUserID = 0;
            this.ReleaseUserInfo = new clcUsersBusiness();

            this.ReleaseApplicationID = 0;
            this.ReleaseApplicationInfo = new clcApplicationBusiness();

        }
        private clcDetainLicensesBusiness(int DetainID, int LicenseID, DateTime DetainDate, float FineFees,
             int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;

            this.LicenseID = LicenseID;
            this.LicenseInfo = clcLicenseBusiness.Find(LicenseID);

            this.DetainDate = DetainDate;
            this.FineFees = FineFees;

            this.CreatedByUserID = CreatedByUserID;
            this.CreatedUserInfo = clcUsersBusiness.FindByUserID(CreatedByUserID);

            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;

            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseUserInfo = clcUsersBusiness.FindByUserID(ReleasedByUserID);

            this.ReleaseApplicationID = ReleaseApplicationID;
            this.ReleaseApplicationInfo = clcApplicationBusiness.Find(ReleaseApplicationID);

        }

        public static clcDetainLicensesBusiness findByDetainID(int DetainID)
        {
            int LicenseID = 0, CreatedByUserID = 0, ReleasedByUserID = 0, ReleaseApplicationID = 0;
            DateTime DetainDate = DateTime.MinValue, ReleaseDate = DateTime.MinValue;
            float FineFees = 0;
            bool IsReleased = false;

            if(clcDetainLicenseData.GetDetainLicenseByDetainID(DetainID, ref LicenseID, ref DetainDate, ref FineFees,
             ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clcDetainLicensesBusiness(DetainID, LicenseID, DetainDate, FineFees,
                         CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            else
            {
                return null;
            }

        }
        public static clcDetainLicensesBusiness findByLicenseID(int LicenseID)
        {
            int DetainID = 0, CreatedByUserID = 0, ReleasedByUserID = 0, ReleaseApplicationID = 0;
            DateTime DetainDate = DateTime.MinValue, ReleaseDate = DateTime.MinValue;
            float FineFees = 0;
            bool IsReleased = false;

            if (clcDetainLicenseData.GetDetainLicenseByLicenseID(LicenseID, ref DetainID, ref DetainDate, ref FineFees,
             ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clcDetainLicensesBusiness(DetainID, LicenseID, DetainDate, FineFees,
                         CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            else
            {
                return null;
            }

        }
        public static DataTable GetAllDetainedLicenses()
        {
            return clcDetainLicenseData.GetAllDetainedLicenses();
        }
        public static int GetDetainIDForLicense(int licenseID)
        {
            return clcDetainLicenseData.GetDetainIDForLicense(licenseID);
        }
        public static bool IsLicenseDetain(int licenseID)
        {
            return clcDetainLicenseData.GetDetainIDForLicense(licenseID) > 0;
        }
        public bool DetainLicense()
        {
            this.DetainID = clcDetainLicenseData.DetainLicense(LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased);
            return this.DetainID > 0;
        }
        public bool ReleaseDetainedLicense()
        {
            return clcDetainLicenseData.ReleaseDetainedLicense(this.DetainID, this.ReleasedByUserID, this.ReleaseApplicationID);
        }

    }
}
