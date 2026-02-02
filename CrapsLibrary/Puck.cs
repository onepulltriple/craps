using System.Drawing;

namespace CrapsLibrary
{
    public class Puck : IBet
    {
        public bool IsOn;

        public int? passPoint;

        public List<int> craps = new List<int> { 2, 3, 12};
        public List<int> points = new List<int> { 4, 5, 6, 8, 9, 10};
        public int seven = 7;
        //public int yo = 11;

        public Puck()
        {
            this.IsOn = false;
            this.passPoint = null;
            CrapsTable.scoreboard.NewSubscriber(this.EvaluateBet);
        }

        public bool FlipPuck()
        {
            this.AnnounceStatus();
            return !this.IsOn;
        }

        public void AnnounceStatus()
        {
            if (this.IsOn)
            {
                Console.WriteLine("The point is ON!");
            }
            else
            {
                Console.WriteLine("The point is OFF!");
            }
        }

        public void EvaluateBet(int firstOutcome, int secondOutcome)
        {
            if (this.MeetsFirstWinningCondition(firstOutcome, secondOutcome))
            {
                this.FlipPuck();
                this.AnnounceStatus();
            }

            if (this.MeetsLosingCondition(firstOutcome, secondOutcome))
            {
                this.FlipPuck();
                this.AnnounceStatus();
            }
        }

        public bool MeetsFirstWinningCondition(int firstOutcome, int secondOutcome)
        {
            // What would flip the puck ON?
            if (!this.IsOn && points.Contains(firstOutcome + secondOutcome))
            {
                return true;
            }
            return false;
        }

        public bool MeetsLosingCondition(int firstOutcome, int secondOutcome)
        {
            // What would flip the puck OFF?
            if ((this.IsOn && points.Contains(firstOutcome + secondOutcome)) ||  // point is made
                (this.IsOn &&       seven == (firstOutcome + secondOutcome)))    // seven out
            {
                return true;
            }
            return false;
        }
    }
}
