using CrapsLibrary;

namespace ConsoleAppForCraps.DealerCLIState
{
    internal class DealerCLIStateMachine
    {
        public CrapsTable? crapsTable;

        internal DealerCLIState? currentDealerCLIState;

        public DealerCLIStateMachine()
        {
            // the starting state could go here
        }

        public void ChangeState(DealerCLIState nextDealerCLIState)
        {
            currentDealerCLIState?.Exit(); // if this object is not null, then call this method
            currentDealerCLIState = nextDealerCLIState;
            currentDealerCLIState.Enter();
        }
    }
}
