using CrapsLibrary.Bets;

namespace CrapsLibrary.BetWorkingState
{
    abstract class BetWorkingState
    {
        // Rather than each bet itself being a subscriber, the states of the bet will manage the bet's subscription.
        // Thus, by virtue of a bet's state being subscribed or unsubscribed, a bet will become active, paused, lost, etc.
        protected BetWorkingStateMachine betWorkingStateMachine; 

        protected Bet betInQuestion;

        public abstract string Name { get; }

        /// <summary>
        /// Constructor for one of a bet's working states.
        /// </summary>
        /// <param name="betWorkingStateMachine">The state machine which manages the bet's states.</param>
        /// <param name="betInQuestion">The bet whose state will be managed.</param>
        public BetWorkingState(BetWorkingStateMachine betWorkingStateMachine, Bet betInQuestion)
        {
            this.betWorkingStateMachine = betWorkingStateMachine; // one of the many possible working states for the a bet
            this.betInQuestion = betInQuestion;
        }

        public abstract void Enter();
        public abstract void EvaluateBet(byte firstOutcome, byte secondOutcome);
        public abstract void Exit();

        protected void AnnounceReturnWinnings(byte firstOutcome, byte secondOutcome)
        {
            betWorkingStateMachine.crapsTable.gameEventFeed.Add(
                $"Hooray! {betInQuestion.betOwner.name} " +
                $"won {betInQuestion.name} " +
                $"with {firstOutcome}, {secondOutcome}! " +
                $"The payout was {betInQuestion.payout} " +
                $"credits and goes to {betInQuestion.betOwner.name}.",
                GameEventType.Message
                );
        }

        protected void AnnounceFullParlay(byte firstOutcome, byte secondOutcome)
        {
            betWorkingStateMachine.crapsTable.gameEventFeed.Add(
                $"Oh boy... {betInQuestion.betOwner.name} " +
                $"won {betInQuestion.name} " +
                $"with {firstOutcome}, {secondOutcome}! " +
                $"The payout of {betInQuestion.Commitment} credits will be fully parlayed.",
                GameEventType.Message
                );
        }

        protected void AnnounceLost()
        {
            betWorkingStateMachine.crapsTable.gameEventFeed.Add(
                $"Oh no! {betInQuestion.betOwner.name} " +
                $"lost {betInQuestion.name}! " +
                $"The commitment of {betInQuestion.Commitment} credits goes to the house.",
                GameEventType.Message
                );
        }

        protected void AnnouncePaused()
        {
            betWorkingStateMachine.crapsTable.gameEventFeed.Add(
                $"{betInQuestion.betOwner.name} has paused " +
                $"the bet {betInQuestion.name}!",
                GameEventType.Message
                );
        }

        //protected void AnnounceTaunt()
        //{
        //    betWorkingStateMachine.crapsTable.gameEventFeed.Add(
        //        $"Uuuuuu... Since {betInQuestion.betOwner.playerName} has paused " +
        //        $"the bet {betInQuestion.betName}," +
        //        $"they didn't !",
        //        GameEventType.Message
        //        );
        //}
    }
}
