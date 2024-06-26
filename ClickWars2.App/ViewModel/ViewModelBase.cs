﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClickWars2.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual Task LoadAsync() => Task.CompletedTask;
        public virtual Task WriteAsync() => Task.CompletedTask;
    }
}
