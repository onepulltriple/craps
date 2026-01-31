using System.Numerics;

namespace CrapsLibrary
{
    internal class Bet
    {
        public string betName;
        public int payoutNumerator;
        public int payoutDenominator;
        public bool isWorking;
        public int winningTotal;

        public Bet(string betName, int winningTotal, int payoutNumerator, int payoutDenominator)
        {
            this.betName = betName;
            this.winningTotal = winningTotal;
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
