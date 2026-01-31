using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrapsLibrary
{
    public class CrapsTable
    {
        public int tableMinimum {  get; set; }
        public int tableMaximum {  get; set; }

        public Scoreboard scoreboard;

        public CrapsTable(int tableMinimum, int tableMaximum)
        {
            this.tableMinimum = tableMinimum;
            this.tableMaximum = tableMaximum;
            this.scoreboard = new Scoreboard();
        }

        public (int, int) RollDice(int numSides01, int numSides02)
        {
            Die Die01 = new(numSides01);
            Die Die02 = new(numSides02);
            this.scoreboard.die01Rolls.Add(Die01.NewOutcome());
            this.scoreboard.die02Rolls.Add(Die02.NewOutcome());
            return (this.scoreboard.die01Rolls.Last(), this.scoreboard.die02Rolls.Last());
        }
    }
}
