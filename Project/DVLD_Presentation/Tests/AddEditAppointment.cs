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
using static DVLD_Business.clcTestTypeBusiness;

namespace DVLD_Presentation
{
    public partial class AddEditAppointment : Form
    {
        private int _LDLApplicationID = -1;
        private clcTestTypeBusiness._enTestType _TestTypeID;
        private int _TestAppointmentID = -1;
        public AddEditAppointment(int LDLApplicationID, clcTestTypeBusiness._enTestType TestTypeID, int TestAppointmentID = -1)
        {
            InitializeComponent();
            _LDLApplicationID = LDLApplicationID;
            _TestTypeID = TestTypeID;
            _TestAppointmentID = TestAppointmentID;
        }

        private void AddEditAppointment_Load(object sender, EventArgs e)
        {
            ctrllScheduleTest1.TestTypeID = _TestTypeID;
            ctrllScheduleTest1.LoadTestAppointments_View(_LDLApplicationID, _TestTypeID, _TestAppointmentID);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
