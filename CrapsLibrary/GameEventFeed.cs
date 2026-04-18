namespace CrapsLibrary
{
    public class GameEventFeed // Game timeline (events that affect all players)
    {
        private readonly LinkedList<GameEvent> _events = new();

        public IReadOnlyCollection<GameEvent> Events => _events;

        public GameEventFeed() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameEvent"></param>
        public void Add(GameEvent gameEvent)
        {
            //_events.AddFirst(gameEvent); // newest first
            _events.AddLast(gameEvent); // newest first
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        public void Add(string text, GameEventType type = GameEventType.Message)
        {
            Add(new GameEvent(text, type));
        }
    }
}
