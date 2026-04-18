using ConsoleAppForCraps.DealerCLIState;

namespace ConsoleAppForCraps
{
    public class DealerCLI // translation layer between the CrapsLibrary and the CLI
    {
        public const int columnWidth = 20;
        public const int gameFeedHeight = 12;
        public const int gameFeedWidth = 60;

        public const int sleepDurationMilliseconds = 700;

        internal DealerCLIStateMachine dealerCLIStateMachine;

        public DealerCLI()
        {
            this.dealerCLIStateMachine = new();
            dealerCLIStateMachine.ChangeState(new DealerCLIStateInitialState(this.dealerCLIStateMachine));
        }


    }
}
