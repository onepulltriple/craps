using System.Numerics;

namespace CrapsLibrary
{
    public class Bet
    {
        public string betName;

        public bool isWorking;

        public uint commitment;

        public uint payout;

        public List<int> winningTotals;

        public Bet(string betName, uint payout, List<int> winningTotals)
        {
            this.betName = betName;
            this.winningTotals = winningTotals;
            this.payout = payout;
            this.StartWorking(); // activates bet upon creation
        }

        public void StartWorking()
        {
            this.isWorking = true;
        }

        public void QuitWorking()
        {
            this.isWorking = false;
        }
    }
}
