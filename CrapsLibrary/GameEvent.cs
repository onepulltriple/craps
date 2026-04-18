namespace CrapsLibrary
{
    public enum GameEventType
    {
        DiceRoll,       // starts a block
        Outcome,        // indented under last DiceRoll
        Message,        // normal line
        Warning         // normal line (maybe prefixed later)
    }

    public class GameEvent // What happened?
    {
        public GameEventType Type { get; }

        public string Text { get; }

        public bool IsContinuation { get; }

        public GameEvent(string text, GameEventType type = GameEventType.Message, bool isContinuation = false)
        {
            Text = text; 
            Type = type;
            IsContinuation = isContinuation;
        }
    }
}
