using CrapsLibrary;
using CrapsTableWPF.Infrastructure;
using CrapsTableWPF.Services;
using System.Numerics;
using System.Windows;
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

        private readonly DialogService dialogService;

        // Bindable Properties ///////////////////////////////////////////////
        private readonly CrapsTable crapsTable;

        public int SlotIndex { get; }

        public int DisplayedSlotIndex => SlotIndex + 1;

        public bool IsEmpty => PlayerViewModel == null;

        // Commands //////////////////////////////////////////////////////////
        public ICommand AddPlayerCommand { get; }

        public ICommand UpdatePlayerCommand { get; }

        public ICommand RemovePlayerCommand { get; }

        public PlayerSlotViewModel(CrapsTable crapsTable, Player? player, int slotIndex, DialogService dialogService) 
        {
            // Bindable Properties
            this.crapsTable = crapsTable;
            this.PlayerViewModel = player is null ? null : new PlayerViewModel(player);
            this.SlotIndex = slotIndex;
            this.dialogService = dialogService;

            // Commands
            this.AddPlayerCommand = new RelayCommand(_ => AddPlayer());
            this.UpdatePlayerCommand = new RelayCommand(_ => UpdatePlayer());
            this.RemovePlayerCommand = new RelayCommand(_ => RemovePlayer());
        }

        private void AddPlayer()
        {
            // call dialog service to collect new player info
            var newPlayerDTO = dialogService.CreateOrUpdatePlayerDialog(null);

            if (newPlayerDTO == null)
                return;

            // create new player using DTO data
            Player player = new Player(newPlayerDTO.Name, newPlayerDTO.Purse);

            // attempt to add player to the player slot at this PlayerSlotViewModel's index
            Result<bool> addPlayerResult = crapsTable.InsertPlayerAtSlot(SlotIndex, player);

            // error message if there are problems with inserting player at a slot
            if (!addPlayerResult.Success)
            {
                crapsTable.gameEventFeed.AddMultiLine(addPlayerResult);
                return;
            }

            // announce that the player has been added
            crapsTable.gameEventFeed.AddMultiLine(addPlayerResult);

            PlayerViewModel = new PlayerViewModel(player);

        }

        // TODO consolidate UpdatePlayer() and AddPlayer() methods?
        private void UpdatePlayer()
        {
            // null check
            if (this.PlayerViewModel == null || this.PlayerViewModel.Model == null)
                return;

            // load existing player into DTO data
            var playerDTO = dialogService.CreateOrUpdatePlayerDialog(this.PlayerViewModel.Model);

            if (playerDTO == null)
                return;

            // announce that the player has been updated
            crapsTable.gameEventFeed.Add($"{PlayerViewModel.Name} is now {playerDTO.Name} with a purse of {playerDTO.Purse}.");

            // perform the updates
            this.PlayerViewModel.Name = playerDTO.Name;
            this.PlayerViewModel.Purse = playerDTO.Purse;
        }

        private void RemovePlayer()
        {
            // null check
            if (this.PlayerViewModel == null)
                return;

            // pop-up to ask for confirmation
            MessageBoxResult answer01 = MessageBoxResult.No;

            answer01 = MessageBox.Show(
                $"Are you sure you want to remove {this.PlayerViewModel.Name} from the table?",
                "Remove player?",
                MessageBoxButton.YesNo);

            if (answer01 == MessageBoxResult.No)
                return;

            // remove player from craps table
            Result<bool> removePlayerResult = crapsTable.RemovePlayer(PlayerViewModel.Model);

            // announce the outcome
            crapsTable.gameEventFeed.AddMultiLine(removePlayerResult);

            // remove playerviewmodel from list
            PlayerViewModel = null;
        }

    }
}
