using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "Research/Robot Subtype Research", fileName = "RobotSubtypeResearch", order = 0)]
    public class RobotSubtypeResearch : ScriptableObject, ISkillPoint
    {
        [SerializeField] private RobotSubtypes _robotSubtype;
        
        public RobotSubtypes RobotSubtype => _robotSubtype;
    }
}