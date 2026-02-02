namespace CrapsLibrary
{
    public class Scoreboard // a publisher
    {
        public List<int> die01Rolls;

        public List<int> die02Rolls;

        public delegate void OnDiceRolled(int outcome01, int outcome02);

        public List<OnDiceRolled> ListOfBetsToEvaluate = new List<OnDiceRolled>();

        public Scoreboard()
        {
            this.die01Rolls = new List<int>();
            this.die02Rolls = new List<int>();
        }

        public void NewSubscriber(OnDiceRolled newBet)
        {
            this.ListOfBetsToEvaluate.Add(newBet);
        }

        public void Unsubscribe(OnDiceRolled deadBet)
        {
            this.ListOfBetsToEvaluate.Remove(deadBet);
        }

        public void PublishOutcomes() //report latest outcome (tell everyone what happened)
        {
            foreach(OnDiceRolled BetToEvaluate in this.ListOfBetsToEvaluate.ToList())
            {
                // check if the outcome is positive negative according to the evaluation method defined by the bet
                BetToEvaluate.Invoke(this.die01Rolls.Last(),this.die02Rolls.Last());
            }
        }
    }
}
