using Services;
using Sirenix.OdinInspector;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Meta.Shop
{
    public class ShopView : BaseView<IData>
    {
        [SerializeField, ChildGameObjectsOnly] private Button _exitButton;
        [Header("Cards")]
        [SerializeField, ChildGameObjectsOnly] private Button _cardsButton;
        [SerializeField, ChildGameObjectsOnly] private Image _cardsButtonImage;
        [SerializeField, ChildGameObjectsOnly] private GameObject _cardsPanel;
        [Header("Sets")]
        [SerializeField, ChildGameObjectsOnly] private Button _setsButton;
        [SerializeField, ChildGameObjectsOnly] private Image _setsButtonImage;
        [SerializeField, ChildGameObjectsOnly] private GameObject _setsPanel;
        [Header("Cash")]
        [SerializeField, ChildGameObjectsOnly] private Button _cashButton;
        [SerializeField, ChildGameObjectsOnly] private Image _cashButtonImage;
        [SerializeField, ChildGameObjectsOnly] private GameObject _cashPanel;
        [Header("Colors")]
        [SerializeField] private Color _backgroundColor;
        [SerializeField] private Color _buttonColor ;
        [Header("Wallets")]
        [SerializeField] private TMP_Text _cashText;
        [SerializeField] private TMP_Text _tokensText;
        [SerializeField] private TMP_Text _greenCardText;
        [SerializeField] private TMP_Text _purpleCardText;
        
        private WalletView _walletView;

        public Button ExitButton => _exitButton;
        public Button CardsButton => _cardsButton;
        public Image CardsButtonImage => _cardsButtonImage;
        public Button SetsButton => _setsButton;
        public Image SetsButtonImage => _setsButtonImage;
        public Button CashButton => _cashButton;
        public Image CashButtonImage => _cashButtonImage;
        public GameObject CardsPanel => _cardsPanel;
        public GameObject SetsPanel => _setsPanel;
        public GameObject CashPanel => _cashPanel;
        
        public Color BackgroundColor => _backgroundColor;
        public Color ButtonColor => _buttonColor;

        [Inject]
        private void Construct(WalletView walletView)
        {
            _walletView = walletView;
        }

        public void UpdateWallets(WalletData walletWalletData)
        {
            _cashText.text =_walletView.FormatCurrency(walletWalletData.CashCount);
            _tokensText.text = _walletView.FormatCurrency(walletWalletData.TokensCount);
            _greenCardText.text = _walletView.FormatCurrency(walletWalletData.GreenCardCount);
            _purpleCardText.text = _walletView.FormatCurrency(walletWalletData.PurpleCardCount);
        }
    }
}
