using System;
using VContainer.Unity;

namespace Core
{
    public class TimeObserver : IInitializable, IDisposable
    {
        private readonly TimeSystem _timeSystem;
        private readonly CountDownTimer _timePerDayInSecondsTimer;

        public TimeObserver(TimeSystem timeSystem, CountDownTimer timePerDayInSecondsTimer)
        {
            _timeSystem = timeSystem;
            _timePerDayInSecondsTimer = timePerDayInSecondsTimer;
        }

        public void Initialize()
        {
            _timePerDayInSecondsTimer.OnEnded += TimePerDayInSecondsTimerOnOnEnded;
        }

        void IDisposable.Dispose()
        {
            _timePerDayInSecondsTimer.OnEnded -= TimePerDayInSecondsTimerOnOnEnded;
        }


        private void TimePerDayInSecondsTimerOnOnEnded() => _timeSystem.SetNextDay();

        private void StartTime() => _timeSystem.StartTime();

        private void StopTime() => _timeSystem.StopTime();

        private void SetX2Time()
        {
            if (!_timeSystem.Timer.IsPlaying)
                _timeSystem.Timer.Play();
            
            _timeSystem.SetTimeMultiplying(2);
        }

        private void SetX3Time()
        {
            if (!_timeSystem.Timer.IsPlaying)
                _timeSystem.Timer.Play();
            
            _timeSystem.SetTimeMultiplying(3);
        }
    }
}