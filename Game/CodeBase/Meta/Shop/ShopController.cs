using Core.CameraControl;
using Infrastructure;
using Services;
using VContainer.Unity;

namespace Meta.Shop
{
    public class ShopController : IStartable
    {
        private readonly ShopView _shopView;
        private readonly Wallet _wallet;
        private readonly AudioController _audioController;
        private readonly CameraController _cameraController;

        public ShopController(ShopView shopView, Wallet wallet,AudioController audioController, CameraController cameraController)
        {
            _shopView = shopView;
            _wallet = wallet;
            _audioController = audioController;
            _cameraController = cameraController;
        }

        public void Start()
        {
            _shopView.ForcedHide();
            _shopView.UpdateWallets(_wallet.WalletData);
        }

        public void ShowCashView()
        {
            _shopView.CashPanel.SetActive(true);
            _shopView.CashButtonImage.color = _shopView.BackgroundColor;
            
            _shopView.CardsPanel.SetActive(false);
            _shopView.CardsButtonImage.color = _shopView.ButtonColor;
            
            _shopView.SetsPanel.SetActive(false);
            _shopView.SetsButtonImage.color = _shopView.ButtonColor;
            
        }

        public void ShowSetsView()
        {
            _shopView.SetsPanel.SetActive(true);
            _shopView.SetsButtonImage.color = _shopView.BackgroundColor;
            
            _shopView.CardsPanel.SetActive(false);
            _shopView.CardsButtonImage.color = _shopView.ButtonColor;
            
            _shopView.CashPanel.SetActive(false);
            _shopView.CashButtonImage.color = _shopView.ButtonColor;
        }

        public void ShowCardsView()
        {
            _shopView.CardsPanel.SetActive(true);
            _shopView.CardsButtonImage.color = _shopView.BackgroundColor;
            
            _shopView.CashPanel.SetActive(false);
            _shopView.CashButtonImage.color = _shopView.ButtonColor;
            
            _shopView.SetsPanel.SetActive(false);
            _shopView.SetsButtonImage.color = _shopView.ButtonColor;
        }

        public void HideShopView()
        {
            if (_shopView.IsAnimated)
                return;
            
            _shopView.Hide(); 
            _cameraController.SetActiveStateCamera(true);
        }

        public void ShowView()
        {
            if (_shopView.IsAnimated)
                return;

            if (_shopView.IsActive)
            {
                HideShopView();
                return;
            }
            
            _cameraController.SetActiveStateCamera(false);
            
            _shopView.Show();
            _shopView.CardsPanel.SetActive(true);
            _shopView.CardsButtonImage.color = _shopView.BackgroundColor;
            
            _shopView.SetsPanel.SetActive(false);
            _shopView.SetsButtonImage.color = _shopView.ButtonColor;
            
            _shopView.CashPanel.SetActive(false);
            _shopView.CashButtonImage.color = _shopView.ButtonColor;
        }

        public void TryPurchase(ShopCardConfig cardConfig)
        {
            // add IAP
            foreach (var shopCardReward in cardConfig.Reward)
            {
                _wallet.AddWalletCount(shopCardReward.Type, (long)shopCardReward.CountToGiveReward);
                _audioController.PlayEffects(_audioController.SoundConfigs.ShopPurchaseSound);
                _shopView.UpdateWallets(_wallet.WalletData);
            }
        }
    }
}