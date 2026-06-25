using CrapsLibrary;
using System.Collections.ObjectModel;

namespace CrapsTableWPF.ViewModels
{
    public class EventChunkViewModel
    {
        public ObservableCollection<GameEvent> Events { get; } = new();

        public EventChunkViewModel() { }

        public EventChunkViewModel(IEnumerable<GameEvent> gameEvents)
        {
            foreach (var gameEvent in gameEvents)
            {
                Events.Add(gameEvent);
            }
        }
    }
}
