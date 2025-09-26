using System.Collections.Generic;
using Infrastructure;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace Core
{
    public class TrainingAiComponent : ProductionComponent<AiComponentConfig>, IPointerClickHandler
    {
        [SerializeField] private AiComponentType _componentType;
        private AudioController _audioController;
        private SoundConfigs _soundConfigs;
        public AiComponentType ComponentType => _componentType;

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
                return;
            }

            // None = 0
            switch (_componentType)
            {
                case AiComponentType.Dataset:
                {
                    _currentComponentConfig = _playerConfig.TrainingAIData.AllHaveDatasets[componentIndex - 1];
                    break;
                }
                case AiComponentType.NetworkArchitecture:
                {
                    _currentComponentConfig =
                        _playerConfig.TrainingAIData.AllHaveNetworkArchitecture[componentIndex - 1];
                    break;
                }
                case AiComponentType.ModelSize:
                {
                    _currentComponentConfig = _playerConfig.TrainingAIData.AllHaveModelSize[componentIndex - 1];
                    break;
                }
                case AiComponentType.TrainingMethod:
                {
                    _currentComponentConfig = _playerConfig.TrainingAIData.AllHaveTrainingMethods[componentIndex - 1];
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
                case AiComponentType.Dataset:
                {
                    foreach (var config in _playerConfig.TrainingAIData.AllHaveDatasets)
                        allHaveComponentNames.Add(config.Name);
                    break;
                }
                case AiComponentType.NetworkArchitecture:
                {
                    foreach (var config in _playerConfig.TrainingAIData.AllHaveNetworkArchitecture)
                        allHaveComponentNames.Add(config.Name);
                    break;
                }
                case AiComponentType.ModelSize:
                {
                    foreach (var config in _playerConfig.TrainingAIData.AllHaveModelSize)
                        allHaveComponentNames.Add(config.Name);
                    break;
                }
                case AiComponentType.TrainingMethod:
                {
                    foreach (var config in _playerConfig.TrainingAIData.AllHaveTrainingMethods)
                        allHaveComponentNames.Add(config.Name);
                    break;
                }
            }
            
            _dropdown.AddOptions(allHaveComponentNames);
        }
    }
}