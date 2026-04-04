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
            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Create a player");
            Console.WriteLine("2. Read a player");
            Console.WriteLine("3. Update a player");
            Console.WriteLine("4. Delete a player");
            Console.WriteLine("5. Go back"); //?
            
            PerformTask(ValidateUserInput());
        }

        public override void PerformTask(int input)
        {
            switch (input)
            {
                case 1:
                    NameNewPlayer();
                    break;

                case 2:
                    ;
                    break;

                case 3:
                    ;
                    break;

                case 4:
                    ;
                    break;

                case 5:
                    ;
                    break;

                default:
                    ;
                    break;
            }
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }

        private void NameNewPlayer()
        {
            string enteredName = "";
            bool validName = false;

            int maxLength = DealerCLI.columnWidth - 2;

            while (validName == false)
            {
                Console.Clear();
                Console.WriteLine($"Enter player name (max {maxLength} characters):");

                enteredName = Console.ReadLine();

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

            DealerCLI.crapsTable.AddPlayer(new Player(enteredName));
        }

    }
}
