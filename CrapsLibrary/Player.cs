using CrapsLibrary.Bets;
using System.Collections.ObjectModel;

namespace CrapsLibrary
{
    public class Player : ObservableObject
    {
        readonly CrapsTable crapsTable;

        // Observeable properties
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

        public Player(CrapsTable crapsTable, string playerName, uint startingPurse = 0)
        {
            this.crapsTable = crapsTable;
            this.Name = playerName;
            this.Purse = startingPurse;
            this.PlayerBetList = new ObservableCollection<Bet>();
        }


        public void AddOneBet(Bet bet)
        {
            this.PlayerBetList.Add(bet);

            // announce that a bet has been placed
            //this.crapsTable.OnBetPlaced();
        }

        public void RemoveOneBet(Bet bet)
        {
            this.PlayerBetList.Remove(bet);
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
