using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clcLicenseData
    {
        public static bool GetLicensesByLicenseID(int LicenseID, ref int ApplicationID, ref int DriverID,
            ref int LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes,
            ref float PaidFees, ref bool IsActive, ref int IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"select * from Licenses where LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];

                    if (reader["Notes"] != DBNull.Value)
                    {
                        Notes = reader["Notes"].ToString();
                    }
                    else
                    {
                        Notes = "";
                    }

                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                    IssueReason = (byte)reader["IssueReason"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    isFound = true;
                }

                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            { connection.Close(); }

            return isFound;
        }

        public static bool GetLicensesByApplicationID(int ApplicationID, ref int LicenseID , ref int DriverID,
            ref int LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes,
            ref float PaidFees, ref bool IsActive, ref int IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"select * from Licenses where ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    LicenseID = (int)reader["LicenseID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueReason = (byte)reader["IssueReason"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];

                    if (reader["Notes"] != DBNull.Value)
                    {
                        Notes = reader["Notes"].ToString();
                    }
                    else
                    {
                        Notes = "";
                    }

                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);                   
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    isFound = true;
                }

                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            { connection.Close(); }

            return isFound;
        }

        public static int AddNewLicense(int ApplicationID, int DriverID,
            int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes,
            float PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
        {
            int NewLicenseID = 0;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"insert into Licenses
                values
                (@ApplicationID, @DriverID, @LicenseClass, @IssueDate, @ExpirationDate
                    , @Notes, @PaidFees, @IsActive, @IssueReason, @CreatedByUserID)
                        update Applications 
                        set 
                        ApplicationStatus = 3,
                        LastStatusDate = GETDATE() 
                        where ApplicationID = @ApplicationID
                        SELECT SCOPE_IDENTITY()";

            //if(!clcApplicationData.UpdateStatus(ApplicationID, 3))
            //    return NewLicenseID;

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            if(!string.IsNullOrEmpty(Notes))
            {
                command.Parameters.AddWithValue("@Notes", Notes);
            }
            else
            {
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            }
            
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
                {
                    NewLicenseID = ID;
                }
                //else
                //{
                //    clcApplicationData.UpdateStatus(ApplicationID, 1);
                //}
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return NewLicenseID;
        }

        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return GetLicenseByPersonID(PersonID, LicenseClassID) > 0;
        }

        public static int GetLicenseByPersonID(int PersonID, int LicenseClassID)
        {
            int LicenseID = 0;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"SELECT Licenses.LicenseID FROM Licenses INNER JOIN
                         Applications ON Licenses.ApplicationID = Applications.ApplicationID
						 where Applications.ApplicantPersonID = @PersonID and Licenses.LicenseClass = @LicenseClassID
                            and Licenses.ExpirationDate > GETDATE()";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int ID))
                {
                    LicenseID = ID;
                }
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }
            return LicenseID;
        }

        public static bool DeactivateLicense(int LicenseID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);

            string query = @"UPDATE Licenses
                           SET 
                              IsActive = 0
                             
                         WHERE LicenseID=@LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);


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
    }
}
