﻿using DataValidations;
using Healthcare_App.Command;
using Healthcare_App.View.Doctor;
using Healthcare_App.View.Maintenance;
using Healthcare_App.View.Manager;
using Healthcare_App.View.Master;
using Healthcare_App.View.Patient;
using Healthcare_App.View.Registration;
using HealthcareData.Models;
using HealthcareData.Repositories;
using HealthcareData.Validations;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;

namespace Healthcare_App.ViewModel
{
	class MainWindowViewModel : ViewModelBase
	{
		#region Fields
		private string userName;
		public static readonly string clinicAccessPath = @"..\ClinicAccess.txt";

		readonly MainWindow loginView;
		#endregion

		#region Constructor
		internal MainWindowViewModel(MainWindow view)
		{
			this.loginView = view;
			MasterUserName = File.ReadAllLines(clinicAccessPath)[0];
			MasterPassword = File.ReadAllLines(clinicAccessPath)[1];
		}

		static MainWindowViewModel()
		{
			if (!File.Exists(clinicAccessPath))
			{
				File.WriteAllLines(clinicAccessPath, new string[]
				{
					"Klinika",
					"Klinika123*"
				});
			}
		}
		#endregion
		public string MasterUserName { get; private set; }
		public string MasterPassword { get; private set; }
		#region Properties
		public string UserName
		{
			get
			{
				return userName;
			}
			set
			{
				userName = value;
				OnPropertyChanged(nameof(UserName));
			}
		}
		#endregion
		//login
		private ICommand submitCommand;
		public ICommand SubmitCommand
		{
			get
			{
				if (submitCommand == null)
				{
					submitCommand = new RelayCommand(Submit);
					return submitCommand;
				}
				return submitCommand;
			}
		}

		void Submit(object obj)
		{
			string password = (obj as PasswordBox).Password;
			var validate = new Validations();
			var validateHealthcareData = new HealthcareValidations();
			var db = new HealtcareDBRepository();
			if (UserName == MasterUserName && password == MasterPassword)
			{
				MasterView masterView = new MasterView();
				loginView.Close();
				masterView.Show();
				return;
			}
			if (validateHealthcareData.IsCorrectUser(userName, password))
			{
				int userDataId = db.GetUserDataId(userName);
				if (userDataId != 0)
				{
					if (validateHealthcareData.GetUserType(userDataId) == nameof(tblClinicDoctor))
					{
						var doctor = db.LoadDoctorByUserDataId(userDataId);
						//DoctorView doctorView = new DoctorView(doctor);
						//loginView.Close();
						//doctorView.Show();
						return;
					}
					if (validateHealthcareData.GetUserType(userDataId) == nameof(tblClinicMaintenance))
					{
						var maintenance = db.LoadMaintenanceByUserDataId(userDataId);
						//MaintenanceView maintenanceView = new MaintenanceView(maintenance);
						//loginView.Close();
						//maintenanceView.Show();
						return;
					}
					if (validateHealthcareData.GetUserType(userDataId) == nameof(tblClinicManager))
					{
						var manager = db.LoadManagerByUserDataId(userDataId);
						//ManagerView managerView = new ManagerView(manager);
						//loginView.Close();
						//managerView.Show();
						return;
					}
					if (validateHealthcareData.GetUserType(userDataId) == nameof(tblClinicPatient))
					{
						var patient = db.LoadPatientByUserDataId(userDataId);
						//PatientView patientView = new PatientView(patient);
						//loginView.Close();
						//patientView.Show();
						return;
					}
				}
			}
		}

		//registrate
		private ICommand registrateCommand;
		public ICommand RegistrateCommand
		{
			get
			{
				if (registrateCommand == null)
				{
					registrateCommand = new RelayCommand(Registrate);
					return registrateCommand;
				}
				return registrateCommand;
			}
		}

		private void Registrate(object obj)
		{
			RegistrationView registrateView = new RegistrationView();
			loginView.Close();
			registrateView.Show();
			return;
		}
	}
}
