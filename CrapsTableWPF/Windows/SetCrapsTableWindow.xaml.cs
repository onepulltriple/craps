using CrapsLibrary;
using CrapsTableWPF.Services;
using System.Windows;
using System.Windows.Input;

namespace CrapsTableWPF.Windows
{
    /// <summary>
    /// Interaction logic for SetCrapsTableWindow.xaml
    /// </summary>
    public partial class SetCrapsTableWindow : Window
    {
        public SetCrapsTableWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(TB00);
        }

        private void TB00KeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProceedButtonClicked(sender, e);
            } 
        }

        private void QuitButtonClicked(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ProceedButtonClicked(object sender, RoutedEventArgs e)
        {
            Result<uint> resultOfCheck = CrapsTable.ValidateCrapsTableMinimum(TB00.Text);

            if (!resultOfCheck.Success)
            {
                UIErrorMessage.Text = resultOfCheck.Messages[0];
                UIErrorMessage.Visibility = Visibility.Visible;
                return;
            }

            // start the service and create the craps table
            var service = new CrapsTableService();
            service.CreateTable(resultOfCheck.Value);

            MainWindow main = new(service);
            main.Show();
            this.Close();
        }
    }
}
