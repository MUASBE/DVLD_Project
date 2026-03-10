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
    public class clcDetainLicenseData
    {
        public static bool GetDetainLicenseByDetainID(int DetainID, ref int LicenseID, ref DateTime DetainDate, ref float FineFees, 
            ref int CreatedByUserID,ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"select * from DetainedLicenses where DetainID = @DetainID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    LicenseID = (int)reader["LicenseID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    if (reader["ReleasedByUserID"] != null)
                    {
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];
                    }
                    else
                    {
                        ReleasedByUserID = -1;
                    }
                    if (reader["ReleaseApplicationID"] != null)
                    {
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                    }
                    else
                    {
                        ReleaseApplicationID = -1;
                    }

                    DetainDate = (DateTime)reader["DetainDate"];

                    if (reader["ReleaseDate"] != null)
                    {
                        ReleaseDate = (DateTime)reader["ReleaseDate"];
                    }
                    else
                    {
                        ReleaseDate = DateTime.MinValue;
                    }
                        FineFees = Convert.ToSingle(reader["FineFees"]);
                    IsReleased = Convert.ToBoolean(reader["IsActive"]);

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
        public static bool GetDetainLicenseByLicenseID(int LicenseID, ref int DetainID, ref DateTime DetainDate, ref float FineFees,
            ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"select top 1* from DetainedLicenses where LicenseID = @LicenseID order by DetainID desc;";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    DetainID = (int)reader["DetainID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    if (reader["ReleasedByUserID"] != null)
                    {
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];
                    }
                    else
                    {
                        ReleasedByUserID = -1;
                    }
                    if (reader["ReleaseApplicationID"] != null)
                    {
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                    }
                    else
                    {
                        ReleaseApplicationID = -1;
                    }

                    DetainDate = (DateTime)reader["DetainDate"];

                    if (reader["ReleaseDate"] != null)
                    {
                        ReleaseDate = (DateTime)reader["ReleaseDate"];
                    }
                    else
                    {
                        ReleaseDate = DateTime.MinValue;
                    }
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    IsReleased = Convert.ToBoolean(reader["IsActive"]);

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
        public static int DetainLicense(int LicenseID, DateTime DetainDate, float FineFees,
             int CreatedByUserID, bool IsReleased)
        {
            int DetainID = 0;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"insert into DetainedLicenses
                values
                (@LicenseID, @DetainDate, @FineFees, @CreatedByUserID, @IsReleased, null, null, null)
                        SELECT SCOPE_IDENTITY()";


            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
            
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
                {
                    DetainID = ID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return DetainID;
        }

        public static bool  UpdateDetainLicense(int DetainID,int LicenseID, DateTime DetainDate, float FineFees,
             int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            int effectedRows = 0;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"Update DetainedLicenses
                set
                LicenseID = @LicenseID
                DetainDate = @DetainDate
                FineFees = @FineFees
                CreatedByUserID = @CreatedByUserID
                IsReleased = @IsReleased
                ReleaseDate = @ReleaseDate
                ReleasedByUserID = @ReleasedByUserID
                ReleaseApplicationID = @ReleaseApplicationID
                where DetainID = @DetainID
                        SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
            if (ReleaseDate != DateTime.MinValue)
            {
                command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            }
            else
            {
                command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);
            }

            if (ReleasedByUserID > 0)
            {
                command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            }
            else
            {
                command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);
            }

            if (ReleaseApplicationID > 0)
            {
                command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            }
            else
            {
                command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);
            }

            try
            {
                connection.Open();
                object result = command.ExecuteNonQuery();
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

            return effectedRows > 0;
        }
        public static bool ReleaseDetainedLicense(int DetainID,
                 int ReleasedByUserID, int ReleaseApplicationID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);

            string query = @"UPDATE dbo.DetainedLicenses
                              SET IsReleased = 1, 
                              ReleaseDate = @ReleaseDate, 
                              ReleaseApplicationID = @ReleaseApplicationID   
                              WHERE DetainID=@DetainID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
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

        public static int GetDetainIDForLicense(int  licenseID)
        {

            int DetainID = 0;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"select DetainID from DetainedLicenses where licenseID = @licenseID and IsReleased = 0";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@licenseID", licenseID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
                {
                    DetainID = ID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            { connection.Close(); }
            return DetainID;
        }

        public static DataTable GetAllDetainedLicenses()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);

            string query = "select * from detainedLicenses_View order by IsReleased ,DetainID;";

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
    }
}
