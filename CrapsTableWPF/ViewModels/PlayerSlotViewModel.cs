using CrapsLibrary;
using System.Windows.Input;

namespace CrapsTableWPF.ViewModels
{
    public class PlayerSlotViewModel
    {
        public PlayerViewModel PlayerViewModel {get;}

        public int Index { get; }

        public bool IsEmpty => PlayerViewModel == null;

        private readonly CrapsTable crapsTable;

        //public ICommand ClickCommand { get; }

        public PlayerSlotViewModel(CrapsTable crapsTable, Player? player, int slotIndex) //, ICommand clickCommand)
        {
            this.PlayerViewModel = player is null ? null : new PlayerViewModel(player);

            this.Index = slotIndex;
            this.crapsTable = crapsTable;
            //this.ClickCommand = clickCommand;
        }
    }
}
