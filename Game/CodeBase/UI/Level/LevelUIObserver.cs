using System;
using Meta;
using VContainer.Unity;

namespace UI
{
    public class LevelUIObserver : IInitializable, IDisposable
    {
        private readonly LevelUI _levelUI;
        private readonly LevelUIController _levelUIController;
        private readonly DialogueController _dialogueController;

        public LevelUIObserver(LevelUI levelUI, LevelUIController levelUIController, DialogueController dialogueController)
        {
            _levelUI = levelUI;
            _levelUIController = levelUIController;
            _dialogueController = dialogueController;
        }

        public void Initialize()
        {
            _dialogueController.OnDialogueStart += HideUI;
            _dialogueController.OnDialogueEnd += ShowUI;
            
            _levelUI.ProductionUIButton.onClick.AddListener(ShowProductionView);
            _levelUI.PauseMenuButton.onClick.AddListener(ShowPauseMenu);
            _levelUI.RouletteButton.onClick.AddListener(ShowRouletteView);
            _levelUI.ResearchUIButton.onClick.AddListener(ShowResearchView);
            _levelUI.ShopButton.onClick.AddListener(ShowShopView);
            _levelUI.TeamSystemButton.onClick.AddListener(ShowTeamSystemView);
            _levelUI.AllLeaderboardsButton.onClick.AddListener(ShowAllLeaderboardsView);
            _levelUI.SettingsButton.onClick.AddListener(ShowSettingsView);
        }

        public void Dispose()
        {
 
            _dialogueController.OnDialogueStart -= HideUI;
            _dialogueController.OnDialogueEnd -= ShowUI;
            
            _levelUI.ProductionUIButton.onClick.RemoveListener(ShowProductionView);
            _levelUI.PauseMenuButton.onClick.RemoveListener(ShowPauseMenu);
            _levelUI.RouletteButton.onClick.RemoveListener(ShowRouletteView);
        }

        private void ShowSettingsView()
        {
            _levelUIController.ShowSettings();
        }

        private void ShowAllLeaderboardsView()
        {
            _levelUIController.ShowAllLeaderboardsView();
        }

        private void ShowTeamSystemView()
        {
            _levelUIController.ShowTeamSystemView();
        }

        private void ShowShopView()
        {
            _levelUIController.ShowShopView();
        }

        private void ShowRouletteView()
        {
            _levelUIController.ShowRouletteView();
        }

        private void ShowUI()
        {
            _levelUIController?.ShowUI();
        }

        private void HideUI()
        {
            _levelUIController?.HideUI();
        }

        private void ShowProductionView()
        {
            _levelUIController.ShowProductionView();
        }

        private void ShowResearchView()
        {
            _levelUIController.ShowProductionResearchView();
        }

        private void ShowPauseMenu()
        {
            _levelUIController.ShowPauseMenu();
        }
    }
}