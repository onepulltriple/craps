using System.Drawing;
using System.Linq.Expressions;

namespace CrapsLibrary
{
    public enum betType
    { 
        No_Bet,
        Aces,  
        hard_4,
        hard_6,
        hard_8,
        hard_10,
        hard_12
    }

    public class BetFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerBetType">Should be <see cref="betType"/></param>
        /// <returns></returns>

        public Bet? CreateBet(betType playerBetType) //, player, mulitplier)
        {
            Bet? tempBet;

            // betType (is this the betName?)
            // player
            // multiplier (i.e. the amount)

            switch (playerBetType)
            {
                case betType.Aces:
                    tempBet = new HardWayBet(playerBetType.ToString(), new List<int>{2}, 30, 1);
                    break;

                default:
                    tempBet = null;
                    break;

            }

            return tempBet;
        }
    }
}
