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

            this.BetViewModel = new BetViewModel(bet);

            slotOwner.PlayerBetList.CollectionChanged += OnPlayerBetListChanged;
        }

        private void OnPlayerBetListChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems?.Count > 0)
            {
                var bet = (Bet)e.NewItems.OfType<Bet>().Select(b => b.betType == this.betType);

                this.BetViewModel = new BetViewModel(bet);
            }

            if (e.OldItems?.Count > 0)
            {
                var bet = (Bet)e.OldItems.OfType<Bet>().Select(b => b.betType == this.betType);

                // do what?
            }
        }
    }
}
