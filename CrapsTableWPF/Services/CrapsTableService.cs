using CrapsLibrary;
using CrapsTableWPF;

namespace CrapsTableWPF.Services
{
    public class CrapsTableService : ICrapsTableService
    {
        public CrapsTable? crapsTable;

        public void CreateTable(uint tableMinimum)
        {
            this.crapsTable = new CrapsTable(tableMinimum, tableMinimum * 100); // TODO do not hard code this here
        }


    }
}
