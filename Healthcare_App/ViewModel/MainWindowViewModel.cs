using DataValidations;
using Healthcare_App.Command;
using Healthcare_App.View;
using Healthcare_App.View.Administrator;
using Healthcare_App.View.Doctor;
using Healthcare_App.View.Maintenance;
using Healthcare_App.View.Manager;
using Healthcare_App.View.Master;
using Healthcare_App.View.Patient;
using Healthcare_App.View.Registration;
using Healthcare_App.ViewModel.Administrator;
using HealthcareData.Models;
using HealthcareData.Repositories;
using HealthcareData.Validations;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Healthcare_App.ViewModel
{
	class MainWindowViewModel : ViewModelBase
	{
		#region Fields
		private string userName;
		private static readonly string clinicAccessPath = @"..\ClinicAccess.txt";
		private string masterUserName;
		private string masterPassword;
		readonly MainWindow loginView;
		private readonly HealtcareDBRepository db = new HealtcareDBRepository();
		#endregion

		#region Constructor
		internal MainWindowViewModel(MainWindow view)
		{
			this.loginView = view;		
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

		#region Meethods

		private string ReadMasterUsername()
		{
			try
			{
				return File.ReadAllLines(clinicAccessPath)[0];
			}
			catch (Exception)
			{
				return "Klinika";
			}
		}
		private string ReadMasterPasword()
		{
			try
			{
				return File.ReadAllLines(clinicAccessPath)[1];
			}
			catch (Exception)
			{
				return "Klinika123*";
			}
		}
        #endregion
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
			masterUserName = ReadMasterUsername();
			masterPassword = ReadMasterPasword();
			if (UserName == masterUserName && password == masterPassword)
			{
				MasterView masterView = new MasterView();
				loginView.Close();
				masterView.Show();
				return;
			}
			else if (validateHealthcareData.IsCorrectUser(userName, password))
			{
				int userDataId = db.GetUserDataId(userName);
				if (userDataId != 0)
				{
					if (validateHealthcareData.GetUserType(userDataId) == nameof(tblClinicAdministrator))
					{
						if (!db.IsHealthcareInstitutionCreated())
						{
							var createInstitution = new AddNewHealthcareInstitutionView();
							createInstitution.Show();
							loginView.Close();
							if ((createInstitution.DataContext as AddNewHealthcareInstitutionViewModel).IsAddedNewInstitution == true)
							{
								AdministratorView administratorView = new AdministratorView();
								loginView.Close();
								administratorView.Show();
								return;
							}
						}
						else
						{
							AdministratorView administratorView = new AdministratorView();
							loginView.Close();
							administratorView.Show();
							return;
						}				
					}
					if (validateHealthcareData.GetUserType(userDataId) == nameof(tblClinicDoctor))
					{
						var doctor = db.LoadDoctorByUserDataId(userDataId);
						DoctorView doctorView = new DoctorView();
						loginView.Close();
						doctorView.Show();
						return;
					}
					if (validateHealthcareData.GetUserType(userDataId) == nameof(tblClinicMaintenance))
					{
						var maintenance = db.LoadMaintenanceByUserDataId(userDataId);
						MaintenanceView maintenanceView = new MaintenanceView(maintenance);
						loginView.Close();
						maintenanceView.Show();
						return;
					}
					if (validateHealthcareData.GetUserType(userDataId) == nameof(tblClinicManager))
					{
						var manager = db.LoadManagerByUserDataId(userDataId);
						ManagerView managerView = new ManagerView(manager);
						loginView.Close();
						managerView.Show();
						return;
					}
					if (validateHealthcareData.GetUserType(userDataId) == nameof(tblClinicPatient))
					{
						var patient = db.LoadPatientByUserDataId(userDataId);
						PatientView patientView = new PatientView();
						loginView.Close();
						patientView.Show();
						return;
					}
				}
			}
			else
			{
				WarningView warning = new WarningView(loginView);
				warning.Show("User name or password are not correct!");
				UserName = null;
				(obj as PasswordBox).Password = null;
				return;
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
					registrateCommand = new RelayCommand(Register);
					return registrateCommand;
				}
				return registrateCommand;
			}
		}

		private void Register(object obj)
		{
			if (!db.LoadDoctors().Any())
			{
				MessageBox.Show("You can not register as a patient at the moment. There is no available doctor. Please, try later."); 
				return;
			}
			RegistrationView registrateView = new RegistrationView();
			loginView.Close();
			registrateView.Show();
			return;
		}
	}
}
