using System;
using UI;
using UnityEngine;

namespace Services
{
    [Serializable]
    public class WalletData :  IData
    {
        [SerializeField] private long _cashCount;
        [SerializeField] private long _tokensCount;
        [SerializeField] private long _greenCardCount;
        [SerializeField] private long _purpleCardCount;
        [SerializeField] private Sprite _cashIcon;
        [SerializeField] private Sprite _tokensIcon;
        [SerializeField] private Sprite _mythicalIcon;
        [SerializeField] private Sprite _legendaryIcon;
        

        public long CashCount => _cashCount;
        public long TokensCount => _tokensCount;
        public long GreenCardCount => _greenCardCount;
        public long PurpleCardCount => _purpleCardCount;
        public Sprite CashIcon => _cashIcon;
        public Sprite TokensIcon => _tokensIcon;
        public Sprite MythicalIcon => _mythicalIcon;
        public Sprite LegendaryIcon => _legendaryIcon;

        public void SetMoney(long moneyValue) => _cashCount = moneyValue;

        public void SetDiamonds(long diamondsValue) => _tokensCount = diamondsValue;

        public void AddWallet(WalletType walletType, long count)
        {
            switch (walletType)
            {
                case WalletType.Cash:
                    _cashCount += count;
                    break;
                case WalletType.Tokens:
                    _tokensCount += count;
                    break;                
                case WalletType.GreenCard:
                    _greenCardCount += count;
                    break;
                case WalletType.PurpleCard:
                    _purpleCardCount += count;
                    break;
            }
        }

        public void SpendWallet(WalletType walletType, long count)
        {
            switch (walletType)
            {
                case WalletType.Cash:
                    _cashCount -= count;
                    break;
                case WalletType.Tokens:
                    _tokensCount -= count;
                    break;
                case WalletType.GreenCard:
                    _greenCardCount -= count;
                    break;
                case WalletType.PurpleCard:
                    _purpleCardCount -= count;
                    break;
            }
        }

        public Sprite GetIcon(WalletType walletType)
        {
            return walletType switch
            {
                WalletType.Cash => CashIcon,
                WalletType.Tokens => TokensIcon,
                WalletType.GreenCard => MythicalIcon,
                WalletType.PurpleCard => LegendaryIcon,
                _ => null
            };
        }
    }
}