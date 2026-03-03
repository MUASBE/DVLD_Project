using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clcTestData
    {
        public static bool GetTestByTestID(int TestID, ref int TestAppoinmtID,
            ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @" select * from Tests where TestID = @TestID";
            
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    TestAppoinmtID = Convert.ToInt16(reader["TestAppointmentID"]);
                    TestResult = Convert.ToBoolean(reader["TestResult"]);
                    Notes = reader["Notes"].ToString();
                    CreatedByUserID = Convert.ToInt16(reader["CreatedByUserID"]);
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

        public static bool GetTestByTestAppoinmentID(int TestAppoinmtID, ref int TestID,
            ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @" select * from Tests where TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppoinmtID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    TestID = Convert.ToInt16(reader["TestID"]);
                    TestResult = Convert.ToBoolean(reader["TestResult"]);
                    if(reader["Notes"] != DBNull.Value)
                    {
                        Notes = reader["Notes"].ToString();
                    }
                    else
                    {
                        Notes = "";
                    }
                   
                    CreatedByUserID = Convert.ToInt16(reader["CreatedByUserID"]);
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

        public static int AddTest (int TestAppoinmtID,
            bool TestResult, string Notes, int CreatedByUserID)
        {
            int NewTestID = 0;

            SqlConnection Connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"
                            INSERT INTO Tests
                            VALUES
                            (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID)
                            
                            UPDATE TestAppointments 
                                SET IsLocked=1 where TestAppointmentID = @TestAppointmentID;

                            SELECT SCOPE_IDENTITY()";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppoinmtID);
            Command.Parameters.AddWithValue("@TestResult", TestResult);
            if (Notes != null)
            {
                Command.Parameters.AddWithValue("@Notes", Notes);
            }
            else
            {
                Command.Parameters.AddWithValue("@Notes", DBNull.Value);
            }
            
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();
                object res = Command.ExecuteScalar();
                if(res != null && int.TryParse(res.ToString(), out int ID))
                {
                    NewTestID = ID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            { Connection.Close(); }

            return NewTestID;
        }
        public static bool GetLastTestByPersonAndTestTypeAndLicenseClass
            (int PersonID, int LicenseClassID, int TestTypeID, ref int TestID,
              ref int TestAppointmentID, ref bool TestResult,
              ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);

            string query = @"SELECT  top 1 Tests.TestID, 
                Tests.TestAppointmentID, Tests.TestResult, 
			    Tests.Notes, Tests.CreatedByUserID, Applications.ApplicantPersonID
                FROM            LocalDrivingLicenseApplications INNER JOIN
                                         Tests INNER JOIN
                                         TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                         Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                WHERE        (Applications.ApplicantPersonID = @PersonID) 
                        AND (LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID)
                        AND ( TestAppointments.TestTypeID=@TestTypeID)
                ORDER BY Tests.TestAppointmentID DESC";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    
                    TestID = (int)reader["TestID"];
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    if (reader["Notes"] == DBNull.Value)

                        Notes = "";
                    else
                        Notes = (string)reader["Notes"];

                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    isFound = true;

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

    }
}
