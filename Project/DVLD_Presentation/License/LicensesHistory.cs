using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentation.License
{
    public partial class LicensesHistory : Form
    {
        private int _PersonID = -1;
        public LicensesHistory(int personID)
        {
            InitializeComponent();
            _PersonID = personID;
        }
        public LicensesHistory()
        {
            InitializeComponent();
        }

        private void LicensesHistory_Load(object sender, EventArgs e)
        {
            if(_PersonID == -1)
            {
                ctrlShowPersonDetailsWithFilter1.FilterEnable = true;
                ctrlShowPersonDetailsWithFilter1.EnableAddPersonButton = false;
                
            }
            else
            {
                ctrlShowPersonDetailsWithFilter1.FilterEnable = false;
                ctrlShowPersonDetailsWithFilter1.LoadPersonInfo(_PersonID);
                if (ctrlShowPersonDetailsWithFilter1.PersonInfo == null)
                {
                    return;
                }


                ctrlDriverLicenses1.LoadDriverLicenses(_PersonID);
            }
                

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlShowPersonDetailsWithFilter1_OnPersonClick(int obj)
        {
            _PersonID = obj;

            if(_PersonID > 0)
            {
                ctrlShowPersonDetailsWithFilter1.LoadPersonInfo(_PersonID);
                if (ctrlShowPersonDetailsWithFilter1.PersonInfo == null)
                {
                    return;
                }


                ctrlDriverLicenses1.LoadDriverLicenses(_PersonID);
            }

        }
    }
}
