
namespace CrapsLibrary
{
    public class PassBet : Bet
    {
        List<int> losingTotals;

        public PassBet(Player betOwner, string betName, uint commitment, List<int> winningTotals, uint payout)
            : base(betOwner, betName, commitment, winningTotals, payout)
        {
            losingTotals = new List<int> {2, 3, 12}; // crap out/missout
            winningTotals = new List<int> {7, 11 };  // natural pass
            CrapsTable.scoreboard.NewSubscriber(this.EvaluateBet);
        }

        protected override bool MeetsFirstWinningCondition(byte firstOutcome, byte secondOutcome)
        {
            // if point is off, 7 and 11 win
            // if point is on, matching the point wins

            if (CrapsTable.puck.IsOn == false && winningTotals.Contains(firstOutcome + secondOutcome))
            {
                Console.Write("Natural pass! ");
                return true;
            }

            if (CrapsTable.puck.IsOn == true && CrapsTable.puck.passPoint == (firstOutcome + secondOutcome))
            {
                Console.Write("Point made! Pass! ");
                return true;
            }
            return false;
        }

        protected override bool MeetsLosingCondition(byte firstOutcome, byte secondOutcome)
        {
            // if point is off, craps loses
            // if point is on, 7 loses

            if (CrapsTable.puck.IsOn == false && losingTotals.Contains(firstOutcome + secondOutcome))
            {
                Console.Write("Crap out! ");
                return true;
            }

            if (CrapsTable.puck.IsOutcomeSevenOut(firstOutcome, secondOutcome))
            {
                Console.Write("Point missed! Seven out! ");
                return true;
            }
            return false;
        }
    }
}
