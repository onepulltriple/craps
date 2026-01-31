namespace CrapsLibrary
{
    public class Puck
    {
        public bool IsOn;

        public Puck(bool isOn = false)
        {
            this.IsOn = isOn;
        }

        public bool flipPuck()
        {
            return !this.IsOn;
        }
    }
}
