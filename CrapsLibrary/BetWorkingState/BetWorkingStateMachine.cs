namespace CrapsLibrary.BetWorkingState
{
    internal class BetWorkingStateMachine
    {
        internal BetWorkingState? currentBetWorkingState;

        public BetWorkingStateMachine()
        {
            // the starting state would go here
            currentBetWorkingState = new BetWorkingStateReturnWinnings(this);
        }

        public void ChangeState(BetWorkingState nextBetWorkingState)
        {
            currentBetWorkingState?.Exit();
            currentBetWorkingState = nextBetWorkingState;
            currentBetWorkingState.Enter();
        }
    }
}
