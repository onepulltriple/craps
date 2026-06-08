namespace CrapsTableWPF.ViewModels
{
    public class CreateOrUpdatePlayerDialogViewModel : ViewModelBase
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

        private uint _purse;
        public uint Purse
        {
            get => _purse;
            set
            {
                _purse = value;
                OnPropertyChanged(nameof(Purse));
            }
        }
    }
}
