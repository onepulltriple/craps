namespace CrapsLibrary
{
    public class SingleRollBet : Bet
    {
        public SingleRollBet(string betName, uint amount, List<int> winningTotals)
            : base(betName, amount, winningTotals)
        {
            ;
        }
    }
}
