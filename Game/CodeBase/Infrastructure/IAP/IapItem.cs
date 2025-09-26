using System;
using UnityEngine.Purchasing;

namespace Infrastructure.IAP
{
    [Serializable]
    public class IapItem
    {
        public string productId;
        public ProductType type = ProductType.Consumable; // Consumable / NonConsumable / Subscription
    }
}