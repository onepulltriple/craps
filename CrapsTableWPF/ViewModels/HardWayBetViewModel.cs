using CrapsLibrary;
using CrapsLibrary.Bets;
using CrapsTableWPF.Infrastructure;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CrapsTableWPF.ViewModels
{
    public class HardWayBetViewModel : ViewModelBase
    {
        public ObservableCollection<BetSlotViewModel> BetSlotViewModels { get; }

        private readonly CrapsTable crapsTable;

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

        public HardWayBetViewModel(CrapsTable crapsTable, betType betType)
        {
            this.crapsTable = crapsTable;
            this.betType = betType;
            this.UnitOfBet = crapsTable.tableMinimum /
                CrapsTable.absoluteTableMinimum *
                BetFactory.BetDefinitions[betType].payoutDenominator;

            this.BetSlotViewModels = new ObservableCollection<BetSlotViewModel>(
                crapsTable.Slots.Select(
                    (player, slotIndex) =>
                        {
                            var bet = player?.playerBetList.FirstOrDefault(b => b.betType == betType);

                            return new BetSlotViewModel(bet, slotIndex);
                        }
                )
            );

            this.CreateBetCommand = new RelayCommand(_ => CreateBet());

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

            // check if the player is allowed to place this bet
            Result<bool> isBetAllowed = BetFactory.CheckIfCreateBetAllowed(this.crapsTable, currentPlayer.Value, this.betType);

            // error message if the bet is not allowed
            if (!isBetAllowed.Success)
            {
                crapsTable.gameEventFeed.AddMultiLine(isBetAllowed);
                return;
            }

            // determine commitment
            if (CountOfUnitsToBet == null)
                return;

            uint commitment = (uint)CountOfUnitsToBet * UnitOfBet;

            // create bet for this player
            Result<Bet> createBetResult = BetFactory.CreateBet(this.crapsTable, currentPlayer.Value, this.betType, commitment);

            // error message if the bet creation failed
            if (!createBetResult.Success)
            {
                crapsTable.gameEventFeed.AddMultiLine(createBetResult);
                return;
            }


        }
    }
}
