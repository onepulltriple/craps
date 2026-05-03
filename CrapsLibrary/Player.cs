using CrapsLibrary.Bets;

namespace CrapsLibrary
{
    public class Player
    {
        public string playerName; // TODO make private

        public string Name => playerName;

        public uint purse;

        public List<Bet> playerBetList;

        public Player(string playerName, uint startingPurse = 0)
        {
            this.playerName = playerName;
            purse = startingPurse;
            playerBetList = new List<Bet>();
        }

        public uint BuyChips(uint purchase)
        {
            purse += purchase;
            return purse;
        }

        //public void CreateBet(Bet newBet)
        //{
        //    playerBetList.Add(newBet);
        //}
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
