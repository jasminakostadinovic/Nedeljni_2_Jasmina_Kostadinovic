using Healthcare_App.ViewModel.Interfaces;
using System.Windows.Input;

namespace Healthcare_App.Command
{
    class LogoutCommand : ILogoutCommand
    {
        public IExit Exit { get; set; }

        private ICommand logout;

        public LogoutCommand(IExit exit)
        {
            Exit = exit;
        }

        public ICommand Logout
        {
            get
            {
                if (logout == null)
                {
                    logout = new RelayCommand(param => ExitExecute(), param => CanExitExecute());
                }
                return logout;
            }
        }
     
        public bool CanExitExecute()
        {
            return Exit.CanExitExecute();
        }

        public void ExitExecute()
        {
            Exit.ExitExecute();
        }
    }
}
