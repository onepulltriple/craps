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
            Console.Clear();
            UpdateScreen();

            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Manage players");
            Console.WriteLine("2. Roll dice");
            Console.WriteLine("3. Update a player");
            Console.WriteLine("4. Delete a player");
            Console.WriteLine("5. Quit game");

            List<int> listOfAcceptableInts = new List<int>() { 1, 2, 3, 4, 5 };
            PerformTask(ValidateUserInput(listOfAcceptableInts));
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
                    ;
                    break;

                case 3:
                    ;
                    break;

                case 4:
                    ;
                    break;

                case 5:
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

        /// <summary>
        /// Show a list of players, their purses, and their bets
        /// </summary>
        private void UpdateScreen()
        {
            var players = DealerCLI.crapsTable.Players;

            if (players.Count == 0)
            {
                Console.WriteLine("No players at the table.");
                return;
            }

            // Find max number of bets any player has
            int maxBetCount = players.Max(p => p.playerBetList.Count);

            // Total rows = 1 for name + 1 for purse + maxBets
            int totalRows = 2 + maxBetCount;


            // Present information on screen
            string horizontalBorder = "+" + string.Join("+", players.Select(p => new string('-', DealerCLI.columnWidth))) + "+";
            Console.WriteLine(horizontalBorder);

            for (int row = 0; row < totalRows; row++)
            {
                Console.Write("|");

                foreach (var player in players)
                {
                    string cellText = "";

                    if (row == 0)
                    {
                        cellText = player.playerName;
                    }
                    else if (row == 1)
                    {
                        cellText = player.purse.ToString();
                    }
                    else
                    {
                        int betIndex = row - 2;

                        if (betIndex < player.playerBetList.Count)
                        {
                            cellText = player.playerBetList[betIndex]?.ToString() ?? "";
                        }
                    }

                    Console.Write(" " + cellText.PadRight(DealerCLI.columnWidth - 1) + "|");
                }

                Console.WriteLine("");
                Console.WriteLine(horizontalBorder);
            }
        }



    }
}
