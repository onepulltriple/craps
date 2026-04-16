using CrapsLibrary.Bets;

namespace CrapsLibrary.BetWorkingState
{
    abstract class BetWorkingState
    {
        protected BetWorkingStateMachine betWorkingStateMachine; // the container to hold a bet's working state

        protected Bet betInQuestion;

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


    }
}
