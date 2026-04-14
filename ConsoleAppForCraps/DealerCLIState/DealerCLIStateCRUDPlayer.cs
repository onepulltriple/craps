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
            Console.WriteLine("\n0. Go to overview"); //?

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


        public override void Exit()
        {
            ;
        }

        private void RenamePlayerCLI()
        {
            Console.WriteLine("Select the player to rename:");
            Player playerToRename = SelectPlayerCLI();

            string oldName = playerToRename.playerName;
            playerToRename.playerName = NamePlayerCLI();

            Console.WriteLine($"\n{oldName} was successfully renamed to {playerToRename.playerName}.");
            SleepCLI();

            this.Enter();
        }


        private void CreatePlayerCLI()
        {
            string enteredName = NamePlayerCLI();

            DealerCLI.crapsTable.AddPlayer(new Player(enteredName, 100));

            Console.WriteLine($"\n{enteredName} was successfully created.");
            SleepCLI();

            this.Enter();
        }

        private void AddMoneyToPurseCLI()
        {
            Console.WriteLine("Select the player who shall receive money:");
            Player playerToFund = SelectPlayerCLI();

            Console.Write("Please enter an amount to credit to the player (enter a positive whole number or 0 to abort): ");
            uint amountToCredit = ValidateUserInputUInt();
            playerToFund.purse += amountToCredit;

            Console.WriteLine($"\n{playerToFund.playerName} has been credited {amountToCredit} which brings their total purse to {playerToFund.purse}.");
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

            DealerCLI.crapsTable.RemovePlayer(playerToRemove);

            string oldName = playerToRemove.playerName;
            Console.WriteLine($"\n{oldName} was successfully deleted.");
            SleepCLI();

            this.Enter();
        }

    }
}
