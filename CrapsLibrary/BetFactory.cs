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
        PassBet
    }

    public class BetFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerBetType">Should be <see cref="betType"/></param>
        /// <returns></returns>

        public static Dictionary<betType, (uint payoutNumerator, uint payoutDenominator)> betPayoutRatios =
            new Dictionary<betType, (uint payoutNumerator, uint payoutDenominator)>()
            {
                {betType.Aces,    (30, 1)},
                {betType.Hard_4,  (7, 1)},
                {betType.Hard_6,  (9, 1)},
                {betType.Hard_8,  (9, 1)},
                {betType.Hard_10, (7, 1)},
                {betType.Hard_12, (30, 1)},
                {betType.PassBet, (2, 1)}
            };

        public Bet? CreateBet(betType playerBetType, Player player, uint amountThrownAtBet) 
        {
            if (player.purse < amountThrownAtBet) // the player cannot bet more than they have
                return null;

            uint unitOfBet = 
                CrapsTable.tableMinimum * 
                CrapsTable.upscaleRatio * 
                betPayoutRatios[playerBetType].payoutDenominator;

            uint countOfUnitsToBet = amountThrownAtBet / unitOfBet; // the quotient
            uint amountToBet = countOfUnitsToBet * unitOfBet; // quotient times units
            uint amountChangeToReturn = amountThrownAtBet - amountToBet; // remainder to return to player

            player.purse -= amountThrownAtBet;
            player.purse += amountChangeToReturn;

            Bet? tempBet;

            switch (playerBetType)
            {
                case betType.Aces: // wins on 1,1 (snake eyes)
                    tempBet = new HardWayBet(playerBetType.ToString(), amountToBet, new List<int>{ 2 });
                    break;

                case betType.Hard_4: // wins on 2,2
                    tempBet = new HardWayBet(playerBetType.ToString(), amountToBet, new List<int> { 4 });
                    break;

                case betType.Hard_6: // wins on 3,3
                    tempBet = new HardWayBet(playerBetType.ToString(), amountToBet, new List<int> { 6 });
                    break;

                case betType.Hard_8: // wins on 4,4
                    tempBet = new HardWayBet(playerBetType.ToString(), amountToBet, new List<int> { 8 });
                    break;

                case betType.Hard_10: // wins on 5,5
                    tempBet = new HardWayBet(playerBetType.ToString(), amountToBet, new List<int> { 10 });
                    break;

                case betType.Hard_12: // wins on 6,6
                    tempBet = new HardWayBet(playerBetType.ToString(), amountToBet, new List<int> { 12 });
                    break;

                case betType.PassBet: // wins on natural passes, winning based on point behavior handled internally
                    tempBet = new PassBet(playerBetType.ToString(), amountToBet, new List<int> { 7, 11 });
                    break;

                default:
                    tempBet = null;
                    break;
            }
            return tempBet;
        }
    }
}
