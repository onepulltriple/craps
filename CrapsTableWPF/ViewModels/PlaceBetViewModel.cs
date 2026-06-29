using CrapsLibrary;
using CrapsLibrary.Bets;
using CrapsTableWPF.Services;
using System.ComponentModel;

namespace CrapsTableWPF.ViewModels
{
    public class PlaceBetViewModel : BettingAreaViewModelBase
    {
        private readonly Puck _puck;

        private betType _betType;

        public Puck Puck => _puck;


        private bool _thisPointIsSet;
        public bool ThisPointIsSet
        {
            get => _thisPointIsSet;
            set
            {
                if (_thisPointIsSet != value)
                {
                    _thisPointIsSet = value;
                    OnPropertyChanged(nameof(ThisPointIsSet));
                }
            }
        }

        public PlaceBetViewModel(CrapsTable crapsTable, DialogService dialogService, betType betType) : base(crapsTable, dialogService, betType)
        {
            this._puck = crapsTable.puck;
            this._puck.PropertyChanged += this.OnPuckPropertyChanged;
            this._betType = betType;
        }

        private void OnPuckPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Puck.PassPoint))
            {
                if (_puck.PassPoint == BetFactory.BetDefinitions[this._betType].winningTotals.First())
                    this.ThisPointIsSet = true;
                else
                    this.ThisPointIsSet = false;
            }
        }
    }
}
