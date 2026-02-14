using System.Numerics;

namespace CrapsLibrary
{
    public abstract class Bet
    {
        public Player betOwner;
        
        public string betName;

        public uint commitment;

        public List<int> winningTotals;

        public uint payout;

        public bool isWorking;

        public Bet(Player betOwner, string betName, uint commitment, List<int> winningTotals, uint payout)
        {
            this.betOwner = betOwner;
            this.betName = betName;
            this.commitment = commitment;
            this.winningTotals = winningTotals;
            this.payout = payout;
            this.StartWorking(); // activates bet upon creation
        }

        public void StartWorking()
        {
            this.isWorking = true;
        }

        public void QuitWorking()
        {
            this.isWorking = false;
        }

        public void EvaluateBet(byte firstOutcome, byte secondOutcome)
        {
            if (this.isWorking == false)
                return;

            if (this.MeetsFirstWinningCondition(firstOutcome, secondOutcome))
            {
                Console.WriteLine($"Hooray! I won {this.betName} with {firstOutcome}, {secondOutcome}!");
                betOwner.purse += this.payout;
                return;
            }

            if (this.MeetsLosingCondition(firstOutcome, secondOutcome))
            {
                Console.WriteLine($"Ouhr nouhr! I lost {this.betName} with {firstOutcome}, {secondOutcome}!");
                CrapsTable.scoreboard.Unsubscribe(this.EvaluateBet);
                betOwner.workingBets.Remove(this);
                this.isWorking = false;  // it may be okay to delete this later, since I may only need it for the testing putposes in the console app
            }
        }

        protected abstract bool MeetsLosingCondition(byte firstOutcome, byte secondOutcome);

        protected abstract bool MeetsFirstWinningCondition(byte firstOutcome, byte secondOutcome);
    }
}
