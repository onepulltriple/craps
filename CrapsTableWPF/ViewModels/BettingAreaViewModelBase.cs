using CrapsLibrary;
using CrapsLibrary.Bets;
using CrapsTableWPF.Infrastructure;
using CrapsTableWPF.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CrapsTableWPF.ViewModels
{
    public class BettingAreaViewModelBase : ViewModelBase
    {
        protected readonly CrapsTable crapsTable;

        protected readonly DialogService dialogService;

        protected readonly betType betType;

        public ObservableCollection<BetSlotViewModel?> BetSlotViewModels { get; }

        public ICommand CreateOrUpdateBetCommand { get; }

        public BettingAreaViewModelBase(CrapsTable crapsTable, DialogService dialogService, betType betType)
        {
            this.crapsTable = crapsTable;
            this.dialogService = dialogService;
            this.betType = betType;
            this.BetSlotViewModels = new(crapsTable.Slots.Select(x => (BetSlotViewModel?)null));

            this.CreateOrUpdateBetCommand = new RelayCommand(_ => CreateOrUpdateBet());
        }

        public void CreateOrUpdateBet() 
        {
            // determine which player is trying to bet
            Result<Player> currentPlayer = crapsTable.GetCurrentPlayer();

            // error message if current player retrieval fails
            if (!currentPlayer.Success)
            {
                crapsTable.gameEventFeed.AddMultiLine(currentPlayer);
                return;
            }

            // determine if the player is editing an existing bet
            Bet? existingBet = currentPlayer.Value.PlayerBetList.FirstOrDefault(
                bet => bet.betType == this.betType
                );

            // should no existing bet be found, check if the player is allowed to place a new bet of this type
            if (existingBet == null)
            {
                Result<bool> isBetAllowed = BetFactory.CheckIfCreateBetAllowed(this.crapsTable, currentPlayer.Value, this.betType);

                // error message if the bet is not allowed
                if (!isBetAllowed.Success)
                {
                    crapsTable.gameEventFeed.AddMultiLine(isBetAllowed);
                    return;
                }
            }

            // call dialog service
            var tempBet = dialogService.CreateOrUpdateBetDialog(this.crapsTable, currentPlayer.Value, this.betType, existingBet);

            // null check
            if (tempBet == null)
                return;

            // populate the view
            this.PopulateBetSlot(currentPlayer.Value, tempBet);
        }

        public void PopulateBetSlot(Player slotOwner, Bet bet)
        {
            // determine slot index of the betting player
            int slotIndex = Array.IndexOf(crapsTable.Slots, slotOwner);

            // populate bet slot view
            BetSlotViewModels[slotIndex] = new BetSlotViewModel(slotOwner, slotIndex, this.betType, bet);
        }
    }
}
