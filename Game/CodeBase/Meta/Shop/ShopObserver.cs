using System;
using VContainer.Unity;

namespace Meta.Shop
{
    public class ShopObserver : IInitializable, IDisposable
    {
        private readonly ShopView _shopView;
        private readonly ShopController _shopController;

        public ShopObserver(ShopView shopView, ShopController shopController)
        {
            _shopView = shopView;
            _shopController = shopController;
        }

        public void Initialize()
        {
            _shopView.ExitButton.onClick.AddListener(HideView);
            _shopView.CardsButton.onClick.AddListener(ShowCardsView);
            _shopView.SetsButton.onClick.AddListener(ShowSetsView);
            _shopView.CashButton.onClick.AddListener(ShowCashView);
        }

        public void Dispose()
        {
            _shopView.ExitButton.onClick.RemoveAllListeners();
            _shopView.CardsButton.onClick.RemoveAllListeners();
            _shopView.SetsButton.onClick.RemoveAllListeners();
            _shopView.CashButton.onClick.RemoveAllListeners();
        }

        private void ShowCashView()
        {
            _shopController.ShowCashView();
        }

        private void ShowSetsView()
        {
            _shopController.ShowSetsView();
        }

        private void ShowCardsView()
        {
            _shopController.ShowCardsView();
        }

        private void HideView()
        {
            _shopController.HideShopView();
        }
    }
}