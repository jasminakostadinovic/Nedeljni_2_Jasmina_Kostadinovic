using Healthcare_App.ViewModel.Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
                 || e.Column.Header.ToString() == "UserDataID")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}
