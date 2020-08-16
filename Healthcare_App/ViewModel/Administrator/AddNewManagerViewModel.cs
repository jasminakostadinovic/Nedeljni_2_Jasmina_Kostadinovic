using DataValidations;
using Healthcare_App.Command;
using Healthcare_App.Loggers;
using Healthcare_App.View.Administrator;
using Healthcare_App.ViewModel.Interfaces;
using HealthcareData.Models;
using HealthcareData.Repositories;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Healthcare_App.ViewModel.Administrator
{
    class AddNewManagerViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Fields
        private readonly AddNewManagerView  addNewManagerView;
        private tblClinicManager manager;    
        private tblHealthcareUserData healthcareUserData;
        private IValidatedUserData userData;
        private readonly HealtcareDBRepository db = new HealtcareDBRepository();
        private string floorNumber;
        private int floorNumberValue;
        private string maxCountOfDoctors;
        private int maxCountOfDoctorsValue;
        private string minCountOfRooms;
        private int minCountOfRoomsValue;

        #endregion
        #region Properties  
        public string MinCountOfRooms
        {
            get
            {
                return minCountOfRooms;
            }
            set
            {
                if (minCountOfRooms == value) return;
                minCountOfRooms = value;
                OnPropertyChanged(nameof(MinCountOfRooms));
            }
        }
        public string MaxCountOfDoctors
        {
            get
            {
                return maxCountOfDoctors;
            }
            set
            {
                if (maxCountOfDoctors == value) return;
                maxCountOfDoctors = value;
                OnPropertyChanged(nameof(MaxCountOfDoctors));
            }
        }
        public string FloorNumber
        {
            get
            {
                return floorNumber;
            }
            set
            {
                if (floorNumber == value) return;
                floorNumber = value;
                OnPropertyChanged(nameof(FloorNumber));
            }
        }
        public bool IsAddedNewManager { get; internal set; }       
        public tblClinicManager Manager
        {
            get
            {
                return manager;
            }
            set
            {
                manager = value;
                OnPropertyChanged(nameof(Manager));
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
        public AddNewManagerViewModel(AddNewManagerView addNewManagerView)
        {
            this.addNewManagerView = addNewManagerView;     
            Manager = new tblClinicManager();
            healthcareUserData = new tblHealthcareUserData();
            MaxCountOfDoctors = string.Empty;
            MinCountOfRooms = string.Empty;
            FloorNumber = string.Empty;
            UserData = new UserData();
        }

        #endregion

        #region IDataErrorInfoImplementation
        //validations

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string name]
        {
            get
            {
                string validationMessage = string.Empty;
                if (name == nameof(MaxCountOfDoctors))
                {
                    if (!int.TryParse(MaxCountOfDoctors, out maxCountOfDoctorsValue) 
                        || maxCountOfDoctorsValue < 0)
                    {
                        validationMessage = "Invalid number format!";
                        UserData.CanSave = false;
                    }
                }
                else if (name == nameof(MinCountOfRooms))
                {
                    if (!int.TryParse(MinCountOfRooms, out minCountOfRoomsValue) 
                        || minCountOfRoomsValue < 0)
                    {
                        validationMessage = "Invalid number format!";
                        UserData.CanSave = false;
                    }
                }
                else if (name == nameof(FloorNumber))
                {
                    if (!int.TryParse(FloorNumber, out floorNumberValue) 
                        || floorNumberValue < 0)
                    {
                        validationMessage = "Invalid number format!";
                        UserData.CanSave = false;
                    }
                }
                if (string.IsNullOrEmpty(validationMessage))
                    UserData.CanSave = true;

                return validationMessage;
            }
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
                || string.IsNullOrWhiteSpace(MaxCountOfDoctors)
                || string.IsNullOrWhiteSpace(MinCountOfRooms)
                || string.IsNullOrWhiteSpace(FloorNumber)
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
                    manager.UserDataID = userId;
                    manager.FloorNumber = floorNumberValue;
                    manager.MaxCountOfDoctors = maxCountOfDoctorsValue;
                    manager.MinCountOfRooms = minCountOfRoomsValue;

                    IsAddedNewManager = db.TryAddNewManager(manager);
                    if (IsAddedNewManager == false)
                    {
                        MessageBox.Show("Something went wrong. New clinic manager is not created.");
                        db.TryRemoveUserData(userId);
                    }
                    else
                    {
                        Logger.Instance.LogCRUD($"[{DateTime.Now.ToString("dd.MM.yyyy hh: mm")}] Created new clinic manager with ID Card Number : '{UserData.IDCardNo}'");
                        MessageBox.Show("The new clinic manager is sucessfully created.");
                    }
                    AdministratorView administratorView = new AdministratorView();
                    addNewManagerView.Close();
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
            IsAddedNewManager = false;
            addNewManagerView.Close();
        }
        #endregion
    }
}
