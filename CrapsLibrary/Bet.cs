using System.Numerics;

namespace CrapsLibrary
{
    public class Bet
    {
        public string betName;

        public uint commitment;

        public List<int> winningTotals;

        public uint payout;

        public bool isWorking;

        public Bet(string betName, uint commitment, List<int> winningTotals, uint payout)
        {
            this.betName = betName;
            this.commitment = commitment;
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
