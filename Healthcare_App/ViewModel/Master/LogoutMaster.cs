using Healthcare_App.View.Master;
using Healthcare_App.ViewModel.Interfaces;

namespace Healthcare_App.ViewModel.Master
{
    class LogoutMaster : ILogout
    {
        readonly MasterView masterView;
        public LogoutMaster(MasterView masterView)
        {
            this.masterView = masterView;
        }

        public bool CanLogoutExecute()
        {
            return true;
        }

        public void LogoutExecute()
        {
            MainWindow loginWindow = new MainWindow();
            masterView.Close();
            loginWindow.Show();
        }
    }
}
