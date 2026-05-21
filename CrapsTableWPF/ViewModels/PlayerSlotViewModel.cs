using CrapsLibrary;
using CrapsTableWPF.Infrastructure;
using System.Windows.Input;

namespace CrapsTableWPF.ViewModels
{
    public class PlayerSlotViewModel : ViewModelBase
    {

        // Bindable Properties ///////////////////////////////////////////////
        private PlayerViewModel? _playerViewModel;
        public PlayerViewModel? PlayerViewModel
        {
            get => _playerViewModel;
            private set
            {
                if (_playerViewModel == value) return;
                _playerViewModel = value;
                OnPropertyChanged(nameof(PlayerViewModel));
                OnPropertyChanged(nameof(IsEmpty));
            }
        }

        public int SlotIndex { get; }

        public bool IsEmpty => PlayerViewModel == null;

        private readonly CrapsTable crapsTable;

        // Commands //////////////////////////////////////////////////////////
        public ICommand AddPlayerCommand { get; }


        public PlayerSlotViewModel(CrapsTable crapsTable, Player? player, int slotIndex) 
        {
            this.PlayerViewModel = player is null ? null : new PlayerViewModel(player);

            this.SlotIndex = slotIndex;
            this.crapsTable = crapsTable;
            this.AddPlayerCommand = new RelayCommand(_ => AddPlayer());
        }

        private void AddPlayer()
        {
            Player player = new Player("Marty McFly", 600);

            var result = crapsTable.InsertPlayerAtSlot(SlotIndex, player);

            if (result.Success)
            {
                PlayerViewModel = new PlayerViewModel(player);
            }
        }
    }
}
