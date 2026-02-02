namespace CrapsLibrary
{
    public class Puck
    {
        public bool IsOn;

        public int? passPoint;

        public Puck()
        {
            this.IsOn = false;
            this.passPoint = null;
        }

        public bool FlipPuck()
        {
            return !this.IsOn;
        }
    }
}
