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
    public partial class ApplicationTypesList : Form
    {

        DataTable DTApplicationTypes;

        private void _ResfreshApplicationTypes()
        {
            DTApplicationTypes = clcApplicationTypesBusiness.GetAllApplicationTypes();
            DGVApplication.DataSource = DTApplicationTypes;
            lblNumebrOfApplications.Text = DGVApplication.Rows.Count.ToString();
        }

        public ApplicationTypesList()
        {
            InitializeComponent();
        }

        private void ApplicationTypesList_Load(object sender, EventArgs e)
        {
            _ResfreshApplicationTypes();

            if (DGVApplication.Rows.Count > 0)
            {
                DGVApplication.Columns[0].HeaderText = "ID";
                DGVApplication.Columns[0].Width = 80;

                DGVApplication.Columns[1].HeaderText = "Application Type Name";
                DGVApplication.Columns[1].Width = 350;

                DGVApplication.Columns[2].HeaderText = "Application Type Fees";
                DGVApplication.Columns[2].Width = 100;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditApplicationType frmEditApplicationType = new EditApplicationType((int)DGVApplication.CurrentRow.Cells[0].Value);
            frmEditApplicationType.ShowDialog();
            _ResfreshApplicationTypes();
        }
    }
}
