using CrapsLibrary;
using System.Collections.ObjectModel;

namespace CrapsTableWPF.ViewModels
{
    public class GameEventFeedViewModel : ViewModelBase
    {
        private readonly GameEventFeed _model;

        public ObservableCollection<GameEvent> DisplayedEvents { get; } = new();

        public GameEventFeedViewModel(GameEventFeed model)
        {
            this._model = model;

            foreach (var ev in _model.Events.Take(10).Reverse())
            {
                DisplayedEvents.Add(ev);
            }

            _model.EventAdded += OnEventAdded;
        }

        public void OnEventAdded(GameEvent gameEvent)
        {
            DisplayedEvents.Insert(0, gameEvent);

            while (DisplayedEvents.Count > 10)
            {
                DisplayedEvents.RemoveAt(DisplayedEvents.Count - 1);
            }
        }

    }
}
