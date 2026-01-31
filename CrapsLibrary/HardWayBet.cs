namespace CrapsLibrary
{
    internal class HardWayBet : Bet
    {
        int winningHalf;
        public HardWayBet(string betName, int winningTotal, int payoutNumerator, int payoutDenominator)
            :base(betName, winningTotal, payoutNumerator, payoutDenominator)
        {
            this.winningHalf = winningTotal / 2;
        }

        public bool MeetsWinningCondition01(int firstOutcome, int secondOutcome)
        {
            if (firstOutcome == winningHalf && secondOutcome == winningHalf)
            {
                return true;
            }
            return false;
        }
    }
}
