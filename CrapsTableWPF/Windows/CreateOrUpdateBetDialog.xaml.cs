using CrapsLibrary;
using System.Windows;
using System.Windows.Input;

namespace CrapsTableWPF.Windows
{
    /// <summary>
    /// Interaction logic for CreateOrUpdateBetDialog.xaml
    /// </summary>
    public partial class CreateOrUpdateBetDialog : Window
    {
        public CreateOrUpdateBetDialog()
        {
            InitializeComponent();
        }

        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(TB01);
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
            // validate player construction parameters
            Result<uint> resultOfCheckingCommitment = CrapsTable.ValidateUserInputUInt(TB01.Text);

            // display data validation error messages
            if (!resultOfCheckingCommitment.Success)
            {
                UIErrorMessage.Text = resultOfCheckingCommitment.Messages[0];
                UIErrorMessage.Visibility = Visibility.Visible;
                return;
            }

            DialogResult = true;
        }
    }
}
