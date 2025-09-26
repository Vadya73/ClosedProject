using Core.CameraControl;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Infrastructure
{
    public class Level2Bootstrapper : MonoBehaviour
    {
        [SerializeField] private CameraConfig _cameraConfig;
        
        private Button[] _allButtons;
        private AudioController _audioController;
        private SoundConfigs _soundConfigs;
        private CameraController _cameraController;

        [Inject]
        private void Construct(AudioController audioController, SoundConfigs soundConfigs, CameraController cameraController)
        {
            _audioController = audioController;
            _soundConfigs = soundConfigs;
            _cameraController = cameraController;
        }

        private void Awake()
        {
            _allButtons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            
            foreach (var button in _allButtons)
                button.onClick.AddListener(() => _audioController.PlayEffects(_soundConfigs.DefaultClickSound));
            
            _cameraController.SetConfig(_cameraConfig);
        }

        private void OnDestroy()
        {
            foreach (var button in _allButtons)
                button.onClick.RemoveListener(() => _audioController.PlayEffects(_soundConfigs.DefaultClickSound));
        }
    }
}