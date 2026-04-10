namespace ConsoleAppForCraps.DealerCLIState
{
    internal class DealerCLIStateInitialState : DealerCLIState
    {
        public DealerCLIStateInitialState(DealerCLIStateMachine dealerCLIStateMachine) : base(dealerCLIStateMachine)
        {
            // this.dealerCLIStateMachine = dealerCLIStateMachine;
        }

        public override void Enter()
        {
            ;
        }

        public override void PerformTask(int input)
        {
            ;
        }

        public override void Exit()
        {
            Console.WriteLine("Let's go gambling!");
            SleepCLI();
        }
    }
}
