using CrapsLibrary;

namespace ConsoleAppForCraps
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            bool state = new Puck().IsOn;
            Console.WriteLine(state);
        }
    }
}
