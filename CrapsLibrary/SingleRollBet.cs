namespace CrapsLibrary
{
    public class SingleRollBet : Bet
    {
        public SingleRollBet(Player betOwner, string betName, uint commitment, List<int> winningTotals, uint payout)
            : base(betOwner, betName, commitment, winningTotals, payout)
        {
            ;
        }

        protected override bool MeetsFirstWinningCondition(byte firstOutcome, byte secondOutcome)
        {
            throw new NotImplementedException();
        }

        protected override bool MeetsLosingCondition(byte firstOutcome, byte secondOutcome)
        {
            throw new NotImplementedException();
        }
    }
}
