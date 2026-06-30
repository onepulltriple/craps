namespace CrapsLibrary.Bets
{
    public class PassBet : Bet
    {
        List<int> losingTotals;

        public PassBet(CrapsTable crapsTable, Player betOwner, betType betType, uint countOfUnitsToBet, uint unitOfBet, List<int> winningTotals, uint payout)
            : base(crapsTable, betOwner, betType, countOfUnitsToBet, unitOfBet, winningTotals, payout)
        {
            losingTotals = new List<int> {2, 3, 12}; // crap out/missout
            //winningTotals = new List<int> {7, 11 };  // natural pass --> moved to BetDefinitions dictionary
        }

        internal override bool MeetsFirstWinningCondition(byte firstOutcome, byte secondOutcome)
        {
            // if puck is OFF, 7 and 11 win
            // if puck is ON, matching the point wins

            if (crapsTable.puck.IsOn == false && winningTotals.Contains(firstOutcome + secondOutcome))
            {
                crapsTable.gameEventFeed.Add(
                    $"Natural pass!",
                    GameEventType.Message,
                    true
                    );
                return true;
            }

            if (crapsTable.puck.IsOn == true && crapsTable.puck.PassPoint == firstOutcome + secondOutcome)
            {
                crapsTable.gameEventFeed.Add(
                    $"Point made! Pass!",
                    GameEventType.Message,
                    true
                    );
                return true;
            }
            return false;
        }

        internal override bool MeetsLosingCondition(byte firstOutcome, byte secondOutcome)
        {
            // if puck is OFF, craps loses
            // if puck is ON, 7 loses

            if (crapsTable.puck.IsOn == false && losingTotals.Contains(firstOutcome + secondOutcome))
            {
                crapsTable.gameEventFeed.Add(
                    $"Craps! Pass line loses.",
                    GameEventType.Message,
                    true
                    );
                return true;
            }

            if (crapsTable.puck.IsOutcomeSevenOut(firstOutcome, secondOutcome))
            {
                //crapsTable.gameEventFeed.Add(
                //    $"Point missed! Seven out!",
                //    GameEventType.Message,
                //    true
                //    );
                return true;
            }
            return false;
        }
    }
}
