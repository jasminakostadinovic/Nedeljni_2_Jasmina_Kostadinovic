using Healthcare_App.ViewModel.Doctor;
using System.Windows;

namespace Healthcare_App.View.Doctor
{
    /// <summary>
    /// Interaction logic for DoctorView.xaml
    /// </summary>
    public partial class DoctorView : Window
    {
        public DoctorView()
        {
            InitializeComponent();
            this.DataContext = new DoctorViewModel(this);
        }
    }
}
