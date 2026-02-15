namespace CrapsLibrary
{
    public class HardWayBet : Bet
    {
        int winningHalf;

        public HardWayBet(Player betOwner, string betName, uint commitment, List<int> winningTotals, uint payout)
            : base(betOwner, betName, commitment, winningTotals, payout)
        {
            this.winningHalf = winningTotals.First() / 2;
            CrapsTable.scoreboard.NewSubscriber(this.EvaluateBet);
        }

        protected override bool MeetsFirstWinningCondition(byte firstOutcome, byte secondOutcome)
        {
            if (firstOutcome == winningHalf && secondOutcome == winningHalf)
            {
                return true;
            }
            return false;
        }

        protected override bool MeetsLosingCondition(byte firstOutcome, byte secondOutcome)
        {
            if (CrapsTable.puck.IsOutcomeSevenOut(firstOutcome, secondOutcome))
            {
                return true;
            }

            if ((firstOutcome + secondOutcome == this.winningTotals.First()) &&
                (firstOutcome != winningHalf || secondOutcome != winningHalf))
            {
                return true;
            }
            return false;
        }
    }
}
