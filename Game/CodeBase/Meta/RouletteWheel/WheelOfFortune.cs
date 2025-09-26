using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.RouletteWheel
{
    public class WheelOfFortune : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private WheelConfig _config;
        [SerializeField] private RectTransform _wheelTransform;
        [SerializeField] private Button _spinButton;
        [SerializeField] private TMP_Text _cooldownText;
        
        public WheelConfig Config => _config;
        public RectTransform WheelTransform => _wheelTransform;
        public Button SpinButton => _spinButton;
        public TMP_Text CooldownText => _cooldownText;

        public void UpdateCooldownText(string toString)
        {
            _cooldownText.text = toString;
        }
    }
}
