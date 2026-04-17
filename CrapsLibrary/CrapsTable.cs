using CrapsLibrary.Bets;

namespace CrapsLibrary
{
    public class CrapsTable
    {
        public static uint tableMinimum;

        public const uint absTableMinimum = 5;

        public static uint tableMaximum;
        
        public static Scoreboard scoreboard = new Scoreboard();

        public static Puck puck = new Puck();

        public static BetFactory betFactory = new BetFactory();

        public static MessageFeed messageFeed = new MessageFeed();

        public static GameEventFeed gameEventFeed = new GameEventFeed();

        // Player information
        private readonly List<Player> players = new List<Player>();
        private int currentIndex = 0;

        public IReadOnlyList<Player> Players => players;
        public Player CurrentPlayer => players[currentIndex];


        public CrapsTable(uint tableMinimum, uint tableMaximum)
        {
            CrapsTable.tableMinimum = tableMinimum;
            CrapsTable.tableMaximum = tableMaximum;
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

        public (byte, byte) RollDice(byte numSides01, byte numSides02)
        {
            Die Die01 = new(numSides01);
            Die Die02 = new(numSides02);
            byte outcome01 = Die01.NewOutcome();
            byte outcome02 = Die02.NewOutcome();

            return (outcome01, outcome02);
        }

        public void UpdateScoreboardAndPublishOutcomes(byte outcome01, byte outcome02)
        {
            scoreboard.die01Rolls.Add(outcome01);
            scoreboard.die02Rolls.Add(outcome02);

            gameEventFeed.Add(
                $"{outcome01}, {outcome02} --> {outcome01 + outcome02}",
                GameEventType.DiceRoll
                );

            scoreboard.PublishOutcomes();
        }
    }
}
