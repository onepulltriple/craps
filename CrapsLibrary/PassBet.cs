
namespace CrapsLibrary
{
    public class PassBet : Bet, IBet
    {
        List<int> losingTotals;

        public PassBet(string betName, List<int> winningTotals, int payoutNumerator, int payoutDenominator)
            : base(betName, winningTotals, payoutNumerator, payoutDenominator)
        {
            losingTotals = new List<int> {2, 3, 12}; // crap out/missout
            //winningTotals = new List<int> {7, 11 };  // natural pass
            CrapsTable.scoreboard.NewSubscriber(this.EvaluateBet);
        }

        public void EvaluateBet(int firstOutcome, int secondOutcome)
        {
            if (this.isWorking == false)
                return;

            if (this.MeetsFirstWinningCondition(firstOutcome, secondOutcome))
            {
                Console.WriteLine($"Hooray! I won {this.betName} with {firstOutcome}, {secondOutcome}!");
                return;
            }

            if (this.MeetsLosingCondition(firstOutcome, secondOutcome))
            {
                Console.WriteLine($"Ouhr nouhr! I lost {this.betName} with {firstOutcome}, {secondOutcome}!");
                CrapsTable.scoreboard.Unsubscribe(this.EvaluateBet);

                this.isWorking = false;  // it may be okay to delete this later, since I may only need it for the testing putposes in the console app
            }
        }

        public bool MeetsFirstWinningCondition(int firstOutcome, int secondOutcome)
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

        public bool MeetsLosingCondition(int firstOutcome, int secondOutcome)
        {
            // if point is off, craps loses
            // if point is on, 7 loses

            if (CrapsTable.puck.IsOn == false && losingTotals.Contains(firstOutcome + secondOutcome))
            {
                Console.Write("Crap out! ");
                return true;
            }

            if (CrapsTable.puck.IsOn == true && CrapsTable.puck.seven == (firstOutcome + secondOutcome))
            {
                Console.Write("Point missed! Seven out! ");
                return true;
            }
            return false;
        }
    }
}
