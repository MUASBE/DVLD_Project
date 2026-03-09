using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccess
{
    public class clcInternationalLicenseData
    {
        public static bool GetInternationalLicenseByInternationalLicenseID(int InternationalLicenseID, ref int ApplicationID, 
            ref int DriverID, ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate
            ,ref bool IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;
            
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"select * from InternationalLicenses where InternationalLicenseID = @InternationalLicenseID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
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
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetInternationalLicenseByIssuedUsingLocalLicenseID(int IssuedUsingLocalLicenseID, ref int ApplicationID,
            ref int DriverID, ref int InternationalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate
            , ref bool IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"select * from InternationalLicenses where IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    InternationalLicenseID = (int)reader["InternationalLicenseID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
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
            {
                connection.Close();
            }

            return isFound;
        }

        public static int AddInternationalLicense( int ApplicationID,
            int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate
            , bool IsActive, int CreatedByUserID)
        {
            int NewLicenseID = 0;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"
                               Update InternationalLicenses 
                               set IsActive=0
                               where DriverID=@DriverID;
                
                insert into InternationalLicenses
                values
                (@ApplicationID, @DriverID, @IssuedUsingLocalLicenseID, @IssueDate, @ExpirationDate
                    , @IsActive, @CreatedByUserID)
                        SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
                {
                    NewLicenseID = ID;
                }
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

        public static DataTable GetInternationalLicensesByDriverID(int DriverID)
        {
            DataTable DtInternationalLicenses = new DataTable();

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"select InternationalLicenseID , ApplicationID, IssuedUsingLocalLicenseID,
                            IssueDate, ExpirationDate, IsActive from InternationalLicenses
                            where DriverID = @DriverID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    DtInternationalLicenses.Load(reader);
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

            return DtInternationalLicenses;
        }

        public static int GetActiverInternationalLicenseID(int IssuedUsingLocalLicenseID)
        {
            int InternationalLicenseID = 0;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"select InternationalLicenseID  from InternationalLicenses
                            where IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID
                            and ExpirationDate > GETDATE() and IsActive = 1";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
                {
                    InternationalLicenseID = ID;
                }
                
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return InternationalLicenseID;
        }

        public static DataTable GetInternationalLicensesList()
        {
            DataTable DtInternationalLicenses = new DataTable();

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"select InternationalLicenseID , ApplicationID, DriverID, IssuedUsingLocalLicenseID,
                            IssueDate, ExpirationDate, IsActive from InternationalLicenses";

            SqlCommand command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    DtInternationalLicenses.Load(reader);
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

            return DtInternationalLicenses;
        }

    }
}
