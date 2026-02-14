namespace CrapsLibrary
{
    public class HardWayBet : Bet, IBet
    {
        int winningHalf;

        public HardWayBet(Player betOwner, string betName, uint commitment, List<int> winningTotals, uint payout)
            : base(betOwner, betName, commitment, winningTotals, payout)
        {
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
                betOwner.purse += this.payout;
                return;
            }
            
            if (this.MeetsLosingCondition(firstOutcome, secondOutcome))
            {
                Console.WriteLine($"Ouhr nouhr! I lost a {this.betName} with {firstOutcome}, {secondOutcome}!");
                CrapsTable.scoreboard.Unsubscribe(this.EvaluateBet);
                betOwner.workingBets.Remove(this);
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
