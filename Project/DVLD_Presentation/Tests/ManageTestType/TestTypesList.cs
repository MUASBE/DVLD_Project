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
    public partial class TestTypesList : Form
    {
        DataTable DtTestType;

        private void _RefreshTestTypeList()
        {
            DtTestType = clcTestTypeBusiness.GetAllTestTypes();
            DGVTypes.DataSource = DtTestType;
            lblNumebrOfTypes.Text = DGVTypes.Rows.Count.ToString();
        }

        public TestTypesList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TestTypesList_Load(object sender, EventArgs e)
        {

            _RefreshTestTypeList();

            if (DGVTypes.Rows.Count > 0)
            {
                DGVTypes.Columns[0].HeaderText = "ID";
                DGVTypes.Columns[0].Width = 80;

                DGVTypes.Columns[1].HeaderText = "Name";
                DGVTypes.Columns[1].Width = 200;

                DGVTypes.Columns[2].HeaderText = "Description";
                DGVTypes.Columns[2].Width = 400;

                DGVTypes.Columns[3].HeaderText = "Fees";
                DGVTypes.Columns[3].Width = 100;
            }
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditTestType frmEditTestType = new EditTestType((int)DGVTypes.CurrentRow.Cells[0].Value);
            frmEditTestType.ShowDialog();
            _RefreshTestTypeList();
        }
    }
}
