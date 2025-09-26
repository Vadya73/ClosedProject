using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Meta
{
    [Serializable]
    public class DataCenterData
    {
        [SerializeField] private List<DataCenterConfig> _allHaveDataCenterLevelsThirdLevel;
        [SerializeField] private List<DataCenterConfig> _allHaveDataCenterLevelsFourLevel;
        
        public List<DataCenterConfig> AllHaveDataCenterLevelsThirdLevel => _allHaveDataCenterLevelsThirdLevel;
        public List<DataCenterConfig> AllHaveDataCenterLevelsFourLevel => _allHaveDataCenterLevelsFourLevel;

        public void AddLevel(ISkillPoint skillPointData)
        {
            DataCenterConfig dataCenterConfig = (DataCenterConfig)skillPointData;

            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 3:
                    _allHaveDataCenterLevelsThirdLevel.Add(dataCenterConfig);
                    break;
                case 4:
                    _allHaveDataCenterLevelsFourLevel.Add(dataCenterConfig);
                    break;
            }
        }

        public void Initialize()
        {
            _allHaveDataCenterLevelsThirdLevel ??= new List<DataCenterConfig>();
            _allHaveDataCenterLevelsFourLevel ??= new List<DataCenterConfig>();
        }
    }
}