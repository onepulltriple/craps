namespace CrapsLibrary
{
    public class GameEventFeed // Game timeline (events that affect all players)
    {
        private readonly LinkedList<GameEvent> _events = new();

        public IReadOnlyCollection<GameEvent> Events => _events;

        public event Action<GameEvent>? EventAdded; // an exposed event
        
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
        /// <param name="gameEventType"></param>
        /// <param name="isContinuation">If true, this information will be treated as a continuation of the preceding information.</param>
        public void Add(string text, GameEventType gameEventType = GameEventType.Message, bool isContinutation = false)
        {
            Add(new GameEvent(text, gameEventType, isContinutation));
        }

        /// <summary>
        /// Adds the first message to the game event feed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resultOfAttempt">An instance of the Result<T> class.</param>
        /// <param name="gameEventType"></param>
        /// <param name="isContinutation"></param>
        public void AddSingleLine<T>(Result<T> resultOfAttempt, GameEventType gameEventType = GameEventType.Message, bool isContinutation = false)
        {
            if (resultOfAttempt.Messages.Count == 0)
                return;

            Add(
                resultOfAttempt.Messages[0],
                gameEventType,
                isContinutation
                );
        }

        /// <summary>
        /// Adds all messages to the game event feed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resultOfAttempt">An instance of the Result<T> class.</param>
        /// <param name="gameEventType"></param>
        /// <param name="isContinutation"></param>
        public void AddMultiLine<T>(Result<T> resultOfAttempt, GameEventType gameEventType = GameEventType.Message, bool isContinutation = false)
        {
            if (resultOfAttempt.Messages.Count == 0)
                return;

            foreach (string message in resultOfAttempt.Messages)
            {
                Add(
                    message,
                    gameEventType,
                    isContinutation
                    );
            }
        }
    }
}
