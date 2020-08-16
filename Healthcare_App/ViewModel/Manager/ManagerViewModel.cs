using Healthcare_App.Command;
using Healthcare_App.View.Manager;
using Healthcare_App.ViewModel.Interfaces;
using HealthcareData.Models;
using HealthcareData.Repositories;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Healthcare_App.ViewModel.Manager
{
    class ManagerViewModel : ViewModelBase
    {
        #region Fields
        readonly ManagerView managerView;
        private readonly HealtcareDBRepository db = new HealtcareDBRepository();
        private List<tblClinicDoctor> doctors;
        private tblClinicDoctor doctor;
        private int managerId;
        private int doctorCount;
        private int maxDoctorsCount;
        private int omissionCount;
        #endregion

        #region Constructor
        internal ManagerViewModel(ManagerView managerView, tblClinicManager manager)
        {
            this.managerView = managerView;
            Doctor = new tblClinicDoctor();
            Doctors = LoadDoctors();
            managerId = manager.ClinicManagerID;
            maxDoctorsCount = manager.MaxCountOfDoctors;
            omissionCount = manager.OmissionsCount;
            LogoutManager = new LogoutCommand(new LogoutManager(managerView));
        }     
        #endregion

        #region Properies
        public ILogoutCommand LogoutManager { get; set; }
        private List<tblClinicDoctor> LoadDoctors()
        {
            return db.LoadDoctors();
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
        #endregion
        #region Commands

        //adding new Clinic Maintenance

        private ICommand addNewDoctor;
        public ICommand AddNewDoctor
        {
            get
            {
                if (addNewDoctor == null)
                {
                    addNewDoctor = new RelayCommand(param => AddNewDoctorExecute(), param => CanAddNewDoctor());
                }
                return addNewDoctor;
            }
        }

        private void AddNewDoctorExecute()
        {
            try
            {
                AddNewDoctorView addNewDoctorView = new AddNewDoctorView(managerId);
                addNewDoctorView.ShowDialog();
                if ((addNewDoctorView.DataContext as AddNewDoctorViewModel).IsAddedNewDoctor == true)
                {
                    Doctors = LoadDoctors();
                    doctorCount++;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddNewDoctor()
        {
            if (maxDoctorsCount - doctorCount == 0 
                || omissionCount > 5)
                return false;
            return true;
        }
        #endregion
    }
}
