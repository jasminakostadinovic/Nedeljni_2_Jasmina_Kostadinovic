using System.Windows.Input;

namespace Healthcare_App.ViewModel.Interfaces
{
    interface ILogoutCommand : IExit
    {
        ICommand Logout { get; }
    }
}
