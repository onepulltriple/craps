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

        private readonly uint UnitOfBet;

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
            this.UnitOfBet = crapsTable.tableMinimum /
                CrapsTable.absoluteTableMinimum *
                BetFactory.BetDefinitions[betType].payoutDenominator;
            this.BetSlotViewModels = new(crapsTable.Slots.Select(x => (BetSlotViewModel?)null));

            this.CreateBetCommand = new RelayCommand(_ => CreateBet());


        }

        public void PopulateBetSlot(Player slotOwner, Bet bet)
        {
            // determine slot index of the betting player
            int slotIndex = Array.IndexOf(crapsTable.Slots, slotOwner);

            // populate bet slot view
            BetSlotViewModels[slotIndex] = new BetSlotViewModel(slotOwner, slotIndex, this.betType, bet);
        }

        public void CreateBet() // TODO send to a base class and make CreateOrUpdate()
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

            // check if the player is allowed to place a new bet of this type, should no existing bet be found
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

            // call dialog service to administer bet info
            var betVO = dialogService.CreateOrUpdateBetDialog(this.crapsTable, this.betType, existingBet);



            //// determine commitment
            //if (CountOfUnitsToBet == null)
            //    return;

            //uint commitment = (uint)CountOfUnitsToBet * UnitOfBet;

            if (betVO == null)
                return;

            // create a bet (with this player in mind) using the betVO data
            // the best will not yet be added to the player bet list
            Result<Bet> createBetResult = BetFactory.CreateBet(this.crapsTable, currentPlayer.Value, this.betType, betVO.Commitment);

            // error message if the bet creation failed
            crapsTable.gameEventFeed.AddMultiLine(createBetResult);

            // abort if the bet creation failed
            if (!createBetResult.Success)
                return;

            // add the bet to the player's bet list
            currentPlayer.Value.AddOneBet(createBetResult.Value);

            // populate the view
            this.PopulateBetSlot(currentPlayer.Value, createBetResult.Value);

            // also fix the ugliness of the popup dialog
        }
    }
}
