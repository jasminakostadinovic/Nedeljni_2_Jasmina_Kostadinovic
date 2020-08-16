using DataValidations;
using Healthcare_App.ViewModel.Interfaces;
using HealthcareData.Validations;
using System;
using System.Globalization;

namespace Healthcare_App.ViewModel
{
    class UserData : ViewModelBase, IValidatedUserData
    {
        #region Fields
        private string surname;
        private string givenName;
        private string idCardNo;
        private string sex;
        private string citizenship;
        private string dateOfBirth;
        internal DateTime dateDateValue;
        private string username;
        private string password;
        private string[] sexTypes = new string[] { "M", "F", "N", "X" };

        public UserData()
        {
            IDCardNo = string.Empty;
            Sex = string.Empty;
            Citizenship = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            GivenName = string.Empty;
            Surname = string.Empty;
            CanSave = true;
        }
        #endregion

        #region Properties
        public DateTime DateDateValue
        {
            get
            {
                return dateDateValue;
            }
            set
            {
                if (dateDateValue == value) return;
                dateDateValue = value;
            }
        }
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
                if (string.IsNullOrEmpty(validationMessage))
                    CanSave = true;

                return validationMessage;
            }
        }
        #endregion
    }
}
