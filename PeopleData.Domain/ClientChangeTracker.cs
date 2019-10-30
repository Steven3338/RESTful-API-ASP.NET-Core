using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace PeopleInformation.Domain
{
    // Raise a notification from the object to the UI to let it know when the IsDirty flag has
    // changed. It has a single Boolean property called IsDirty, so that's the property I will
    // want in my classes engages .NET's notification API, so the class implements
    // INotificationPropertyChanged interface
    public class ClientChangeTracker : INotifyPropertyChanged
    {
        private bool _isDirty;

        public bool IsDirty
        {
            get { return _isDirty; }
            set { SetWithNotify(value, ref _isDirty); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // [CallerMemberName] attribute allows you to obtain the method or property name of 
        // the caller to the method
        protected void SetWithNotify<T>(T value, ref T field, [CallerMemberName] string propertyName = "")
        {
            if (!Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}