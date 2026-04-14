namespace CrapsLibrary.BetWorkingState
{
    abstract class BetWorkingState
    {
        protected BetWorkingStateMachine betWorkingStateMachine; // the garage

        public BetWorkingState(BetWorkingStateMachine betWorkingStateMachine)
        {
            this.betWorkingStateMachine = betWorkingStateMachine; // one of several cars
        }

        public abstract void Enter();
        public abstract void PerformTask(int input);
        public abstract void Exit();


    }
}
