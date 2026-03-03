using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clcApplicationData
    {
        public static bool GetApplicationByApplicationID(int ApplicationID, ref int ApplicantPersonID,
            ref DateTime AppliactionDate, ref int ApplicationTypeID, ref int ApplicationStatus,
             ref DateTime LastStatusDate, ref float PaidFees, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string query = "select * from Applications where Applications.ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ApplicantPersonID = Convert.ToInt32(reader["ApplicantPersonID"]);
                    AppliactionDate = Convert.ToDateTime(reader["ApplicationDate"]);
                    ApplicationTypeID = Convert.ToInt32(reader["ApplicationTypeID"]);
                    ApplicationStatus = Convert.ToInt32(reader["ApplicationStatus"]);
                    LastStatusDate = Convert.ToDateTime(reader["LastStatusDate"]);
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
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
        public static int AddApplication(int ApplicantPersonID,
             DateTime AppliactionDate, int ApplicationTypeID,int ApplicationStatus,
             DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            int effectedRows = 0;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @" INSERT INTO Applications
                                values
                                (@ApplicantPersonID, @AppliactionDate, @ApplicationTypeID, 
                                 @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID)
                                SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@AppliactionDate", AppliactionDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int count))
                {
                    effectedRows = count;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return effectedRows;
        }

        public static bool UpdateApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
             int ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);

            string query = @"Update  Applications  
                            set ApplicantPersonID = @ApplicantPersonID,
                                ApplicationDate = @ApplicationDate,
                                ApplicationTypeID = @ApplicationTypeID,
                                ApplicationStatus = @ApplicationStatus, 
                                LastStatusDate = @LastStatusDate,
                                PaidFees = @PaidFees,
                                CreatedByUserID=@CreatedByUserID
                            where ApplicationID=@ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("ApplicantPersonID", @ApplicantPersonID);
            command.Parameters.AddWithValue("ApplicationDate", @ApplicationDate);
            command.Parameters.AddWithValue("ApplicationTypeID", @ApplicationTypeID);
            command.Parameters.AddWithValue("ApplicationStatus", @ApplicationStatus);
            command.Parameters.AddWithValue("LastStatusDate", @LastStatusDate);
            command.Parameters.AddWithValue("PaidFees", @PaidFees);
            command.Parameters.AddWithValue("CreatedByUserID", @CreatedByUserID);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
        public static bool UpdateStatus(int ApplicationID, int ApplicationStatus)
        {
            int EffectedRows = 0;  
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"update Applications 
                        set 
                        ApplicationStatus = @ApplicationStatus,
                        LastStatusDate = GETDATE() 
                        where ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            try
            {
                connection.Open();
                EffectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return EffectedRows > 0;

        }

        public static bool DeleteApplication(int ApplicationID)
        {
            int EffectedRows = 0;
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"delete from Applications where ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                EffectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return EffectedRows > 0;
        }

        public static int GetActiveApplicationID(int ApplicantPersonID, int ApplicationTypeID)//Not used is App type 1
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);

            string Query = @"SELECT ActiveApplicationID=ApplicationID FROM Applications 
                        where Applications.ApplicantPersonID = @ApplicantPersonID and 
                        ApplicationStatus = 1 and ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ApplID))
                {
                    ID = ApplID ;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return ID;

        }
        public static bool IsApplicantHasSameActiveApplication(int ApplicantPersonID, int ApplicationTypeID)
        {
            return GetActiveApplicationID(ApplicantPersonID, ApplicationTypeID) != -1;
        }
        public static bool IsApplicationExist(int ApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);

            string query = "SELECT Found=1 FROM Applications WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static int GetActiveApplicationIDForLicenseClass(int ApplicantPersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int ID = -1;
            
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"SELECT ActiveApplicationID=Applications.ApplicationID  
                            From
                            Applications INNER JOIN
                            LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE ApplicantPersonID = @ApplicantPersonID 
                            and ApplicationTypeID=@ApplicationTypeID 
							and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                            and ApplicationStatus=1";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ApplID))
                {
                    ID = ApplID;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return ID;
        }

    }
}
