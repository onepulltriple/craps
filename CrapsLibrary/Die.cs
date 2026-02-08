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
    }
}
