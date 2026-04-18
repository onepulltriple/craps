
using CrapsLibrary;
using CrapsLibrary.Bets;
using System.Numerics;

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

            Console.Write("Please enter an amount to bet (enter a positive whole number or 0 to abort): ");
            uint amountThrownAtBet = ValidateUserInputUInt();

            // somewhere there needs to be info for the player about the table minimum, and something should happen if they give bogus input

            Result<Bet> newBetResult = BetFactory.CreateBet(dealerCLIStateMachine.crapsTable!, betPlacer, selectedBetType, amountThrownAtBet);

            if (newBetResult.Success)
            {
                betPlacer.playerBetList.Add(newBetResult.Value);
            }

            foreach (string message in newBetResult.Messages)
            {
                Console.WriteLine(message);
            }

            Console.ReadKey(); // TODO figure out how to handle these error messages (add loop so the user doesn't have to start from the beginning each time)
            //SleepCLI();

            this.Enter();
        }

        private void PauseBetCLI()
        {
            Console.WriteLine("Select the player whose bets will be paused");
            Player betPauser = SelectPlayerCLI();

            Console.WriteLine("Select the bet to be paused:");
            Bet betToPause = SelectBetFromPlayerCLI(betPauser);

            betToPause.PauseBet();

            Console.WriteLine($"\n{betPauser.playerName} has paused their {betToPause.betName}.");
            SleepCLI();

            this.Enter();
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
