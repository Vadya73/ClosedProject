using Sirenix.OdinInspector;
using UnityEngine;

namespace Meta.RouletteWheel
{
    [CreateAssetMenu(menuName = "WheelOfFortune Config", fileName = "WheelOfFortuneConfig", order = 0)]
    public class WheelConfig : ScriptableObject
    {
        [Header("Сектора рулетки (по часовой стрелке)")]
        public WheelReward[] rewards = new WheelReward[8];

        [Header("Настройки вращения")]
        public float spinDuration = 4f; // время крутки
        public int extraRounds = 5; // кол-во полных оборотов

        [Header("Кулдаун")]
        public float cooldownSeconds = 10f;
    }
    
    [System.Serializable]
    public class WheelReward
    {
        [EnumToggleButtons] public RewardType RewardType;
        public int Amount;
    }
    
    public enum RewardType
    {
        None = 0,
        Cash = 1,
        Tokens = 2,
        GreenCard = 3,
        PurpleCard = 4
    }
}