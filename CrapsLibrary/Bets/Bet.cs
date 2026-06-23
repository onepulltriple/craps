using CrapsLibrary.BetWorkingState;

namespace CrapsLibrary.Bets
{
    public abstract class Bet : ObservableObject
    {
        protected CrapsTable crapsTable;

        public Player betOwner;
        
        public betType betType { get; }

        public List<int> winningTotals;

        internal BetWorkingStateMachine betWorkingStateMachine;

        public string BetWorkingState => betWorkingStateMachine.CurrentStateName;


        // Observeable properties
        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private uint _countOfUnitsToBet;
        public uint CountOfUnitsToBet
        {
            get => _countOfUnitsToBet;
            set
            {
                if (_countOfUnitsToBet != value)
                {
                    _countOfUnitsToBet = value;
                    OnPropertyChanged(nameof(CountOfUnitsToBet));
                }
            }
        }

        private uint _unitOfBet;
        public uint UnitOfBet
        {
            get => _unitOfBet;
            set
            {
                if (_unitOfBet != value)
                {
                    _unitOfBet = value;
                    OnPropertyChanged(nameof(UnitOfBet));
                }
            }
        }

        // what is at risk
        private uint _commitment;
        public uint Commitment
        {
            get => _commitment;
            set
            {
                if (_commitment != value)
                {
                    _commitment = value;
                    OnPropertyChanged(nameof(Commitment));
                }
            }
        }

        // what can be won
        private uint _payout;
        public uint Payout
        {
            get => _payout;
            set
            {
                if (_payout != value)
                {
                    _payout = value;
                    OnPropertyChanged(nameof(Payout));
                }
            }
        }

        /// <summary>
        /// A bet is 'working' when it is subscribed to the outcomes of the the scoreboard and has an owner (a player).
        /// A bet is paused when it is unsubscribed but still has an owner.
        /// When a bet is lost its state changes accordingly, which holds the owner's bet history.
        /// </summary>
        /// <param name="crapsTable"></param>
        /// <param name="betOwner"></param>
        /// <param name="betType"></param>
        /// <param name="countOfUnitsToBet"></param>
        /// <param name="unitOfBet"></param>
        /// <param name="winningTotals"></param>
        /// <param name="payout"></param>
        public Bet(CrapsTable crapsTable, Player betOwner, betType betType, uint countOfUnitsToBet, uint unitOfBet, List<int> winningTotals, uint payout)
        {
            this.crapsTable = crapsTable;
            this.betOwner = betOwner;
            this.betType = betType;
            this.winningTotals = winningTotals;

            // Observeable properties
            this.Name = BetFactory.BetDefinitions[betType].Name;
            this.CountOfUnitsToBet = countOfUnitsToBet;
            this.UnitOfBet = unitOfBet;
            this.Commitment = countOfUnitsToBet * unitOfBet;
            this.Payout = payout;

            // Create a state machine to manage this bet's states
            betWorkingStateMachine = new(crapsTable);

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

        public Result<bool> IsPausingBetAllowed()
        {
            Result<bool> isPausingAllowed;

            // insert checks here
            // when is pausing not allowed?
            isPausingAllowed = Result<bool>.Pass(true);

            if (!isPausingAllowed.Success)
                return Result<bool>.Fail("Pausing this bet is not allowed.");

            return Result<bool>.Pass(true, $"\n{this.betOwner} has paused their {this.Name}.");
        }


        /// <summary>
        /// Method to manually pay out and then remove a bet from a player's bet list.
        /// </summary>
        public void QuitBet()
        {
            betOwner.Purse += (CountOfUnitsToBet * UnitOfBet);
            betOwner.PlayerBetList.Remove(this);
        }

        public void LoseCommitment()
        {
            // TODO pay commitment to house (whenever the house gets a purse)
            Commitment = 0;
        }

        internal abstract bool MeetsLosingCondition(byte firstOutcome, byte secondOutcome);

        internal abstract bool MeetsFirstWinningCondition(byte firstOutcome, byte secondOutcome);
    }
}
