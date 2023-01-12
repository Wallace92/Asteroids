using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace Observer
{
    public class View : MonoBehaviour, INotifyPropertyChanged
    {
        protected void SetValue<T>(T value, ref T field, [CallerMemberName] string propertyName = null)
        {
            if (value.Equals(field))
                return;

            field = value;
            OnPropertyChanged(propertyName);
        }
    
        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChange?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChange;
    }
}