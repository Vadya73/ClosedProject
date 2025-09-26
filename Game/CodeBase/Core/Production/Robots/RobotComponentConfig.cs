using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "BuildRobot/RobotComponentConfig", fileName = "RobotComponentConfig", order = 0)]
    public class RobotComponentConfig : ScriptableObject, ISkillPoint
    {
        [SerializeField] private string _name;
        [SerializeField] private int _additionalDaysToCreateRobot;
        [SerializeField] private int _additionalPointsToBubbles;
        [SerializeField] private RobotComponentType _type;
        [SerializeField] private RobotSubtypes _acceptedRobots;
        [SerializeField] private RobotСharacters _influenceOnRobot;

        public string Name => _name;
        public RobotComponentType Type => _type;
        public int AdditionalDaysToCreateRobot => _additionalDaysToCreateRobot;
        public int AdditionalPointsToBubbles => _additionalPointsToBubbles;
        public RobotSubtypes AcceptedRobots => _acceptedRobots;
        public RobotСharacters InfluenceOnRobot => _influenceOnRobot;
    }

    public interface ISkillPoint
    {
    }
}