using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;
namespace DVLD_Business
{
    public class clcApplicationTypesBusiness
    {
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeName { get; set; }
        public float ApplicatinTypeFees { get; set; }
        public clcApplicationTypesBusiness()
        {
            ApplicationTypeID = 0;
            ApplicationTypeName = "";
            ApplicatinTypeFees = 0;
        }
        private clcApplicationTypesBusiness(int ApplicationTypeID, string ApplicationTypeName, float ApplicatinTypeFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeName = ApplicationTypeName;
            this.ApplicatinTypeFees = ApplicatinTypeFees;
        }
        public static clcApplicationTypesBusiness Find(int ApplicationTypeID)
        {

            string ApplicationTypeName = "";
            float ApplicatinTypeFees = 0;

            if (clcApplicationTypesData.GetApplicationTypeByID(ApplicationTypeID, ref ApplicationTypeName, ref ApplicatinTypeFees))
            {
                return new clcApplicationTypesBusiness(ApplicationTypeID, ApplicationTypeName, ApplicatinTypeFees);
            }
            else
            {
                return null;
            }
        }
        public static DataTable GetAllApplicationTypes()
        {
            return clcApplicationTypesData.GetAllApplicationTypes();
        }
        public bool Update()
        {
            return clcApplicationTypesData.UpdateApplicationType(this.ApplicationTypeID, this.ApplicationTypeName, this.ApplicatinTypeFees);
        }

        public static float GetApplicationTypeFees(int ApplicationTypeID)
        {
            return clcApplicationTypesData.GetApplicationTypeFees(ApplicationTypeID);
        }
    }
}
