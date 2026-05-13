using CrapsLibrary;
using System.Windows.Input;

namespace CrapsTableWPF.ViewModels
{
    public class PlayerSlotViewModel
    {
        //public Player? Player {get;}
        public PlayerViewModel PlayerViewModel {get;}

        public int Index { get; }

        public bool IsEmpty => PlayerViewModel == null;

        private readonly CrapsTable crapsTable;

        //public ICommand ClickCommand { get; }

        public PlayerSlotViewModel(CrapsTable crapsTable, Player? player, int index) //, ICommand clickCommand)
        {
            //this.Player = player;
            PlayerViewModel = player is null ? null : new PlayerViewModel(player);

            this.Index = index;
            this.crapsTable = crapsTable;
            //this.ClickCommand = clickCommand;
        }
    }
}
