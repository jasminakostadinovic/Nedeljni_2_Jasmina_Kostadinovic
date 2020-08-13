using Healthcare_App.ViewModel.Administrator;
using System.Windows;

namespace Healthcare_App.View.Administrator
{
    /// <summary>
    /// Interaction logic for AddNewHealthcareInstitutionView.xaml
    /// </summary>
    public partial class AddNewHealthcareInstitutionView : Window
    {
        public AddNewHealthcareInstitutionView()
        {
            InitializeComponent();
            this.DataContext = new AddNewHealthcareInstitutionViewModel(this);
        }
    }
}
