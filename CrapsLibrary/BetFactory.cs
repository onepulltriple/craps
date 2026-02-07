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
        hard_12,
        PassBet
    }

    public class BetFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerBetType">Should be <see cref="betType"/></param>
        /// <returns></returns>

        public Bet? CreateBet(betType playerBetType, int amount) //, player, multiplier)
        {
            Bet? tempBet;

            // check amount validity (using switch?)
            // on what level should this be checked? the base Bet?


            // betType (is this the betName?)
            // player
            // multiplier (i.e. the amount)

            switch (playerBetType)
            {
                case betType.Aces: // wins on snake eyes
                    tempBet = new HardWayBet(playerBetType.ToString(), amount, new List<int>{ 2 }, 30, 1);
                    break;

                case betType.PassBet: // wins on natural passes, point handled internally
                    tempBet = new PassBet(playerBetType.ToString(), amount, new List<int> { 7, 11 }, 2, 1);
                    break;

                default:
                    tempBet = null;
                    break;

            }

            return tempBet;
        }
    }
}
