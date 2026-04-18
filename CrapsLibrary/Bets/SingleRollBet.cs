namespace CrapsLibrary.Bets
{
    public class SingleRollBet : Bet
    {
        public SingleRollBet(CrapsTable crapsTable, Player betOwner, string betName, uint commitment, List<int> winningTotals, uint payout)
            : base(crapsTable, betOwner, betName, commitment, winningTotals, payout)
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
