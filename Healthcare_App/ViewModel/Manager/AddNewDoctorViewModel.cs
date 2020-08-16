using DataValidations;
using Healthcare_App.Command;
using Healthcare_App.Loggers;
using Healthcare_App.View.Manager;
using Healthcare_App.ViewModel.Interfaces;
using HealthcareData.Models;
using HealthcareData.Repositories;
using HealthcareData.Validations;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Healthcare_App.ViewModel.Manager
{
    class AddNewDoctorViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Fields
        private readonly AddNewDoctorView addNewDoctorView;
        private tblClinicDoctor doctor;      
        private tblHealthcareUserData healthcareUserData;
        private IValidatedUserData userData;
        private readonly HealtcareDBRepository db = new HealtcareDBRepository();
        private string[] shifts = { "morning", "afternoon", "night", "24-hour on duty"};
        private string shift;
        private string number;
        private string bankAccountNo;
        private string department;
        private bool? isInChargeOfAdmissionOfPatients;
        private int managerId;
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
            Number = string.Empty;
            Shift = string.Empty;
            BankAccountNo = string.Empty;
            Department = string.Empty;
            IsInChargeOfAdmissionOfPatients = false;
            Doctor = new tblClinicDoctor();
            healthcareUserData = new tblHealthcareUserData();
            this.managerId = managerId;
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
                var validate = new Validations();
                var companyValidation = new HealthcareValidations();
                string validationMessage = string.Empty;               
                if (name == nameof(BankAccountNo))
                {
                    if (BankAccountNo.Length != 16
                        || !validate.IsDigitsOnly(BankAccountNo)
                        || !companyValidation.IsUniqueBankAccountNumber(BankAccountNo))
                    {
                        validationMessage = "Bank Account number must be unique and 16 digits lonque!";
                        UserData.CanSave = false;
                    }
                }
                else if(name == nameof(Number))
                {
                    if (Number.Length != 5
                        || !validate.IsDigitsOnly(Number)
                        || !companyValidation.IsUniqueDoctorNumber(Number))
                    {
                        validationMessage = "Doctor number must be unique and 5 digits lonque!";
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
                || string.IsNullOrWhiteSpace(Number)
                || string.IsNullOrWhiteSpace(Shift)
                || string.IsNullOrWhiteSpace(Department)
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

                //adding new doctor to database 
                db.TryAddNewUserData(healthcareUserData);
                var userId = db.GetUserDataId(UserData.Username);
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
                        MessageBox.Show("Something went wrong. New clinic doctor is not created.");
                        db.TryRemoveUserData(userId);
                    }
                    else
                    {
                        Logger.Instance.LogCRUD($"[{DateTime.Now.ToString("dd.MM.yyyy hh: mm")}] Created new clinic doctor with ID Card Number : '{UserData.IDCardNo}'");
                        MessageBox.Show("The new clinic doctor is sucessfully created.");
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
