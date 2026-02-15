using CrapsLibrary;

namespace ConsoleAppForCraps
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Players!");

            // initialize table (with minimum bet)
            CrapsTable Table01 = new(5, 5);

            // do I need a dealer?

            // add new player (click on open slot?), can join at any time

            // player buys chips (can buy more at any time) starts with 100 by default



            // 1 player creates bets

            // TODO betting closed

            // dice rolled

            // kill bets, pay bets, flip pick, pause place bets (if puck switches to off)

            // cycle through players, (back to 1)

            Player player1 = new Player("Chase");
            player1.purse += 100;


            foreach(betType bet in Enum.GetValues<betType>())
            {
                Bet? newBet = CrapsTable.betFactory.CreateBet(player1, bet, 6);
                if (newBet != null)
                    player1.playerBetList.Add(newBet);
            }
            bool temp = PlaceBet.IsPlaceBetAllowed(player1);

            for (int i = 0; i < 100; i++)
            {
                Table01.RollDice(6, 6);
                Console.WriteLine($"{player1.playerName} now has {player1.purse} credits.");
            }

        }
    }
}
