using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer;

namespace Core
{
    public class StartStopTimeButton : MonoBehaviour, IPointerClickHandler
    {
        [Header("Components")] 
        [SerializeField] private Image _imageComponent;

        [SerializeField] private SpeedingTimeButton _speedingTimeButton;
        [Header("Sprites")]
        [SerializeField] private Sprite _playSprite;
        [SerializeField] private Sprite _pauseSprite;
        
        private TimeSystem _timeSystem;
        private bool _isStarted;

        [Inject]
        private void Construct(TimeSystem timeSystem)
        {
            _timeSystem = timeSystem;
        }

        private void Start()
        {
            _isStarted = _timeSystem.Timer.IsPlaying;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isStarted)
                StopTime();
            else
                StartTime();
        }

        private void StartTime()
        {
            _isStarted = true;
            _timeSystem.StartTime();
            _imageComponent.sprite = _playSprite;

            if (Mathf.Approximately(_timeSystem.Timer.Multiplying, 1))
                _speedingTimeButton.Show1XSpeedTime();
            else if (Mathf.Approximately(_timeSystem.Timer.Multiplying, 2))
                _speedingTimeButton.Show2XSpeedTime();
            else if (Mathf.Approximately(_timeSystem.Timer.Multiplying, 3))
                _speedingTimeButton.Show3XSpeedTime();
        }

        private void StopTime()
        {
            _isStarted = false;
            _timeSystem.StopTime();
            _imageComponent.sprite = _pauseSprite;
        }
    }
}