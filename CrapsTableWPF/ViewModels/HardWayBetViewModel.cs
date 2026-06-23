using CrapsLibrary;
using CrapsLibrary.Bets;
using CrapsTableWPF.Infrastructure;
using CrapsTableWPF.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;

namespace CrapsTableWPF.ViewModels
{
    public class HardWayBetViewModel : ViewModelBase
    {
        public ObservableCollection<BetSlotViewModel?> BetSlotViewModels { get; }

        private readonly CrapsTable crapsTable;

        private readonly DialogService dialogService;

        private readonly betType betType;

        private readonly uint UnitOfBet; // TODO show UnitOfBet in hoverover pop up

        private int? _countOfUnitsToBet = 1;

        public int? CountOfUnitsToBet
        {
            get => _countOfUnitsToBet;
            set
            {
                _countOfUnitsToBet = value;
                OnPropertyChanged(nameof(CountOfUnitsToBet));
            }
        }

        public ICommand CreateBetCommand { get; }

        public HardWayBetViewModel(CrapsTable crapsTable, DialogService dialogService, betType betType)
        {
            this.crapsTable = crapsTable;
            this.dialogService = dialogService;
            this.betType = betType;
            this.UnitOfBet = BetFactory.DetermineUnitOfBet(crapsTable, betType);
            this.BetSlotViewModels = new(crapsTable.Slots.Select(x => (BetSlotViewModel?)null));

            this.CreateBetCommand = new RelayCommand(_ => CreateOrUpdateBet());


        }

        public void PopulateBetSlot(Player slotOwner, Bet bet)
        {
            // determine slot index of the betting player
            int slotIndex = Array.IndexOf(crapsTable.Slots, slotOwner);

            // populate bet slot view
            BetSlotViewModels[slotIndex] = new BetSlotViewModel(slotOwner, slotIndex, this.betType, bet);
        }

        public void CreateOrUpdateBet() // TODO send to a base class
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
    }
}
