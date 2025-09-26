using System;
using Services;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "Team/Worker Config", fileName = "WorkerConfig", order = 0)]
    public class WorkerConfig : ScriptableObject
    {
        [SerializeField] private WorkerData _workerData;
        [SerializeField] private Worker _workerPrefab;
        
        public WorkerData WorkerData => _workerData;
        public Worker WorkerPrefab => _workerPrefab;
    }

    [Serializable]
    public class WorkerData : IData
    {
        [SerializeField] private bool _isPlayer;
        [SerializeField] private string _name;
        [SerializeField, TextArea] private string _description;
        [SerializeField] private Sprite _avatar;
        [SerializeField] private long _defaultSalary;
        [SerializeField, ReadOnly] private long _currentCurrentSalary;
        [SerializeField] private WorkerСharacteristics _stats;
        [SerializeField, ReadOnly] private float _buildRobotBubbleCooldown;
        [SerializeField, ReadOnly] private float _trainingAiBubbleCooldown;
        [SerializeField, ReadOnly] private float _researchBubbleCooldown;
        
        public string Name => _name;
        public string Description => _description;
        public Sprite Avatar => _avatar;
        public long CurrentSalary => _currentCurrentSalary;
        public WorkerСharacteristics Stats => _stats;
        public float BuildRobotBubbleCooldown => _buildRobotBubbleCooldown;
        public float TrainingAiBubbleCooldown => _trainingAiBubbleCooldown;
        public float ResearchBubbleCooldown => _researchBubbleCooldown;

        public void ResetBubbleCooldown(ProductionType productionType, TimeData tData)
        {
            switch (productionType)
            {
                case ProductionType.BuildRobot:
                    float buildRobotAvgStat = ((_stats.DesignLevel + _stats.TechnologyLevel) / 2f) / tData.BubbleTimeDivider; // 0–10
                    _buildRobotBubbleCooldown = tData.TimePerDayInSeconds / buildRobotAvgStat;
                    break;
                case ProductionType.TrainingAI:                    
                    float trainingAIAvgStat = _stats.DataScienceLevel / tData.BubbleTimeDivider;
                    _trainingAiBubbleCooldown = tData.TimePerDayInSeconds / trainingAIAvgStat;
                    break;
                case ProductionType.Research:
                    float researchAvgStat = _stats.ResearchLevel / tData.BubbleTimeDivider;
                    _researchBubbleCooldown = tData.TimePerDayInSeconds / researchAvgStat;
                    break;
            }
        }

        public void ResetAllBubbleCooldown(TimeData timeData)
        {
            float buildRobotAvgStat = ((_stats.DesignLevel + _stats.TechnologyLevel) / 2f) / timeData.BubbleTimeDivider;
            _buildRobotBubbleCooldown = timeData.TimePerDayInSeconds / buildRobotAvgStat;
            
            float trainingAIAvgStat = _stats.DataScienceLevel / timeData.BubbleTimeDivider;
            _trainingAiBubbleCooldown = timeData.TimePerDayInSeconds / trainingAIAvgStat;
            
            float researchAvgStat = _stats.ResearchLevel / timeData.BubbleTimeDivider;
            _researchBubbleCooldown = timeData.TimePerDayInSeconds / researchAvgStat;
        }

        public void MinusCooldown(float deltaTime, ProductionType productionType)
        {
            switch (productionType)
            {
                case ProductionType.BuildRobot:
                    _buildRobotBubbleCooldown -= deltaTime;
                    break;
                case ProductionType.TrainingAI:
                    _trainingAiBubbleCooldown -= deltaTime;
                    break;
                case ProductionType.Research:
                    _researchBubbleCooldown -= deltaTime;
                    break;
            }
        }

        public long CalculateSalary(long multiplier)
        {
            if (_isPlayer)
                return 0;
            
            var statsSummaryLevel = _stats.DesignLevel + _stats.TechnologyLevel + _stats.DataScienceLevel + _stats.ResearchLevel;
            _currentCurrentSalary = (statsSummaryLevel * multiplier) + _defaultSalary;
            
            return _currentCurrentSalary;
        }
    }

    [Serializable]
    public class WorkerСharacteristics
    {
        public int MaxLevel = 30;
        [Header("DataScience")]
        public WalletType DataScienceWalletType;
        public long DataScienceDefaultUpgradeCost;
        [PropertyRange(1, "MaxLevel")] public int DataScienceLevel;
        [Header("Technology")]
        public WalletType TechnologyWalletType;
        public long TechnologyDefaultUpgradeCost;
        [PropertyRange(1,"MaxLevel")] public int TechnologyLevel;
        [Header("Design")]
        public WalletType DesignWalletType;
        public long DesignDefaultUpgradeCost;
        [PropertyRange(1,"MaxLevel")] public int DesignLevel;
        [Header("Research")]
        public WalletType ResearchWalletType;
        public long ResearchDefaultUpgradeCost;
        [PropertyRange(1,"MaxLevel")] public int ResearchLevel;
        
        public void AddDataScience(int science) => DataScienceLevel += science;
        public void AddTechnology(int science) => TechnologyLevel += science;
        public void AddDesign(int science) => DesignLevel += science;
        public void AddResearch(int science) => ResearchLevel += science;
    }
}
