using CrapsLibrary;
using CrapsTableWPF.Infrastructure;
using CrapsTableWPF.Services;
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

        private readonly DialogService dialogService;

        // Commands //////////////////////////////////////////////////////////
        public ICommand AddPlayerCommand { get; }


        public PlayerSlotViewModel(CrapsTable crapsTable, Player? player, int slotIndex, DialogService dialogService) 
        {
            this.PlayerViewModel = player is null ? null : new PlayerViewModel(player);

            this.SlotIndex = slotIndex;
            this.crapsTable = crapsTable;
            this.dialogService = dialogService;
            this.AddPlayerCommand = new RelayCommand(_ => AddPlayer());
        }

        private void AddPlayer()
        {
            var data = dialogService.ShowAddPlayerDialog();

            if (data == null)
                return;

            //Player player = new Player("Marty McFly", 600);
            Player player = new Player(data.Name, data.Purse);

            var result = crapsTable.InsertPlayerAtSlot(SlotIndex, player);

            // TODO tell user why this didn't work (when exactly should this happen?)

            if (result.Success)
            {
                PlayerViewModel = new PlayerViewModel(player);
                // TODO show messages
            }
        }
    }
}
