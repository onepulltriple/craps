using CrapsLibrary;
using System.Windows;
using System.Windows.Input;

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

        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(TB00);
        }

        private void Ok_Clicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void TB00KeyDownHandler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OKButtonClicked(sender, e);
            }
        }

        private void TB01KeyDownHandler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OKButtonClicked(sender, e);
            }
        }

        private void QuitButtonClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void OKButtonClicked(object sender, RoutedEventArgs e)
        {
            Result<string> resultOfCheckingName  = CrapsTable.ValidateUserInputPlayerName(TB00.Text);
            Result<uint> resultOfCheckingPurse   = CrapsTable.ValidateUserInputUInt(TB01.Text);
            
            if (!resultOfCheckingName.Success)
            {
                UIErrorMessage.Text = resultOfCheckingName.Messages[0];
                UIErrorMessage.Visibility = Visibility.Visible;
                return;
            }

            if (!resultOfCheckingPurse.Success)
            {
                UIErrorMessage.Text = resultOfCheckingPurse.Messages[0];
                UIErrorMessage.Visibility = Visibility.Visible;
                return;
            }

            DialogResult = true;
            // TODO announce new player to the game feed
        }
    }
}
