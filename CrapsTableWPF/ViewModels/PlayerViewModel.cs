using CrapsLibrary;
using CrapsTableWPF.Infrastructure;
using System.Windows.Input;

namespace CrapsTableWPF.ViewModels
{
    public class PlayerViewModel : ViewModelBase
    {
        private readonly Player _model;

        public Player Model => _model;

        // Bindable Properties ///////////////////////////////////////////////
        public string Name
        {
            get => _model.name;
            set
            {
                if (_model.name != value)
                {
                    _model.name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public uint Purse
        {
            get => _model.Purse;
            set
            {
                if (_model.Purse != value)
                    _model.Purse = value;
            }
        }

        // Commands //////////////////////////////////////////////////////////

        public PlayerViewModel(Player player)
        {
            this._model = player;

            this._model.PurseChanged += (_, _) =>
            {
                OnPropertyChanged(nameof(Purse));
            };
        }

    }
}
