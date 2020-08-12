using Healthcare_App.Command;
using Healthcare_App.View;
using System.Windows.Input;

namespace Healthcare_App.ViewModel
{
    class ShouldDeleteViewModel
    {
        #region Fields
        private ShouldDeleteView deleteView;
        #endregion

        #region Properties
        public bool ShouldDelete { get; set; }
        #endregion

        #region Constructors
        public ShouldDeleteViewModel(ShouldDeleteView deleteView)
        {
            this.deleteView = deleteView;

        }
        #endregion

        #region Commands
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        private bool CanSaveExecute()
        {
            return true;
        }

        private void SaveExecute()
        {
            ShouldDelete = true;
            deleteView.Close();
        }

        private ICommand exit;
        public ICommand Exit
        {
            get
            {
                if (exit == null)
                {
                    exit = new RelayCommand(param => ExitExecute(), param => CanExitExecute());
                }
                return exit;
            }
        }

        private bool CanExitExecute()
        {
            return true;
        }

        private void ExitExecute()
        {
            ShouldDelete = false;
            deleteView.Close();
        }
        #endregion
    }
}
