using UnityEngine;

namespace Services
{
    [CreateAssetMenu(menuName = "Create WalletConfig", fileName = "WalletConfig", order = 0)]
    public class WalletConfig : ScriptableObject
    {
        [SerializeField] private WalletData _walletData;
        public WalletData WalletData => _walletData;
    }
}