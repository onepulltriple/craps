using CrapsLibrary;
using System.Collections.ObjectModel;

namespace CrapsTableWPF.DesignTime_Classes
{
    public class DesignPlayerSlotViewModel
    {
        public DesignPlayerViewModel? PlayerViewModel { get; set; }

        public int SlotIndex { get; set; }

        public int DisplayedSlotIndex => SlotIndex + 1;

        public bool IsEmpty => PlayerViewModel == null;

        public bool IsCurrentPlayer { get; set; } = true;


        public DesignPlayerSlotViewModel() 
        {
            SlotIndex = 0;

            PlayerViewModel = new DesignPlayerViewModel
            {
                Name = "Current Player",
                Purse = 1200
            };

        }
    }
}
