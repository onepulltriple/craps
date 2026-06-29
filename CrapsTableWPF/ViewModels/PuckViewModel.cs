using CrapsLibrary;

namespace CrapsTableWPF.ViewModels
{
    public class PuckViewModel
    {
        private readonly Puck _model;

        public Puck Model => _model;

        public PuckViewModel(Puck puck)
        {
            this._model = puck;
        }
    }
}
