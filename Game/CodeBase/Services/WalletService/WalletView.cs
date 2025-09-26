using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Services
{
    public class WalletView : BaseView<WalletData>
    {
        [Header("Change Wallet Component")]
        [SerializeField] private ChangeWallet _changeWallet;
        [Header("Cash")]
        [SerializeField, ChildGameObjectsOnly] private Image _moneyImage;
        [SerializeField, ChildGameObjectsOnly] private TMP_Text _moneyText;
        [Header("Tokens")]
        [SerializeField, ChildGameObjectsOnly] private Image _diamondsImage;
        [SerializeField, ChildGameObjectsOnly] private TMP_Text _tokensText;
        [Header("Green Cards")]
        [SerializeField, ChildGameObjectsOnly] private Image _greenCardImage;
        [SerializeField, ChildGameObjectsOnly] private TMP_Text _greenCardText;
        [Header("Purple Cards")]
        [SerializeField, ChildGameObjectsOnly] private Image _purpleCardImage;
        [SerializeField, ChildGameObjectsOnly] private TMP_Text _purpleCardText;

        public override void UpdateView(WalletData taskData)
        {
            _moneyText.text = FormatCurrency(taskData.CashCount);
            _tokensText.text = FormatCurrency(taskData.TokensCount);
            _greenCardText.text = FormatCurrency(taskData.GreenCardCount);
            _purpleCardText.text = FormatCurrency(taskData.PurpleCardCount);
        }

        public void UpdateView(WalletType walletType, float oldCount, float newCount)
        {
            switch (walletType)
            {
                case WalletType.Cash:
                    DOTween.To(() => oldCount, _ => _moneyText.text = FormatCurrency(_), newCount, 1f);
                    break;
                case WalletType.Tokens:
                    DOTween.To(() => oldCount, _ => _tokensText.text = FormatCurrency(_), newCount, 1f);
                    break;
                case WalletType.GreenCard:
                    DOTween.To(() => oldCount, _ => _greenCardText.text = FormatCurrency(_), newCount, 1f);
                    break;
                case WalletType.PurpleCard:
                    DOTween.To(() => oldCount, _ => _purpleCardText.text = FormatCurrency(_), newCount, 1f);
                    break;
            }
        }

        public void ShowChangeWalletPopUp(WalletType walletType, float count)
        {
            _changeWallet.ShowChangeWallet(walletType, count);
        }
        
        public string FormatCurrency(float amount)
        {
            return amount switch
            {
                >= 1000000000 => (amount / 1000000000f).ToString("0.##") + "B",
                >= 1000000 => (amount / 1000000f).ToString("0.##") + "M",
                >= 10000 => (amount / 1000f).ToString("0.##") + "k",
                _ => amount.ToString("0")
            };
        }
    }
}