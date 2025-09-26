using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Infrastructure.IAP
{
    [DisallowMultipleComponent]
    public class IAPManager : MonoBehaviour
    {
        [Header("Продукты (ID должны совпадать с App Store / Google Play)")]
        public List<IapItem> _products = new();

        private StoreController _store;

        private async void Awake()
        {
            _store = UnityIAPServices.StoreController();

            _store.OnProductsFetched += OnProductsFetched;
            _store.OnProductsFetchFailed += OnProductsFetchFailed;
            _store.OnPurchasesFetched += OnPurchasesFetched;

            _store.OnPurchasePending += OnPurchasePending;
            _store.OnPurchaseFailed += OnPurchaseFailed;
            _store.OnPurchaseConfirmed += OnPurchaseConfirmed;

            try
            {
                await _store.Connect();

                // Отдаём список ProductDefinition для фетчинга
                var defs = new List<ProductDefinition>(_products.Count);
                foreach (var p in _products)
                    defs.Add(new ProductDefinition(p.productId, p.type));
                _store.FetchProducts(defs); // после этого придёт OnProductsFetched
            }
            catch (Exception e)
            {
                Debug.LogError($"IAP connect failed: {e}");
            }
        }
        
        public void Buy(string productId)
        {
            if (_store == null)
            {
                Debug.LogWarning("Store not ready yet");
                return;
            }

            _store.PurchaseProduct(productId); // события Pending/Failed
        }

#if UNITY_IOS
        public void RestorePurchases()
        {
            _store?.RestoreTransactions((ok, error) =>
            {
                Debug.Log($"Restore finished. ok={ok}, error={error}");
            });
        }
#endif

        // ====== CALLBACKS ======

        private void OnProductsFetched(IReadOnlyCollection<Product> fetched)
        {
            Debug.Log($"Products fetched: {fetched.Count}");
            // Тут можно обновить UI ценами: p.metadata.localizedPriceString и т.п.
            // Также можно дернуть _store.FetchPurchases() если нужно подтянуть владения.
        }

        private void OnProductsFetchFailed(ProductFetchFailed obj)
        {
            Debug.LogError($"Products fetch failed: {obj.FailureReason}");
        }

        private void OnPurchasesFetched(Orders orders)
        {
            // Возвращаются подтверждённые покупки (например non-consumable / подписки).
            Debug.Log($"Purchases fetched: {orders}");
        }

        // Покупка ушла в стор, пользователь подтвердил, транзакция PENDING — пора выдать контент
        private void OnPurchasePending(PendingOrder pending)
        {
            try
            {
                // В pending.CartOrdered содержится корзина; обычно у нас один товар
                foreach (var item in pending.CartOrdered.Items())
                {
                    var prod = item.Product;
                    var id = prod.definition.id;       // айди продукта
                    var type = prod.definition.type;   // тип продукта

                    // 1) Выдать контент игроку (например, валюту)
                    GrantProduct(id, type);

                    // 2) Подтверждаем покупку (ACK), иначе Google/Apple будут считать её незавершённой
                    _store.ConfirmPurchase(pending);
                    Debug.Log($"Purchase confirmed: {id}");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error while granting purchase: {e}");
                // Если произошла ошибка начисления, НЕ подтверждать покупку, повторить логику позже.
            }
        }

        private void OnPurchaseConfirmed(Order order)
        {
            Debug.Log($"OnPurchaseConfirmed: {order}");
        }

        private void OnPurchaseFailed(FailedOrder failed)
        {
            Debug.LogWarning($"Purchase failed: {failed}");
        }

        // ====== Бизнес-логика выдачи контента ======

        private void GrantProduct(string productId, ProductType type)
        {
            // Пример: маппинг ID -> награда
            switch (productId)
            {
                case "coins_100":
                    AddCoins(100);
                    break;
                case "no_ads":
                    SetNoAdsOwned(true);
                    break;
                default:
                    Debug.LogWarning($"Unknown product id: {productId}");
                    break;
            }
        }

        private void AddCoins(int amount) { Debug.Log($"+{amount} coins"); /* ... */ }
        private void SetNoAdsOwned(bool owned) { Debug.Log($"NoAds={owned}"); /* ... */ }

        private void OnDestroy()
        {
            if (_store == null) return;

            _store.OnProductsFetched -= OnProductsFetched;
            _store.OnProductsFetchFailed -= OnProductsFetchFailed;
            _store.OnPurchasesFetched -= OnPurchasesFetched;

            _store.OnPurchasePending -= OnPurchasePending;
            _store.OnPurchaseFailed -= OnPurchaseFailed;
            _store.OnPurchaseConfirmed -= OnPurchaseConfirmed;
        }
    }
}
