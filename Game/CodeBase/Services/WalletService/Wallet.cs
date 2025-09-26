using Infrastructure;
using VContainer;

namespace Services
{
    public sealed class Wallet
    {
        public delegate void WalletDelegate(WalletData walletData, WalletType walletType, long oldCount, long newCount);
        public event WalletDelegate OnWalletChanged;
        
        private readonly WalletConfig _walletConfig;
        private readonly AudioController _audioController;
        private readonly WalletData _walletData;
        public WalletData WalletData => _walletData;
        public WalletConfig WalletConfig => _walletConfig;

        [Inject]
        public Wallet(WalletConfig walletConfig, AudioController audioController)
        {
            _walletConfig = walletConfig;
            _audioController = audioController;
            _walletData = walletConfig.WalletData;
        }

        public void AddWalletCount(WalletType walletType, long count)
        {
            long oldCount;
            switch (walletType)
            {
                case WalletType.Cash:
                    oldCount = _walletData.CashCount;
                    _walletData.AddWallet(WalletType.Cash, count);
                    _audioController.PlayEffects(_audioController.SoundConfigs.AddCashSound);
                    OnWalletChanged?.Invoke(_walletData, walletType, oldCount, _walletData.CashCount);
                    break;
                case WalletType.Tokens:
                    oldCount = _walletData.TokensCount;
                    _walletData.AddWallet(walletType, count);
                    _audioController.PlayEffects(_audioController.SoundConfigs.AddTokensSound);
                    OnWalletChanged?.Invoke(_walletData, walletType, oldCount, _walletData.TokensCount);
                    break;
                case WalletType.GreenCard:
                    oldCount = _walletData.TokensCount;
                    _walletData.AddWallet(walletType, count);
                    _audioController.PlayEffects(_audioController.SoundConfigs.AddTokensSound);
                    OnWalletChanged?.Invoke(_walletData, walletType, oldCount, _walletData.GreenCardCount);
                    break;
                case WalletType.PurpleCard:
                    oldCount = _walletData.TokensCount;
                    _walletData.AddWallet(walletType, count);
                    _audioController.PlayEffects(_audioController.SoundConfigs.AddTokensSound);
                    OnWalletChanged?.Invoke(_walletData, walletType, oldCount, _walletData.PurpleCardCount);
                    break;
            }
        }
        
        public bool TrySpend(WalletType walletType, long count)
        {
            switch (walletType)
            {
                case WalletType.Cash:
                {
                    if (count > _walletData.CashCount)
                        return false;

                    Spend(walletType, count);
                    return true;
                }
                case WalletType.Tokens:
                {
                    if (count > _walletData.TokensCount)
                        return false;

                    Spend(walletType, count);
                    return true;
                }
                default:
                    return false;
            }
        }

        private void Spend(WalletType walletType, long count)
        {
            long oldCount;
            switch (walletType)
            {
                case WalletType.Cash:
                    oldCount = _walletData.CashCount;
                    _walletData.SpendWallet(WalletType.Cash, count);
                    OnWalletChanged?.Invoke(_walletData, walletType, oldCount, _walletData.CashCount);
                    break;
                case WalletType.Tokens:
                    oldCount = _walletData.TokensCount;
                    _walletData.SpendWallet(walletType, count);
                    OnWalletChanged?.Invoke(_walletData, walletType, oldCount, _walletData.TokensCount);
                    break;
                default:
                    return;
            }
        }

        public bool HasMoney(WalletType walletType, float cost)
        {
            switch (walletType)
            {
                case WalletType.Cash when cost <= _walletData.CashCount:
                case WalletType.Tokens when cost <= _walletData.TokensCount:
                    return true;
                default:
                    return false;
            }
        }
    }
}