using System;
using System.Collections.Generic;
using Core;
using Meta;
using OtherSO;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerConfig", fileName = "PlayerConfig", order = 0)]
public class PlayerConfig : ScriptableObject
{
    [SerializeField] private BubblesConfig _bubbleConfig;
    [SerializeField] private LeaderboardConfig _1LevelLeaderboardConfig;
    [SerializeField] private RobotNamesConfig _robotNamesConfig;
    [SerializeField] private AiDataConfig _aiDataConfig;
    [SerializeField] private Bubble _bubblePrefab;
    [SerializeField] private TaskView _taskViewPrefab;
    [SerializeField] private SaleGraph _saleGraphPrefab;
    [SerializeField] private BuildRobotData _buildRobotData;
    [SerializeField] private TrainingAIData _trainingAIData;
    [SerializeField] private LocationsData _locationsData;
    [SerializeField] private DataCenterData _dataCenterData;
    [SerializeField] private SkillData _skillsData;
    [SerializeField] private TaskData[] _currentQuests;

    public BubblesConfig BubbleConfig => _bubbleConfig;
    public LeaderboardConfig FirstLevelLeaderboardConfig => _1LevelLeaderboardConfig;
    public BuildRobotData BuildRobotData => _buildRobotData;
    public  TrainingAIData TrainingAIData => _trainingAIData;
    public LocationsData LocationData => _locationsData;
    public RobotNamesConfig RobotNamesConfig => _robotNamesConfig;
    public AiDataConfig AiDataConfig => _aiDataConfig;
    public Bubble BubblePrefab => _bubblePrefab;
    public SkillData SkillsData => _skillsData;
    public TaskData[] CurrentQuests => _currentQuests;
    public TaskView TaskViewPrefab => _taskViewPrefab;
    public SaleGraph SaleGraph => _saleGraphPrefab;
    public DataCenterData DataCenterData => _dataCenterData;


    private void Awake()
    {
        _buildRobotData.Initialize();
        _trainingAIData.Initialize();
        _locationsData.Initialize(); 
        _skillsData.Initialize();
        _dataCenterData.Initialize();
        _currentQuests ??= new TaskData[0];

        foreach (var data in _dataCenterData.AllHaveDataCenterLevelsFourLevel)
        {
            data.SetActivate(true);
        }
    }

    public bool PlayerHasSubtype(RobotSubtypes subtype)
    {
        return (_buildRobotData.PlayerHaveRobotSubtypes & subtype) == subtype;
    }

    public void AddRobotSubtype(ISkillPoint skillPointData)
    {
        RobotSubtypeResearch robotSubtypeResearch = (RobotSubtypeResearch)skillPointData;
        _buildRobotData.AddRobotSubtype(robotSubtypeResearch.RobotSubtype);
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
    
    public void DeleteRobotSubtype(RobotSubtypes subtypesToRemove)
    {
        _buildRobotData.DeleteRobotSubtype(subtypesToRemove);
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
    
    [Serializable]
    public class LocationsData
    {
        [SerializeField] private List<LevelConfig> _allHaveLocations = new();
        
        public List<LevelConfig> AllHaveLocations => _allHaveLocations;

        public void Initialize()
        {
            _allHaveLocations ??= new List<LevelConfig>();
        }

        public void AddLocation(ISkillPoint skillPointData)
        {
            LevelConfig loc = skillPointData as LevelConfig;
            _allHaveLocations.Add(loc);
        }
    }
    
    [Serializable]
    public class SkillData
    {
        public void Initialize()
        {
        }

        public void AddSkill(ISkillPoint skillPointData)
        {
        }
    }
}