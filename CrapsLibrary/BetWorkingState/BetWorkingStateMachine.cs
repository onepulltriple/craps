using CrapsLibrary.Bets;
namespace CrapsLibrary.BetWorkingState
{
    internal class BetWorkingStateMachine
    {
        public CrapsTable crapsTable;

        internal BetWorkingState? currentBetWorkingState;

        public string CurrentStateName => currentBetWorkingState?.Name ?? "";

        public BetWorkingStateMachine(CrapsTable crapsTable)
        {
            // the starting state would go here
            //currentBetWorkingState = new BetWorkingStateReturnWinnings(this, null);
            //currentBetWorkingState = new BetWorkingStateReturnWinnings()

            this.crapsTable = crapsTable;
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
