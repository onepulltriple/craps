using CrapsLibrary;
using System.Windows.Input;

namespace CrapsTableWPF.ViewModels
{
    public class PlayerSlotViewModel
    {
        public int Index { get; }

        public Player? Player {get;}

        public bool IsEmpty => Player == null;

        //public ICommand ClickCommand { get; }

        public PlayerSlotViewModel(Player? player, int index) //, ICommand clickCommand)
        {
            this.Player = player;
            this.Index = index;
            //this.ClickCommand = clickCommand;
        }
    }
}
