using CrapsLibrary;

namespace ConsoleAppForCraps
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            bool state1 = new Puck().IsOn;
            Puck Puck1 = new();
            bool state2 = Puck1.FlipPuck();

            Die firstOne = new(6);
            Die secondOne = new(6);
            int result1 = firstOne.NewOutcome();
            int result2 = secondOne.NewOutcome();
            Console.WriteLine(state1);
            Console.WriteLine(state2);
            Console.WriteLine(result1);
            Console.WriteLine(result2);

            CrapsTable Table01 = new(5, 5);
            
            Console.WriteLine(Table01.RollDice(6, 6));
            Console.WriteLine(Table01.RollDice(6, 6));
            Console.WriteLine(Table01.RollDice(6, 6));
            Console.WriteLine(Table01.RollDice(6, 6));
            Console.WriteLine(Table01.RollDice(6, 6));
            Console.WriteLine(Table01.RollDice(6, 6));
            Console.WriteLine(Table01.RollDice(6, 6));


        }
    }
}
