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
            Console.Clear();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Create a player");
            Console.WriteLine("2. Rename a player");
            Console.WriteLine("3. Delete a player");
            Console.WriteLine("4. Go back"); //?

            List<int> listOfAcceptableInts = new List<int>() { 1, 2, 3, 4 };
            PerformTask(ValidateUserInput(listOfAcceptableInts));
        }

        public override void PerformTask(int input)
        {
            switch (input)
            {
                case 1:
                    CreatePlayer();
                    break;

                case 2:
                    RenamePlayer();
                    break;

                case 3:
                    DeletePLayer();
                    break;

                case 4:
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

        }

        private void RenamePlayer()
        {
            Console.WriteLine("Select the player to rename:");
            Player playerToRename = SelectPlayer();

            string oldName = playerToRename.playerName;
            playerToRename.playerName = NamePlayer();

            Console.WriteLine($"\n{oldName} was successfully renamed to {playerToRename.playerName}.");
            Thread.Sleep(700);

            this.Enter();
        }

        private Player SelectPlayer()
        {
            List<int> listOfAcceptableInts = new();

            var players = DealerCLI.crapsTable.Players;

            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {players[i].playerName}");
                listOfAcceptableInts.Add(i + 1);
            }

            return players[ValidateUserInput(listOfAcceptableInts) - 1]; ;
        }

        private void CreatePlayer()
        {
            string enteredName = NamePlayer();

            DealerCLI.crapsTable.AddPlayer(new Player(enteredName));

            Console.WriteLine($"\n{enteredName} was successfully created.");
            Thread.Sleep(700);

            this.Enter();
        }

        private string NamePlayer()
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

        private void DeletePLayer()
        {
            Console.WriteLine("Select the player to delete:");
            Player playerToRemove = SelectPlayer();

            DealerCLI.crapsTable.RemovePlayer(playerToRemove);

            string oldName = playerToRemove.playerName;
            Console.WriteLine($"\n{oldName} was successfully deleted.");
            Thread.Sleep(700);

            this.Enter();
        }

    }
}
