using Healthcare_App.View.Patient;
using Healthcare_App.ViewModel.Interfaces;

namespace Healthcare_App.ViewModel.Patient
{
    class LogoutPatient : ILogout
    {
        readonly PatientView patientView;
        public LogoutPatient(PatientView patientView)
        {
            this.patientView = patientView;
        }

        public bool CanLogoutExecute()
        {
            return true;
        }

        public void LogoutExecute()
        {
            MainWindow loginWindow = new MainWindow();
            patientView.Close();
            loginWindow.Show();
        }
    }
}
