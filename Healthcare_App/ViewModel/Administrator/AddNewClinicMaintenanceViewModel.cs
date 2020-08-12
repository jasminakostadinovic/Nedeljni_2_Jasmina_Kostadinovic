using DataValidations;
using Healthcare_App.Command;
using Healthcare_App.Loggers;
using Healthcare_App.View.Administrator;
using HealthcareData.Models;
using HealthcareData.Repositories;
using HealthcareData.Validations;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace Healthcare_App.ViewModel.Administrator
{
    class AddNewClinicMaintenanceViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Fields
        private readonly AddNewClinicMaintenanceView addNewMaintenanceView;
        private tblClinicMaintenance maintenance;
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

        public string[] permits = { "yes", "no" };
        public string[] responsibilities = { "For Disabled Access", "For Ambulance Access " };
        public string permit;
        public string responsibility;
        #endregion
        #region Properties
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
            IDCardNo = string.Empty;
            Sex = string.Empty;
            Citizenship = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            GivenName = string.Empty;
            Surname = string.Empty;
            Responsibility = string.Empty;
            Permit = string.Empty;
            Maintenance = new tblClinicMaintenance();
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
                || string.IsNullOrWhiteSpace(Responsibility)
                || string.IsNullOrWhiteSpace(Permit)
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
                        Logger.Instance.LogCRUD($"[{DateTime.Now.ToString("dd.MM.yyyy hh: mm")}] Created new clinic maintenance with ID Card Number : '{IDCardNo}'");
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
            IsAddedNewMaintenance = false;
            addNewMaintenanceView.Close();
        }
        #endregion
    }
}
