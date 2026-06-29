using System.Collections.ObjectModel;
using System.Windows.Input;
using CrapsLibrary;
using CrapsLibrary.Bets;
using CrapsTableWPF.Infrastructure;
using CrapsTableWPF.Services;

namespace CrapsTableWPF.ViewModels
{
    // This is the ViewModel used for the MainWindow.

    public class CrapsTableViewModel : ViewModelBase
    {
        private CrapsTable _model;
        public readonly Puck puck;

        public ObservableCollection<PlayerSlotViewModel> PlayerSlotViewModels { get; }

        public GameEventFeedViewModel GameEventFeedViewModel { get; }

        public BettingAreaViewModelBase PassBetViewModel { get; }
        public PuckViewModel PuckViewModel { get; }
        public BettingAreaViewModelBase PlaceBetViewModel_PlaceBet_04 { get; }
        public BettingAreaViewModelBase PlaceBetViewModel_PlaceBet_05 { get; }
        public BettingAreaViewModelBase PlaceBetViewModel_PlaceBet_06 { get; }
        public BettingAreaViewModelBase PlaceBetViewModel_PlaceBet_08 { get; }
        public BettingAreaViewModelBase PlaceBetViewModel_PlaceBet_09 { get; }
        public BettingAreaViewModelBase PlaceBetViewModel_PlaceBet_10 { get; }

        public HardWayBetViewModel HardWayBetViewModel_Hard_04 { get; }
        public HardWayBetViewModel HardWayBetViewModel_Hard_06 { get; }
        public HardWayBetViewModel HardWayBetViewModel_Hard_08 { get; }
        public HardWayBetViewModel HardWayBetViewModel_Hard_10 { get; }

        


        // Bindable Properties ///////////////////////////////////////////////
        public uint TableMinimum
        {
            get => _model.tableMinimum;
            private set
            {
                if (_model.tableMinimum != value)
                {
                    _model.tableMinimum = value;
                    OnPropertyChanged(nameof(TableMinimum));
                }
            }
        }

        public string DisplayedTableMinimum => $"Table minimum: {TableMinimum}";

        public uint TableMaximum
        {
            get => _model.tableMaximum;
            private set
            {
                if (_model.tableMaximum != value)
                {
                    _model.tableMaximum = value;
                    OnPropertyChanged(nameof(TableMaximum));
                }
            }
        }

        public string DisplayedTableMaximum => $"Table maximum: {TableMaximum}";

        public int CurrentPlayerIndex => _model.CurrentPlayerIndex;

        // Commands //////////////////////////////////////////////////////////
        public ICommand RollDiceCommand { get; }

        public ICommand NextTurnCommand { get; }

        public CrapsTableViewModel(CrapsTable crapsTable)
        {
            this._model = crapsTable;
            this.puck = crapsTable.puck;

            // start DialogService
            var dialogService = new DialogService();

            // ViewModels
            this.PlayerSlotViewModels = new ObservableCollection<PlayerSlotViewModel>(
                _model.Slots.Select((player,slotIndex) => 
                new PlayerSlotViewModel(crapsTable, player, slotIndex, dialogService)) 
                );

            this.GameEventFeedViewModel = new GameEventFeedViewModel(crapsTable.gameEventFeed);

            this.PassBetViewModel = new BettingAreaViewModelBase(crapsTable, dialogService, betType.PassBet);
            this.PuckViewModel = new PuckViewModel(this.puck);

            this.PlaceBetViewModel_PlaceBet_04 = new PlaceBetViewModel(crapsTable, dialogService, betType.PlaceBet_04);
            this.PlaceBetViewModel_PlaceBet_05 = new PlaceBetViewModel(crapsTable, dialogService, betType.PlaceBet_05);
            this.PlaceBetViewModel_PlaceBet_06 = new PlaceBetViewModel(crapsTable, dialogService, betType.PlaceBet_06);
            this.PlaceBetViewModel_PlaceBet_08 = new PlaceBetViewModel(crapsTable, dialogService, betType.PlaceBet_08);
            this.PlaceBetViewModel_PlaceBet_09 = new PlaceBetViewModel(crapsTable, dialogService, betType.PlaceBet_09);
            this.PlaceBetViewModel_PlaceBet_10 = new PlaceBetViewModel(crapsTable, dialogService, betType.PlaceBet_10);

            this.HardWayBetViewModel_Hard_04 = new HardWayBetViewModel(crapsTable, dialogService, betType.Hard_04);
            this.HardWayBetViewModel_Hard_06 = new HardWayBetViewModel(crapsTable, dialogService, betType.Hard_06);
            this.HardWayBetViewModel_Hard_08 = new HardWayBetViewModel(crapsTable, dialogService, betType.Hard_08);
            this.HardWayBetViewModel_Hard_10 = new HardWayBetViewModel(crapsTable, dialogService, betType.Hard_10);

            // Bindable Properties
            this._model.PropertyChanged += (_, _) =>
            {
                OnPropertyChanged(nameof(CurrentPlayerIndex));
            };

            // Commands
            this.RollDiceCommand = new RelayCommand(_ => _model.RollDiceAndAnnounceOutcomes());
            this.NextTurnCommand = new RelayCommand(_ => _model.NextTurn());

        }
    }
}
