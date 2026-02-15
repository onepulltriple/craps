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

        public List<Player> players = new List<Player>();

        public CrapsTable(uint tableMinimum, uint tableMaximum)
        {
            CrapsTable.tableMinimum = tableMinimum;
            CrapsTable.tableMaximum = tableMaximum;
        }

        public void NewPlayer(string playerName)
        {
            Player tempPlayer = new Player(playerName);
            players.Add(tempPlayer);
        }

        public (byte, byte) RollDice(byte numSides01, byte numSides02)
        {
            Die Die01 = new(numSides01);
            Die Die02 = new(numSides02);
            scoreboard.die01Rolls.Add(Die01.NewOutcome());
            scoreboard.die02Rolls.Add(Die02.NewOutcome());

            //to be removed later
            int sum = scoreboard.die01Rolls.Last() + scoreboard.die02Rolls.Last();
            Console.WriteLine($"{scoreboard.die01Rolls.Last()}, {scoreboard.die02Rolls.Last()} = {sum}");
            
            scoreboard.PublishOutcomes();

            return (scoreboard.die01Rolls.Last(), scoreboard.die02Rolls.Last());
        }
    }
}
