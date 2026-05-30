using System.Windows;

namespace CrapsTableWPF.Windows
{
    /// <summary>
    /// Interaction logic for AddPlayerDialog.xaml
    /// </summary>
    public partial class AddPlayerDialog : Window
    {
        public AddPlayerDialog()
        {
            InitializeComponent();
        }

        private void Ok_Clicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
