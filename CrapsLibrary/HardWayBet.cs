namespace CrapsLibrary
{
    public class HardWayBet : Bet, IBet
    {
        int winningHalf;

        uint minimumBetAmount;

        public HardWayBet(string betName, uint amount, List<int> winningTotals)
            :base(betName, amount, winningTotals)
        {
            minimumBetAmount = CrapsTable.tableMinimum / 5;
            this.winningHalf = winningTotals.First() / 2;
            CrapsTable.scoreboard.NewSubscriber(this.EvaluateBet);
        }

        public void EvaluateBet(byte firstOutcome, byte secondOutcome) 
        {
            if (this.isWorking == false)
                return;

            if (this.MeetsFirstWinningCondition(firstOutcome, secondOutcome))
            {
                Console.WriteLine($"Hooray! I won a {this.betName} with {firstOutcome}, {secondOutcome}!");
                return;
            }
            
            if (this.MeetsLosingCondition(firstOutcome, secondOutcome))
            {
                Console.WriteLine($"Ouhr nouhr! I lost a {this.betName} with {firstOutcome}, {secondOutcome}!");
                CrapsTable.scoreboard.Unsubscribe(this.EvaluateBet);
                this.isWorking = false;
            }
        }

        public bool MeetsFirstWinningCondition(byte firstOutcome, byte secondOutcome)
        {
            if (firstOutcome == winningHalf && secondOutcome == winningHalf)
            {
                return true;
            }
            return false;
        }

        public bool MeetsLosingCondition(byte firstOutcome, byte secondOutcome)
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
