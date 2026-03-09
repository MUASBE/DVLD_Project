using DVLD_Business;
using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Business.clcTestAppointmentBusiness;

namespace DVLD_Business
{
    public class clcDriverBusiness
    {
        public int DriverID { get; set; }

        public int PersonID { get; set; }
        public clcPersonBusiness PersonInfo;
        public int CreatedByUserID { get; set; }
        public clcUsersBusiness CreatedByUserInfo;
        public DateTime CreatedDate { get; set; }

        public clcDriverBusiness()
        {
            this.DriverID = 0;
            this.PersonID = 0;
            this.CreatedByUserID = 0;
            this.CreatedByUserInfo = new clcUsersBusiness();
            this.CreatedDate = DateTime.Now;
        }
        private clcDriverBusiness(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate) 
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.PersonInfo = clcPersonBusiness.Find(PersonID);
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clcUsersBusiness.FindByUserID(this.CreatedByUserID);
            this.CreatedDate = CreatedDate;
        }

        public static clcDriverBusiness FindByDriverID(int DriverID)
        {
            int PersonID = 0, CreatedByUserID = 0;
            DateTime CreatedDate = DateTime.MinValue;

            if(clcDriverData.GetDriverInfoByDriverID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
            {
               
                return new clcDriverBusiness(DriverID, PersonID, CreatedByUserID, CreatedDate);
                

            }
            else
            {
                return null;
            }

        }

        public static clcDriverBusiness FindByPersonID(int PersonID)
        {
            int DriverID = 0, CreatedByUserID = 0;
            DateTime CreatedDate = DateTime.MinValue;

            if (clcDriverData.GetDriverInfoByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clcDriverBusiness(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
            {
                return null;
            }

        }

        public bool AddNewDriver()
        {
            this.DriverID = clcDriverData.AddDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);
            return this.DriverID > 0;
        }
        public static bool isPersonDriver(int PersonID)
        {
            return clcDriverData.isPersonDriver(PersonID);
        }
        public static int GetDriverIDByPersonID(int PersonID)
        {
            return clcDriverData.GetDriverIDByPersonID(PersonID);
        }
        public static DataTable GetAllDrivers()
        {
            return clcDriverData.GetAllDrivers();
        }

        public static int GetPersonIDByDriverID(int DriverID)
        {
            return clcDriverData.GetPersonIDByDriverID(DriverID);
        }
    }
}
