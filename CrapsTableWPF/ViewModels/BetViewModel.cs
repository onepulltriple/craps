using CrapsLibrary.Bets;

namespace CrapsTableWPF.ViewModels
{
    // here is where the visibility of chips, etc. will be dictated by the bet's state
    public class BetViewModel : ViewModelBase
    {
        private Bet _model;

        public string Name => _model.name;



        public string BetWorkingState => _model.BetWorkingState; // TODO this should be a dictionary or enum


        //public uint UnitOfBet => _model.unitOfBet;

        public uint Commitment => _model.commitment;



        public BetViewModel(Bet bet)
        {
            this._model = bet;


        }
    }
}
