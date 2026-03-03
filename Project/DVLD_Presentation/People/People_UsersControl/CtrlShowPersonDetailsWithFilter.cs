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
    public partial class CtrlShowPersonDetailsWithFilter : UserControl
    {
        //Define a custom event handler delegate with parameters
        public event Action<int> OnPersonClick;
        // Create a protected method to raise the event with a parameter
        protected virtual void PersonClick(int PersonID)
        {
            Action<int> handler = OnPersonClick;
            if (handler != null)
            {
                handler(PersonID); // Raise the event with the parameter
            }
        }

        private int _PersonID;
        public int PersonID
        {
            get { return ctrlShowPersonDetails1._PersonID; }
        }

        private bool _FilterEnable;
        public bool FilterEnable
        {
            get { return _FilterEnable; }
            set
            { 
                _FilterEnable = value;
                GbFilter.Enabled = _FilterEnable;
            }
        }


        private bool _EnableAddPersonButton;
        public bool EnableAddPersonButton
        {
            get { return _EnableAddPersonButton; }
            set {
                    _EnableAddPersonButton = value;
                    btnAddPerson.Enabled = _EnableAddPersonButton;
                }
        }

        public clcPersonBusiness PersonInfo
        {
            get { return ctrlShowPersonDetails1.PersonInfo; }
        }
        private void FindNow()
        {
            switch (cbUserChoice.SelectedIndex)
            {
                case 0:
                    {
                        ctrlShowPersonDetails1.LoadPersonInfo(Convert.ToInt32(txtUserChoice.Text));
                        break;
                    }

                case 1:
                    {
                        ctrlShowPersonDetails1.LoadPersonInfo(txtUserChoice.Text.ToString().Trim());
                        break;
                    }

                default:
                    {
                        break;
                    }
            }

            if(OnPersonClick != null && FilterEnable)
            {
                OnPersonClick(ctrlShowPersonDetails1._PersonID);
            }

        }
        public void LoadPersonInfo(int PersonID)
        {

            cbUserChoice.SelectedIndex = 0;
            txtUserChoice.Text = PersonID.ToString();
            FindNow();

        }
        public CtrlShowPersonDetailsWithFilter()
        {
            InitializeComponent();
           
        }
        private void CtrlShowPersonDetailsWithFilter_Load(object sender, EventArgs e)
        {
            cbUserChoice.SelectedIndex = 0;
            cbUserChoice.Focus();
        }

        private void btnFindPerson_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())
            {
                return;
            }
            FindNow();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            AddEditPerson FrmAddEditPerson = new AddEditPerson();
            FrmAddEditPerson.DataBack += DataBackEvent;
            FrmAddEditPerson.ShowDialog(); 
        }

        public void DataBackEvent(object sender, int PersonID)
        {
            cbUserChoice.SelectedIndex = 1;
            txtUserChoice.Text = PersonID.ToString();
            ctrlShowPersonDetails1.LoadPersonInfo(PersonID);
        }

        private void txtUserChoice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                btnFindPerson.PerformClick();
            }

            if(cbUserChoice.SelectedIndex == 0)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }

        }

        private void cbUserChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUserChoice.Text = "";
            cbUserChoice.Focus();
        }

        private void txtUserChoice_Validating(object sender, CancelEventArgs e)
        {
            if(txtUserChoice.Text == "")
            {
                errorProvider1.SetError(txtUserChoice, "Filed is require");
                e.Cancel = true;
                txtUserChoice.Focus();
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserChoice, null);
            }
        }

        private void txtUserChoice_TextChanged_1(object sender, EventArgs e)
        {
            if (txtUserChoice.Text.Contains("\n"))
            {
                txtUserChoice.Text = txtUserChoice.Text.Substring(0, txtUserChoice.Text.Length - 2);
            }
        }
    }
}
