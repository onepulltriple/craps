using CrapsLibrary;
using CrapsLibrary.Bets;
using System.Transactions;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleAppForCraps.DealerCLIState
{
    abstract class DealerCLIState
    {
        protected DealerCLIStateMachine dealerCLIStateMachine; // the garage

        public DealerCLIState(DealerCLIStateMachine dealerCLIStateMachine)
        {
            this.dealerCLIStateMachine = dealerCLIStateMachine; // one of several cars
        }

        public abstract void Enter();
        public abstract void PerformTask(int input);
        public abstract void Exit();

        public int ValidateUserInputCLIMenu(List<int> listOfAcceptableInts)
        {
            bool isInt;
            int result;

            do {
                Console.Write("\nPlease enter a choice: ");
                string? input = Console.ReadLine();
                isInt = int.TryParse(input, out result);

            } while (!(isInt && listOfAcceptableInts.Contains(result)));

            return result;
        }

        public uint ValidateUserInputUInt()
        {
            bool isUInt;
            uint result;

            do
            {
                //Console.Write("Please enter an amount to credit to the player (enter a positive whole number or 0 to abort): ");
                string? input = Console.ReadLine();
                isUInt = uint.TryParse(input, out result);

            } while (!isUInt);

            return result;
        }

        public void SleepCLI(int milliseconds = DealerCLI.sleepDurationMilliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        protected Player SelectPlayerCLI()
        {
            List<int> listOfAcceptableInts = new();

            var players = DealerCLI.crapsTable.Players;

            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {players[i].playerName}");
                listOfAcceptableInts.Add(i + 1);
            }

            return players[ValidateUserInputCLIMenu(listOfAcceptableInts) - 1];
        }

        protected betType SelectBetFromFactoryCLI()
        {
            List<int> listOfAcceptableInts = new();

            var bets = BetFactory.betPayoutRatios.ToList();

            for (int i = 0; i < bets.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {bets[i].Key} (payout ratio {bets[i].Value.payoutNumerator}:{bets[i].Value.payoutDenominator})");
                listOfAcceptableInts.Add(i + 1);
            }

            return bets[ValidateUserInputCLIMenu(listOfAcceptableInts) - 1].Key;
        }

        protected Bet SelectBetFromPlayerCLI(Player player)
        {
            List<int> listOfAcceptableInts = new();

            var betsOfPlayer = player.playerBetList;

            for (int i = 0; i < betsOfPlayer.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {betsOfPlayer[i].betName}");
                listOfAcceptableInts.Add(i + 1);
            }

            return betsOfPlayer[ValidateUserInputCLIMenu(listOfAcceptableInts) - 1];
        }

        /// <summary>
        /// Show a list of players, their purses, and their bets
        /// </summary>
        protected void UpdateScreen()
        {
            Console.Clear();

            var players = DealerCLI.crapsTable.Players;

            if (players.Count == 0)
            {
                Console.WriteLine("No players at the table.\n\n");
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
                            cellText = player.playerBetList[betIndex]?.betName.ToString() ?? "";
                        }
                        // if bet state is paused, add " (OFF)" to the end of the cell text
                    }

                    Console.Write(" " + cellText.PadRight(DealerCLI.columnWidth - 1) + "|");
                }

                Console.WriteLine("");
                if (row <= 1)
                    Console.WriteLine(horizontalBorder);
            }
            if (maxBetCount > 0) 
                Console.WriteLine(horizontalBorder);
            Console.WriteLine();
            Console.WriteLine();
        }

        //protected void ShowScoreboard()
        //{
        //    Console.WriteLine();
        //    int count = CrapsTable.scoreboard.die01Rolls.Count;

        //    var lastEight = Enumerable
        //        .Range(Math.Max(0, count - DealerCLI.scoreboardHeight), Math.Min(DealerCLI.scoreboardHeight, count))
        //        .Select(i => new
        //        {
        //            Die01 = CrapsTable.scoreboard.die01Rolls[i],
        //            Die02 = CrapsTable.scoreboard.die02Rolls[i]
        //        })
        //        .Reverse() // most recent first
        //        .ToList(); // materialize so that padding can be added

        //    int displayCount = DealerCLI.scoreboardHeight;

        //    // border
        //    string horizontalBorder = "+" + new string('-', DealerCLI.columnWidth) + "+";

        //    Console.WriteLine(horizontalBorder);

        //    for (int i = 0; i < displayCount; i++)
        //    {
        //        Console.Write("| ");

        //        if (i < lastEight.Count)
        //        {
        //            var roll = lastEight[i];
        //            int sum = roll.Die01 + roll.Die02;
        //            string text = $"{roll.Die01}, {roll.Die02} = {sum}";
        //            Console.Write(text.PadRight(DealerCLI.columnWidth - 2));

        //        }
        //        else
        //        {
        //            // empty row padding
        //            Console.Write(new string(' ', DealerCLI.columnWidth - 2));
        //        }
        //        Console.Write(" |");

        //        if (i == 0)
        //        {
        //            string puckStatus = CrapsTable.puck.IsOn ? "ON" : "OFF";
        //            Console.Write($"   Puck: {puckStatus}");
        //        }

        //        Console.WriteLine();

        //    }

        //    Console.WriteLine(horizontalBorder);
        //    Console.WriteLine();

        //}

        protected void RenderGameFeedCLI()
        {
            Console.WriteLine();

            string border = "+" + new string('-', DealerCLI.columnWidth) + "+";
            Console.WriteLine(border);

            foreach (var ev in CrapsTable.gameEventFeed.Events.Take(DealerCLI.scoreboardHeight))
            {
                string prefix = "| ";

                string text = ev.Text;

                Console.WriteLine(prefix + text.PadRight(DealerCLI.columnWidth - 2) +  " |");
            }
            Console.WriteLine(border);
            Console.WriteLine();
        }
    }
}
