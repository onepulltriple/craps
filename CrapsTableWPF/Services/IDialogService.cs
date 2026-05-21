using CrapsTableWPF.Data_Transfer_Objects;

namespace CrapsTableWPF.Services
{
    public interface IDialogService
    {
        NewPlayerData? ShowAddPlayerDialog();
    }
}
