using System.Numerics;

namespace CrapsLibrary
{
    public class Bet
    {
        public string betName;
        public int payoutNumerator;
        public int payoutDenominator;
        public bool isWorking;
        public List<int> winningTotals;

        public Bet(string betName, List<int> winningTotals, int payoutNumerator, int payoutDenominator)
        {
            this.betName = betName;
            this.winningTotals = winningTotals;
            this.payoutNumerator = payoutNumerator;
            this.payoutDenominator = payoutDenominator;
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

        public bool AmountIsAllowed(int amount)
        {
            if (amount <= 0)
            {
                return false;
                // error message: amount must be greater than zero!
            }

            if (amount % payoutDenominator != 0)
            {
                return false;
                // error message: amount must be a multiple of payoutDenominator
                // e.g. payoutDenominator * 1, payoutDenominator * 2, payoutDenominator * 3 ...
            }

            return true;
        }
    }
}
