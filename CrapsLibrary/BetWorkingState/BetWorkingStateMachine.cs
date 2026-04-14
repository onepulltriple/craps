namespace CrapsLibrary.BetWorkingState
{
    internal class BetWorkingStateMachine
    {
        internal BetWorkingState? currentBetWorkingState;

        public BetWorkingStateMachine()
        {
            // the starting state would go here
            //currentBetWorkingState = new BetWorkingStateReturnWinnings(this, null);
            //currentBetWorkingState = new BetWorkingStateReturnWinnings()
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nextBetWorkingState">The next working state of the bet.</param>
        public void ChangeState(BetWorkingState nextBetWorkingState)
        {
            currentBetWorkingState?.Exit();
            currentBetWorkingState = nextBetWorkingState;
            currentBetWorkingState.Enter();
        }
    }
}
