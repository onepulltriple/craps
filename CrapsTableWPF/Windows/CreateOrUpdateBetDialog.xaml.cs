using CrapsLibrary;
using CrapsTableWPF.ViewModels;
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
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is CreateOrUpdateBetDialogViewModel viewModel)
            {
                // subscribe to the viewmodel's RequestClose event
                viewModel.RequestClose += () => DialogResult = true;
            }
        }

        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(TB01);
        }


        private void QuitButtonClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

    }
}
