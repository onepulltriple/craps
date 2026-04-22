using CrapsLibrary.Bets;

namespace CrapsLibrary.BetWorkingState
{
    internal class BetWorkingStatePaused : BetWorkingState
    {
        public override string Name { get; }

        public BetWorkingStatePaused(BetWorkingStateMachine betWorkingStateMachine, Bet betInQuestion) : base(betWorkingStateMachine, betInQuestion)
        {
            //this.betWorkingStateMachine = betWorkingStateMachine; // this is done by the base constructor
            //this.betInQuestion = betInQuestion; // this is done by the base constructor
            this.Name = "Paused";
        }

        public override void Enter()
        {
            //betWorkingStateMachine.crapsTable.scoreboard.NewSubscriber(this.EvaluateBet);

            // Called-off/paused bets remain paused until set back to working or removed/quit.
            AnnouncePaused();
        }

        public override void EvaluateBet(byte firstOutcome, byte secondOutcome)
        {
            if (betInQuestion.MeetsFirstWinningCondition(firstOutcome, secondOutcome))
            {
                // taunt player (like a good dealer would)
                // you would have to know if the place bets were paused by the player or the game itself
                //AnnounceTaunt();

                //return;
            }

            if (betInQuestion.MeetsLosingCondition(firstOutcome, secondOutcome))
            {
                // do nothing
                // announce bullet dodged
            }
        }

        public override void Exit()
        {
            //betWorkingStateMachine.crapsTable.scoreboard.Unsubscribe(this.EvaluateBet);
        }
    }
}
