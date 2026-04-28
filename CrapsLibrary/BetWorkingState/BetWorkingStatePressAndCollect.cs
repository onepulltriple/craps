using CrapsLibrary.Bets;

namespace CrapsLibrary.BetWorkingState
{
    internal class BetWorkingStatePartialParlay : BetWorkingState
    {
        public override string Name { get; }
        
        public BetWorkingStatePartialParlay(BetWorkingStateMachine betWorkingStateMachine, Bet betInQuestion) : base(betWorkingStateMachine, betInQuestion)
        {
            //this.betWorkingStateMachine = betWorkingStateMachine; // this is done by the base constructor
            //this.betInQuestion = betInQuestion; // this is done by the base constructor
            this.Name = "Partial Parlay"; // aka "press and collect"
        }

        public override void Enter()
        {
            betWorkingStateMachine.crapsTable.scoreboard.NewSubscriber(this.EvaluateBet);

            // prompt user to specify the press-to-collect ratio

        }

        public override void EvaluateBet(byte firstOutcome, byte secondOutcome)
        {
            if (betInQuestion.MeetsFirstWinningCondition(firstOutcome, secondOutcome))
            {
                AnnounceReturnWinnings(firstOutcome, secondOutcome);

                // split winnings between parlay and payout
                betInQuestion.commitment += betInQuestion.payout/2; // TODO need to calculate these two based on betting units
                betInQuestion.betOwner.purse += betInQuestion.payout/2;
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
