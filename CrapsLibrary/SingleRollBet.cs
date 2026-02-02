namespace CrapsLibrary
{
    public class SingleRollBet : Bet
    {
        public SingleRollBet(string betName, List<int> winningTotals, int payoutNumerator, int payoutDenominator)
            : base(betName, winningTotals, payoutNumerator, payoutDenominator)
        {
            ;
        }
    }
}
