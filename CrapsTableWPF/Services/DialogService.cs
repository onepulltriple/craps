using CrapsTableWPF.Data_Transfer_Objects;
using CrapsTableWPF.ViewModels;
using CrapsTableWPF.Windows;

namespace CrapsTableWPF.Services
{
    // responsible for showing popup windows
    public class DialogService
    {
        public NewPlayerData? ShowAddPlayerDialog()
        {
            var viewModel = new AddPlayerDialogViewModel();

            var dialog = new AddPlayerDialog
            {
                DataContext = viewModel
            };

            bool? result = dialog.ShowDialog();

            if (result != true)
                return null;

            // TODO validate this data!

            return new NewPlayerData
            {
                Name = viewModel.Name,
                Purse = viewModel.Purse
            };
        }
    }
}
