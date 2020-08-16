using Healthcare_App.ViewModel.Patient;
using System.Windows;

namespace Healthcare_App.View.Patient
{
    /// <summary>
    /// Interaction logic for PatientView.xaml
    /// </summary>
    public partial class PatientView : Window
    {
        public PatientView()
        {
            InitializeComponent();
            this.DataContext = new PatientViewModel(this);
        }
    }
}
