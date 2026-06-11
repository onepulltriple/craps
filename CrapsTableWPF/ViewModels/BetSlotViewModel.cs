using CrapsLibrary.Bets;

namespace CrapsTableWPF.ViewModels
{
    public class BetSlotViewModel : ViewModelBase
    {
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

        // Bindable Properties ///////////////////////////////////////////////
        public int SlotIndex { get; }

        public int DisplayedSlotIndex => SlotIndex + 1;

        public bool IsEmpty => BetViewModel == null;

        public BetSlotViewModel(Bet? bet, int slotIndex)
        {
            // Bindable Properties
            this.BetViewModel = bet is null ? null : new BetViewModel(bet);
            this.SlotIndex = slotIndex;
        }
    }
}
