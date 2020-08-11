using DataValidations;
using Healthcare_App.Command;
using Healthcare_App.Loggers;
using Healthcare_App.View.Master;
using HealthcareData.Models;
using HealthcareData.Repositories;
using HealthcareData.Validations;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace Healthcare_App.ViewModel.Master
{
    class AddNewClinicAdministratorViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Fields
        private readonly AddNewClinicAdministratorView addNewAdministratorView;
        private tblClinicAdministrator administrator;
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
        private string[] sexTypes = new string[] { "M", "F", "N", "X" };
        #endregion
        #region Properties
        public bool IsAddedNewAdministrator { get; internal set; }
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
        #endregion
        #region Constructors
        public AddNewClinicAdministratorViewModel(AddNewClinicAdministratorView addNewAdministratorView)
        {
            this.addNewAdministratorView = addNewAdministratorView;
            IDCardNo = string.Empty;
            Sex = string.Empty;
            Citizenship = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            GivenName = string.Empty;
            Surname = string.Empty;
            Administrator = new tblClinicAdministrator();
            CanSave = true;
            userData = new tblHealthcareUserData();
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
                    if (!DateTime.TryParse(DateOfBirth, culture, styles, out dateDateValue))
                    {
                        validationMessage = "Invalid date format! use: [4/10/2008]";
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
                    administrator.UserDataID = userId;
                    IsAddedNewAdministrator = db.TryAddNewAdministrator(administrator);
                    if (IsAddedNewAdministrator == false)
                        MessageBox.Show("Something went wrong. New administrator is not created.");
                    else
                    {
                        Logger.Instance.LogCRUD($"[{DateTime.Now.ToString("dd.MM.yyyy hh: mm")}] Created new administrator with ID Card Number : '{IDCardNo}'");
                    }
                    addNewAdministratorView.Close();
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
