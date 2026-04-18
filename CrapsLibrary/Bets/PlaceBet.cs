using System.Linq;

namespace CrapsLibrary.Bets
{
    public class PlaceBet : Bet
    {
        public PlaceBet(CrapsTable crapsTable, Player betOwner, string betName, uint commitment, List<int> winningTotals, uint payout) 
            : base(crapsTable, betOwner, betName, commitment, winningTotals, payout)
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

        public static bool IsPlaceBetAllowed(CrapsTable crapsTable, Player playerToCheck, string betName)
        {
            return playerToCheck.playerBetList.Any(bet => bet.betName == "PassBet") // player must have placed a pass bet
                && crapsTable.puck.IsOn                                             // the puck may not be OFF, i.e. a point must be established
                && int.Parse(betName.Split('_')[1]) != crapsTable.puck.passPoint;   // place bets cannot be placed on the point
        }
    }
}
