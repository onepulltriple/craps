using CrapsLibrary;

namespace ConsoleAppForCraps.DealerCLIState
{
    internal class DealerCLIStateCRUDPlayer : DealerCLIState
    {
        public DealerCLIStateCRUDPlayer(DealerCLIStateMachine dealerCLIStateMachine) : base(dealerCLIStateMachine)
        {
            // this.dealerCLIStateMachine = dealerCLIStateMachine; // this is done by the base constructor
        }

        public override void Enter()
        {
            UpdateScreen();

            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Create a player");
            Console.WriteLine("2. Rename a player");
            Console.WriteLine("3. Delete a player");
            Console.WriteLine("4. Add money to a player's purse");

            Console.WriteLine("\n0. Go to overview");

            List<int> listOfAcceptableInts = new List<int>() { 0, 1, 2, 3, 4 };
            PerformTask(ValidateUserInputCLIMenu(listOfAcceptableInts));
        }

        public override void PerformTask(int input)
        {
            switch (input)
            {
                case 1:
                    CreatePlayerCLI();
                    break;

                case 2:
                    RenamePlayerCLI();
                    break;

                case 3:
                    DeletePlayerCLI();
                    break;

                case 4:
                    AddMoneyToPurseCLI();
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


        public override void Exit() { }

        private void RenamePlayerCLI()
        {
            Console.WriteLine("Select the player to rename:");
            Player playerToRename = SelectPlayerCLI();

            string oldName = playerToRename.name;
            playerToRename.name = NamePlayerCLI();

            Console.WriteLine($"\n{oldName} was successfully renamed to {playerToRename.name}.");
            SleepCLI();

            this.Enter();
        }

        private void CreatePlayerCLI()
        {
            string enteredName = NamePlayerCLI();

            Result<bool> outcome = dealerCLIStateMachine.crapsTable!.AddPlayerToNextFreeSlot(new Player(enteredName, 100));

            foreach (string message in outcome.Messages)
                Console.Write(message);

            SleepCLI();

            this.Enter();
        }

        private void AddMoneyToPurseCLI()
        {
            Console.WriteLine("Select the player who shall receive money: ");
            Player playerToFund = SelectPlayerCLI();

            Console.Write("Please enter an amount to credit to the player: ");
            uint amountToCredit = ValidateUserInputUIntCLI();
            playerToFund.Purse += amountToCredit;

            Console.WriteLine($"\n{playerToFund.name} has been credited {amountToCredit} which brings their total purse to {playerToFund.Purse}.");
            SleepCLI();

            this.Enter();
        }

        private string NamePlayerCLI()
        {
            string enteredName = "";
            bool validName = false;

            int maxLength = DealerCLI.columnWidth - 2;

            while (validName == false)
            {
                Console.WriteLine($"Enter player name (max {maxLength} characters):");

                enteredName = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(enteredName))
                {
                    Console.WriteLine("Name cannot be empty.");
                }
                else if (enteredName.Length > maxLength)
                {
                    Console.WriteLine($"Name too long! Max {maxLength} characters.");
                }
                else
                {
                    validName = true;
                }

                if (validName == false)
                {
                    Console.WriteLine("Press any key to try again...");
                    Console.ReadKey();
                }
            }

            return enteredName;
        }

        private void DeletePlayerCLI()
        {
            Console.WriteLine("Select the player to delete:");
            Player playerToRemove = SelectPlayerCLI();

            dealerCLIStateMachine.crapsTable!.RemovePlayer(playerToRemove);

            string oldName = playerToRemove.name;
            Console.WriteLine($"\n{oldName} was successfully deleted.");
            SleepCLI();

            this.Enter();
        }

    }
}
