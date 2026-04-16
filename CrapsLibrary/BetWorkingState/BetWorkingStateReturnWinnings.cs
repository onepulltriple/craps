using CrapsLibrary.Bets;

namespace CrapsLibrary.BetWorkingState
{
    internal class BetWorkingStateReturnWinnings : BetWorkingState
    {
        public BetWorkingStateReturnWinnings(BetWorkingStateMachine betWorkingStateMachine, Bet betInQuestion) : base(betWorkingStateMachine, betInQuestion)
        {
            //this.betWorkingStateMachine = betWorkingStateMachine; // this is done by the base constructor
            //this.betInQuestion = betInQuestion; // this is done by the base constructor
        }

        public override void Enter()
        {
            CrapsTable.scoreboard.NewSubscriber(this.EvaluateBet);
        }

        /// <summary>
        /// Rather than each bet itself being a subscriber, the states of the bet will manage the bet's subscription.
        /// Thus, by virtue of being subscribed or unsubscribed, a bet will become active or paused, respectively.
        /// </summary>
        /// <param name="firstOutcome"></param>
        /// <param name="secondOutcome"></param>
        public override void EvaluateBet(byte firstOutcome, byte secondOutcome)
        {
            if (betInQuestion.MeetsFirstWinningCondition(firstOutcome, secondOutcome))
            {
                Console.WriteLine($"Hooray! {betInQuestion.betOwner.playerName} won {betInQuestion.betName} with {firstOutcome}, {secondOutcome}! The payout was {betInQuestion.payout} credits and goes to {betInQuestion.betOwner.playerName}.");

                betInQuestion.betOwner.purse += betInQuestion.payout;
                return;
            }

            if (betInQuestion.MeetsLosingCondition(firstOutcome, secondOutcome))
            {
                Console.WriteLine($"Ouhr nouhr! {betInQuestion.betOwner.playerName} lost {betInQuestion.betName} with {firstOutcome}, {secondOutcome}! The commitment of {betInQuestion.commitment} credits goes to the house.");

                CrapsTable.scoreboard.Unsubscribe(this.EvaluateBet);
                // Don't subtract commitment here, since that has already been given up when placing the bet.
                betInQuestion.betOwner.playerBetList.Remove(betInQuestion);
            }
        }

        public override void Exit()
        {
            CrapsTable.scoreboard.Unsubscribe(this.EvaluateBet);
        }
    }
}
