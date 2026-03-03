using DVLD_Business;
using DVLD_Presentation.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentation
{
    public partial class AddEditPerson : Form
    {
        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;
        int PersonID = -1;
        clcPersonBusiness Person;
        enum enMode { AddMode = 1, EditMode = 2 }
        enMode Mode = enMode.AddMode;
        private void FillFieldWithData()
        {
            Person = clcPersonBusiness.Find(this.PersonID);

            if (Person != null)
            {
                txtFirstName.Text = Person.FirstName;
                txtSecondName.Text = Person.SecondName;
                txtThirdName.Text = Person.ThirdName;
                txtLastName.Text = Person.LastName;
                txtEmail.Text = Person.Email;
                txtPhone.Text = Person.Phone;
                DTPDateOfBirth.Value = Person.DateOfBirth;
                txtAddress.Text = Person.Address;
                txtNationalNo.Text = Person.NationalNo;

                if (Person.imagePath != null && Person.imagePath != "")
                {
                    pbPerson.ImageLocation = Person.imagePath;
                    btnRemoveImage.Visible = true;
                }
                else
                {

                    if (Person.Gendor == 1)
                    {
                        pbPerson.Image = Resources.Female_512;
                    }
                    else
                    {
                        pbPerson.Image = Resources.Male_512;
                    }
                }

                if (Person.Gendor == 1)
                {
                    rbFemale.Checked = true;
                }
                else
                {
                    rdMale.Checked = true;
                }

                if(Person.CountryInfo.CountryName != null)
                {
                    cbUserChoice.SelectedIndex = cbUserChoice.FindString(Person.CountryInfo.CountryName);
                }
                

            }
            else
            {
                MessageBox.Show("Person was not found", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void GetMaxDateOfBirth()
        {
            DateTime today = DateTime.Today;
            DateTime maxDate = today.AddYears(-18);
            DTPDateOfBirth.MaxDate = maxDate;
            DTPDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
        }
        private void FillCountryComboBox()
        {
            DataTable dtCountries = clcCountryBusiness.GetCountriesList();
            foreach (DataRow row in dtCountries.Rows)
            {
                cbUserChoice.Items.Add(row["CountryName"].ToString());
            }
            cbUserChoice.SelectedIndex = cbUserChoice.FindStringExact("Sudan");
        }

        private void _ResetDefualtValues()
        {
            FillCountryComboBox();
            GetMaxDateOfBirth();

            if(Mode == enMode.AddMode)
            {
                lblFormMode.Text = "Add New Person";
                Person = new clcPersonBusiness();
            }

            else
            {
                lblFormMode.Text = "Edit Person";
                
            }

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            rdMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";

            if (rdMale.Checked)
                pbPerson.Image = Resources.Male_512;
            else
                pbPerson.Image = Resources.Female_512;

            btnRemoveImage.Visible = (pbPerson.ImageLocation != null);

        }

        private bool _HandleImage()
        {
            if(Person.imagePath != pbPerson.ImageLocation)
            {
                if(Person.imagePath != "" && Person.imagePath != null)
                {
                    try
                    {
                        System.IO.File.Delete(Person.imagePath);
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }

            if(pbPerson.ImageLocation != null)
            {
                string FileDestination = pbPerson.ImageLocation.ToString();
                if (clcUtill.CopyImageToProjectImageFolder(ref FileDestination))
                {
                    pbPerson.ImageLocation = FileDestination;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
        
        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                Temp.Focus();
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }

        }
        public AddEditPerson(int PersonID)
        {
            InitializeComponent();
            this.PersonID = PersonID;
            Mode = enMode.EditMode;
        }
        public AddEditPerson()
        {
            InitializeComponent();
            Mode = enMode.AddMode;
        }
        private void AddEditPerson_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if(Mode == enMode.EditMode)
            {
                FillFieldWithData();
            }
        }
        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
        }

        private void txtSecondName_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
            if(clcPersonBusiness.isPersonExist(txtNationalNo.Text) && Mode == enMode.AddMode)
            {
                e.Cancel = true;
                txtNationalNo.Focus();
                errorProvider1.SetError(txtNationalNo, "National No is already exist");
            }
            else if(txtNationalNo.Text == "")
            {
                e.Cancel = true;
                txtNationalNo.Focus();
                errorProvider1.SetError(txtNationalNo, "This field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNationalNo, null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text.Trim() == "")
                return;

            //validate email format
            if (!DVLD_Presentation.clcValidating.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, null);
            }
        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
        }

        private void btnSavePersonInfo_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Please fill all required fields", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(!_HandleImage())
            {
                return;
            }

            if (MessageBox.Show("Are you sure you want to save information", "Confirm",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information
                , MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                Person.FirstName = txtFirstName.Text.Trim();
                Person.SecondName = txtSecondName.Text.Trim();
                Person.ThirdName = txtThirdName.Text.Trim();
                Person.LastName = txtLastName.Text.Trim();
                Person.NationalNo = txtNationalNo.Text.Trim();
                Person.Email = txtEmail.Text.Trim();
                Person.Phone = txtPhone.Text.Trim();
                Person.DateOfBirth = DTPDateOfBirth.Value;
                Person.Address = txtAddress.Text.Trim();
                Person.NationalCountryID = clcCountryBusiness.find(cbUserChoice.Text).CountryID;
                if (rdMale.Checked)
                {
                    Person.Gendor = 0;
                }
                else
                {
                    Person.Gendor = 1;
                }

                if (pbPerson.ImageLocation != null)
                    Person.imagePath = pbPerson.ImageLocation;
                else
                    Person.imagePath = "";

                if(Person.Save())
                {
                    MessageBox.Show("Person information saved successfully", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Trigger the event to send data back

                    //if (OnCalculationComplete != null)
                    //    // Raise the event with a parameter
                    //    CalculationComplete(this.PersonID);
                    DataBack?.Invoke(this, Person.PersonID);
                    lblFormMode.Text = "Edit Person";
                    lblPersonID.Text = Person.PersonID.ToString();
                }
                else
                {
                    MessageBox.Show("Failed to save person information", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnColse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            btnRemoveImage.Visible = false;
            if (rdMale.Checked)
            {
                pbPerson.Image = Resources.Male_512;
            }
            else
            {
                pbPerson.Image = Resources.Female_512;
            }   
            
            pbPerson.ImageLocation = null;
        }

        private void rdMale_Click(object sender, EventArgs e)
        {
            if (pbPerson.ImageLocation == null)
            {
                pbPerson.Image = Resources.Male_512;
            }
        }

        private void rbFemale_Click(object sender, EventArgs e)
        {
            if (pbPerson.ImageLocation == null)
            {
                pbPerson.Image = Resources.Female_512;
            }
            
        }

        private void btnSetImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                pbPerson.Load(selectedFilePath);
                btnRemoveImage.Visible = true;
                // ...
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
             e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
