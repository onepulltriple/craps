using CrapsTableWPF.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace CrapsTableWPF.Windows
{
    /// <summary>
    /// Interaction logic for AddPlayerDialog.xaml
    /// </summary>
    public partial class CreateOrUpdatePlayerDialog : Window
    {
        public CreateOrUpdatePlayerDialog()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is CreateOrUpdatePlayerDialogViewModel viewModel)
            {
                // subscribe to the viewmodel's RequestClose event
                viewModel.RequestClose += () => DialogResult = true;
            }
        }

        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(TB00);
        }

        private void QuitButtonClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

    }
}
