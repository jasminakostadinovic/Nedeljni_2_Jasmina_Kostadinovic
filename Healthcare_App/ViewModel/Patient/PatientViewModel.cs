using Healthcare_App.Command;
using Healthcare_App.View.Patient;
using Healthcare_App.ViewModel.Interfaces;

namespace Healthcare_App.ViewModel.Patient
{
    class PatientViewModel : ViewModelBase
    {
        #region Fields
        readonly PatientView patientView;
        #endregion

        #region Constructor
        internal PatientViewModel(PatientView patientView)
        {
            this.patientView = patientView;
            LogoutPatient = new LogoutCommand(new LogoutPatient(patientView));
        }
        #endregion
        #region Properties
        public ILogoutCommand LogoutPatient { get; set; }
        #endregion       
    }
}
