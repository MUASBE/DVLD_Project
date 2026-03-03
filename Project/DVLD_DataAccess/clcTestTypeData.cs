using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clcTestTypeData
    {
        public static bool GetTestTypeByID(int TestTypeID, ref string TestTypeTitle, ref string TestTypeDescription, ref float TestFees)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string query = "select * from TestTypes where TestTypeID = @TestTypeID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    TestTypeTitle = reader["TestTypeTitle"].ToString();
                    TestTypeDescription = reader["TestTypeDescription"].ToString();
                    TestFees = Convert.ToSingle(reader["TestTypeFees"].ToString());
                    IsFound = true;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }
        public static DataTable GetAllTestTypes()
        {
            DataTable DtTestTypes = new DataTable();

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);

            string query = "select * from TestTypes;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    DtTestTypes.Load(reader);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return DtTestTypes;

        }
        public static bool UpdateTestType(int TestTypeID,  string TestTypeTitle, string TestTypeDescription,float TestFees)
        {
            int EffectedRows = -1;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);

            string query = @"UPDATE TestTypes
                SET 
                TestTypeTitle = @TestTypeTitle,
                TestTypeDescription = @TestTypeDescription,
                TestTypeFees = @TestTypeFees
                WHERE TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestFees);

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

    }
}
