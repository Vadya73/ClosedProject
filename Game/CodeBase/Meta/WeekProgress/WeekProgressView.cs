using Core;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Meta
{
    public class WeekProgressView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private TimeSystem _timeSystem;

        [Inject]
        private void Construct(TimeSystem timeSystem)
        {
            _timeSystem = timeSystem;
        }

        private void Start()
        {
            _timeSystem.TimeData.OnWeekChange += AddSliderWeek;
            _slider.value = 0f;
        }

        private void AddSliderWeek()
        {
            _slider.value += 1;

            if (Mathf.Approximately(_slider.maxValue, _slider.value))
            {
                NewMonth();
            }
        }

        private void NewMonth()
        {
            _slider.value = 0f;
        }
    }
}
