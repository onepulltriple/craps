using CrapsLibrary.Bets;

namespace CrapsLibrary.BetWorkingState
{
    internal class BetWorkingStateLost : BetWorkingState
    {
        public BetWorkingStateLost(BetWorkingStateMachine betWorkingStateMachine, Bet betInQuestion) : base(betWorkingStateMachine, betInQuestion)
        {
            //this.betWorkingStateMachine = betWorkingStateMachine; // this is done by the base constructor
            //this.betInQuestion = betInQuestion; // this is done by the base constructor
        }

        public override void Enter()
        {
            AnnounceLost();
        }

        public override void EvaluateBet(byte firstOutcome, byte secondOutcome)
        {
            // do nothing
        }

        public override void Exit()
        {
            // do nothing
        }
    }
}
