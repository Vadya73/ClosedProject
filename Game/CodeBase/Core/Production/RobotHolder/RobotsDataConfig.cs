using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "Robots/Robots Data Config", fileName = "RobotsDataConfig", order = 0)]
    public class RobotsDataConfig : ScriptableObject
    {
        [SerializeField] private RobotsData _robotsData;
        
        public RobotsData RobotsData => _robotsData;
    }
    
    [Serializable]
    public class RobotsData
    {
        [SerializeField] private List<Robot> _currentRobots = new();
        
        public List<Robot> CurrentRobots => _currentRobots;

        public void AddRobot(Robot robot)
        {
            _currentRobots.Add(robot);
        }
    }
}