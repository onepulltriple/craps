
using CrapsLibrary;

namespace ConsoleAppForCraps.DealerCLIState
{
    internal class DealerCLIStateCRUDBet : DealerCLIState
    {
        public DealerCLIStateCRUDBet(DealerCLIStateMachine dealerCLIStateMachine) : base(dealerCLIStateMachine)
        {
            // this.dealerCLIStateMachine = dealerCLIStateMachine;
        }

        public override void Enter()
        {
            UpdateScreen();

            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Place a bet");
            Console.WriteLine("2. Call a bet off (pause)");
            Console.WriteLine("3. Take down a bet (remove completely)");
            Console.WriteLine("\n0. Go to overview"); //?

            List<int> listOfAcceptableInts = new List<int>() { 0, 1, 2, 3 };
            PerformTask(ValidateUserInputCLIMenu(listOfAcceptableInts));
        }

        public override void PerformTask(int input)
        {
            switch (input)
            {
                case 1:
                    CreateBetCLI();
                    break;

                case 2:
                    PauseBetCLI();
                    break;

                case 3:
                    DeleteBetCLI();
                    break;

                case 0:
                    // Change state to Overview
                    dealerCLIStateMachine.ChangeState(new DealerCLIStateOverview(dealerCLIStateMachine));
                    break;

                default:
                    ;
                    break;
            }
        }

        private void CreateBetCLI()
        {
            Console.WriteLine("Select the player who will be placing the bet:");
            Player betPlacer = SelectPlayerCLI();

            Console.WriteLine("Select the type of bet to be placed:");
            betType selectedBetType = SelectBetFromFactoryCLI();

            // Add info here to specify the minimum amount to bet based on the table minimum. Is that already shown in the BetFactory?
            Console.Write("Please enter an amount to bet (enter a positive whole number or 0 to abort): ");
            uint amountThrownAtBet = ValidateUserInputUInt();

            // somewhere there needs to be info for the player about the table minimum, and something should happen if they give bogus input
            // this info should be UI agnostic

            Bet? newBet = CrapsTable.betFactory.CreateBet(betPlacer, selectedBetType, amountThrownAtBet);

            if (newBet != null)
                betPlacer.playerBetList.Add(newBet);

            Console.WriteLine($"\n{betPlacer.playerName} has bet...");
            SleepCLI();

            this.Enter();
        }

        private void PauseBetCLI()
        {
            throw new NotImplementedException();
        }

        private void DeleteBetCLI()
        {
            throw new NotImplementedException();
        }

        public override void Exit()
        {
            
        }
    }
}
