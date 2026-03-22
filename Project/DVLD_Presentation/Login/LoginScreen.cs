using DVLD_Business;
using DVLD_Presentation.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentation
{
    public partial class LoginScreen : Form
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtUserName.Text.Trim() == "" || txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Please fill required fileds", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clcGlobal.GolbalUser = clcUsersBusiness.FindByUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());

            if(clcGlobal.GolbalUser != null )
            {
                if (chkRememberMe.Checked)
                {
                    clcGlobal.RememberUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                }

                else
                {
                    clcGlobal.RememberUsernameAndPassword("", "");
                }

                if (!clcGlobal.GolbalUser.isActive)
                {

                    txtUserName.Focus();
                    MessageBox.Show("Your account is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.Hide();
                MainForm mainForm = new MainForm(this);
                mainForm.ShowDialog();

            }
            else
            {
                MessageBox.Show("Incorrect user name or password ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LoginScreen_Load(object sender, EventArgs e)
        {
            string userName = "", password = "";

            if(clcGlobal.GetStoredCredential(ref userName, ref password))
            {
                txtUserName.Text = userName;
                txtPassword.Text = password;
                chkRememberMe.Checked = true;
            }
            else
            {
                chkRememberMe.Checked = false;
            }

            //if(!string.IsNullOrEmpty(txtPassword.Text))
            //{
            //    txtPassword.PasswordChar = '*';
            //    btnVisibility.Visible = true;
            //    btnVisibility.Image = Resources.visibility_off;
                

            //}
            //else
            //{
            //    btnVisibility.Visible = false;
            //}
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            btnVisibility.Visible = true;
            btnVisibility.Image = (txtPassword.PasswordChar == '*')?  Resources.visibility_off : Resources.visable;
        }

        private void btnVisibility_Click(object sender, EventArgs e)
        {
            if(txtPassword.PasswordChar == '*')
            {
                txtPassword.PasswordChar = txtUserName.PasswordChar;
                btnVisibility.Image = Resources.visable;
            }
            else
            {
                txtPassword.PasswordChar = '*';
                btnVisibility.Image = Resources.visibility_off;
            }
        }
    }
}
