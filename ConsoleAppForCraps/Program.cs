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

            CrapsTable.betFactory.CreateBet(betType.PassBet, player1, 14);




            //HardWayBet exampleHard10 = new("Hard 10", new List<int> { 10 });
            //HardWayBet exampleHard8 = new("Hard 8", new List<int> { 8 });
            //HardWayBet exampleHard6 = new("Hard 6", new List<int> { 6 });
            //HardWayBet exampleHard4 = new("Hard 4", new List<int> { 4 });
            //PassBet examplePassBet1 = new("PassBet1", 10, new List<int> { 7, 11 });
            //PassBet examplePassBet2 = new("PassBet2", new List<int> { 7, 11 });
            
            for(int i = 0; i < 100; i++)
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
