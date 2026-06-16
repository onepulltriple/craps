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

        public ICommand CreateBetCommand { get; }

        public HardWayBetViewModel(CrapsTable crapsTable, betType betType)
        {
            this.crapsTable = crapsTable;
            this.betType = betType;

            this.BetSlotViewModels = new ObservableCollection<BetSlotViewModel>(
                crapsTable.Slots.Select(
                    (player, slotIndex) =>
                        {
                            var bet = player?.playerBetList.FirstOrDefault(b => b.betType == betType);

                            return new BetSlotViewModel(bet, slotIndex);
                        }
                )
            );

            this.CreateBetCommand = new RelayCommand(_ => CreateBetWPF());

        }

        public void CreateBetWPF() // TODO send to a base class and make CreateOrUpdate()
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

            // create bet for this player
            Result<Bet> createBetResult = BetFactory.CreateBet(this.crapsTable, currentPlayer.Value, this.betType, 0);

            // error message if the bet creation failed
            if (!createBetResult.Success)
            {
                crapsTable.gameEventFeed.AddMultiLine(createBetResult);
                return;
            }


        }
    }
}
