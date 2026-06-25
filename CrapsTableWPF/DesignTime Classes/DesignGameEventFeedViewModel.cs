using CrapsLibrary;
using CrapsTableWPF.ViewModels;
using System.Collections.ObjectModel;

namespace CrapsTableWPF.DesignTime_Classes
{
    public class DesignGameEventFeedViewModel
    {
        public ObservableCollection<EventChunkViewModel> DisplayedChunks { get; }
        public DesignGameEventFeedViewModel()
        {
            DisplayedChunks = new ObservableCollection<EventChunkViewModel>();

            // Newest: Invalid bet warning
            var invalidBetChunk = new EventChunkViewModel();
            invalidBetChunk.Events.Add(
                new GameEvent(
                    "Player attempted an invalid bet.",
                    GameEventType.Warning));

            DisplayedChunks.Add(invalidBetChunk);

            // Point made
            var pointMadeChunk = new EventChunkViewModel();
            pointMadeChunk.Events.Add(
                new GameEvent(
                    "Roll: 6 + 4 = 10",
                    GameEventType.DiceRoll));

            pointMadeChunk.Events.Add(
                new GameEvent(
                    "Point made!",
                    GameEventType.Outcome,
                    true));

            pointMadeChunk.Events.Add(
                new GameEvent(
                    "Pass Line wins.",
                    GameEventType.Outcome,
                    true));

            DisplayedChunks.Add(pointMadeChunk);

            // Point established
            var point10Chunk = new EventChunkViewModel();
            point10Chunk.Events.Add(
                new GameEvent(
                    "Roll: 5 + 5 = 10",
                    GameEventType.DiceRoll));

            point10Chunk.Events.Add(
                new GameEvent(
                    "Point established: 10",
                    GameEventType.Outcome,
                    true));

            point10Chunk.Events.Add(
                new GameEvent(
                    "Shooter continues.",
                    GameEventType.Message,
                    true));

            DisplayedChunks.Add(point10Chunk);

            // Come-out roll 7
            var roll7Chunk = new EventChunkViewModel();
            roll7Chunk.Events.Add(
                new GameEvent(
                    "Roll: 3 + 4 = 7",
                    GameEventType.DiceRoll));

            roll7Chunk.Events.Add(
                new GameEvent(
                    "Pass Line wins.",
                    GameEventType.Outcome,
                    true));

            roll7Chunk.Events.Add(
                new GameEvent(
                    "Don't Pass loses.",
                    GameEventType.Outcome,
                    true));

            DisplayedChunks.Add(roll7Chunk);

            // Minimum bet warning
            var warningChunk = new EventChunkViewModel();
            warningChunk.Events.Add(
                new GameEvent(
                    "Minimum bet is $10.",
                    GameEventType.Warning));

            DisplayedChunks.Add(warningChunk);

            // Oldest: Welcome message
            var welcomeChunk = new EventChunkViewModel();
            welcomeChunk.Events.Add(
                new GameEvent(
                    "Welcome to the table.",
                    GameEventType.Message));

            DisplayedChunks.Add(welcomeChunk);
        }
    }
}
