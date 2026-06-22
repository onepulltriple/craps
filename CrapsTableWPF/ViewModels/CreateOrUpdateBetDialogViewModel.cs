using CrapsLibrary;
using CrapsLibrary.Bets;
using CrapsTableWPF.Infrastructure;
using System.Windows;
using System.Windows.Input;

namespace CrapsTableWPF.ViewModels
{
    public class CreateOrUpdateBetDialogViewModel : DialogViewModelBase
    {
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

        private string? _commitment;
        public string? Commitment
        {
            get => _commitment;
            set
            {
                _commitment = value;
                OnPropertyChanged(nameof(Commitment));
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

        public CreateOrUpdateBetDialogViewModel(CrapsTable crapsTable, Player currentPlayer, betType betType)
        {
            this.PlayerName = currentPlayer.Name;
            this.PlayerPurse = currentPlayer.Purse.ToString();
            var betInfo = BetFactory.BetDefinitions[betType];
            this.UnitOfBet = BetFactory.DetermineUnitOfBet(crapsTable, betType).ToString();


            this.Heading = $"Manage {PlayerName}'s {betInfo.Name} bet";

        }

        public void OnSpinnerValueChanged()
        {
            if (UnitOfBet != null && CountOfUnitsToBet != null) 
                Commitment = (uint.Parse(UnitOfBet) * uint.Parse(CountOfUnitsToBet)).ToString();
        }

        internal override void OKButtonClicked()
        {
            // validate bet commitment
            Result<uint> resultOfCheckingCommitment = CrapsTable.ValidateUserInputUInt(this.Commitment);

            // display data validation error messages
            if (!resultOfCheckingCommitment.Success)
            {
                this.ErrorMessage = resultOfCheckingCommitment.Messages[0];
                this.HasErrors = true;
                return;
            }

            // ask dialog to close
            this.HasErrors = false;
            this.RaiseRequestClose();
        }
    }
}
