using DataValidations;
using Healthcare_App.Command;
using Healthcare_App.Loggers;
using Healthcare_App.View.Registration;
using HealthcareData.Models;
using HealthcareData.Repositories;
using HealthcareData.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Healthcare_App.ViewModel.Registration
{
    class RegistrationViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Fields
        private readonly RegistrationView registrationView;
        private tblClinicPatient patient;
        private tblHealthcareUserData healthcareUserData;
        private UserData userData;
        private readonly HealtcareDBRepository db = new HealtcareDBRepository();
        private List<tblClinicDoctor> doctors;
        private tblClinicDoctor doctor;
        private string healthInsuranceCardNo;
        #endregion
        #region Properties
        public string HealthInsuranceCardNo
        {
            get
            {
                return healthInsuranceCardNo;
            }
            set
            {
                healthInsuranceCardNo = value;
                OnPropertyChanged(nameof(HealthInsuranceCardNo));
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
        public List<tblClinicDoctor> Doctors
        {
            get
            {
                return doctors;
            }
            set
            {
                doctors = value;
                OnPropertyChanged(nameof(Doctors));
            }
        }
        public UserData UserData
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
      
        public bool IsAddedNewPatient { get; internal set; }

        public tblClinicPatient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
                OnPropertyChanged(nameof(Patient));
            }
        }
        #endregion
        #region Constructors
        public RegistrationViewModel(RegistrationView registrationView)
        {
            this.registrationView = registrationView;
            HealthInsuranceCardNo = string.Empty;
            Patient = new tblClinicPatient();
            healthcareUserData = new tblHealthcareUserData();
            UserData = new UserData();
            Doctor = new tblClinicDoctor();
            Doctors = db.LoadDoctors();
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
              if (name == nameof(HealthInsuranceCardNo))
                {
                    if (!validate.IsDigitsOnly(HealthInsuranceCardNo)
                        || !companyValidation.IsUniqueHealthInsuranceCardNo(HealthInsuranceCardNo))
                    {
                        validationMessage = "Health Insurance Card No must be unique and must contains digits only!";
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
                || string.IsNullOrWhiteSpace(HealthInsuranceCardNo)
                || string.IsNullOrWhiteSpace(Doctor.Number)
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
                healthcareUserData.DateOfBirth = UserData.dateDateValue;
                healthcareUserData.Citizenship = UserData.Citizenship;
                healthcareUserData.Username = UserData.Username;
                healthcareUserData.Password = SecurePasswordHasher.Hash(UserData.Password);

                //adding new doctor to database 
                db.TryAddNewUserData(healthcareUserData);
                var userId = db.GetUserDataId(UserData.Username);
                if (userId != 0)
                {                    
                    patient.UserDataID = userId;
                    patient.HealthInsuranceCardNo = HealthInsuranceCardNo;
                    patient.ClinicDoctorID = Doctor.ClinicDoctorID;
                    patient.NumberOfDoctor = Doctor.Number;

                    IsAddedNewPatient = db.TryAddNewPatient(patient);
                    if (IsAddedNewPatient == false)
                    {
                        MessageBox.Show("Something went wrong. New patient is not created.");
                        db.TryRemoveUserData(userId);
                    }
                    else
                    {
                        Logger.Instance.LogCRUD($"[{DateTime.Now.ToString("dd.MM.yyyy hh: mm")}] Created new patient with ID Card Number : '{UserData.IDCardNo}'");
                        MessageBox.Show("The new patient is sucessfully created.");
                    }
                    registrationView.Close();
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
            MainWindow loginWindow = new MainWindow();     
            loginWindow.Show();
            IsAddedNewPatient = false;
            registrationView.Close();
        }
        #endregion
    }
}
