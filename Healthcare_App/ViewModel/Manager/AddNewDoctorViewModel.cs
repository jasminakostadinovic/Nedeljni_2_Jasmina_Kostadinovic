using DataValidations;
using Healthcare_App.Command;
using Healthcare_App.Loggers;
using Healthcare_App.View.Manager;
using HealthcareData.Models;
using HealthcareData.Repositories;
using HealthcareData.Validations;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace Healthcare_App.ViewModel.Manager
{
    class AddNewDoctorViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Fields
        private readonly AddNewDoctorView addNewDoctorView;
        private tblClinicDoctor doctor;
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

        private string[] shifts = { "morning", "afternoon", "night", "24-hour on duty"};
        private string shift;
        private string number;
        private string bankAccountNo;
        private string department;
        private bool? isInChargeOfAdmissionOfPatients;
        private int managerId;
        #endregion
        #region Properties
        public string Department
        {
            get
            {
                return department;
            }
            set
            {
                if (department == value) return;
                department = value;
                OnPropertyChanged(nameof(Department));
            }
        }
        public string BankAccountNo
        {
            get
            {
                return bankAccountNo;
            }
            set
            {
                if (bankAccountNo == value) return;
                bankAccountNo = value;
                OnPropertyChanged(nameof(BankAccountNo));
            }
        }
        public bool? IsInChargeOfAdmissionOfPatients
        {
            get { return isInChargeOfAdmissionOfPatients; }
            set
            {
                isInChargeOfAdmissionOfPatients = value;
                isInChargeOfAdmissionOfPatients = (isInChargeOfAdmissionOfPatients != null) ? value : false;
                OnPropertyChanged(nameof(IsInChargeOfAdmissionOfPatients));
            }
        }
        public string Number
        {
            get
            {
                return number;
            }
            set
            {
                if (number == value) return;
                number = value;
                OnPropertyChanged(nameof(Number));
            }
        }
  
        public string[] Shifts
        {
            get
            {
                return shifts;
            }
            set
            {
                if (shifts == value) return;
                shifts = value;
                OnPropertyChanged(nameof(Shifts));
            }
        }
        public string Shift
        {
            get
            {
                return shift;
            }
            set
            {
                if (shift == value) return;
                shift = value;
                OnPropertyChanged(nameof(Shift));
            }
        }
        public bool IsAddedNewDoctor{ get; internal set; }
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

        public tblClinicDoctor Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                doctor = value;
                OnPropertyChanged(nameof(Doctor));
            }
        }
        #endregion
        #region Constructors
        public AddNewDoctorViewModel(AddNewDoctorView addNewDoctorView, int managerId)
        {
            this.addNewDoctorView = addNewDoctorView;
            IDCardNo = string.Empty;
            Sex = string.Empty;
            Citizenship = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            GivenName = string.Empty;
            Surname = string.Empty;
            Number = string.Empty;
            Shift = string.Empty;
            BankAccountNo = string.Empty;
            department = string.Empty;
            IsInChargeOfAdmissionOfPatients = false;
            Doctor = new tblClinicDoctor();
            CanSave = true;
            userData = new tblHealthcareUserData();
            this.managerId = managerId;
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
                else if (name == nameof(Number))
                {
                    if (Number.Length != 5 
                        || validate.IsDigitsOnly(Number)
                        || !companyValidation.IsUniqueDoctorNumber(Number))
                    {
                        validationMessage = "Doctor number must be unique and 5 digits lonque!";
                        CanSave = false;
                    }
                }
                else if (name == nameof(BankAccountNo))
                {
                    if (Number.Length != 16
                        || validate.IsDigitsOnly(BankAccountNo)
                        || !companyValidation.IsUniqueBankAccountNumber(BankAccountNo))
                    {
                        validationMessage = "Bank Account number must be unique and 16 digits lonque!";
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
                || string.IsNullOrWhiteSpace(Number)
                || string.IsNullOrWhiteSpace(Shift)
                || string.IsNullOrWhiteSpace(Department)
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
                    if (IsInChargeOfAdmissionOfPatients.HasValue)
                        doctor.IsInChargeOfAdmissionOfPatients = (bool)IsInChargeOfAdmissionOfPatients;
                    else
                        doctor.IsInChargeOfAdmissionOfPatients = false;
                    doctor.Department = Department;
                    Doctor.Number = Number;
                    Doctor.BankAccountNo = bankAccountNo;
                    doctor.Shift = Shift;
                    doctor.ClinicManagerID = managerId;
                    doctor.UserDataID = userId;

                    IsAddedNewDoctor = db.TryAddNewDoctor(doctor);
                    if (IsAddedNewDoctor == false)
                    {
                        MessageBox.Show("Something went wrong. New clinic maintenance is not created.");
                        db.TryRemoveUserData(userId);
                    }

                    else
                    {
                        Logger.Instance.LogCRUD($"[{DateTime.Now.ToString("dd.MM.yyyy hh: mm")}] Created new clinic maintenance with ID Card Number : '{IDCardNo}'");
                        MessageBox.Show("The new clinic maintenance is sucessfully created.");
                    }
                    
                    addNewDoctorView.Close();
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
            IsAddedNewDoctor = false;
            addNewDoctorView.Close();
        }
        #endregion
    }
}
