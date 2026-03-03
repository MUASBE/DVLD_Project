using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business;
namespace DVLD_Presentation
{
    public partial class AddEditLDLApplication : Form
    {
        private int _LDLApplicationID;
        public clcLDLApplicationBusiness LDLApplicationInfo;
        private int PrevLicenseIDBeforeUpdate = -1;
        public enum enMode { AddMode = 1, UpdateMode = 2 }
        private enMode _Mode = enMode.AddMode;
        private void _FillLicenseCombo()
        {
            DataTable DTLicenseClasses = clcLicenseClassesBusiness.GetAllLicenseClasses();
            foreach (DataRow DR in DTLicenseClasses.Rows)
            {
                cbLicenseClass.Items.Add(DR["ClassName"].ToString());
            }
            cbLicenseClass.SelectedIndex = 2;
        }
        private void _SetDefaultValue()
        {
            _FillLicenseCombo();

            if(_Mode == enMode.AddMode)
            {
                LDLApplicationInfo = new clcLDLApplicationBusiness();
                lblTitle.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";
                lblCreatedByUser.Text = clcGlobal.GolbalUser.UserName;
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                LDLApplicationInfo.ApplicationInfo.ApplicationTypeID = (int)clcApplicationBusiness.enApplicationType.NewDrivingLicense;
                LDLApplicationInfo.ApplicationInfo.ApplicationTypeInfo = clcApplicationTypesBusiness.Find(LDLApplicationInfo.ApplicationInfo.ApplicationTypeID);
                lblFees.Text = LDLApplicationInfo.ApplicationInfo.ApplicationTypeInfo.ApplicatinTypeFees.ToString();
            }
             else
            {

                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";

                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
            }
        }
        private void _FillLDLApplicationFormWithData()
        {
            LDLApplicationInfo = clcLDLApplicationBusiness.Find(_LDLApplicationID);

            if(LDLApplicationInfo != null)
            {
                ctrlShowPersonDetailsWithFilter1.LoadPersonInfo(LDLApplicationInfo.ApplicationInfo.ApplicantPersonID);
                lblLocalDrivingLicebseApplicationID.Text = LDLApplicationInfo.LDLApplicationID.ToString();
                lblApplicationDate.Text = LDLApplicationInfo.ApplicationInfo.ApplicationDate.ToShortDateString();
                lblCreatedByUser.Text = LDLApplicationInfo.ApplicationInfo.GreatedUserInfo.UserName;
                cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(LDLApplicationInfo.LicenseInfo.LicenseName.Trim());
                lblFees.Text = LDLApplicationInfo.ApplicationInfo.PaidFees.ToString();
                btnApplicationInfoNext.Enabled = true;
                btnSave.Enabled = true;

                PrevLicenseIDBeforeUpdate = LDLApplicationInfo.LicenseInfo.LicenseID;
            }
            else
            {
                MessageBox.Show("Unable to load the LDL Application information. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

        }
        public AddEditLDLApplication()
        {
            InitializeComponent();
            _Mode = enMode.AddMode;
            ctrlShowPersonDetailsWithFilter1.FilterEnable = true;
            ctrlShowPersonDetailsWithFilter1.EnableAddPersonButton = true;       
            btnApplicationInfoNext.Enabled = false;
            btnSave.Enabled = false;
        }
        public AddEditLDLApplication(int LDLApplicationID)
        {
            InitializeComponent();
            _LDLApplicationID = LDLApplicationID;
            _Mode = enMode.UpdateMode;
            ctrlShowPersonDetailsWithFilter1.FilterEnable = false;
            ctrlShowPersonDetailsWithFilter1.EnableAddPersonButton = false;
            btnApplicationInfoNext.Enabled = true;
        }
        private void AddEditLDLApplication_Load(object sender, EventArgs e)
        {
            _SetDefaultValue();

            if (_Mode == enMode.UpdateMode)
            {
                _FillLDLApplicationFormWithData();
            }
        }

        private void btnSave_Click(object sender, EventArgs e) // under development , not completed yet
        {
            if(ctrlShowPersonDetailsWithFilter1.PersonInfo == null)
            {                 
                MessageBox.Show("Please select a person for the application.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LDLApplicationInfo.LicenseInfo = clcLicenseClassesBusiness.Find(cbLicenseClass.SelectedItem.ToString());
            LDLApplicationInfo.LicenseID = LDLApplicationInfo.LicenseInfo.LicenseID;

            if ((clcLicenseBusiness.IsLicenseExistByPersonID(ctrlShowPersonDetailsWithFilter1.PersonInfo.PersonID,
                LDLApplicationInfo.LicenseInfo.LicenseID)))
            {
                MessageBox.Show($"The selected person already has an active License . Please select a different person or license class.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int TempID = clcApplicationBusiness.GetActiveApplicationIDForApplicant(ctrlShowPersonDetailsWithFilter1.PersonID,
                (int)clcApplicationBusiness.enApplicationType.NewDrivingLicense, LDLApplicationInfo.LicenseID);
            


            if (TempID != -1 && PrevLicenseIDBeforeUpdate != LDLApplicationInfo.LicenseInfo.LicenseID)
            {
                MessageBox.Show($"The selected person already has an active application for the selected license class with ApplicationID {TempID}. Please select a different person or license class.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to save the application information?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LDLApplicationInfo.ApplicationInfo.ApplicantPersonID = ctrlShowPersonDetailsWithFilter1.PersonInfo.PersonID;
                LDLApplicationInfo.ApplicationInfo.ApplicantPersonInfo = ctrlShowPersonDetailsWithFilter1.PersonInfo;
                LDLApplicationInfo.ApplicationInfo.ApplicationDate = DateTime.Now;
                LDLApplicationInfo.ApplicationInfo.GreatedUserID = clcGlobal.GolbalUser.UserID;
                LDLApplicationInfo.ApplicationInfo.GreatedUserInfo = clcGlobal.GolbalUser;
                LDLApplicationInfo.ApplicationInfo.ApplicationStatus = clcApplicationBusiness._enStatus.New;
                LDLApplicationInfo.ApplicationInfo.PaidFees = Convert.ToSingle(lblFees.Text);
                LDLApplicationInfo.ApplicationInfo.LastStatusDate = DateTime.Now;


                LDLApplicationInfo.ApplicationID = LDLApplicationInfo.ApplicationInfo.ApplicationID;
                

                if (LDLApplicationInfo.Save())
                {
                    MessageBox.Show("Application information saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to save the LDL application information. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
            }

        }

        private void ctrlShowPersonDetailsWithFilter1_OnPersonClick(int obj)
        {
            btnSave.Enabled = true;
            btnApplicationInfoNext.Enabled = true;
        }

        private void btnApplicationInfoNext_Click(object sender, EventArgs e)
        {
            if(ctrlShowPersonDetailsWithFilter1.PersonInfo != null)
            {
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
            }
        }
    }
}
