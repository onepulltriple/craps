namespace CrapsLibrary
{
    public class Puck
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
            CrapsTable.scoreboard.EvaluatePuckStatus = this.EvaluateStatus;
        }

        public void EvaluateStatus(byte firstOutcome, byte secondOutcome)
        {
            if (this.MeetsTurnOnCondition(firstOutcome, secondOutcome))
            {
                this.IsOn = true;
                return;
            }
            
            if (this.MeetsTurnOffCondition(firstOutcome, secondOutcome))
            {
                this.IsOn = false;
            }
        }

        private bool MeetsTurnOnCondition(byte firstOutcome, byte secondOutcome)
        {
            // What would flip the puck ON?
            if (!this.IsOn && points.Contains(firstOutcome + secondOutcome))
            {
                passPoint = firstOutcome + secondOutcome;
                Console.WriteLine($"The puck is ON! The point is {passPoint}");
                return true;
            }
            return false;
        }

        private bool MeetsTurnOffCondition(byte firstOutcome, byte secondOutcome)
        {
            // What would flip the puck OFF?
            if (this.IsOn && this.passPoint == (firstOutcome + secondOutcome))
            {
                Console.WriteLine($"The point {passPoint} was MADE. The puck is OFF! Winner!");
                return true;
            }

            if(this.IsOn && seven == (firstOutcome + secondOutcome))
            {
                Console.WriteLine("The puck is OFF! Seven out!");
                this.passPoint = null;
                Console.WriteLine("New roller!");
                Console.WriteLine();
                return true;
            }
            return false;
        }

        // TODO Move bool sevenOut to its own method on the puck
    }
}
