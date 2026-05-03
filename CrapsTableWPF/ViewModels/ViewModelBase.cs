using System.ComponentModel;

namespace CrapsTableWPF.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        // a delegate that notifies the WPF binding system of a new value
        // (this is the observable)
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, e);
            }
        }
    }
}
