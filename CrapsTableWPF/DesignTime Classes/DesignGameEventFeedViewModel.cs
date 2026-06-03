using CrapsLibrary;
using System.Collections.ObjectModel;

namespace CrapsTableWPF.DesignTime_Classes
{
    public class DesignGameEventFeedViewModel
    {
        public ObservableCollection<GameEvent> DisplayedEvents { get; set; } = new()
        {
            // Message
            new GameEvent("Welcome to the table.", GameEventType.Message),

            // Warning
            new GameEvent("Minimum bet is $10.", GameEventType.Warning),

            // DiceRoll
            new GameEvent("Roll: 3 + 4 = 7", GameEventType.DiceRoll),

            // Outcome
            new GameEvent("Pass Line wins.", GameEventType.Outcome, true),
            new GameEvent("Don't Pass loses.", GameEventType.Outcome, true),

            // Another roll
            new GameEvent("Roll: 5 + 5 = 10", GameEventType.DiceRoll),

            // Outcome
            new GameEvent("Point established: 10", GameEventType.Outcome, true),

            // Message
            new GameEvent("Shooter continues.", GameEventType.Message),

            // Another roll
            new GameEvent("Roll: 6 + 4 = 10", GameEventType.DiceRoll),

            // Outcome
            new GameEvent("Point made!", GameEventType.Outcome, true),
            new GameEvent("Pass Line wins.", GameEventType.Outcome, true),

            // Warning
            new GameEvent("Player attempted an invalid bet.", GameEventType.Warning),
        };

        public DesignGameEventFeedViewModel() { }
    }
}
