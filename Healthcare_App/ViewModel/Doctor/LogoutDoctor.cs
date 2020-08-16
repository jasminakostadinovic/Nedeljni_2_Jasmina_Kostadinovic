using Healthcare_App.View.Doctor;
using Healthcare_App.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare_App.ViewModel.Doctor
{
    class LogoutDoctor : ILogout
    {
        readonly DoctorView doctorView;
        public LogoutDoctor(DoctorView doctorView)
        {
            this.doctorView = doctorView;
        }

        public bool CanLogoutExecute()
        {
            return true;
        }

        public void LogoutExecute()
        {
            MainWindow loginWindow = new MainWindow();
            doctorView.Close();
            loginWindow.Show();
        }
    }
}
