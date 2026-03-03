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
    public partial class CtrlShowUserInfo : UserControl
    {
        private int _UserID;
        private clcUsersBusiness _User;
        public clcUsersBusiness User { get { return _User; } } 
        public int UserUD { get { return _UserID; } }
        public CtrlShowUserInfo()
        {
            InitializeComponent();
        }

        public void LoadUserInfo(int UserID)
        {
            _User = clcUsersBusiness.FindByUserID(UserID);
            if(User != null)
            {
                _UserID = UserID;
                ctrlShowPersonDetails1.LoadPersonInfo(User.PersonID);
                lblUserName.Text = User.UserName;
                lblUserID.Text = User.UserID.ToString();
                lblIsActive.Text = (User.isActive == true) ? "Yes" : "NO";
            }
            else
            {
                MessageBox.Show("User was not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
