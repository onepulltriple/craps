using CrapsLibrary;

namespace ConsoleAppForCraps.DealerCLIState
{
    internal class DealerCLIStateRollDice : DealerCLIState
    {
        public DealerCLIStateRollDice(DealerCLIStateMachine dealerCLIStateMachine) : base(dealerCLIStateMachine)
        {
            // this.dealerCLIStateMachine = dealerCLIStateMachine;
        }

        public override void Enter()
        {
            UpdateScreen();
            RenderGameFeedCLI();

            Console.WriteLine("Ready to roll?");
            Console.WriteLine("1: Roll dice!");
            Console.WriteLine("\n0. Return to overview");

            List<int> listOfAcceptableInts = new List<int>() { 0, 1 };
            PerformTask(ValidateUserInputCLIMenu(listOfAcceptableInts));
        }

        public override void PerformTask(int input)
        {
            switch (input)
            {
                case 1:
                    RollDiceCLI();
                    break;

                case 0:
                    // Change state to overview
                    dealerCLIStateMachine.ChangeState(new DealerCLIStateOverview(dealerCLIStateMachine));
                    break;

                default:
                    ;
                    break;
            }
        }

        private void RollDiceCLI()
        {
            //(byte outcome01, byte outcome02) = dealerCLIStateMachine.crapsTable!.RollDice(6, 6);
            (byte outcome01, byte outcome02) = Die.RollDice(6, 6);

            //dealerCLIStateMachine.crapsTable!.UpdateScoreboardAndPublishOutcomes(outcome01, outcome02);
            dealerCLIStateMachine.crapsTable!.scoreboard
                .UpdateScoreboardAndPublishOutcomes(dealerCLIStateMachine.crapsTable, outcome01, outcome02);

            SleepCLI();

            this.Enter();
        }

        public override void Exit()
        {
            
        }
    }
}
