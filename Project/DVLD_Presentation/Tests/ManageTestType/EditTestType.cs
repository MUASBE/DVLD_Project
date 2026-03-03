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
    public partial class EditTestType : Form
    {
        private int _TestTypeID;
        private clcTestTypeBusiness _TestTypeInfo;
        private void LoadTestTypeInfo()
        {
            _TestTypeInfo = clcTestTypeBusiness.Find(_TestTypeID);
            if (_TestTypeInfo != null)
            {
                lblTestTypesID.Text = Convert.ToInt16(_TestTypeInfo.TestTypeID).ToString();
                txtTestTypeName.Text = _TestTypeInfo.TestTypeName;
                txtTestTypeDescription.Text = _TestTypeInfo.TestTypeDescription;
                txtTestTypeFees.Text = _TestTypeInfo.TestTypeFees.ToString();
                btnSave.Focus();
            }
            else
            {
                MessageBox.Show("Error: Test Type not found.");
                this.Close();
            }
        }
        private void ValidateTextBoxes(object sender, CancelEventArgs e)
        {
            TextBox Temp = (TextBox)sender;

            if (string.IsNullOrWhiteSpace(Temp.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }

        }
        public EditTestType(int TestType)
        {
            InitializeComponent();
            _TestTypeID = TestType;
        }
        private void EditTestType_Load(object sender, EventArgs e)
        {
            LoadTestTypeInfo();
        }

        private void btnColse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTestTypeName_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextBoxes(sender, e);
        }

        private void txtTestTypeDescription_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextBoxes(sender, e);
        }

        private void txtTestTypeFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTestTypeFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestTypeFees, "This field is required.");
            }
            else if (!clcValidating.IsNumber(txtTestTypeFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestTypeFees, "Please enter a valid non-negative number.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtTestTypeFees, null);
            }
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
                _TestTypeInfo.TestTypeName = txtTestTypeName.Text.Trim();
                _TestTypeInfo.TestTypeDescription = txtTestTypeDescription.Text.Trim();
                _TestTypeInfo.TestTypeFees = Convert.ToSingle(txtTestTypeFees.Text.Trim());

                if (_TestTypeInfo.Update())
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

