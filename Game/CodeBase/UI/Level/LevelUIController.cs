using Core;
using Meta;
using Meta.RouletteWheel;
using Meta.Shop;
using UI.Settings;
using UnityEngine;
using VContainer;

namespace UI
{
    public class LevelUIController
    {
        private readonly LevelUI _levelUI;
        private readonly WheelOfFortuneView _wheelOfFortuneView;
        private readonly ShopController _shopController;
        private readonly TeamSystemController _teamSystemController;
        private readonly LeaderboardController _leaderboardController;
        private readonly SettingsController _settingsController;
        private readonly ProductionController _productionController;
        private readonly GameObject _updatableUI;
        
        [Inject]
        public LevelUIController(ProductionController productionController, LevelUI levelUI, WheelOfFortuneView wheelOfFortuneView,
            ShopController shopController, TeamSystemController teamSystemController, LeaderboardController leaderboardController,
            SettingsController settingsController)
        {
            _productionController = productionController;
            _levelUI = levelUI;
            _wheelOfFortuneView = wheelOfFortuneView;
            _shopController = shopController;
            _teamSystemController = teamSystemController;
            _leaderboardController = leaderboardController;
            _settingsController = settingsController;
            _updatableUI = _levelUI.UpdatableUI;
        }

        public void ShowProductionView()
        {
            _productionController.ShowMainAndBRView();
        }

        public void ShowPauseMenu()
        {
        }

        public void ShowUI()
        {
            _levelUI?.ForcedShow();
            _updatableUI.SetActive(true);
        }

        public void HideUI()
        {
            _levelUI.ForcedHide();
            _updatableUI.SetActive(false);
        }

        public void ShowRouletteView()
        {
            if (_wheelOfFortuneView.IsActive)
            {
                _wheelOfFortuneView.Hide();
                return;
            }
            
            _wheelOfFortuneView.Show();
        }

        public void ShowProductionResearchView()
        {
            _productionController.ShowMainAndResearchView();
        }

        public void ShowShopView()
        {
            _shopController.ShowView();
        }

        public void ShowTeamSystemView()
        {
            _teamSystemController.ShowView();
        }

        public void ShowAllLeaderboardsView()
        {
            _leaderboardController.ShowAllLeaderboards();
        }

        public void ShowSettings()
        {
            _settingsController.ShowView();
        }
    }
}