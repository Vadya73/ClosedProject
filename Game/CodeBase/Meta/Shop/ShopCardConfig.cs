using UnityEngine;

namespace Meta.Shop
{
    [CreateAssetMenu(menuName = "Shop/ShopCardConfig", fileName = "ShopCardConfig", order = 0)]
    public class ShopCardConfig : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private Color _backgroundColor;
        [SerializeField] private float _costInDollars;
        [SerializeField] private ShopCardReward[] _reward;
        
        public Sprite Icon => _icon;
        public Color BackgroundColor => _backgroundColor;
        public float CostInDollars => _costInDollars;
        public ShopCardReward[] Reward => _reward;
    }
}