using System.Collections.Generic;
using Core;
using Meta;
using Services;
using UnityEngine;
using VContainer;

namespace Infrastructure.SaveLoadData
{
    public class SaveLoadSystem : MonoBehaviour
    {
        private List<WorkerConfig> _2LvlTeamConfig;
        private List<WorkerConfig> _3LvlTeamConfig;
        private List<WorkerConfig> _4LvlTeamConfig;
        private List<WorkerConfig> _2LvlUnhiredTeamConfig;
        private List<WorkerConfig> _3LvlUnhiredTeamConfig;
        private List<WorkerConfig> _4LvlUnhiredTeamConfig;
        
        private WalletData _walletData;
        private LeaderboardData _aiLeaderboardData;
        private LeaderboardData _robotLeaderboardData;
        private BuildRobotData _buildRobotData;
        private TrainingAIData _trainingAIData;
        private PlayerConfig.LocationsData _locationData;
        private DataCenterData _dataCenterData;
        private TaskData _taskData;
        private TimeData _timeData;
        
        private RobotsData _haveRobotsData;

        [Inject]
        private void Construct(PlayerConfig playerConfig)
        {
            
        }
        
        public void Awake()
        {
            LoadData();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
                SaveData();
        }

        private void SaveData()
        {
            ES3.Save("2lvlTeamConfig", _2LvlTeamConfig);
            ES3.Save("3lvlTeamConfig", _3LvlTeamConfig);
            ES3.Save("4lvlTeamConfig", _4LvlTeamConfig);
            ES3.Save("2lvlUnhiredTeamConfig", _2LvlUnhiredTeamConfig);
            ES3.Save("3lvlUnhiredTeamConfig", _3LvlUnhiredTeamConfig);
            ES3.Save("4lvlUnhiredTeamConfig", _4LvlUnhiredTeamConfig);
            ES3.Save("walletData", _walletData);
            ES3.Save("aiLeaderboardData", _aiLeaderboardData);
            ES3.Save("robotLeaderboardData", _robotLeaderboardData);
            ES3.Save("buildRobotData", _buildRobotData);
            ES3.Save("trainingAIData", _trainingAIData);
            ES3.Save("locationData", _locationData);
            ES3.Save("dataCenterData", _dataCenterData);
            ES3.Save("taskData", _taskData);
            ES3.Save("timeData", _timeData);
        }

        private void LoadData()
        { 
            ES3.Load("2lvlTeamConfig");
            ES3.Load("3lvlTeamConfig");
            ES3.Load("4lvlTeamConfig");
            ES3.Load("2lvlUnhiredTeamConfig");
            ES3.Load("3lvlUnhiredTeamConfig");
            ES3.Load("4lvlUnhiredTeamConfig");
            ES3.Load("walletData");
            ES3.Load("aiLeaderboardData");
            ES3.Load("robotLeaderboardData");
            ES3.Load("buildRobotData");
            ES3.Load("trainingAIData");
            ES3.Load("locationData");
            ES3.Load("dataCenterData");
            ES3.Load("taskData");
            ES3.Load("timeData");
        }
    }
}
