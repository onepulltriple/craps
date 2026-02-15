using System.Linq;

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
            if (CrapsTable.puck.IsOutcomeSevenOut(firstOutcome, secondOutcome))
            {
                return true;
            }
            return false;
        }

        public static bool IsPlaceBetAllowed(Player playerToCheck, string betName)
        {
            return playerToCheck.playerBetList.Any(bet => bet.betName == "PassBet") // player must have placed a pass bet
                && CrapsTable.puck.IsOn                                             // the puck may not be OFF, i.e. a point must be established
                && int.Parse(betName.Split('_')[1]) != CrapsTable.puck.passPoint;   // place bets cannot be placed on the point
        }
    }
}
