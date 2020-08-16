using Healthcare_App.Command;
using Healthcare_App.View.Doctor;
using Healthcare_App.ViewModel.Interfaces;

namespace Healthcare_App.ViewModel.Doctor
{
    class DoctorViewModel
    {
        #region Fields
        readonly DoctorView doctorView;
        #endregion

        #region Constructor
        internal DoctorViewModel(DoctorView doctorView)
        {
            this.doctorView = doctorView;
            LogoutDoctor = new LogoutCommand(new LogoutDoctor(doctorView));
        }
        #endregion
        #region Properties
        public ILogoutCommand LogoutDoctor { get; set; }
        #endregion   
    }
}
