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
            //ShowScoreboard();
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
            (byte outcome01, byte outcome02) = DealerCLI.crapsTable.RollDice(6, 6);

            DealerCLI.crapsTable.UpdateScoreboardAndPublishOutcomes(outcome01, outcome02);

            SleepCLI();

            this.Enter();
        }

        public override void Exit()
        {
            
        }
    }
}
