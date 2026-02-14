namespace CrapsLibrary
{
    public class PlaceBet : Bet, IBet
    {
        public PlaceBet(string betName, uint amount, List<int> winningTotals) 
            : base(betName, amount, winningTotals)
        {

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
            // the puck is on and the roll results in a board number
            // (betting directly on the point number is prohibited)
            if (CrapsTable.puck.IsOn == true && this.winningTotals.Contains(firstOutcome + secondOutcome))
            {
                return true;
            }
            return false;
        }

        public bool MeetsLosingCondition(byte firstOutcome, byte secondOutcome)
        {
            // the puck is on and then a seven is rolled
            if (CrapsTable.puck.IsOn == true && CrapsTable.puck.seven == (firstOutcome + secondOutcome))
            {
                return true;
            }
            return false;
        }
    }
}
