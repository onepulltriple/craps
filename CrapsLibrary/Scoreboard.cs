namespace CrapsLibrary
{
    public class Scoreboard // a publisher
    {
        public List<byte> die01Rolls;

        public List<byte> die02Rolls;

        public delegate void OnDiceRolled(byte outcome01, byte outcome02);

        public List<OnDiceRolled> ListOfBetsToEvaluate = new List<OnDiceRolled>();

        public OnDiceRolled? PuckEvaluateStatus, PuckAnnounceSevenOut, PuckAnnounceNewRoller;

        public Scoreboard()
        {
            this.die01Rolls = new List<byte>();
            this.die02Rolls = new List<byte>();
        }

        public void NewSubscriber(OnDiceRolled newBet)
        {
            this.ListOfBetsToEvaluate.Add(newBet);
        }

        public void Unsubscribe(OnDiceRolled deadBet)
        {
            this.ListOfBetsToEvaluate.Remove(deadBet);
        }

        /// <summary>
        /// The delegate methods should be invoked in the order that one would expect in a real craps game, which is:
        /// 1 announce seven out, 
        /// 2 figure out which bets won or lost, 
        /// 3 ask for a new roller,
        /// 4 change the puck status (as the last step), 
        /// The PushlishOutcomes method "forces" the relative order of outcome determiniation relative to the collection of bets, since the collection of bets will be evaluated in an unordered fashion
        /// </summary>
        public void PublishOutcomes() // report latest outcome (tell everyone what happened)
        {
            // 1 Announce seven out
            if (this.PuckAnnounceSevenOut != null)
            {
                PuckAnnounceSevenOut.Invoke(this.die01Rolls.Last(), this.die02Rolls.Last());
            }

            // 2 Check outcomes for ALL bets
            //   Check if the betting outcomes are positive or negative via the evaluation method defined by each bet
            foreach (OnDiceRolled BetToEvaluate in this.ListOfBetsToEvaluate.ToList())
            {
                BetToEvaluate.Invoke(this.die01Rolls.Last(), this.die02Rolls.Last());
            }

            // 3 Announce new roller
            if (this.PuckAnnounceNewRoller != null)
            {
                PuckAnnounceNewRoller.Invoke(this.die01Rolls.Last(), this.die02Rolls.Last());
            }

            // 4 Update the puck status (after evaluating all bets)
            if (this.PuckEvaluateStatus != null)
            {
                PuckEvaluateStatus.Invoke(this.die01Rolls.Last(), this.die02Rolls.Last());
            }
        }
    }
}
