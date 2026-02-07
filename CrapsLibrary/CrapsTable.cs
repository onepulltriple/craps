namespace CrapsLibrary
{
    public class CrapsTable
    {
        public int tableMinimum {  get; set; } //do something with this
        public int tableMaximum {  get; set; } //do something with this

        public static Scoreboard scoreboard = new Scoreboard();

        public static Puck puck = new Puck();

        public static BetFactory betFactory = new BetFactory();

        public List<Player> players = new List<Player>();

        public CrapsTable(int tableMinimum, int tableMaximum)
        {
            this.tableMinimum = tableMinimum;
            this.tableMaximum = tableMaximum;
        }

        public void NewPlayer(Player player)
        {
            players.Add(player);
        }

        public (int, int) RollDice(int numSides01, int numSides02)
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
