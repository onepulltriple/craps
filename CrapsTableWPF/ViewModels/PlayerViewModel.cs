using CrapsLibrary;
using CrapsLibrary.Bets;

namespace CrapsTableWPF.ViewModels
{
    public class PlayerViewModel : ViewModelBase
    {
        private Player _model;

        // Bindable Properties ///////////////////////////////////////////////
        public string Name
        {
            get => _model.Name;
            private set
            {
                if (_model.playerName != value)
                {
                    _model.playerName = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public uint Purse
        {
            get => _model.purse;
            private set
            {
                if (_model.purse != value)
                {
                    _model.purse = value;
                    OnPropertyChanged(nameof(Purse));
                }
            }
        }

        public PlayerViewModel(Player player)
        {
            this._model = player;
        }


    }
}
