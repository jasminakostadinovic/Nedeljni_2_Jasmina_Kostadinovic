using DataValidations;
using Healthcare_App.Command;
using Healthcare_App.Loggers;
using Healthcare_App.View.Administrator;
using Healthcare_App.ViewModel.Interfaces;
using HealthcareData.Models;
using HealthcareData.Repositories;
using System;
using System.Windows;
using System.Windows.Input;

namespace Healthcare_App.ViewModel.Administrator
{
    class AddNewClinicMaintenanceViewModel : ViewModelBase
    {
        #region Fields
        private readonly AddNewClinicMaintenanceView addNewMaintenanceView;
        private tblClinicMaintenance maintenance;  
        private tblHealthcareUserData healthcareUserData;
        private IValidatedUserData userData;
        private readonly HealtcareDBRepository db = new HealtcareDBRepository();
        public string[] permits = { "yes", "no" };
        public string[] responsibilities = { "For Disabled Access", "For Ambulance Access " };
        public string permit;
        public string responsibility;
        #endregion
        #region Properties
        public IValidatedUserData UserData
        {
            get
            {
                return userData;
            }
            set
            {
                userData = value;
                OnPropertyChanged(nameof(UserData));
            }
        }
        public string Responsibility
        {
            get
            {
                return responsibility;
            }
            set
            {
                if (responsibility == value) return;
                responsibility = value;
                OnPropertyChanged(nameof(Responsibility));
            }
        }
        public string[] Responsibilities
        {
            get
            {
                return responsibilities;
            }
            set
            {
                if (responsibilities == value) return;
                responsibilities = value;
                OnPropertyChanged(nameof(Responsibilities));
            }
        }
        public string[] Permits
        {
            get
            {
                return permits;
            }
            set
            {
                if (permits == value) return;
                permits = value;
                OnPropertyChanged(nameof(Permits));
            }
        }
        public string Permit
        {
            get
            {
                return permit;
            }
            set
            {
                if (permit == value) return;
                permit = value;
                OnPropertyChanged(nameof(Permit));
            }
        }
        public bool IsAddedNewMaintenance { get; internal set; }       

        public tblClinicMaintenance Maintenance
        {
            get
            {
                return maintenance;
            }
            set
            {
                maintenance = value;
                OnPropertyChanged(nameof(Maintenance));
            }
        }
        #endregion
        #region Constructors
        public AddNewClinicMaintenanceViewModel(AddNewClinicMaintenanceView addNewMaintenanceView)
        {
            this.addNewMaintenanceView = addNewMaintenanceView;       
            Responsibility = string.Empty;
            Permit = string.Empty;
            Maintenance = new tblClinicMaintenance();
            healthcareUserData = new tblHealthcareUserData();
            UserData = new UserData();
        }

        #endregion

        #region Commands
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        private bool CanSaveExecute()
        {
            if (
                string.IsNullOrWhiteSpace(UserData.GivenName)
                || string.IsNullOrWhiteSpace(UserData.Surname)
                || string.IsNullOrWhiteSpace(UserData.Sex)
                || string.IsNullOrWhiteSpace(UserData.Citizenship)
                || string.IsNullOrWhiteSpace(UserData.DateOfBirth)
                || string.IsNullOrWhiteSpace(UserData.IDCardNo)
                || string.IsNullOrWhiteSpace(UserData.Username)
                || string.IsNullOrWhiteSpace(UserData.Password)
                || string.IsNullOrWhiteSpace(Responsibility)
                || string.IsNullOrWhiteSpace(Permit)
                || UserData.CanSave == false)
                return false;
            return true;
        }
        private void SaveExecute()
        {
            try
            {
                healthcareUserData.GivenName = UserData.GivenName;
                healthcareUserData.Surname = UserData.Surname;
                healthcareUserData.IDCardNo = UserData.IDCardNo;
                healthcareUserData.Sex = UserData.Sex;
                healthcareUserData.DateOfBirth = UserData.DateDateValue;
                healthcareUserData.Citizenship = UserData.Citizenship;
                healthcareUserData.Username = UserData.Username;
                healthcareUserData.Password = SecurePasswordHasher.Hash(UserData.Password);

                //adding new administrator to database 
                db.TryAddNewUserData(healthcareUserData);
                var userId = db.GetUserDataId(UserData.Username);
                if (userId != 0)
                {
                    maintenance.UserDataID = userId;
                    if (permit == permits[0])
                        maintenance.HasExpansionPermit = true;
                    else
                        maintenance.HasExpansionPermit = false;
                    if(responsibility == responsibilities[0])
                    {
                        maintenance.IsResponsibleForDisabledAccess = true;
                        maintenance.IsResponsibleForAmbulanceAccess = false;
                    }
                    else
                    {
                        maintenance.IsResponsibleForDisabledAccess = false;
                        maintenance.IsResponsibleForAmbulanceAccess = true;
                    }
                   
                    IsAddedNewMaintenance = db.TryAddNewMaintenance(maintenance);
                    if (IsAddedNewMaintenance == false)
                    {
                        MessageBox.Show("Something went wrong. New clinic maintenance is not created.");
                        db.TryRemoveUserData(userId);
                    }

                    else
                    {
                        Logger.Instance.LogCRUD($"[{DateTime.Now.ToString("dd.MM.yyyy hh: mm")}] Created new clinic maintenance with ID Card Number : '{UserData.IDCardNo}'");
                        MessageBox.Show("The new clinic maintenance is sucessfully created.");
                    }
                    AdministratorView administratorView = new AdministratorView();
                    addNewMaintenanceView.Close();
                    administratorView.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Escaping action
        private ICommand exit;
        public ICommand Exit
        {
            get
            {
                if (exit == null)
                {
                    exit = new RelayCommand(param => ExitExecute(), param => CanExitExecute());
                }
                return exit;
            }
        }
        private bool CanExitExecute()
        {
            return true;
        }

        private void ExitExecute()
        {
            AdministratorView administratorView = new AdministratorView(); 
            administratorView.Show();
            IsAddedNewMaintenance = false;
            addNewMaintenanceView.Close();
        }
        #endregion
    }
}
