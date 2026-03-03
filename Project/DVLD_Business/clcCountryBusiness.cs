using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clcCountryBusiness
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public clcCountryBusiness()
        {
            CountryID = -1;
            CountryName = string.Empty;
        }

        private clcCountryBusiness(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        }

        public static clcCountryBusiness find(int CountryID)
        {
            string countryName = string.Empty;

            if(clcCountryData.GetCountryNameByID(CountryID, ref countryName))
            {
                return new clcCountryBusiness(CountryID, countryName);
            }
            else
            {
                return null;
            }
        }
        public static clcCountryBusiness find(string CountryName)
        {
            int CountryID = -1;

            if (clcCountryData.GetCountryIDByCountryName(CountryName, ref CountryID))
            {
                return new clcCountryBusiness(CountryID, CountryName);
            }
            else
            {
                return null;
            }
        }
        public static DataTable GetCountriesList()
        {
            return DVLD_DataAccess.clcCountryData.GetCountriesList();
        }

        


    }
}
