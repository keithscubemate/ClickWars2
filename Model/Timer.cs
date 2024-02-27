using System.Timers;
using System.Windows;

namespace ClickWars2.Model;

public class Timer : IDisposable
{
    private readonly System.Timers.Timer _timer;

    private bool _wasRunning = false;
    private DateTime _endTime;
    
    public event Action? RemainingSecondsChanged;

    public double SecondsRemaining => DateTimeToSeconds(_endTime) - DateTimeToSeconds(DateTime.Now);
    public bool IsRunning => SecondsRemaining > 0;

    public Timer()
    {
        this._timer = new System.Timers.Timer();
        this._timer.Elapsed += this.TimerElapsed;
    }

    public void Dispose()
    {
        this._timer.Dispose();
    }

    public void Start(int durationInSeconds)
    {
        this._timer?.Start();
        this._endTime = DateTime.Now.AddSeconds(durationInSeconds);
        
        this.OnRemainingSecondsChanged();
    }
    
    private void TimerElapsed(object? sender, ElapsedEventArgs e)
    {
        this.OnRemainingSecondsChanged();

        _wasRunning = this.IsRunning;
    }

    private void OnRemainingSecondsChanged()
    {
        RemainingSecondsChanged?.Invoke();
    }

    private static double DateTimeToSeconds(DateTime dateTime)
    {
        return TimeSpan.FromTicks(dateTime.Ticks).TotalSeconds;
    }
}