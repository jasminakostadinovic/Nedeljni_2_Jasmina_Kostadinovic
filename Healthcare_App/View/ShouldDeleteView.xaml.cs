using Healthcare_App.ViewModel;
using System.Windows;

namespace Healthcare_App.View
{
    /// <summary>
    /// Interaction logic for ShouldDeleteView.xaml
    /// </summary>
    public partial class ShouldDeleteView : Window
    {
        public ShouldDeleteView()
        {
            InitializeComponent();
            DataContext = new ShouldDeleteViewModel(this);
        }
        public void Show(string message)
        {
            lblText.Content = message;
            this.Show();
        }
    }
}
