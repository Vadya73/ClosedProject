using Core;
using Core.CameraControl;
using Meta;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Infrastructure
{
    public class Level1Bootstrapper : MonoBehaviour
    {
        [SerializeField] private DialogueConfig _startDialogueConfig;
        [SerializeField] private CameraConfig _cameraConfig;

        private DialogueController _dialogueController;
        private IObjectResolver _objectResolver;
        private Button[] _allButtons;
        private AudioController _audioController;
        private SoundConfigs _soundConfigs;
        private CameraController _cameraController;
        private TimeSystem _timeSystem;

        [Inject]
        private void Construct(IObjectResolver objectResolver, AudioController audioController, SoundConfigs soundConfigs, 
            CameraController cameraController, TimeSystem timeSystem)
        {
            _objectResolver = objectResolver;
            _audioController = audioController;
            _soundConfigs = soundConfigs;
            _cameraController = cameraController;
            _timeSystem = timeSystem;
        }

        private void Awake()
        {
            _allButtons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            Debug.Log("buttons on level " + _allButtons.Length);

            foreach (var button in _allButtons)
            {
                button.onClick.AddListener(() => _audioController.PlayEffects(_soundConfigs.DefaultClickSound));
            }

            _timeSystem.TimeData.ResetTimeData();
        }

        private void Start()
        {
            _dialogueController = _objectResolver.Resolve<DialogueController>();
            _dialogueController.StartDialogue(_startDialogueConfig);
            
            _cameraController.SetConfig(_cameraConfig);
        }
    }
}