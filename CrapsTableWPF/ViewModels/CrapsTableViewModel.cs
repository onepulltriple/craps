using System.Collections.ObjectModel;
using CrapsLibrary;
using CrapsLibrary.Bets;

namespace CrapsTableWPF.ViewModels
{
    // This is the ViewModel used for the MainWindow.

    public class CrapsTableViewModel : ViewModelBase
    {
        private CrapsTable _model;

        public ObservableCollection<PlayerSlotViewModel> PlayerSlotViewModels { get; }

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


        public CrapsTableViewModel(CrapsTable crapsTable)
        {
            this._model = crapsTable;

            this.PlayerSlotViewModels = new ObservableCollection<PlayerSlotViewModel>(
                _model.Slots.Select((player,slotIndex) => 
                new PlayerSlotViewModel(crapsTable,player,slotIndex)) 
                );


        }
    }
}
