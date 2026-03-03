using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clcPersonBusiness
    {
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int NationalCountryID { get; set; }
        public string imagePath { get; set; }

        public clcCountryBusiness CountryInfo;
        public short Gendor { get; set; }
        public string Address { get; set; }

        public enum _enMode { AddMode = 1, UpdateMode = 2, DeleteMode = 3 }
        public _enMode Mode = _enMode.AddMode;

        private clcPersonBusiness(int PersonID, string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, string Email,  string Phone, DateTime DateOfBirth, 
            int NationalCountryID,string ImagePath, short Gendor, string Address)
        {
            Mode = _enMode.UpdateMode;
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.DateOfBirth = DateOfBirth;
            this.NationalCountryID = NationalCountryID;
            CountryInfo = clcCountryBusiness.find(NationalCountryID);
            this.imagePath = ImagePath;
            this.Gendor = Gendor;
            this.Address = Address;

        }
        public clcPersonBusiness()
        {
            PersonID = -1;
            NationalNo = null;
            FirstName = null;
            SecondName = null;
            ThirdName = null;
            LastName = null;
            Email = null;
            Phone = null;
            DateOfBirth = DateTime.Now;
            NationalCountryID = 0;
            CountryInfo = new clcCountryBusiness();
            imagePath = null;
            Gendor = 0;
            Address = null;
            Mode = _enMode.AddMode;
        }

        public static DataTable GetAllPersons()
        {
            return DVLD_DataAccess.clcPersonData.GetAllPeople();
        }

        public static clcPersonBusiness Find(int ID)
        {
            string NationalNo = null, FirstName = null, SecondName = null,
            ThirdName = null, LastName = null, Email = null, Phone = null,
            ImagePath = null, Address = null;

            short Gendor = 0;
            DateTime DateOfBirth = DateTime.Now;
            int NationalCountryID = 0;

            if(DVLD_DataAccess.clcPersonData.GetPeronInfoByID(ID, ref NationalNo, ref FirstName,
                ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor,
                ref Address, ref Phone, ref Email, ref NationalCountryID, ref ImagePath))
            {
                return new clcPersonBusiness(ID, NationalNo, FirstName, SecondName,
                    ThirdName, LastName, Email, Phone, DateOfBirth,
                    NationalCountryID, ImagePath, Gendor, Address);
            }

            else
            {
                return null;
            }
        }
        public static clcPersonBusiness Find(string NationalNo)
        {
            string FirstName = null, SecondName = null,
            ThirdName = null, LastName = "", Email = null, Phone = null,
            ImagePath = null, Address = null;

            short Gendor = 0;
            DateTime DateOfBirth = DateTime.Now;
            int PeronID = 0, NationalCountryID = 0;

            if (DVLD_DataAccess.clcPersonData.GetPeronInfoByNationalNo(NationalNo, ref PeronID, ref FirstName,
                ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor,
                ref Address, ref Phone, ref Email, ref NationalCountryID, ref ImagePath))
            {
                return new clcPersonBusiness(PeronID, NationalNo, FirstName, SecondName,
                    ThirdName, LastName, Email, Phone, DateOfBirth,
                    NationalCountryID, ImagePath, Gendor, Address);
            }

            else
            {
                return null;
            }
        }

        private bool AddPerson()
        {
            this.PersonID =  DVLD_DataAccess.clcPersonData.AddPerson(this.NationalNo, this.FirstName,
                this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor,
                this.Address, this.Phone, this.Email, this.NationalCountryID,
                this.imagePath);

            return (this.PersonID != -1);
        }

        private bool UpdatePerson()
        {
            return DVLD_DataAccess.clcPersonData.UpdatePerson(this.PersonID, this.NationalNo, this.FirstName,
                this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor,
                this.Address, this.Phone, this.Email, this.NationalCountryID,
                this.imagePath);
        }
        public bool Save()
        {
            switch(Mode)
            {
                case _enMode.AddMode:
                    {
                        if(AddPerson())
                        {
                            Mode = _enMode.UpdateMode;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        
                    }
                case _enMode.UpdateMode:
                    {
                        if(UpdatePerson())
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

        public static bool Delete(int PersonID)
        {
            return DVLD_DataAccess.clcPersonData.DeletePerson(PersonID);
        }

        public static bool isPersonExist(string NationalNo)
        {
            return DVLD_DataAccess.clcPersonData.ISPersonExistByNationalNo(NationalNo);
        }
        public static bool isPersonExist(int PersonID)
        {
            return DVLD_DataAccess.clcPersonData.ISPersonExistByID(PersonID);
        }
    }
}
