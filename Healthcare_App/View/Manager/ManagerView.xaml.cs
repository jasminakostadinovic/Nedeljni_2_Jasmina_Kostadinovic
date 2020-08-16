using Healthcare_App.ViewModel.Manager;
using HealthcareData.Models;
using System.Windows;
using System.Windows.Controls;

namespace Healthcare_App.View.Manager
{
    /// <summary>
    /// Interaction logic for ManagerView.xaml
    /// </summary>
    public partial class ManagerView : Window
    {
        public ManagerView(tblClinicManager manager)
        {
            InitializeComponent();
            this.DataContext = new ManagerViewModel(this, manager);
        }
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //hiding id columns
            if (e.Column.Header.ToString() == "ClinicDoctorID"
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
