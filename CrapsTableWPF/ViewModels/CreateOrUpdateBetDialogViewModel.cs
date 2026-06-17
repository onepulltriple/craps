using CrapsLibrary.Bets;

namespace CrapsTableWPF.ViewModels
{
    public class CreateOrUpdateBetDialogViewModel : ViewModelBase
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

        private uint _commitment;
        public uint Commitment
        {
            get => _commitment;
            set
            {
                _commitment = value;
                OnPropertyChanged(nameof(Commitment));
            }
        }

        public CreateOrUpdateBetDialogViewModel() { }
    }
}
