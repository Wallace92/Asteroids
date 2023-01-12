using System.ComponentModel;

namespace Observer
{
    public interface INotifyPropertyChanged
    {
        event PropertyChangedEventHandler PropertyChange;
    }
}