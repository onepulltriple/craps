namespace CrapsLibrary.Bets
{
    public enum betType // a container for constants with extra properties built in
    {
        Hard_02,
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
        
        public record BetInfoRecord(uint payoutNumerator, uint payoutDenominator, string Name, List<int> winningTotals);

        public static Dictionary<betType, BetInfoRecord> BetDefinitions =
            new()
            {
                // for bets whose minimum commitment match the tables minimum, payoutDenominator must equal 5
                // 0 means winningTotals handled internally

                { betType.Hard_02,       new( 30, 1, "Aces"           , new List<int>{ 2 } ) },
                { betType.Hard_04,       new(  7, 1, "Hard 4"         , new List<int>{ 4 } ) },
                { betType.Hard_06,       new(  9, 1, "Hard 6"         , new List<int>{ 6 } ) },
                { betType.Hard_08,       new(  9, 1, "Hard 8"         , new List<int>{ 8 } ) },
                { betType.Hard_10,       new(  7, 1, "Hard 10"        , new List<int>{ 10} ) },
                { betType.Hard_12,       new( 30, 1, "Boxcars"        , new List<int>{ 12} ) },
                { betType.PassBet,       new(  5, 5, "Pass Line Bet"  , new List<int>{ 0 } ) }, // pays 1:1
                { betType.PlaceBet_04,   new(  9, 5, "Place Bet 4"    , new List<int>{ 4 } ) },
                { betType.PlaceBet_05,   new(  7, 5, "Place Bet 5"    , new List<int>{ 5 } ) },
                { betType.PlaceBet_06,   new(  7, 6, "Place Bet 6"    , new List<int>{ 6 } ) },
                { betType.PlaceBet_08,   new(  7, 6, "Place Bet 8"    , new List<int>{ 8 } ) },
                { betType.PlaceBet_09,   new(  7, 5, "Place Bet 9"    , new List<int>{ 9 } ) },
                { betType.PlaceBet_10,   new(  9, 5, "Place Bet 10"   , new List<int>{ 10} ) }
            };


        public static Result<Bet> CreateBet(CrapsTable crapsTable, Player player, betType betType, uint amountThrownAtBet)
        {
            if (player.purse < amountThrownAtBet) // the player cannot bet more than they have
                return Result<Bet>.Fail(
                    "The bet amount may not exceed the player's purse amount.", "You can't bet money you don't have!"
                    );

            // determine betting units
            uint unitOfBet =
                crapsTable.tableMinimum /
                CrapsTable.absoluteTableMinimum *
                BetDefinitions[betType].payoutDenominator;

            if (amountThrownAtBet < unitOfBet) // the player player cannot cover at least one bet of that type (e.g. throwing 5 credits at a Place_06)
                return Result<Bet>.Fail(
                    $"The minimum bet amount is {unitOfBet}."
                    );

            uint countOfUnitsToBet = amountThrownAtBet / unitOfBet; // the quotient
            uint amountToBet = countOfUnitsToBet * unitOfBet; // quotient times units
            uint amountChangeToReturn = amountThrownAtBet - amountToBet; // remainder to return to player

            // charge player for the bet
            player.purse -= amountThrownAtBet;
            player.purse += amountChangeToReturn;

            // calculate potential payout
            uint payout =
                amountToBet *
                BetDefinitions[betType].payoutNumerator /
                BetDefinitions[betType].payoutDenominator;

            Bet? tempBet = null;

            Result<bool> check = CheckIfBetAllowed(crapsTable, player, betType);
            if (!check.Success)
                return Result<Bet>.Fail(check.Messages.ToArray());

            switch (betType)
            {
                case betType.Hard_02:
                case betType.Hard_04:
                case betType.Hard_06:
                case betType.Hard_08:
                case betType.Hard_10:
                case betType.Hard_12:
                    tempBet = new HardWayBet(crapsTable, player, betType, amountToBet, BetDefinitions[betType].winningTotals, payout);
                    break;

                case betType.PassBet:
                    tempBet = new PassBet(crapsTable, player, betType, amountToBet, BetDefinitions[betType].winningTotals, payout);
                    break;

                case betType.PlaceBet_04:
                case betType.PlaceBet_05:
                case betType.PlaceBet_06:
                case betType.PlaceBet_08:
                case betType.PlaceBet_09:
                case betType.PlaceBet_10:
                    tempBet = new PlaceBet(crapsTable, player, betType, amountToBet, BetDefinitions[betType].winningTotals, payout);
                    break;

                default:
                    Result<Bet>.Fail("Unspecified bet attempted.");
                    break;
            }

            if (tempBet == null)
                return Result<Bet>.Fail("but why");

            return Result<Bet>.Pass(tempBet, $"great! ---.");
            //return Result<Bet>.Pass(tempBet, $"{tempBet.betOwner.Name} has bet {tempBet.commitment} on ---.");
        }















        //    string tempBetName = betType.ToString();

        //    switch (betType)
        //    {
        //        case betType.Hard_02: // wins on 1,1 (snake eyes)
        //            tempBet = new HardWayBet(crapsTable, player, tempBetName, amountToBet, new List<int>{ 2 }, payout);
        //            break;

        //        case betType.Hard_04: // wins on 2,2
        //            tempBet = new HardWayBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 4 }, payout);
        //            break;

        //        case betType.Hard_06: // wins on 3,3
        //            tempBet = new HardWayBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 6 }, payout);
        //            break;

        //        case betType.Hard_08: // wins on 4,4
        //            tempBet = new HardWayBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 8 }, payout);
        //            break;

        //        case betType.Hard_10: // wins on 5,5
        //            tempBet = new HardWayBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 10 }, payout);
        //            break;

        //        case betType.Hard_12: // wins on 6,6
        //            tempBet = new HardWayBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 12 }, payout);
        //            break;

        //        case betType.PassBet: // wins on natural passes, winning based on point behavior, which is handled internally
        //            tempBet = new PassBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 0 }, payout);
        //            break;                                                                       
                                                                                                 
        //        case betType.PlaceBet_04: // wins when puck is on, then this number is rolled     
        //            if (IsPlaceBetAllowed(crapsTable, player, tempBetName))
        //                tempBet = new PlaceBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 4 }, payout);
        //            else
        //                return Result<Bet>.Fail("The player must have an active pass line bet to access this bet.");
        //            // TODO user feedback should state exactly why creation failed (no pass line bet, vs puck off, point = chosen bet OR just don't offer inelgible bets
        //            break;                                                                       
                                                                                                 
        //        case betType.PlaceBet_05: // wins when puck is on, then this number is rolled     
        //            if (IsPlaceBetAllowed(crapsTable, player, tempBetName))
        //                tempBet = new PlaceBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 5 }, payout);
        //            else
        //                return Result<Bet>.Fail("The player must have an active pass line bet to access this bet.");
        //            break;                                                                       
                                                                                                 
        //        case betType.PlaceBet_06: // wins when puck is on, then this number is rolled     
        //            if (IsPlaceBetAllowed(crapsTable, player, tempBetName)) 
        //                tempBet = new PlaceBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 6 }, payout);
        //            else
        //                return Result<Bet>.Fail("The player must have an active pass line bet to access this bet.");
        //            break;                                                                       
                                                                                                 
        //        case betType.PlaceBet_08: // wins when puck is on, then this number is rolled     
        //            if (IsPlaceBetAllowed(crapsTable, player, tempBetName)) 
        //                tempBet = new PlaceBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 8 }, payout);
        //            else
        //                return Result<Bet>.Fail("The player must have an active pass line bet to access this bet.");
        //            break;                                                                       
                                                                                                 
        //        case betType.PlaceBet_09: // wins when puck is on, then this number is rolled     
        //            if (IsPlaceBetAllowed(crapsTable, player, tempBetName)) 
        //                tempBet = new PlaceBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 9 }, payout);
        //            else
        //                return Result<Bet>.Fail("The player must have an active pass line bet to access this bet.");
        //            break;                                                                       
                                                                                                 
        //        case betType.PlaceBet_10: // wins when puck is on, then this number is rolled    
        //            if (IsPlaceBetAllowed(crapsTable, player, tempBetName))
        //                tempBet = new PlaceBet(crapsTable, player, tempBetName, amountToBet, new List<int> { 10 }, payout);
        //            else
        //                return Result<Bet>.Fail("The player must have an active pass line bet to access this bet.");
        //            break;

        //        default:
        //            return Result<Bet>.Fail("Unspecified bet attempted.");
        //    }

        //    return Result<Bet>.Pass(tempBet, $"{tempBet.betOwner.Name} has bet {tempBet.commitment} on {tempBet.betName}.");
        //}

        public static Result<bool> CheckIfBetAllowed(CrapsTable crapsTable, Player playerToCheck, betType playerBetType)
        {
            switch (playerBetType)
            {
                case betType.PlaceBet_04:
                case betType.PlaceBet_05:
                case betType.PlaceBet_06:
                case betType.PlaceBet_08:
                case betType.PlaceBet_09:
                case betType.PlaceBet_10:
                    return IsPlaceBetAllowed(crapsTable, playerToCheck, playerBetType);

                case betType.PassBet:
                    if (crapsTable.puck.IsOn)
                        return Result<bool>.Fail("Pass Line bets can only be made before a point is established.");
                    return Result<bool>.Pass(true);

                case betType.Hard_02:
                case betType.Hard_04:
                case betType.Hard_06:
                case betType.Hard_08:
                case betType.Hard_10:
                case betType.Hard_12:
                    return Result<bool>.Pass(true);

                default:
                    return Result<bool>.Fail("Unknown bet type!");
            }
        }

        public static Result<bool> IsPlaceBetAllowed(CrapsTable crapsTable, Player playerToCheck, betType betType)
        {
            int placeBetNumber = betType switch
            {
                betType.PlaceBet_04 => 4,
                betType.PlaceBet_05 => 5,
                betType.PlaceBet_06 => 6,
                betType.PlaceBet_08 => 8,
                betType.PlaceBet_09 => 9,
                betType.PlaceBet_10 => 10,
                _ => throw new ArgumentException("Invalid place bet type") // default (should never come this)
            };

            if (!crapsTable.puck.IsOn)
                return Result<bool>.Fail("Place Bets are inaccessible when the puck is OFF.");

            if (!playerToCheck.playerBetList.Any(bet => bet.betType == betType.PassBet))
                return Result<bool>.Fail("Players must have a Pass Line Bet in order to access Place Bets");

            if (placeBetNumber == crapsTable.puck.passPoint)
                return Result<bool>.Fail("Players may not bet on the point. Instead, place an Odds bet (behind the pass line).");

            return Result<bool>.Pass(true);
        }

        public static List<betType> GetAllowedBets(CrapsTable crapsTable, Player player)
        {
            return Enum.GetValues<betType>()
                .Where(betType => CheckIfBetAllowed(crapsTable, player, betType).Success)
                .ToList();
        }
    }
}
