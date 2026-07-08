namespace CrapsLibrary.Bets
{
    public class MultiRollBet : Bet
    {
        public MultiRollBet(CrapsTable crapsTable, Player betOwner, betType betType, uint countOfUnitsToBet, uint unitOfBet, List<int> winningTotals, uint payout)
            : base(crapsTable, betOwner, betType, countOfUnitsToBet, unitOfBet, winningTotals, payout)
        {
            
        }

        internal override bool MeetsFirstWinningCondition(byte firstOutcome, byte secondOutcome)
        {
            if (winningTotals.Contains(firstOutcome + secondOutcome))
                return true;

            return false;
        }

        internal override bool MeetsLosingCondition(byte firstOutcome, byte secondOutcome)
        {
            // loses on seven out, i.e. these are not single-roll bets!
            if (crapsTable.puck.IsOutcomeSevenOut(firstOutcome, secondOutcome))
                return true;

            return false;
        }
    }
}
