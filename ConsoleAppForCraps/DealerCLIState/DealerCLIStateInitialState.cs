namespace ConsoleAppForCraps.DealerCLIState
{
    internal class DealerCLIStateInitialState : DealerCLIState
    {
        public DealerCLIStateInitialState(DealerCLIStateMachine dealerCLIStateMachine) : base(dealerCLIStateMachine)
        {
            // this.dealerCLIStateMachine = dealerCLIStateMachine;
            //dealerCLIStateMachine.crapsTable;
        }

        public override void Enter()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Set the minimum bet amount for the craps table.");
            
            Console.WriteLine("\n0. Quit");

            List<int> listOfAcceptableInts = new List<int>() { 0, 1 };
            PerformTask(ValidateUserInputCLIMenu(listOfAcceptableInts));
        }

        public override void PerformTask(int input)
        {
            switch (input)
            {
                case 1:
                    uint tableMinimum = SetCrapsTableMinimum();
                    dealerCLIStateMachine.crapsTable = new(tableMinimum, tableMinimum * 1000); 
                    // TODO handle table max properly

                    dealerCLIStateMachine.ChangeState(new DealerCLIStateOverview(dealerCLIStateMachine));
                    break;

                case 0:
                    Environment.Exit(0);
                    break;

                default:
                    break;
            }
        }

        public override void Exit()
        {
            Console.WriteLine("\nLet's go gambling!");
            SleepCLI();
        }
    }
}
