using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace DVLD_DataAccess
{
    public class clcApplicationTypesData
    {
        public static bool GetApplicationTypeByID(int ApplicationTypeID,ref string ApplicationTypeTitle, ref float ApplicationFees)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clcSetting.connectionString);
            string query = "select * from ApplicationTypes where ApplicationTypeID = @ApplicationTypeID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader  = command.ExecuteReader();
                if(reader.Read())
                {
                    ApplicationTypeTitle = reader["ApplicationTypeTitle"].ToString();
                    ApplicationFees = float.Parse(reader["ApplicationFees"].ToString());
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
        public static DataTable GetAllApplicationTypes()
        {
            DataTable DtApplicationTypes = new DataTable();

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);

            string query = "select * from ApplicationTypes;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader  = command.ExecuteReader();
                if(reader.HasRows)
                    DtApplicationTypes.Load(reader);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return DtApplicationTypes;

        }
        public static bool UpdateApplicationType(int ApplicationTypeID, string ApplicationTypeTitle, float ApplicationFees)
        {
            int EffectedRows = 0;

            SqlConnection connection = new SqlConnection(clcSetting.connectionString);

            string query = @"UPDATE ApplicationTypes
                SET 
                ApplicationTypeTitle = @ApplicationTypeTitle,
                ApplicationFees = @ApplicationFees
                WHERE ApplicationTypeID = @ApplicationTypeID"; 

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);

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
