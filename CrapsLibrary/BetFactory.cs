namespace CrapsLibrary
{
    public enum betType
    { 
        Aces,  
        Hard_4,
        Hard_6,
        Hard_8,
        Hard_10,
        Hard_12,
        PassBet,
        PlaceBet_4,
        PlaceBet_5,
        PlaceBet_6,
        PlaceBet_8,
        PlaceBet_9,
        PlaceBet_10
    }

    public class BetFactory
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
                {betType.Hard_4,        (7, 1)},
                {betType.Hard_6,        (9, 1)},
                {betType.Hard_8,        (9, 1)},
                {betType.Hard_10,       (7, 1)},
                {betType.Hard_12,      (30, 1)},
                {betType.PassBet,       (5, 5)}, // pays 1:1
                {betType.PlaceBet_4,    (9, 5)},
                {betType.PlaceBet_5,    (7, 5)},
                {betType.PlaceBet_6,    (7, 6)},
                {betType.PlaceBet_8,    (7, 6)},
                {betType.PlaceBet_9,    (7, 5)},
                {betType.PlaceBet_10,   (9, 5)}
            };

        public Bet? CreateBet(Player player, betType playerBetType, uint amountThrownAtBet)
        {
            if (player.purse < amountThrownAtBet) // the player cannot bet more than they have
                return null;

            // determine betting units
            uint unitOfBet = 
                CrapsTable.tableMinimum / 
                CrapsTable.absTableMinimum * 
                betPayoutRatios[playerBetType].payoutDenominator;

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

            Bet? tempBet;

            switch (playerBetType)
            {
                case betType.Aces: // wins on 1,1 (snake eyes)
                    tempBet = new HardWayBet(player, playerBetType.ToString(), amountToBet, new List<int>{ 2 }, payout);
                    break;

                case betType.Hard_4: // wins on 2,2
                    tempBet = new HardWayBet(player, playerBetType.ToString(), amountToBet, new List<int> { 4 }, payout);
                    break;

                case betType.Hard_6: // wins on 3,3
                    tempBet = new HardWayBet(player, playerBetType.ToString(), amountToBet, new List<int> { 6 }, payout);
                    break;

                case betType.Hard_8: // wins on 4,4
                    tempBet = new HardWayBet(player, playerBetType.ToString(), amountToBet, new List<int> { 8 }, payout);
                    break;

                case betType.Hard_10: // wins on 5,5
                    tempBet = new HardWayBet(player, playerBetType.ToString(), amountToBet, new List<int> { 10 }, payout);
                    break;

                case betType.Hard_12: // wins on 6,6
                    tempBet = new HardWayBet(player, playerBetType.ToString(), amountToBet, new List<int> { 12 }, payout);
                    break;

                case betType.PassBet: // wins on natural passes, winning based on point behavior, which is handled internally
                    tempBet = new PassBet(player, playerBetType.ToString(), amountToBet, new List<int> { 0 }, payout);
                    break;                                                                       
                                                                                                 
                case betType.PlaceBet_4: // wins when puck is on, then this number is rolled     
                    tempBet = new PassBet(player, playerBetType.ToString(), amountToBet, new List<int> { 4 }, payout);
                    break;                                                                       
                                                                                                 
                case betType.PlaceBet_5: // wins when puck is on, then this number is rolled     
                    tempBet = new PassBet(player, playerBetType.ToString(), amountToBet, new List<int> { 5 }, payout);
                    break;                                                                       
                                                                                                 
                case betType.PlaceBet_6: // wins when puck is on, then this number is rolled     
                    tempBet = new PassBet(player, playerBetType.ToString(), amountToBet, new List<int> { 6 }, payout);
                    break;                                                                       
                                                                                                 
                case betType.PlaceBet_8: // wins when puck is on, then this number is rolled     
                    tempBet = new PassBet(player, playerBetType.ToString(), amountToBet, new List<int> { 8 }, payout);
                    break;                                                                       
                                                                                                 
                case betType.PlaceBet_9: // wins when puck is on, then this number is rolled     
                    tempBet = new PassBet(player, playerBetType.ToString(), amountToBet, new List<int> { 9 }, payout);
                    break;                                                                       
                                                                                                 
                case betType.PlaceBet_10: // wins when puck is on, then this number is rolled    
                    tempBet = new PassBet(player, playerBetType.ToString(), amountToBet, new List<int> { 10 }, payout);
                    break;

                default:
                    tempBet = null;
                    break;
            }
            return tempBet;
        }
    }
}
