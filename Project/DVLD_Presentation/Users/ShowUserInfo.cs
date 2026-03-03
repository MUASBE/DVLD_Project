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
    public partial class ShowUserInfo : Form
    {
        public ShowUserInfo()
        {
            InitializeComponent();
            ctrlShowUserInfo1.LoadUserInfo(-1);
        }

        public ShowUserInfo(int UserID)
        {
            InitializeComponent();
            ctrlShowUserInfo1.LoadUserInfo(UserID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
