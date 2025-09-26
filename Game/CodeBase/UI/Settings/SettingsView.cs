using System;
using Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace UI.Settings
{
    public class SettingsView : BaseView<IData>
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _musicButton;
        [SerializeField] private Button _soundButton;
        [SerializeField] private Button _restorePurchasesButton;
        [SerializeField] private GameObject _musicOn;
        [SerializeField] private GameObject _musicOff;
        [SerializeField] private GameObject _effectsOn;
        [SerializeField] private GameObject _effectsOff;
        
        public Button CloseButton => _closeButton;
        public Button MusicButton => _musicButton;
        public Button SoundButton => _soundButton;
        public Button RestorePurchasesButton => _restorePurchasesButton;
        
        public GameObject MusicOn => _musicOn;
        public GameObject MusicOff => _musicOff;
        public GameObject EffectsOn => _effectsOn;
        public GameObject EffectsOff => _effectsOff;
    }

    public class SettingsObserver : IInitializable, IDisposable
    {
        private readonly SettingsView _settingsView;
        private readonly SettingsController _settingsController;

        [Inject]
        public SettingsObserver(SettingsView settingsView, SettingsController settingsController)
        {
            _settingsView = settingsView;
            _settingsController = settingsController;
        }

        public void Initialize()
        {
            _settingsView.CloseButton.onClick.AddListener(CloseSettingsView);
            _settingsView.MusicButton.onClick.AddListener(ChangeMusicState);
            _settingsView.SoundButton.onClick.AddListener(ChangeSoundState);
            _settingsView.RestorePurchasesButton.onClick.AddListener(RestorePurchases);
        }

        public void Dispose()
        {
            _settingsView.CloseButton.onClick.RemoveListener(CloseSettingsView);
            _settingsView.MusicButton.onClick.RemoveListener(ChangeMusicState);
            _settingsView.SoundButton.onClick.RemoveListener(ChangeSoundState);
            _settingsView.RestorePurchasesButton.onClick.RemoveListener(RestorePurchases);
        }

        private void CloseSettingsView()
        {
            _settingsController.HideView();
        }

        private void RestorePurchases()
        {
            _settingsController.RestorePurchases();
        }

        private void ChangeSoundState()
        {
            _settingsController.ChangeSoundState();
        }

        private void ChangeMusicState()
        {
            _settingsController.ChangeMusicState();
        }
    }

    public class SettingsController : IInitializable
    {
        private readonly AudioController _audioController;
        private readonly SettingsView _settingsView;

        public SettingsController(AudioController audioController, SettingsView settingsView)
        {
            _audioController = audioController;
            _settingsView = settingsView;
        }

        public void Initialize()
        {
            _settingsView.ForcedHide();
        }

        public void HideView()
        {
            if (_settingsView.IsAnimated)
                return;
            
            _settingsView.Hide();
        }

        public void RestorePurchases()
        {
            Debug.Log("Restore Purchases");
        }

        public void ChangeSoundState()
        {
            _audioController.ChangeSoundState();

            _settingsView.EffectsOn.SetActive(!_audioController.EffectsSource.mute);
            _settingsView.EffectsOff.SetActive(_audioController.EffectsSource.mute);
        }

        public void ChangeMusicState()
        {
            _audioController.ChangeMusicState();
            
            _settingsView.MusicOff.SetActive(_audioController.MusicSource.mute);
        }

        public void ShowView()
        {
            if (_settingsView.IsAnimated)
                return;

            if (_settingsView.IsActive)
            {
                HideView();
                return;
            }
            
            _settingsView.Show();
        }
    }
}