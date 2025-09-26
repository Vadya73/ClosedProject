using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Meta
{
    [Serializable]
    public class DataCenterLevel
    {
        [SerializeField] private DataCenterConfig _levelConfig;
        [SerializeField, ChildGameObjectsOnly] private Transform _bubbleSpawnPosition;
        [SerializeField, ChildGameObjectsOnly] private GameObject _visualGameobject;
        
        public DataCenterConfig LevelConfig => _levelConfig;
        public Transform BubbleSpawnPosition => _bubbleSpawnPosition;
        public GameObject VisualGameobject => _visualGameobject;

        public void SetAcivateLevel(bool active)
        {
            _levelConfig.SetActivate(active);
            _visualGameobject.SetActive(active);
        }
    }
}