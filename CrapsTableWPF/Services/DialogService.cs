using CrapsTableWPF.Data_Transfer_Objects;
using CrapsTableWPF.ViewModels;
using CrapsTableWPF.Windows;
using CrapsLibrary;

namespace CrapsTableWPF.Services
{
    // responsible for showing popup windows
    public class DialogService
    {
        
        public NewPlayerDTO? ShowAddPlayerDialog()
        {
            var addPlayerDialogViewModel = new AddPlayerDialogViewModel();

            var addPlayerDialog = new AddPlayerDialog
            {
                DataContext = addPlayerDialogViewModel
            };

            bool? result = addPlayerDialog.ShowDialog();

            if (result != true)
                return null;

            return new NewPlayerDTO
            {
                Name = addPlayerDialogViewModel.Name,
                Purse = addPlayerDialogViewModel.Purse
            };
        }

        public NewPlayerDTO? LoadPlayerDialog(Player player)
        {
            var playerDialogViewModel = new AddPlayerDialogViewModel();

            playerDialogViewModel.Name = player.name;
            playerDialogViewModel.Purse = player.purse;

            var editPlayerDialog = new AddPlayerDialog
            {
                DataContext = playerDialogViewModel
            };

            bool? result = editPlayerDialog.ShowDialog();

            if (result != true)
                return null;

            return new NewPlayerDTO
            {
                Name = playerDialogViewModel.Name,
                Purse = playerDialogViewModel.Purse
            };
        }
    }
}
