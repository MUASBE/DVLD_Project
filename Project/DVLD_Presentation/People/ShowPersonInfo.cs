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
    public partial class ShowPersonInfo : Form
    {
        int PersonID;
        public ShowPersonInfo(int PersonID)
        {
            InitializeComponent();
            ctrlShowPersonDetails1.LoadPersonInfo(PersonID);
        }

        public ShowPersonInfo(string NationalNo)
        {
            InitializeComponent();
            ctrlShowPersonDetails1.LoadPersonInfo(NationalNo);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
