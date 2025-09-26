using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace Core
{
    public class SpeedingTimeButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject _1XTimeSpeedGameObject;
        [SerializeField] private GameObject _2XTimeSpeedGameObject;
        [SerializeField] private GameObject _3XTimeSpeedGameObject;
        
        private TimeSystem _timeSystem;

        [Inject]
        private void Construct(TimeSystem timeSystem)
        {
            _timeSystem = timeSystem;
        }

        private void Start()
        {
            if (Mathf.Approximately(_timeSystem.Timer.Multiplying, 1))
                Show1XSpeedTime();
            else if (Mathf.Approximately(_timeSystem.Timer.Multiplying, 2))
                Show2XSpeedTime();
            else if (Mathf.Approximately(_timeSystem.Timer.Multiplying, 3))
                Show3XSpeedTime();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Mathf.Approximately(_timeSystem.Timer.Multiplying, 1))
            {
                _timeSystem.SetTimeMultiplying(2);
                Show2XSpeedTime();
            }
            else if (Mathf.Approximately(_timeSystem.Timer.Multiplying, 2))
            {
                _timeSystem.SetTimeMultiplying(3);
                Show3XSpeedTime();
            }
            else if (Mathf.Approximately(_timeSystem.Timer.Multiplying, 3))
            {
                _timeSystem.SetTimeMultiplying(1);
                Show1XSpeedTime();
            }
        }

        public void Show1XSpeedTime()
        {
            _1XTimeSpeedGameObject.gameObject.SetActive(true);
            
            _2XTimeSpeedGameObject.gameObject.SetActive(false);
            _3XTimeSpeedGameObject.gameObject.SetActive(false);
        }

        public void Show2XSpeedTime()
        {
            _2XTimeSpeedGameObject.gameObject.SetActive(true);
            
            _1XTimeSpeedGameObject.gameObject.SetActive(false);
            _3XTimeSpeedGameObject.gameObject.SetActive(false);
        }

        public void Show3XSpeedTime()
        {
            _3XTimeSpeedGameObject.gameObject.SetActive(true);
            
            _1XTimeSpeedGameObject.gameObject.SetActive(false);
            _2XTimeSpeedGameObject.gameObject.SetActive(false);
        }
    }
}