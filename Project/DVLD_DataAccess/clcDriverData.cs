using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clcDriverData
    {
        public static bool GetDriverInfoByDriverID(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"select * from Drivers where DriverID = @DriverID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                if (reader.Read())
                {
                    PersonID = (int)reader["PersonID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                    isFound = true;
                }

                reader.Close();
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
        public static bool GetDriverInfoByPersonID(int PersonID, ref int DriverID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"select * from Drivers where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    DriverID = (int)reader["DriverID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                    isFound = true;
                }

                reader.Close();
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
        public static int AddDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int DriverID = 0;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"INSERT INTO Drivers
                            values
                            (@PersonID, @CreatedByUserID, @CreatedDate)
                            SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(Query, connection);
            command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

            try
            {
                connection.Open ();
                object result = command.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(), out int ID))
                {
                    DriverID = ID;
                }

            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return DriverID;
        }
        public static bool isPersonDriver(int PersonID)
        {
            
            return GetDriverIDByPersonID(PersonID) > 0;
        }
        public static int GetDriverIDByPersonID(int PersonID)
        {
            int NewID = 0;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"select DriverID from Drivers where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
                {
                     NewID = ID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return NewID;
        }
        public static DataTable GetAllDrivers()
        {
            DataTable DTAllDrivers = new DataTable();

            SqlConnection connection = new SqlConnection (clcSetting.connectionString);
            string Query = @"select * from Drivers_View ";

            SqlCommand command = new SqlCommand (Query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    DTAllDrivers.Load(reader);
                }
                reader.Close();
            }
            catch(Exception ex)
            {

            }
            finally
            { connection.Close(); }
            return DTAllDrivers;
        }
    }
}
