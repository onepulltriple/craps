using CrapsLibrary;
using CrapsLibrary.Bets;

namespace CrapsTableWPF.ViewModels
{
    public class CreateOrUpdateBetDialogViewModel : DialogViewModelBase
    {
        private readonly CrapsTable crapsTable;

        private readonly Player currentPlayer;

        private readonly betType betType;

        public Bet? bet;

        private string _playerName = "";
        public string PlayerName
        {
            get => _playerName;
            set
            {
                _playerName = value;
                OnPropertyChanged(nameof(PlayerName));
            }
        }

        private string _playerPurse = "";
        public string PlayerPurse
        {
            get => _playerPurse;
            set
            {
                _playerPurse = value;
                OnPropertyChanged(nameof(PlayerPurse));
            }
        }

        private string _betName = "";
        public string BetName
        {
            get => _betName;
            set
            {
                _betName = value;
                OnPropertyChanged(nameof(BetName));
            }
        }

        private string? _countOfUnitsToBet;
        public string? CountOfUnitsToBet
        {
            get => _countOfUnitsToBet;
            set
            {
                _countOfUnitsToBet = value;
                OnPropertyChanged(nameof(CountOfUnitsToBet));
                OnSpinnerValueChanged();
            }
        }

        private string? _unitOfBet;
        public string? UnitOfBet
        {
            get => _unitOfBet;
            set
            {
                _unitOfBet = value;
                OnPropertyChanged(nameof(UnitOfBet));
            }
        }

        private string?_amountThrownAtBet;
        public string? AmountThrownAtBet
        {
            get => _amountThrownAtBet;
            set
            {
                _amountThrownAtBet = value;
                OnPropertyChanged(nameof(AmountThrownAtBet));
            }
        }


        public CreateOrUpdateBetDialogViewModel(CrapsTable crapsTable, Player currentPlayer, betType betType, Bet? bet)
        {
            this.crapsTable = crapsTable;
            this.currentPlayer = currentPlayer;
            this.betType = betType;

            this.PlayerName = currentPlayer.Name;
            this.PlayerPurse = currentPlayer.Purse.ToString();
            var betInfo = BetFactory.BetDefinitions[betType];
            
            // initial values
            this.CountOfUnitsToBet = 1.ToString();
            this.UnitOfBet = BetFactory.DetermineUnitOfBet(crapsTable, betType).ToString();
            this.Heading = $"Manage {PlayerName}'s {betInfo.Name} bet";
            OnSpinnerValueChanged();

            if (bet != null)
            {
                this.bet = bet;
                this.BetName = bet.Name;
                this.UnitOfBet = bet.UnitOfBet.ToString();
                this.CountOfUnitsToBet = bet.CountOfUnitsToBet.ToString();
                this.AmountThrownAtBet = bet.Commitment.ToString();
            }
        }

        public void OnSpinnerValueChanged()
        {
            if (UnitOfBet != null && CountOfUnitsToBet != null) 
                AmountThrownAtBet = (uint.Parse(UnitOfBet) * uint.Parse(CountOfUnitsToBet)).ToString();
        }

        internal override void OKButtonClicked()
        {
            // validate bet commitment
            Result<uint> resultOfCheckingUintInput = CrapsTable.ValidateUserInputUInt(this.AmountThrownAtBet);

            // display data validation error messages
            if (!resultOfCheckingUintInput.Success)
            {
                this.ErrorMessage = resultOfCheckingUintInput.Messages[0];
                this.HasErrors = true;
                return;
            }

            // create or update a bet (with the current player in mind) 
            // the bet will not yet be added to the player bet list
            Result<Bet> createOrUpdateBetResult = BetFactory.CreateOrUpdateBet(this.crapsTable, this.currentPlayer, this.betType, resultOfCheckingUintInput.Value);

            // display error messages if bet management failed
            if (!createOrUpdateBetResult.Success)
            {
                this.ErrorMessage = createOrUpdateBetResult.Messages[0];
                this.HasErrors = true;
                return;
            }

            // if the bet was newly created, add it to the player's bet list
            if (this.bet == null)
                this.currentPlayer.AddOneBet(createOrUpdateBetResult.Value);

            // make the bet available for UI display (BetViewModel needs this to populate UI)
            this.bet = createOrUpdateBetResult.Value;

            // ask dialog to close
            this.HasErrors = false;
            this.RaiseRequestClose();
        }
    }
}
