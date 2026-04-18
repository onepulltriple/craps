using System.Reflection.Metadata.Ecma335;

namespace CrapsLibrary.Bets
{
    public enum betType // a container for constants with extra properties built in
    {
        Aces,
        Hard_04,
        Hard_06,
        Hard_08,
        Hard_10,
        Hard_12,
        PassBet,
        PlaceBet_04,
        PlaceBet_05,
        PlaceBet_06,
        PlaceBet_08,
        PlaceBet_09,
        PlaceBet_10
    }

    public static class BetFactory
    {
        /// <summary>
        /// The bet factory stores all lookup information for bet creation including:
        ///  - payout ratios
        ///  - bet names
        ///  - dice sums which result in a win condition for each bet (called winningTotals)
        ///  
        /// Winning totals are handed over on bet creation because for most Bet child classes, e.g. hard way bets, 
        /// the identifying characteristic is the dice sum that would result in the player winning.
        /// For bets where this is not the case, winning totals are defined on the child class.
        /// Losing conditions are always defined on the child class.
        /// 
        /// The bet factory also calculates several values needed when creating a bet including:
        ///  - minimum bet amounts (calculated based on several factors)
        ///  - payout amounts
        ///  
        /// The bet factory also distributes created bets to players.
        /// 
        /// </summary>
        /// <param name="playerBetType">Should be <see cref="betType"/></param>
        /// <returns></returns>

        public static Dictionary<betType, (uint payoutNumerator, uint payoutDenominator)> betPayoutRatios =
            new Dictionary<betType, (uint payoutNumerator, uint payoutDenominator)>()
            {
                // For bets whose minimum matches the tables minimum, payoutDenominator must equal 5.

                {betType.Aces,         (30, 1)},
                {betType.Hard_04,       (7, 1)},
                {betType.Hard_06,       (9, 1)},
                {betType.Hard_08,       (9, 1)},
                {betType.Hard_10,       (7, 1)},
                {betType.Hard_12,      (30, 1)},
                {betType.PassBet,       (5, 5)}, // pays 1:1
                {betType.PlaceBet_04,   (9, 5)},
                {betType.PlaceBet_05,   (7, 5)},
                {betType.PlaceBet_06,   (7, 6)},
                {betType.PlaceBet_08,   (7, 6)},
                {betType.PlaceBet_09,   (7, 5)},
                {betType.PlaceBet_10,   (9, 5)}
            };

        public static Result<Bet> CreateBet(CrapsTable crapsTable, Player player, betType playerBetType, uint amountThrownAtBet)
        {
            if (player.purse < amountThrownAtBet) // the player cannot bet more than they have
                return Result<Bet>.Fail("Test01", "Test02");

            // determine betting units
            uint unitOfBet =
                crapsTable.tableMinimum /
                crapsTable.absTableMinimum * 
                betPayoutRatios[playerBetType].payoutDenominator;

            if (amountThrownAtBet < unitOfBet) // the player player cannot cover at least one bet of that type (e.g. throwing 5 credits at a Place_6)
                return Result<Bet>.Fail("Test03", "Test04");

            uint countOfUnitsToBet = amountThrownAtBet / unitOfBet; // the quotient
            uint amountToBet = countOfUnitsToBet * unitOfBet; // quotient times units
            uint amountChangeToReturn = amountThrownAtBet - amountToBet; // remainder to return to player

            // charge player for the bet
            player.purse -= amountThrownAtBet;
            player.purse += amountChangeToReturn;

            // calculate potential payout
            uint payout = 
                amountToBet *
                betPayoutRatios[playerBetType].payoutNumerator / 
                betPayoutRatios[playerBetType].payoutDenominator;

            Bet? tempBet = null;
            string tempBetName = playerBetType.ToString();

            switch (playerBetType)
            {
                case betType.Aces: // wins on 1,1 (snake eyes)
                    tempBet = new HardWayBet(crapsTable, player, tempBetName, amountToBet, new List<int>{ 2 }, payout);
                    break;

                case betType.Hard_04: // wins on 2,2
                    tempBet = new HardWayBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 4 }, payout);
                    break;

                case betType.Hard_06: // wins on 3,3
                    tempBet = new HardWayBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 6 }, payout);
                    break;

                case betType.Hard_08: // wins on 4,4
                    tempBet = new HardWayBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 8 }, payout);
                    break;

                case betType.Hard_10: // wins on 5,5
                    tempBet = new HardWayBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 10 }, payout);
                    break;

                case betType.Hard_12: // wins on 6,6
                    tempBet = new HardWayBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 12 }, payout);
                    break;

                case betType.PassBet: // wins on natural passes, winning based on point behavior, which is handled internally
                    tempBet = new PassBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 0 }, payout);
                    break;                                                                       
                                                                                                 
                case betType.PlaceBet_04: // wins when puck is on, then this number is rolled     
                    if (PlaceBet.IsPlaceBetAllowed(crapsTable, player, tempBetName))
                        tempBet = new PlaceBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 4 }, payout);
                    break;                                                                       
                                                                                                 
                case betType.PlaceBet_05: // wins when puck is on, then this number is rolled     
                    if (PlaceBet.IsPlaceBetAllowed(crapsTable, player, tempBetName))
                        tempBet = new PlaceBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 5 }, payout);
                    break;                                                                       
                                                                                                 
                case betType.PlaceBet_06: // wins when puck is on, then this number is rolled     
                    if (PlaceBet.IsPlaceBetAllowed(crapsTable, player, tempBetName)) 
                        tempBet = new PlaceBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 6 }, payout);
                    break;                                                                       
                                                                                                 
                case betType.PlaceBet_08: // wins when puck is on, then this number is rolled     
                    if (PlaceBet.IsPlaceBetAllowed(crapsTable, player, tempBetName)) 
                        tempBet = new PlaceBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 8 }, payout);
                    break;                                                                       
                                                                                                 
                case betType.PlaceBet_09: // wins when puck is on, then this number is rolled     
                    if (PlaceBet.IsPlaceBetAllowed(crapsTable, player, tempBetName)) 
                        tempBet = new PlaceBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 9 }, payout);
                    break;                                                                       
                                                                                                 
                case betType.PlaceBet_10: // wins when puck is on, then this number is rolled    
                    if (PlaceBet.IsPlaceBetAllowed(crapsTable, player, tempBetName))
                        // TODO notify player why creating a place bet didn't work
                        tempBet = new PlaceBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 10 }, payout);
                    break;

                default:
                    return Result<Bet>.Fail("Unspecified bet attempted.");
            }
            if (tempBet == null)
                return Result<Bet>.Fail("Somehow tempBet is null when exiting the factory."); // bc of e.g. place bets

            return Result<Bet>.Pass(tempBet, "Good job");
        }
    }
}
