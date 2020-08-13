using Healthcare_App.ViewModel.Maintenance;
using HealthcareData.Models;
using System.Windows;

namespace Healthcare_App.View.Maintenance
{
    /// <summary>
    /// Interaction logic for MaintenanceView.xaml
    /// </summary>
    public partial class MaintenanceView : Window
    {
        public MaintenanceView(tblClinicMaintenance clinicMaintenance)
        {
            InitializeComponent();
            this.DataContext = new MaintenanceViewModel(this, clinicMaintenance);
        }
    }
}
