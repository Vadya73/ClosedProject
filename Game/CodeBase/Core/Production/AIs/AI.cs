using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class AI : IData
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private AiСharacters _сharacters;
        [SerializeField] private List<AiAddonConfig> _addons = new(); 
        [SerializeField] private AiComponentConfig _datasetComponent;
        [SerializeField] private AiComponentConfig _networkArchitectureComponent;
        [SerializeField] private AiComponentConfig _modelSizeComponent;
        [SerializeField] private AiComponentConfig _trainingMethodComponent;
        [SerializeField] private int _score;
        [SerializeField] private int _addingPointsToBubbleRobot;
        [SerializeField] private Sprite _image;
        [Header("To Bubble Progress")]
        [SerializeField] private int _requiredToCreateBubbleScore;
        [SerializeField] private int _currentToCreateBubbleScore;

        public string Name => _name;
        public string Description => _description;
        public AiСharacters Characters => _сharacters;
        public int Score => _score;
        public int AdditionalPointsToBubblesRobot => _addingPointsToBubbleRobot;
        public Sprite Image => _image;
        public int RequiredToCreateBubbleScore => _requiredToCreateBubbleScore;

        public void SetComponent(AiComponentConfig trainingAiComponent)
        {
            switch (trainingAiComponent.Type)
            {
                case AiComponentType.Dataset:
                    _datasetComponent = trainingAiComponent;
                    break;
                case AiComponentType.NetworkArchitecture:
                    _networkArchitectureComponent = trainingAiComponent;
                    break;
                case AiComponentType.ModelSize:
                    _modelSizeComponent = trainingAiComponent;
                    break;
                case AiComponentType.TrainingMethod:
                    _trainingMethodComponent = trainingAiComponent;
                    break;
            }
        }

        public void SetAddons(AiAddonConfig aiAddon) => _addons.Add(aiAddon);

        public void SetName(string aiName) => _name = aiName;

        public void SetDescription(string description) => _description = description;
        public void SetImage(Sprite image) => _image = image;

        public void CalculateParams()
        {
            if (_datasetComponent != null)
                _сharacters.Accuracy = _datasetComponent.InfluenceOnChars.Accuracy;
            
            if (_networkArchitectureComponent != null)
                _сharacters.Flexibility = _networkArchitectureComponent.InfluenceOnChars.Flexibility;
            
            if (_modelSizeComponent != null)
                _сharacters.Stability = _modelSizeComponent.InfluenceOnChars.Stability;
            
            if (_trainingMethodComponent != null)
                _сharacters.Adaptiveness = _trainingMethodComponent.InfluenceOnChars.Adaptiveness;
        }

        public void CalculateScore()
        {
            var score = (_сharacters.Stability + _сharacters.Flexibility + _сharacters.Accuracy + _сharacters.Adaptiveness) / 4;
            var clamp = Mathf.Clamp(score, 0, 100);
            
            _score = clamp;
            _addingPointsToBubbleRobot = (int)(clamp * .5f);
            Debug.Log($"AI Score is: {_score}");
        }

        public void CalculateBubbleScore()
        {
            if (_datasetComponent != null)
                _requiredToCreateBubbleScore += _datasetComponent.AdditionalBubblePointsToRobot;
            
            if (_networkArchitectureComponent != null)
                _requiredToCreateBubbleScore += _networkArchitectureComponent.AdditionalBubblePointsToRobot;
            
            if (_modelSizeComponent != null)
                _requiredToCreateBubbleScore += _modelSizeComponent.AdditionalBubblePointsToRobot;
            
            if (_trainingMethodComponent != null)
                _requiredToCreateBubbleScore += _trainingMethodComponent.AdditionalBubblePointsToRobot;
        }

        public void SetBubbleCurrentCount(int firstBubble)
        {
            _currentToCreateBubbleScore = firstBubble;

            if (_currentToCreateBubbleScore < _requiredToCreateBubbleScore)
            {
                float missingPercent = 1f - (float)_currentToCreateBubbleScore / _requiredToCreateBubbleScore;
    
                _сharacters.Accuracy = Mathf.Clamp(Mathf.RoundToInt(_сharacters.Accuracy * (1f - missingPercent)), 0, 100);
                _сharacters.Flexibility = Mathf.Clamp(Mathf.RoundToInt(_сharacters.Flexibility * (1f - missingPercent)), 0, 100);
                _сharacters.Stability = Mathf.Clamp(Mathf.RoundToInt(_сharacters.Stability * (1f - missingPercent)), 0, 100);
                _сharacters.Adaptiveness = Mathf.Clamp(Mathf.RoundToInt(_сharacters.Adaptiveness * (1f - missingPercent)), 0, 100);
                
                CalculateScore();
            }
        }
    }
}