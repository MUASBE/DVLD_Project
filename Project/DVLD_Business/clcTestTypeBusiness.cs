using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DVLD_Business
{
    public class clcTestTypeBusiness
    {

        public enum _enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public _enTestType TestTypeID { get; set; }
        public string TestTypeName { get; set; }

        public string TestTypeDescription { get; set; }
        public float TestTypeFees { get; set; }
        public clcTestTypeBusiness()
        {
            this.TestTypeID = 0;
            this.TestTypeName = "";
            this.TestTypeDescription = "";
            this.TestTypeFees = 0;
        }
        public clcTestTypeBusiness(clcTestTypeBusiness._enTestType TestTypeID, string TestTypeName, string TestTypeDescription, float TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeName = TestTypeName;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
        }
        public static clcTestTypeBusiness Find(int TestTypeID)
        {

            string TestTypeName = "";
            string TestTypeDescription = "";
            float TestTypeFees = 0;

            if (clcTestTypeData.GetTestTypeByID((int)TestTypeID, ref TestTypeName, ref TestTypeDescription, ref TestTypeFees))
            {
                return new clcTestTypeBusiness((_enTestType)TestTypeID, TestTypeName, TestTypeDescription, TestTypeFees);
            }
            else
            {
                return null;
            }
        }
        public static DataTable GetAllTestTypes()
        {
            return clcTestTypeData.GetAllTestTypes();
        }
        public bool Update()
        {
            return clcTestTypeData.UpdateTestType((int)this.TestTypeID, this.TestTypeName, this.TestTypeDescription, this.TestTypeFees);
        }
    }
}
