using CrapsLibrary;
using CrapsLibrary.Bets;
using System.Collections.ObjectModel;

namespace CrapsTableWPF.ViewModels
{
    public class HardWayBetViewModel : ViewModelBase
    {
        public ObservableCollection<BetSlotViewModel> BetSlotViewModels { get; }

        private readonly CrapsTable crapsTable;

        //public readonly betType BetType;

        public HardWayBetViewModel(CrapsTable crapsTable, betType betType)
        {
            this.crapsTable = crapsTable;

            this.BetSlotViewModels = new ObservableCollection<BetSlotViewModel>(
                crapsTable.Slots.Select((player, slotIndex) =>
                {
                    var bet = player?.playerBetList.FirstOrDefault(b => b.betType == betType);

                    return new BetSlotViewModel(bet, slotIndex);
                }));

            //this.BetType = betType;

        }
    }
}
