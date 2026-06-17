using CrapsLibrary.Bets;

namespace CrapsLibrary
{
    public class Player
    {
        public string name { get; set; }

        private uint _purse;
        public uint Purse
        {
            get => _purse;
            set
            {
                if (_purse != value)
                {
                    _purse = value;
                    PurseChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler? PurseChanged;

        public List<Bet> playerBetList;

        public Player(string playerName, uint startingPurse = 0)
        {
            this.name = playerName;
            this.Purse = startingPurse;
            this.playerBetList = new List<Bet>();
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
