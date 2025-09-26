using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class Robot : IData
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private RobotTypes _robotType;
        [SerializeField] private RobotSubtypes _robotSubType;
        [SerializeField] private List<RobotAddonConfig> _addons = new(); 
        [SerializeField] private RobotComponentConfig _materialComponent;
        [SerializeField] private AI _aiComponent;
        [SerializeField] private RobotComponentConfig _chipComponent;
        [SerializeField] private RobotComponentConfig _engineComponent;
        [SerializeField] private RobotComponentConfig _batteryComponent;
        [SerializeField] private ProductSaleData _productSaleData;
        [SerializeField] private RobotСharacters _robotСharacters;
        [SerializeField] private int _score;
        [SerializeField] private RequiredBubbles _requiredBubblesScore;
        [SerializeField] private Sprite _sprite;
        
        public string Name => _name;
        public string Description => _description;
        public RobotSubtypes RobotSubType => _robotSubType;
        public ProductSaleData ProductSaleData => _productSaleData;
        public RobotСharacters RobotСharacters => _robotСharacters;
        public int Score => _score;
        public RequiredBubbles RequiredBubblesScore => _requiredBubblesScore;
        public Sprite Image => _sprite;


        public void SetComponent(RobotComponentConfig buildRobotComponent)
        {
            switch (buildRobotComponent.Type)
            {
                case RobotComponentType.Material:
                    _materialComponent = buildRobotComponent;
                    break;
                case RobotComponentType.Chip:
                    _chipComponent = buildRobotComponent;
                    break;
                case RobotComponentType.Engine:
                    _engineComponent = buildRobotComponent;
                    break;
                case RobotComponentType.Battery:
                    _batteryComponent = buildRobotComponent;
                    break;
            }
        }

        public void SetAddons(RobotAddonConfig buildRobotAddon)
        {
            _addons.Add(buildRobotAddon);
        }

        public void CreateProductSaleData()
        {
            _productSaleData = new ProductSaleData();
        }

        public void SetScore(int score) => _score = score;

        public void CalculateParams()
        {
            if (_batteryComponent != null)
            {
                if (_batteryComponent.AcceptedRobots.HasFlag(_robotSubType))
                    _robotСharacters.Longevity = _batteryComponent.InfluenceOnRobot.Longevity;
                else
                {
                    float twentyPercent = _batteryComponent.InfluenceOnRobot.Longevity * 0.2f;
                    _robotСharacters.Longevity = _batteryComponent.InfluenceOnRobot.Longevity - (int)twentyPercent;
                }
            }
            
            if (_chipComponent != null)
            {
                if (_chipComponent.AcceptedRobots.HasFlag(_robotSubType))
                    _robotСharacters.Responsiveness = _chipComponent.InfluenceOnRobot.Responsiveness;
                else
                {
                    float twentyPercent = _chipComponent.InfluenceOnRobot.Responsiveness * 0.2f;
                    _robotСharacters.Responsiveness = _chipComponent.InfluenceOnRobot.Responsiveness - (int)twentyPercent;
                }
            }
            if (_engineComponent != null)
            {
                if (_engineComponent.AcceptedRobots.HasFlag(_robotSubType))
                    _robotСharacters.Speed = _engineComponent.InfluenceOnRobot.Speed;
                else
                {
                    float twentyPercent = _engineComponent.InfluenceOnRobot.Speed * 0.2f;
                    _robotСharacters.Speed = _engineComponent.InfluenceOnRobot.Speed - (int)twentyPercent;
                }
            }
            if (_materialComponent != null)
            {
                if (_materialComponent.AcceptedRobots.HasFlag(_robotSubType))
                    _robotСharacters.Weight = _materialComponent.InfluenceOnRobot.Weight;
                else
                {
                    float twentyPercent = _materialComponent.InfluenceOnRobot.Weight * 0.2f;
                    _robotСharacters.Weight = _materialComponent.InfluenceOnRobot.Weight - (int)twentyPercent;
                }
            }
            if (_aiComponent != null)
            {
                _robotСharacters.Intellect = (_aiComponent.Characters.Adaptiveness
                                              + _aiComponent.Characters.Flexibility
                                              + _aiComponent.Characters.Accuracy
                                              + _aiComponent.Characters.Stability) / 4;
            }
        }

        public void SetTypeAndSubType(RobotTypes robotType, RobotSubtypes robotSubtype)
        {
            _robotType = robotType;
            _robotSubType = robotSubtype;
        }

        public void CalculateScore()
        {
            var score = (_robotСharacters.Intellect + _robotСharacters.Responsiveness + _robotСharacters.Longevity +
                    _robotСharacters.Speed + _robotСharacters.Weight) / 5;
            var clamp = Mathf.Clamp(score, 0, 100);
            
            _score = clamp;
            Debug.Log($"Robot Score is: {_score}");
        }

        public void SetName(string name) => _name = name;
        public void SetDescription(string description) => _description = description;
        public void SetAI(AI componentCurrentAI) => _aiComponent = componentCurrentAI;

        public void CalculateBubblesScore()
        {
            if (_batteryComponent != null)
            {
                _requiredBubblesScore.DesignRequiredBubbles += _batteryComponent.AdditionalPointsToBubbles;
            }
            if (_chipComponent != null)
            {
                _requiredBubblesScore.TechnologyRequiredBubbles += _chipComponent.AdditionalPointsToBubbles;
            }
            if (_engineComponent != null)
            {
                _requiredBubblesScore.DesignRequiredBubbles += _engineComponent.AdditionalPointsToBubbles;
            }
            if (_materialComponent != null)
            {
                _requiredBubblesScore.DesignRequiredBubbles += _materialComponent.AdditionalPointsToBubbles;
            }
            if (_aiComponent != null)
            {
                _requiredBubblesScore.TechnologyRequiredBubbles += _aiComponent.AdditionalPointsToBubblesRobot;
            }
        }

        public void SetBubbleCurrentCount(int firstBubble, int secondBubble)
        {
            _requiredBubblesScore.TechnologyCurrentBubbles = firstBubble;
            _requiredBubblesScore.DesignCurrentBubbles = secondBubble;

            if (_requiredBubblesScore.TechnologyCurrentBubbles < _requiredBubblesScore.TechnologyRequiredBubbles)
            {
                float missingPercent = 1f - (float)_requiredBubblesScore.TechnologyCurrentBubbles / _requiredBubblesScore.TechnologyRequiredBubbles;
                
                Debug.Log($"Before Responsiveness: {_robotСharacters.Responsiveness}");
                _robotСharacters.Responsiveness = Mathf.Clamp(Mathf.RoundToInt(_robotСharacters.Responsiveness * (1f - missingPercent)), 0, 100);
                Debug.Log($"After Responsiveness: {_robotСharacters.Responsiveness}");
                
                Debug.Log($"Before Intellect: {_robotСharacters.Intellect}");
                _robotСharacters.Intellect = Mathf.Clamp(Mathf.RoundToInt(_robotСharacters.Intellect * (1f - missingPercent)), 0, 100);
                Debug.Log($"After Intellect: {_robotСharacters.Intellect}");
                
                CalculateScore();
            }

            if (_requiredBubblesScore.DesignCurrentBubbles < _requiredBubblesScore.DesignRequiredBubbles)
            {
                float missingPercent = 1f - (float)_requiredBubblesScore.DesignCurrentBubbles / _requiredBubblesScore.DesignRequiredBubbles;

                Debug.Log($"Before Longevity: {_robotСharacters.Longevity}");
                _robotСharacters.Longevity = Mathf.Clamp(Mathf.RoundToInt(_robotСharacters.Longevity * (1f - missingPercent)), 0, 100);
                Debug.Log($"After Longevity: {_robotСharacters.Longevity}");
                
                Debug.Log($"Before Weight: {_robotСharacters.Weight}");
                _robotСharacters.Weight = Mathf.Clamp(Mathf.RoundToInt(_robotСharacters.Weight * (1f - missingPercent)), 0, 100);
                Debug.Log($"After Longevity: {_robotСharacters.Weight}");

                Debug.Log($"Before Speed: {_robotСharacters.Speed}");
                _robotСharacters.Speed = Mathf.Clamp(Mathf.RoundToInt(_robotСharacters.Speed * (1f - missingPercent)), 0, 100);
                Debug.Log($"After Speed: {_robotСharacters.Speed}");
                
                CalculateScore();
            }
        }
    }

    [Serializable]
    public struct RequiredBubbles
    {
        public int DataScienceCurrentBubbles;
        public int DataScienceRequiredBubbles;
        
        public int ResearchCurrentBubbles;
        public int ResearchRequiredBubbles;
        
        public int DesignCurrentBubbles;
        public int DesignRequiredBubbles;
        
        public int TechnologyCurrentBubbles;
        public int TechnologyRequiredBubbles;
    }
}