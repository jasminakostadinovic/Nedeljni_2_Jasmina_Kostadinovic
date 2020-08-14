using DataValidations;
using Healthcare_App.Command;
using Healthcare_App.Loggers;
using Healthcare_App.View.Administrator;
using HealthcareData.Models;
using HealthcareData.Repositories;
using HealthcareData.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Healthcare_App.ViewModel.Administrator
{
    class AddNewManagerViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Fields
        private readonly AddNewManagerView  addNewManagerView;
        private tblClinicManager manager;
        private string surname;
        private string givenName;
        private string idCardNo;
        private string sex;
        private string citizenship;
        private string dateOfBirth;
        private DateTime dateDateValue;
        private string username;
        private string password;
        private tblHealthcareUserData userData;
        private readonly HealtcareDBRepository db = new HealtcareDBRepository();
        private string[] sexTypes = { "M", "F", "N", "X" };

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
        public bool CanSave { get; set; }
        public string DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }
            set
            {
                if (dateOfBirth == value) return;
                dateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }
        public string[] SexTypes
        {
            get
            {
                return sexTypes;
            }
            set
            {
                if (sexTypes == value) return;
                sexTypes = value;
                OnPropertyChanged(nameof(SexTypes));
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (password == value) return;
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                if (username == value) return;
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Citizenship
        {
            get
            {
                return citizenship;
            }
            set
            {
                if (citizenship == value) return;
                citizenship = value;
                OnPropertyChanged(nameof(Citizenship));
            }
        }
        public string Sex
        {
            get
            {
                return sex;
            }
            set
            {
                if (sex == value) return;
                sex = value;
                OnPropertyChanged(nameof(Sex));
            }
        }
        public string IDCardNo
        {
            get
            {
                return idCardNo;
            }
            set
            {
                if (idCardNo == value) return;
                idCardNo = value;
                OnPropertyChanged(nameof(IDCardNo));
            }
        }
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                if (surname == value) return;
                surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        public string GivenName
        {
            get
            {
                return givenName;
            }
            set
            {
                if (givenName == value) return;
                givenName = value;
                OnPropertyChanged(nameof(GivenName));
            }
        }

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
        #endregion
        #region Constructors
        public AddNewManagerViewModel(AddNewManagerView addNewManagerView)
        {
            this.addNewManagerView = addNewManagerView;
            IDCardNo = string.Empty;
            Sex = string.Empty;
            Citizenship = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            GivenName = string.Empty;
            Surname = string.Empty;
            Manager = new tblClinicManager();
            CanSave = true;
            userData = new tblHealthcareUserData();
            MaxCountOfDoctors = string.Empty;
            MinCountOfRooms = string.Empty;
            FloorNumber = string.Empty;
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
                CanSave = true;
                var validate = new Validations();
                var companyValidation = new HealthcareValidations();
                string validationMessage = string.Empty;

                if (name == nameof(IDCardNo))
                {
                    if (!validate.IsValidIDCardFormat(IDCardNo))
                    {
                        validationMessage = "Invalid ID Card number format!";
                        CanSave = false;
                    }

                    if (!companyValidation.IsUniqueIDCardNo(IDCardNo))
                    {
                        validationMessage = "ID Card number must be unique!";
                        CanSave = false;
                    }
                }
                else if (name == nameof(Username))
                {
                    if (!companyValidation.IsUniqueUsername(Username))
                    {
                        validationMessage = "Username number must be unique!";
                        CanSave = false;
                    }
                }
                else if (name == nameof(Password))
                {
                    if (!validate.IsValidPasswordFormat(Password))
                    {
                        validationMessage = "Password must be between at least 8 characters long, must contains at least one number, uppercase letter and special character.";
                        CanSave = false;
                    }
                }
                else if (name == nameof(DateOfBirth))
                {
                    var culture = CultureInfo.InvariantCulture;
                    var styles = DateTimeStyles.None;
                    if (!DateTime.TryParse(DateOfBirth, culture, styles, out dateDateValue) || dateDateValue.Year < 1900)
                    {
                        validationMessage = "Invalid date format! use: [4/10/2008]";
                        CanSave = false;
                    }
                }
                else if (name == nameof(MaxCountOfDoctors))
                {
                    if (!int.TryParse(MaxCountOfDoctors, out maxCountOfDoctorsValue) 
                        || maxCountOfDoctorsValue < 0)
                    {
                        validationMessage = "Invalid number format!";
                        CanSave = false;
                    }
                }
                else if (name == nameof(MinCountOfRooms))
                {
                    if (!int.TryParse(MinCountOfRooms, out minCountOfRoomsValue) 
                        || minCountOfRoomsValue < 0)
                    {
                        validationMessage = "Invalid number format!";
                        CanSave = false;
                    }
                }
                else if (name == nameof(FloorNumber))
                {
                    if (!int.TryParse(FloorNumber, out floorNumberValue) 
                        || floorNumberValue < 0)
                    {
                        validationMessage = "Invalid number format!";
                        CanSave = false;
                    }
                }
                if (string.IsNullOrEmpty(validationMessage))
                    CanSave = true;

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
                string.IsNullOrWhiteSpace(GivenName)
                || string.IsNullOrWhiteSpace(Surname)
                || string.IsNullOrWhiteSpace(Sex)
                || string.IsNullOrWhiteSpace(Citizenship)
                || string.IsNullOrWhiteSpace(DateOfBirth)
                || string.IsNullOrWhiteSpace(IDCardNo)
                || string.IsNullOrWhiteSpace(Username)
                || string.IsNullOrWhiteSpace(Password)
                || string.IsNullOrWhiteSpace(MaxCountOfDoctors)
                || string.IsNullOrWhiteSpace(MinCountOfRooms)
                || string.IsNullOrWhiteSpace(FloorNumber)
                || CanSave == false)
                return false;
            return true;
        }
        private void SaveExecute()
        {
            try
            {
                userData.GivenName = GivenName;
                userData.Surname = Surname;
                userData.IDCardNo = IDCardNo;
                userData.Sex = Sex;
                userData.DateOfBirth = dateDateValue;
                userData.Citizenship = Citizenship;
                userData.Username = Username;
                userData.Password = SecurePasswordHasher.Hash(Password);

                //adding new administrator to database 
                db.TryAddNewUserData(userData);
                var userId = db.GetUserDataId(Username);
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
                        Logger.Instance.LogCRUD($"[{DateTime.Now.ToString("dd.MM.yyyy hh: mm")}] Created new clinic manager with ID Card Number : '{IDCardNo}'");
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
            IsAddedNewManager = false;
            addNewManagerView.Close();
        }
        #endregion
    }
}
