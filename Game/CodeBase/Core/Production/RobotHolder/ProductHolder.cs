using System.Collections.Generic;
using VContainer.Unity;

namespace Core
{
    public class ProductHolder : IInitializable
    {
        private readonly RobotsData _robotsData;
        private readonly AIData _aIData;
        
        private List<Robot> _currentRobots;
        private List<AI> _currentAis;

        public ProductHolder(RobotsData robotsData, AIData aIData)
        {
            _robotsData = robotsData;
            _aIData = aIData;
            _currentRobots = robotsData.CurrentRobots;
            _currentAis = aIData.CurrentAis;
        }

        public void Initialize()
        {
        }

        public void AddRobot(Robot robot)
        {
            _currentRobots.Add(robot);
        }

        public void AddAI(AI ai)
        {
            _currentAis.Add(ai);
        }
    }
}