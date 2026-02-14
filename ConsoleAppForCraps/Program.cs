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

            // betting closed

            // dice rolled

            // kill bets, pay bets, flip pick, pause place bets (if puck switches to off)

            // cycle through players, (back to 1)

            Player player1 = new Player("Chase");
            player1.purse += 100;

            Bet? newBet1 = CrapsTable.betFactory.CreateBet(player1, betType.PassBet, 14);
            Bet? newBet2 = CrapsTable.betFactory.CreateBet(player1, betType.Hard_10, 14);

            if (newBet1 != null)
            {
                player1.workingBets.Add(newBet1);
            }

            if (newBet2 != null)
            {
                player1.workingBets.Add(newBet2);
            }




            for (int i = 0; i < 100; i++)
            {
                //if (player1.workingBets.First().isWorking == false)
                //{
                //    //examplePassBet1 = new PassBet($"PassBet{i}", 10, new List<int> { 7, 11 });
                //    CrapsTable.betFactory.CreateBet(betType.PassBet, player1, 14);
                //}
                Table01.RollDice(6, 6);

            }
        }
    }
}
