using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clcUsersBusiness
    {
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool isActive { get; set; }
        public clcPersonBusiness Person { get; set; }
        private enum enMode { AddMode = 0, EditMode};
        private enMode Mode = enMode.AddMode;        
        private bool _AddNewUser()
        {
            this.UserID = clcUserData.AddUser(this.PersonID, this.UserName, this.Password, this.isActive);
            return (this.UserID != -1);
        }
        private bool UpdateUser()
        {
            return clcUserData.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.isActive);
        }
        public clcUsersBusiness()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.isActive = false;
            this.Mode = enMode.AddMode;
        }
        private clcUsersBusiness(int userID, int personID, string userName, string password, bool isActive)
        {
            this.UserID = userID;
            this.PersonID = personID;
            this.UserName = userName;
            this.Password = password;
            this.isActive = isActive;
            Person = clcPersonBusiness.Find(personID);
            this.Mode = enMode.EditMode;
        }       
        public static clcUsersBusiness FindByUserID(int UserID)
        {
            string UserName = "", Password = "";
            int PersonID = -1;
            bool isActive = false;
            if(clcUserData.GetUserInfoByUserID(UserID, ref PersonID, ref UserName, ref Password, ref isActive))
            {
                return new clcUsersBusiness(UserID,  PersonID, UserName, Password, isActive);
            }
            else
            {
                return null;
            }
        }
        public static clcUsersBusiness FindByPersonID(int PersonID)
        {
            string UserName = "", Password = "";
            int UserID = -1;
            bool isActive = false;
            if (clcUserData.GetUserInfoByPersonID(PersonID, ref UserID, ref UserName, ref Password, ref isActive))
            {
                return new clcUsersBusiness(UserID, PersonID, UserName, Password, isActive);
            }
            else
            {
                return null;
            }
        }
        public static clcUsersBusiness FindByUsernameAndPassword(string UserName, string Password)
        {
            int UserID = -1, PersonID = -1;
            bool IsActive = false;

            if(clcUserData.GetUserInfoByUsernameAndPassword(UserName, Password, ref UserID, ref PersonID, ref IsActive))
            {
                return new clcUsersBusiness(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }

        }
        public static DataTable GetAllUsers()
        {
            return clcUserData.GetAllUsers();
        }
        public static bool DeleteUser(int UserID)
        {
            return clcUserData.DeleteUser(UserID);
        }
        public static bool isUserExistByUserID(int UserID)
        {
            return clcUserData.ISUserExistByUserID(UserID);
        }
        public static bool isUserExistByPersonID(int PersonID)
        {
            return clcUserData.ISUserExistByPersonID(PersonID);
        }
        public static bool IsUserNameExist(string UserName)
        {
            return clcUserData.ISUserNameExist(UserName);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddMode:
                    {
                        if (_AddNewUser())
                        {
                            Mode = enMode.EditMode;
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                case enMode.EditMode:
                    {
                        if (UpdateUser())
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
            }

            return false;

        }
    }
}
