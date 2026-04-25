using CrapsLibrary;

namespace CrapsTableWPF
{
    public class DealerWPF // translation layer
    {
        // This class manages:
        //  - the table's slots (which player is sitting where) and thereby the ordering of player interaction
        // who is the current roller?
        // the players themselves keep track of their bets and their purses



        public Player currentPlayer;

        public Player currentRoller;

        public List<Player> orderingOfPlayers;

        public DealerWPF()
        {
            // should store temporary information about the game
            this.currentPlayer = new Player("chase");
            this.currentRoller = new Player("chase2");

        }

        private readonly Player?[] playerSlots = new Player?[8]; // TODO do not hard code this number

        public void PlacePlayer(int slotIndex, Player player)
        {
            playerSlots[slotIndex] = player;
        }

        public void RemovePlayer(int slotIndex)
        {
            playerSlots[slotIndex] = null;
        }

        public int GetInsertionIndex(int slotIndex, List<Player> players)
        {
            // Find nearest occupied slot BEFORE this one (searching counter-clockwise)
            for (int i = 1; i <= playerSlots.Length; i++)
            {
                int check = (slotIndex - 1 + playerSlots.Length) % playerSlots.Length;

                if (playerSlots[check] != null)
                {
                    var player = playerSlots[check];
                    return players.IndexOf(player) + 1;
                }
            }

            return 0; // first player
        }

        // int index = slotManager.GetInsertionIndex(slotC, CrapsTable (or Table01).Players);
        // CrapsTable.InsertPlayers(index, newPlayer);
        // slotManager.PlacePlayer(slotC, newPlayer);
    }
}
