namespace CrapsLibrary
{
    public class SingleRollBet : Bet
    {
        public SingleRollBet(string betName, int amount, List<int> winningTotals, int payoutNumerator, int payoutDenominator)
            : base(betName, amount, winningTotals, payoutNumerator, payoutDenominator)
        {
            ;
        }
    }
}
