using Healthcare_App.ViewModel.Interfaces;
using System.Windows.Input;

namespace Healthcare_App.Command
{
    class LogoutCommand : ILogoutCommand
    {
        public ILogout Exit { get; set; }

        private ICommand logout;

        public LogoutCommand(ILogout exit)
        {
            Exit = exit;
        }

        public ICommand Logout
        {
            get
            {
                if (logout == null)
                {
                    logout = new RelayCommand(param => LogoutExecute(), param => CanLogoutExecute());
                }
                return logout;
            }
        }
     
        public bool CanLogoutExecute()
        {
            return Exit.CanLogoutExecute();
        }

        public void LogoutExecute()
        {
            Exit.LogoutExecute();
        }
    }
}
