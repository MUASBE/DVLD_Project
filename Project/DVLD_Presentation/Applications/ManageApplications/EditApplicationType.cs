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
    public partial class EditApplicationType : Form
    {

        private int _ApplicationTypeID;
        private clcApplicationTypesBusiness _ApplicationType;
        private void FillApplicationTypeForm()
        {
            _ApplicationType = clcApplicationTypesBusiness.Find(_ApplicationTypeID);

            if(_ApplicationType != null)
            {
                lblAppplicationTypesID.Text = _ApplicationType.ApplicationTypeID.ToString();
                txtApplicationTypeName.Text = _ApplicationType.ApplicationTypeName;
                txtApplicationTypeFees.Text = _ApplicationType.ApplicatinTypeFees.ToString();
                btnSave.Focus();
            }
            else
            {
                MessageBox.Show("Error: Application Type Not Found");
                this.Close();
            }

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
        public EditApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
        }

        private void btnColse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtApplicationTypeName_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
        }

        private void txtApplicationTypeFees_Validating(object sender, CancelEventArgs e)
        {

            if(!clcValidating.IsNumber(txtApplicationTypeFees.Text.Trim()))
            {
                e.Cancel = true;
                txtApplicationTypeFees.Focus();
                errorProvider1.SetError(txtApplicationTypeFees, "This field must be a number!");
            }

            else if(string.IsNullOrEmpty(txtApplicationTypeFees.Text.Trim()))
            {
                e.Cancel = true;
                txtApplicationTypeFees.Focus();
                errorProvider1.SetError(txtApplicationTypeFees, "This field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtApplicationTypeFees, null);
            }

        }
        private void EditApplicationType_Load(object sender, EventArgs e)
        {
            FillApplicationTypeForm();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Please fill all required fields", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to save information", "Confirm",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information
                , MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                _ApplicationType.ApplicationTypeName = txtApplicationTypeName.Text.Trim();
                _ApplicationType.ApplicatinTypeFees = Convert.ToSingle(txtApplicationTypeFees.Text.Trim());

                if (_ApplicationType.Update())
                {
                    MessageBox.Show("Information Updated Successfully", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error: Failed to Update Information", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
