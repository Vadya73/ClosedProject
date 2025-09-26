using System;
using Services;

namespace Meta.Shop
{
    [Serializable]
    public struct ShopCardReward
    {
        public WalletType Type;
        public float CountToGiveReward;
    }
}