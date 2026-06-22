using CrapsTableWPF.Infrastructure;
using System.Windows.Input;

namespace CrapsTableWPF.ViewModels
{
    public abstract class DialogViewModelBase : ViewModelBase
    {
        private string? _heading;
        public string? Heading
        {
            get => _heading;
            set
            {
                if (_heading != value)
                {
                    _heading = value;
                    OnPropertyChanged(nameof(Heading));
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

        public event Action? RequestClose;

        public ICommand OKButtonCommand { get; }

        public DialogViewModelBase()
        {
            this.OKButtonCommand = new RelayCommand(_ => OKButtonClicked());
        }

        protected void RaiseRequestClose()
        {
            this.RequestClose?.Invoke();
        }

        internal abstract void OKButtonClicked();

    }
}
