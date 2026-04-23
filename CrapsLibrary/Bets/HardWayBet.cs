namespace CrapsLibrary.Bets
{
    public class HardWayBet : Bet
    {
        int winningHalf;

        public HardWayBet(CrapsTable crapsTable, Player betOwner, betType betType, uint commitment, List<int> winningTotals, uint payout)
            : base(crapsTable, betOwner, betType, commitment, winningTotals, payout)
        {
            winningHalf = winningTotals.First() / 2;
        }

        internal override bool MeetsFirstWinningCondition(byte firstOutcome, byte secondOutcome)
        {
            if (firstOutcome == winningHalf && secondOutcome == winningHalf)
            {
                return true;
            }
            return false;
        }

        internal override bool MeetsLosingCondition(byte firstOutcome, byte secondOutcome)
        {
            if (crapsTable.puck.IsOutcomeSevenOut(firstOutcome, secondOutcome))
            {
                return true;
            }

            if (firstOutcome + secondOutcome == winningTotals.First() &&
                (firstOutcome != winningHalf || secondOutcome != winningHalf))
            {
                return true;
            }
            return false;
        }
    }
}
