using CrapsTableWPF.Data_Transfer_Objects;
using CrapsTableWPF.ViewModels;
using CrapsTableWPF.Windows;
using CrapsLibrary;

namespace CrapsTableWPF.Services
{
    // responsible for showing popup windows
    public class DialogService
    {

        public PlayerDTO? CreateOrUpdatePlayerDialog(Player? player)
        {
            var playerDialogViewModel = new CreateOrUpdatePlayerDialogViewModel();

            if (player != null)
            {
                playerDialogViewModel.Name = player.name;
                playerDialogViewModel.Purse = player.purse;
            }

            var editPlayerDialog = new CreateOrUpdatePlayerDialog
            {
                DataContext = playerDialogViewModel
            };

            bool? result = editPlayerDialog.ShowDialog();

            if (result != true)
                return null;

            return new PlayerDTO
            {
                Name = playerDialogViewModel.Name,
                Purse = playerDialogViewModel.Purse
            };
        }
    }
}
