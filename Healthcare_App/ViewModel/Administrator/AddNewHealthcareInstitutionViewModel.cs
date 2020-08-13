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
    class AddNewHealthcareInstitutionViewModel : ViewModelBase, IDataErrorInfo
    {

        #region Fields
        private readonly AddNewHealthcareInstitutionView addNewHealthcareInstitutionView;
        private tblHealthcareInstitution institution;
        private string name;
        private string owner;
        private string address;
        private string numberOfFloors;
        private int numberOfFloorsValue;
        private string completionDate;
        private string numberOfPersonsPerFloor;
        private int numberOfPersonsPerFloorValue;
        private DateTime completionDateValue;
        private bool? hasBalcony;
        private bool? hasBackyard;  
        public string numberOfAmbulanceAccess;
        public int numberOfAmbulanceAccessValue;
        public string numberOfDisabledPersonsAccess;
        public int numberOfDisabledPersonsAccessValue;
        private readonly HealtcareDBRepository db = new HealtcareDBRepository();
        #endregion
        #region Properties
        public bool? HasBackyard
        {
            get { return hasBackyard; }
            set
            {
                hasBackyard = value;
                hasBackyard = (hasBackyard != null) ? value : false;
                OnPropertyChanged(nameof(HasBalcony));
            }
        }
        public bool? HasBalcony
        {
            get { return hasBalcony; }
            set
            {
                hasBalcony = value;
                hasBalcony = (hasBalcony != null) ? value : false;
                OnPropertyChanged(nameof(HasBalcony));
            }
        }
        public string NumberOfDisabledPersonsAccess
        {
            get
            {
                return numberOfDisabledPersonsAccess;
            }
            set
            {
                if (numberOfDisabledPersonsAccess == value) return;
                numberOfDisabledPersonsAccess = value;
                OnPropertyChanged(nameof(NumberOfDisabledPersonsAccess));
            }
        }
        public string NumberOfAmbulanceAccess
        {
            get
            {
                return numberOfAmbulanceAccess;
            }
            set
            {
                if (numberOfAmbulanceAccess == value) return;
                numberOfAmbulanceAccess = value;
                OnPropertyChanged(nameof(NumberOfAmbulanceAccess));
            }
        }
        public bool IsAddedNewInstitution { get; internal set; }
        public bool CanSave { get; set; }
        public string NumberOfPersonsPerFloor
        {
            get
            {
                return numberOfPersonsPerFloor;
            }
            set
            {
                if (numberOfPersonsPerFloor == value) return;
                numberOfPersonsPerFloor = value;
                OnPropertyChanged(nameof(NumberOfPersonsPerFloor));
            }
        }
        public string CompletionDate
        {
            get
            {
                return completionDate;
            }
            set
            {
                if (completionDate == value) return;
                completionDate = value;
                OnPropertyChanged(nameof(CompletionDate));
            }
        }
        public string NumberOfFloors
        {
            get
            {
                return numberOfFloors;
            }
            set
            {
                if (numberOfFloors == value) return;
                numberOfFloors = value;
                OnPropertyChanged(nameof(NumberOfFloors));
            }
        }
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                if (address == value) return;
                address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name == value) return;
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Owner
        {
            get
            {
                return owner;
            }
            set
            {
                if (owner == value) return;
                owner = value;
                OnPropertyChanged(nameof(Owner));
            }
        }

        public tblHealthcareInstitution Institution
        {
            get
            {
                return institution;
            }
            set
            {
                institution = value;
                OnPropertyChanged(nameof(Institution));
            }
        }
        #endregion
        #region Constructors
        public AddNewHealthcareInstitutionViewModel(AddNewHealthcareInstitutionView addNewHealthcareInstitutionView)
        {
            this.addNewHealthcareInstitutionView = addNewHealthcareInstitutionView;
            Address = string.Empty;
            NumberOfFloors = string.Empty;
            CompletionDate = string.Empty;
            Owner = string.Empty;
            Name = string.Empty;
            NumberOfDisabledPersonsAccess = string.Empty;
            NumberOfAmbulanceAccess = string.Empty;
            Institution = new tblHealthcareInstitution();
            CanSave = true;
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
                 if (name == nameof(CompletionDate))
                {
                    var culture = CultureInfo.InvariantCulture;
                    var styles = DateTimeStyles.None;
                    if (!DateTime.TryParse(CompletionDate, culture, styles, out completionDateValue) || completionDateValue.Year < 1800)
                    {
                        validationMessage = "Invalid date format! use: [4/10/2008]";
                        CanSave = false;
                    }
                }
                 else if(name == nameof(NumberOfAmbulanceAccess))
                {
                    if(!int.TryParse(NumberOfAmbulanceAccess, out numberOfAmbulanceAccessValue) || numberOfAmbulanceAccessValue < 0)
                    {
                        validationMessage = "Invalid format!";
                        CanSave = false;
                    }
                }
                else if (name == nameof(NumberOfDisabledPersonsAccess))
                {
                    if (!int.TryParse(NumberOfDisabledPersonsAccess, out numberOfDisabledPersonsAccessValue) || numberOfDisabledPersonsAccessValue < 0)
                    {
                        validationMessage = "Invalid format!";
                        CanSave = false;
                    }
                }
                else if (name == nameof(NumberOfPersonsPerFloor))
                {
                    if (!int.TryParse(NumberOfPersonsPerFloor, out numberOfPersonsPerFloorValue) || numberOfPersonsPerFloorValue < 0)
                    {
                        validationMessage = "Invalid format!";
                        CanSave = false;
                    }
                }
                else if (name == nameof(NumberOfFloors))
                {
                    if (!int.TryParse(NumberOfFloors, out numberOfFloorsValue) || numberOfFloorsValue < 0)
                    {
                        validationMessage = "Invalid format!";
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
                string.IsNullOrWhiteSpace(Owner)
                || string.IsNullOrWhiteSpace(Name)
                || string.IsNullOrWhiteSpace(NumberOfFloors)
                || string.IsNullOrWhiteSpace(CompletionDate)
                || string.IsNullOrWhiteSpace(NumberOfPersonsPerFloor)
                || string.IsNullOrWhiteSpace(Address)
                || string.IsNullOrWhiteSpace(NumberOfDisabledPersonsAccess)
                || string.IsNullOrWhiteSpace(NumberOfAmbulanceAccess)
                || CanSave == false)
                return false;
            return true;
        }
        private void SaveExecute()
        {
            try
            {
                institution.Owner = Owner;
                institution.Name = Name;
                institution.Address = Address;
                institution.NumberOfFloors = numberOfFloorsValue;
                institution.CompletionDate = completionDateValue;
                institution.NumberOfAmbulanceAccess = numberOfAmbulanceAccessValue;
                institution.NumberOfDisabledPersonsAccess = numberOfDisabledPersonsAccessValue;
                institution.NumberOfPersonsPerFloor = numberOfPersonsPerFloorValue;
                if (HasBackyard.HasValue)
                    institution.HasBackyard = (bool)HasBalcony;
                else
                    institution.HasBackyard = false;
                if (HasBalcony.HasValue)
                    institution.HasBalcony = (bool)HasBalcony;
                else
                    institution.HasBalcony = false;

                //adding new institution to database 
                IsAddedNewInstitution = db.TryAddNewInstitution(institution);
                if (!IsAddedNewInstitution)
                {
                    MessageBox.Show("Something went wrong. New healthcare institution is not created.");
                    Logger.Instance.LogCRUD($"[{DateTime.Now.ToString("dd.MM.yyyy hh: mm")}] Fail to create the new healthcare institution.");
                }

                else
                {
                    Logger.Instance.LogCRUD($"[{DateTime.Now.ToString("dd.MM.yyyy hh: mm")}] Created new healthcare institution with name : '{Name}'");
                    MessageBox.Show("The new healthcare institution is sucessfully created.");
                }
                AdministratorView administratorView = new AdministratorView();
                addNewHealthcareInstitutionView.Close();
                administratorView.Show();
                return;

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
            IsAddedNewInstitution = false;

            addNewHealthcareInstitutionView.Close();
        }
        #endregion
    }
}
