using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.RouletteWheel
{
    public class WheelSectorView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _amountText;
        
        public Image Icon => _icon;
        public TMP_Text AmountText => _amountText;

        public void Setup(Sprite icon, int amount)
        {
            _icon.sprite = icon;
            _amountText.text = $"x{amount}";
        }
    }
}