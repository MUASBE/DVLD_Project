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
    public partial class ShowLDLApplicationInfo : Form
    {
        int _LDLApplicationID;
        public ShowLDLApplicationInfo(int LDLApplicationID)
        {
            InitializeComponent();
            _LDLApplicationID = LDLApplicationID;
            ctrlLDLApplicationInfo1.LoadLDLApplicationInfo(_LDLApplicationID);
        }

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
