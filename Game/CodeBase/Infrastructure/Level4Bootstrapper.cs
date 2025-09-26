using Core.CameraControl;
using Meta;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Infrastructure
{
    public class Level4Bootstrapper : MonoBehaviour
    {
        [SerializeField] private CameraConfig _cameraConfig;
        
        private Button[] _allButtons;
        private AudioController _audioController;
        private SoundConfigs _soundConfigs;
        private CameraController _cameraController;
        private LeaderboardController _leaderboardController;
        private LoadScreen _loadScreen;

        [Inject]
        private void Construct(AudioController audioController, SoundConfigs soundConfigs, CameraController cameraController,
            LeaderboardController leaderboardController, LoadScreen loadScreen)
        {
            _audioController = audioController;
            _soundConfigs = soundConfigs;
            _cameraController = cameraController;
            _leaderboardController = leaderboardController;
            _loadScreen = loadScreen;
        }

        private void Awake()
        {
            _leaderboardController.AiLeaderboardData.OnEntryAdd += CheckEndGame;
            
            _allButtons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            
            foreach (var button in _allButtons)
                button.onClick.AddListener(() => _audioController.PlayEffects(_soundConfigs.DefaultClickSound));
            
            _cameraController.SetConfig(_cameraConfig);

            if (!_cameraController.IsActive)
                _cameraController.SetActiveStateCamera(true);
        }

        private void OnDestroy()
        {
            _leaderboardController.AiLeaderboardData.OnEntryAdd -= CheckEndGame;
            
            foreach (var button in _allButtons)
                button.onClick.RemoveListener(() => _audioController.PlayEffects(_soundConfigs.DefaultClickSound));
        }

        private void CheckEndGame(LeaderboardData leaderboardData)
        {
            if (leaderboardData.Entrys[0].isPlayerEntrys)
            {
                LoadEndLevel();
            }
        }

        private void LoadEndLevel()
        {
            _loadScreen.LoadScene(5);
        }
    }
}