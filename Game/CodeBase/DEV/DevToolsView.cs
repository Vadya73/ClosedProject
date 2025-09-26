using UI;
using UnityEngine;
using UnityEngine.UI;

namespace DEV
{
    public class DevToolsView : BaseView<IData>
    {
        [SerializeField] private Button _devButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _1lvlButton;
        [SerializeField] private Button _2lvlButton;
        [SerializeField] private Button _3lvlButton;
        [SerializeField] private Button _4lvlButton;
        [SerializeField] private Button _add100CashButton;
        [SerializeField] private Button _add1000CashButton;
        [SerializeField] private Button _add10000CashButton;
        [SerializeField] private Button _timex1Button;
        [SerializeField] private Button _timex2Button;
        [SerializeField] private Button _timex3Button;
        
        public Button DevButton => _devButton;
        public Button ExitButton => _exitButton;
        public Button OneLevelButton => _1lvlButton;
        public Button TwoLevelButton => _2lvlButton;
        public Button ThreeLevelButton => _3lvlButton;
        public Button FourLevelButton => _4lvlButton;
        public Button Add100CashButton => _add100CashButton;
        public Button Add1000CashButton => _add1000CashButton;
        public Button Add10000CashButton => _add10000CashButton;
        public Button Timex1Button => _timex2Button;
        public Button Timex2Button => _timex2Button;
        public Button Timex3Button => _timex3Button;
    }
}
