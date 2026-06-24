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
            this.BetSlotViewModels = new(crapsTable.Slots.Select(_ => (BetSlotViewModel?)null));

            this.CreateOrUpdateBetCommand = new RelayCommand(_ => CreateOrUpdateBet());
            // TODO block button click if player cannot place bet of this type, then remove CheckIfCreateBetAllowed from CreateOrUpdateBet()
            // because the check is performed once again in BetFactory.CreateOrUpdateBet()
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

            // dialog updates model
            dialogService.CreateOrUpdateBetDialog(this.crapsTable, currentPlayer.Value, this.betType, existingBet);

            // caller refreshes from model
            this.RefreshBetSlot(currentPlayer.Value);
        }

        public void RefreshBetSlot(Player slotOwner, Bet? bet = null)
        {
            // determine slot index of the betting player
            int slotIndex = Array.IndexOf(crapsTable.Slots, slotOwner);
            
            // find out if there is a bet to display
            bet ??= slotOwner.PlayerBetList.FirstOrDefault(b => b.betType == this.betType);

            // if there is a bet to display, construct a BetSlotViewModel
            if (bet != null)
                BetSlotViewModels[slotIndex] = new BetSlotViewModel(slotOwner, slotIndex, this.betType, bet);

            // if there is still no bet to display, i.e. because an existing bet was removed, set the BetSlotViewModel to null
            else
                BetSlotViewModels[slotIndex] = null;
        }
    }
}
