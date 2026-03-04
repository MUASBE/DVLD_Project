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

namespace DVLD_Presentation.Drivers
{
    public partial class DriverList : Form
    {
        DataTable DtDriversList;

        private void _FillDriversListWithData()
        {
            DtDriversList = clcDriverBusiness.GetAllDrivers();
            dgvDrivers.DataSource = DtDriversList;
            lblRecordsCount.Text = dgvDrivers.RowCount.ToString();
            
            cbFilterBy.SelectedIndex = cbFilterBy.FindString("None");
            txtFilterValue.Text = "";
            txtFilterValue.Visible = false;

        }

        public DriverList()
        {
            InitializeComponent();
        }

        private void DriverList_Load(object sender, EventArgs e)
        {
            _FillDriversListWithData();

            if(dgvDrivers.RowCount > 0)
            {

                dgvDrivers.Columns[0].HeaderText = "Driver ID";
                dgvDrivers.Columns[0].Width = 100;

                dgvDrivers.Columns[1].HeaderText = "Person ID.";
                dgvDrivers.Columns[1].Width = 100;


                dgvDrivers.Columns[2].HeaderText = "National No";
                dgvDrivers.Columns[2].Width = 100;

                dgvDrivers.Columns[3].HeaderText = "Full Name";
                dgvDrivers.Columns[3].Width = 265;


                dgvDrivers.Columns[4].HeaderText = "Date";
                dgvDrivers.Columns[4].Width = 130;

                dgvDrivers.Columns[5].HeaderText = "Active Licenses";
                dgvDrivers.Columns[5].Width = 110;
                
                
                cbFilterBy.Enabled = true;
                DtDriversList.DefaultView.RowFilter = "";
            }
            else
            {
                cbFilterBy.Enabled = false ;
                txtFilterValue.Text = "";
                txtFilterValue.Visible = false;
            }
                
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilterBy.SelectedItem.ToString() == "None")
            {
                txtFilterValue.Text = "";
                txtFilterValue.Visible = false;
            }
            else
            {
                txtFilterValue.Text = "";
                txtFilterValue.Visible = true;
                txtFilterValue.Focus();
            }

            DtDriversList.DefaultView.RowFilter = "";
            lblRecordsCount.Text = DtDriversList.DefaultView.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() == "None")
            {
                return;
            }

            string FilterText = "";
            switch (cbFilterBy.SelectedItem.ToString())
            {
                case "Person ID":
                    {
                        FilterText = "PersonID";
                        break;
                    }

                case "National No":
                    {
                        FilterText = "NationalNo";
                        break;
                    }

                case "Driver ID":
                    {
                        FilterText = "DriverID";
                        break;
                    }

                case "Full Name":
                    {
                        FilterText = "FullName";
                        break;
                    }
            }

            if (FilterText == "PersonID" || FilterText == "DriverID")
            {
                if (txtFilterValue.Text == "")
                {
                    DtDriversList.DefaultView.RowFilter = "";
                    lblRecordsCount.Text = DtDriversList.DefaultView.Count.ToString();
                    return;
                }


                DtDriversList.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterText, txtFilterValue.Text.Trim());
            }

            else
            {
                DtDriversList.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterText, txtFilterValue.Text.Trim());
            }

            lblRecordsCount.Text = DtDriversList.DefaultView.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPersonInfo frm = new ShowPersonInfo((int)dgvDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();

            DriverList_Load(null, null);

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() == "Person ID" || cbFilterBy.SelectedItem.ToString() == "Driver ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
    }
}
