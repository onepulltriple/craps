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

        public CrapsTable(int tableMinimum, int tableMaximum)
        {
            this.tableMinimum = tableMinimum;
            this.tableMaximum = tableMaximum;

        }

        public (int, int) RollDice(int numSides01, int numSides02)
        {
            Die Die01 = new(numSides01);
            Die Die02 = new(numSides02);
            return (Die01.NewOutcome(), Die02.NewOutcome());
        }
    }
}
