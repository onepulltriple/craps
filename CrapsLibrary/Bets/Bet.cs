using CrapsLibrary.BetWorkingState;
using System.Numerics;

namespace CrapsLibrary.Bets
{
    public abstract class Bet
    {
        public Player betOwner;
        
        public string betName;

        public uint commitment;

        public List<int> winningTotals;

        public uint payout;

        internal BetWorkingStateMachine betWorkingStateMachine;

        /// <summary>
        /// A bet is 'working' when it is subscribed to the outcomes of the CrapsTable.scoreboard and has an owner, i.e. a player.
        /// A bet is paused when it is unsubscribed but still has an owner.
        /// A bet is no longer working when it loses its owner, at which point it will be killed and queued for garbage collection.
        /// </summary>
        /// <param name="betOwner"></param>
        /// <param name="betName"></param>
        /// <param name="commitment"></param>
        /// <param name="winningTotals"></param>
        /// <param name="payout"></param>
        public Bet(Player betOwner, string betName, uint commitment, List<int> winningTotals, uint payout)
        {
            this.betOwner = betOwner;
            this.betName = betName;
            this.commitment = commitment;
            this.winningTotals = winningTotals;
            this.payout = payout;

            // Create a state machine to manage this bet's states
            betWorkingStateMachine = new();

            // Set the initial bet state by changing state to BetWorkingStateReturnWinnings, whose constructor requires:
            // - a reference to the instance of the bet's state machine, i.e. 'betWorkingStateMachine'
            // - a reference to the instance of the bet whose state is about to change, i.e. be initialised, which is this bet
            betWorkingStateMachine.ChangeState(new BetWorkingStateReturnWinnings(betWorkingStateMachine, this));
        }

        /// <summary>
        /// Method to manually call off (pause) a bet.
        /// </summary>
        public void PauseBet()
        {
            betWorkingStateMachine.ChangeState(new BetWorkingStatePaused(betWorkingStateMachine, this));
        }

        /// <summary>
        /// Method to manually pay out and then remove a bet from a player's bet list.
        /// </summary>
        public void QuitBet()
        {
            betOwner.purse += commitment;
            betOwner.playerBetList.Remove(this);
        }

        internal abstract bool MeetsLosingCondition(byte firstOutcome, byte secondOutcome);

        internal abstract bool MeetsFirstWinningCondition(byte firstOutcome, byte secondOutcome);
    }
}
