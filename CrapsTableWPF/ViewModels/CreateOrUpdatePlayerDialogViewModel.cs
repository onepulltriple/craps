using CrapsLibrary;
using CrapsTableWPF.Infrastructure;
using System.Windows;
using System.Windows.Input;

namespace CrapsTableWPF.ViewModels
{
    public class CreateOrUpdatePlayerDialogViewModel : ViewModelBase
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


        private string? _errorMessage;
        public string? ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }


        private bool _hasErrors = false;
        public bool HasErrors
        {
            get => _hasErrors;
            set
            {
                if (_hasErrors != value)
                {
                    _hasErrors = value;
                    OnPropertyChanged(nameof(HasErrors));
                }
            }

        }
        
        public ICommand OKButtonCommand { get; }

        public event Action? RequestClose;

        public CreateOrUpdatePlayerDialogViewModel()
        {
            this.OKButtonCommand = new RelayCommand(_ => OKButtonClicked());
        }

        private void OKButtonClicked()
        {
            // validate player name
            Result<string> resultOfCheckingName = CrapsTable.ValidateUserInputPlayerName(this.Name);

            
            // display data validation error messages
            if (!resultOfCheckingName.Success)
            {
                ErrorMessage = resultOfCheckingName.Messages[0];
                HasErrors = true;
                return;
            }

            // validate player purse
            Result<uint> resultOfCheckingPurse = CrapsTable.ValidateUserInputUInt(this.Purse);

            if (!resultOfCheckingPurse.Success)
            {
                ErrorMessage = resultOfCheckingPurse.Messages[0];
                HasErrors = true;
                return;
            }

            HasErrors = false;
            this.RequestClose?.Invoke();
        }
    }
}
