using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clcTestAppointmentData
    {
        public static bool GetTestAppointmentByTestAppointmentsID(
            int TestAppointmentID, ref int TestTypeID, ref int LDLApplicationID,
            ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID,
            ref bool isLocked, ref int retakeTestApplicationID)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"select * from TestAppointments where TestAppointmentID =  @TestAppointmentID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    TestTypeID = Convert.ToInt32(Reader["TestTypeID"]);
                    LDLApplicationID = Convert.ToInt32(Reader["LocalDrivingLicenseApplicationID"]);
                    AppointmentDate = Convert.ToDateTime(Reader["AppointmentDate"]);
                    PaidFees = Convert.ToSingle(Reader["PaidFees"]);
                    CreatedByUserID = Convert.ToInt32(Reader["CreatedByUserID"]);
                    isLocked = Convert.ToBoolean(Reader["isLocked"]);
                    if (Reader["retakeTestApplicationID"] != DBNull.Value)
                    {
                        retakeTestApplicationID = Convert.ToInt32(Reader["retakeTestApplicationID"]);
                    }
                    else
                    {
                        retakeTestApplicationID = -1;
                    }

                    isFound = true;
                }


            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return isFound;
        }
        public static bool GetLastTestAppointmentByLDLApplicationID(
            int LDLApplicationID, int TestTypeID, ref int TestAppointmentID,
            ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID,
            ref bool isLocked, ref int retakeTestApplicationID)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"select top 1 * from TestAppointments
                    where LocalDrivingLicenseApplicationID = @LDLApplicationID
                    and TestTypeID = @TestTypeID
                order by TestAppointmentID DESC";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.HasRows)
                {
                    TestAppointmentID = Convert.ToInt32(Reader["TestAppointmentID"]);
                    AppointmentDate = Convert.ToDateTime(Reader["AppointmentDate"]);
                    PaidFees = Convert.ToSingle(Reader["PaidFees"]);
                    CreatedByUserID = Convert.ToInt32(Reader["CreatedByUserID"]);
                    isLocked = Convert.ToBoolean(Reader["isLocked"]);
                    if (Reader["retakeTestApplicationID"] != DBNull.Value)
                    {
                        retakeTestApplicationID = Convert.ToInt32(Reader["retakeTestApplicationID"]);
                    }
                    else
                    {
                        retakeTestApplicationID = -1;
                    }

                    isFound = true;
                }


            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }

            return isFound;
        }
        public static int AddTestAppointment(
            int TestTypeID, int LDLApplicationID,
            DateTime AppointmentDate, float PaidFees, int CreatedByUserID,
            bool isLocked, int retakeTestApplicationID)

        {
            int newTestAppointmentID = -1;

            SqlConnection Connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"
                            INSERT INTO TestAppointments
                            VALUES
                            (@TestTypeID, @LDLApplicationID, @AppointmentDate, @PaidFees
                                , @CreatedByUserID, @isLocked, @retakeTestApplicationID)
                            SELECT SCOPE_IDENTITY()";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            Command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);
            Command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@isLocked", isLocked);

            if (retakeTestApplicationID > 0)
            {
                Command.Parameters.AddWithValue("@retakeTestApplicationID", retakeTestApplicationID);
            }
            else
            {
                Command.Parameters.AddWithValue("@retakeTestApplicationID", DBNull.Value);
            }

            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int n))
                {
                    newTestAppointmentID = n;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                Connection.Close();
            }

            return newTestAppointmentID;
        }

        public static bool UpdateTestAppointment(int TestAppointmentID, int TestTypeID, int LDLApplicationID,
            DateTime AppointmentDate, float PaidFees, int CreatedByUserID,
            bool isLocked, int retakeTestApplicationID)
        {
            bool isUpdated = false;
            SqlConnection Connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"
                            UPDATE TestAppointments
                            SET
                            TestTypeID  =   @TestTypeID,
                            LocalDrivingLicenseApplicationID  =   @LDLApplicationID,
                            PaidFees  =   @PaidFees,
                            CreatedByUserID  =   @CreatedByUserID,
                            isLocked  =   @isLocked,
                            retakeTestApplicationID  =   @retakeTestApplicationID,                   
                            AppointmentDate = @AppointmentDate
                            WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            Command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            Command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@isLocked", isLocked);

            if(retakeTestApplicationID>0)
            {
                Command.Parameters.AddWithValue("@retakeTestApplicationID", retakeTestApplicationID);
            }
            else
            {
                Command.Parameters.AddWithValue("@retakeTestApplicationID", DBNull.Value);
            }
            

            try
            {
                Connection.Open();
                int rowsAffected = Command.ExecuteNonQuery();
                isUpdated = rowsAffected > 0;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                Connection.Close();
            }
            return isUpdated;
        }

        public static DataTable GetAllTestAppointments()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);

            string query = @"select * from TestAppointments_View
                                  order by AppointmentDate Desc";


            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);

            string query = @"SELECT TestAppointmentID, AppointmentDate,PaidFees, IsLocked
                        FROM TestAppointments
                        WHERE  
                        (TestTypeID = @TestTypeID) 
                        AND (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
                        order by TestAppointmentID desc;";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

        public static int GetTestID(int TestAppointmentID)
        {
            int TestID = 0;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);

            string query = @"select TestID from Tests where TestAppointmentID=@TestAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return TestID;
        }

    }
}
