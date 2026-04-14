namespace CrapsLibrary.BetWorkingState
{
    abstract class BetWorkingState
    {
        protected BetWorkingStateMachine betWorkingStateMachine; // the garage

        protected Bet betInQuestion;

        public BetWorkingState(BetWorkingStateMachine betWorkingStateMachine, Bet betInQuestion)
        {
            this.betWorkingStateMachine = betWorkingStateMachine; // one of several cars
            this.betInQuestion = betInQuestion;
        }

        public abstract void Enter();
        public abstract void EvaluateBet(byte firstOutcome, byte secondOutcome);
        public abstract void Exit();


    }
}
