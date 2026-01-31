namespace CrapsLibrary
{
    public class HardWayBet : Bet
    {
        int winningHalf;
        public HardWayBet(string betName, int winningTotal, int payoutNumerator, int payoutDenominator)
            :base(betName, winningTotal, payoutNumerator, payoutDenominator)
        {
            this.winningHalf = winningTotal / 2;
            CrapsTable.scoreboard.NewSubscriber(this.EvaluateBet);
        }

        public bool MeetsWinningCondition01(int firstOutcome, int secondOutcome)
        {
            if (firstOutcome == winningHalf && secondOutcome == winningHalf)
            {
                return true;
            }
            return false;
        }

        public bool MeetsLosingCondition01(int firstOutcome, int secondOutcome)
        {
            if((firstOutcome + secondOutcome == this.winningTotal) &&
                (firstOutcome != winningHalf || secondOutcome != winningHalf))
            {
                return true;
            }
            return false;
        }

        public void EvaluateBet(int outcome01, int outcome02) 
        {
            if (this.isWorking == false)
            {
                return;
            }

            if(this.MeetsWinningCondition01(outcome01, outcome02))
            {
                Console.WriteLine($"Hooray! I won with a {outcome01}, {outcome02}!");
            }
            
            if(this.MeetsLosingCondition01(outcome01, outcome02))
            {
                Console.WriteLine($"Fuck! I lost with a {outcome01}, {outcome02}!");
                CrapsTable.scoreboard.Unsubscribe(this.EvaluateBet);
            }
        }

        // code some way to turn off bet (switch to not working) upon player request
    }
}
