using CrapsLibrary;
using CrapsLibrary.Bets;
using System.Collections.Specialized;
using System.Linq;

namespace CrapsTableWPF.ViewModels
{
    public class BetSlotViewModel : ViewModelBase
    {
        public betType betType;

        private BetViewModel? _betViewModel;
        
        private readonly Bet? _bet;

        public BetViewModel? BetViewModel
        {
            get => _betViewModel;
            private set
            {
                if (_betViewModel == value) 
                    return;
                _betViewModel = value;
                OnPropertyChanged(nameof(BetViewModel));
                OnPropertyChanged(nameof(IsEmpty));
            }
        }

        private Player? slotOwner;

        // Bindable Properties ///////////////////////////////////////////////
        public int SlotIndex { get; }

        public int DisplayedSlotIndex => SlotIndex + 1;

        public bool IsEmpty => BetViewModel == null;

        public BetSlotViewModel(Player slotOwner, int slotIndex, betType betType, Bet bet)
        {
            // Bindable Properties
            this.slotOwner = slotOwner;
            this.SlotIndex = slotIndex;
            this.betType = betType;
            this._bet = bet;

            this.BetViewModel = new BetViewModel(bet);

            slotOwner.PlayerBetList.CollectionChanged += OnPlayerBetListChanged;
        }

        private void OnPlayerBetListChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems?.Count > 0)
            {
                // 
            }

            // if there are removed items
            // and one of them is the bet which belonged in this slot
            if (e.OldItems?.Count > 0 && e.OldItems.Contains(this._bet))
            {
                // set IsEmpty to true by way of nullifying the BetViewModel
                this.BetViewModel = null;
            }
        }
    }
}
