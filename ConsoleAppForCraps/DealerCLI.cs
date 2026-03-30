using CrapsLibrary;

namespace ConsoleAppForCraps
{
    public class DealerCLI // translation layer
    {
        public CrapsTable table00;

        public DealerCLI()
        {
            GreetPlayers();

            // Initialize table (with minimum bet)
            this.table00 = new(5, 5);

            // Initialize list of players
            NameNewPlayer();

            RunTable(table00);
        }

        private void GreetPlayers()
        {
            Console.WriteLine("Hello, Players!");
        }

        private void RunTable(CrapsTable table00)
        {
            while (table00.Players.Count > 0)
            {
                UpdateScreen();
            }
        }

        private void UpdateScreen()
        {
            Console.Clear();
        }

        public void NameNewPlayer()
        {
            string enteredName = "";
            bool validName = false;
            while (validName == false)
            {
                Console.Clear();
                Console.WriteLine("What is the name of the first player?");
                enteredName = Console.ReadLine();
                if (enteredName != "" &&
                    !enteredName.Contains("!@#$%^&*()")
                    )
                {
                    validName = true;
                }
            }
            table00.AddPlayer(new Player(enteredName));
        }
    }
}
