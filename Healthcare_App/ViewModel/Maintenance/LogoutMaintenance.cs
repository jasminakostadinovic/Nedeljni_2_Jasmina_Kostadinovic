using Healthcare_App.View.Maintenance;
using Healthcare_App.ViewModel.Interfaces;

namespace Healthcare_App.ViewModel.Maintenance
{
    class LogoutMaintenance : ILogout
    {
        private readonly MaintenanceView maintenanceView;
        public LogoutMaintenance(MaintenanceView maintenanceView)
        {
            this.maintenanceView = maintenanceView;
        }
        public bool CanLogoutExecute()
        {
            return true;
        }
        public void LogoutExecute()
        {
            MainWindow loginWindow = new MainWindow();
            maintenanceView.Close();
            loginWindow.Show();
        }
    }
}
