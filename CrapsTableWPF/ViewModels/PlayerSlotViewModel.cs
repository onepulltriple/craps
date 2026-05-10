using CrapsLibrary;
using System.Windows.Input;

namespace CrapsTableWPF.ViewModels
{
    public class PlayerSlotViewModel
    {
        public int Index { get; }

        public PlayerViewModel? Player {get;}

        public bool IsEmpty => Player == null;

        public ICommand ClickCommand { get; }

        public PlayerSlotViewModel(int index, PlayerViewModel? player, ICommand clickCommand)
        {
            this.Index = index;
            this.Player = player;
            this.ClickCommand = clickCommand;
        }
    }
}
