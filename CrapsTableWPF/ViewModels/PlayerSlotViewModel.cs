using CrapsLibrary;
using System.Windows.Input;

namespace CrapsTableWPF.ViewModels
{
    public class PlayerSlotViewModel
    {
        public Player? Player {get;}

        public int Index { get; }

        public bool IsEmpty => Player == null;

        private readonly CrapsTable crapsTable;

        //public ICommand ClickCommand { get; }

        public PlayerSlotViewModel(CrapsTable crapsTable, Player? player, int index) //, ICommand clickCommand)
        {
            this.Player = player;
            this.Index = index;
            this.crapsTable = crapsTable;
            //this.ClickCommand = clickCommand;
        }
    }
}
