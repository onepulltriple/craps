using System.Collections.ObjectModel;
using CrapsLibrary;
using CrapsLibrary.Bets;
namespace CrapsTableWPF.ViewModels
{
    public class CrapsTableViewModel : ViewModelBase
    {
        //public ObservableCollection<PlayerSlotViewModel> Slots { get; }

        private readonly CrapsTable _model;

        public string TableMinimum => _model.tableMinimum.ToString();

        // bindable properties
        //private string _tableMinimum;

        //public string TableMinimum
        //{
        //    get => _tableMinimum;
        //    private set
        //    {
        //        _tableMinimum = value;
        //        OnPropertyChanged(nameof(TableMinimum));
        //    }
        //}

        public CrapsTableViewModel(CrapsTable crapsTable)
        {
            _model = crapsTable;
        }
    }
}
