namespace Healthcare_App.ViewModel.Interfaces
{
    interface ILogout
    {
        bool CanLogoutExecute();
        void LogoutExecute();     
    }
}
