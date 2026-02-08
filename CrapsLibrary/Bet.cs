using System.Numerics;

namespace CrapsLibrary
{
    public class Bet
    {
        public string betName;

        public bool isWorking;

        public uint amount;

        public List<int> winningTotals;

        public Bet(string betName, uint amount, List<int> winningTotals)
        {
            this.betName = betName;
            this.winningTotals = winningTotals;
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
