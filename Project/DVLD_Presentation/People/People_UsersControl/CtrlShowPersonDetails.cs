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
    public partial class CtrlShowPersonDetails : UserControl
    {
        clcPersonBusiness Person;
        int PersonID = -1;

        public int _PersonID
        {
            get { return PersonID; }
        }
        public clcPersonBusiness PersonInfo
        {
            get { return Person; }
        }
        private void _resetDefaultValue()
        {
            lblPersonID.Text = "????";
            lblName.Text = "????";
            lblNationalNo.Text = "????";
            lblEmail.Text = "????";
            lblPhone.Text = "????";
            lblDateOfBirth.Text = "????";
            lblAddress.Text = "????";
            lblCountry.Text = "????";
            pbPerson.Image = Properties.Resources.Male_512;
            lblGendor.Text = "????";
        }

        public CtrlShowPersonDetails()
        {
            InitializeComponent();
            
        }
        public void LoadPersonInfo(int PersonID)
        {
            Person = clcPersonBusiness.Find(PersonID);
            if (Person != null)
            {
                this.PersonID = Person.PersonID;
                lblPersonID.Text = Person.PersonID.ToString();
                lblName.Text = Person.FirstName + " " + Person.SecondName + " " + Person.ThirdName + " " + Person.LastName;
                lblNationalNo.Text = Person.NationalNo;
                lblEmail.Text = Person.Email;
                lblPhone.Text = Person.Phone;
                lblDateOfBirth.Text = Person.DateOfBirth.ToShortDateString();
                lblAddress.Text = Person.Address;
                lblCountry.Text = Person.CountryInfo.CountryName;

                if (Person.Gendor == 1)
                {
                    pbPerson.Image = Properties.Resources.Female_512;
                    lblGendor.Text = "Female";
                }
                else
                {
                    pbPerson.Image = Properties.Resources.Male_512;
                    lblGendor.Text = "Male";
                }

                // Load image
                if (!string.IsNullOrEmpty(Person.imagePath))
                {
                    try
                    {
                        pbPerson.ImageLocation = Person.imagePath;
                    }
                    catch
                    {
                        // Handle image loading error
                        pbPerson.ImageLocation = null;
                    }
                }
            }

            else
            {
                MessageBox.Show("Person not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _resetDefaultValue();
            }

        }
        public void LoadPersonInfo(string NationalNo)
        {
            Person = clcPersonBusiness.Find(NationalNo);
            if (Person != null)
            {
                this.PersonID = Person.PersonID;
                lblPersonID.Text = Person.PersonID.ToString();
                lblName.Text = Person.FirstName + " " + Person.SecondName + " " + Person.ThirdName + " " + Person.LastName;
                lblNationalNo.Text = Person.NationalNo;
                lblEmail.Text = Person.Email;
                lblPhone.Text = Person.Phone;
                lblDateOfBirth.Text = Person.DateOfBirth.ToShortDateString();
                lblAddress.Text = Person.Address;
                lblCountry.Text = Person.CountryInfo.CountryName;

                if (Person.Gendor == 1)
                {
                    pbPerson.Image = Properties.Resources.Female_512;
                    lblGendor.Text = "Female";
                }
                else
                {
                    pbPerson.Image = Properties.Resources.Male_512;
                    lblGendor.Text = "Male";
                }

                // Load image
                if (!string.IsNullOrEmpty(Person.imagePath))
                {
                    try
                    {
                        pbPerson.ImageLocation = Person.imagePath;
                    }
                    catch
                    {
                        // Handle image loading error
                        pbPerson.ImageLocation = null;
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Person not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _resetDefaultValue();
            }

        }
        private void btnEditPersonInfo_Click(object sender, EventArgs e)
        {
            AddEditPerson addEditPerson = new AddEditPerson(this.PersonID);
            addEditPerson.ShowDialog();

            LoadPersonInfo(this.PersonID);
        }
    }
}
