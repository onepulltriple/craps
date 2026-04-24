namespace CrapsLibrary.Bets
{
    public class PassBet : Bet
    {
        List<int> losingTotals;

        public PassBet(CrapsTable crapsTable, Player betOwner, betType betType, uint commitment, List<int> winningTotals, uint payout)
            : base(crapsTable, betOwner, betType, commitment, winningTotals, payout)
        {
            losingTotals = new List<int> {2, 3, 12}; // crap out/missout
            //winningTotals = new List<int> {7, 11 };  // natural pass
            // TODO find out why the winningTotals don't overwrite here (or are the winning totals really better off in the dictionary)
        }

        internal override bool MeetsFirstWinningCondition(byte firstOutcome, byte secondOutcome)
        {
            // if puck is OFF, 7 and 11 win
            // if puck is ON, matching the point wins

            if (crapsTable.puck.IsOn == false && winningTotals.Contains(firstOutcome + secondOutcome))
            {
                crapsTable.gameEventFeed.Add(
                    $"Natural pass!",
                    GameEventType.Message
                    );
                return true;
            }

            if (crapsTable.puck.IsOn == true && crapsTable.puck.passPoint == firstOutcome + secondOutcome)
            {
                crapsTable.gameEventFeed.Add(
                    $"Point made! Pass!",
                    GameEventType.Message
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
                    $"Craps!!",
                    GameEventType.Message
                    );
                return true;
            }

            if (crapsTable.puck.IsOutcomeSevenOut(firstOutcome, secondOutcome))
            {
                crapsTable.gameEventFeed.Add(
                    $"Point missed! Seven out!",
                    GameEventType.Message
                    );
                return true;
            }
            return false;
        }
    }
}
