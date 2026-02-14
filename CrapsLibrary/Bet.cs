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

        public void QuitBet()
        {
            betOwner.purse += this.commitment;
            betOwner.playerBetList.Remove(this);
        }

        public void EvaluateBet(byte firstOutcome, byte secondOutcome)
        {
            if (this.isWorking == false)
                return;

            if (this.MeetsFirstWinningCondition(firstOutcome, secondOutcome))
            {
                Console.WriteLine($"Hooray! {betOwner.playerName} won {this.betName} with {firstOutcome}, {secondOutcome}! The payout was {this.payout} credits and goes to {betOwner.playerName}.");
                betOwner.purse += this.payout;
                return;
            }

            if (this.MeetsLosingCondition(firstOutcome, secondOutcome))
            {
                Console.WriteLine($"Ouhr nouhr! {betOwner.playerName} lost {this.betName} with {firstOutcome}, {secondOutcome}! The commitment of {this.commitment} credits goes to the house.");
                CrapsTable.scoreboard.Unsubscribe(this.EvaluateBet);
                // Don't subtract commitment here, since that has already been given up when placing the bet.
                betOwner.playerBetList.Remove(this);
            }
        }

        protected abstract bool MeetsLosingCondition(byte firstOutcome, byte secondOutcome);

        protected abstract bool MeetsFirstWinningCondition(byte firstOutcome, byte secondOutcome);
    }
}
