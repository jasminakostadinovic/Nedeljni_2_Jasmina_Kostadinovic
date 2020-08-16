using System.Windows.Input;

namespace Healthcare_App.ViewModel.Interfaces
{
    interface ILogoutCommand : ILogout
    {
        ICommand Logout { get; }
    }
}
