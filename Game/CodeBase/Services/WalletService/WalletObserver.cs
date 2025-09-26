using System;
using VContainer;
using VContainer.Unity;

namespace Services
{
    public class WalletObserver : IInitializable, IStartable, IDisposable
    {
        private readonly Wallet _wallet;
        private readonly WalletView _walletView;

        [Inject]
        public WalletObserver(Wallet wallet, WalletView walletView)
        {
            _wallet = wallet;
            _walletView = walletView;
        }

        void IInitializable.Initialize()
        {
            _wallet.OnWalletChanged += OnWalletChanged;
        }

        void IStartable.Start()
        {
            SetWalletData(_wallet.WalletData);
            //without this inject dont work (??)
        }

        void IDisposable.Dispose()
        {
            _wallet.OnWalletChanged -= OnWalletChanged;
        }

        private void OnWalletChanged(WalletData walletData, WalletType walletType, long oldCount, long newCount)
        {
            _walletView.UpdateView(walletType, oldCount, newCount);
            _walletView.ShowChangeWalletPopUp(walletType, newCount - oldCount);
        }

        private void SetWalletData(WalletData walletData) => _walletView.UpdateView(walletData);
    }
}