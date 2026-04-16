namespace CrapsLibrary
{
    public class MessageFeed // UI notifications
    {
        private readonly LinkedList<string> _messages = new();

        public IReadOnlyCollection<string> Messages => _messages;

        public void Add(string message)
        {
            _messages.AddFirst(message); // newest at top
        }

        public void AddMessages(IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                Add(message);
            }
        }
    }
}
