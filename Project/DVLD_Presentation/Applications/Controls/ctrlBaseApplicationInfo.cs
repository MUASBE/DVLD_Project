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
    public partial class ctrlBaseApplicationInfo : UserControl
    {

        public event Action<int> OnApplicationClick;
        // Create a protected method to raise the event with a parameter
        protected virtual void ApplicationClick(int ApplicationID)
        {
            Action<int> handler = OnApplicationClick;
            if (handler != null)
            {
                handler(ApplicationID); // Raise the event with the parameter
            }
        }

        private int _AppliactionID; 
        private clcApplicationBusiness _Application;
        public int ApplicationID 
        {
            get
            {
                return _AppliactionID;
            }
        }
        public clcApplicationBusiness Application 
        {
            get
            {
                return _Application;
            }
        }
        private void _resetDefaultValue()
        {
            _AppliactionID = 0;
            _Application = null;
        }
        public ctrlBaseApplicationInfo()
        {
            InitializeComponent();
        }
        public void LoadApplicationInfo(int ApplicationID)
        {
            _AppliactionID = ApplicationID;
            _Application = clcApplicationBusiness.Find(ApplicationID);
            if (_Application != null)
            {
                lblApplicationID.Text = _Application.ApplicationID.ToString();
                lblApplicant.Text = _Application.ApplicantPersonInfo.FirstName + " " + _Application.ApplicantPersonInfo.SecondName
                    + " " + _Application.ApplicantPersonInfo.ThirdName + " " + _Application.ApplicantPersonInfo.LastName ;
                lblStatus.Text = _Application.ApplicationStatus.ToString();
                lblType.Text = _Application.ApplicationTypeInfo.ApplicationTypeName;
                lblFees.Text = _Application.PaidFees.ToString("0.00");
                lblDate.Text = _Application.ApplicationDate.ToShortDateString();
                lblStatusDate.Text = _Application.LastStatusDate.ToShortDateString();
                lblCreatedByUser.Text = _Application.GreatedUserInfo.UserName;

                if (OnApplicationClick != null)
                {
                    OnApplicationClick(_AppliactionID);
                }

            }
            else
            {
                MessageBox.Show("Appliaction was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _resetDefaultValue();
            }
        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPersonInfo frmShowPersonInfo = new ShowPersonInfo(_Application.ApplicantPersonID);
            frmShowPersonInfo.ShowDialog();
            LoadApplicationInfo(_Application.ApplicationID);
        }
    }
}
