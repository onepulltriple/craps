namespace CrapsLibrary
{
    public class HardWayBet : Bet, IBet
    {
        int winningHalf;

        public HardWayBet(string betName, List<int> winningTotals, int payoutNumerator, int payoutDenominator)
            :base(betName, winningTotals, payoutNumerator, payoutDenominator)
        {
            this.winningHalf = winningTotals.First() / 2;
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
            if (firstOutcome == winningHalf && secondOutcome == winningHalf)
            {
                return true;
            }
            return false;
        }

        public bool MeetsLosingCondition(int firstOutcome, int secondOutcome)
        {
            if ((firstOutcome + secondOutcome == this.winningTotals.First()) &&
                (firstOutcome != winningHalf || secondOutcome != winningHalf))
            {
                return true;
            }
            return false;
        }

    }
}
