namespace CrapsLibrary
{
    public class Die
    {
        private byte numSides;

        static Random Generator = new Random();
        
        public Die(byte numSides)
        {
            this.numSides = numSides;
        }

        public byte NewOutcome()
        {
            return (byte)Generator.Next(1, this.numSides + 1);
        }

        public static (byte, byte) RollDice(byte numSides01, byte numSides02)
        {
            Die Die01 = new(numSides01);
            Die Die02 = new(numSides02);
            byte outcome01 = Die01.NewOutcome();
            byte outcome02 = Die02.NewOutcome();

            return (outcome01, outcome02);
        }
    }
}
