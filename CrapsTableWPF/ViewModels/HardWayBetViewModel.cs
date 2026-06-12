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

        public ICommand CreateBetCommand { get; }

        public HardWayBetViewModel(CrapsTable crapsTable, betType betType)
        {
            this.crapsTable = crapsTable;

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

        public void CreateBetWPF()
        {
            var temp = crapsTable.GetCurrentPlayer();

            // pick up here later
        }
    }
}
