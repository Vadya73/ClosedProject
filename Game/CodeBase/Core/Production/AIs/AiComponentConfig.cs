using System;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "AI/Component Config", fileName = "AiComponentConfig", order = 0)]
    public class AiComponentConfig : ScriptableObject , ISkillPoint
    {
        [SerializeField] private string _name;
        [SerializeField] private int _additionalDaysToCreateRobot;
        [SerializeField] private int _additionalBubblePointsToRobot;
        [SerializeField] private AiComponentType _type;
        [SerializeField] private AiСharacters _influenceOnChars;

        public string Name => _name;
        public AiComponentType Type => _type;
        public int AdditionalDaysToCreateRobot => _additionalDaysToCreateRobot;
        public int AdditionalBubblePointsToRobot => _additionalBubblePointsToRobot;
        public AiСharacters InfluenceOnChars => _influenceOnChars;
    }
    
    [Serializable]
    public struct AiСharacters
    {
        [Range(0,100)] public int Accuracy;
        [Range(0,100)] public int Flexibility;
        [Range(0,100)] public int Stability;
        [Range(0,100)] public int Adaptiveness;
    }
    
}