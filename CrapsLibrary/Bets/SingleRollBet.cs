namespace CrapsLibrary.Bets
{
    public class SingleRollBet : Bet
    {
        public SingleRollBet(Player betOwner, string betName, uint commitment, List<int> winningTotals, uint payout)
            : base(betOwner, betName, commitment, winningTotals, payout)
        {
        }

        internal override bool MeetsFirstWinningCondition(byte firstOutcome, byte secondOutcome)
        {
            throw new NotImplementedException();
        }

        internal override bool MeetsLosingCondition(byte firstOutcome, byte secondOutcome)
        {
            throw new NotImplementedException();
        }
    }
}
