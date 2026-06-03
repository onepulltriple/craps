using CrapsLibrary;
using System.Collections.ObjectModel;

namespace CrapsTableWPF.ViewModels
{
    public class GameEventFeedViewModel : ViewModelBase
    {
        private const int eventsCount = 12;
        
        private readonly GameEventFeed _model;

        public ObservableCollection<GameEvent> DisplayedEvents { get; } = new();

        public GameEventFeedViewModel(GameEventFeed model)
        {
            this._model = model;

            foreach (var ev in _model.Events.Take(eventsCount).Reverse())
            {
                DisplayedEvents.Add(ev);
            }

            _model.EventAdded += OnEventAdded;
        }

        public void OnEventAdded(GameEvent gameEvent)
        {
            DisplayedEvents.Insert(0, gameEvent);

            while (DisplayedEvents.Count > eventsCount)
            {
                DisplayedEvents.RemoveAt(DisplayedEvents.Count - 1);
            }
        }

    }
}
