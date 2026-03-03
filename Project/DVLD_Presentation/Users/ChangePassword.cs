using DVLD_Business;
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
    public partial class ChangePassword : Form
    {
        private clcUsersBusiness _User;
        private int _UserID;
        public ChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if(txtCurrentPassword.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Filed is require");
                txtCurrentPassword.Focus();
            }
            else if(txtCurrentPassword.Text.Trim() != _User.Password)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Password is incorrect");
                txtCurrentPassword.Focus();
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCurrentPassword, null);
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtNewPassword.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "Filed is require");
                txtNewPassword.Focus();
            }

            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNewPassword, null);
            }
        }

        private void txtConfirmNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmNewPassword.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmNewPassword, "Filed is require");
                txtConfirmNewPassword.Focus();
            }
            else if (txtConfirmNewPassword.Text.Trim() != txtNewPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmNewPassword, "Password does not match");
                txtConfirmNewPassword.Focus();
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmNewPassword, null);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show(@"Are you sure you want to save user's information ", "Confirmation",
                  MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                _User.Password = txtNewPassword.Text.Trim();
                if(_User.Save())
                {
                    MessageBox.Show("Password Changed Successfully.",
                   "Saved.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failing when save user's information try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            _User = clcUsersBusiness.FindByUserID(_UserID);
            if (_User == null)
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Could not Find User with id = " + _UserID,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

                return;

            }
            txtConfirmNewPassword.Focus();
            ctrlShowUserInfo1.LoadUserInfo(_UserID);
        }
    }
}
