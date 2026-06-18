using CrapsLibrary.Bets;

namespace CrapsLibrary
{
    public class CrapsTable : ObservableObject
    {
        public const uint absoluteTableMinimum = 5;
        public const uint absoluteTableMaximum = 5000;
        private const int slotCount = 8;
        private const int maxLengthOfPlayerName = 40;

        // Table Accessories ///////////////////////////////////////////////
        public Puck puck;

        // A scoreboard is bound to a crapsTable by virtue of the crapsTable's puck's delegate methods,
        // which are set in the puck's constructor. The scoreboard itself does not needs a crapsTable reference
        // because the scoreboard does not need to refer to any crapsTable members.
        public Scoreboard scoreboard = new Scoreboard(); 

        public GameEventFeed gameEventFeed = new GameEventFeed();

        // Player information ///////////////////////////////////////////////
        public Player?[] Slots { get; } = new Player?[slotCount];

        public IEnumerable<Player> Players => Slots.OfType<Player>();

        // Properties
        public uint tableMinimum;

        public uint tableMaximum;

        // Observeable properties
        private int _currentPlayerIndex = -1;
        public int CurrentPlayerIndex
        {
            get => _currentPlayerIndex;
            set
            {
                if (_currentPlayerIndex != value)
                {
                    _currentPlayerIndex = value;
                    OnPropertyChanged(nameof(CurrentPlayerIndex));
                }
            }
        }


        // Implementation ///////////////////////////////////////////////
        public CrapsTable(uint tableMinimum, uint tableMaximum)
        {
            this.tableMinimum = tableMinimum;
            this.tableMaximum = tableMaximum;
            this.puck = new Puck(this);


            #region temp setup for testing

            Player player1 = new Player("Chase", 1000);
            Slots[0] = player1;
            _currentPlayerIndex = 0;
            foreach (betType bet in Enum.GetValues<betType>())
            {
                Result<Bet> newBetResult = BetFactory.CreateBet(this, player1, bet, 10);

                if (!newBetResult.Success)
                    return;

                player1.PlayerBetList.Add(newBetResult.Value);
            }

            //player1.name = "Chaser";
            //Slots[1] = new Player("Christian", 200);

            #endregion
        }

        public static Result<uint> ValidateCrapsTableMinimum(string? inputToCheck)
        {
            uint checkedInput;
            bool isUInt = uint.TryParse(inputToCheck, out checkedInput);

            if (!isUInt || checkedInput < absoluteTableMinimum || checkedInput % 5 != 0)
                return Result<uint>.Fail("Please input a whole number that is greater than or equal to 5 and also a multiple of 5");

            if (checkedInput > absoluteTableMaximum)
                return Result<uint>.Fail($"The table minimum must not exceed {absoluteTableMaximum}");

            return Result<uint>.Pass(checkedInput, $"The table minimum has been set to {checkedInput}.");
        }

        public static Result<uint> ValidateUserInputUInt(string? inputToCheck)
        {
            uint checkedInput;
            bool isUInt = uint.TryParse(inputToCheck, out checkedInput);

            if (!isUInt)
                return Result<uint>.Fail("Please enter a positive whole number."); 

            return Result<uint>.Pass(checkedInput);
        }

        // TODO validate player starting purse (e.g. no higher starting purse than table maximum)

        public static Result<string> ValidateUserInputPlayerName(string? inputToCheck)
        {
            if (inputToCheck is null || inputToCheck.Length > maxLengthOfPlayerName)
            {
                return Result<string>.Fail($"Please enter a string whose length is less than {maxLengthOfPlayerName + 1} characters.");
            }
            return Result<string>.Pass(inputToCheck);
        }

        public Result<bool> AddPlayerToNextFreeSlot(Player playerToAdd)
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                if (Slots[i] is null)
                {
                    Slots[i] = playerToAdd;

                    if (CurrentPlayerIndex == -1) // initialize the first added player as the current player
                        CurrentPlayerIndex = i;

                    return Result<bool>.Pass(true, $"{playerToAdd.Name} was created and added to seat {i + 1}.");
                }
            }

            return Result<bool>.Fail("Table is full!");
        }

        public Result<bool> InsertPlayerAtSlot(int slotIndex, Player playerToInsert)
        {
            if (slotIndex < 0  || slotIndex >= Slots.Length)
                return Result<bool>.Fail($"Slot index must be between 0 and {Slots.Length - 1}.");

            if (Slots[slotIndex] is not null)
                return Result<bool>.Fail($"Slot {slotIndex + 1} is already occupied.");

            Slots[slotIndex] = playerToInsert;

            if (CurrentPlayerIndex == -1) // the first inserted player becomes the current player
                CurrentPlayerIndex = slotIndex;

            return Result<bool>.Pass(true, $"{playerToInsert.Name} has been placed at seat {slotIndex + 1} with a purse of {playerToInsert.Purse}.");
        }

        public Result<Player> GetCurrentPlayer()
        {
            if (CurrentPlayerIndex < 0 || CurrentPlayerIndex >= Slots.Length)
                return Result<Player>.Fail("There is no current player.");

            Player? player = Slots[CurrentPlayerIndex];

            if (player is null)
                return Result<Player>.Fail("There is no current player.");

            return Result<Player>.Pass(player);
        }

        public Result<bool> RemovePlayer(Player playerToRemove)
        {
            // TODO close all bets for this player (state if the player owes money)
            // TODO remove all bets' subscriptions

            for (int i = 0; i < Slots.Length; i++)
            {
                if (Slots[i] == playerToRemove)
                {
                    Slots[i] = null;

                    // if table is now empty
                    if (!Players.Any())
                    {
                        CurrentPlayerIndex = -1;
                        return Result<bool>.Pass(true, $"{playerToRemove.Name} has been removed. The table is now empty.");
                    }

                    // if current player is removed, advance turn
                    if (i == CurrentPlayerIndex)
                    {
                        Result<bool> nextTurnResult = NextTurn();

                        if (!nextTurnResult.Success)
                            return Result<bool>.Fail(nextTurnResult.Messages.ToArray());

                        return Result<bool>.Pass(
                            true,
                            new[]
                            {
                                $"{playerToRemove.Name} has been removed."
                            }
                            .Concat(nextTurnResult.Messages)
                            .ToArray()
                            );
                    }

                    return Result<bool>.Pass(true, $"{playerToRemove.Name} has been removed.");
                }
            }

            return Result<bool>.Fail($"{playerToRemove.Name} was not found at the table.");
        }

        public Result<bool> NextTurn()
        {
            if (!Players.Any())
                return Result<bool>.Fail("No players at the table.");

            int startIndex = CurrentPlayerIndex;

            do
            {
                CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Slots.Length;
            } 
            while (Slots[CurrentPlayerIndex] is null && CurrentPlayerIndex != startIndex);

            return Result<bool>.Pass(true, $"{Slots[CurrentPlayerIndex]!.Name}'s turn!");
        }

        public void RollDiceAndAnnounceOutcomes()
        {
            (byte outcome01, byte outcome02) = Die.RollDice(6, 6);

            this.gameEventFeed.Add(
                $"{outcome01}, {outcome02} --> {outcome01 + outcome02}",
                GameEventType.DiceRoll
                );

            this.scoreboard.UpdateScoreboardAndPublishOutcomes(outcome01, outcome02);
        }
    }
}
