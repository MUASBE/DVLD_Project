using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clcUserData
    {
        public static bool GetUserInfoByUserID(int UserID, ref int PersonID,
           ref string UserName, ref string Password, ref bool isActive )
        {
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = "SELECT * FROM Users WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    isActive = (bool)reader["isActive"];
                    isFound = true;
                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
                isFound = false;
            }
            finally
            {
                connection.Close();
            }



            return isFound;
        }

        public static bool GetUserInfoByPersonID(int PersonID, ref int UserID,  
           ref string UserName, ref string Password, ref bool isActive)
        {
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = "SELECT * FROM Users WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    UserID = (int)reader["UserID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    isActive = (bool)reader["isActive"];
                    isFound= true;
                }
                else
                {
                    isFound = false;
                }
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
        public static bool GetUserInfoByUsernameAndPassword(string UserName, string Password,
            ref int UserID, ref int PersonID, ref bool IsActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);

            string query = "SELECT * FROM Users WHERE Username = @Username and Password=@Password;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Username", UserName);
            command.Parameters.AddWithValue("@Password", Password);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    UserID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];
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
        public static int AddUser(int PersonID, string UserName, 
            string Password, bool isActive)
        {
            int NewUserID = -1;
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"INSERT INTO Users 
                VALUES
                (@PersonID, @UserName, @Password, @isActive)
                SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@isActive", isActive);
            
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    NewUserID = insertedID;
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }


            return NewUserID;
        }

        public static bool UpdateUser(int UserID, int PersonID, string UserName,
            string Password, bool isActive)
        {
            int EffectedRows = 0;
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"UPDATE Users
                SET 
                PersonID = @PersonID,
                UserName = @UserName,
                Password = @Password,
                isActive = @isActive
                WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@isActive", isActive);
            
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

            return (EffectedRows > 0);
        }

        public static bool DeleteUser(int UserID)
        {
            int EffectedRows = 0;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = "DELETE FROM Users WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
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

            return (EffectedRows > 0);
        }

        public static DataTable GetAllUsers()
        {
            DataTable DTUsers = new DataTable();
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"SELECT Users.UserID, Users.PersonID, (People.FirstName + ' ' + People.SecondName + 
                ' ' + case 
                    when 
                    People.ThirdName is  null then ''
                    else
                    People.ThirdName
                    end + ' ' + People.LastName) as FullName, Users.UserName, Users.IsActive
                FROM Users INNER JOIN People ON Users.PersonID = People.PersonID";
            SqlCommand command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    DTUsers.Load(reader);
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

            return DTUsers;
        }

        public static bool ISUserExistByUserID(int UserID)
        {
            bool isExist = false;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = "SELECT Top 1 *  FROM Users WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int count))
                {
                    isExist = (count > 0);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return isExist;
        }

        public static bool ISUserExistByPersonID(int PersonID)
        {
            bool isExist = false;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = "SELECT Top 1 *  FROM Users WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int count))
                {
                    isExist = (count > 0);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return isExist;
        }

        public static bool ISUserNameExist(string UserName)
        {
            bool isExist = false;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = "SELECT Top 1 *  FROM Users WHERE UserName = @UserName";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int count))
                {
                    isExist = (count > 0);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return isExist;
        }
        public static bool ChangePassword(int UserID, string NewPassword)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);

            string query = @"Update  Users  
                            set Password = @Password
                            where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
    }
}
