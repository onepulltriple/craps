using ConsoleAppForCraps.DealerCLIState;
using CrapsLibrary;

namespace ConsoleAppForCraps
{
    public class DealerCLI // translation layer between the CrapsLibrary and the CLI
    {
        public const int columnWidth = 20;

        public static CrapsTable crapsTable = new (5, 5);

        public const int sleepDurationMilliseconds = 700;

        internal DealerCLIStateMachine dealerCLIStateMachine;

        public DealerCLI()
        {
            this.dealerCLIStateMachine = new();
            dealerCLIStateMachine.ChangeState(new DealerCLIStateOverview(this.dealerCLIStateMachine));
        }


    }
}
