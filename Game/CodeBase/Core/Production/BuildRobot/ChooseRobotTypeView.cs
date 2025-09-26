using System.Collections.Generic;
using Infrastructure;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace Core
{
    public class ChooseRobotTypeView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private RobotTypes _robotType;
        [SerializeField] private List<RobotSubtypes> _availableSubtypes = new();
        [Header("System")]
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private TMP_Dropdown _dropdown;

        private BuildRobotController _buildRobotController;
        private AudioController _audioController;
        private SoundConfigs _soundConfigs;

        public RobotTypes RobotType => _robotType;
        
        [Inject]
        private void Construct(BuildRobotController buildRobotController, AudioController audioController, SoundConfigs soundConfigs)
        {
            _buildRobotController = buildRobotController;
            _audioController = audioController;
            _soundConfigs = soundConfigs;
        }

        private void Awake()
        {
            if (_dropdown == null)
                _dropdown = GetComponent<TMP_Dropdown>();
            
            _dropdown.onValueChanged.AddListener(SetRobotType);
            
            UpdateAvailableSubtypes();
            UpdateDropDown();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _audioController.PlayEffects(_soundConfigs.DefaultClickSound);
        }

        public void ResetDropdown() => 
            _dropdown.value = -1;

        private void SetRobotType(int arg0)
        {
            if (arg0 < 0)
                return;
            
            _buildRobotController.SetRobotType(_robotType, _availableSubtypes[arg0 ]);
            _buildRobotController.HighLightRobotType(this.transform.position);
        }

        public void UpdateAvailableSubtypes()
        {
            _availableSubtypes.Clear();
            
            if (_robotType == RobotTypes.None)
                return;

            foreach (RobotSubtypes subtype in GetSubtypesForCategory(_robotType))
            {
                if (_playerConfig.PlayerHasSubtype(subtype))
                    _availableSubtypes.Add(subtype);
            }
        }

        public void UpdateDropDown()
        {
            _dropdown.ClearOptions();
            List<TMP_Dropdown.OptionData> optionData = new List<TMP_Dropdown.OptionData>();
            
            foreach (var subtype in _availableSubtypes)
                optionData.Add(new TMP_Dropdown.OptionData(subtype.ToString()));
            
            _dropdown.AddOptions(optionData);
        }

        private IEnumerable<RobotSubtypes> GetSubtypesForCategory(RobotTypes category)
        {
            switch (category)
            {
                case RobotTypes.Toy:
                    yield return RobotSubtypes.Dog;
                    yield return RobotSubtypes.Transformer;
                    yield return RobotSubtypes.Repeater;
                    yield return RobotSubtypes.Companion;
                    break;
                    
                case RobotTypes.Utilitarian:
                    yield return RobotSubtypes.SmartVacuumCleaner;
                    yield return RobotSubtypes.DeliveryDrone;
                    yield return RobotSubtypes.SmartSpeaker;
                    yield return RobotSubtypes.Officiant;
                    yield return RobotSubtypes.SidelkaRobot;
                    break;
                    
                case RobotTypes.Humanoid:
                    yield return RobotSubtypes.Humanoid;
                    break;
            }
        }
    }
}