using Healthcare_App.Command;
using Healthcare_App.View.Maintenance;
using HealthcareData.Models;
using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Healthcare_App.ViewModel.Maintenance
{
    class MaintenanceViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Fields
        private readonly MaintenanceView maintenanceView;
        private string project;
        private string hours;
        private tblClinicMaintenance maintenance;
        private int maintenanceId;
        private string maintenancePath;
        #endregion
        #region Properties

        public bool CanSave { get; set; }
        public string Project
        {
            get
            {
                return project;
            }
            set
            {
                if (project == value) return;
                project = value;
                OnPropertyChanged(nameof(Project));
            }
        }

        public string Hours
        {
            get
            {
                return hours;
            }
            set
            {
                hours = value;
                OnPropertyChanged(nameof(Hours));
            }
        }
        #endregion

        #region Constructors
        public MaintenanceViewModel(MaintenanceView maintenanceView, tblClinicMaintenance maintenance)
        {
            this.maintenanceView = maintenanceView;
            Project = string.Empty;
            this.maintenance = maintenance;
            maintenanceId = maintenance.ClinicMaintenanceID;
            maintenancePath = GeneratePath();
        }

        private string GeneratePath()
        {
            var sb = new StringBuilder();
            sb.Append(@"..\ClinicMaintenanceData");
            sb.Append($"{maintenanceId}");
            sb.Append(".txt");
            return sb.ToString();
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

                string validationMessage = string.Empty;

                if (name == "Hours")
                {
                    if (!int.TryParse(Hours, out int result) || result > 12 || result <= 0)
                    {
                        validationMessage = "Number of hours must be between 0 - 12h!";
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
                string.IsNullOrWhiteSpace(Hours)
                || string.IsNullOrWhiteSpace(Project)
                || CanSave == false)
                return false;
            return true;
        }
        private void SaveExecute()
        {
            try
            {
                var sb = new StringBuilder();

                sb.Append($"Employee ID: {maintenanceId}\n");
                sb.Append($"Date: {DateTime.Now.ToShortDateString()}\n");
                sb.Append($"Hours: {Hours} h\n");
                sb.Append($"Description: {Project}\n");
                sb.Append("--------------");

                using (StreamWriter sw = File.AppendText(maintenancePath))
                {
                    sw.WriteLine(sb);
                }
                MainWindow loginWindow = new MainWindow();
                MessageBox.Show("You have successfully created new daily realisation.");
                maintenanceView.Close();
                loginWindow.Show();
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
            maintenanceView.Close();
            loginWindow.Show();

        }

        #endregion
    }
}
