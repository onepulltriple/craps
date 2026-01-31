namespace CrapsLibrary
{
    public class Die
    {
        private int numSides;
        
        public Die(int numSides)
        {
            this.numSides = numSides;
        }

        public int newOutcome()
        {
            Random Generator = new();

            return Generator.Next(1, this.numSides);
        }
    }
}
