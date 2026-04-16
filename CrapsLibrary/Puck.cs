namespace CrapsLibrary
{
    public class Puck
    {
        public bool IsOn;

        public int? passPoint;

        public List<int> craps = new List<int> { 2, 3, 12};
        public List<int> points = new List<int> { 4, 5, 6, 8, 9, 10};
        public const int seven = 7;
        //public const int yo = 11;

        public Puck()
        {
            this.IsOn = false;
            this.passPoint = null;
            CrapsTable.scoreboard.PuckEvaluateStatus = this.EvaluateStatus;
            CrapsTable.scoreboard.PuckAnnounceSevenOut = this.AnnounceSevenOut;
            CrapsTable.scoreboard.PuckAnnounceNewRoller = this.AnnounceNewRoller;
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
                return;
            }

            if (IsOutcomeSevenOut(firstOutcome, secondOutcome))
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
                //Console.WriteLine($"The puck is ON! The point is {passPoint}");
                CrapsTable.gameEventFeed.Add(
                    $"The puck is ON! The point is {passPoint}",
                    GameEventType.RuleOutcome
                    );

                return true;
            }
            return false;
        }

        private bool MeetsTurnOffCondition(byte firstOutcome, byte secondOutcome)
        {
            // What would flip the puck OFF?
            if (this.IsOn && this.passPoint == (firstOutcome + secondOutcome))
            {
                //Console.WriteLine($"The point {passPoint} was MADE. The puck is OFF! Winner!");
                CrapsTable.gameEventFeed.Add(
                    $"The point {passPoint} was MADE. The puck is OFF! Winner!",
                    GameEventType.RuleOutcome
                    );

                return true;
            }
            return false;
        }

        public bool IsOutcomeSevenOut(byte firstOutcome, byte secondOutcome)
        {
            // The puck is on and then a seven is rolled
            if (this.IsOn && seven == (firstOutcome + secondOutcome))
            {
                //Console.WriteLine("The puck is OFF! Seven out!");
                this.passPoint = null;
                //Console.WriteLine("New roller!");
                //Console.WriteLine();
                return true;
            }
            return false;
        }

        public void AnnounceSevenOut(byte firstOutcome, byte secondOutcome)
        {
            // The puck is on and then a seven is rolled
            if (this.IsOn && seven == (firstOutcome + secondOutcome))
            {
                //Console.WriteLine("The puck is OFF! Seven out!");
                CrapsTable.gameEventFeed.Add(
                    "The puck is OFF! Seven out!",
                    GameEventType.RuleOutcome
                    );
            }
        }

        public void AnnounceNewRoller(byte firstOutcome, byte secondOutcome)
        {
            // The puck is on and then a seven is rolled
            if (this.IsOn && seven == (firstOutcome + secondOutcome))
            {
                //Console.WriteLine("New roller!");
                //Console.WriteLine();
                CrapsTable.gameEventFeed.Add(
                    "New roller!",
                    GameEventType.RuleOutcome
                    );
            }
        }
    }
}
