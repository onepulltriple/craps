namespace CrapsLibrary
{
    public class Player
    {
        public uint purse;

        public string playerName;

        public List<Bet> playerBetList;

        public Player(string playerName)
        {
            this.playerName = playerName;
            purse = 0;
            playerBetList = new List<Bet>();
        }

        public uint BuyChips(uint purchase)
        {
            purse += purchase;
            return purse;
        }

        public void CreateBet(Bet newBet)
        {
            //if (purse >= newBet.)
            playerBetList.Add(newBet);
        }
        // become new roller
        // skip my roll
        // throw dice
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
