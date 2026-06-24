using CrapsTableWPF.ValueObjects;
using CrapsTableWPF.ViewModels;
using CrapsTableWPF.Windows;
using CrapsLibrary;
using CrapsLibrary.Bets;

namespace CrapsTableWPF.Services
{
    // responsible for showing pop-up windows
    public class DialogService
    {
        public PlayerVO? CreateOrUpdatePlayerDialog(Player? player)
        {
            var playerDialogViewModel = new CreateOrUpdatePlayerDialogViewModel();

            if (player != null)
            {
                playerDialogViewModel.Name = player.Name;
                playerDialogViewModel.Purse = player.Purse.ToString();
            }

            // set the data context for the dialog
            var editPlayerDialog = new CreateOrUpdatePlayerDialog
            {
                DataContext = playerDialogViewModel
            };

            bool? result = editPlayerDialog.ShowDialog();

            if (result != true)
                return null;

            return new PlayerVO
            {
                Name = playerDialogViewModel.Name!,
                Purse = uint.Parse(playerDialogViewModel.Purse!)
            };
        }

        public void CreateOrUpdateBetDialog(CrapsTable crapsTable, Player currentPlayer, betType betType, Bet? bet)
        {
            var betDialogViewModel = new CreateOrUpdateBetDialogViewModel(crapsTable, currentPlayer, betType, bet);

            // set the data context for the dialog
            var editBetDialog = new CreateOrUpdateBetDialog
            {
                DataContext = betDialogViewModel
            };

            bool? result = editBetDialog.ShowDialog();
        }
    }
}
