namespace CrapsLibrary
{
    public enum GameEventType
    {
        DiceRoll,       // states the outcomes of the two die rolls (D6) and their sum
        Outcome,        // indented under last DiceRoll
        Message,        
        Warning         
    }

    public class GameEvent 
    {
        public GameEventType Type { get; }

        public string Text { get; } // what happened?

        public bool IsContinuation { get; } // does this information belong with another?

        public GameEvent(string text, GameEventType type = GameEventType.Message, bool isContinuation = false)
        {
            Text = text; 
            Type = type;
            IsContinuation = isContinuation;
        }
    }
}
