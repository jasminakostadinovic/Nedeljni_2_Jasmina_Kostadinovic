using Healthcare_App.Command;
using Healthcare_App.View.Administrator;
using HealthcareData.Repositories;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Healthcare_App.ViewModel.Administrator
{
    class AdministratorViewModel : ViewModelBase
    {
        #region Fields
        readonly AdministratorView adminView;
        private readonly HealtcareDBRepository db = new HealtcareDBRepository();
        #endregion

        #region Constructor
        internal AdministratorViewModel(AdministratorView adminView)
        {
            this.adminView = adminView;
        }
        #endregion

        #region Commands

        //adding new Clinic Maintenance

        private ICommand addNewClinicMaintenance;
        public ICommand AddNewClinicMaintenance
        {
            get
            {
                if (addNewClinicMaintenance == null)
                {
                    addNewClinicMaintenance = new RelayCommand(param => AddNewClinicAdministratorExecute(), param => CanAddNewClinicAdministrator());
                }
                return addNewClinicMaintenance;
            }
        }

        private void AddNewClinicAdministratorExecute()
        {
            try
            {
                AddNewClinicMaintenanceView addNewClinicMaintenanceViewView = new AddNewClinicMaintenanceView();
                addNewClinicMaintenanceViewView.ShowDialog();
                adminView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddNewClinicAdministrator()
        {
            return true;
        }

        //closing the view

        private ICommand logout;
        public ICommand Logout
        {
            get
            {
                if (logout == null)
                {
                    logout = new RelayCommand(param => ExitExecute(), param => CanExitExecute());
                }
                return logout;
            }
        }

        private bool CanExitExecute()
        {
            return true;
        }

        private void ExitExecute()
        {
            MainWindow loginWindow = new MainWindow();
            adminView.Close();
            loginWindow.Show();
        }
        #endregion
    }
}
