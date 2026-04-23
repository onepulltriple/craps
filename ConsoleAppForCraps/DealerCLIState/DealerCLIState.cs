using CrapsLibrary;
using CrapsLibrary.Bets;

namespace ConsoleAppForCraps.DealerCLIState
{
    abstract class DealerCLIState
    {
        protected DealerCLIStateMachine dealerCLIStateMachine;

        public DealerCLIState(DealerCLIStateMachine dealerCLIStateMachine)
        {
            this.dealerCLIStateMachine = dealerCLIStateMachine;
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

        public uint ValidateUserInputUIntCLI()
        {
            Result<uint> validUInt;
            do
            {
                string? input = Console.ReadLine();
                validUInt = CrapsTable.ValidateUserInputUInt(input);

                foreach (string message in validUInt.Messages)
                    Console.Write(message);

            } while (!validUInt.Success);

            return validUInt.Value;
        }

        public uint SetCrapsTableMinimumCLI()
        {
            Result<uint> tableMin;
            do
            {
                string? input = Console.ReadLine();
                tableMin = CrapsTable.SetCrapsTableMinimum(input);
                
                foreach (string message in tableMin.Messages)
                    Console.Write(message);
                
            } while (!tableMin.Success);

            return tableMin.Value;
        }

        public void SleepCLI(int milliseconds = DealerCLI.sleepDurationMilliseconds)
        {
            Thread.Sleep(milliseconds);
        }


        //protected T SelectObjectByNameCLI<T>(List<T> listOfObjects) where T : IHasName
        //{
        //    List<int> listOfAcceptableInts = new();

        //    for (int i = 0; i < listOfObjects.Count; i++)
        //    {
        //        Console.WriteLine($"{i + 1}. {listOfObjects[i].Name}");
        //        listOfAcceptableInts.Add(i + 1);
        //    }

        //    return listOfObjects[ValidateUserInputCLIMenu(listOfAcceptableInts) - 1];
        //}

        //protected Player SelectPlayerCLI()
        //{
        //    return SelectObjectByNameCLI(dealerCLIStateMachine.crapsTable!.Players);
        //}


        protected Player SelectPlayerCLI()
        {
            List<int> listOfAcceptableInts = new();

            var players = dealerCLIStateMachine.crapsTable!.Players;

            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine($"{i + 1,2}. {players[i].playerName}");
                listOfAcceptableInts.Add(i + 1);
            }

            return players[ValidateUserInputCLIMenu(listOfAcceptableInts) - 1];
        }

        protected betType SelectBetFromFactoryCLI(Player betPlacer)
        {
            List<int> listOfAcceptableInts = new();

            //var bets = BetFactory.BetDefinitions.ToList();
            var bets = BetFactory.GetAllowedBets(dealerCLIStateMachine.crapsTable!, betPlacer);

            int scaleUpFactor = (int)(dealerCLIStateMachine.crapsTable!.tableMinimum / CrapsTable.absoluteTableMinimum);

            for (int i = 0; i < bets.Count; i++)
            {
                //Console.WriteLine(
                //    $"{i + 1,4}. " +
                //    $"{bets[i].Key,-15}" +
                //    $"Minimum bet = {scaleUpFactor * bets[i].Value.payoutDenominator,-4} " +
                //    $"(payout ratio {bets[i].Value.payoutNumerator,2}" +
                //    $":{bets[i].Value.payoutDenominator,2})");
                var currentBetDef = BetFactory.BetDefinitions[bets[i]];
                Console.WriteLine(
                    $"{i + 1,4}. " +
                    $"{currentBetDef.Name,-15}" +
                    $"Minimum bet = {scaleUpFactor * currentBetDef.payoutDenominator,-4} " +
                    $"(payout ratio {currentBetDef.payoutNumerator,2}" +
                    $":{currentBetDef.payoutDenominator,2})");

                listOfAcceptableInts.Add(i + 1);
            }

            return bets[ValidateUserInputCLIMenu(listOfAcceptableInts) - 1];//.Key;
        }

        protected Bet SelectBetFromPlayerCLI(Player player)
        {
            List<int> listOfAcceptableInts = new();

            var betsOfPlayer = player.playerBetList;

            for (int i = 0; i < betsOfPlayer.Count; i++)
            {
                Console.WriteLine($"{i + 1,2}. {betsOfPlayer[i].Name}");
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
            
            var players = dealerCLIStateMachine.crapsTable!.Players;

            if (players.Count == 0)
            {
                Console.WriteLine("No players at the table.\n\n");
                return;
            }

            // find max number of bets any player has
            int maxBetCount = players.Max(p => p.playerBetList.Count);

            // total rows = 1 for name + 1 for purse + maxBets
            int totalRows = 2 + maxBetCount;

            // present information on screen
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
                            cellText = 
                                $"{player.playerBetList[betIndex]?.Name.ToString() ?? ""} " +
                               $"[{player.playerBetList[betIndex]?.BetWorkingState}]";
                        }
                        // TODO if bet state is paused, add " (OFF)" to the end of the cell text
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

        protected void RenderGameFeedCLI()
        {
            Console.WriteLine();

            string border = "+" + new string('-', DealerCLI.gameFeedWidth) + "+";
            Console.WriteLine(border);

            GameEvent? lastDice = null;

            foreach (var ev in dealerCLIStateMachine.crapsTable!.gameEventFeed.Events.Take(DealerCLI.gameFeedHeight).Reverse())
            {
                if (ev.Type == GameEventType.DiceRoll)
                {
                    lastDice = ev;

                    Console.WriteLine("| " +
                        ev.Text.PadRight(DealerCLI.gameFeedWidth - 2) + " |");
                }
                else
                {
                    string prefix = "    ";

                    string text;

                    if (ev.Type == GameEventType.Outcome)
                    {
                        text = prefix + ev.Text;
                    }
                    else
                    {
                        text = ev.Text;
                    }

                    Console.WriteLine("| " +
                        text.PadRight(DealerCLI.gameFeedWidth - 2) + " |");
                }
            }
            Console.WriteLine(border);
            Console.WriteLine();
        }
    }
}
