using CrapsLibrary.Bets;

namespace CrapsLibrary.BetWorkingState
{
    internal class BetWorkingStateFullParlay : BetWorkingState
    {
        public BetWorkingStateFullParlay(BetWorkingStateMachine betWorkingStateMachine, Bet betInQuestion) : base(betWorkingStateMachine, betInQuestion)
        {
            //this.betWorkingStateMachine = betWorkingStateMachine; // this is done by the base constructor
            //this.betInQuestion = betInQuestion; // this is done by the base constructor
        }

        public override void Enter()
        {
            betWorkingStateMachine.crapsTable.scoreboard.NewSubscriber(this.EvaluateBet);
        }

        public override void EvaluateBet(byte firstOutcome, byte secondOutcome)
        {
            if (betInQuestion.MeetsFirstWinningCondition(firstOutcome, secondOutcome))
            {
                AnnounceReturnWinnings(firstOutcome, secondOutcome);

                // parlay full amount
                betInQuestion.commitment += betInQuestion.payout;
                return;
            }

            if (betInQuestion.MeetsLosingCondition(firstOutcome, secondOutcome))
            {
                // Don't subtract commitment here, since that has already been given up when placing the bet.
                betInQuestion.betWorkingStateMachine.ChangeState(
                    new BetWorkingStateLost(betWorkingStateMachine, betInQuestion));
            }
        }

        public override void Exit()
        {
            betWorkingStateMachine.crapsTable.scoreboard.Unsubscribe(this.EvaluateBet);
        }
    }
}
