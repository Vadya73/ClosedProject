using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.RouletteWheel
{
    public class WheelOfFortuneView : BaseView<IData>
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _spinButton;
        [SerializeField] private TMP_Text _cooldownTextUI;
        public Button ExitButton => _exitButton;
        public Button SpinButton => _spinButton;
        public TMP_Text CooldownTextUI => _cooldownTextUI;

        public void UpdateCooldownTextUI(string cooldown)
        {
            _cooldownTextUI.text = cooldown;
        }
    }
}