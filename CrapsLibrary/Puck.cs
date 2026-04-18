namespace CrapsLibrary
{
    public class Puck
    {
        CrapsTable crapsTable;
        
        public bool IsOn;

        public int? passPoint;

        public List<int> craps = new List<int> { 2, 3, 12};
        public List<int> points = new List<int> { 4, 5, 6, 8, 9, 10};
        public const int seven = 7;
        //public const int yo = 11;

        public Puck(CrapsTable crapsTable)
        {
            this.crapsTable = crapsTable;
            this.IsOn = false;
            this.passPoint = null;
            crapsTable.scoreboard.PuckEvaluateStatus = this.EvaluateStatus;
            crapsTable.scoreboard.PuckAnnounceSevenOut = this.AnnounceSevenOut;
            crapsTable.scoreboard.PuckAnnounceNewRoller = this.AnnounceNewRoller;
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

        private bool MeetsTurnOnCondition(byte firstOutcome, byte secondOutcome) // Announce point was established
        {
            // What would flip the puck ON?
            if (!this.IsOn && points.Contains(firstOutcome + secondOutcome))
            {
                // Set the point
                passPoint = firstOutcome + secondOutcome;

                // Announce that the point has been set
                crapsTable.gameEventFeed.Add(
                    $"The puck is ON! The point is {passPoint}",
                    GameEventType.Outcome
                    );

                return true;
            }
            return false;
        }

        private bool MeetsTurnOffCondition(byte firstOutcome, byte secondOutcome) // Announce point was made
        {
            // What would flip the puck OFF?
            if (this.IsOn && this.passPoint == (firstOutcome + secondOutcome))
            {
                crapsTable.gameEventFeed.Add(
                    $"The point {passPoint} was MADE. The puck is OFF! Winner!",
                    GameEventType.Outcome
                    );

                return true;
            }
            return false;
        }

        public bool IsOutcomeSevenOut(byte firstOutcome, byte secondOutcome)
        {
            // The puck is ON and then a seven is rolled
            if (this.IsOn && (firstOutcome + secondOutcome) == seven)
            {
                this.passPoint = null;
                return true;
            }
            return false;
        }

        public void AnnounceSevenOut(byte firstOutcome, byte secondOutcome)
        {
            // The puck is on and then a seven is rolled
            if (this.IsOn && (firstOutcome + secondOutcome) == seven)
            {
                crapsTable.gameEventFeed.Add(
                    "The puck is OFF! SEVEN OUT!",
                    GameEventType.Outcome
                    );
            }
        }

        public void AnnounceNewRoller(byte firstOutcome, byte secondOutcome)
        {
            // The puck is on and then a seven is rolled
            if (this.IsOn && (firstOutcome + secondOutcome) == seven)
            {
                crapsTable.gameEventFeed.Add(
                    "New roller needed!",
                    GameEventType.Outcome
                    );
            }
        }
    }
}
