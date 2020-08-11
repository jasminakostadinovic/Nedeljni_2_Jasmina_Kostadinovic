using Healthcare_App.ViewModel.Master;
using System.Windows;

namespace Healthcare_App.View.Master
{
    /// <summary>
    /// Interaction logic for MasterView.xaml
    /// </summary>
    public partial class MasterView : Window
    {
        public MasterView()
        {
            InitializeComponent();
            this.DataContext = new MasterViewModel(this);
        }
    }
}
