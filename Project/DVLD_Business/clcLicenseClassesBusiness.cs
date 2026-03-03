using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;
namespace DVLD_Business
{
    public class clcLicenseClassesBusiness
    {
        public int LicenseID { get; set; }
        public string LicenseName { get; set; }
        public string Description { get; set; }
        public int MinimumAllowedAge { get; set; }
        public int DefaultValidityLength { get; set; }
        public float ClassFees { get; set; }
        public clcLicenseClassesBusiness() 
        {
            LicenseID = 0;
            LicenseName = string.Empty;
            Description = string.Empty;
            MinimumAllowedAge = 0;
            DefaultValidityLength = 0;
            ClassFees = 0.0f;
        }
        private clcLicenseClassesBusiness(int licenseID, string licenseName, string description, int minimumAllowedAge, int defaultValidityLength, float classFees)
        {
            this.LicenseID = licenseID;
            this.LicenseName = licenseName;
            this.Description = description;
            this.MinimumAllowedAge = minimumAllowedAge;
            this.DefaultValidityLength = defaultValidityLength;
            this.ClassFees = classFees;
        }

        public static clcLicenseClassesBusiness Find(int licenseID)
        {
            string licenseName = "", description = "";
            int minimumAllowedAge = 0, defaultValidityLength = 0;
            float classFees = 0.0f;

            if(clcLicenseClassesData.GetLicenseInfoByID(licenseID, ref licenseName, ref description,
                ref minimumAllowedAge, ref defaultValidityLength, ref classFees))
            {
                return new clcLicenseClassesBusiness(licenseID, licenseName, description, minimumAllowedAge, defaultValidityLength, classFees);
            }
            else
            {
                return null;
            }

        }
        public static clcLicenseClassesBusiness Find(string licenseName)
        {
            string description = "";
            int licenseID = 0, minimumAllowedAge = 0, defaultValidityLength = 0;
            float classFees = 0.0f;

            if (clcLicenseClassesData.GetLicenseInfoByName(licenseName,ref licenseID, ref description,
                ref minimumAllowedAge, ref defaultValidityLength, ref classFees))
            {
                return new clcLicenseClassesBusiness(licenseID, licenseName, description, minimumAllowedAge, defaultValidityLength, classFees);
            }
            else
            {
                return null;
            }

        }

        public static DataTable GetAllLicenseClasses()
        {
            return clcLicenseClassesData.GetAllLicenseClasses();
        }
    }
}
