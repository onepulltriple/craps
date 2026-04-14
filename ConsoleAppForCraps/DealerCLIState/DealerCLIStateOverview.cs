using CrapsLibrary;

namespace ConsoleAppForCraps.DealerCLIState
{
    internal class DealerCLIStateOverview : DealerCLIState
    {
        public DealerCLIStateOverview(DealerCLIStateMachine dealerCLIStateMachine) : base(dealerCLIStateMachine)
        {
            // this.dealerCLIStateMachine = dealerCLIStateMachine;
        }

        public override void Enter()
        {
            UpdateScreen();

            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Manage players and/or their purses");
            Console.WriteLine("2. Manage a player's bets");
            Console.WriteLine("3. Roll dice");
            Console.WriteLine("4. -----");
            Console.WriteLine("\n0. Quit game");

            List<int> listOfAcceptableInts = new List<int>() { 0, 1, 2, 3, 4 };
            PerformTask(ValidateUserInputCLIMenu(listOfAcceptableInts));
        }

        public override void PerformTask(int input)
        {
            switch (input)
            {
                case 1:
                    // change to CRUDPlayer state
                    dealerCLIStateMachine.ChangeState(new DealerCLIStateCRUDPlayer(dealerCLIStateMachine));
                    break;

                case 2:
                    // change to CRUDBet state
                    dealerCLIStateMachine.ChangeState(new DealerCLIStateCRUDBet(dealerCLIStateMachine));
                    break;

                case 3:
                    ;
                    break;

                case 4:
                    ;
                    break;

                case 0:
                    Environment.Exit(0);
                    break;

                default:
                    ;
                    break;
            }
        }

        public override void Exit()
        {
            
        }

    }
}
