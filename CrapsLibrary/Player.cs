using CrapsLibrary.Bets;
using System.Collections.ObjectModel;

namespace CrapsLibrary
{
    public class Player : ObservableObject
    {
        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private uint _purse;
        public uint Purse
        {
            get => _purse;
            set
            {
                if (_purse != value)
                {
                    _purse = value;
                    OnPropertyChanged(nameof(Purse));
                }
            }
        }

        public ObservableCollection<Bet> PlayerBetList { get; }

        public Player(string playerName, uint startingPurse = 0)
        {
            this.Name = playerName;
            this.Purse = startingPurse;
            this.PlayerBetList = new ObservableCollection<Bet>();
        }

        // become new roller
        // skip my roll
        // cash out/quit

        public void PauseOneBet()
        {

        }

        public void PauseAllBets()
        {

        }

        public void ReactivateOneBet()
        {

        }
        
        public void ReactivateAllBets()
        {

        }
    }
}
