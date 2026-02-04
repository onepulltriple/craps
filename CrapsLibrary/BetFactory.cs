using System.Drawing;

namespace CrapsLibrary
{
    public class BetFactory
    {
        public BetFactory()
        {
            ;
        }

        public Bet CreateBet(Point position) // int mulitplier
        {


            Bet tempBet = new Bet(betName, winningTotals, payoutNumerator, payoutDenominator);
            return tempBet;
        }
    }
}
