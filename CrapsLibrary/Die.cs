namespace CrapsLibrary
{
    public class Die
    {
        private int numSides;

        static Random Generator = new Random();
        
        public Die(int numSides)
        {
            this.numSides = numSides;
        }

        public int NewOutcome()
        {
            return Generator.Next(1, this.numSides);
        }
    }
}
