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
    public partial class MainForm : Form
    {
        Form _frmLogin;
        public MainForm(Form frmLogin)
        {
            InitializeComponent();
            _frmLogin = frmLogin;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ManagePeopleForm managePeopleForm = new ManagePeopleForm();
            managePeopleForm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clcGlobal.GolbalUser = null;
            _frmLogin.Show();
            this.Close();
        }

        private void StripMenuUsers_Click(object sender, EventArgs e)
        {
            ManageUsers frmManageUsers = new ManageUsers();
            frmManageUsers.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowUserInfo showUserInfo = new ShowUserInfo(clcGlobal.GolbalUser.UserID);
            showUserInfo.ShowDialog();
        }

        private void chagePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword frmChangePassword = new ChangePassword(clcGlobal.GolbalUser.UserID);
            frmChangePassword.ShowDialog();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationTypesList frmApplicationTypesList = new ApplicationTypesList();
            frmApplicationTypesList.ShowDialog();
        }

        private void manageTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestTypesList frmTestTypesList = new TestTypesList();
            frmTestTypesList.ShowDialog();

        }

        private void localApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LDLApplicationList frmLDLApplicationList = new LDLApplicationList();
            frmLDLApplicationList.ShowDialog();
        }
    }
}
