using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Core
{
    public class TrainingAiView : BaseView<IData>
    {
        [SerializeField] private Button _startResearchButton;
        [SerializeField] private TrainingAiComponent[] _components;
        [SerializeField] private TrainingAiAddon[] _addons;
        [Header("Name")]
        [SerializeField] private TMP_InputField _nameInputField;
        [SerializeField] private Button _randomNameButton;
        
        private PlayerConfig _playerConfig;

        public Button StartResearchButton => _startResearchButton;
        public TrainingAiComponent[] Components => _components;
        public TrainingAiAddon[] Addons => _addons;
        public TMP_InputField NameInputField => _nameInputField;
        public Button RandomNameButton => _randomNameButton;

        [Inject]
        private void Construct(PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;
        }
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            _components = GetComponentsInChildren<TrainingAiComponent>();
            _addons = GetComponentsInChildren<TrainingAiAddon>();
        }
#endif
        
        private void Start()
        {
            foreach (var addon in _addons)
                addon.Toggle.isOn = false;
        }

        private void OnEnable()
        {
            foreach (var buildRobotAddon in _addons)
                buildRobotAddon.Toggle.interactable = false;
            
            foreach (var buildRobotAddon in _addons)
            {
                foreach (var addonConfig in _playerConfig.TrainingAIData.Addons)
                {
                    if (addonConfig == buildRobotAddon.RobotAddonConfig)
                        buildRobotAddon.Toggle.interactable = true;
                }
            }
        }
    }
}