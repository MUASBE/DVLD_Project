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
    public partial class AddEditUser : Form
    {
        private enum  _enMode { AddMode = 0, EditMode };
        _enMode Mode = _enMode.AddMode;
        clcUsersBusiness _User;
        int _UserID = -1;
        
        private void _RefreshDefaultvalue()
        {
            if(Mode == _enMode.AddMode)
            {
                lblTitle.Text = "Add New User";
                _User = new clcUsersBusiness();
                ctrlShowPersonDetailsWithFilter1.FilterEnable = true;
                ctrlShowPersonDetailsWithFilter1.EnableAddPersonButton = true;
                btnSave.Enabled = false;
                tpLoginInfo.Enabled = false;
            }

            else
            {
                lblTitle.Text = "Edit User";
                ctrlShowPersonDetailsWithFilter1.FilterEnable = false;
                ctrlShowPersonDetailsWithFilter1.EnableAddPersonButton = false;
                btnSave.Enabled = true;
                tpLoginInfo.Enabled = true;
            }
        }
        void _LoadUserCard()
        {

            _User = clcUsersBusiness.FindByUserID(_UserID);
            if(_User != null )
            {
                ctrlShowPersonDetailsWithFilter1.LoadPersonInfo(_User.PersonID);
                lblUserID.Text = _User.UserID.ToString();
                txtUserName.Text = _User.UserName;
                txtNewPassword.Text = _User.Password;
                txtConfirmNewPassword.Text = _User.Password;
                chkIsActive.Checked = _User.isActive;
            }
            else
            {
                MessageBox.Show("User was not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        public AddEditUser()
        {
            InitializeComponent();
            Mode = _enMode.AddMode;
        }

        public AddEditUser(int UserID)
        {
            InitializeComponent();
            Mode = _enMode.EditMode;
            _UserID = UserID;

        }

        private void btnPersonInfoNext_Click(object sender, EventArgs e)
        {

            if (Mode == _enMode.EditMode)
            {
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                return;
            }

            if (ctrlShowPersonDetailsWithFilter1.PersonInfo != null)
            {
                if(clcUsersBusiness.isUserExistByPersonID(ctrlShowPersonDetailsWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                    tpLoginInfo.Enabled = true;
                    btnSave.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("Please select Person to complete process", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if(txtUserName.Text == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "This filed is required !");
                txtUserName.Focus();
            }
            else if(clcUsersBusiness.IsUserNameExist(txtUserName.Text.Trim()) && Mode == _enMode.AddMode)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "User name is already exist, choose another one!");
                txtUserName.Focus();
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserName, null);
            }
        }
        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtNewPassword.Text == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "This filed is required !");
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
            if (txtConfirmNewPassword.Text == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmNewPassword, "This filed is required !");
                txtConfirmNewPassword.Focus();
            }
            else if(txtNewPassword.Text == "")
            {
                errorProvider1.SetError(txtNewPassword, "This filed is required, set a new password first!");
                txtNewPassword.Focus();
            }
            else if (txtConfirmNewPassword.Text.Trim() != txtNewPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmNewPassword, "Password does not match !");
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
                MessageBox.Show("Please fill required fields !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if(MessageBox.Show("Are you sure ou want to save information", "Comfirm", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK) 
            {
                _User.UserName = txtUserName.Text.Trim();
                _User.Password = txtNewPassword.Text.Trim();
                _User.isActive = chkIsActive.Checked;
                _User.PersonID = ctrlShowPersonDetailsWithFilter1.PersonID;

                if(_User.Save())
                {
                    MessageBox.Show("User's information saved successfully");
                    Mode = _enMode.EditMode;
                    lblTitle.Text = "Edit User";
                    lblUserID.Text = _User.UserID.ToString();
                }
                else
                {
                    MessageBox.Show("Error occurs when saving user's information");
                }
            }
        }
        private void AddEditUser_Load(object sender, EventArgs e)
        {
            _RefreshDefaultvalue();

            if (Mode == _enMode.EditMode)
                _LoadUserCard();
        }
    }
}
