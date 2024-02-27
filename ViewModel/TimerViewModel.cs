using ClickWars2.Command;
using Timer = ClickWars2.Model.Timer;

namespace ClickWars2.ViewModel;

public class TimerViewModel : ViewModelBase
{
    private const int GameDuration = 30;
    private readonly Timer _model;

    private bool _wasRunning;

    public int SecondsRemaining
    {
        get
        {
            var secs = (int)this._model.SecondsRemaining;
            return secs > 0 ? secs : 0;
        }
    }

    public double PercentRemaining => this._model.SecondsRemaining / TimerViewModel.GameDuration;
    public bool IsRunning => this._model.IsRunning;

    public readonly DelegateCommand StartCommand;
    
    public TimerViewModel()
    {
        this._model = new Timer();
        this._model.RemainingSecondsChanged += ModelOnRemainingSecondsChanged;

        this._wasRunning = false;

        this.StartCommand = new DelegateCommand(Start, CanStart);
    }

    private void Start(object? obj)
    {
        this._model.Start(TimerViewModel.GameDuration);
        this._wasRunning = true;
    }

    private bool CanStart(object? arg)
    {
        return !this._model.IsRunning;
    }

    private void ModelOnRemainingSecondsChanged()
    {
        RaisePropertyChanged(nameof(SecondsRemaining));
        RaisePropertyChanged(nameof(PercentRemaining));

        if (this._wasRunning == this.IsRunning) return;
        
        RaisePropertyChanged(nameof(IsRunning));
        this._wasRunning = this.IsRunning;
    }
}