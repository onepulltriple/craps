using CrapsLibrary;
using System.Collections.ObjectModel;

namespace CrapsTableWPF.DesignTime_Classes
{
    public class DesignPlayerViewModel
    {
        public string Name { get; set; } = "High Roller";

        public uint Purse { get; set; } = 1080;

        public DesignPlayerViewModel() { }
    }
}
