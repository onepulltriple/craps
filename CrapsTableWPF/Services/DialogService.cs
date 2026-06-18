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

        public BetVO? CreateOrUpdateBetDialog(Bet? bet)
        {
            var betDialogViewModel = new CreateOrUpdateBetDialogViewModel();

            if (bet != null)
            {
                betDialogViewModel.Name = $"Edit {bet.betOwner.Name}'s {bet.Name}";
                betDialogViewModel.Commitment = bet.Commitment;
            }

            // set the data context for the dialog
            var editBetDialog = new CreateOrUpdateBetDialog
            {
                DataContext = betDialogViewModel
            };

            bool? result = editBetDialog.ShowDialog();

            if (result != true) 
                return null;

            return new BetVO
            {
                Name = betDialogViewModel.Name,
                Commitment = betDialogViewModel.Commitment
            };
        }
    }
}
