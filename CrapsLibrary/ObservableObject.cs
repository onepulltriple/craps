using System.ComponentModel;

namespace CrapsLibrary
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        // a delegate that notifies subscribers of a new value
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
