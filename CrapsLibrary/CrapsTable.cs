namespace CrapsLibrary
{
    public class CrapsTable
    {
        public uint tableMinimum;

        public const uint absoluteTableMinimum = 5;
        public const uint absoluteTableMaximum = 5000;

        public uint tableMaximum;
        
        public Puck puck;

        // A scoreboard is bound to a crapsTable by virture of the crapsTable's puck's delegate methods,
        // which are set in the puck's constructor. The scoreboard itself does not needs a crapsTable reference
        // because the scoreboard does not need to refer to any craps table members.
        public Scoreboard scoreboard = new Scoreboard(); 

        public GameEventFeed gameEventFeed = new GameEventFeed();

        // Player information
        private readonly List<Player> players = new List<Player>();

        private int currentIndex = 0;

        public IReadOnlyList<Player> Players => players;

        public Player CurrentPlayer => players[currentIndex];


        public CrapsTable(uint tableMinimum, uint tableMaximum)
        {
            this.tableMinimum = tableMinimum;
            this.tableMaximum = tableMaximum;
            this.puck = new Puck(this);
        }

        public static Result<uint> ValidateCrapsTableMinimum(string? inputToCheck)
        {
            uint checkedInput;
            bool isUInt = uint.TryParse(inputToCheck, out checkedInput);

            if (!isUInt || checkedInput < absoluteTableMinimum || checkedInput % 5 != 0)
                return Result<uint>.Fail("Please input a whole number that is greater than or equal to 5 and also a multiple of 5");

            if (checkedInput > absoluteTableMaximum)
                return Result<uint>.Fail($"The table minimum must not exceed {absoluteTableMaximum}");

            return Result<uint>.Pass(checkedInput, $"The table minimum has been set to {checkedInput}.");
        }

        public static Result<uint> ValidateUserInputUInt(string? inputToCheck)
        {
            uint checkedInput;
            bool isUInt = uint.TryParse(inputToCheck, out checkedInput);

            if (isUInt)
            {
                return Result<uint>.Pass(checkedInput);
            }
            return Result<uint>.Fail("Please enter a positive whole number (or 0 to abort): "); 
            // only works for addition (multiplication should use 1 to abort)
        }


        public void AddPlayer(Player playerToAdd) // adds the new player to the end
        {
            players.Add(playerToAdd);
        }

        public void InsertPlayer(int index, Player playerToInsert) // inserts a player at an index
        {
            players.Insert(index, playerToInsert);

            if (index <= currentIndex)
                currentIndex++;
        }

        public void RemovePlayer(Player playerToRemove) // removes a player at an index
        {
            // Close all bets for this player (state if the player owes money)

            int index = players.IndexOf(playerToRemove);
            if (index == -1) return;

            players.RemoveAt(index);

            if (index < currentIndex)
                currentIndex--;

            if (currentIndex >= players.Count)
                currentIndex = 0;
        }

        public void NextTurn()
        {
            currentIndex = (currentIndex + 1) % players.Count;
        }


    }
}
