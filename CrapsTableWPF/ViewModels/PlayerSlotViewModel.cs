using CrapsLibrary;
using CrapsTableWPF.Data_Transfer_Objects;
using CrapsTableWPF.Infrastructure;
using CrapsTableWPF.Services;
using System.Windows.Input;

namespace CrapsTableWPF.ViewModels
{
    public class PlayerSlotViewModel : ViewModelBase
    {
        private PlayerViewModel? _playerViewModel;
        public PlayerViewModel? PlayerViewModel
        {
            get => _playerViewModel;
            private set
            {
                if (_playerViewModel == value) 
                    return;
                _playerViewModel = value;
                OnPropertyChanged(nameof(PlayerViewModel));
                OnPropertyChanged(nameof(IsEmpty));
            }
        }

        // Bindable Properties ///////////////////////////////////////////////
        private readonly CrapsTable crapsTable;

        public int SlotIndex { get; }
        public int DisplayedSlotIndex => SlotIndex + 1;

        private readonly DialogService dialogService;

        public bool IsEmpty => PlayerViewModel == null;

        // Commands //////////////////////////////////////////////////////////
        public ICommand AddPlayerCommand { get; }
        public ICommand RenamePlayerCommand { get; }


        public PlayerSlotViewModel(CrapsTable crapsTable, Player? player, int slotIndex, DialogService dialogService) 
        {
            // Bindable Properties
            this.crapsTable = crapsTable;
            this.PlayerViewModel = player is null ? null : new PlayerViewModel(player);
            this.SlotIndex = slotIndex;
            this.dialogService = dialogService;

            // Commands
            this.AddPlayerCommand = new RelayCommand(_ => AddPlayer());
            this.RenamePlayerCommand = new RelayCommand(_ => RenamePlayer());
        }

        private void RenamePlayer()
        {
            // right-click?
            throw new NotImplementedException();
        }

        private void AddPlayer()
        {
            // call dialog service to collect new player info
            var newPlayerDTO = dialogService.ShowAddPlayerDialog();

            if (newPlayerDTO == null)
                return;

            // create new player
            Player player = new Player(newPlayerDTO.Name, newPlayerDTO.Purse);

            // add player to the player slot at this PlayerSlotViewModel's index
            Result<bool> result = crapsTable.InsertPlayerAtSlot(SlotIndex, player);

            // TODO tell user why this didn't work (when exactly should this happen?)

            if (result.Success)
            {
                PlayerViewModel = new PlayerViewModel(player);
                // TODO show messages
            }
        }
    }
}
