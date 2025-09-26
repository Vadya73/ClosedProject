using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "Team/Team Config", fileName = "TeamSystemConfig", order = 0)]
    public class TeamSystemConfig : ScriptableObject
    {
        [SerializeField] private UnhiredWorkerCard _unhiredWorkerCardPrefab;
        [SerializeField] private HiredWorkerCard _hiredWorkerCardPrefab;
        [SerializeField] private Worker _playerPrefab;
        [SerializeField] private WorkerConfig _playerConfig;
        [SerializeField] private List<WorkerConfig> _2LvlTeamConfig;
        [SerializeField] private List<WorkerConfig> _3LvlTeamConfig;
        [SerializeField] private List<WorkerConfig> _4LvlTeamConfig;
        [SerializeField] private List<WorkerConfig> _2LvlUnhiredTeamConfig;
        [SerializeField] private List<WorkerConfig> _3LvlUnhiredTeamConfig;
        [SerializeField] private List<WorkerConfig> _4LvlUnhiredTeamConfig;
        [SerializeField] private float _salaryMultiplier = 10;

        public UnhiredWorkerCard UnhiredWorkerCardPrefab => _unhiredWorkerCardPrefab;
        public HiredWorkerCard HiredWorkerCardPrefab => _hiredWorkerCardPrefab;
        public Worker PlayerPrefab => _playerPrefab;
        public WorkerConfig PlayerConfig => _playerConfig;
        public List<WorkerConfig> TwoLvlTeamConfig => _2LvlTeamConfig;
        public List<WorkerConfig> ThreeLvlTeamConfig => _3LvlTeamConfig;
        public List<WorkerConfig> FourLvlTeamConfig => _4LvlTeamConfig;
        public List<WorkerConfig> SecondLvlUnhiredTeamConfig => _2LvlUnhiredTeamConfig;
        public List<WorkerConfig> ThirdLvlUnhiredTeamConfig => _3LvlUnhiredTeamConfig;
        public List<WorkerConfig> FourLvlUnhiredTeamConfig => _4LvlUnhiredTeamConfig;
        public float SalaryMultiplier => _salaryMultiplier;
    }
}