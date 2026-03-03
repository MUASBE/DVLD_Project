using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clcCountryData
    {
        public static bool  GetCountryNameByID(int countryID, ref string CountryName)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string query = "SELECT CountryName FROM Countries WHERE CountryID = @CountryID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", countryID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                
                if (result != null && result != DBNull.Value)
                {
                    CountryName = result.ToString();
                    isFound = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static bool GetCountryIDByCountryName(string CountryName, ref int countryID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string query = "SELECT CountryID FROM Countries WHERE CountryName = @CountryName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    countryID = (int)result;
                    isFound = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static DataTable GetCountriesList()
        {
            DataTable DTCountries = new DataTable();
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = "SELECT CountryName FROM Countries";
            SqlCommand command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                DTCountries.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return DTCountries;
        }

    }
}
