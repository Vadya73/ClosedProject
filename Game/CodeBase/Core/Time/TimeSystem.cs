using Infrastructure;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class TimeSystem : IInitializable
    {
        private CountDownTimer _timer;
        private TimeObserver _timeObserver;
        private readonly TimeData _timeData;
        private readonly MonoBehaviour _monoHelper;
        private int _lastTimeSpeed;

        public TimeData TimeData => _timeData;
        public CountDownTimer Timer => _timer;
        
        [Inject]
        public TimeSystem(MonoHelper monoHelper, TimeData timeData)
        {
            _monoHelper = monoHelper;
            _timeData = timeData;
        }
        

        public void Initialize()
        {
            _timer = new CountDownTimer(_monoHelper, _timeData.TimePerDayInSeconds); // TO LTScope
            _timeObserver = new TimeObserver(this, _timer); // TO LTScope
            
            _timeObserver.Initialize();
            _timer.Play();

            _timeData.SetDay(1);
        }

        public void StartTime()
        {
            _timer.Play();
            _timer.SetMultiplying(_lastTimeSpeed);
        }

        public void SetNextDay()
        {
            _timeData.SetNextDay();
            _timer.ResetTime();
            _timer.Play();
        }

        public void StopTime()
        {
            _timer.Stop();
            _lastTimeSpeed = (int)_timer.Multiplying;
        }

        public void SetTimeMultiplying(int multiplying)
        {
            _lastTimeSpeed = multiplying;
            _timer.SetMultiplying(multiplying);
        }
    }
}
