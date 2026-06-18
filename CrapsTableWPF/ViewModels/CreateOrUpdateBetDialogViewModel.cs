using CrapsLibrary;
using CrapsLibrary.Bets;
using System.Windows;
using System.Windows.Input;

namespace CrapsTableWPF.ViewModels
{
    public class CreateOrUpdateBetDialogViewModel : DialogViewModelBase
    {
        private string _name = "";
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
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

        public event Action? RequestClose;

        public CreateOrUpdateBetDialogViewModel()
        {
            //
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
            this.RequestClose?.Invoke();
        }
    }
}
