using CrapsLibrary;
using CrapsTableWPF.Infrastructure;
using System.Windows;
using System.Windows.Input;

namespace CrapsTableWPF.ViewModels
{
    public class CreateOrUpdatePlayerDialogViewModel : DialogViewModelBase
    {
        private string? _name;
        public string? Name
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

        private string? _purse;
        public string? Purse
        {
            get => _purse;
            set
            {
                if (_purse != value)
                {
                    _purse = value;
                    OnPropertyChanged(nameof(Purse));
                }
            }
        }


        public event Action? RequestClose;

        public CreateOrUpdatePlayerDialogViewModel()
        {
            //
        }

        internal override void OKButtonClicked()
        {
            // validate player name
            Result<string> resultOfCheckingName = CrapsTable.ValidateUserInputPlayerName(this.Name);

            // display data validation error messages
            if (!resultOfCheckingName.Success)
            {
                this.ErrorMessage = resultOfCheckingName.Messages[0];
                this.HasErrors = true;
                return;
            }

            // validate player purse
            Result<uint> resultOfCheckingPurse = CrapsTable.ValidateUserInputUInt(this.Purse);

            if (!resultOfCheckingPurse.Success)
            {
                this.ErrorMessage = resultOfCheckingPurse.Messages[0];
                this.HasErrors = true;
                return;
            }

            // ask dialog to close
            this.HasErrors = false;
            this.RequestClose?.Invoke();
        }
    }
}
