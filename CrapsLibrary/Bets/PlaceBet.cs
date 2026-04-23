namespace CrapsLibrary.Bets
{
    public class PlaceBet : Bet
    {
        public PlaceBet(CrapsTable crapsTable, Player betOwner, betType betType, uint commitment, List<int> winningTotals, uint payout) 
            : base(crapsTable, betOwner, betType, commitment, winningTotals, payout)
        {
        }

        internal override bool MeetsFirstWinningCondition(byte firstOutcome, byte secondOutcome)
        {
            // the puck is on and the roll results in a board number
            // (betting directly on the point number is prohibited)
            if (crapsTable.puck.IsOn == true && winningTotals.Contains(firstOutcome + secondOutcome))
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
            return false;
        }
    }
}
