using Healthcare_App.ViewModel.Interfaces;

namespace Healthcare_App.View.Master
{
    class ExitMaster : IExit
    {
        readonly MasterView masterView;
        public ExitMaster(MasterView masterView)
        {
            this.masterView = masterView;
        }

        public bool CanExitExecute()
        {
            return true;
        }

        public void ExitExecute()
        {
            MainWindow loginWindow = new MainWindow();
            masterView.Close();
            loginWindow.Show();
        }
    }
}
