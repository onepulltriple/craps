namespace CrapsLibrary
{
    public enum GameEventType
    {
        DiceRoll,
        RuleOutcome,
        Info,
        Warning
    }

    public class GameEvent // What happened?
    {
        public GameEventType Type { get; }

        public string Text { get; }

        public GameEvent(string text, GameEventType type = GameEventType.Info)
        {
            Text = text; 
            Type = type;
        }
    }
}
