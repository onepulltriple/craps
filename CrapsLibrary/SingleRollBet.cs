namespace CrapsLibrary
{
    public class SingleRollBet : Bet
    {
        public SingleRollBet(string betName, uint commitment, List<int> winningTotals, uint payout)
            : base(betName, commitment, winningTotals, payout)
        {
            ;
        }
    }
}
