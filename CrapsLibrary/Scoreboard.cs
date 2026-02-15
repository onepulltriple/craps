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

        public void PublishOutcomes() // report latest outcome (tell everyone what happened)
        {
            // announce seven out
            if (this.PuckAnnounceSevenOut != null)
            {
                PuckAnnounceSevenOut.Invoke(this.die01Rolls.Last(), this.die02Rolls.Last());
            }

            // check if the betting outcomes are positive or negative according to the evaluation method defined by each bet
            foreach (OnDiceRolled BetToEvaluate in this.ListOfBetsToEvaluate.ToList())
            {
                BetToEvaluate.Invoke(this.die01Rolls.Last(), this.die02Rolls.Last());
            }

            // announce new roller
            if (this.PuckAnnounceNewRoller != null)
            {
                PuckAnnounceNewRoller.Invoke(this.die01Rolls.Last(), this.die02Rolls.Last());
            }

            // update the puck status (after evaluating all bets)
            if (this.PuckEvaluateStatus != null)
            {
                PuckEvaluateStatus.Invoke(this.die01Rolls.Last(), this.die02Rolls.Last());
            }
        }
    }
}
