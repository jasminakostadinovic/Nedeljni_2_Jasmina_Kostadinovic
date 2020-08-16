using DataValidations;
using Healthcare_App.Command;
using Healthcare_App.Loggers;
using Healthcare_App.View.Master;
using Healthcare_App.ViewModel.Interfaces;
using HealthcareData.Models;
using HealthcareData.Repositories;
using System;
using System.Windows;
using System.Windows.Input;

namespace Healthcare_App.ViewModel.Master
{
    class AddNewClinicAdministratorViewModel : ViewModelBase
    {
        #region Fields
        private readonly AddNewClinicAdministratorView addNewAdministratorView;
        private tblClinicAdministrator administrator;     
        private tblHealthcareUserData healthcareUserData;
        private readonly HealtcareDBRepository db = new HealtcareDBRepository();
        private IValidatedUserData userData;
        #endregion
        #region Properties
        public bool IsAddedNewAdministrator { get; internal set; }
    
        public tblClinicAdministrator Administrator
        {
            get
            {
                return administrator;
            }
            set
            {
                administrator = value;
                OnPropertyChanged(nameof(Administrator));
            }
        }
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
        #endregion
        #region Constructors
        public AddNewClinicAdministratorViewModel(AddNewClinicAdministratorView addNewAdministratorView)
        {
            this.addNewAdministratorView = addNewAdministratorView;
            UserData = new UserData();       
            Administrator = new tblClinicAdministrator();     
            healthcareUserData = new tblHealthcareUserData();
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
                    administrator.UserDataID = userId;
                    IsAddedNewAdministrator = db.TryAddNewAdministrator(administrator);
                    if (IsAddedNewAdministrator == false)
                    {
                        MessageBox.Show("Something went wrong. New administrator is not created.");
                        db.TryRemoveUserData(userId);
                    }               
                    else
                    {
                        Logger.Instance.LogCRUD($"[{DateTime.Now.ToString("dd.MM.yyyy hh: mm")}] Created new administrator with ID Card Number : '{UserData.IDCardNo}'");
                        MessageBox.Show("The new clinic administrator is sucessfully created.");
                    }
                    MasterView masterView = new MasterView();
                    addNewAdministratorView.Close();
                    masterView.Show();
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
            IsAddedNewAdministrator = false;
            addNewAdministratorView.Close();
        }
        #endregion
    }
}
