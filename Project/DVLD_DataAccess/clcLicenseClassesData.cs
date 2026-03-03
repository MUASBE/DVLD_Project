using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clcLicenseClassesData
    {
        public static bool GetLicenseInfoByID(int LicenseID, ref string ClassName, ref string ClassDescription,
            ref int MinimumAllowedAge, ref int DefaultValidityLength, ref float ClassFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Querey = @"select * from LicenseClasses where LicenseClasses.LicenseClassID = @LicenseID";

            SqlCommand command = new SqlCommand(Querey, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ClassName = reader["ClassName"].ToString();
                    ClassDescription = reader["ClassDescription"].ToString();
                    MinimumAllowedAge = Convert.ToInt32(reader["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToInt32(reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();

            }

            return isFound;
        }

        public static bool GetLicenseInfoByName(string ClassName, ref int LicenseID, ref string ClassDescription,
            ref int MinimumAllowedAge, ref int DefaultValidityLength, ref float ClassFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Querey = @"select * from LicenseClasses where LicenseClasses.ClassName = @ClassName";

            SqlCommand command = new SqlCommand(Querey, connection);
            command.Parameters.AddWithValue("@ClassName", ClassName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    LicenseID = (int)reader["LicenseClassID"];
                    ClassDescription = reader["ClassDescription"].ToString();
                    MinimumAllowedAge = Convert.ToInt32(reader["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToInt32(reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();

            }

            return isFound;
        }

        public static DataTable GetAllLicenseClasses()
        {
            DataTable DTLicenseClasses = new DataTable();
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Querey = @"select ClassName from LicenseClasses";

            SqlCommand command = new SqlCommand(Querey, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                DTLicenseClasses.Load(reader);

                reader.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return DTLicenseClasses;
        }

    }
}
