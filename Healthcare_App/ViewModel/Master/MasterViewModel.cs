﻿using Healthcare_App.Command;
using Healthcare_App.View.Master;
using Healthcare_App.ViewModel.Interfaces;
using HealthcareData.Repositories;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Healthcare_App.ViewModel.Master
{
    class MasterViewModel : ViewModelBase
    {
        #region Fields
        readonly MasterView masterView;
        private readonly HealtcareDBRepository db = new HealtcareDBRepository();
        #endregion

        #region Properties
        public ILogoutCommand LogoutMaster { get; set; }
        #endregion

        #region Constructor
        internal MasterViewModel(MasterView masterView)
        {
            this.masterView = masterView;
            LogoutMaster = new LogoutCommand(new LogoutMaster(masterView));
        }
        #endregion

        #region Commands

        //adding new employee

        private ICommand addNewClinicAdministrator;
        public ICommand AddNewClinicAdministrator
        {
            get
            {
                if (addNewClinicAdministrator == null)
                {
                    addNewClinicAdministrator = new RelayCommand(param => AddNewClinicAdministratorExecute(), param => CanAddNewClinicAdministrator());
                }
                return addNewClinicAdministrator;
            }
        }

        private void AddNewClinicAdministratorExecute()
        {
            try
            {
                AddNewClinicAdministratorView addNewClinicAdministratorViewView = new AddNewClinicAdministratorView();
                addNewClinicAdministratorViewView.ShowDialog();
                masterView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddNewClinicAdministrator()
        {
            if (db.LoadAdministrators().Any())
                return false;
            return true;
        }
        #endregion
    }
}
