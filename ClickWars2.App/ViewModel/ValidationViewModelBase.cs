using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClickWars2.ViewModel
{
    public class ValidationViewModelBase : ViewModelBase
    {
        private Dictionary<string, List<string>> _errorsByPropertyName = new();

        public bool HasErrors => this._errorsByPropertyName.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (propertyName == null || !this._errorsByPropertyName.ContainsKey(propertyName))
            {
                return Enumerable.Empty<string>();
            }

            return this._errorsByPropertyName[propertyName];
        }

        protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs args)
        {
            ErrorsChanged?.Invoke(this, args);
        }

        protected void AddError(string error, [CallerMemberName] string? propertyName = null)
        {
            if (propertyName is null) return;

            if (!this._errorsByPropertyName.ContainsKey(propertyName))
            {
                this._errorsByPropertyName[propertyName] = new List<string>();
            }

            if (!this._errorsByPropertyName[propertyName].Contains(error))
            {
                this._errorsByPropertyName[propertyName].Add(error);
                this.OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
                RaisePropertyChanged(nameof(HasErrors));
            }

        }

        protected void ClearErrors([CallerMemberName] string? propertyName = null)
        {
            if (propertyName is null) return;

            if (this._errorsByPropertyName.ContainsKey(propertyName))
            {
                this._errorsByPropertyName.Remove(propertyName);
                this.OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
                RaisePropertyChanged(nameof(HasErrors));
            }
    }

}
}
