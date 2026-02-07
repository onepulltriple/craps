namespace CrapsLibrary
{
    public class Player
    {
        public int purse;

        public string playerName;

        public List<Bet> workingBets;

        public List<Bet> nonWorkingBets;

        public Player(string playerName)
        {
            this.playerName = playerName;
            purse = 0;
            workingBets = new List<Bet>();
            nonWorkingBets = new List<Bet>();
        }

        public int BuyChips(int purchase)
        {
            purse += purchase;
            return purse;
        }

        public void CreateBet(Bet newBet)
        {
            //if (purse >= newBet.)
            workingBets.Add(newBet);
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
