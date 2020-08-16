using Healthcare_App.ViewModel.Administrator;
using System.Windows;
using System.Windows.Controls;

namespace Healthcare_App.View.Administrator
{
    /// <summary>
    /// Interaction logic for AdministratorView.xaml
    /// </summary>
    public partial class AdministratorView : Window
    {
        public AdministratorView()
        {
            InitializeComponent();
            this.DataContext = new AdministratorViewModel(this);
        }
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //hiding id columns
            if (e.Column.Header.ToString() == "ClinicMaintenanceID"
                 || e.Column.Header.ToString() == "UserDataID"
                 || e.Column.Header.ToString() == "tblHealthcareUserData"
                 || e.Column.Header.ToString() == "HealthcareInstitutionID"
                 || e.Column.Header.ToString() == "CompletionDate"
                 || e.Column.Header.ToString() == "ClinicManagerID")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}
