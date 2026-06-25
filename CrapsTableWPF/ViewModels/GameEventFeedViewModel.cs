using CrapsLibrary;
using System.Collections.ObjectModel;

namespace CrapsTableWPF.ViewModels
{
    public class GameEventFeedViewModel : ViewModelBase
    {
        private const int eventsCount = 12;
        
        private readonly GameEventFeed _model;

        public ObservableCollection<EventChunkViewModel> DisplayedChunks { get; } = new();

        public GameEventFeedViewModel(GameEventFeed model)
        {
            this._model = model;

            BuildInitialChunks();

            _model.EventAdded += OnEventAdded;
        }

        public void OnEventAdded(GameEvent gameEvent)
        {
            if (!gameEvent.IsContinuation)
            {
                var chunk = new EventChunkViewModel();
                chunk.Events.Add(gameEvent);

                DisplayedChunks.Insert(0, chunk);
            }
            else if (DisplayedChunks.Count > 0)
            {
                DisplayedChunks[0].Events.Add(gameEvent);
            }

            while (DisplayedChunks.Count > eventsCount)
            {
                DisplayedChunks.RemoveAt(DisplayedChunks.Count - 1);
            }
        }

        private void BuildInitialChunks()
        {
            EventChunkViewModel? currentChunk = null;

            foreach (var ev in _model.Events.Reverse())
            {
                if (!ev.IsContinuation)
                {
                    currentChunk = new EventChunkViewModel();
                    currentChunk.Events.Add(ev);

                    DisplayedChunks.Insert(0, currentChunk);
                }
                else if(currentChunk != null)
                {
                    currentChunk.Events.Add(ev);
                }
            }

            while (DisplayedChunks.Count > eventsCount)
            {
                DisplayedChunks.RemoveAt(DisplayedChunks.Count - 1);
            }
        }
    }
}
