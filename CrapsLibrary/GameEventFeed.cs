namespace CrapsLibrary
{
    public class GameEventFeed // Game timeline (events that affect all players)
    {
        private readonly LinkedList<GameEvent> _events = new();

        public IReadOnlyCollection<GameEvent> Events => _events;

        public event Action<GameEvent>? EventAdded; // an exposed event
        // TODO determine if this breaks MVVM principles
        public GameEventFeed() { }

        public void Add(GameEvent gameEvent)
        {
            _events.AddFirst(gameEvent); // newest first

            EventAdded?.Invoke(gameEvent); // broadcasts "a new game event was added"
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">The information to be displayed to the user.</param>
        /// <param name="type"></param>
        /// <param name="isContinuation">If true, this information will be treated as a continuation of the preceding information.</param>
        public void Add(string text, GameEventType type = GameEventType.Message, bool isContinutation = false)
        {
            Add(new GameEvent(text, type, isContinutation));
        }
    }
}
