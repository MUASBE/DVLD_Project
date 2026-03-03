using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clcPersonData
    {

        public static bool GetPeronInfoByID(int PersonID, ref string NationalNo, ref string FirstName,
           ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
           ref short Gendor, ref string Address, ref string Phone,
           ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = "SELECT * FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = Convert.ToInt16(reader["Gendor"]);
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }

                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }

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
                isFound = false;
            }
            finally
            {
                connection.Close();
            }



            return isFound;
        }

        public static bool GetPeronInfoByNationalNo(string NationalNo,ref int PersonID, ref string FirstName,
           ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
           ref short Gendor, ref string Address, ref string Phone,
           ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = "SELECT * FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    PersonID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = Convert.ToInt16(reader["Gendor"]);
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    NationalityCountryID = (int)reader["NationalityCountryID"];

                    
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }

                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }

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
                isFound = false;
            }
            finally
            {
                connection.Close();
            }



            return isFound;
        }

        public static int AddPerson(string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, DateTime DateOfBirth, short Gendor, string Address,
            string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int NewPeronID = -1;
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"INSERT INTO People 
                (NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address,
                Phone, Email, NationalityCountryID, ImagePath)
                VALUES
                (@NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth, @Gendor, @Address,
                @Phone, @Email, @NationalityCountryID, @ImagePath)
                SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            if (ThirdName != "")
            {
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            }
            else
            {
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);
            }
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            if (Email != "")
            {
                command.Parameters.AddWithValue("@Email", Email);
            }
            else
            {
                command.Parameters.AddWithValue("@Email", DBNull.Value);
            }
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            
            if (ImagePath != "")
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                
                if(result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    NewPeronID = insertedID;
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }


            return NewPeronID;
        }

        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName, string SecondName, 
            string ThirdName, string LastName, DateTime DateOfBirth,short Gendor,string Address,
            string Phone, string Email, int NationalityCountryID, string ImagePath )
        {
            int EffectedRows = 0;
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"UPDATE People
                SET 
                NationalNo = @NationalNo,
                FirstName = @FirstName,
                SecondName = @SecondName,
                ThirdName = @ThirdName,
                LastName = @LastName,
                DateOfBirth = @DateOfBirth,
                Gendor = @Gendor,
                Address = @Address,
                Phone = @Phone,
                Email = @Email,
                NationalityCountryID = @NationalityCountryID,
                ImagePath = @ImagePath
                WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            if (ThirdName != "")
            {
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            }
            else
            {
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);
            }
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            if (Email != "")
            {
                command.Parameters.AddWithValue("@Email", Email);
            }
            else
            {
                command.Parameters.AddWithValue("@Email", DBNull.Value);
            }
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath != "")
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                connection.Open();
                EffectedRows =  command.ExecuteNonQuery();

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

        public static bool DeletePerson(int PersonID)
        {
            int EffectedRows = 0;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = "DELETE FROM People WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
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

        public static DataTable GetAllPeople()
        {
            DataTable DTPeople = new DataTable();
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = @"SELECT People.PersonID, People.NationalNo, People.SecondName, People.FirstName, People.ThirdName, People.LastName, People.DateOfBirth, 
                        case
                        when Gendor = 0 then 'Male'
                        else
                        'Female'
                        end as GendorCaption,
                        People.Address, People.Phone, People.Email, 
                        People.NationalityCountryID, Countries.CountryName, People.ImagePath
                        FROM People INNER JOIN
                         Countries ON People.NationalityCountryID = Countries.CountryID
						 order by People.FirstName;";
            SqlCommand command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    DTPeople.Load(reader);
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

            return DTPeople;
        }

        public static bool ISPersonExistByID(int PersonID)
        {
            bool isExist = false;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = "SELECT Top 1 *  FROM People WHERE PersonID = @PersonID";
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

        public static bool ISPersonExistByNationalNo(string NationalNo)
        {
            bool isExist = false;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string Query = "SELECT Top 1 *  FROM People WHERE NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
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
    }
}
