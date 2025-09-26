using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer;

namespace Meta.Shop
{
    public class ShopCardView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private ShopCardConfig _cardConfig;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _costText;
        
        private ShopController _shopController;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!_cardConfig)
                return;

            _costText.text = $"{_cardConfig.CostInDollars}$";
            _iconImage.sprite = _cardConfig.Icon;
            _backgroundImage.color = _cardConfig.BackgroundColor;
        }
#endif

        [Inject]
        private void Construct(ShopController shopController)
        {
            _shopController = shopController;
        }

        private void Start()
        {
            if (!_cardConfig)
                return;

            _costText.text = $"{_cardConfig.CostInDollars}$";
            _iconImage.sprite = _cardConfig.Icon;
            _backgroundImage.color = _cardConfig.BackgroundColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _shopController.TryPurchase(_cardConfig);
        }
    }
}