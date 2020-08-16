using Healthcare_App.View.Manager;
using Healthcare_App.ViewModel.Interfaces;

namespace Healthcare_App.ViewModel.Manager
{
    class LogoutManager : ILogout
    {
        readonly ManagerView managerView;
        public LogoutManager(ManagerView managerView)
        {
            this.managerView = managerView;
        }

        public bool CanLogoutExecute()
        {
            return true;
        }

        public void LogoutExecute()
        {
            MainWindow loginWindow = new MainWindow();
            managerView.Close();
            loginWindow.Show();
        }
    }
}
