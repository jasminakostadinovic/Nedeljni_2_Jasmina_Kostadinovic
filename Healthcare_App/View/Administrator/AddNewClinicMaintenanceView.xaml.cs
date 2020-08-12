using Healthcare_App.ViewModel.Administrator;
using System.Windows;

namespace Healthcare_App.View.Administrator
{
    /// <summary>
    /// Interaction logic for AddNewClinicMaintenanceView.xaml
    /// </summary>
    public partial class AddNewClinicMaintenanceView : Window
    {
        public AddNewClinicMaintenanceView()
        {
            InitializeComponent();
            this.DataContext = new AddNewClinicMaintenanceViewModel(this);
        }
    }
}
