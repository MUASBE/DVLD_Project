using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clcLDLApplicationData
    {
        public static bool GetLDLApplicationByID(int LDLApplicationID, ref int ApplicationID, ref int LicenseClassID)
        {
            bool isFound = false;

            SqlConnection conncetion = new SqlConnection(clcSetting.connectionString);

            string Query = @"select * from LocalDrivingLicenseApplications 
                where LocalDrivingLicenseApplicationID = @LDLApplicationID";

            SqlCommand command = new SqlCommand(Query, conncetion);
            command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);

            try
            {
                conncetion.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conncetion.Close();
            }

            return isFound;
        }
        public static bool GetLDLApplicationByApplicationID(int ApplicationID , ref int LDLApplicationID, ref int LicenseClassID)
        {
            bool isFound = false;

            SqlConnection conncetion = new SqlConnection(clcSetting.connectionString);

            string Query = @"select * from LocalDrivingLicenseApplications 
                where ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(Query, conncetion);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                conncetion.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    LDLApplicationID = Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]);
                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
                    isFound = true;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conncetion.Close();
            }

            return isFound;
        }
        public static int AddLDLApplication(int ApplicationID, int LicenseClassID)
        {
            int ID = -1;
            SqlConnection conncetion = new SqlConnection(clcSetting.connectionString);
            string Query = @"insert into LocalDrivingLicenseApplications(ApplicationID, LicenseClassID) 
                values(@ApplicationID, @LicenseClassID)
                    SELECT SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(Query, conncetion);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                conncetion.Open();
                object result = command.ExecuteNonQuery();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ID = insertedID;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conncetion.Close();
            }

            return ID;
        }
        public static bool UpdateLDLApplication(int ApplicationID, int LicenseClassID)
        {
            int EffectedRows = 0;
            SqlConnection conncetion = new SqlConnection(clcSetting.connectionString);
            string Query = @"Update LocalDrivingLicenseApplications
                    set LicenseClassID = @LicenseClassID
                    where ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(Query, conncetion);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                conncetion.Open();
                object result = command.ExecuteNonQuery();
                if (result != null && int.TryParse(result.ToString(), out int Count))
                {
                    EffectedRows = Count;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conncetion.Close();
            }

            return EffectedRows > 0;
        }
        public static bool DeleteLDLApplication(int LDLApplicationID)
        {
            int EffectedRows = 0;
            SqlConnection conncetion = new SqlConnection(clcSetting.connectionString);
            string Query = @"Delete from LocalDrivingLicenseApplications
                    where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
            SqlCommand command = new SqlCommand(Query, conncetion);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLApplicationID);
            try
            {
                conncetion.Open();
                object result = command.ExecuteNonQuery();
                if (result != null && int.TryParse(result.ToString(), out int Count))
                {
                    EffectedRows = Count;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conncetion.Close();
            }
            return EffectedRows > 0;
        }
        public static DataTable GetAllLDLApplications()
        {
            DataTable DTLDLApplications = new DataTable();

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string query = "select * from LocalDrivingLicenseApplications_View";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    DTLDLApplications.Load(reader);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return DTLDLApplications;
        }
        public static int GetActiveApplicationIDForLicenseClass(int ApplicantPersonID, int LicenseClassID) // leak some logic 
        {
            return clcApplicationData.GetActiveApplicationIDForLicenseClass(ApplicantPersonID, 1, LicenseClassID);
        }
        public static bool isApplicationActiveForLicenseClass(int ApplicantPersonID, int LicenseClassID)
        {
            return GetActiveApplicationIDForLicenseClass(ApplicantPersonID, LicenseClassID) != -1;

        }

        public static int GetApplicationIDForActiveLDLApplication(int LDLApplicationID)
        {
            int ApplicationID = -1;
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string query = @"select ApplicationID from LocalDrivingLicenseApplications
                where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLApplicationID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int appID))
                {
                    ApplicationID = appID;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return ApplicationID;
        }

        public static DataTable GetAllAppointmentForLDLApplicationWithTestType(int LDLApplicationID, int TestTypeID)
        {
            DataTable DTAppointment = new DataTable();

            SqlConnection connection = new SqlConnection();
            string Query = @" select TestAppointmentID, AppointmentDate, PaidFees, IsLocked
                            from TestAppointments where LocalDrivingLicenseApplicationID = @LDLApplicationID 
                            and TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                    DTAppointment.Load(reader);

                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return DTAppointment;
        }

        public static int GetTestTrialCount(int LDLApplicationID, int TestTypeID)
        {
            int Trailcount = -1;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string query = @"SELECT count (*) as TrailCount
                FROM TestAppointments INNER JOIN
                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                where TestAppointments.LocalDrivingLicenseApplicationID = @LDLApplicationID 
                and  TestAppointments.TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int count))
                {
                    Trailcount = count;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return Trailcount;
        }

        public static bool IsThereAnActiveAppointment(int LDLApplicationID, int TestTypeID)
        {


            bool Result = false;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);

            string query = @"  select top 1 Found = 1 from TestAppointments where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
            and TestTypeID = @TestTypeID and IsLocked = 0";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null)
                {
                    Result = true;
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

            return Result;
        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {


            bool Result = false;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);

            string query = @" SELECT top 1 TestResult
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                            ORDER BY TestAppointments.TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && bool.TryParse(result.ToString(), out bool returnedResult))
                {
                    Result = returnedResult;
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

            return Result;

        }

        public static bool DoesAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {


            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);

            string query = @" SELECT top 1 Found=1
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                            ORDER BY TestAppointments.TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    IsFound = true;
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

            return IsFound;

        }

        public static int PassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            int PassedTests = 0;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"SELECT count (*) FROM TestAppointments INNER JOIN
                Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                where TestAppointments.LocalDrivingLicenseApplicationID =
                @LocalDrivingLicenseApplicationID and Tests.TestResult = 1";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int Count))
                {
                    PassedTests = Count;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return PassedTests;
        }
    }
}
