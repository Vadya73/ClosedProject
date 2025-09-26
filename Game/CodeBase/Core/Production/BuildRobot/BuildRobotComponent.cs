using System.Collections.Generic;
using Infrastructure;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace Core
{
    public class BuildRobotComponent : ProductionComponent<RobotComponentConfig>, IPointerClickHandler
    {
        [SerializeField] private RobotComponentType _componentType;
        [SerializeField] private AI _currentAI;
        
        private AudioController _audioController;
        private SoundConfigs _soundConfigs;

        public RobotComponentType ComponentType => _componentType;
        public AI CurrentAI => _currentAI;

        [Inject]
        private void Construct(AudioController audioController, SoundConfigs soundConfigs)
        {
            _audioController = audioController;
            _soundConfigs = soundConfigs;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _audioController.PlayEffects(_soundConfigs.DefaultClickSound);
        }

        protected override void SetCurrentComponent(int componentIndex)
        {
            if (componentIndex <= 0)
            {
                _dropdown.value = -1;
                _currentComponentConfig = null;

                if (_componentType == RobotComponentType.AI)
                    _currentAI = null;
                
                return;
            }
            // None = 0
            switch (_componentType)
            {
                case RobotComponentType.Material:
                { 
                    _currentComponentConfig = _playerConfig.BuildRobotData.AllHaveMaterials[componentIndex - 1];
                    break;
                }
                case RobotComponentType.AI:
                {
                    _currentAI = _playerConfig.AiDataConfig.AiData.CurrentAis[componentIndex - 1];
                    break;
                }
                case RobotComponentType.Chip:
                {
                    _currentComponentConfig = _playerConfig.BuildRobotData.AllHaveChip[componentIndex - 1];
                    break;
                }
                case RobotComponentType.Engine:
                {
                    _currentComponentConfig = _playerConfig.BuildRobotData.AllHaveEngine[componentIndex - 1];
                    break;
                }
                case RobotComponentType.Battery:
                {
                    _currentComponentConfig = _playerConfig.BuildRobotData.AllHaveBattery[componentIndex - 1];
                    break;
                }
            }
        }

        protected override void FillDropDown()
        {
            List<string> allHaveComponentNames = new List<string>();
            allHaveComponentNames.Add("None");
            
            switch (_componentType)
            {
                case RobotComponentType.Material:
                {
                    foreach (var config in _playerConfig.BuildRobotData.AllHaveMaterials)
                        allHaveComponentNames.Add(config.Name);
                    break;
                }
                case RobotComponentType.AI:
                {
                    foreach (var config in _playerConfig.AiDataConfig.AiData.CurrentAis)
                        allHaveComponentNames.Add(config.Name);
                    break;
                }
                case RobotComponentType.Chip:
                {
                    foreach (var config in _playerConfig.BuildRobotData.AllHaveChip)
                        allHaveComponentNames.Add(config.Name);
                    break;
                }
                case RobotComponentType.Engine:
                {
                    foreach (var config in _playerConfig.BuildRobotData.AllHaveEngine)
                        allHaveComponentNames.Add(config.Name);
                    break;
                }
                case RobotComponentType.Battery:
                {
                    foreach (var config in _playerConfig.BuildRobotData.AllHaveBattery)
                        allHaveComponentNames.Add(config.Name);
                    break;
                }
            }
            
            _dropdown.AddOptions(allHaveComponentNames);
        }

        public void ResetAi() => _currentAI = null;
    }
}