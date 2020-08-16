using Healthcare_App.View.Administrator;
using Healthcare_App.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare_App.ViewModel.Administrator
{
    class LogoutAdministrator : ILogout
    {
        readonly AdministratorView adminView;
        public LogoutAdministrator(AdministratorView adminView)
        {
            this.adminView = adminView;
        }

        public bool CanLogoutExecute()
        {
            return true;
        }

        public void LogoutExecute()
        {
            MainWindow loginWindow = new MainWindow();
            adminView.Close();
            loginWindow.Show();
        }
    }
}
