using Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Meta
{
    [CreateAssetMenu(menuName = "DataCenterLevel Config", fileName = "DataCenterConfig", order = 0)]
    public class DataCenterConfig : ScriptableObject, ISkillPoint
    {
        [SerializeField] private int _level;
        [SerializeField] private float _bubbleSpawnCooldown; 
        [SerializeField, ReadOnly] private float _currentBubbleSpawnCooldown;
        [SerializeField] private bool _hasBuy;
        public bool HasBuy => _hasBuy;
        public float CurrentBubbleSpawnCooldown => _currentBubbleSpawnCooldown;
        public float BubbleSpawnCooldown => _bubbleSpawnCooldown;

        public void SetActivate(bool active)
        {
            _hasBuy = active;
            _currentBubbleSpawnCooldown = _bubbleSpawnCooldown;
        }
        
        public void SetCurrentSpawnCooldown(float cooldown) => _currentBubbleSpawnCooldown = cooldown;
        public void SubtractCurrentBubbleSpawnCooldown(float cooldown) => _currentBubbleSpawnCooldown -= cooldown;
        public void SetBubbleSpawnCooldown(float cooldown) => _bubbleSpawnCooldown = cooldown;
    }
}