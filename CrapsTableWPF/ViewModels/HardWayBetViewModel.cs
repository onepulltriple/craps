using CrapsLibrary;
using CrapsLibrary.Bets;
using CrapsTableWPF.Infrastructure;
using CrapsTableWPF.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;

namespace CrapsTableWPF.ViewModels
{
    public class HardWayBetViewModel : BettingAreaViewModelBase
    {
        // TODO save this class for bet-specific tooltips or other purposes
        public HardWayBetViewModel(CrapsTable crapsTable, DialogService dialogService, betType betType) : base(crapsTable, dialogService, betType)
        {

        }
        
    }
}
