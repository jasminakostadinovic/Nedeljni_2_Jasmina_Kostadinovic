﻿using Healthcare_App.Command;
using Healthcare_App.View;
using Healthcare_App.View.Administrator;
using HealthcareData.Models;
using HealthcareData.Repositories;
using System;
using System.Collections.Generic;
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
        private List<tblClinicMaintenance> clinicMaintenances;
        private tblClinicMaintenance clinicMaintenance;
        private List<tblHealthcareInstitution> healthcareInstitutions;
        private tblHealthcareInstitution healthcareInstitution;
        #endregion

        #region Constructor
        internal AdministratorViewModel(AdministratorView adminView)
        {
            this.adminView = adminView;
            ClinicMaintenance = new tblClinicMaintenance();
            ClinicMaintenances = LoadClinicMaintenance();
            HealtcareIstitution = new tblHealthcareInstitution();
            HealtcareIstitutions = LoadHealthcareInstitutions();
        }

        private List<tblHealthcareInstitution> LoadHealthcareInstitutions()
        {
            return db.LoadHealthcareInstitutions();
        }

        private List<tblClinicMaintenance> LoadClinicMaintenance()
        {
            return db.LoadMaintenances();
        }
        #endregion

        #region Properies
        public List<tblHealthcareInstitution> HealtcareIstitutions
        {
            get
            {
                return healthcareInstitutions;
            }
            set
            {
                healthcareInstitutions = value;
                OnPropertyChanged(nameof(HealtcareIstitutions));
            }
        }
        public tblHealthcareInstitution HealtcareIstitution
        {
            get
            {
                return healthcareInstitution;
            }
            set
            {
                healthcareInstitution = value;
                OnPropertyChanged(nameof(HealtcareIstitution));
            }
        }
        public tblClinicMaintenance ClinicMaintenance
        {
            get
            {
                return clinicMaintenance;
            }
            set
            {
                clinicMaintenance = value;
                OnPropertyChanged(nameof(ClinicMaintenance));
            }
        }
        public List<tblClinicMaintenance> ClinicMaintenances
        {
            get
            {
                return clinicMaintenances;
            }
            set
            {
                clinicMaintenances = value;
                OnPropertyChanged(nameof(ClinicMaintenances));
            }
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
                    addNewClinicMaintenance = new RelayCommand(param => AddNewClinicMaintenanceExecute(), param => CanAddNewClinicMaintenance());
                }
                return addNewClinicMaintenance;
            }
        }

        private void AddNewClinicMaintenanceExecute()
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
        private bool CanAddNewClinicMaintenance()
        {
            return true;
        }

        //removing Clinic Maintenance
        private ICommand deleteClinicMaintenance;
        public ICommand DeleteClinicMaintenance
        {
            get
            {
                if (deleteClinicMaintenance == null)
                {
                    deleteClinicMaintenance = new RelayCommand(param => DeleteClinicMaintenanceExecute(), param => CanDeleteClinicMaintenance());
                }
                return deleteClinicMaintenance;
            }
        }

        private bool CanDeleteClinicMaintenance()
        {
            if (ClinicMaintenance == null)
                return false;
            return true;
        }

        private void DeleteClinicMaintenanceExecute()
        {
            try
            {
                if (ClinicMaintenance != null)
                {
                    ShouldDeleteView deleteOrder = new ShouldDeleteView(); 
                    deleteOrder.lblText.Content = "Are you sure you want to delete this clinic maintenance?";
                    deleteOrder.ShowDialog();
                    if ((deleteOrder.DataContext as ShouldDeleteViewModel).ShouldDelete == true)
                    {
                        bool isRemovedClinicMaintenance = db.TryRemoveClinicMaintenance(ClinicMaintenance.ClinicMaintenanceID);
                        if (isRemovedClinicMaintenance == true)
                        {
                            db.TryRemoveUserData(ClinicMaintenance.UserDataID);
                            MessageBox.Show("You have successfully deleted the clinic maintenance.");
                            ClinicMaintenances = LoadClinicMaintenance();
                        }
                        else
                            MessageBox.Show("Something went wrong. The clinic maintenance is not deleted.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //logging out

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
