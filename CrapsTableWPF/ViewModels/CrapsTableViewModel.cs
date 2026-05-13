using System.CodeDom;
using System.Collections.ObjectModel;
using CrapsLibrary;
using CrapsLibrary.Bets;

namespace CrapsTableWPF.ViewModels
{
    public class CrapsTableViewModel : ViewModelBase
    {
        private CrapsTable _model;

        public ObservableCollection<PlayerSlotViewModel> PlayerSlotViewModels { get; }

        public string TableMaximum => _model.tableMaximum.ToString();

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

        public CrapsTableViewModel(CrapsTable crapsTable)
        {
            this._model = crapsTable;

            this.PlayerSlotViewModels = new ObservableCollection<PlayerSlotViewModel>(
                _model.Slots.Select((m,i) => new PlayerSlotViewModel(crapsTable,m,i)) 
                );


        }
    }
}
