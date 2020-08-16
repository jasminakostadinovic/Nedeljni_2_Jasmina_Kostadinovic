using Healthcare_App.View.Administrator;
using Healthcare_App.ViewModel.Interfaces;

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
