namespace CrapsLibrary
{
    public class PlaceBet : Bet
    {
        public PlaceBet(Player betOwner, string betName, uint commitment, List<int> winningTotals, uint payout) 
            : base(betOwner, betName, commitment, winningTotals, payout)
        {
            CrapsTable.scoreboard.NewSubscriber(this.EvaluateBet);
        }

        protected override bool MeetsFirstWinningCondition(byte firstOutcome, byte secondOutcome)
        {
            // the puck is on and the roll results in a board number
            // (betting directly on the point number is prohibited)
            if (CrapsTable.puck.IsOn == true && this.winningTotals.Contains(firstOutcome + secondOutcome))
            {
                return true;
            }
            return false;
        }

        protected override bool MeetsLosingCondition(byte firstOutcome, byte secondOutcome)
        {
            // the puck is on and then a seven is rolled
            if (CrapsTable.puck.IsOn == true && CrapsTable.puck.seven == (firstOutcome + secondOutcome))
            {
                return true;
            }
            return false;
        }
    }
}
