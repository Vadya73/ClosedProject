using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelUI : BaseView<IData>
    {
        [SerializeField, ChildGameObjectsOnly] private Button _productionUIButton;
        [SerializeField, ChildGameObjectsOnly] private Button _researchUIButton;
        [SerializeField, ChildGameObjectsOnly] private Button _pauseMenuButton;
        [SerializeField, ChildGameObjectsOnly] private Button _rouletteButton;
        [SerializeField, ChildGameObjectsOnly] private Button _shopButton;
        [SerializeField, ChildGameObjectsOnly] private Button _teamSystemButton;
        [SerializeField, ChildGameObjectsOnly] private Button _allLeaderboardsButton;
        [SerializeField, ChildGameObjectsOnly] private Button _settingsButton;
        [SerializeField] private GameObject _updatableUI;
        
        public Button ProductionUIButton => _productionUIButton;
        public Button ResearchUIButton => _researchUIButton;
        public Button PauseMenuButton => _pauseMenuButton;
        public Button RouletteButton => _rouletteButton;
        public Button ShopButton => _shopButton;
        public Button TeamSystemButton => _teamSystemButton;
        public Button AllLeaderboardsButton => _allLeaderboardsButton;
        public Button SettingsButton => _settingsButton;
        public GameObject UpdatableUI => _updatableUI;
    }
}
