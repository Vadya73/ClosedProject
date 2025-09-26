using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Core
{
    public class BuildRobotView : BaseView<IData>
    {
        [SerializeField] private Button _startBuildButton;
        [SerializeField] private BuildRobotComponent[] _components;
        [SerializeField] private BuildRobotAddon[] _addons;
        [SerializeField] private ChooseRobotTypeView[] _chooseRobotTypeViews;
        [SerializeField] private GameObject _highlightObject;
        [Header("Name")]
        [SerializeField] private TMP_InputField _nameInputField;
        [SerializeField] private Button _randomNameButton;
        [SerializeField] private int _charLimits;
        
        private PlayerConfig _playerConfig;

        public Button StartBuildButton => _startBuildButton;
        public BuildRobotComponent[] Components => _components;
        public BuildRobotAddon[] Addons => _addons;
        public ChooseRobotTypeView[] ChooseRobotTypeViews => _chooseRobotTypeViews;
        public GameObject HighlightObject => _highlightObject;
        public TMP_InputField NameInputField => _nameInputField;
        public Button RandomNameButton => _randomNameButton;

#if UNITY_EDITOR
        private void OnValidate()
        {
            _components = GetComponentsInChildren<BuildRobotComponent>();
            _addons = GetComponentsInChildren<BuildRobotAddon>();
            _chooseRobotTypeViews = GetComponentsInChildren<ChooseRobotTypeView>();
        }
#endif

        [Inject]
        private void Construct(PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;
        }

        protected override void Awake()
        {
            base.Awake();
            _highlightObject.SetActive(false);
            _nameInputField.characterLimit = _charLimits;
        }

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
                foreach (var addonConfig in _playerConfig.BuildRobotData.Addons)
                {
                    if (addonConfig == buildRobotAddon.RobotAddonConfig)
                        buildRobotAddon.Toggle.interactable = true;
                }
            }
        }
    }
}