namespace ConsoleAppForCraps.DealerCLIState
{
    internal class DealerCLIStateMachine
    {
        private DealerCLIState currentDealerCLIState;

        public DealerCLIStateMachine()
        {
            // the starting state would go here
        }

        public void ChangeState(DealerCLIState nextDealerCLIState)
        {
            currentDealerCLIState?.Exit(); // if this object is not null, then call this method
            currentDealerCLIState = nextDealerCLIState;
            currentDealerCLIState.Enter();
        }
    }
}
