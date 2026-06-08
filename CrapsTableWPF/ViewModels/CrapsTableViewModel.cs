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

        public ObservableCollection<PlayerSlotViewModel> PlayerSlotViewModels { get; }

        public GameEventFeedViewModel GameEventFeedViewModel { get; }

        // TODO decide how to implement puck


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


        // Commands //////////////////////////////////////////////////////////


        public CrapsTableViewModel(CrapsTable crapsTable)
        {
            this._model = crapsTable;

            var dialogService = new DialogService();

            this.PlayerSlotViewModels = new ObservableCollection<PlayerSlotViewModel>(
                _model.Slots.Select((player,slotIndex) => 
                new PlayerSlotViewModel(crapsTable, player, slotIndex, dialogService)) 
                );

            this.GameEventFeedViewModel = new GameEventFeedViewModel(crapsTable.gameEventFeed);
        }
    }
}
