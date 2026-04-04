using CrapsLibrary;

namespace ConsoleAppForCraps
{
    public class DealerCLI // translation layer
    {
        const int columnWidth = 20;

        public CrapsTable table00;

        public DealerCLI()
        {
            GreetPlayers();

            // Initialize table (with minimum bet)
            this.table00 = new(5, 5);

            RunTable(table00);
        }

        private void GreetPlayers()
        {
            Console.WriteLine("Hello, Players!");
            Thread.Sleep(700);
        }

        private void RunTable(CrapsTable table00)
        {
            bool isRunning = true;

            while (isRunning)
            {
                UpdateScreen();

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        // data validate, then call AddPlayer()
                        NameNewPlayer(); 
                        break;

                    case "2":
                        // BetFactory.CreateBet();
                        break;

                    case "3":
                        table00.RollDice(6, 6);
                        break;

                    case "4":
                        //RemovePlayer();
                        break;

                    case "5":
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice...");
                        Thread.Sleep(1000);
                        break;
                }
            }
        }

        private void UpdateScreen()
        {
            // show a list of players, their purses, and their bets
            Console.Clear();

            //const int columnWidth = 20;

            var players = table00.Players;

            if (players.Count == 0)
            {
                Console.WriteLine("No players at the table.");
                ShowMenu();
                return;
            }

            // Find max number of bets any player has
            int maxBetCount = players.Max(p => p.playerBetList.Count);

            // Total rows = 1 for name + 1 for purse + maxBets
            int totalRows = 2 + maxBetCount;


            // Present information on screen
            string horizontalBorder = "+" + string.Join("+", players.Select(p => new string('-', columnWidth))) + "+";
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
                            cellText = player.playerBetList[betIndex].ToString();
                        }
                    }

                    Console.Write(" " + cellText.PadRight(columnWidth - 1) + "|");
                }

                Console.WriteLine("");
                Console.WriteLine(horizontalBorder);
            }

            ShowMenu();

        }

        private void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Add a player");
            Console.WriteLine("2. Make a bet");
            Console.WriteLine("3. Roll dice");
            Console.WriteLine("4. Remove a player");
            Console.WriteLine("5. Exit");

            Console.Write("Enter choice: ");
        }

        private void NameNewPlayer()
        {
            string enteredName = "";
            bool validName = false;

            int maxLength = columnWidth - 2;

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

            table00.AddPlayer(new Player(enteredName));
        }


    }
}
