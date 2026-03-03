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
using DVLD_Business;

namespace DVLD_Presentation
{
    public partial class ManagePeopleForm : Form
    {
        static DataTable DTAllPeople = clcPersonBusiness.GetAllPersons();

        DataTable DTPeople = DTAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GendorCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");

        public ManagePeopleForm()
        {
            InitializeComponent();
        }
         private void RefreshPeopleList()
         {
            DTAllPeople = clcPersonBusiness.GetAllPersons();
            DTPeople = DTAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GendorCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");

            lblNumebrOfPeople.Text = DTPeople.Rows.Count.ToString();
            DGVPeople.DataSource = DTPeople;
         }
        private void ManagePeopleForm_Load(object sender, EventArgs e)
        {
            RefreshPeopleList();

            if(DGVPeople.Rows.Count > 0)
            {
                DGVPeople.Columns[0].HeaderText = "Person ID";
                DGVPeople.Columns[0].Width = 110;

                DGVPeople.Columns[1].HeaderText = "National No.";
                DGVPeople.Columns[1].Width = 120;


                DGVPeople.Columns[2].HeaderText = "First Name";
                DGVPeople.Columns[2].Width = 120;

                DGVPeople.Columns[3].HeaderText = "Second Name";
                DGVPeople.Columns[3].Width = 140;


                DGVPeople.Columns[4].HeaderText = "Third Name";
                DGVPeople.Columns[4].Width = 120;

                DGVPeople.Columns[5].HeaderText = "Last Name";
                DGVPeople.Columns[5].Width = 120;

                DGVPeople.Columns[6].HeaderText = "Gendor";
                DGVPeople.Columns[6].Width = 120;

                DGVPeople.Columns[7].HeaderText = "Date Of Birth";
                DGVPeople.Columns[7].Width = 140;

                DGVPeople.Columns[8].HeaderText = "Nationality";
                DGVPeople.Columns[8].Width = 120;


                DGVPeople.Columns[9].HeaderText = "Phone";
                DGVPeople.Columns[9].Width = 120;


                DGVPeople.Columns[10].HeaderText = "Email";
                DGVPeople.Columns[10].Width = 170;
            }

            cbUserChoice.SelectedIndex = 0;
            txtUserChoice.Visible = false;

        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            AddEditPerson addPersonForm = new AddEditPerson();
            addPersonForm.ShowDialog();

            RefreshPeopleList();
        }

        private void showPersonInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPersonInfo FrmShowPersonInfo = new ShowPersonInfo((int)DGVPeople.CurrentRow.Cells[0].Value);          
            FrmShowPersonInfo.ShowDialog();

            RefreshPeopleList();
        }

        private void findPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFindPersonInfo frmFindPersonInfo = new frmFindPersonInfo();
            frmFindPersonInfo.ShowDialog();
        }

        private void cbUserChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbUserChoice.SelectedIndex == 0)
            {
                txtUserChoice.Visible=false;
            }

            else
            {
                txtUserChoice.Visible = true;
                txtUserChoice.Text = "";
                txtUserChoice.Focus();
            }


            DTPeople.DefaultView.RowFilter = "";
            lblNumebrOfPeople.Text = DTPeople.DefaultView.Count.ToString();
        }

        private void txtUserChoice_TextChanged(object sender, EventArgs e)
        {
            string FilterText = "";
            switch (cbUserChoice.Text)
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

                case "First Name":
                    {
                        FilterText = "FirstName";
                        break;
                    }

                case "Second Name":
                    {
                        FilterText = "SecondName";
                        break;
                    }

                case "Third Name":
                    {
                        FilterText = "ThirdName";
                        break;
                    }

                case "Last Name":
                    {
                        FilterText = "LastName";
                        break;
                    }

                case "Email":
                    {
                        FilterText = "Email";
                        break;
                    }

                case "Phone":
                    {
                        FilterText = "Phone";
                        break;
                    }

                case "Country Name":
                    {
                        FilterText = "CountryName";
                        break;
                    }

                case "Gendor":
                    {
                        FilterText = "GendorCaption";
                        break;
                    }
            }

            if (FilterText == "PersonID")
            {
                if (txtUserChoice.Text == "")
                {
                    DTPeople.DefaultView.RowFilter = "";
                    lblNumebrOfPeople.Text = DTPeople.DefaultView.Count.ToString();
                    return;
                }


                DTPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterText, txtUserChoice.Text.Trim());
            }

            else
            {
                DTPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterText, txtUserChoice.Text.Trim());
            }

            lblNumebrOfPeople.Text = DTPeople.DefaultView.Count.ToString();
        }

        private void deletePersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this person this id" + 
                (int)DGVPeople.CurrentRow.Cells[0].Value, "Confirm", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
               
                if (clcPersonBusiness.Delete((int)DGVPeople.CurrentRow.Cells[0].Value))
                {
                    DataRow[] result = DTAllPeople.Select("PersonID = " + DGVPeople.CurrentRow.Cells[0].Value);

                    foreach (DataRow row in result)
                    {
                        if (row["ImagePath"] != "")
                        {
                            try
                            {
                                System.IO.File.Delete(row["ImagePath"].ToString());
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    MessageBox.Show("Person deleted successfully");
                }
                else
                {
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                RefreshPeopleList();
            }
        }

        private void editPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditPerson frmAddEditPerson = new AddEditPerson((int)DGVPeople.CurrentRow.Cells[0].Value);
            frmAddEditPerson.ShowDialog();

            RefreshPeopleList();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditPerson frmAddEditPerson = new AddEditPerson();
            frmAddEditPerson.ShowDialog();

            RefreshPeopleList();
        }

        private void txtUserChoice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbUserChoice.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
    }
}
