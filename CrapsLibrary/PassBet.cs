
namespace CrapsLibrary
{
    public class PassBet : Bet, IBet
    {
        List<int> losingTotals;

        public PassBet(string betName, List<int> winningTotals, int payoutNumerator, int payoutDenominator)
            : base(betName, winningTotals, payoutNumerator, payoutDenominator)
        {
            losingTotals = new List<int>{2, 3, 12};
            CrapsTable.scoreboard.NewSubscriber(this.EvaluateBet);
        }

        public void EvaluateBet(int firstOutcome, int secondOutcome)
        {
            if (this.isWorking == false)
                return;

            if (this.MeetsFirstWinningCondition(firstOutcome, secondOutcome))
            {
                Console.WriteLine($"Hooray! I won a {this.betName} with {firstOutcome}, {secondOutcome}!");
            }

            if (this.MeetsLosingCondition(firstOutcome, secondOutcome))
            {
                Console.WriteLine($"Ouhr nouhr! I lost a {this.betName} with {firstOutcome}, {secondOutcome}!");
                CrapsTable.scoreboard.Unsubscribe(this.EvaluateBet);
            }
        }

        public bool MeetsFirstWinningCondition(int firstOutcome, int secondOutcome)
        {
            if (winningTotals.Contains(firstOutcome + secondOutcome))
            {
                return true;
            }
            return false;
        }

        public bool MeetsLosingCondition(int firstOutcome, int secondOutcome)
        {
            if (losingTotals.Contains(firstOutcome + secondOutcome))
            {
                return true;
            }
            return false;
        }
    }
}
