namespace CrapsLibrary
{
    public class Puck : ObservableObject
    {
        CrapsTable crapsTable;
        

        public List<int> craps = new List<int> { 2, 3, 12};
        public List<int> points = new List<int> { 4, 5, 6, 8, 9, 10};
        public const int seven = 7;
        public const int yo = 11;

        // Observeable properties
        private bool _isOn;
        public bool IsOn
        {
            get => _isOn;
            set
            {
                if (_isOn != value)
                {
                    _isOn = value;
                    OnPropertyChanged(nameof(IsOn));
                }
            }
        }

        public int? _passPoint;
        public int? PassPoint
        {
            get => _passPoint;
            set
            {
                if (_passPoint != value)
                {
                    _passPoint = value;
                    OnPropertyChanged(nameof(PassPoint));
                }
            }
        }


        public Puck(CrapsTable crapsTable)
        {
            this.crapsTable = crapsTable;
            this.IsOn = false;
            this.PassPoint = null;
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
                this.PassPoint = null;
                return;
            }

            if (IsOutcomeSevenOut(firstOutcome, secondOutcome))
            {
                this.IsOn = false;
                this.PassPoint = null;
            }
        }

        private bool MeetsTurnOnCondition(byte firstOutcome, byte secondOutcome) // Announce point was established
        {
            // What would flip the puck ON?
            if (!this.IsOn && points.Contains(firstOutcome + secondOutcome))
            {
                // Set the point
                PassPoint = firstOutcome + secondOutcome;

                // Announce that the point has been set
                crapsTable.gameEventFeed.Add(
                    //$"The puck is ON! The point is {PassPoint}.",
                    $"The point is now {PassPoint}.",
                    GameEventType.Outcome,
                    true
                    );
                crapsTable.gameEventFeed.Add(
                    $"Place bets are now available!",
                    GameEventType.Outcome,
                    true
                    );

                return true;
            }
            return false;
        }

        private bool MeetsTurnOffCondition(byte firstOutcome, byte secondOutcome) // Announce point was made
        {
            // What would flip the puck OFF?
            if (this.IsOn && this.PassPoint == (firstOutcome + secondOutcome))
            {
                crapsTable.gameEventFeed.Add(
                    //$"The point {PassPoint} was MADE. The puck is OFF! Winner!",
                    $"The point {PassPoint} was made. Winner!",
                    GameEventType.Outcome,
                    true
                    );
                crapsTable.gameEventFeed.Add(
                    $"Place bets are now paused/inaccessible!",
                    GameEventType.Outcome,
                    true
                    );

                return true;
            }
            return false;
        }


        /// <summary>
        /// Happens BEFORE the bets get evaluated
        /// </summary>
        /// <param name="firstOutcome"></param>
        /// <param name="secondOutcome"></param>
        /// <returns></returns>
        public bool IsOutcomeSevenOut(byte firstOutcome, byte secondOutcome)
        {
            // The puck is ON and then a seven is rolled
            if (this.IsOn && (firstOutcome + secondOutcome) == seven)
            {
                this.PassPoint = null;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Happens AFTER the bets get evaluated
        /// </summary>
        /// <param name="firstOutcome"></param>
        /// <param name="secondOutcome"></param>
        public void AnnounceSevenOut(byte firstOutcome, byte secondOutcome)
        {
            // The puck is on and then a seven is rolled
            if (this.IsOn && (firstOutcome + secondOutcome) == seven)
            {
                crapsTable.gameEventFeed.Add(
                    //"The puck is OFF! SEVEN OUT!",
                    "Seven out!",
                    GameEventType.Outcome,
                    true
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
