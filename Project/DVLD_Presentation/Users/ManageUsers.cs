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
    public partial class ManageUsers : Form
    {
        DataTable DTAllUsers;

        private void _RefreshUsersList()
        {
            DTAllUsers = clcUsersBusiness.GetAllUsers();
            DGVUsers.DataSource = DTAllUsers;
            cbUserChoice.SelectedIndex = 0;
            txtUserChoice.Visible = false;
            cbIsActiveChoice.Visible = false;
            lblNumebrOfUser.Text = DTAllUsers.Rows.Count.ToString();

        }

        public ManageUsers()
        {
            InitializeComponent();
        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();

            if(DGVUsers.Rows.Count > 0)
            {
                DGVUsers.Columns[0].HeaderText = "User ID";
                DGVUsers.Columns[0].Width = 110;

                DGVUsers.Columns[1].HeaderText = "Person ID";
                DGVUsers.Columns[1].Width = 110;

                DGVUsers.Columns[2].HeaderText = "Full Name";
                DGVUsers.Columns[2].Width = 420;

                DGVUsers.Columns[3].HeaderText = "User Name";
                DGVUsers.Columns[3].Width = 140;

                DGVUsers.Columns[4].HeaderText = "Is Active";
                DGVUsers.Columns[4].Width = 110;
            }


        }

        private void cbUserChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbUserChoice.SelectedIndex == 0)
            {
                txtUserChoice.Visible = false;
                cbIsActiveChoice.Visible = false;
            }
            else if(cbUserChoice.Text == "Is Active")
            {
                txtUserChoice.Visible = false;
                cbIsActiveChoice.Visible = true;
                cbIsActiveChoice.SelectedIndex = 0;
            }
            else
            {
                txtUserChoice.Visible = true;
                txtUserChoice.Text = "";
                cbIsActiveChoice.Visible = false;
                txtUserChoice.Focus();
            }

            DTAllUsers.DefaultView.RowFilter = "";
            lblNumebrOfUser.Text = DTAllUsers.DefaultView.Count.ToString();
        }

        private void txtUserChoice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbUserChoice.Text == "Person ID" || cbUserChoice.Text == "User ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void txtUserChoice_TextChanged(object sender, EventArgs e)
        {
            if(cbUserChoice.SelectedIndex == 0 || cbUserChoice.Text == "Is Active")
            {
                return; 
            }

            string FilteredText = "";
            switch (cbUserChoice.Text)
            {
                case "User ID":
                    {
                        FilteredText = "UserID";
                        break;
                    }

                case "Person ID":
                    {
                        FilteredText = "PersonID";
                        break;
                    }

                case "User Name":
                    {
                        FilteredText = "UserName";
                        break;
                    }

                case "Full Name":
                    {
                        FilteredText = "FullName";
                        break;
                    }

                default:
                    {
                        break;
                    }
            }

            if (FilteredText == "PersonID" || FilteredText == "UserID")
            {
                if (txtUserChoice.Text == "")
                {
                    DTAllUsers.DefaultView.RowFilter = "";
                    lblNumebrOfUser.Text = DTAllUsers.DefaultView.Count.ToString();
                    return;
                }


                DTAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilteredText, txtUserChoice.Text.Trim());
            }

            else
            {
                DTAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilteredText, txtUserChoice.Text.Trim());
            }

            lblNumebrOfUser.Text = DTAllUsers.DefaultView.Count.ToString();

        }

        private void cbIsActiveChoice_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch(cbIsActiveChoice.Text)
            {
                case "All":
                    {
                        DTAllUsers.DefaultView.RowFilter = "";
                        break;
                    }

                case "YES":
                    {
                        DTAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", "IsActive", 1);
                        break;
                    }

                case "NO":
                    {
                        DTAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", "IsActive", 0);
                        break;
                    }

                default:
                    {
                        break; 
                    }
            }


            lblNumebrOfUser.Text = DTAllUsers.DefaultView.Count.ToString();
        }

        private void showPersonInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowUserInfo frmShowUserInfo = new ShowUserInfo((int)DGVUsers.CurrentRow.Cells[0].Value);
            frmShowUserInfo.ShowDialog();
            _RefreshUsersList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            AddEditUser addEditUser = new AddEditUser();
            addEditUser.ShowDialog();
            _RefreshUsersList();
        }

        private void editPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditUser editEditUser = new AddEditUser((int)DGVUsers.CurrentRow.Cells[0].Value);
            editEditUser.ShowDialog();
            _RefreshUsersList();
        }

        private void deletePersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show(@"Are you sure you want to delete user with ID "
                  + (int)DGVUsers.CurrentRow.Cells[0].Value, "Confirmation", 
                  MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                if(clcUsersBusiness.DeleteUser((int)DGVUsers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User deleted successfully");
                }
                else
                {
                    MessageBox.Show("User was not deleted becauese there are data link to it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            _RefreshUsersList();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditUser addEditUser = new AddEditUser();
            addEditUser.ShowDialog();
            _RefreshUsersList();
        }

        private void findPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword frmChangePassword = new ChangePassword((int)DGVUsers.CurrentRow.Cells[0].Value);
            frmChangePassword.ShowDialog();
            _RefreshUsersList();
        }
    }
}
