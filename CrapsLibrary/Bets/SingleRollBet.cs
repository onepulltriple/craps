namespace CrapsLibrary.Bets
{
    public class SingleRollBet : Bet
    {
        public SingleRollBet(CrapsTable crapsTable, Player betOwner, betType betType, uint countOfUnitsToBet, uint unitOfBet, List<int> winningTotals, uint payout) 
            : base(crapsTable, betOwner, betType, countOfUnitsToBet, unitOfBet, winningTotals, payout)
        {
            // any single roll bet is always affected (win or lose) by the outcome of any roll
        }

        internal override bool MeetsFirstWinningCondition(byte firstOutcome, byte secondOutcome)
        {
            if (!winningTotals.Contains(firstOutcome + secondOutcome))
                return false;

            if (this.betType == betType.Field &&
                    (
                        (firstOutcome + secondOutcome) == 2 || (firstOutcome + secondOutcome) == 12
                    )
               )
            {
                this.Payout *= 2; // TODO test parlaying of field bets when the 2 or 12 hits
                return true;
            } 

            return true;
        }

        internal override bool MeetsLosingCondition(byte firstOutcome, byte secondOutcome)
        {
            // loses if the roll doesn't match the outcome 
            if (!winningTotals.Contains(firstOutcome + secondOutcome))
                return true;

            return false;
        }
    }
}
