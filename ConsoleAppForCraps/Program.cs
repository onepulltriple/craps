using CrapsLibrary;

namespace ConsoleAppForCraps
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Players!");

            CrapsTable Table01 = new(5, 5);



            //HardWayBet exampleHard10 = new("Hard 10", new List<int> { 10 }, 6, 1);
            //HardWayBet exampleHard8 = new("Hard 8", new List<int> { 8 }, 6, 1);
            //HardWayBet exampleHard6 = new("Hard 6", new List<int> { 6 }, 6, 1);
            //HardWayBet exampleHard4 = new("Hard 4", new List<int> { 4 }, 6, 1);
            PassBet examplePassBet1 = new("PassBet1", 10, new List<int> { 7, 11 }, 2, 1);
            //PassBet examplePassBet2 = new("PassBet2", new List<int> { 7, 11 }, 2, 1);
            
            for(int i = 0; i < 100; i++)
            {
                if (examplePassBet1.isWorking == false)
                {
                    examplePassBet1 = new PassBet($"PassBet{i}", 10,new List<int> { 7, 11 }, 2, 1);
                }
                Table01.RollDice(6, 6);

            }
        }
    }
}
