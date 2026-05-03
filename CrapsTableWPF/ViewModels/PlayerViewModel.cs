using CrapsLibrary;
using CrapsLibrary.Bets;

namespace CrapsTableWPF.ViewModels
{
    public class PlayerViewModel : ViewModelBase
    {
        // bindable properties
        private string _name;

        public string Name
        {
            get => _name;
            private set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _purse;

        public string Purse
        {
            get => _purse;
            private set
            {
                _purse = value;
                OnPropertyChanged(nameof(Purse));
            }
        }

        private List<Bet> _playerBetList;

        public List<Bet> PlayerBetList
        {
            get => _playerBetList;
            private set
            {
                _playerBetList = value;
                OnPropertyChanged(nameof(PlayerBetList));
            }
        }

        public PlayerViewModel()
        {
            Player player = new("chase", 90);
            _name = player.Name;
            _purse = player.purse.ToString();
            _playerBetList = player.playerBetList;
        }
    }
}
