using CrapsLibrary.Bets;

namespace CrapsTableWPF.ViewModels
{
    public class BetViewModel : ViewModelBase
    {
        private Bet _model;

        public string BetWorkingState => _model.BetWorkingState; // TODO this should be a dictionary or enum

        public string Name => _model.name;

        //public uint UnitOfBet => _model.unitOfBet;

        public uint Commitment => _model.commitment;



        public BetViewModel(Bet bet)
        {
            this._model = bet;


        }
    }
}
