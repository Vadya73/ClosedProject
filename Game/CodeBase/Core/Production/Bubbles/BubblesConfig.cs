using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "BubblesConfig", fileName = "BubblesConfig", order = 0)]
    public class BubblesConfig : ScriptableObject
    {
        [SerializeField] private Color _buildRobotDesignColor;
        [SerializeField] private Color _buildRobotTechnologyColor;
        [SerializeField] private Color _aiTrainingDataScienceColor;
        [SerializeField] private Color _researchColor;
        
        public Color BuildRobotDesignColor => _buildRobotDesignColor;
        public Color BuildRobotTechnologyColor => _buildRobotTechnologyColor;
        public Color AITrainingDataScienceColor => _aiTrainingDataScienceColor;
        public Color ResearchColor => _researchColor;
    }
}