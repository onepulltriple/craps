using CrapsLibrary.Bets;

namespace CrapsLibrary.BetWorkingState
{
    internal class BetWorkingStatePaused : BetWorkingState
    {
        public BetWorkingStatePaused(BetWorkingStateMachine betWorkingStateMachine, Bet betInQuestion) : base(betWorkingStateMachine, betInQuestion)
        {
            //this.betWorkingStateMachine = betWorkingStateMachine; // this is done by the base constructor
            //this.betInQuestion = betInQuestion; // this is done by the base constructor
        }

        public override void Enter()
        {
            betWorkingStateMachine.crapsTable.scoreboard.NewSubscriber(this.EvaluateBet);

            // Called-off/paused bets remain paused until set back to working or removed/quit.
            Console.WriteLine($"{betInQuestion.betOwner.playerName} has called off (paused) the bet {betInQuestion.betName}!");
        }

        public override void EvaluateBet(byte firstOutcome, byte secondOutcome)
        {
            // do nothing
            Console.WriteLine($"{betInQuestion.betOwner.playerName}'s {betInQuestion.betName} is still off.");
        }

        public override void Exit()
        {
            // Console.WriteLine($"{betInQuestion.betOwner.playerName}'s {betInQuestion.betName} is back on."); 
            // the bet could also be taken down/quit
            betWorkingStateMachine.crapsTable.scoreboard.Unsubscribe(this.EvaluateBet);
        }
    }
}
