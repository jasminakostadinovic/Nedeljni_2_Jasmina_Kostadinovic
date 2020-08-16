using Healthcare_App.ViewModel.Administrator;
using System.Windows;

namespace Healthcare_App.View.Administrator
{
    /// <summary>
    /// Interaction logic for AddNewManagerView.xaml
    /// </summary>
    public partial class AddNewManagerView : Window
    {
        public AddNewManagerView()
        {
            InitializeComponent();
            this.DataContext = new AddNewManagerViewModel(this);
        }
    }
}
