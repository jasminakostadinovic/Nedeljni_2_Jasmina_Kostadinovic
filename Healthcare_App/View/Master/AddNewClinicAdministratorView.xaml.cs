using Healthcare_App.ViewModel.Master;
using System.Windows;

namespace Healthcare_App.View.Master
{
    /// <summary>
    /// Interaction logic for AddNewClinicAdministratorView.xaml
    /// </summary>
    public partial class AddNewClinicAdministratorView : Window
    {
        public AddNewClinicAdministratorView()
        {
            InitializeComponent();
            this.DataContext = new AddNewClinicAdministratorViewModel(this);
        }
    }
}
