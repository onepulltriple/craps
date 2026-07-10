namespace CrapsLibrary.Bets
{
    public class DontPassBet : Bet
    {
        List<int> losingTotals;

        public DontPassBet(CrapsTable crapsTable, Player betOwner, betType betType, uint countOfUnitsToBet, uint unitOfBet, List<int> winningTotals, uint payout)
            : base(crapsTable, betOwner, betType, countOfUnitsToBet, unitOfBet, winningTotals, payout)
        {
            losingTotals = new List<int> {7, 11}; // pushes on 12, i.e. no result for this bet
            //winningTotals = new List<int> {2, 3};  
        }

        internal override bool MeetsFirstWinningCondition(byte firstOutcome, byte secondOutcome)
        {
            // if puck is OFF, 2 and 3 win
            // if puck is ON, 7 wins

            if (!crapsTable.puck.IsOn && winningTotals.Contains(firstOutcome + secondOutcome))
            {
                //crapsTable.gameEventFeed.Add(
                //    $"Don't pass!",
                //    GameEventType.Message,
                //    true
                //    );
                return true;
            }

            if (crapsTable.puck.IsOn && crapsTable.puck.IsOutcomeSevenOut(firstOutcome, secondOutcome))
            {
                //crapsTable.gameEventFeed.Add(
                //    $"Point lost! Don't Pass wins!",
                //    GameEventType.Message,
                //    true
                //    );
                return true;
            }
            return false;
        }

        internal override bool MeetsLosingCondition(byte firstOutcome, byte secondOutcome)
        {
            // if puck is OFF, 7, 11 lose
            // if puck is ON, matching the point loses

            if (!crapsTable.puck.IsOn && losingTotals.Contains(firstOutcome + secondOutcome))
            {
                //crapsTable.gameEventFeed.Add(
                //    $"Pass! Don't pass loses.",
                //    GameEventType.Message,
                //    true
                //    );
                return true;
            }

            if (crapsTable.puck.IsOn && crapsTable.puck.PassPoint == (firstOutcome + secondOutcome))
            {
                //crapsTable.gameEventFeed.Add(
                //    $"Point made! Don't pass loses!",
                //    GameEventType.Message,
                //    true
                //    );
                return true;
            }
            return false;
        }
    }
}
